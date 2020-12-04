using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using ClosedXML.Excel;

using GemBox.Spreadsheet;
using GemBox.Spreadsheet.Tables;



namespace Epanet
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            Console.Clear();
            lbl_res.Text = "";
            lbl_cost.Text = " ";
            lbl_pressuredifference.Text = "";

            ResIndexClass.Kullanıcı_ResilienceIndex = float.Parse(txt_resindex.Text);
            Sabitler.MinHead= float.Parse(txt_minPressure.Text);
            DonguClass.DonguGiris();

            
           

            
            











            //DonguClass.DonguCalistir();

            lbl_pressuredifference.Text = FailureIndexClass.Calculate_Failureindex().ToString();
            lbl_res.Text= ResIndexClass.Calculate_Resindex().ToString();
            float a= PipeOp.CostCalculation(PublicListeler.SabitListe);
            lbl_cost.Text = a.ToString();


            for (int i = 0; i < PublicListeler.SabitListe.Count; i++)
            {
                UnsafeNativeMethods.ENgetlinkvalue(i + 1, LinkValue.Diameter, out float dia);

                dataGridView1.Rows.Add("Pipe " + (i + 1) + ":", dia);
                
            }

            

            PublicListeler.resres.Clear();
            PublicListeler.ANALISTE.Clear();
            PublicListeler.IterListesi.Clear();
            PublicListeler.ANALISTE.Clear();
            PublicListeler.Powdis_Dict.Clear();
            PublicListeler.Powdis_Dict_ILK.Clear();
            PublicListeler.AraList.Clear();
            PublicListeler.GuncelBoruListesi.Clear();
            PublicListeler.List_BoruvCap.Clear();
            PublicListeler.AraList_kontrolSonrasi.Clear();
            PublicListeler.dongu.Clear();
            PublicListeler.SabitListe.Clear();
            PublicListeler.DonguListesi.Clear();
            PublicListeler.Node_Demand_list.Clear();
            PublicListeler.Node_HGL_list_CAP1.Clear();
            PublicListeler.Node_HGL_list_CAP2.Clear();

        }

       


        void Cizdir(int n1x, int n1y,int n2x, int n2y)
        {
            

         

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

           // txt_resindex.Text = "";
            PublicListeler.ANALISTE.Clear();
            PublicListeler.IterListesi.Clear();
            PublicListeler.ANALISTE.Clear();
            PublicListeler.Powdis_Dict.Clear();
            PublicListeler.Powdis_Dict_ILK.Clear();
            PublicListeler.AraList.Clear();
            PublicListeler.GuncelBoruListesi.Clear();
            PublicListeler.List_BoruvCap.Clear();
            PublicListeler.AraList_kontrolSonrasi.Clear();
            PublicListeler.dongu.Clear();
            PublicListeler.SabitListe.Clear();
            PublicListeler.DonguListesi.Clear();
            PublicListeler.Node_Demand_list.Clear();
            PublicListeler.Node_HGL_list_CAP1.Clear();
            PublicListeler.Node_HGL_list_CAP2.Clear();
        }
        private int sayi = 0;
        private void btn_Pareto_Click(object sender, EventArgs e)
        {
            //SAYAC

            ind.Text = Convert.ToString(sayi);
            timer1.Interval = 1000;
            timer1.Start();
           

            //SAYAC


            //dataGridView2.Rows.Clear();
            for (int i = 10; i < 98; i+=3)
            {
                //PublicListeler.ANALISTE.Clear();
                Console.Clear();

                double res = i / 100.0;

                ResIndexClass.Kullanıcı_ResilienceIndex = (float)res;
                Sabitler.MinHead = float.Parse(txt_minPressure.Text);
                DonguClass.DonguGiris();
                DonguClass.DonguCalistir();

                string a = FailureIndexClass.Calculate_Failureindex().ToString();
                string b = ResIndexClass.Calculate_Resindex().ToString(); 
                float c= PipeOp.CostCalculation(PublicListeler.SabitListe); 

                dataGridView2.Rows.Add( i , res , a , b,c );
                                
                    
                




                PublicListeler.resres.Clear();
                PublicListeler.ANALISTE.Clear();
                PublicListeler.IterListesi.Clear();
                PublicListeler.ANALISTE.Clear();
                PublicListeler.Powdis_Dict.Clear();
                PublicListeler.Powdis_Dict_ILK.Clear();
                PublicListeler.AraList.Clear();
                PublicListeler.GuncelBoruListesi.Clear();
                PublicListeler.List_BoruvCap.Clear();
                PublicListeler.AraList_kontrolSonrasi.Clear();
                PublicListeler.dongu.Clear();
                PublicListeler.SabitListe.Clear();
                PublicListeler.DonguListesi.Clear();
                PublicListeler.Node_Demand_list.Clear();
                PublicListeler.Node_HGL_list_CAP1.Clear();
                PublicListeler.Node_HGL_list_CAP2.Clear();

                
            }
            timer1.Stop();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int StartCol = 1;
            int StartRow = 1;
            for (int j = 0; j < dataGridView2.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                myRange.Value2 = dataGridView2.Columns[j].HeaderText;
            }
            StartRow++;
            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView2.Columns.Count; j++)
                {

                    Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                    myRange.Value2 = dataGridView2[j, i].Value == null ? "" : dataGridView2[j, i].Value;
                    myRange.Select();


                }
            }


        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayi = sayi + 1;
            ind.Text = Convert.ToString(sayi);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            Excel.Application excel = new Excel.Application();
            excel.Visible = true;
            object Missing = Type.Missing;
            Workbook workbook = excel.Workbooks.Add(Missing);
            Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
            int StartCol = 1;
            int StartRow = 1;
            for (int j = 0; j < dataGridView1.Columns.Count; j++)
            {
                Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                myRange.Value2 = dataGridView1.Columns[j].HeaderText;
            }
            StartRow++;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {

                    Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                    myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                    myRange.Select();


                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //SAYAC

            ind.Text = Convert.ToString(sayi);
            timer1.Interval = 1000;
            timer1.Start();

            
            //SAYAC
        }

        private void btn_selectINP_Click(object sender, EventArgs e)
        {


        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResIndexClass.Kullanıcı_ResilienceIndex = float.Parse(txt_resindex.Text);
            Sabitler.MinHead = float.Parse(txt_minPressure.Text);
            DonguClass.DonguGiris();

            UnsafeNativeMethods.ENsetlinkvalue(0, LinkValue.Diameter, (float)500);
            UnsafeNativeMethods.ENsetlinkvalue(1, LinkValue.Diameter, (float)500);
            UnsafeNativeMethods.ENsetlinkvalue(2, LinkValue.Diameter, (float)500);
            UnsafeNativeMethods.ENsetlinkvalue(3, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(4, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(5, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(6, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(7, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(8, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(9, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(10, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(11, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(12, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(13, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(14, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(15, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(16, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(17, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(18, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(19, LinkValue.Diameter, (float)300);
            UnsafeNativeMethods.ENsetlinkvalue(20, LinkValue.Diameter, (float)250);
            UnsafeNativeMethods.ENsetlinkvalue(21, LinkValue.Diameter, (float)250);
            UnsafeNativeMethods.ENsetlinkvalue(22, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(23, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(24, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(25, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(26, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(27, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(28, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(29, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(30, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(31, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(32, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(33, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(34, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(35, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(36, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(37, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(38, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(39, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(40, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(41, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(42, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(43, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(44, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(45, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(46, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(47, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(48, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(49, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(50, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(51, LinkValue.Diameter, (float)200);
            UnsafeNativeMethods.ENsetlinkvalue(52, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(53, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(54, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(55, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(56, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(57, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(58, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(59, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(60, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(61, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(62, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(63, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(64, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(65, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(66, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(67, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(68, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(69, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(70, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(71, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(72, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(73, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(74, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(75, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(76, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(77, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(78, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(79, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(80, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(81, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(82, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(83, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(84, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(85, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(86, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(87, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(88, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(89, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(90, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(91, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(92, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(93, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(94, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(95, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(96, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(97, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(98, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(99, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(100, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(101, LinkValue.Diameter, (float)125);
            UnsafeNativeMethods.ENsetlinkvalue(102, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(103, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(104, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(105, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(106, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(107, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(108, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(109, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(110, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(111, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(112, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(113, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(114, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(115, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(116, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(117, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(118, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(119, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(120, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(121, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(122, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(123, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(124, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(125, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(126, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(127, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(128, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(129, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(130, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(131, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(132, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(133, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(134, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(135, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(136, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(137, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(138, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(139, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(140, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(141, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(142, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(143, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(144, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(145, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(146, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(147, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(148, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(149, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(150, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(151, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(152, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(153, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(154, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(155, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(156, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(157, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(158, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(159, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(160, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(161, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(162, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(163, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(164, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(165, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(166, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(167, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(168, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(169, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(170, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(171, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(172, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(173, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(174, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(175, LinkValue.Diameter, (float)100);
            UnsafeNativeMethods.ENsetlinkvalue(176, LinkValue.Diameter, (float)100);

            NetworkOp.SaveAndRun();
            float x= ResIndexClass.Calculate_Resindex();
            ind.Text = x.ToString();
        }
    }
}
