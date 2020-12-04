
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
     public class Deneme1
    {
        public static int sayac = 0;
        public static void Main()
        {
            ResIndexClass ResIndex= new ResIndexClass();
            NetworkOp RunSim = new NetworkOp();
                      
            RunSim.StartSimulation();  // Networkü perform eder

            //Console.WriteLine("Resilience Index: ");//Kullanıcıdan Resilience index alınır
            //ResIndex.SetResindex(Convert.ToDouble(Console.ReadLine()));
            //double UserDefinedResindex=ResIndex.GetResindex();      //UserDefinedResindex Resilience indexi veriyor
            //PipeOp.Get_Flow(PipeOp.Get_numofPipe());

            PipeOp.Get_Flow();
            NetworkOp.SaveAndRun();

            //Console.WriteLine(PipeOp.PipeCost_Dia(1));
            //ReservoirOp.Get_ResElevation();

            //BU REZERVUAR YÜKSEKLİKLERİNİ ALABİLDİĞİM KISIM
            //float[] resyukseklikleri = ReservoirOp.Get_ResElevations();
            //foreach (float i in resyukseklikleri) 
            //{
            //Console.WriteLine( "{0}", i);
            //}
            //BU REZERVUAR YÜKSEKLİKLERİNİ ALABİLDİĞİM KISIM

            //ReservoirOp.Calculate_TotalPowerofSystem();
            //PipeOp.Set_PipeDia(550);
            NetworkOp.SaveAndRun();


            //float[] Diameter1 = new float[PipeOp.Get_numofPipe()];
            //float[] Diameter2 = new float[PipeOp.Get_numofPipe()];

            //for (int j = 0; j < PipeOp.Get_numofPipe(); j++)
            //{                
            //    Diameter1[j] = 550;
            //    Diameter2[j] = 500;

            //}

            // PipeOp.Calculate_PowerDissipation(Diameter1, Diameter2);
            PipeOp.Set_PipeDia(550);
            



            ////////////////////////////////////////DÖNGÜ/////////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < PipeOp.Get_numofPipe(); i++)
            {
                PublicListeler.ANALISTE.Add(i,Sabitler.Pipe_Diameters[Sabitler.Pipe_Diameters.Length-1]);//yani 550 
            }
                      
            Console.WriteLine(1 + ". Döngü Başlangıcı");
            PublicListeler.DonguListesi = Iterations.Iteration_ILK(PublicListeler.ANALISTE).ToDictionary(entry => entry.Key, entry => entry.Value);
            PublicListeler.AraList_kontrolSonrasi.Clear();
            PublicListeler.SabitListe.Add(-1, 0);

            do
            {
                sayac = sayac + 1;
                Console.WriteLine("------------------------------------");
                Console.WriteLine(sayac + 1 + ". Döngü Başlangıcı");
                PublicListeler.DonguListesi= Iterations.Iteration(PublicListeler.DonguListesi).ToDictionary(entry => entry.Key,entry => entry.Value);
                PublicListeler.AraList_kontrolSonrasi.Clear();
            } while (PublicListeler.SabitListe.Count < PublicListeler.ANALISTE.Count);
           // 
            ////////////////////////////////////////DÖNGÜ/////////////////////////////////////////////////////////////////////////////////////////////                                                                        






            Console.WriteLine(" ");
            Console.WriteLine("-----------SON DURUM-------------------------");
            foreach (KeyValuePair<int, float> kvp in PublicListeler.SabitListe)
            {
                Console.WriteLine("Sabit Liste > Boru = {0}, Çap = {1}", kvp.Key, kvp.Value);
            }



            Console.ReadLine();
        }






        public static float ToSingle(double value)
        {
            return (float)value;
        }
        private static void Error(string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(format, args);
            Console.Error.WriteLine("Aborting.");
            Console.ResetColor();
            Console.ReadLine();
        }
        
    }


}