using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epanet
{
    class ReservoirOp
    {
        public static Dictionary<int, float> RezKotlari = new Dictionary<int, float>();
        public static Dictionary<int, float> Rezbosaltim = new Dictionary<int, float>();
        public float[] ResNodeElevations = new float[RezKotlari.Count];
        public float[] ResNodeDischarges = new float[RezKotlari.Count];

        public static String ChkNodeType = "Reservoir";
        public static float[] Get_ResElevations()
        {
            for (int i = 0; i < NodeOp.Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodetype(i + 1, out NodeType kontrol);
                if (kontrol.ToString() == ChkNodeType)
                {
                    UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Head, out float ResHead);
                    if (ResHead > 0)
                    {
                        if (RezKotlari.ContainsKey(i))
                        {
                            //null
                        }
                        else
                        {
                            RezKotlari.Add(i, ResHead);
                        }
                    }
                }
            }
            float[] ResNodeElevations = RezKotlari.Select(z => z.Value).ToArray();
            //float[] ResNodeElevations = RezKotlari.ToArray();
            return ResNodeElevations;
        }
        public static float[] Get_ResNodeDischarges()
        {

            for (int i = 0; i < NodeOp.Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodetype(i + 1, out NodeType kontrol);

                if (kontrol.ToString() == ChkNodeType)
                {
                    UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Head, out float ResHead);
                    UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Demand, out float ResDischarge);
                    if (ResHead > 0)
                    {
                        if (Rezbosaltim.ContainsKey(i))
                        {
                            //null
                        }
                        else
                        {
                            Rezbosaltim.Add(i, Math.Abs(ResDischarge));
                        }
                    }
                }
            }
            float[] ResNodeDischarges = Rezbosaltim.Select(z => z.Value).ToArray();
            return ResNodeDischarges;
        }
        public static float Calculate_TotalPowerofSystem()
        {
            float H_Q = 0;
            float[] resyukseklikleri = ReservoirOp.Get_ResElevations();
            float[] rezbosaltimlari = ReservoirOp.Get_ResNodeDischarges();
            for (int i = 0; i < resyukseklikleri.Length; i++)
            {
                float TotalPower = rezbosaltimlari[i] * resyukseklikleri[i] * (float)3.6;
                H_Q = H_Q + TotalPower;
            }
            return H_Q;
        }
    }
}
