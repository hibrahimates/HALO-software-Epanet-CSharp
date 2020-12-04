namespace Epanet
{
    partial class Form1
    {
        // burası formun tasarımını tutan dosya. Bunu bulamadığı için boş bir form oluşturup bişey ekleyemiyor :)
        //peki bi form daha yapmak istesem ve artık başlangıçta onla açılsın istesem?


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txt_minPressure = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_resindex = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.MultiLine_PipeNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MultiLine_PipeDiameter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_resin = new System.Windows.Forms.Label();
            this.cost = new System.Windows.Forms.Label();
            this.lbl_cost = new System.Windows.Forms.Label();
            this.lbl_res = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_pressuredifference = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.sayac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Girilen_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Difference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Hesaplanan_Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Toplam_Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_Pareto = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ind = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.btn_selectINP = new System.Windows.Forms.Button();
            this.txt_select = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button5 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_minPressure
            // 
            this.txt_minPressure.Location = new System.Drawing.Point(233, 97);
            this.txt_minPressure.Name = "txt_minPressure";
            this.txt_minPressure.Size = new System.Drawing.Size(63, 20);
            this.txt_minPressure.TabIndex = 9;
            this.txt_minPressure.Text = "30";
            this.txt_minPressure.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Minimum pressure :";
            // 
            // txt_resindex
            // 
            this.txt_resindex.Location = new System.Drawing.Point(233, 65);
            this.txt_resindex.Name = "txt_resindex";
            this.txt_resindex.Size = new System.Drawing.Size(63, 20);
            this.txt_resindex.TabIndex = 7;
            this.txt_resindex.Text = "0.5";
            this.txt_resindex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "User Defined Resilience Index :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 136);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(281, 45);
            this.button1.TabIndex = 5;
            this.button1.Text = "Calculate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.button1_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MultiLine_PipeNumber,
            this.MultiLine_PipeDiameter,
            this.Column1});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnF2;
            this.dataGridView1.Location = new System.Drawing.Point(12, 244);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(280, 244);
            this.dataGridView1.TabIndex = 11;
            // 
            // MultiLine_PipeNumber
            // 
            this.MultiLine_PipeNumber.HeaderText = "Pipe Number";
            this.MultiLine_PipeNumber.Name = "MultiLine_PipeNumber";
            this.MultiLine_PipeNumber.Width = 93;
            // 
            // MultiLine_PipeDiameter
            // 
            this.MultiLine_PipeDiameter.HeaderText = "Diameter (mm)";
            this.MultiLine_PipeDiameter.Name = "MultiLine_PipeDiameter";
            this.MultiLine_PipeDiameter.Width = 99;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.Width = 73;
            // 
            // txt_resin
            // 
            this.txt_resin.AutoSize = true;
            this.txt_resin.Location = new System.Drawing.Point(12, 534);
            this.txt_resin.Name = "txt_resin";
            this.txt_resin.Size = new System.Drawing.Size(88, 13);
            this.txt_resin.TabIndex = 12;
            this.txt_resin.Text = "Resilience Index:";
            // 
            // cost
            // 
            this.cost.AutoSize = true;
            this.cost.Location = new System.Drawing.Point(69, 560);
            this.cost.Name = "cost";
            this.cost.Size = new System.Drawing.Size(31, 13);
            this.cost.TabIndex = 13;
            this.cost.Text = "Cost:";
            // 
            // lbl_cost
            // 
            this.lbl_cost.AutoSize = true;
            this.lbl_cost.Location = new System.Drawing.Point(118, 560);
            this.lbl_cost.Name = "lbl_cost";
            this.lbl_cost.Size = new System.Drawing.Size(16, 13);
            this.lbl_cost.TabIndex = 15;
            this.lbl_cost.Text = "   ";
            // 
            // lbl_res
            // 
            this.lbl_res.AutoSize = true;
            this.lbl_res.Location = new System.Drawing.Point(118, 534);
            this.lbl_res.Name = "lbl_res";
            this.lbl_res.Size = new System.Drawing.Size(22, 13);
            this.lbl_res.TabIndex = 14;
            this.lbl_res.Text = "     ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 508);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Min pressure diff:";
            // 
            // lbl_pressuredifference
            // 
            this.lbl_pressuredifference.AutoSize = true;
            this.lbl_pressuredifference.Location = new System.Drawing.Point(118, 508);
            this.lbl_pressuredifference.Name = "lbl_pressuredifference";
            this.lbl_pressuredifference.Size = new System.Drawing.Size(22, 13);
            this.lbl_pressuredifference.TabIndex = 14;
            this.lbl_pressuredifference.Text = "     ";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sayac,
            this.Girilen_Index,
            this.Difference,
            this.Hesaplanan_Index,
            this.Toplam_Cost});
            this.dataGridView2.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView2.Location = new System.Drawing.Point(414, 12);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(593, 568);
            this.dataGridView2.TabIndex = 16;
            // 
            // sayac
            // 
            this.sayac.HeaderText = "No";
            this.sayac.Name = "sayac";
            // 
            // Girilen_Index
            // 
            this.Girilen_Index.HeaderText = "Girilen Index";
            this.Girilen_Index.Name = "Girilen_Index";
            // 
            // Difference
            // 
            this.Difference.HeaderText = "Pressure Difference";
            this.Difference.Name = "Difference";
            // 
            // Hesaplanan_Index
            // 
            this.Hesaplanan_Index.HeaderText = "Hesaplanan Index";
            this.Hesaplanan_Index.Name = "Hesaplanan_Index";
            // 
            // Toplam_Cost
            // 
            this.Toplam_Cost.HeaderText = "Cost";
            this.Toplam_Cost.Name = "Toplam_Cost";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(103, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            // 
            // btn_Pareto
            // 
            this.btn_Pareto.Location = new System.Drawing.Point(328, 13);
            this.btn_Pareto.Name = "btn_Pareto";
            this.btn_Pareto.Size = new System.Drawing.Size(80, 87);
            this.btn_Pareto.TabIndex = 17;
            this.btn_Pareto.Text = "Pareto";
            this.btn_Pareto.UseVisualStyleBackColor = true;
            this.btn_Pareto.Click += new System.EventHandler(this.btn_Pareto_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(328, 116);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 23);
            this.button2.TabIndex = 18;
            this.button2.Text = "Copy Table";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // ind
            // 
            this.ind.AutoSize = true;
            this.ind.Location = new System.Drawing.Point(336, 404);
            this.ind.Name = "ind";
            this.ind.Size = new System.Drawing.Size(70, 13);
            this.ind.TabIndex = 19;
            this.ind.Text = "index sonucu";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 215);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(238, 23);
            this.button3.TabIndex = 20;
            this.button3.Text = "Copy Table";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(373, 555);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(30, 23);
            this.button4.TabIndex = 21;
            this.button4.Text = "but";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // btn_selectINP
            // 
            this.btn_selectINP.Location = new System.Drawing.Point(12, 12);
            this.btn_selectINP.Name = "btn_selectINP";
            this.btn_selectINP.Size = new System.Drawing.Size(136, 23);
            this.btn_selectINP.TabIndex = 22;
            this.btn_selectINP.Text = "Select INP";
            this.btn_selectINP.UseVisualStyleBackColor = true;
            this.btn_selectINP.Click += new System.EventHandler(this.btn_selectINP_Click);
            // 
            // txt_select
            // 
            this.txt_select.Location = new System.Drawing.Point(154, 15);
            this.txt_select.Name = "txt_select";
            this.txt_select.Size = new System.Drawing.Size(142, 20);
            this.txt_select.TabIndex = 23;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(328, 215);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 167);
            this.button5.TabIndex = 24;
            this.button5.Text = "yayla orjinal indexi kaç?";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 190);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.txt_select);
            this.Controls.Add(this.btn_selectINP);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ind);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_Pareto);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.lbl_cost);
            this.Controls.Add(this.lbl_pressuredifference);
            this.Controls.Add(this.lbl_res);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cost);
            this.Controls.Add(this.txt_resin);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txt_minPressure);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_resindex);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "HALO Software";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_minPressure;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_resindex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn MultiLine_PipeNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn MultiLine_PipeDiameter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.Label txt_resin;
        private System.Windows.Forms.Label cost;
        private System.Windows.Forms.Label lbl_cost;
        private System.Windows.Forms.Label lbl_res;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_pressuredifference;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn sayac;
        private System.Windows.Forms.DataGridViewTextBoxColumn Girilen_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Difference;
        private System.Windows.Forms.DataGridViewTextBoxColumn Hesaplanan_Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn Toplam_Cost;
        private System.Windows.Forms.Button btn_Pareto;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.Label ind;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btn_selectINP;
        private System.Windows.Forms.TextBox txt_select;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button5;
    }
}