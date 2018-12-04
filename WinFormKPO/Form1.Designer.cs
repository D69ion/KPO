namespace WinFormKPO
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.textBox = new System.Windows.Forms.TextBox();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.buttonTask1 = new System.Windows.Forms.Button();
            this.buttonTask2 = new System.Windows.Forms.Button();
            this.buttonTask3 = new System.Windows.Forms.Button();
            this.buttonTask4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox.Location = new System.Drawing.Point(12, 3);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(179, 630);
            this.textBox.TabIndex = 0;
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.AutoSize = true;
            this.zedGraphControl.Location = new System.Drawing.Point(197, 53);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(1016, 580);
            this.zedGraphControl.TabIndex = 1;
            this.zedGraphControl.UseExtendedPrintDialog = true;
            // 
            // buttonTask1
            // 
            this.buttonTask1.Location = new System.Drawing.Point(252, 12);
            this.buttonTask1.Name = "buttonTask1";
            this.buttonTask1.Size = new System.Drawing.Size(75, 23);
            this.buttonTask1.TabIndex = 2;
            this.buttonTask1.Text = "Task1";
            this.buttonTask1.UseVisualStyleBackColor = true;
            this.buttonTask1.Click += new System.EventHandler(this.buttonTask1_Click);
            // 
            // buttonTask2
            // 
            this.buttonTask2.Location = new System.Drawing.Point(333, 12);
            this.buttonTask2.Name = "buttonTask2";
            this.buttonTask2.Size = new System.Drawing.Size(75, 23);
            this.buttonTask2.TabIndex = 3;
            this.buttonTask2.Text = "Task2";
            this.buttonTask2.UseVisualStyleBackColor = true;
            this.buttonTask2.Click += new System.EventHandler(this.buttonTask2_Click);
            // 
            // buttonTask3
            // 
            this.buttonTask3.Location = new System.Drawing.Point(414, 12);
            this.buttonTask3.Name = "buttonTask3";
            this.buttonTask3.Size = new System.Drawing.Size(75, 23);
            this.buttonTask3.TabIndex = 4;
            this.buttonTask3.Text = "Task3";
            this.buttonTask3.UseVisualStyleBackColor = true;
            this.buttonTask3.Click += new System.EventHandler(this.buttonTask3_Click);
            // 
            // buttonTask4
            // 
            this.buttonTask4.Location = new System.Drawing.Point(495, 12);
            this.buttonTask4.Name = "buttonTask4";
            this.buttonTask4.Size = new System.Drawing.Size(75, 23);
            this.buttonTask4.TabIndex = 5;
            this.buttonTask4.Text = "Task4";
            this.buttonTask4.UseVisualStyleBackColor = true;
            this.buttonTask4.Click += new System.EventHandler(this.buttonTask4_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1225, 645);
            this.Controls.Add(this.buttonTask4);
            this.Controls.Add(this.buttonTask3);
            this.Controls.Add(this.buttonTask2);
            this.Controls.Add(this.buttonTask1);
            this.Controls.Add(this.zedGraphControl);
            this.Controls.Add(this.textBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox;
        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.Button buttonTask1;
        private System.Windows.Forms.Button buttonTask2;
        private System.Windows.Forms.Button buttonTask3;
        private System.Windows.Forms.Button buttonTask4;
    }
}

