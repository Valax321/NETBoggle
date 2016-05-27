namespace NETBoggleClient
{
    partial class Debugger
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
            this.textboxDebugLog = new System.Windows.Forms.RichTextBox();
            this.menuDebug = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // textboxDebugLog
            // 
            this.textboxDebugLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxDebugLog.Location = new System.Drawing.Point(12, 27);
            this.textboxDebugLog.Name = "textboxDebugLog";
            this.textboxDebugLog.ReadOnly = true;
            this.textboxDebugLog.Size = new System.Drawing.Size(444, 339);
            this.textboxDebugLog.TabIndex = 0;
            this.textboxDebugLog.Text = "";
            // 
            // menuDebug
            // 
            this.menuDebug.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuDebug.Location = new System.Drawing.Point(0, 0);
            this.menuDebug.Name = "menuDebug";
            this.menuDebug.Size = new System.Drawing.Size(468, 24);
            this.menuDebug.TabIndex = 1;
            this.menuDebug.Text = "Debug Menu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // Debugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 378);
            this.Controls.Add(this.textboxDebugLog);
            this.Controls.Add(this.menuDebug);
            this.MainMenuStrip = this.menuDebug;
            this.Name = "Debugger";
            this.Text = "Debugger";
            this.menuDebug.ResumeLayout(false);
            this.menuDebug.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox textboxDebugLog;
        private System.Windows.Forms.MenuStrip menuDebug;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
    }
}