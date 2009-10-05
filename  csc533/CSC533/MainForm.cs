using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CSC533
{
    public partial class MainForm : Form
    {
        private Knowledgebase knowledgebase;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private bool knowledgebaseChanged;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            knowledgebase = new Knowledgebase();
            knowledgebaseChanged = false;

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt";
            openFileDialog.FileName = "Knowledgebase.txt";
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
            saveFileDialog.FileName = "Knowledgebase.txt";
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
                        resultLabel.Visible = false;
                        knowledgebaseChanged = true;
                    }
                    else
                        MessageBox.Show("That rule already exists.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        //Remove the selected rule from the knowledgebase
        private void removeRuleButton_Click(object sender, EventArgs e)
        {
            if (ruleListBox.SelectedItem != null)
            {
                entryTextBox.Text = ruleListBox.SelectedItem.ToString();
                knowledgebase.Remove((Rule)ruleListBox.SelectedItem);
                ruleListBox.Items.RemoveAt(ruleListBox.SelectedIndex);
                resultLabel.Visible = false;
                knowledgebaseChanged = true;
            }
        }

        //Evaluate query by forward chaining
        private void forwardButton_Click(object sender, EventArgs e)
        {
            string query = queryTextBox.Text.Trim();

            if (!String.IsNullOrEmpty(query))
            {
                if (knowledgebase.AskForward(query.ToUpper()))
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

        //Evaluate query by backward chaining
        private void backwardButton_Click(object sender, EventArgs e)
        {
            string query = queryTextBox.Text.Trim();

            if (!String.IsNullOrEmpty(query))
            {
                if (knowledgebase.AskBackward(query.ToUpper()))
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

        //Open a text file containing rules: one rule per line in the same format as they are entered in
        //the entry text box
        private void openButton_Click(object sender, EventArgs e)
        {
            if (knowledgebaseChanged)
            {
                if (MessageBox.Show("The knowledgebase has changed. Would you like to save it first?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        {
                            knowledgebase.SaveToText(writer);
                        }
                    }
                }
            }

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ruleListBox.Items.Clear();
                knowledgebase = new Knowledgebase();

                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    while (!reader.EndOfStream)
                    {
                        string nextLine = reader.ReadLine();

                        if (!String.IsNullOrEmpty(nextLine))
                        {
                            Rule rule = parseRule(nextLine);

                            //If parsing was successful, enter rule into knowledgebase
                            if (rule != null)
                            {
                                if (!knowledgebase.ContainsRule(rule))
                                {
                                    knowledgebase.Tell(rule);
                                    ruleListBox.Items.Add(rule);
                                }
                            }
                        }
                    }
                }

                entryTextBox.Clear();
                resultLabel.Visible = false;
                knowledgebaseChanged = false;
            }

        }

        //Save the knowledgebase to a text file
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    knowledgebase.SaveToText(writer);
                }

                knowledgebaseChanged = false;
            }
        }

    }
}
