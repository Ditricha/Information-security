
namespace Homework_3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.EncryptButton = new System.Windows.Forms.Button();
            this.DecryptButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.справкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оЗаданиеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EncryptButton
            // 
            this.EncryptButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.EncryptButton.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.EncryptButton.Location = new System.Drawing.Point(12, 55);
            this.EncryptButton.Name = "EncryptButton";
            this.EncryptButton.Size = new System.Drawing.Size(115, 170);
            this.EncryptButton.TabIndex = 0;
            this.EncryptButton.Text = "выбрать картинку и текст для шифра\r\n";
            this.EncryptButton.UseVisualStyleBackColor = false;
            this.EncryptButton.Click += new System.EventHandler(this.EncryptButton_Click);
            // 
            // DecryptButton
            // 
            this.DecryptButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DecryptButton.Font = new System.Drawing.Font("Century Gothic", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DecryptButton.Location = new System.Drawing.Point(10, 348);
            this.DecryptButton.Name = "DecryptButton";
            this.DecryptButton.Size = new System.Drawing.Size(115, 170);
            this.DecryptButton.TabIndex = 1;
            this.DecryptButton.Text = "выбрать картинку для расшифровки и сохранить результат";
            this.DecryptButton.UseVisualStyleBackColor = false;
            this.DecryptButton.Click += new System.EventHandler(this.DecryptButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(131, 55);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(705, 463);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.справкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(847, 30);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // справкаToolStripMenuItem
            // 
            this.справкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оЗаданиеToolStripMenuItem});
            this.справкаToolStripMenuItem.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.справкаToolStripMenuItem.Name = "справкаToolStripMenuItem";
            this.справкаToolStripMenuItem.Size = new System.Drawing.Size(107, 26);
            this.справкаToolStripMenuItem.Text = "Справка";
            // 
            // оЗаданиеToolStripMenuItem
            // 
            this.оЗаданиеToolStripMenuItem.Name = "оЗаданиеToolStripMenuItem";
            this.оЗаданиеToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.оЗаданиеToolStripMenuItem.Text = "О задание";
            this.оЗаданиеToolStripMenuItem.Click += new System.EventHandler(this.оЗаданиеToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(847, 528);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.DecryptButton);
            this.Controls.Add(this.EncryptButton);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "ЛР 3: Стеганография";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button EncryptButton;
        private System.Windows.Forms.Button DecryptButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem справкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оЗаданиеToolStripMenuItem;
    }
}

