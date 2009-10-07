namespace CSC533
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fowardChainDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backwardChainingDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardButton = new System.Windows.Forms.Button();
            this.backwardButton = new System.Windows.Forms.Button();
            this.entryTextBox = new System.Windows.Forms.TextBox();
            this.addRuleButton = new System.Windows.Forms.Button();
            this.removeRuleButton = new System.Windows.Forms.Button();
            this.queryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.ruleListBox = new System.Windows.Forms.ListBox();
            this.openButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(641, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fowardChainDemoToolStripMenuItem,
            this.backwardChainingDemoToolStripMenuItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // fowardChainDemoToolStripMenuItem
            // 
            this.fowardChainDemoToolStripMenuItem.Name = "fowardChainDemoToolStripMenuItem";
            this.fowardChainDemoToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.fowardChainDemoToolStripMenuItem.Text = "Foward Chaining Demo";
            this.fowardChainDemoToolStripMenuItem.Click += new System.EventHandler(this.fowardChainDemoToolStripMenuItem_Click);
            // 
            // backwardChainingDemoToolStripMenuItem
            // 
            this.backwardChainingDemoToolStripMenuItem.Name = "backwardChainingDemoToolStripMenuItem";
            this.backwardChainingDemoToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.backwardChainingDemoToolStripMenuItem.Text = "Backward Chaining Demo";
            this.backwardChainingDemoToolStripMenuItem.Click += new System.EventHandler(this.backwardChainingDemoToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(211, 22);
            this.toolStripMenuItem1.Text = "About";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(208, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // forwardButton
            // 
            this.forwardButton.Location = new System.Drawing.Point(299, 150);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(75, 23);
            this.forwardButton.TabIndex = 2;
            this.forwardButton.Text = "Forward";
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // backwardButton
            // 
            this.backwardButton.Location = new System.Drawing.Point(299, 179);
            this.backwardButton.Name = "backwardButton";
            this.backwardButton.Size = new System.Drawing.Size(75, 23);
            this.backwardButton.TabIndex = 3;
            this.backwardButton.Text = "Backward";
            this.backwardButton.UseVisualStyleBackColor = true;
            this.backwardButton.Click += new System.EventHandler(this.backwardButton_Click);
            // 
            // entryTextBox
            // 
            this.entryTextBox.Location = new System.Drawing.Point(12, 33);
            this.entryTextBox.Name = "entryTextBox";
            this.entryTextBox.Size = new System.Drawing.Size(156, 20);
            this.entryTextBox.TabIndex = 4;
            this.entryTextBox.Text = "Enter rule here...";
            this.entryTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.entryTextBox_KeyPress);
            // 
            // addRuleButton
            // 
            this.addRuleButton.Location = new System.Drawing.Point(174, 31);
            this.addRuleButton.Name = "addRuleButton";
            this.addRuleButton.Size = new System.Drawing.Size(75, 23);
            this.addRuleButton.TabIndex = 5;
            this.addRuleButton.Text = "Add Rule";
            this.addRuleButton.UseVisualStyleBackColor = true;
            this.addRuleButton.Click += new System.EventHandler(this.addRuleButton_Click);
            // 
            // removeRuleButton
            // 
            this.removeRuleButton.Location = new System.Drawing.Point(174, 265);
            this.removeRuleButton.Name = "removeRuleButton";
            this.removeRuleButton.Size = new System.Drawing.Size(75, 23);
            this.removeRuleButton.TabIndex = 7;
            this.removeRuleButton.Text = "Remove";
            this.removeRuleButton.UseVisualStyleBackColor = true;
            this.removeRuleButton.Click += new System.EventHandler(this.removeRuleButton_Click);
            // 
            // queryTextBox
            // 
            this.queryTextBox.Location = new System.Drawing.Point(274, 124);
            this.queryTextBox.Name = "queryTextBox";
            this.queryTextBox.Size = new System.Drawing.Size(100, 20);
            this.queryTextBox.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(271, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Query:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Result: ";
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.ForeColor = System.Drawing.Color.Maroon;
            this.resultLabel.Location = new System.Drawing.Point(295, 236);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(45, 20);
            this.resultLabel.TabIndex = 11;
            this.resultLabel.Text = "True";
            this.resultLabel.Visible = false;
            // 
            // ruleListBox
            // 
            this.ruleListBox.FormattingEnabled = true;
            this.ruleListBox.Location = new System.Drawing.Point(12, 60);
            this.ruleListBox.Name = "ruleListBox";
            this.ruleListBox.Size = new System.Drawing.Size(237, 199);
            this.ruleListBox.TabIndex = 12;
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(12, 265);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 13;
            this.openButton.Text = "Open...";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(93, 265);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 14;
            this.saveButton.Text = "Save...";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(392, 60);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(237, 199);
            this.outputTextBox.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(389, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Output:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 296);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.ruleListBox);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.queryTextBox);
            this.Controls.Add(this.removeRuleButton);
            this.Controls.Add(this.addRuleButton);
            this.Controls.Add(this.entryTextBox);
            this.Controls.Add(this.backwardButton);
            this.Controls.Add(this.forwardButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "CSC 533 ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem fowardChainDemoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backwardChainingDemoToolStripMenuItem;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button backwardButton;
        private System.Windows.Forms.TextBox entryTextBox;
        private System.Windows.Forms.Button addRuleButton;
        private System.Windows.Forms.Button removeRuleButton;
        private System.Windows.Forms.TextBox queryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.ListBox ruleListBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label3;
    }
}

