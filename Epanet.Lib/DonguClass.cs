using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epanet
{
    class DonguClass
    {
        public static void DonguGiris()
        {
            NetworkOp.StartSimulation();  // Networkü perform eder
            NetworkOp.SaveAndRun();
            //PipeOp.Set_PipeDia(Sabitler.Pipe_Diameters[Sabitler.Pipe_Diameters.Length - 1]);
            NetworkOp.SaveAndRun();
            ResIndexClass.SetResindex();
            NetworkOp.SaveAndRun();
            PipeOp.Get_numofPipe();
            Console.WriteLine("Current Resilience index: " + ResIndexClass.Calculate_Resindex());
            Console.WriteLine("Current Surplus value: " + FailureIndexClass.Calculate_Failureindex());
        }
        public static void DonguCalistir()
        {
            int sayac = 0;
            ////////////////////////////////////////LOOP//////////////////////////////////////////////////////////////////////////////////////////
            for (int i = 0; i < PipeOp.Get_numofPipe(); i++)
            {
                PublicListeler.ANALISTE.Add(i, Sabitler.Pipe_Diameters[Sabitler.Pipe_Diameters.Length - 1]);//yani 558,8 
                UnsafeNativeMethods.ENsetlinkvalue(i + 1, LinkValue.InitSetting, 130);
            }
            Console.WriteLine(1 + ". Döngü Başlangıcı");
            PublicListeler.DonguListesi = Iterations.Iteration_ILK(PublicListeler.ANALISTE).ToDictionary(entry => entry.Key, entry => entry.Value);
            Console.WriteLine("-------1. iterasyon sonucu Reilience index: " + ResIndexClass.Calculate_Resindex());
            Console.WriteLine("-------1. iterasyon sonucu head difference: " + FailureIndexClass.Calculate_Failureindex());
            Console.WriteLine("Hizlar ");
            var hizlar = PipeOp.Get_Velocity();
            for (int i = 0; i < PipeOp.Get_numofPipe(); i++)
            {
                Console.WriteLine(hizlar[i]);
            }
            PublicListeler.AraList_kontrolSonrasi.Clear();
            do
            {
                sayac = sayac + 1;
                Console.WriteLine("------------------------------------");
                Console.WriteLine(sayac + 1 + ". Döngü Başlangıcı");
                PublicListeler.DonguListesi = Iterations.Iteration(PublicListeler.DonguListesi).ToDictionary(entry => entry.Key, entry => entry.Value);
                int say = sayac + 1;
                Console.WriteLine("-------" + say + ". iterasyon sonucu Reilience index: " + ResIndexClass.Calculate_Resindex());
                Console.WriteLine("-------" + say + ". iterasyon sonucu head difference: " + FailureIndexClass.Calculate_Failureindex());
                Console.WriteLine("Hizlar ");
                var hizlar2 = PipeOp.Get_Velocity();
                for (int i = 0; i < PipeOp.Get_numofPipe(); i++)
                {
                    Console.WriteLine(hizlar2[i]);
                }
                PublicListeler.AraList_kontrolSonrasi.Clear();
            } while (PublicListeler.SabitListe.Count < PublicListeler.ANALISTE.Count);
            ////////////////////////////////////////LOOP/////////////////////////////////////////////////////////////////////////////////////////////                                                                        
            Console.WriteLine(" ");
            var sonhal = PublicListeler.SabitListe.OrderBy(x => x.Key).ToList();
            Console.WriteLine(" ");
            Console.WriteLine("-----------SON DURUMDA ÇAPLAR---------------");
            foreach (KeyValuePair<int, float> kvp in sonhal)
            {
                UnsafeNativeMethods.ENsetlinkvalue(kvp.Key + 1, LinkValue.Diameter, kvp.Value);
                Console.WriteLine("Son hal > Boru = {0}, Çap = {1}", kvp.Key + 1, kvp.Value);
            }
            Console.WriteLine(" ");
            float min = FailureIndexClass.Calculate_Failureindex();
            float rez = ResIndexClass.Calculate_Resindex();
            PipeOp.CostCalculation(PublicListeler.SabitListe);
            Console.WriteLine("Pressure-30m= " + min);
            Console.WriteLine("Resilience index: " + rez);
        }
    }
}
