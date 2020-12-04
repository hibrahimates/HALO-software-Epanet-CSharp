
using System;
using System.IO;
using System.Reflection;
using Epanet.Examples;
using System.Data.OleDb;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace Epanet
{
    public partial class AnaMetod
    {
        private Button button1;
        private Label label1;
        private TextBox txt_resindex;
        private TextBox txt_minPressure;
        private Label label2;
        public static int sayac = 0;
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
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
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_resindex = new System.Windows.Forms.TextBox();
            this.txt_minPressure = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();

            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(64, 108);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 65);
            this.button1.TabIndex = 0;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Defined Resilience Index :";
            // 
            // txt_resindex
            // 
            this.txt_resindex.Location = new System.Drawing.Point(204, 26);
            this.txt_resindex.Name = "txt_resindex";
            this.txt_resindex.Size = new System.Drawing.Size(63, 20);
            this.txt_resindex.TabIndex = 2;
            this.txt_resindex.Text = "0,4122522";
            this.txt_resindex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_minPressure
            // 
            this.txt_minPressure.Location = new System.Drawing.Point(204, 61);
            this.txt_minPressure.Name = "txt_minPressure";
            this.txt_minPressure.Size = new System.Drawing.Size(63, 20);
            this.txt_minPressure.TabIndex = 4;
            this.txt_minPressure.Text = "29.5";
            this.txt_minPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(101, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Minimum pressure :";
            // 
            // AnaMetod
            // 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DonguClass.DonguGiris();
            DonguClass.DonguCalistir();
        }
    }
}