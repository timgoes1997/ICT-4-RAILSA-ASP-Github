using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ICT4Rails.Scripts;

namespace ICT4Rails
{
    public partial class frmLogin : Form
    {
        private frmAlgemeen algemeen;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            InlogController ic = new InlogController();
            DatabaseController d = new DatabaseController();

            if (d.IsLoginCorrect(tbGebruikersnaam.Text, ic.GetHashSha256(tbWachtwoord.Text)))
            {
                frmAlgemeen f = new frmAlgemeen();
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Onjuiste login-gegevens.");
            }
        }   

        private void btnAnnuleer_Click(object sender, EventArgs e)
        {
            DatabaseController dc = new DatabaseController();
        }
    }
}
