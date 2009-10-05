using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSC533
{
    public partial class MainForm : Form
    {
        private Knowledgebase knowledgebase;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            knowledgebase = new Knowledgebase();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fowardChainDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           textBoxMain.Clear();
            
           textBoxMain.Text = "Forward Chaining Demo"; 
        }

        private void backwardChainingDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxMain.Clear();

            textBoxMain.Text = "Backwards Chaining Demo"; 
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form about = new About();
            about.Show();
        }

        private void addRuleButton_Click(object sender, EventArgs e)
        {
            List<string> antecedents = new List<string>();
            string consequent = "";
            Rule rule;

            if (entryTextBox.Text.Contains('='))
            {
                string[] major = entryTextBox.Text.Split('=');
                consequent = major[1].Trim();
                string[] minor = major[0].Split('*');
                foreach (string term in minor)
                {
                    antecedents.Add(term.Trim());
                }
            }
            else
            {
                consequent = entryTextBox.Text.Trim();
            }

            rule = new Rule(antecedents, consequent);
            if (!knowledgebase.ContainsRule(rule))
            {
                knowledgebase.Tell(rule);
                ruleListBox.Items.Add(rule);
            }
            else
                MessageBox.Show("That rule already exists.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void removeRuleButton_Click(object sender, EventArgs e)
        {
            if (ruleListBox.SelectedItem != null)
            {
                knowledgebase.Remove((Rule)ruleListBox.SelectedItem);
                ruleListBox.Items.RemoveAt(ruleListBox.SelectedIndex);
            }
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            string query = queryTextBox.Text.Trim();
            
            if (!String.IsNullOrEmpty(query))
            {
                if (knowledgebase.AskForward(query))
                {
                    resultLabel.Text = "True";
                }
                else
                {
                    resultLabel.Text = "False";
                }

                resultLabel.Visible = true;
            }
        }

        private void backwardButton_Click(object sender, EventArgs e)
        {
            string query = queryTextBox.Text.Trim();

            if (!String.IsNullOrEmpty(query))
            {
                if (knowledgebase.AskBackward(query))
                {
                    resultLabel.Text = "True";
                }
                else
                {
                    resultLabel.Text = "False";
                }

                resultLabel.Visible = true;
            }
        }

       

        
       
    }
}
