using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evidencija_Predmeta
{
    public partial class Login : Form
    {
        //Promenljive u koje cuvamo vrednosti za Referenta i Korisnika koje sluze ostalim klasama
        public static String Referent = "";
        public static String Korisnik = "";
        public Login()
        {
            this.MinimumSize = new Size(349, 489);
            InitializeComponent();
            //////delet dis
            textBoxKIme.Text = "aleksa";
            textBoxLozinka.Text = "1234";
            ///////deletdelet
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String lozinka = SQLHelper.GetLozinka(textBoxKIme.Text);

            if (lozinka == null)
            {
                MessageBox.Show("Nepoznato korisničko ime.");
                return;
            }

            if (textBoxLozinka.Text != lozinka)
            {
                MessageBox.Show("Pogrešna lozinka!");
                return;

            }
            //Unosimo vrednsoti u stringove
            Referent = SQLHelper.GetReferent(textBoxKIme.Text);
            Korisnik = textBoxKIme.Text;

            Unos forma = new Unos();
            this.Hide();
            forma.ShowDialog();
        }
    }
}
