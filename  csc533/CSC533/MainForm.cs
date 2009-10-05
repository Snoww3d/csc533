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
            
           textBoxMain.Text = "Forward Chainning Demo"; 
        }

        private void backwardChainingDemoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBoxMain.Clear();

            textBoxMain.Text = "Backwards Chainning Demo"; 
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form about = new About();
            about.Show();
        }

       

        
       
    }
}
