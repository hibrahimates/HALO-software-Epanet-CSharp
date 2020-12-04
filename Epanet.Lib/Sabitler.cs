using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epanet
{
    class Sabitler
    {
        public static float g = (float)9.81;
        public static float MinHead = (float)29.5;
        public static float[] Pipe_Diameters = new float[14]
        {
           (float)25.4  ,
           (float)50.8  ,
           (float)76.2  ,
           (float)101.6 ,
           (float)152.4 ,
           (float)203.2 ,
           (float)254.0 ,
           (float)304.8 ,
           (float)355.6 ,
           (float)406.4 ,
           (float)457.2 ,
           (float)508.0 ,
           (float)558.8,
           (float)609.6
        };

        public static float[] Pipe_Cost = new float[14]
        {
            2  ,
            5  ,
            8  ,
            11 ,
            16 ,
            23 ,
            32 ,
            50 ,
            60 ,
            90 ,
            130 ,
            170 ,
            300,
            550
        };
    }
}
