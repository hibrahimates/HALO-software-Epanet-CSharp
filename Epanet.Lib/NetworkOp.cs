using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epanet
{
    class NetworkOp
    {
        public string f1 = "todini.inp";
        public static void StartSimulation()
        {
            string f1 = "todini.inp";         //Defining the INP file
            string f2 = "outDos.txt";
            string f3 = "repDos2.txt";
            UnsafeNativeMethods.ENopen(f1, f2, f3);     //Opening the INP file
            UnsafeNativeMethods.ENgetcount(CountType.Link, out int numofPipe);
            UnsafeNativeMethods.ENgetcount(CountType.Node, out int numofNode);
            UnsafeNativeMethods.ENsolveH();  // Solving hydraulically
        }
        public static void OnlySave()
        {
            UnsafeNativeMethods.ENsaveinpfile("todini.inp");
        }
        public static void SaveAndRun()
        {
            UnsafeNativeMethods.ENsaveinpfile("todini.inp");
            UnsafeNativeMethods.ENsolveH();  // Solving hydraulically

        }


    }
}
