using System;
using System.Collections.Generic;
using System.Linq;

namespace Epanet
{
    class Iterations
    {
        public static bool Kontroller(int b) ///b. boru için kontrol yapar\\\
        {
            float[] Velocityler = PipeOp.Get_Velocity();
            float f_index = FailureIndexClass.Calculate_Failureindex();
            float r_index = ResIndexClass.Calculate_Resindex();
            PublicListeler.resres.Add(r_index);
            if (Velocityler.Max() <= 2 && r_index > ResIndexClass.Kullanıcı_ResilienceIndex && f_index >= 0)
            {
                return true;
            }
            return false;
        }
        public static Dictionary<int, float> Iteration_ILK(Dictionary<int, float> icListem)
        {
            PipeOp.Calculate_PowDis_ILK(icListem);
            var RankedList = PublicListeler.Powdis_Dict_ILK.OrderByDescending(x => x.Value).ToList();
            foreach (var itemq in RankedList.ToList())
            {
                UnsafeNativeMethods.ENgetlinkvalue(itemq.Key + 1, LinkValue.Diameter, out float valueDiam);
                PublicListeler.List_BoruvCap.Add(itemq.Key, valueDiam);
            }
            RankedList.Clear(); // RANKEDLIST ICINI TEMIZLIYORUM BURDA1
            NetworkOp.SaveAndRun();
            foreach (var FLitemler in PublicListeler.List_BoruvCap)
            {
                int a = (Array.FindIndex(Sabitler.Pipe_Diameters, x => x.Equals(FLitemler.Value)));
                float b = Sabitler.Pipe_Diameters[a - 1];
                UnsafeNativeMethods.ENsetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, b);  //Diameter Reduction


                if (Iterations.Kontroller(FLitemler.Key) == true)
                {
                    Console.WriteLine(FLitemler.Key + 1 + ". pipe will be Reduced.....");
                    UnsafeNativeMethods.ENgetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, out float valueDiaa);
                    PublicListeler.AraList_kontrolSonrasi.Add(FLitemler.Key, valueDiaa);
                }
                else
                {
                    int d = (Array.FindIndex(Sabitler.Pipe_Diameters, x => x.Equals(FLitemler.Value)));
                    float e = Sabitler.Pipe_Diameters[d];
                    UnsafeNativeMethods.ENsetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, e);
                    UnsafeNativeMethods.ENgetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, out float valueDiaa2);
                    Console.WriteLine(FLitemler.Key + 1 + ". pipe will be remained SAME");
                    PublicListeler.AraList_kontrolSonrasi.Add(FLitemler.Key, valueDiaa2);
                    NetworkOp.SaveAndRun();
                    PublicListeler.SabitListe.Add(FLitemler.Key, valueDiaa2);       //boru çapı azalmayacağı için Sabit listeye atanıyor.
                }
            }
            NetworkOp.OnlySave();
            Console.WriteLine("_____________________________");
            var yazdirilk = PublicListeler.List_BoruvCap.OrderBy(x => x.Key);
            foreach (KeyValuePair<int, float> kvp in yazdirilk)
            {
                UnsafeNativeMethods.ENsetlinkvalue(kvp.Key + 1, LinkValue.Diameter, kvp.Value);
                Console.WriteLine("iterasyon sonucunda > Boru = {0} = {1}", kvp.Key + 1, kvp.Value);
            }
            PublicListeler.List_BoruvCap.Clear();// List_BoruvCap LISTESINI TEMIZLIYORUM BURDA
            return PublicListeler.AraList_kontrolSonrasi;
        }
        public static Dictionary<int, float> Iteration(Dictionary<int, float> icListe)
        {
            PipeOp.Calculate_PowerDissipation(icListe);

            var RankedList = PublicListeler.Powdis_Dict.OrderByDescending(x => x.Value).ToList();
            foreach (var itemq in RankedList.ToList())
            {
                UnsafeNativeMethods.ENgetlinkvalue(itemq.Key + 1, LinkValue.Diameter, out float valueDiam);
                PublicListeler.List_BoruvCap.Add(itemq.Key, valueDiam);
            }
            RankedList.Clear(); // RANKEDLIST ICINI TEMIZLIYORUM BURDA
            PublicListeler.Powdis_Dict.Clear();//Powdis_Dict ICINI TEMIZLIYORUM

            foreach (var FLitemler in PublicListeler.List_BoruvCap) //kontroller ve azaltmalar
            {
                if (PublicListeler.SabitListe.ContainsKey(FLitemler.Key))//Eğer test edilen eleman Sabit listede varsa
                {
                    UnsafeNativeMethods.ENgetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, out float c2);
                    PublicListeler.AraList_kontrolSonrasi.Add(FLitemler.Key, c2);
                }
                else
                {
                    if (Array.FindIndex(Sabitler.Pipe_Diameters, x => x.Equals(FLitemler.Value)) == 0)
                    {
                        int a = (Array.FindIndex(Sabitler.Pipe_Diameters, x => x.Equals(FLitemler.Value)));
                        UnsafeNativeMethods.ENsetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, a);  //AZALTMAYI BURDA YAPIYORUM\\\
                        NetworkOp.SaveAndRun();
                    }
                    else
                    {
                        int a = (Array.FindIndex(Sabitler.Pipe_Diameters, x => x.Equals(FLitemler.Value)));
                        float b = Sabitler.Pipe_Diameters[a - 1];//bu son boruya gelince saçmalıyor düzeltmemiz lazım
                        UnsafeNativeMethods.ENsetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, b);  //AZALTMAYI BURDA YAPIYORUM\\\
                        NetworkOp.SaveAndRun();
                    }


                    if (Iterations.Kontroller(FLitemler.Key) == true)
                    {
                        Console.WriteLine(FLitemler.Key + 1 + ". pipe will be Reduced.....");
                        UnsafeNativeMethods.ENgetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, out float valueDiaa);
                        PublicListeler.AraList_kontrolSonrasi.Add(FLitemler.Key, valueDiaa);
                    }
                    else
                    {
                        int a = (Array.FindIndex(Sabitler.Pipe_Diameters, x => x.Equals(FLitemler.Value)));
                        float c = Sabitler.Pipe_Diameters[a];
                        UnsafeNativeMethods.ENsetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, c);
                        //UnsafeNativeMethods.ENgetlinkvalue(FLitemler.Key + 1, LinkValue.Diameter, out float valueDiaa2);
                        Console.WriteLine(FLitemler.Key + 1 + ". pipe will be remained SAME");
                        PublicListeler.AraList_kontrolSonrasi.Add(FLitemler.Key, c);
                        NetworkOp.SaveAndRun();
                    }
                }
            }

            NetworkOp.SaveAndRun();
            foreach (var eleman in PublicListeler.AraList_kontrolSonrasi)
            {
                if (eleman.Value == PublicListeler.List_BoruvCap[eleman.Key])   //&& 
                {
                    if (PublicListeler.SabitListe.ContainsKey(eleman.Key))
                    {
                        // Console.WriteLine("Sabit listede var Boru = {0}, Çap = {1}", eleman.Key, eleman.Value);
                    }
                    else
                    {
                        PublicListeler.SabitListe.Add(eleman.Key, eleman.Value);
                        // Console.WriteLine("SONHAL" + (eleman.Key + 1) + ". borunun Çapı: " + eleman.Value);
                    }
                }
            }
            Console.WriteLine("_____________________________");
            var yazdir = PublicListeler.List_BoruvCap.OrderBy(x => x.Key);
            foreach (KeyValuePair<int, float> kvp in yazdir)
            {
                UnsafeNativeMethods.ENsetlinkvalue(kvp.Key + 1, LinkValue.Diameter, kvp.Value);
                Console.WriteLine("iterasyon sonucunda > Boru = {0} = {1}", kvp.Key + 1, kvp.Value);
            }
            PublicListeler.List_BoruvCap.Clear();// List_BoruvCap LISTESINI TEMIZLIYORUM BURDA
            return PublicListeler.AraList_kontrolSonrasi;



        }

    }
}

