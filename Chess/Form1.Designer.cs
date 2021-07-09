namespace Chess
{
    partial class Chess
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button_PlayWithEngine = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ходы";
            // 
            // button_PlayWithEngine
            // 
            this.button_PlayWithEngine.Location = new System.Drawing.Point(628, 12);
            this.button_PlayWithEngine.Name = "button_PlayWithEngine";
            this.button_PlayWithEngine.Size = new System.Drawing.Size(184, 23);
            this.button_PlayWithEngine.TabIndex = 1;
            this.button_PlayWithEngine.Text = "Играть с шахматным движком";
            this.button_PlayWithEngine.UseVisualStyleBackColor = true;
            this.button_PlayWithEngine.Click += new System.EventHandler(this.button_PlayWithEngine_Click);
            // 
            // Chess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 801);
            this.Controls.Add(this.button_PlayWithEngine);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "Chess";
            this.Text = "Chess";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chess_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Chess_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Chess_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Chess_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_PlayWithEngine;
    }
}

