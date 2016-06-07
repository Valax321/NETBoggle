﻿namespace NETBoggle.Client
{
    partial class MainMenu
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
            this.components = new System.ComponentModel.Container();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hostNewGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.letter_r1c1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.letter_r4c4 = new System.Windows.Forms.Label();
            this.letter_r4c3 = new System.Windows.Forms.Label();
            this.letter_r4c2 = new System.Windows.Forms.Label();
            this.letter_r4c1 = new System.Windows.Forms.Label();
            this.letter_r3c4 = new System.Windows.Forms.Label();
            this.letter_r3c3 = new System.Windows.Forms.Label();
            this.letter_r3c2 = new System.Windows.Forms.Label();
            this.letter_r3c1 = new System.Windows.Forms.Label();
            this.letter_r2c4 = new System.Windows.Forms.Label();
            this.letter_r2c3 = new System.Windows.Forms.Label();
            this.letter_r2c2 = new System.Windows.Forms.Label();
            this.letter_r2c1 = new System.Windows.Forms.Label();
            this.letter_r1c4 = new System.Windows.Forms.Label();
            this.letter_r1c3 = new System.Windows.Forms.Label();
            this.letter_r1c2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridScoreboard = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxWordInput = new System.Windows.Forms.TextBox();
            this.buttonReadyRound = new System.Windows.Forms.Button();
            this.lblTimeRemain = new System.Windows.Forms.Label();
            this.ServerTick = new System.Windows.Forms.Timer(this.components);
            this.labelReadyPlayers = new System.Windows.Forms.Label();
            this.menuMain.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridScoreboard)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(572, 24);
            this.menuMain.TabIndex = 1;
            this.menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hostNewGameToolStripMenuItem,
            this.joinGameToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.newGameToolStripMenuItem.Text = "New Game";
            // 
            // hostNewGameToolStripMenuItem
            // 
            this.hostNewGameToolStripMenuItem.Name = "hostNewGameToolStripMenuItem";
            this.hostNewGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.N)));
            this.hostNewGameToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.hostNewGameToolStripMenuItem.Text = "Host New Game";
            this.hostNewGameToolStripMenuItem.Click += new System.EventHandler(this.hostNewGameToolStripMenuItem_Click);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.joinGameToolStripMenuItem.Text = "Join Game";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            // 
            // letter_r1c1
            // 
            this.letter_r1c1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r1c1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r1c1.Location = new System.Drawing.Point(6, 16);
            this.letter_r1c1.Name = "letter_r1c1";
            this.letter_r1c1.Size = new System.Drawing.Size(42, 40);
            this.letter_r1c1.TabIndex = 2;
            this.letter_r1c1.Text = "A";
            this.letter_r1c1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.letter_r4c4);
            this.groupBox1.Controls.Add(this.letter_r4c3);
            this.groupBox1.Controls.Add(this.letter_r4c2);
            this.groupBox1.Controls.Add(this.letter_r4c1);
            this.groupBox1.Controls.Add(this.letter_r3c4);
            this.groupBox1.Controls.Add(this.letter_r3c3);
            this.groupBox1.Controls.Add(this.letter_r3c2);
            this.groupBox1.Controls.Add(this.letter_r3c1);
            this.groupBox1.Controls.Add(this.letter_r2c4);
            this.groupBox1.Controls.Add(this.letter_r2c3);
            this.groupBox1.Controls.Add(this.letter_r2c2);
            this.groupBox1.Controls.Add(this.letter_r2c1);
            this.groupBox1.Controls.Add(this.letter_r1c4);
            this.groupBox1.Controls.Add(this.letter_r1c3);
            this.groupBox1.Controls.Add(this.letter_r1c2);
            this.groupBox1.Controls.Add(this.letter_r1c1);
            this.groupBox1.Location = new System.Drawing.Point(13, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 204);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Board";
            // 
            // letter_r4c4
            // 
            this.letter_r4c4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r4c4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r4c4.Location = new System.Drawing.Point(150, 155);
            this.letter_r4c4.Name = "letter_r4c4";
            this.letter_r4c4.Size = new System.Drawing.Size(42, 40);
            this.letter_r4c4.TabIndex = 17;
            this.letter_r4c4.Text = "A";
            this.letter_r4c4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r4c3
            // 
            this.letter_r4c3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r4c3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r4c3.Location = new System.Drawing.Point(102, 155);
            this.letter_r4c3.Name = "letter_r4c3";
            this.letter_r4c3.Size = new System.Drawing.Size(42, 40);
            this.letter_r4c3.TabIndex = 16;
            this.letter_r4c3.Text = "A";
            this.letter_r4c3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r4c2
            // 
            this.letter_r4c2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r4c2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r4c2.Location = new System.Drawing.Point(54, 155);
            this.letter_r4c2.Name = "letter_r4c2";
            this.letter_r4c2.Size = new System.Drawing.Size(42, 40);
            this.letter_r4c2.TabIndex = 15;
            this.letter_r4c2.Text = "A";
            this.letter_r4c2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r4c1
            // 
            this.letter_r4c1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r4c1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r4c1.Location = new System.Drawing.Point(6, 155);
            this.letter_r4c1.Name = "letter_r4c1";
            this.letter_r4c1.Size = new System.Drawing.Size(42, 40);
            this.letter_r4c1.TabIndex = 14;
            this.letter_r4c1.Text = "A";
            this.letter_r4c1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r3c4
            // 
            this.letter_r3c4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r3c4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r3c4.Location = new System.Drawing.Point(150, 109);
            this.letter_r3c4.Name = "letter_r3c4";
            this.letter_r3c4.Size = new System.Drawing.Size(42, 40);
            this.letter_r3c4.TabIndex = 13;
            this.letter_r3c4.Text = "A";
            this.letter_r3c4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r3c3
            // 
            this.letter_r3c3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r3c3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r3c3.Location = new System.Drawing.Point(102, 109);
            this.letter_r3c3.Name = "letter_r3c3";
            this.letter_r3c3.Size = new System.Drawing.Size(42, 40);
            this.letter_r3c3.TabIndex = 12;
            this.letter_r3c3.Text = "A";
            this.letter_r3c3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r3c2
            // 
            this.letter_r3c2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r3c2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r3c2.Location = new System.Drawing.Point(54, 109);
            this.letter_r3c2.Name = "letter_r3c2";
            this.letter_r3c2.Size = new System.Drawing.Size(42, 40);
            this.letter_r3c2.TabIndex = 11;
            this.letter_r3c2.Text = "A";
            this.letter_r3c2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r3c1
            // 
            this.letter_r3c1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r3c1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r3c1.Location = new System.Drawing.Point(6, 109);
            this.letter_r3c1.Name = "letter_r3c1";
            this.letter_r3c1.Size = new System.Drawing.Size(42, 40);
            this.letter_r3c1.TabIndex = 10;
            this.letter_r3c1.Text = "A";
            this.letter_r3c1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r2c4
            // 
            this.letter_r2c4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r2c4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r2c4.Location = new System.Drawing.Point(150, 63);
            this.letter_r2c4.Name = "letter_r2c4";
            this.letter_r2c4.Size = new System.Drawing.Size(42, 40);
            this.letter_r2c4.TabIndex = 9;
            this.letter_r2c4.Text = "A";
            this.letter_r2c4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r2c3
            // 
            this.letter_r2c3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r2c3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r2c3.Location = new System.Drawing.Point(102, 63);
            this.letter_r2c3.Name = "letter_r2c3";
            this.letter_r2c3.Size = new System.Drawing.Size(42, 40);
            this.letter_r2c3.TabIndex = 8;
            this.letter_r2c3.Text = "A";
            this.letter_r2c3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r2c2
            // 
            this.letter_r2c2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r2c2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r2c2.Location = new System.Drawing.Point(54, 63);
            this.letter_r2c2.Name = "letter_r2c2";
            this.letter_r2c2.Size = new System.Drawing.Size(42, 40);
            this.letter_r2c2.TabIndex = 7;
            this.letter_r2c2.Text = "A";
            this.letter_r2c2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r2c1
            // 
            this.letter_r2c1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r2c1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r2c1.Location = new System.Drawing.Point(6, 63);
            this.letter_r2c1.Name = "letter_r2c1";
            this.letter_r2c1.Size = new System.Drawing.Size(42, 40);
            this.letter_r2c1.TabIndex = 6;
            this.letter_r2c1.Text = "A";
            this.letter_r2c1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r1c4
            // 
            this.letter_r1c4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r1c4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r1c4.Location = new System.Drawing.Point(150, 16);
            this.letter_r1c4.Name = "letter_r1c4";
            this.letter_r1c4.Size = new System.Drawing.Size(42, 40);
            this.letter_r1c4.TabIndex = 5;
            this.letter_r1c4.Text = "A";
            this.letter_r1c4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r1c3
            // 
            this.letter_r1c3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r1c3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r1c3.Location = new System.Drawing.Point(102, 16);
            this.letter_r1c3.Name = "letter_r1c3";
            this.letter_r1c3.Size = new System.Drawing.Size(42, 40);
            this.letter_r1c3.TabIndex = 4;
            this.letter_r1c3.Text = "A";
            this.letter_r1c3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // letter_r1c2
            // 
            this.letter_r1c2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.letter_r1c2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.letter_r1c2.Location = new System.Drawing.Point(54, 16);
            this.letter_r1c2.Name = "letter_r1c2";
            this.letter_r1c2.Size = new System.Drawing.Size(42, 40);
            this.letter_r1c2.TabIndex = 3;
            this.letter_r1c2.Text = "A";
            this.letter_r1c2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridScoreboard);
            this.groupBox2.Location = new System.Drawing.Point(366, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 479);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Score Board";
            // 
            // dataGridScoreboard
            // 
            this.dataGridScoreboard.AllowUserToAddRows = false;
            this.dataGridScoreboard.AllowUserToDeleteRows = false;
            this.dataGridScoreboard.AllowUserToResizeRows = false;
            this.dataGridScoreboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridScoreboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridScoreboard.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridScoreboard.Location = new System.Drawing.Point(3, 16);
            this.dataGridScoreboard.Name = "dataGridScoreboard";
            this.dataGridScoreboard.ReadOnly = true;
            this.dataGridScoreboard.RowHeadersVisible = false;
            this.dataGridScoreboard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridScoreboard.Size = new System.Drawing.Size(194, 460);
            this.dataGridScoreboard.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.textBoxWordInput);
            this.groupBox3.Location = new System.Drawing.Point(13, 238);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 281);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Words";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(7, 20);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(337, 229);
            this.textBox1.TabIndex = 1;
            // 
            // textBoxWordInput
            // 
            this.textBoxWordInput.Location = new System.Drawing.Point(6, 255);
            this.textBoxWordInput.Name = "textBoxWordInput";
            this.textBoxWordInput.ReadOnly = true;
            this.textBoxWordInput.Size = new System.Drawing.Size(338, 20);
            this.textBoxWordInput.TabIndex = 0;
            // 
            // buttonReadyRound
            // 
            this.buttonReadyRound.Enabled = false;
            this.buttonReadyRound.Location = new System.Drawing.Point(223, 193);
            this.buttonReadyRound.Name = "buttonReadyRound";
            this.buttonReadyRound.Size = new System.Drawing.Size(140, 38);
            this.buttonReadyRound.TabIndex = 6;
            this.buttonReadyRound.Text = "Ready";
            this.buttonReadyRound.UseVisualStyleBackColor = true;
            // 
            // lblTimeRemain
            // 
            this.lblTimeRemain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTimeRemain.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemain.Location = new System.Drawing.Point(224, 44);
            this.lblTimeRemain.Name = "lblTimeRemain";
            this.lblTimeRemain.Size = new System.Drawing.Size(136, 40);
            this.lblTimeRemain.TabIndex = 7;
            this.lblTimeRemain.Text = "label1";
            this.lblTimeRemain.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServerTick
            // 
            this.ServerTick.Tick += new System.EventHandler(this.ServerTick_Tick);
            // 
            // labelReadyPlayers
            // 
            this.labelReadyPlayers.Location = new System.Drawing.Point(224, 174);
            this.labelReadyPlayers.Name = "labelReadyPlayers";
            this.labelReadyPlayers.Size = new System.Drawing.Size(133, 16);
            this.labelReadyPlayers.TabIndex = 8;
            this.labelReadyPlayers.Text = "Ready: 10/50";
            this.labelReadyPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 531);
            this.Controls.Add(this.labelReadyPlayers);
            this.Controls.Add(this.lblTimeRemain);
            this.Controls.Add(this.buttonReadyRound);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuMain);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuMain;
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NET Boggle";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainMenu_FormClosed);
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridScoreboard)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hostNewGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.Label letter_r1c1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label letter_r1c2;
        private System.Windows.Forms.Label letter_r1c4;
        private System.Windows.Forms.Label letter_r1c3;
        private System.Windows.Forms.Label letter_r4c4;
        private System.Windows.Forms.Label letter_r4c3;
        private System.Windows.Forms.Label letter_r4c2;
        private System.Windows.Forms.Label letter_r4c1;
        private System.Windows.Forms.Label letter_r3c4;
        private System.Windows.Forms.Label letter_r3c3;
        private System.Windows.Forms.Label letter_r3c2;
        private System.Windows.Forms.Label letter_r3c1;
        private System.Windows.Forms.Label letter_r2c4;
        private System.Windows.Forms.Label letter_r2c3;
        private System.Windows.Forms.Label letter_r2c2;
        private System.Windows.Forms.Label letter_r2c1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridScoreboard;
        private System.Windows.Forms.TextBox textBoxWordInput;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonReadyRound;
        private System.Windows.Forms.Label lblTimeRemain;
        private System.Windows.Forms.Timer ServerTick;
        private System.Windows.Forms.Label labelReadyPlayers;
    }
}

