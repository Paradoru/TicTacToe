namespace TicTacToe
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
            this.boardPanel = new System.Windows.Forms.Panel();
            this.isAgainstBotCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // isAgainstBotCheckBox
            this.isAgainstBotCheckBox.AutoSize = true;
            this.isAgainstBotCheckBox.Location = new Point(552, 16);
            this.isAgainstBotCheckBox.Name = "isAgainstBotCheckBox";
            this.isAgainstBotCheckBox.TabIndex = 0;
            this.isAgainstBotCheckBox.Text = "Против бота";
            this.isAgainstBotCheckBox.CheckedChanged += new System.EventHandler(this.isAgainstBotCheckBox_CheckedChanged);
            // boardPanel
            this.boardPanel.Location = new System.Drawing.Point(12, 35);
            this.boardPanel.Name = "boardPanel";
            this.boardPanel.TabIndex = 1;
            // Restart
            this.restartButton = new System.Windows.Forms.Button();
            this.restartButton.Location = new System.Drawing.Point(552, 55);
            this.restartButton.Name = "restartButton";
            this.restartButton.Size = new System.Drawing.Size(80, 30);
            this.restartButton.Text = "Restart";
            this.restartButton.Click += new System.EventHandler(this.RestartButton_Click);
            this.Controls.Add(this.restartButton);
            // MainForm
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 447);
            this.Controls.Add(this.isAgainstBotCheckBox);
            this.Name = "MainForm";
            this.Text = "Tic Tac Toe";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.CheckBox isAgainstBotCheckBox;
        private System.Windows.Forms.Panel boardPanel;
        private void isAgainstBotCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            isAgainstBot = checkBox.Checked;
        }
    }
}
