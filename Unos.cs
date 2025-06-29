using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Evidencija_Predmeta
{
    public partial class Unos : Form
    {
        //Deklarisemo stringove koji sluze za manipulisanje putanjom do fajla
        public static String prilogPutanja = "";
        String storageString = "";
        public static int editID = 0;

        //flagovi za postavljanje dugmadi za stil fonta
        public bool tgBold = false;
        public bool tgItalic = false;
        public bool tgUnderline = false;

        public Unos()
        {
            this.MinimumSize = new Size(610, 662);
            InitializeComponent();

            //popunjava padajuce lsite podacima iz baze
            foreach (String s in SQLHelper.GetKlasifikacioniZnak())
            {
                cboxKlasZnak.Items.Add(s);
            }
            foreach (String s in SQLHelper.GetOrgan())
            {
                cbOrgan.Items.Add(s);
            }
            dateTimePicker1.Enabled = false;
            txtReferent.Text = Login.Referent;
            ResetKontrole();
        }

        //Funkcija koja resetuje sve kontrole forme unos na podrazumevano stanje
        public void ResetKontrole()
        {
            //Resetujemo vrednosti kontrola na pocetnu
            cboxKlasZnak.SelectedIndex = -1;
            cbOrgJed.SelectedIndex = -1;
            cbOrgan.SelectedIndex = -1;
            txtBrojPredmeta.Clear();
            txtPodnosilac.Clear();
            txtAdresa.Clear();
            richTxtBliziOpis.Clear();
            richTxtTekst.Clear();
            lblPrilog.Text = "";
            prilogPutanja = "";
            storageString = "";
            hitnoCheckBox.Checked = false;

            //Definisemo pocetni font polja za Opis predmeta i Napomenu
            richTxtTekst.SelectionFont = new Font(richTxtTekst.SelectionFont.FontFamily, 12, richTxtTekst.SelectionFont.Style);
            richTxtTekst.FontChanged += richTxtTekst_SelectionChanged;
            richTxtBliziOpis.SelectionFont = new Font(richTxtTekst.SelectionFont.FontFamily, 12, richTxtTekst.SelectionFont.Style);
            cbFontSize.SelectedIndex = 1;

            //Vracamo izgled dugmica na pocetnu vrednsot
            btnBold.BackColor = SystemColors.Control;
            btnItalic.BackColor = SystemColors.Control;
            btnUnderline.BackColor = SystemColors.Control;

            //Fokusiramo se na prvo polje forme
            cboxKlasZnak.Focus();
        }

        //Metod koji upisuje podatke u tableu Predmeti
        private void btnSacuvaj_Click(object sender, EventArgs e)
        {
            // Podaci iz kontrola Unos forme se cuvaju unutar promenjljive podaci
            SQLHelper.PredmetPodaci podaci = new SQLHelper.PredmetPodaci
            {
                KlasifikacioniZnak = cboxKlasZnak.GetItemText(cboxKlasZnak.SelectedItem).Trim(),
                BrojPredmeta = txtBrojPredmeta.Text.Trim(),
                PodnosilacZahteva = txtPodnosilac.Text.Trim(),
                Adresa = txtAdresa.Text,
                Datum = dateTimePicker1.Value,
                Organ = cbOrgan.GetItemText(cbOrgan.SelectedItem).Trim(),
                OrgJedinica = cbOrgJed.Text.Trim(),
                Tekst = richTxtTekst.Rtf,
                BliziOpis = richTxtBliziOpis.Text.Trim(),
                Referent = txtReferent.Text.Trim(),
                Hitno = hitnoCheckBox.Checked,
                Korisnik = Login.Korisnik.Trim(),
                FajlPutanja = storageString

            };
            List<String> brojevi = SQLHelper.GetBrojPredmeta();
            try
            {
                // proverava da li se unosi novi predmet ili se menja vec postojeci i poziva odgovarajuci metod
                if (brojevi.Contains(txtBrojPredmeta.Text))
                {
                    //Dopisuje u Napomena polje ko je i kada editovao predmet
                    string today = DateTime.Now.ToString();
                    podaci.BliziOpis += "\nKorigovano: " + today + " by " + podaci.Referent;
                    SQLHelper.Izmeni(podaci, editID);
                }
                else
                {
                    SQLHelper.InsertOsnovniPodaci(podaci);
                }
                editID = 0;
                //Ako postoji prilozeni fajl, onda se kopira u folder sa prilozima
                if (prilogPutanja != "")
                {
                    File.Copy(prilogPutanja, storageString);
                }

                //brisemo vrednosti iz kontrola za sledeci unos
                ResetKontrole();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri unosu!\n\n{ex.Message}");
            }
        }

        //otvara formu za pretragu
        private void btnPretraga_Click(object sender, EventArgs e)
        {
            //formi pretraga prosledjujemo instancu klase unos kako bi mogla da koristi njene kontrole
            Pretraga formaPretraga = new Pretraga(this);
            editID = 0;
            tgBold = false;
            tgItalic = false;
            tgUnderline = false;

            formaPretraga.ShowDialog();
        }

        // Menja Enable status cbOrgJed zavisno od izabrane stavke iz padajuce liste cbOrgan
        private void cbOrgan_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //string izabraniOrgan = cbOrgan.Text;
            // if (cbOrgan.GetItemText(cbOrgan.SelectedItem).Trim().Equals(izabraniOrgan))
            {
                cbOrgJed.Items.Clear();
                foreach (String s in SQLHelper.GetOrgJed(cbOrgan.SelectedItem.ToString()))
                {
                    cbOrgJed.Items.Add(s);
                }
                //cbOrgJed.Visible = true;
                //lblOrgJedinica.Visible = true;
                if (cbOrgJed.Items.Count > 0)
                {
                    cbOrgJed.Enabled = true;
                    cbOrgJed.SelectedIndex = 0;
                }
                else
                {
                    cbOrgJed.Enabled = false;
                }
            }
        }
        //Zatvara formu Unos i vraca nas na login 
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Close();
            Login login = new Login();
            login.Show();

            editID = 0;
            tgBold = false;
            tgItalic = false;
            tgUnderline = false;
        }

        //Otvara dijalog za izbor fajla koji zelimo da dodamo kao prilog predmetu
        private void btnNalepi_Click(object sender, EventArgs e)
        {
            OpenFileDialog prilog = new OpenFileDialog();
            string[] ekstenzija;
            string tip = ".";
            using (prilog)
            {
                prilog.Title = "Izaberite PDF fajl";
                prilog.Filter = "PDF Files (*.pdf)|*.pdf";

                if (prilog.ShowDialog() == DialogResult.OK)
                {
                    prilogPutanja = prilog.FileName;
                    ekstenzija = prilogPutanja.Split('.');
                    tip += ekstenzija.Last();
                    if (tip.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                    {
                        lblPrilog.Text = "Priložen fajl: " + prilogPutanja;
                        lblPrilog.Visible = true;
                        storageString = System.Environment.CurrentDirectory + "\\prilozi\\" + Guid.NewGuid().ToString() + ".pdf";
                    }
                    else
                    {
                        MessageBox.Show("Izaberite PDF fajl!", "Neispravan fajl", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        //Dugmici koji menjaju stil fonta unutar polja za Tekst predmeta
        void btnBold_Click(object sender, EventArgs e)
        {
            FontStyle stil = richTxtTekst.SelectionFont.Style ^ FontStyle.Bold;
            richTxtTekst.SelectionFont = new Font(richTxtTekst.SelectionFont.FontFamily, richTxtTekst.SelectionFont.Size, stil);
            tgBold = !richTxtTekst.SelectionFont.Bold;

            btnBold.BackColor = tgBold ? SystemColors.Control : SystemColors.ActiveCaption;
            richTxtTekst.Focus();
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            FontStyle stil = richTxtTekst.SelectionFont.Style ^ FontStyle.Italic;
            richTxtTekst.SelectionFont = new Font(richTxtTekst.SelectionFont.FontFamily, richTxtTekst.SelectionFont.Size, stil);
            tgItalic = !richTxtTekst.SelectionFont.Italic;

            btnItalic.BackColor = tgItalic ? SystemColors.Control : SystemColors.ActiveCaption;
            richTxtTekst.Focus();
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            FontStyle stil = richTxtTekst.SelectionFont.Style ^ FontStyle.Underline;
            richTxtTekst.SelectionFont = new Font(richTxtTekst.SelectionFont.FontFamily, richTxtTekst.SelectionFont.Size, stil);
            tgUnderline = !richTxtTekst.SelectionFont.Underline;

            btnUnderline.BackColor = tgUnderline ? SystemColors.Control : SystemColors.ActiveCaption;
            richTxtTekst.Focus();
        }

        //Menja velicinu fonta zavisno od izabrane vrednosti iz comboboxa
        private void cbFontSize_SelectedIndexChanged(object sender, EventArgs e)//
        {
            richTxtTekst.SelectionFont = new Font(richTxtTekst.SelectionFont.FontFamily, float.Parse(cbFontSize.SelectedItem.ToString()));
        }

        private void Unos_Load(object sender, EventArgs e)
        {

        }

        //Validacija unete vrednosti u polje za broj
        private void txtBrojPredmeta_Validating(object sender, CancelEventArgs e)
        {
            //if (!int.TryParse(txtBrojPredmeta.Text, out _)) // Check if the input can be parsed as an integer
            //{
            //    MessageBox.Show("Unesite ispravan broj!");
            //    e.Cancel = true; // Prevent the focus from leaving the TextBox
            //}

            //txtBrojPredmeta.Validating += txtBrojPredmeta_Validating;
        }

        private void txtBrojPredmeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            //// Check if the pressed key is a control character (e.g., Backspace)
            //if (!char.IsControl(e.KeyChar))
            //{
            //    // Check if the key pressed is not a digit
            //    if (!char.IsDigit(e.KeyChar))
            //    {
            //        e.Handled = true; // Prevent the character from being entered
            //    }
            //}
            ////txtBrojPredmeta.KeyPress += txtBrojPredmeta_KeyPress;

        }

        //Brise podatke trenutno unete u kontrole Unos forme
        private void btnNPredmet_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li želite da otvorite novi predmet? Nesačuvani podaci će biti obrisani.", "Obriši podatke?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ResetKontrole();
            }
        }

        //Otvara dijalog za detaljno biranje fonta
        private void btnFont_Click(object sender, EventArgs e)
        {
            using (FontDialog fontDialog = new FontDialog())
            {
                if (fontDialog.ShowDialog() == DialogResult.OK)
                {
                    richTxtTekst.SelectionFont = fontDialog.Font;
                }
            }
        }

        //Azurira boju dugmica zavisno od izabranog teksta u Tekst polju
        private void richTxtTekst_SelectionChanged(object sender, EventArgs e)
        {
            if (richTxtTekst.SelectionFont != null)
            {
                btnBold.BackColor = richTxtTekst.SelectionFont.Bold ? SystemColors.ActiveCaption : SystemColors.Control;
                btnItalic.BackColor = richTxtTekst.SelectionFont.Italic ? SystemColors.ActiveCaption : SystemColors.Control;
                btnUnderline.BackColor = richTxtTekst.SelectionFont.Underline ? SystemColors.ActiveCaption : SystemColors.Control;
            }
            else
            {
                btnBold.BackColor = SystemColors.Control;
                btnItalic.BackColor = SystemColors.Control;
                btnUnderline.BackColor = SystemColors.Control;
            }
        }
    }
}
