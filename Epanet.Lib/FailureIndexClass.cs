using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epanet
{
    class FailureIndexClass
    {
        public static float Calculate_Failureindex()
        {
            float[] minDeger = new float[NodeOp.Get_numofNode()];
            float[] Pressurelar = NodeOp.Get_Pressure();
            for (int i = 0; i < NodeOp.Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodetype(i + 1, out NodeType kontrol);
                if (kontrol.ToString() != ReservoirOp.ChkNodeType)//reservuar control
                {
                    minDeger[i] = Pressurelar[i] - Sabitler.MinHead;
                }
            }
            float minimumDeger = minDeger.Where(a => a > 0).Min();
            return minimumDeger;
        }
    }
}