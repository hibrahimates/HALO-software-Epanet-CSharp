// 
// Epanet -- Epanet2 Toolkit hydraulics library C# Interface
//                                                                    
// Program.cs -- Sample program (Excel report) entry point
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
using System.Reflection;
using Epanet.Examples;

namespace Epanet {

    internal class Program {
        private static void Main(string[] args) {
            if (args == null || args.Length != 1 || String.IsNullOrEmpty(args[0])) {
                Error("Usage {0} file.inp", Path.GetFileName(Assembly.GetExecutingAssembly().Location));
                return;
            }

            string inFile = Path.GetFullPath(args[0]);

            if (!File.Exists(inFile)) {
                Error("File '{0}' does not exists.", inFile);
                return;
            }

            try {
                ExcelReport.Run(inFile);
            }
            catch (Exception ex) {
                Error(ex.Message);
                return;
            }

            if (EpanetException.Warnings.Count != 0) {
                Console.ForegroundColor = ConsoleColor.Yellow;

                foreach (ErrorCode err in EpanetException.Warnings) {
                    Console.Error.WriteLine(new EpanetException(err).Message);
                }

                Console.ResetColor();
            }

            Console.WriteLine("All done.");
        }

        private static void Error(string format, params object[] args) {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(format, args);
            Console.Error.WriteLine("Aborting.");
            Console.ResetColor();
        }
    }

}
