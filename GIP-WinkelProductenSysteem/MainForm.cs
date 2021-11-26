using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.VisualBasic;
namespace GIP_WinkelProductenSysteem
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void btnChangeProd_Click(object sender, EventArgs e)
        {
            ChangeProd form = new ChangeProd();
            form.Show();
            

        }
    }
}
