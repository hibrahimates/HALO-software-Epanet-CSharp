// 
// Epanet -- Epanet2 Toolkit hydraulics library C# Interface
//                                                                    
// Dumper.cs -- Epanet 2 network dumper.
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
using System.Globalization;
using System.IO;
using System.Text;

namespace Epanet.Examples {

    /// <summary>Dumps EPANET network data to TextWriter / console.</summary>
    public static class Dumper {
        private const string NLFORMAT = "    {0}#{1}: ID={2}, Type={3}";
        public static void Dump() { Dump(Console.Out); }

        public static void Dump(TextWriter w) {
            DumpSummary(w);
            DumpNodes(w);
            DumpLinks(w);
            DumpControls(w);
            DumpPatterns(w);
            // DumpErrorMessages(w);
        }

        public static void DumpSummary(TextWriter w) {
            w.WriteLine();
            w.WriteLine("OPTIONS:");

            foreach (MiscOption option in Enum.GetValues(typeof(MiscOption))) {
                float value;
                EpanetException.Check(UnsafeNativeMethods.ENgetoption(option, out value));
                w.WriteLine("    {0}={1}", option, value);
            }

            w.WriteLine();
            w.WriteLine("TIME PARAMETERS:");

            foreach (TimeParameter param in Enum.GetValues(typeof(TimeParameter))) {
                int value;
                EpanetException.Check(UnsafeNativeMethods.ENgettimeparam(param, out value));
                w.WriteLine("    {0}={1}", param, value);
            }

            FlowUnitsType unit;
            EpanetException.Check(UnsafeNativeMethods.ENgetflowunits(out unit));
            w.WriteLine();
            w.WriteLine("FLOW UNIT: {0}", unit);

            QualType qualcode;
            int tracenode;
            EpanetException.Check(UnsafeNativeMethods.ENgetqualtype(out qualcode, out tracenode));

            w.WriteLine();
            w.WriteLine("QUALITY ANALYSIS: Type={0}, Trace node={1}", qualcode, tracenode);

            int count;

            EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Tank, out count));
            w.WriteLine("TANK COUNT:{0}", count);

            EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Curve, out count));
            w.WriteLine("CURVE COUNT:{0}", count);
        }

        public static void DumpNodes(TextWriter w) {
            int count;
            EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Node, out count));

            w.WriteLine();
            w.WriteLine("NODES ({0}):", count);

            for (int i = 1; i <= count; i++) {
                StringBuilder id = new StringBuilder(UnsafeNativeMethods.MAXID);
                EpanetException.Check(UnsafeNativeMethods.ENgetnodeid(i, id));

                NodeType type;
                EpanetException.Check(UnsafeNativeMethods.ENgetnodetype(i, out type));

                w.WriteLine(NLFORMAT, "NODE", i, id, type);

                foreach (NodeValue code in Enum.GetValues(typeof(NodeValue))) {
                    try {
                        float value;
                        EpanetException.Check(UnsafeNativeMethods.ENgetnodevalue(i, code, out value));
                        w.WriteLine("        {0}={1}", code, value);
                    }
                    catch (EpanetException) {
                        w.WriteLine("        {0}=N/A", code);
                    }
                }
            }
        }

        public static void DumpLinks(TextWriter w) {
            int count;
            EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Link, out count));

            w.WriteLine();
            w.WriteLine("LINKS ({0}):", count);

            for (int i = 1; i <= count; i++) {
                StringBuilder id = new StringBuilder(UnsafeNativeMethods.MAXID);
                EpanetException.Check(UnsafeNativeMethods.ENgetlinkid(i, id));

                LinkType type;
                EpanetException.Check(UnsafeNativeMethods.ENgetlinktype(i, out type));

                w.WriteLine(NLFORMAT, "LINK", i, id, type);

                int fnode, tnode;
                EpanetException.Check(UnsafeNativeMethods.ENgetlinknodes(i, out fnode, out tnode));

                w.WriteLine("        Start node={0}", fnode);
                w.WriteLine("        End   node={0}", tnode);

                foreach (LinkValue code in Enum.GetValues(typeof(LinkValue))) {
                    try {
                        float value;
                        EpanetException.Check(UnsafeNativeMethods.ENgetlinkvalue(i, code, out value));
                        w.WriteLine("        {0}={1}", code, value);
                    }
                    catch (EpanetException) {
                        w.WriteLine("        {0}=N/A", code);
                    }
                }
            }
        }

        public static void DumpPatterns(TextWriter w) {
            int count;
            EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Pattern, out count));

            for (int i = 1; i <= count; i++) {
                int len;
                StringBuilder id = new StringBuilder(UnsafeNativeMethods.MAXID);

                EpanetException.Check(UnsafeNativeMethods.ENgetpatternid(i, id));
                EpanetException.Check(UnsafeNativeMethods.ENgetpatternlen(i, out len));

                w.WriteLine();
                w.WriteLine("PATTERN {0}: Length={1}", id, len);

                string[] values = new string[len];

                for (int j = 1; j <= len; j++) {
                    float value;
                    EpanetException.Check(UnsafeNativeMethods.ENgetpatternvalue(i, j, out value));
                    values[j - 1] = value.ToString(NumberFormatInfo.InvariantInfo);
                }

                w.WriteLine("    [{0}]", string.Join(", ", values));
            }
        }

        public static void DumpControls(TextWriter w) {
            int count;

            EpanetException.Check(UnsafeNativeMethods.ENgetcount(CountType.Control, out count));

            for (int i = 1; i <= count; i++) {
                ControlType type;
                float settings, level;
                int lindex, nindex;
                EpanetException.Check(
                    UnsafeNativeMethods.ENgetcontrol(i, out type, out lindex, out settings, out nindex, out level));

                w.WriteLine();
                w.WriteLine("CONTROL: #{0}, Type={1}", i, type);
                w.WriteLine("    Link={0}", lindex);
                w.WriteLine("    Settings={0}", settings);
                w.WriteLine("    Node={0}", nindex);
                w.WriteLine("    Level={0}", level);
            }
        }

        public static void DumpErrorMessages(TextWriter w) {
            const int MSGLEN = UnsafeNativeMethods.MAXMSG * 2;
            w.WriteLine();
            w.WriteLine("ERROR MESSAGES:");

            StringBuilder sb = new StringBuilder(MSGLEN);

            foreach (ErrorCode err in Enum.GetValues(typeof(ErrorCode))) {
                if (err == ErrorCode.Ok) continue;
                try {
                    EpanetException.Check(UnsafeNativeMethods.ENgeterror(err, sb, MSGLEN));
                    w.WriteLine("    {0}: {1}", err, sb);
                }
                catch (EpanetException ex) {
                    if (ex.ErrorCode != ErrorCode.Err251) throw;
                    Console.Out.WriteLine("    {0}: <undefined>", err);
                }
            }
        }
    }

}
