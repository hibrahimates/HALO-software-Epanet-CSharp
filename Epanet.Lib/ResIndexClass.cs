using System;
using System.IO;
using System.Reflection;
using Epanet.Examples;
using System.Data.OleDb;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
namespace Epanet
{
    class ResIndexClass
    {
        public static float Kullanıcı_ResilienceIndex;
        public static void SetResindex()
        {
            Console.WriteLine("Resilience Index: ");//Kullanıcıdan Resilience index alınır
                                                    // Kullanıcı_ResilienceIndex =float.Parse(Console.ReadLine());
            if (Kullanıcı_ResilienceIndex < 1.0 && Kullanıcı_ResilienceIndex > 0.0)
            {
                //null
            }
            else
            {
                throw new Exception("Resilience Index must be between 0-1 and use comma to define decimal");
            }
            Console.WriteLine("USER DEFINED RESILIENCE INDEX IS: " + Kullanıcı_ResilienceIndex);
        }
        public static float Calculate_Resindex()
        {
            float T_demand_carp_h_eksi_hstar = 0;
            float T_demand_carp_Hdesign = 0;
            float[] Hdesignlar = NodeOp.Get_Hdesign();
            float[] Elevler = NodeOp.Get_NodeElev();
            float[] Demandler = NodeOp.Get_NodeDemand();
            float[] Pressurelar = NodeOp.Get_Pressure();
            float[] h_eksi_hstar = new float[NodeOp.Get_numofNode()];
            float[] demand_carp_h_eksi_hstar = new float[NodeOp.Get_numofNode()];
            float[] demand_carp_Hdesign = new float[NodeOp.Get_numofNode()];
            for (int i = 0; i < NodeOp.Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodetype(i + 1, out NodeType kontrol);
                if (kontrol.ToString() != ReservoirOp.ChkNodeType)//reservuar değilse
                {
                    if (Demandler[i] > 0)
                    {
                        h_eksi_hstar[i] = Pressurelar[i] - Sabitler.MinHead;
                        demand_carp_h_eksi_hstar[i] = Demandler[i] * h_eksi_hstar[i];
                        T_demand_carp_h_eksi_hstar = T_demand_carp_h_eksi_hstar + demand_carp_h_eksi_hstar[i];
                        demand_carp_Hdesign[i] = Demandler[i] * Hdesignlar[i];
                        T_demand_carp_Hdesign = T_demand_carp_Hdesign + demand_carp_Hdesign[i];
                    }
                }
            }
            double ResHesaplanmis = (T_demand_carp_h_eksi_hstar) / (ReservoirOp.Calculate_TotalPowerofSystem() - T_demand_carp_Hdesign);
            double rounded = Math.Round(ResHesaplanmis, 8);
            return (float)rounded;

        }




    }
}

