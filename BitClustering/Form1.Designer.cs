namespace BitClustering
{
    partial class Form1
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_HotSax = new System.Windows.Forms.Button();
            this.label_N_LENGTH = new System.Windows.Forms.Label();
            this.label_W_LENGTH = new System.Windows.Forms.Label();
            this.label_FileName = new System.Windows.Forms.Label();
            this.txt_N_LENGTH = new System.Windows.Forms.TextBox();
            this.txt_W_LENGTH = new System.Windows.Forms.TextBox();
            this.txt_FileName = new System.Windows.Forms.TextBox();
            this.btn_BitCluster = new System.Windows.Forms.Button();
            this.label_K_CENTERS = new System.Windows.Forms.Label();
            this.txt_K_CENTERS = new System.Windows.Forms.TextBox();
            this.txt_best_so_far_dist = new System.Windows.Forms.TextBox();
            this.txt_best_so_far_loc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_time = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Squeezer = new System.Windows.Forms.Button();
            this.txt_Threshold = new System.Windows.Forms.TextBox();
            this.label_threshold = new System.Windows.Forms.Label();
            this.btn_Squeezer_PLA = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Hotsax_Squeezer = new System.Windows.Forms.Button();
            this.btn_RunTest = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox_Method = new System.Windows.Forms.ComboBox();
            this.label_Method = new System.Windows.Forms.Label();
            this.label_Data = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_HotSax
            // 
            this.btn_HotSax.Location = new System.Drawing.Point(295, 148);
            this.btn_HotSax.Name = "btn_HotSax";
            this.btn_HotSax.Size = new System.Drawing.Size(152, 25);
            this.btn_HotSax.TabIndex = 0;
            this.btn_HotSax.Text = "HOT_SAX";
            this.btn_HotSax.UseVisualStyleBackColor = true;
            this.btn_HotSax.Click += new System.EventHandler(this.btn_HotSax_Click);
            // 
            // label_N_LENGTH
            // 
            this.label_N_LENGTH.AutoSize = true;
            this.label_N_LENGTH.Location = new System.Drawing.Point(29, 62);
            this.label_N_LENGTH.Name = "label_N_LENGTH";
            this.label_N_LENGTH.Size = new System.Drawing.Size(65, 13);
            this.label_N_LENGTH.TabIndex = 1;
            this.label_N_LENGTH.Text = "N_LENGTH";
            // 
            // label_W_LENGTH
            // 
            this.label_W_LENGTH.AutoSize = true;
            this.label_W_LENGTH.Location = new System.Drawing.Point(29, 98);
            this.label_W_LENGTH.Name = "label_W_LENGTH";
            this.label_W_LENGTH.Size = new System.Drawing.Size(68, 13);
            this.label_W_LENGTH.TabIndex = 2;
            this.label_W_LENGTH.Text = "W_LENGTH";
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(29, 169);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(54, 13);
            this.label_FileName.TabIndex = 3;
            this.label_FileName.Text = "File Name";
            // 
            // txt_N_LENGTH
            // 
            this.txt_N_LENGTH.Location = new System.Drawing.Point(118, 59);
            this.txt_N_LENGTH.Name = "txt_N_LENGTH";
            this.txt_N_LENGTH.Size = new System.Drawing.Size(100, 20);
            this.txt_N_LENGTH.TabIndex = 4;
            // 
            // txt_W_LENGTH
            // 
            this.txt_W_LENGTH.Location = new System.Drawing.Point(118, 94);
            this.txt_W_LENGTH.Name = "txt_W_LENGTH";
            this.txt_W_LENGTH.Size = new System.Drawing.Size(100, 20);
            this.txt_W_LENGTH.TabIndex = 4;
            // 
            // txt_FileName
            // 
            this.txt_FileName.Enabled = false;
            this.txt_FileName.Location = new System.Drawing.Point(118, 163);
            this.txt_FileName.Name = "txt_FileName";
            this.txt_FileName.Size = new System.Drawing.Size(100, 20);
            this.txt_FileName.TabIndex = 4;
            // 
            // btn_BitCluster
            // 
            this.btn_BitCluster.Location = new System.Drawing.Point(295, 26);
            this.btn_BitCluster.Name = "btn_BitCluster";
            this.btn_BitCluster.Size = new System.Drawing.Size(152, 25);
            this.btn_BitCluster.TabIndex = 5;
            this.btn_BitCluster.Text = "BitClustering Li_Braysy";
            this.btn_BitCluster.UseVisualStyleBackColor = true;
            this.btn_BitCluster.Click += new System.EventHandler(this.btn_algorithm2_Click);
            // 
            // label_K_CENTERS
            // 
            this.label_K_CENTERS.AutoSize = true;
            this.label_K_CENTERS.Location = new System.Drawing.Point(29, 134);
            this.label_K_CENTERS.Name = "label_K_CENTERS";
            this.label_K_CENTERS.Size = new System.Drawing.Size(71, 13);
            this.label_K_CENTERS.TabIndex = 1;
            this.label_K_CENTERS.Text = "K_CENTERS";
            // 
            // txt_K_CENTERS
            // 
            this.txt_K_CENTERS.Location = new System.Drawing.Point(118, 129);
            this.txt_K_CENTERS.Name = "txt_K_CENTERS";
            this.txt_K_CENTERS.Size = new System.Drawing.Size(100, 20);
            this.txt_K_CENTERS.TabIndex = 4;
            // 
            // txt_best_so_far_dist
            // 
            this.txt_best_so_far_dist.Location = new System.Drawing.Point(118, 200);
            this.txt_best_so_far_dist.Name = "txt_best_so_far_dist";
            this.txt_best_so_far_dist.Size = new System.Drawing.Size(100, 20);
            this.txt_best_so_far_dist.TabIndex = 6;
            // 
            // txt_best_so_far_loc
            // 
            this.txt_best_so_far_loc.Location = new System.Drawing.Point(118, 236);
            this.txt_best_so_far_loc.Name = "txt_best_so_far_loc";
            this.txt_best_so_far_loc.Size = new System.Drawing.Size(100, 20);
            this.txt_best_so_far_loc.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "best_so_far_dist";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "best_so_far_loc";
            // 
            // txt_time
            // 
            this.txt_time.Location = new System.Drawing.Point(295, 325);
            this.txt_time.Name = "txt_time";
            this.txt_time.Size = new System.Drawing.Size(152, 20);
            this.txt_time.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(292, 306);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Execution time";
            // 
            // btn_Squeezer
            // 
            this.btn_Squeezer.Location = new System.Drawing.Point(295, 65);
            this.btn_Squeezer.Name = "btn_Squeezer";
            this.btn_Squeezer.Size = new System.Drawing.Size(152, 27);
            this.btn_Squeezer.TabIndex = 5;
            this.btn_Squeezer.Text = "BitCluster + Squeezer";
            this.btn_Squeezer.UseVisualStyleBackColor = true;
            this.btn_Squeezer.Click += new System.EventHandler(this.btn_Squeezer_Click);
            // 
            // txt_Threshold
            // 
            this.txt_Threshold.Location = new System.Drawing.Point(118, 26);
            this.txt_Threshold.Name = "txt_Threshold";
            this.txt_Threshold.Size = new System.Drawing.Size(100, 20);
            this.txt_Threshold.TabIndex = 13;
            // 
            // label_threshold
            // 
            this.label_threshold.AutoSize = true;
            this.label_threshold.Location = new System.Drawing.Point(29, 33);
            this.label_threshold.Name = "label_threshold";
            this.label_threshold.Size = new System.Drawing.Size(74, 13);
            this.label_threshold.TabIndex = 14;
            this.label_threshold.Text = "THRESHOLD";
            // 
            // btn_Squeezer_PLA
            // 
            this.btn_Squeezer_PLA.Location = new System.Drawing.Point(295, 106);
            this.btn_Squeezer_PLA.Name = "btn_Squeezer_PLA";
            this.btn_Squeezer_PLA.Size = new System.Drawing.Size(152, 26);
            this.btn_Squeezer_PLA.TabIndex = 15;
            this.btn_Squeezer_PLA.Text = "BitCluster + Squeezer + PLA";
            this.btn_Squeezer_PLA.UseVisualStyleBackColor = true;
            this.btn_Squeezer_PLA.Click += new System.EventHandler(this.btn_Squeezer_PLA_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Clear.Location = new System.Drawing.Point(392, 230);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(55, 45);
            this.btn_Clear.TabIndex = 16;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Hotsax_Squeezer
            // 
            this.btn_Hotsax_Squeezer.Location = new System.Drawing.Point(295, 189);
            this.btn_Hotsax_Squeezer.Name = "btn_Hotsax_Squeezer";
            this.btn_Hotsax_Squeezer.Size = new System.Drawing.Size(152, 27);
            this.btn_Hotsax_Squeezer.TabIndex = 17;
            this.btn_Hotsax_Squeezer.Text = "HOTSAX + Squeezer";
            this.btn_Hotsax_Squeezer.UseVisualStyleBackColor = true;
            this.btn_Hotsax_Squeezer.Click += new System.EventHandler(this.btn_Hotsax_Squeezer_Click);
            // 
            // btn_RunTest
            // 
            this.btn_RunTest.Location = new System.Drawing.Point(295, 227);
            this.btn_RunTest.Name = "btn_RunTest";
            this.btn_RunTest.Size = new System.Drawing.Size(62, 48);
            this.btn_RunTest.TabIndex = 18;
            this.btn_RunTest.Text = "Run Test";
            this.btn_RunTest.UseVisualStyleBackColor = true;
            this.btn_RunTest.Click += new System.EventHandler(this.btn_RunTest_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(75, 294);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(142, 21);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox_Method
            // 
            this.comboBox_Method.FormattingEnabled = true;
            this.comboBox_Method.Items.AddRange(new object[] {
            "BitClustering Li_Braysy",
            "BitCluster + Squeezer",
            "BitCluster + Squeezer + PLA",
            "HOT_SAX",
            "HOT_SAX + Squeezer"});
            this.comboBox_Method.Location = new System.Drawing.Point(75, 321);
            this.comboBox_Method.Name = "comboBox_Method";
            this.comboBox_Method.Size = new System.Drawing.Size(143, 21);
            this.comboBox_Method.TabIndex = 20;
            // 
            // label_Method
            // 
            this.label_Method.AutoSize = true;
            this.label_Method.Location = new System.Drawing.Point(29, 323);
            this.label_Method.Name = "label_Method";
            this.label_Method.Size = new System.Drawing.Size(46, 13);
            this.label_Method.TabIndex = 21;
            this.label_Method.Text = "Method:";
            // 
            // label_Data
            // 
            this.label_Data.AutoSize = true;
            this.label_Data.Location = new System.Drawing.Point(29, 298);
            this.label_Data.Name = "label_Data";
            this.label_Data.Size = new System.Drawing.Size(33, 13);
            this.label_Data.TabIndex = 22;
            this.label_Data.Text = "Data:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 354);
            this.Controls.Add(this.label_Data);
            this.Controls.Add(this.label_Method);
            this.Controls.Add(this.comboBox_Method);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_RunTest);
            this.Controls.Add(this.btn_Hotsax_Squeezer);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_Squeezer_PLA);
            this.Controls.Add(this.label_threshold);
            this.Controls.Add(this.txt_Threshold);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_time);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_best_so_far_loc);
            this.Controls.Add(this.txt_best_so_far_dist);
            this.Controls.Add(this.btn_Squeezer);
            this.Controls.Add(this.btn_BitCluster);
            this.Controls.Add(this.txt_FileName);
            this.Controls.Add(this.txt_W_LENGTH);
            this.Controls.Add(this.txt_K_CENTERS);
            this.Controls.Add(this.txt_N_LENGTH);
            this.Controls.Add(this.label_FileName);
            this.Controls.Add(this.label_K_CENTERS);
            this.Controls.Add(this.label_W_LENGTH);
            this.Controls.Add(this.label_N_LENGTH);
            this.Controls.Add(this.btn_HotSax);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Discord_Timeseries - Ver 2.5 - Jan 3rd 2017";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_HotSax;
        private System.Windows.Forms.Label label_N_LENGTH;
        private System.Windows.Forms.Label label_W_LENGTH;
        private System.Windows.Forms.Label label_FileName;
        private System.Windows.Forms.TextBox txt_N_LENGTH;
        private System.Windows.Forms.TextBox txt_W_LENGTH;
        private System.Windows.Forms.TextBox txt_FileName;
        private System.Windows.Forms.Button btn_BitCluster;
        private System.Windows.Forms.Label label_K_CENTERS;
        private System.Windows.Forms.TextBox txt_K_CENTERS;
        private System.Windows.Forms.TextBox txt_best_so_far_dist;
        private System.Windows.Forms.TextBox txt_best_so_far_loc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_time;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Squeezer;
        private System.Windows.Forms.TextBox txt_Threshold;
        private System.Windows.Forms.Label label_threshold;
        private System.Windows.Forms.Button btn_Squeezer_PLA;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Hotsax_Squeezer;
        private System.Windows.Forms.Button btn_RunTest;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox_Method;
        private System.Windows.Forms.Label label_Method;
        private System.Windows.Forms.Label label_Data;
    }
}

