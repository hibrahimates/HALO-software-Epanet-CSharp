using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epanet
{
    class PipeOp
    {
        public static int Get_numofPipe()
        {
            UnsafeNativeMethods.ENgetcount(CountType.Link, out int numofPipe);
            return numofPipe;
        }
        public static void Set_PipeDia(float Dia)
        {
            for (int i = 0; i < Get_numofPipe(); i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Dia);
            }
        }
        public static float[] Get_Flow()
        {
            //float valueFlow;
            float[] Flowlar = new float[Get_numofPipe()];
            for (int i = 0; i < Get_numofPipe(); i++)
            {
                UnsafeNativeMethods.ENgetlinkvalue(i + 1, LinkValue.Flow, out float valueFlow);
                Flowlar[i] = Math.Abs(valueFlow);//Flow negatif olamıyor ve birimi m3/h
            }
            return Flowlar;
        }
        public static float[] Get_HeadLoss()
        {
            // float valueHloss;
            float[] HeadLosslar = new float[Get_numofPipe()];

            for (int i = 0; i < Get_numofPipe(); i++)
            {
                UnsafeNativeMethods.ENgetlinkvalue(i + 1, LinkValue.HeadLoss, out float valueHloss);
                HeadLosslar[i] = valueHloss;
            }
            return HeadLosslar;
        }
        public static float[] Get_Velocity()
        {
            //float valueVelocity;
            float[] Velocityler = new float[Get_numofPipe()];
            for (int i = 0; i < Get_numofPipe(); i++)
            {
                UnsafeNativeMethods.ENgetlinkvalue(i + 1, LinkValue.Velocity, out float valueVelocity);
                Velocityler[i] = valueVelocity;
            }
            return Velocityler;
        }
        public static void Calculate_PowDis_ILK(Dictionary<int, float> icListem)
        {
            int[] boruIndex1 = new int[icListem.Count];
            int[] boruIndex2 = new int[icListem.Count];
            float[] boruFiyat1 = new float[icListem.Count];
            float[] boruFiyat2 = new float[icListem.Count];
            float[] Cap1 = new float[icListem.Count]; //8 elemanlı dizi oluşturdu
            float[] Cap2 = new float[icListem.Count];
            float[] boruLenght1 = new float[icListem.Count];
            float[] boruLenght2 = new float[icListem.Count];
            float[] Pipe_Start_Point = new float[icListem.Count];
            float[] Pipe_End_Point = new float[icListem.Count];
            //listeyi sıralıyor..
            var iclistSirali = icListem.OrderBy(x => x.Key).ToList();
            Cap1 = iclistSirali.Select(z => z.Value).ToArray();
            //Boruların başlangıç ve bitiş noktalarını bulup dizi haline getiriyor..
            for (int i = 0; i < icListem.Count; i++)
            {
                UnsafeNativeMethods.ENgetlinknodes(i + 1, out int NodeS, out int NodeF);
                Pipe_End_Point[i] = NodeS;
                Pipe_Start_Point[i] = NodeF;
            }
            //boruların CAP1 ve CAP2 olarak uzunluk ve fiyatlarını buluyor..
            for (int k = 0; k < icListem.Count; k++)
            {
                UnsafeNativeMethods.ENgetlinkvalue(k + 1, LinkValue.Length, out float lenght1);
                boruLenght1[k] = lenght1;
            }
            for (int k = 0; k < icListem.Count; k++)
            {
                boruIndex1[k] = Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]);
                boruFiyat1[k] = Sabitler.Pipe_Cost[boruIndex1[k]] * boruLenght1[k];
                Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]) - 1];
                boruIndex2[k] = Array.IndexOf(Sabitler.Pipe_Diameters, Cap2[k]);
                boruFiyat2[k] = Sabitler.Pipe_Cost[boruIndex2[k]] * boruLenght1[k];
            }
            // Junctionların demandlerini alıyoruz.//BUNU SADECE 1 kere yapıyoruz...
            for (int i = 0; i < NodeOp.Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Demand, out float DemandValue);
                if (DemandValue < 0)
                {
                    PublicListeler.Node_Demand_list.Add(i + 1, 0);
                }
                else
                {
                    float deger = DemandValue * (float)3.6;
                    PublicListeler.Node_Demand_list.Add(i + 1, deger);
                }
            }
            //////////////////////////GAMA DURUMU İÇİN (CAP2)//////////////////////
            // Start_Point noktası için HGL-Demand
            float[] Start_Point_HGL_CAP2 = new float[icListem.Count];
            float[] End_Point_HGL_CAP2 = new float[icListem.Count];
            //Networke CAP2'yi atayıp sistemi analiz ediyoruz... 
            for (int i = 0; i < icListem.Count; i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Cap2[i]);
            }
            NetworkOp.SaveAndRun();
            // HGL değerlerini çekiyoruz... node numarasına denk gelen HGL leri listeliyoruz.
            for (int i = 0; i < NodeOp.Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Head, out float HGL);
                PublicListeler.Node_HGL_list_CAP2.Add(i + 1, HGL);
            }
            //////////////////////////GAMA DURUMU İÇİN (CAP1)//////////////////////
            // Start_Point noktası için HGL-Demand
            float[] Start_Point_HGL_CAP1 = new float[icListem.Count];
            float[] End_Point_HGL_CAP1 = new float[icListem.Count];
            //Networke CAP1 i atayıp sistemi analiz ediyoruz... 
            for (int i = 0; i < icListem.Count; i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Cap1[i]);
            }
            NetworkOp.SaveAndRun();
            // HGL değerlerini çekiyoruz...
            for (int i = 0; i < NodeOp.Get_numofNode(); i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Head, out float HGL);
                PublicListeler.Node_HGL_list_CAP1.Add(i + 1, HGL);
            }
            //power dissipationlar hesaplanacak ve Ratio bulunacak
            float[] PowDisp_1 = new float[icListem.Count];
            float[] PowDisp_2 = new float[icListem.Count];
            float[] Ratio = new float[icListem.Count];

            for (int t = 0; t < icListem.Count; t++)
            {
                UnsafeNativeMethods.ENgetlinknodes(t + 1, out int NodeS, out int NodeF);
                if (PublicListeler.Node_HGL_list_CAP1[NodeS] > PublicListeler.Node_HGL_list_CAP1[NodeF])
                {
                    PowDisp_1[t] = (PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP1[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP1[NodeF]) / 3600;
                }
                else
                {
                    PowDisp_1[t] = -(PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP1[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP1[NodeF]) / 3600;
                }

                if (PublicListeler.Node_HGL_list_CAP2[NodeS] > PublicListeler.Node_HGL_list_CAP2[NodeF])
                {
                    PowDisp_2[t] = (PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP2[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP2[NodeF]) / 3600;

                }
                else
                {
                    PowDisp_2[t] = -(PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP2[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP2[NodeF]) / 3600;
                }

                Ratio[t] = -(boruFiyat2[t] - boruFiyat1[t]) / ((PowDisp_2[t] - PowDisp_1[t]) * Sabitler.g * 1000);
                PublicListeler.Powdis_Dict_ILK.Add(t, Ratio[t]);
            }
            for (int i = 0; i < icListem.Count(); i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Cap1[i]);
            }
            NetworkOp.SaveAndRun();
            PublicListeler.Node_HGL_list_CAP1.Clear();
            PublicListeler.Node_HGL_list_CAP2.Clear();
        }
        public static void Calculate_PowerDissipation(Dictionary<int, float> icListe) //Burdaki Dictionarylerde sorun yokz
        {
            PublicListeler.Powdis_Dict.Clear();//Her sefer yeniden eleman ekleneceği için listeleri temizliyorum.
            int[] boruIndex1 = new int[icListe.Count];
            int[] boruIndex2 = new int[icListe.Count];
            float[] boruFiyat1 = new float[icListe.Count];
            float[] boruFiyat2 = new float[icListe.Count];
            float[] Cap1 = new float[icListe.Count]; //8 elemanlı dizi oluşturdu
            float[] Cap2 = new float[icListe.Count];
            float[] boruLenght1 = new float[icListe.Count];
            float[] boruLenght2 = new float[icListe.Count];
            float[] Pipe_Start_Point = new float[icListe.Count];
            float[] Pipe_End_Point = new float[icListe.Count];
            var iclistSirali = icListe.OrderBy(x => x.Key).ToList();
            Cap1 = iclistSirali.Select(z => z.Value).ToArray();
            //Boruların başlangıç ve bitiş noktalarını bulup dizi haline getiriyor..
            for (int i = 0; i < icListe.Count; i++)
            {
                UnsafeNativeMethods.ENgetlinknodes(i + 1, out int NodeS, out int NodeF);
                Pipe_End_Point[i] = NodeS;
                Pipe_Start_Point[i] = NodeF;
            }
            for (int k = 0; k < icListe.Count; k++)
            {
                UnsafeNativeMethods.ENgetlinkvalue(k + 1, LinkValue.Length, out float lenght1);
                boruLenght1[k] = lenght1;
            }
            for (int k = 0; k < icListe.Count; k++)
            {
                boruIndex1[k] = Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]);
                boruFiyat1[k] = Sabitler.Pipe_Cost[boruIndex1[k]] * boruLenght1[k];

                if (PublicListeler.SabitListe.Count == 0)
                {
                    Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]) - 1];
                }
                else
                {
                    if (PublicListeler.SabitListe.ContainsKey(k))
                    {
                        Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k])];
                    }
                    else
                    {
                        if (Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]) == 0)
                        {
                            Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k])];
                        }
                        else
                        {
                            Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]) - 1];
                        }
                    }
                }
                boruIndex2[k] = Array.IndexOf(Sabitler.Pipe_Diameters, Cap2[k]);
                boruFiyat2[k] = Sabitler.Pipe_Cost[boruIndex2[k]] * boruLenght1[k];
            }
            for (int i = 0; i < icListe.Count(); i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Cap1[i]);//Eski haline getirdi sistemi
            }
            NetworkOp.SaveAndRun();
            //////////////////////////GAMA DURUMU İÇİN (CAP2)//////////////////////
            float[] Start_Point_HGL_CAP2 = new float[icListe.Count];
            float[] End_Point_HGL_CAP2 = new float[icListe.Count];
            //Networke CAP2'yi atayıp sistemi analiz ediyoruz... 
            for (int i = 0; i < icListe.Count; i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Cap2[i]);
            }
            NetworkOp.SaveAndRun();
            // HGL değerlerini çekiyoruz...
            for (int i = 0; i < icListe.Count; i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Head, out float HGL);
                PublicListeler.Node_HGL_list_CAP2.Add(i + 1, HGL);
            }
            //////////////////////////GAMA DURUMU İÇİN (CAP1)//////////////////////
            // Start_Point noktası için HGL-Demand
            float[] Start_Point_HGL_CAP1 = new float[icListe.Count];
            float[] End_Point_HGL_CAP1 = new float[icListe.Count];

            //Networke CAP1 i atayıp sistemi analiz ediyoruz... 
            for (int i = 0; i < icListe.Count; i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Cap1[i]);
            }
            NetworkOp.SaveAndRun();

            // HGL değerlerini çekiyoruz...
            for (int i = 0; i < icListe.Count; i++)
            {
                UnsafeNativeMethods.ENgetnodevalue(i + 1, NodeValue.Head, out float HGL);
                PublicListeler.Node_HGL_list_CAP1.Add(i + 1, HGL);
            }





            ///Power Dissipation Calculation\\\
            float[] PowDisp_1 = new float[icListe.Count];
            float[] PowDisp_2 = new float[icListe.Count];
            float[] Ratio = new float[icListe.Count];


            for (int t = 0; t < icListe.Count; t++)
            {
                UnsafeNativeMethods.ENgetlinknodes(t + 1, out int NodeS, out int NodeF);
                if (PublicListeler.Node_HGL_list_CAP1[NodeS] > PublicListeler.Node_HGL_list_CAP1[NodeF])
                {
                    PowDisp_1[t] = (PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP1[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP1[NodeF]) / 3600;
                }
                else
                {
                    PowDisp_1[t] = -(PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP1[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP1[NodeF]) / 3600;
                }

                if (PublicListeler.Node_HGL_list_CAP2[NodeS] > PublicListeler.Node_HGL_list_CAP2[NodeF])
                {
                    PowDisp_2[t] = (PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP2[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP2[NodeF]) / 3600;

                }
                else
                {
                    PowDisp_2[t] = -(PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP2[NodeS] - PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP2[NodeF]) / 3600;
                }

                //Ratio[t] = -(boruFiyat2[t] - boruFiyat1[t]) / ((PowDisp_2[t] - PowDisp_1[t]) * Sabitler.g * 1000);
                //Console.WriteLine("PublicListeler.Node_Demand_list[NodeS]: " + PublicListeler.Node_Demand_list[NodeS]);
                //Console.WriteLine("PublicListeler.Node_Demand_list[NodeF]: " + PublicListeler.Node_Demand_list[NodeF]);
                //Console.WriteLine("Ratio= -(" + boruFiyat2[t] + " - " + boruFiyat1[t] + " )/( " + PowDisp_2[t] + " - " + PowDisp_1[t] + " * " + Sabitler.g + "*1000= " + Ratio[t]);

                //Console.WriteLine("PowDisp_1[" + t + "]=" + (((PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP1[NodeS]) - (PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP1[NodeF])) / 3600) + " = " + PublicListeler.Node_Demand_list[NodeS] + " * " + PublicListeler.Node_HGL_list_CAP1[NodeS] + " - " + PublicListeler.Node_Demand_list[NodeF] + " * " + PublicListeler.Node_HGL_list_CAP1[NodeF]);
                //Console.WriteLine("PowDisp_2[" + t + "]=" + (((PublicListeler.Node_Demand_list[NodeS] * PublicListeler.Node_HGL_list_CAP2[NodeS]) - (PublicListeler.Node_Demand_list[NodeF] * PublicListeler.Node_HGL_list_CAP2[NodeF])) / 3600) + " = " + PublicListeler.Node_Demand_list[NodeS] + " * " + PublicListeler.Node_HGL_list_CAP2[NodeS] + " - " + PublicListeler.Node_Demand_list[NodeF] + " * " + PublicListeler.Node_HGL_list_CAP2[NodeF]);


                if (PowDisp_1[t] == PowDisp_2[t] || boruFiyat1[t] == boruFiyat2[t])//veya borufiyatlar eşitse
                {
                    PublicListeler.Powdis_Dict.Add(t, -9999999);
                }
                else
                {
                    Ratio[t] = -(boruFiyat2[t] - boruFiyat1[t]) / ((PowDisp_2[t] - PowDisp_1[t]) * Sabitler.g * 1000);
                    PublicListeler.Powdis_Dict.Add(t, Ratio[t]);
                }



            }

            for (int i = 0; i < icListe.Count(); i++)
            {
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.Diameter, Cap1[i]);
            }
            NetworkOp.SaveAndRun();
            PublicListeler.Node_HGL_list_CAP1.Clear();
            PublicListeler.Node_HGL_list_CAP2.Clear();
        }

        public static float CostCalculation(Dictionary<int, float> icListem)
        {
            float toplamfiyat = 0;
            int[] boruIndex = new int[Get_numofPipe()];
            float[] boruFiyat = new float[Get_numofPipe()];
            float[] Cap = new float[icListem.Count];
            float[] boruLenght = new float[icListem.Count];


            var iclistSirali = icListem.OrderBy(x => x.Key).ToList();
            Cap = iclistSirali.Select(z => z.Value).ToArray();

            for (int k = 0; k < icListem.Count; k++)
            {
                UnsafeNativeMethods.ENgetlinkvalue(k + 1, LinkValue.Length, out float lenght);
                boruLenght[k] = lenght;
            }
            for (int k = 0; k < Get_numofPipe(); k++)
            {
                boruIndex[k] = Array.IndexOf(Sabitler.Pipe_Diameters, Cap[k]);
                boruFiyat[k] = Sabitler.Pipe_Cost[boruIndex[k]] * boruLenght[k];
                toplamfiyat = toplamfiyat + boruFiyat[k];

                //  return toplamfiyat;
            }
            Console.WriteLine("Total Price :" + toplamfiyat);
            return toplamfiyat;
        }
    }
}



//for (int k = 0; k<icListe.Count; k++)
//          {
//              boruIndex1[k] = Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]);
//              boruFiyat1[k] = Sabitler.Pipe_Cost[boruIndex1[k]];

//              if (PublicListeler.SabitListe.Count == 0)
//              {
//                  Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]) - 1];
//              }
//              else
//              {
//                  foreach (var item in PublicListeler.SabitListe)
//                  {
//                      if (item.Key == k)//sabitlistin içini kontrol etme şeklini değiştir.///BURDA 2 KERE CAP2 DEĞİŞİYOR
//                      {
//                          Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k])];
//                      }
//                      else
//                      {
//                          if (Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]) == 0)
//                          {
//                              Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k])];
//                          }
//                          else
//                          {
//                              Cap2[k] = Sabitler.Pipe_Diameters[Array.IndexOf(Sabitler.Pipe_Diameters, Cap1[k]) - 1];
//                          }
//                      }
//                  }
//              }


//              boruIndex2[k] = Array.IndexOf(Sabitler.Pipe_Diameters, Cap2[k]);

//              boruFiyat2[k] = Sabitler.Pipe_Cost[boruIndex2[k]];
//          }