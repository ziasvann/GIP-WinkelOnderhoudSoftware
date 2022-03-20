using System;
using System.Windows.Forms;
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

        private void btnKassaFrm_Click(object sender, EventArgs e)
        {
            Kassa form = new Kassa();
            form.Show();
        }
    }
}
