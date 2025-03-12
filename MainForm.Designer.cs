namespace KTANE_Diffusal_Assistant
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
            button1 = new Button();
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtRecognised = new TextBox();
            txtSpeech = new TextBox();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Green;
            button1.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button1.ForeColor = Color.Black;
            button1.Location = new Point(98, 246);
            button1.Name = "button1";
            button1.Size = new Size(391, 78);
            button1.TabIndex = 0;
            button1.Text = "Start Listening";
            button1.UseVisualStyleBackColor = false;
            button1.Click += listenButtonClicked;
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(118, 61);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(376, 40);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += onVoiceChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(36, 61);
            label1.Name = "label1";
            label1.Size = new Size(76, 32);
            label1.TabIndex = 2;
            label1.Text = "Voice:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(36, 113);
            label2.Name = "label2";
            label2.Size = new Size(178, 32);
            label2.TabIndex = 3;
            label2.Text = "Last Command:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(36, 168);
            label3.Name = "label3";
            label3.Size = new Size(168, 32);
            label3.TabIndex = 4;
            label3.Text = "Last Response:";
            // 
            // txtRecognised
            // 
            txtRecognised.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtRecognised.Location = new Point(220, 116);
            txtRecognised.Name = "txtRecognised";
            txtRecognised.Size = new Size(348, 39);
            txtRecognised.TabIndex = 5;
            // 
            // txtSpeech
            // 
            txtSpeech.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSpeech.Location = new Point(220, 168);
            txtSpeech.Name = "txtSpeech";
            txtSpeech.Size = new Size(348, 39);
            txtSpeech.TabIndex = 6;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(610, 336);
            Controls.Add(txtSpeech);
            Controls.Add(txtRecognised);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Name = "MainForm";
            Text = "KTANE Diffusal Assistant";
            Load += onLoad;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtRecognised;
        private TextBox txtSpeech;
    }
}
