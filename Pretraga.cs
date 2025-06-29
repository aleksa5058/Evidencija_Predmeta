using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PdfiumViewer;

namespace Evidencija_Predmeta
{
    public partial class Pretraga : Form
    {
        //Definisemo instancu Unos forme kako bi mogli u Pretraga formi da koristimo njene kontrole
        private Unos unos = new Unos();
        public Pretraga(Unos u)
        {
            unos = u;

            this.MinimumSize = new Size(1000,456);
            InitializeComponent();
            cbParametar.SelectedIndex = 0;
            //Popunjava DataGridView i postavlja ga na read only
            SQLHelper.Pretraga(dataGWPretraga);
            dataGWPretraga.ReadOnly = true;
        }

        //Funkcija koja vraca id izabranog reda iz DGV
        private int GetDGVSelectedID(DataGridView dgv)
        {
            int id = 0;
            if (dgv.SelectedRows.Count == 0) // Ako nije izabran ceo red
            {
                if (dgv.SelectedCells.Count == 1) // Ako je izabrana samo jedna celija 
                {
                    id = Convert.ToInt32(dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[0].Value);
                }
                else
                {
                    throw new Exception("Izabrano više od jedne ćelije!"); // Ako je izabrano vise od jedne celije
                }
            }
            else if (dgv.SelectedRows.Count == 1) // Ako je izabran jedan red
            {
                id = Convert.ToInt32(dgv.SelectedRows[0].Cells[0].Value);
            }
            else
            {
                throw new Exception("Izabrano više od jednog reda!"); // Ako je izabrano vise redova
            }
            return id;
        }

        //Filtrira predmete prema zadatom paremtru
        private void btnTrazi_Click_1(object sender, EventArgs e)
        {
            try
            {
                SQLHelper.Pretraga(txtPretraga.Text, cbParametar.GetItemText(cbParametar.SelectedItem), dataGWPretraga);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void Pretraga_Load(object sender, EventArgs e)
        {
            
        }
        //Funkcija koja brise prilog iz foldera prilozi nakon sto se obrise asocirani predmet
        private void ObrisiPrilog(string filePath)
        {
            try
            {
                if (File.Exists(filePath))//proverimo da li fajl postoji
                {
                    File.Delete(filePath);//ako postoji obrisemo ga
                    //MessageBox.Show("Uspesno obrisano!.", "Obrisnao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Funkcija koja brise predmet izabran u DGV
        private void btnObrisi_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Da li želite da obrišete predmet?", "Brisanje predmeta", MessageBoxButtons.YesNo) == DialogResult.Yes)//dialog box za potvrdu brisanja
                {
                    string s = SQLHelper.GetFajlPutanja(GetDGVSelectedID(dataGWPretraga));//uzmemo putanju do prilozenog fajla
                    SQLHelper.Brisanje(GetDGVSelectedID(dataGWPretraga));//obrisemo red iz tabele
                    ObrisiPrilog(s);//obrisemo sam fajl

                    SQLHelper.Pretraga(dataGWPretraga);//osvezimo prikaz
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greska: " + ex.Message);
            }
        }
        //Poziva event handler za stampanje
        private void btnStampaj_Click(object sender, EventArgs e)
        {
            
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.PrintPreviewControl.Zoom = 1;
          //  printDocument1.DefaultPageSettings.PaperSize = new PaperSize("A4", 1000, 440);
            printPreviewDialog1.ShowDialog();
        }

        //Stampanje fajla
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            /*
            Bitmap img = new Bitmap(dataGWPretraga.Width, dataGWPretraga.Height);
            Graphics g = e.Graphics;

            ////////////// Stampanje red po red
            //string rowText = "";
            //int rowIndex = dataGWPretraga.CurrentRow.Index;
            //for (int colIndex = 0; colIndex < dataGWPretraga.Columns.Count; colIndex++)
            //{
            //    rowText += dataGWPretraga.Rows[rowIndex].Cells[colIndex].Value.ToString() + "|";
            //}
            //rowText = rowText.TrimEnd('|');

            Font font = new Font("Arial", 12, FontStyle.Regular);//           kol  red
            e.Graphics.DrawString("Test 123", font, Brushes.Black, new PointF(100, 100));
            //e.Graphics.DrawString(rowText, font, Brushes.Black, new PointF(100, 120));

            
            ////////// Stampanje slike
             dataGWPretraga.DrawToBitmap(img, new Rectangle(0, 0, dataGWPretraga.Width, dataGWPretraga.Height));//nacrta sadrzaj direktno iz DataGridView-a
            e.Graphics.DrawImage(img, 0, 0);

            e.HasMorePages = false;
            */

            int id = GetDGVSelectedID(dataGWPretraga);
            try
            {
                if (MessageBox.Show("Da li želite da oštampate predmet?", "Štampa predmeta", MessageBoxButtons.YesNo) == DialogResult.Yes)//dialog box za potvrdu brisanja
                {
                    string put = SQLHelper.GetFajlPutanja(id);
                    SQLHelper.PrintPdf(put);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        //Funkicja koja nam omogucava da izmenimo podatke iz vec unetog predmeta
        private void btnEdit_Click(object sender, EventArgs e)
        {
            int id = GetDGVSelectedID(dataGWPretraga);
            try
            {
                if (MessageBox.Show("Da li želite da izmenite predmet? Nesačuvani podaci će biti obrisani.", "Izmeni podatke?", MessageBoxButtons.YesNo) == DialogResult.Yes)//dialog box za potvrdu brisanja
                {
                    unos.ResetKontrole();
                    unos.btnBold.BackColor = SystemColors.Control;
                    unos.btnItalic.BackColor = SystemColors.Control;
                    unos.btnUnderline.BackColor = SystemColors.Control;
                    SQLHelper.PredmetPodaci predmetIzmena = SQLHelper.PopuniZaIzmenu(id);
                    Unos.editID = id;
                    unos.txtBrojPredmeta.Text = predmetIzmena.BrojPredmeta;
                    unos.txtPodnosilac.Text =predmetIzmena.PodnosilacZahteva;
                    unos.txtAdresa.Text = predmetIzmena.Adresa;
                    unos.richTxtBliziOpis.Text = predmetIzmena.BliziOpis;
                    unos.richTxtTekst.Rtf = predmetIzmena.Tekst;
                    //unos.txtReferent.Text = predmetIzmena.Referent;
                    
                    unos.hitnoCheckBox.Checked = predmetIzmena.Hitno;
                    if (unos.cboxKlasZnak.Items.Contains(predmetIzmena.KlasifikacioniZnak))
                    {
                        unos.cboxKlasZnak.SelectedItem = predmetIzmena.KlasifikacioniZnak;
                    }
                    else
                    {
                        unos.cboxKlasZnak.SelectedIndex = -1; 
                    }
                    if (unos.cbOrgan.Items.Contains(predmetIzmena.Organ))
                    {
                        unos.cbOrgan.SelectedItem = predmetIzmena.Organ;
                    }
                    else
                    {
                        unos.cbOrgan.SelectedIndex = -1;
                    }
                    if (unos.cbOrgJed.Items.Contains(predmetIzmena.OrgJedinica))
                    {
                        unos.cbOrgJed.SelectedItem = predmetIzmena.OrgJedinica;
                    }
                    else
                    {
                        unos.cbOrgJed.SelectedIndex = -1;
                    }
                    if (predmetIzmena.FajlPutanja !="")
                    {
                        unos.lblPrilog.Text = predmetIzmena.FajlPutanja;
                        unos.lblPrilog.Visible = true;
                    }
                    this.Close();
                    SQLHelper.Pretraga(dataGWPretraga);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }
    }
}

