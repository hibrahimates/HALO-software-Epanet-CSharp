// 
// Epanet -- Epanet2 Toolkit hydraulics library C# Interface
//                                                                    
// ExcelReport.cs -- Sample program to demostrate writing epanet 2 report to Excel workbook.
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
using System.IO;
using System.Text;

namespace Epanet.Examples {

    /// <summary>This class runs EPANET simulation and writes results to XLSX file.</summary>
    /// <remarks>Ported from EPANet Baseform Java library http://baseform.org/</remarks>
    internal static class ExcelReport {
        public static void Run(string inFile) {
            string logFile = Path.ChangeExtension(inFile, "log");
            // string outFile = Path.ChangeExtension(inFile, "bin");

            // parameter 3 should NOT be null! epanet2.dll does not check it for NULL.
            EpanetException.Check(UnsafeNativeMethods.ENopen(inFile, logFile, string.Empty));

            try {
                using(var workbook = new XlsxWriter()) {
                    int nodeCount, linkCount;
                    EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Node, out nodeCount));
                    EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Link, out linkCount));

                    // set string builder buffer size to be at least MAXID chars 
                    var sb = new StringBuilder(UnsafeNativeMethods.MAXID);

                    var nodesHead = new object[nodeCount + 1];
                    nodesHead[0] = "Time/Node";

                    // Fill list of nodes IDs
                    for (int i = 1; i <= nodeCount; i++) {
                        EpanetException.Check(UnsafeNativeMethods.ENgetnodeid(i, sb));
                        nodesHead[i] = sb.ToString();
                    }

                    var linksHead = new object[linkCount + 1];
                    linksHead[0] = "Time/Link";

                    // Fill list of links IDs
                    for (int i = 1; i <= linkCount; i++) {
                        EpanetException.Check(UnsafeNativeMethods.ENgetlinkid(i, sb));
                        linksHead[i] = sb.ToString();
                    }

                    // Sheet definitions
                    var sheetsList = new[] {
                        new {text = "Node head", isNode = true},
                        new {text = "Node actual demand", isNode = true},
                        new {text = "Node pressure", isNode = true},
                        new {text = "Link flows", isNode = false},
                        new {text = "Link velocity", isNode = false},
                        new {text = "Link unit headloss", isNode = false},
                        new {text = "Link setting", isNode = false}
                    };

                    // Fill workbook with sheets 
                    for (int i = 0; i < sheetsList.Length; i++) {
                        workbook.AddSheet(sheetsList[i].text);
                        workbook[i].AddRow(sheetsList[i].isNode ? nodesHead : linksHead);
                    }

                    var nodeRow = new object[nodeCount + 1];
                    var linkRow = new object[linkCount + 1];

                    EpanetException.Check(UnsafeNativeMethods.ENopenH());
                    EpanetException.Check(UnsafeNativeMethods.ENinitH(SaveOptions.None));

                    int tstep;
                    do {
                        int t;
                        EpanetException.Check(UnsafeNativeMethods.ENrunH(out t));
                        TimeSpan span = TimeSpan.FromSeconds(t);

                        // Set first cell to current run time
                        nodeRow[0] =
                            linkRow[0] =
                                string.Format("{0:d2}:{1:d2}:{2:d2}", (int)span.TotalHours, span.Minutes, span.Seconds);

                        WriteStep(workbook, nodeRow, linkRow);

                        EpanetException.Check(UnsafeNativeMethods.ENnextH(out tstep));
                    }
                    while (tstep > 0);

                    workbook.Save(Path.ChangeExtension(inFile, "xlsx"));
                }
            }
            finally {
                EpanetException.Check(UnsafeNativeMethods.ENclose());
            }
        }

        /// <summary>Retrivies single time step data from EPANET and writes it to workbook.</summary>
        private static void WriteStep(XlsxWriter workbook, object[] nodeRow, object[] linkRow) {
            float value;

            // NODES HEADS
            for (int i = 1; i < nodeRow.Length; i++) {
                EpanetException.Check(UnsafeNativeMethods.ENgetnodevalue(i, NodeValue.Head, out value));
                nodeRow[i] = value;
            }

            workbook[0].AddRow(nodeRow);

            // NODES DEMANDS
            for (int i = 1; i < nodeRow.Length; i++) {
                EpanetException.Check(UnsafeNativeMethods.ENgetnodevalue(i, NodeValue.Demand, out value));
                nodeRow[i] = value;
            }

            workbook[1].AddRow(nodeRow);

            // NODES PRESSURE
            for (int i = 1; i < nodeRow.Length; i++) {
                EpanetException.Check(UnsafeNativeMethods.ENgetnodevalue(i, NodeValue.Pressure, out value));
                nodeRow[i] = value;
            }

            workbook[2].AddRow(nodeRow);

            // LINK FLOW
            for (int i = 1; i < linkRow.Length; i++) {
                EpanetException.Check(UnsafeNativeMethods.ENgetlinkvalue(i, LinkValue.Flow, out value));
                linkRow[i] = value;
            }

            workbook[3].AddRow(linkRow);

            // LINK VELOCITY
            for (int i = 1; i < linkRow.Length; i++) {
                EpanetException.Check(UnsafeNativeMethods.ENgetlinkvalue(i, LinkValue.Velocity, out value));
                linkRow[i] = value;
            }

            workbook[4].AddRow(linkRow);

            // LINK HEADLOSS
            for (int i = 1; i < linkRow.Length; i++) {
                EpanetException.Check(UnsafeNativeMethods.ENgetlinkvalue(i, LinkValue.HeadLoss, out value));
                linkRow[i] = value;
            }

            workbook[5].AddRow(linkRow);

            // LINK FRICTION
            for (int i = 1; i < linkRow.Length; i++) {
                EpanetException.Check(UnsafeNativeMethods.ENgetlinkvalue(i, LinkValue.Setting, out value));
                linkRow[i] = value;
            }

            workbook[6].AddRow(linkRow);
        }
    }

}
