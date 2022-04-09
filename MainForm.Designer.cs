namespace FrequencyAnalysis
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.CloseAppBut = new System.Windows.Forms.Button();
            this.PlainText = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.AnalyzeSentencesType = new System.Windows.Forms.Button();
            this.AnalyzeWordsType = new System.Windows.Forms.Button();
            this.AnalyzeSymbolsType = new System.Windows.Forms.Button();
            this.AnalyzeGistogrammRus = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AnalyzeGistogrammRus)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.CloseAppBut);
            this.panel1.Location = new System.Drawing.Point(0, -2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 38);
            this.panel1.TabIndex = 0;
            // 
            // CloseAppBut
            // 
            this.CloseAppBut.BackColor = System.Drawing.Color.Red;
            this.CloseAppBut.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CloseAppBut.Location = new System.Drawing.Point(1882, 0);
            this.CloseAppBut.Name = "CloseAppBut";
            this.CloseAppBut.Size = new System.Drawing.Size(38, 38);
            this.CloseAppBut.TabIndex = 0;
            this.CloseAppBut.Text = "x";
            this.CloseAppBut.UseVisualStyleBackColor = false;
            this.CloseAppBut.Click += new System.EventHandler(this.CloseAppBut_Click);
            // 
            // PlainText
            // 
            this.PlainText.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PlainText.Location = new System.Drawing.Point(12, 42);
            this.PlainText.Name = "PlainText";
            this.PlainText.Size = new System.Drawing.Size(848, 955);
            this.PlainText.TabIndex = 1;
            this.PlainText.Text = "";
            this.PlainText.TextChanged += new System.EventHandler(this.PlainText_TextChanged);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.AnalyzeSentencesType);
            this.panel2.Controls.Add(this.AnalyzeWordsType);
            this.panel2.Controls.Add(this.AnalyzeSymbolsType);
            this.panel2.Location = new System.Drawing.Point(12, 1003);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(848, 65);
            this.panel2.TabIndex = 2;
            // 
            // AnalyzeSentencesType
            // 
            this.AnalyzeSentencesType.Location = new System.Drawing.Point(610, 3);
            this.AnalyzeSentencesType.Name = "AnalyzeSentencesType";
            this.AnalyzeSentencesType.Size = new System.Drawing.Size(237, 57);
            this.AnalyzeSentencesType.TabIndex = 2;
            this.AnalyzeSentencesType.Text = "По Предложениям";
            this.AnalyzeSentencesType.UseVisualStyleBackColor = true;
            this.AnalyzeSentencesType.Click += new System.EventHandler(this.AnalyzeSentencesType_Click);
            // 
            // AnalyzeWordsType
            // 
            this.AnalyzeWordsType.Location = new System.Drawing.Point(307, 3);
            this.AnalyzeWordsType.Name = "AnalyzeWordsType";
            this.AnalyzeWordsType.Size = new System.Drawing.Size(237, 57);
            this.AnalyzeWordsType.TabIndex = 1;
            this.AnalyzeWordsType.Text = "По Словам";
            this.AnalyzeWordsType.UseVisualStyleBackColor = true;
            this.AnalyzeWordsType.Click += new System.EventHandler(this.AnalyzeWordsType_Click);
            // 
            // AnalyzeSymbolsType
            // 
            this.AnalyzeSymbolsType.Location = new System.Drawing.Point(3, 3);
            this.AnalyzeSymbolsType.Name = "AnalyzeSymbolsType";
            this.AnalyzeSymbolsType.Size = new System.Drawing.Size(237, 57);
            this.AnalyzeSymbolsType.TabIndex = 0;
            this.AnalyzeSymbolsType.Text = "По Буквам";
            this.AnalyzeSymbolsType.UseVisualStyleBackColor = true;
            this.AnalyzeSymbolsType.Click += new System.EventHandler(this.AnalyzeSymbolsType_Click);
            // 
            // AnalyzeGistogrammRus
            // 
            this.AnalyzeGistogrammRus.Location = new System.Drawing.Point(930, 42);
            this.AnalyzeGistogrammRus.Name = "AnalyzeGistogrammRus";
            this.AnalyzeGistogrammRus.Size = new System.Drawing.Size(960, 1022);
            this.AnalyzeGistogrammRus.TabIndex = 4;
            this.AnalyzeGistogrammRus.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.AnalyzeGistogrammRus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.PlainText);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AnalyzeGistogrammRus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button CloseAppBut;
        private System.Windows.Forms.RichTextBox PlainText;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button AnalyzeSentencesType;
        private System.Windows.Forms.Button AnalyzeWordsType;
        private System.Windows.Forms.Button AnalyzeSymbolsType;
        private System.Windows.Forms.PictureBox AnalyzeGistogrammRus;
    }
}
