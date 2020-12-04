// 
// Epanet -- Epanet2 Toolkit hydraulics library C# Interface
//                                                                    
// XLSXWriter.cs -- OpenXML Excel file creator
// 
// CREATED:    02/13/2014                                                                    
// VERSION:    2.00                                               
// DATE:         02/13/2014
//             
// AUTHOR:     slava           
// 
// This is free and unencumbered software released into the public domain.
// 
// Anyone is free to copy, modify, publish, use, compile, sell, or
// distribute this software, either in source code form or as a compiled
// binary, for any purpose, commercial or non-commercial, and by any
// means.
// 
// In jurisdictions that recognize copyright laws, the author or authors
// of this software dedicate any and all copyright interest in the
// software to the public domain. We make this dedication for the benefit
// of the public at large and to the detriment of our heirs and
// successors. We intend this dedication to be an overt act of
// relinquishment in perpetuity of all present and future rights to this
// software under copyright law.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
// IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR
// OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
// ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
// 
// For more information, please refer to <http://unlicense.org/>

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Packaging;
using System.Xml;

namespace Epanet.Examples
{

    /// <summary>Writes Excel OpenXML package file.</summary>
    /// <remarks>
    ///     Needs .NET Framework v3.0 or greater and reference to WindowsBase.dll
    ///     to export namespace System.IO.Packaging.
    /// </remarks>
    /// <remarks>Ported from EPANet Baseform Java library http://baseform.org/</remarks>
    /// <para>
    ///     See also
    ///     http://msdn.microsoft.com/en-us/library/documentformat.openxml.spreadsheet(v=office.14).aspx
    /// </para>
    internal class XlsxWriter : IDisposable
    {
        private const string SCHEMA_MAIN = "http://schemas.openxmlformats.org/spreadsheetml/2006/main";

        private const string NS_ROOT = "application/vnd.openxmlformats-officedocument.spreadsheetml";
        private const string RELATIONSHIP_ROOT = "http://schemas.openxmlformats.org/officeDocument/2006/relationships";

        private const string NS_WORKBOOK = NS_ROOT + ".sheet.main+xml";
        private const string RELATIONSHIP_WORKBOOK = RELATIONSHIP_ROOT + "/officeDocument";

        private const string NS_WORKSHEET = NS_ROOT + ".worksheet+xml";
        private const string RELATIONSHIP_WORKSHEET = RELATIONSHIP_ROOT + "/worksheet";

        private const string NS_SHARED_STRINGS = NS_ROOT + ".sharedStrings+xml";
        private const string RELATIONSHIP_SHAREDSTRINGS = RELATIONSHIP_ROOT + "/sharedStrings";

        private const int BUFFER_SIZE = 1024 * 8;

        private readonly List<string> _sharedStrings = new List<string>();
        private readonly List<Spreadsheet> _sheets = new List<Spreadsheet>();

        public Spreadsheet this[int index] { get { return this._sheets[index]; } }
        //public Spreadsheet this[string name] { get { return this._sheets.Find(x => x.Name == name); } }

        public void Dispose()
        {
            foreach (var x in this._sheets) { if (x != null) ((IDisposable)x).Dispose(); }
            this._sheets.Clear();
        }

        private static void WriteWorksheet(Package p, Spreadsheet sheet, int pos)
        {
            string sPos = pos.ToString(NumberFormatInfo.InvariantInfo);
            string name = string.Format("xl/worksheets/sheet{0}.xml", sPos);
            Uri uri = PackUriHelper.CreatePartUri(new Uri(name, UriKind.Relative));
            PackagePart wsPart = p.CreatePart(uri, NS_WORKSHEET, CompressionOption.Maximum);
            sheet.Save(wsPart.GetStream(FileMode.Create, FileAccess.Write));
            //Create the relationship for the workbook part.
            Uri bookUri = PackUriHelper.CreatePartUri(new Uri("xl/workbook.xml", UriKind.Relative));
            PackagePart bookPart = p.GetPart(bookUri);
            bookPart.CreateRelationship(uri, TargetMode.Internal, RELATIONSHIP_WORKSHEET, "rId" + sPos);
        }

        private void WriteSharedStringsXml(Package p)
        {
            int count = 0;

            foreach (var x in this._sheets) { count += x.WordCount; }

            Uri uri = PackUriHelper.CreatePartUri(new Uri("xl/sharedStrings.xml", UriKind.Relative));

            //Create the workbook part.
            PackagePart part = p.CreatePart(uri, NS_SHARED_STRINGS, CompressionOption.Maximum);

            //Write the workbook XML to the workbook part.
            //Create a new XML document for the sharedStrings.
            XmlWriter writer = XmlWriter.Create(part.GetStream(FileMode.Create, FileAccess.Write));

            //Get a reference to the root node, and then add the XML declaration.
            writer.WriteStartDocument(true);

            //Create and append the sst node.
            writer.WriteStartElement("sst", SCHEMA_MAIN);
            //writer.WriteAttributeString("xmlns", SCHEMA_MAIN);
            writer.WriteAttributeString("count", count.ToString(NumberFormatInfo.InvariantInfo));
            writer.WriteAttributeString(
                "uniqueCount",
                this._sharedStrings.Count.ToString(NumberFormatInfo.InvariantInfo));

            foreach (string s in this._sharedStrings)
            {
                //Create and append the si node.
                writer.WriteStartElement("si");
                //Create and append the t node.
                writer.WriteElementString("t", s);
                writer.WriteEndElement();
            }

            // writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();

            //Create the relationship for the workbook part.
            Uri bookUri = PackUriHelper.CreatePartUri(new Uri("xl/workbook.xml", UriKind.Relative));
            PackagePart bookPart = p.GetPart(bookUri);
            bookPart.CreateRelationship(
                uri,
                TargetMode.Internal,
                RELATIONSHIP_SHAREDSTRINGS,
                "rId" + (this._sheets.Count + 2));
        }

        private void WriteWorkbookXml(Package p)
        {
            Uri uri = PackUriHelper.CreatePartUri(new Uri("xl/workbook.xml", UriKind.Relative));
            PackagePart part = p.CreatePart(uri, NS_WORKBOOK, CompressionOption.Maximum);

            //Create a new XML document for the workbook.
            XmlWriter writer = XmlWriter.Create(part.GetStream(FileMode.Create, FileAccess.Write));

            //Obtain a reference to the root node, and then add the XML declaration.
            writer.WriteStartDocument(true);

            //Create and append the workbook node to the document.
            writer.WriteStartElement("workbook", SCHEMA_MAIN);
            writer.WriteAttributeString("xmlns", "r", null, RELATIONSHIP_ROOT);

            //Create and append the sheets node to the workBook node.
            writer.WriteStartElement("sheets");

            for (int i = 1; i <= this._sheets.Count; i++)
            {
                string sid = i.ToString(NumberFormatInfo.InvariantInfo);
                //Create and append the sheet node to the sheets node.
                writer.WriteStartElement("sheet");
                writer.WriteAttributeString("name", this._sheets[i - 1].Name);
                writer.WriteAttributeString("sheetId", sid);
                writer.WriteAttributeString("id", RELATIONSHIP_ROOT, "rId" + sid);
                writer.WriteEndElement();
            }

            // writer.WriteEndElement();
            // writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Flush();
            writer.Close();

            p.CreateRelationship(uri, TargetMode.Internal, RELATIONSHIP_WORKBOOK, "rId1");
        }

        public void Save(Stream outputStream)
        {
            using (var p = Package.Open(outputStream, FileMode.Create))
            {
                this.Save(p);
                p.Close();
            }
        }

        public void Save(string outputFile)
        {
            using (var p = Package.Open(outputFile, FileMode.Create))
            {
                this.Save(p);
                p.Close();
            }
        }

        private void Save(Package p)
        {
            this.WriteWorkbookXml(p);
            this.WriteSharedStringsXml(p);

            for (int i = 0; i < this._sheets.Count; i++)
            {
                this._sheets[i].Finish();
                WriteWorksheet(p, this._sheets[i], i + 1);
            }

            p.PackageProperties.Creator = this.GetType().Name;
            p.PackageProperties.Created = DateTime.Now;
        }

        public void AddSheet(string name) { this._sheets.Add(new Spreadsheet(name, this)); }

        public void Clear()
        {
            this._sheets.Clear();
            this._sharedStrings.Clear();
        }

        public class Spreadsheet : IDisposable
        {
            private readonly string _name;
            private readonly List<string> _sharedStrings;
            private readonly XmlWriter _writer;
            private int _rowsAdded = 1;
            private FileStream _stream;

            internal Spreadsheet(string name, XlsxWriter workbook)
            {
                this._name = name;
                this._sharedStrings = workbook._sharedStrings;
                string tmpFile = Path.GetTempFileName();

                // Give OS a hint to cache file in memory, if possible
                File.SetAttributes(tmpFile, FileAttributes.Temporary);
                //Delete on close
                this._stream = new FileStream(
                    tmpFile,
                    FileMode.Open,
                    FileAccess.ReadWrite,
                    FileShare.Read,
                    BUFFER_SIZE,
                    FileOptions.DeleteOnClose);

                this._writer = XmlWriter.Create(this._stream);
                this.InitXmlDoc();
            }

            public int WordCount { get; private set; }
            public string Name { get { return this._name; } }

            void IDisposable.Dispose()
            {
                if (this._writer.WriteState != WriteState.Closed)
                {
                    this._writer.Close();
                }

                if (this._stream != null)
                {
                    this._stream.Dispose();
                    this._stream = null;
                }
            }

            public void Save(Stream outStream)
            {
                if (this._stream == null) throw new ObjectDisposedException(this.GetType().Name);
                byte[] buff = new byte[BUFFER_SIZE];
                this._stream.Position = 0;
                int count;

                while ((count = this._stream.Read(buff, 0, BUFFER_SIZE)) > 0)
                {
                    outStream.Write(buff, 0, count);
                }
            }

            private void InitXmlDoc()
            {
                //Get a reference to the root node, and then add the XML declaration.
                this._writer.WriteStartDocument(true);

                //Create and append the worksheet node to the document.
                this._writer.WriteStartElement("worksheet", SCHEMA_MAIN);
                this._writer.WriteAttributeString("xmlns", "r", null, RELATIONSHIP_ROOT);

                //Create and add the sheetData node.
                this._writer.WriteStartElement("sheetData");
            }

            public void Finish()
            {
                if (this._stream == null) throw new ObjectDisposedException(this.GetType().Name);

                // this._writer.WriteEndElement(); // sheetData
                // this._writer.WriteEndElement(); // worksheet

                this._writer.WriteEndDocument();
                // this._writer.Flush();
                this._writer.Close();
            }

            private static string GetColumnName(int index)
            {
                const string COLUMN_NAMES = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

                index -= 1;

                int quotient = index / COLUMN_NAMES.Length;

                return quotient > 0
                    ? GetColumnName(quotient) + COLUMN_NAMES[index % 26]
                    : new string(COLUMN_NAMES[index % 26], 1);
            }

            public void AddRow(params object[] row)
            {
                if (this._stream == null) throw new ObjectDisposedException(this.GetType().Name);
                string rowName = this._rowsAdded.ToString(NumberFormatInfo.InvariantInfo);
                //Create and add the row node. 
                this._writer.WriteStartElement("row");
                this._writer.WriteAttributeString("r", rowName);
                this._writer.WriteAttributeString("spans", "1:" + row.Length.ToString(NumberFormatInfo.InvariantInfo));

                int col = 0;

                foreach (object o in row)
                {
                    string cellAddr = GetColumnName(col + 1) + rowName;
                    this.AddCell(o, cellAddr);

                    col++;
                }

                this._writer.WriteEndElement();
                this._rowsAdded++;
            }

            private static string NumberToStringInvariant(object value)
            {
                // if (!(value is ValueType)) return false;

                if (value is IConvertible)
                {
                    var v = value as IConvertible;

                    switch (v.GetTypeCode())
                    {
                        case TypeCode.SByte:
                        case TypeCode.Byte:
                        case TypeCode.Int16:
                        case TypeCode.UInt16:
                        case TypeCode.Int32:
                        case TypeCode.UInt32:
                        case TypeCode.Int64:
                        case TypeCode.UInt64:
                        case TypeCode.Single:
                        case TypeCode.Double:
                        case TypeCode.Decimal:
                            return v.ToString(NumberFormatInfo.InvariantInfo);

                        default:
                            return null;
                    }
                }

                return null;
            }

            private void AddCell(object o, string cellAddr)
            {
                string type, value = NumberToStringInvariant(o);

                if (value != null)
                {
                    type = "n";
                }
                else
                {
                    string s = o == null ? string.Empty : o.ToString();

                    int idx = this._sharedStrings.IndexOf(s);
                    if (idx < 0)
                    {
                        this._sharedStrings.Add(s);
                        idx = this._sharedStrings.IndexOf(s);
                    }

                    this.WordCount++;

                    type = "s";
                    value = idx.ToString(NumberFormatInfo.InvariantInfo);
                }

                //Create and add the column node.
                this._writer.WriteStartElement("c");
                this._writer.WriteAttributeString("r", cellAddr);
                this._writer.WriteAttributeString("t", type);

                //Add the dataValue text to the worksheet.
                this._writer.WriteElementString("v", value);

                this._writer.WriteEndElement();
            }
        }
    }

}
