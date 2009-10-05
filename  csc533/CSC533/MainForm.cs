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

        //Add the rule that the use has typed into the entry text box to the knowledgebase
        private void addRuleButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(entryTextBox.Text))
            {
                Rule rule = parseRule(entryTextBox.Text);
                
                //If parsing was successful, enter rule into knowledgebase
                if (rule != null)
                {                    
                    if (!knowledgebase.ContainsRule(rule))
                    {
                        knowledgebase.Tell(rule);
                        ruleListBox.Items.Add(rule);
                        entryTextBox.Clear();
                    }
                    else
                        MessageBox.Show("That rule already exists.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void removeRuleButton_Click(object sender, EventArgs e)
        {
            if (ruleListBox.SelectedItem != null)
            {
                entryTextBox.Text = ruleListBox.SelectedItem.ToString();
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

        //Support pressing enter key in entry text box.
        //Question: why does system play default beep when pressing enter?
        private void entryTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                addRuleButton_Click(sender, e);
            }
        }


        //Parses a string and returns a rule. If input is invalid, returns null.
        //Format: Either (symbol) or (conjunction => symbol) using ^ for "AND" and = for "=>"
        //e.g. "A", "A^B=C"
        private Rule parseRule(string input)
        {
            List<string> antecedents = new List<string>();
            string consequent = "";

            input = input.ToUpper();
            input = input.Replace('˄', '^');
            input = input.Replace("=>", "=");

            //Split the input first by '=' and then by '^'
            //Left hand terms go into antecedents list; right hand side goes to consequent
            if (input.Contains('='))
            {
                string[] major = input.Split('=');
                consequent = major[1].Trim();
                string[] minor = major[0].Split('^');
                foreach (string term in minor)
                {
                    antecedents.Add(term.Trim());
                }
            }
            else
            {
                consequent = input.Trim();
            }

            //Check for errors in format here
            if (consequent.Contains('^') || antecedents.Contains(""))
            {
                MessageBox.Show("Invalid rule format.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            else
            {
                return (new Rule(antecedents, consequent));
            }

        }       
       
    }
}
