using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Common;
using System.Data;
using System.Drawing.Printing;
using System.IO;
using System.Drawing;
using PdfiumViewer;

namespace Evidencija_Predmeta
{
    static class SQLHelper
    {
        //Vraca konekcioni string baze unutar Debug foldera
        private static String conString()
        {
            return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='" + System.Environment.CurrentDirectory + "\\bazaPodaci.mdf';Integrated Security=True";
        }

        //metod koji proverava da li je neka vrednost prazan string, i ako jeste vraca null kao vrednost
        public static object CheckNull(string vrednost)
        {
            return string.IsNullOrEmpty(vrednost) ? (object)DBNull.Value : vrednost;
        }
        //Definisemo primitivnu klasu koja cuva unete podatke kao jedan objekat
        public class PredmetPodaci
        {
            public int ID { get; set; }
            public String KlasifikacioniZnak { get; set; }
            public String BrojPredmeta { get; set; }
            public String PodnosilacZahteva { get; set; }
            public String Adresa { get; set; }
            public DateTime Datum { get; set; }
            public String Organ { get; set; }
            public String OrgJedinica { get; set; }
            public String Tekst { get; set; }
            public String BliziOpis { get; set; }
            public String Referent { get; set; }
            public bool Hitno { get; set; }
            public string Korisnik { get; set; }
            public string FajlPutanja { get; set; }
        }
        //Metod koji unosi podatke u tabelu Predmeti
        public static void InsertOsnovniPodaci(PredmetPodaci podaci)
        {
            using (SqlConnection con = new SqlConnection(conString()))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Predmeti (KlasifikacioniZnak, BrojPredmeta, PodnosilacZahteva, Adresa, Datum, Organ, OrgJedinica, OpisPredmeta, Napomena, Referent, Hitno, KorisnikID, FajlPutanja) VALUES (@KlasifikacioniZnak, @BrojPredmeta, @PodnosilacZahteva, @Adresa, @Datum, @Organ, @OrgJedinica, @OpisPredmeta, @Napomena, @Referent, @Hitno, @KorisnikID, @FajlPutanja)", con);
                cmd.Parameters.AddWithValue("@KlasifikacioniZnak", CheckNull(podaci.KlasifikacioniZnak));
                cmd.Parameters.AddWithValue("@BrojPredmeta", CheckNull(podaci.BrojPredmeta));
                cmd.Parameters.AddWithValue("@PodnosilacZahteva", CheckNull(podaci.PodnosilacZahteva));
                cmd.Parameters.AddWithValue("@Adresa", CheckNull(podaci.Adresa));
                cmd.Parameters.AddWithValue("@Datum", podaci.Datum);
                cmd.Parameters.AddWithValue("@Organ", CheckNull(podaci.Organ));
                cmd.Parameters.AddWithValue("@OrgJedinica", CheckNull(podaci.OrgJedinica));
                cmd.Parameters.AddWithValue("@OpisPredmeta", CheckNull(podaci.Tekst));
                cmd.Parameters.AddWithValue("@Napomena", CheckNull(podaci.BliziOpis));
                cmd.Parameters.AddWithValue("@Referent", CheckNull(podaci.Referent));
                cmd.Parameters.AddWithValue("@Hitno", podaci.Hitno ? 1 : 0);
                cmd.Parameters.AddWithValue("@KorisnikID", podaci.Korisnik);
                cmd.Parameters.AddWithValue("@FajlPutanja", podaci.FajlPutanja);
                con.Open();
                int i = cmd.ExecuteNonQuery();

                if (i != 0)
                {
                    MessageBox.Show($"Uspešno uneto!");
                }
                else if (i < 0)
                {
                    MessageBox.Show($"Greška pri unosu!");
                }
            }
        }

        //Metod koji popunjava vrednosti kontrola zavisno od prosledjenog parametra (u ovom slucaju id reda)
        public static PredmetPodaci PopuniZaIzmenu(int id)
        {
            PredmetPodaci p = new PredmetPodaci();
            using (SqlConnection con = new SqlConnection(conString()))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Predmeti WHERE PredmetID = @id", con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        p.BrojPredmeta = reader["BrojPredmeta"].ToString();
                        p.KlasifikacioniZnak = reader["KlasifikacioniZnak"].ToString();
                        p.Hitno = reader["Hitno"] != DBNull.Value && (bool)reader["Hitno"];

                        p.PodnosilacZahteva = reader["PodnosilacZahteva"].ToString();
                        p.Adresa = reader["Adresa"].ToString();
                        //p.Datum = reader["Datum"] != DBNull.Value ? (DateTime)reader["Datum"] : DateTime.MinValue;

                        p.Organ = reader["Organ"].ToString();
                        p.OrgJedinica = reader["OrgJedinica"].ToString();
                        p.Tekst = reader["OpisPredmeta"].ToString();
                        p.BliziOpis = reader["Napomena"].ToString();
                        p.Referent = reader["Referent"].ToString();
                        //p.Korisnik = reader["KorisnikID"].ToString();
                        p.FajlPutanja = reader["FajlPutanja"].ToString();
                    }
                    con.Close();
                }
            }
            return p;
        }

        //Metod koji unosi nove podatke u vec postojeci red unutar tabele
        public static void Izmeni(PredmetPodaci podaci, int id)
        {
            using (SqlConnection con = new SqlConnection(conString()))
            {

                SqlCommand cmd = new SqlCommand(
            "UPDATE Predmeti SET " +
            "PodnosilacZahteva = @PodnosilacZahteva, " +
            "KlasifikacioniZnak = @KlasifikacioniZnak, " +
            "BrojPredmeta = @BrojPredmeta, " +
            "Adresa = @Adresa, " +
            // "Datum = @Datum, " +
            "Organ = @Organ, " +
            "OrgJedinica = @OrgJedinica, " +
            "OpisPredmeta = @OpisPredmeta, " +
            "Napomena = @Napomena, " +
            // "Referent = @Referent, " +
            "Hitno = @Hitno, " +
            "KorisnikID = @KorisnikID, " +
            "FajlPutanja = @FajlPutanja " +
            "WHERE PredmetID = @id", con);

                cmd.Parameters.AddWithValue("@KlasifikacioniZnak", CheckNull(podaci.KlasifikacioniZnak));
                cmd.Parameters.AddWithValue("@BrojPredmeta", CheckNull(podaci.BrojPredmeta));
                cmd.Parameters.AddWithValue("@PodnosilacZahteva", CheckNull(podaci.PodnosilacZahteva));
                cmd.Parameters.AddWithValue("@Adresa", CheckNull(podaci.Adresa));
                //cmd.Parameters.AddWithValue("@Datum", podaci.Datum);
                cmd.Parameters.AddWithValue("@Organ", CheckNull(podaci.Organ));
                cmd.Parameters.AddWithValue("@OrgJedinica", CheckNull(podaci.OrgJedinica));
                cmd.Parameters.AddWithValue("@OpisPredmeta", CheckNull(podaci.Tekst));
                cmd.Parameters.AddWithValue("@Napomena", CheckNull(podaci.BliziOpis));
                // cmd.Parameters.AddWithValue("@Referent", CheckNull(podaci.Referent));
                cmd.Parameters.AddWithValue("@Hitno", podaci.Hitno ? 1 : 0);
                cmd.Parameters.AddWithValue("@KorisnikID", podaci.Korisnik);
                cmd.Parameters.AddWithValue("@FajlPutanja", podaci.FajlPutanja);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();

                if (i != 0)
                {
                    MessageBox.Show($"Uspešna izmena!");
                }
                else
                {
                    MessageBox.Show($"Bez promena!");
                }
            }
        }

        //U zavisnosti od prosledjenog parametra, filtrira prikaz podataka unutar DataGridView-a
        public static void Pretraga(String Vrednost, String parametar, DataGridView dataGridView)
        {
            using (SqlConnection con = new SqlConnection(conString()))
            {
                string query = "";
                DataTable dt = new DataTable();
                switch (parametar)
                {
                    case "Klasifikacioni znak":
                        query = "SELECT * FROM Predmeti WHERE KlasifikacioniZnak LIKE @Vrednost";
                        break;

                    case "Broj predmeta":
                        query = "SELECT * FROM Predmeti WHERE BrojPredmeta LIKE @Vrednost";
                        break;

                    case "Podnosilac zahteva":
                        query = "SELECT * FROM Predmeti WHERE PodnosilacZahteva LIKE @Vrednost";
                        break;

                    case "Adresa":
                        query = "SELECT * FROM Predmeti WHERE Adresa LIKE @Vrednost";
                        break;

                    case "Organ":
                        query = "SELECT * FROM Predmeti WHERE Organ LIKE @Vrednost";
                        break;

                    case "Oganizaciona jedinica":
                        query = "SELECT * FROM Predmeti WHERE OrgJedinica LIKE @Vrednost";
                        break;

                    case "Referent":
                        query = "SELECT * FROM Predmeti WHERE Referent LIKE @Vrednost";
                        break;

                    default:
                        break;
                }
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Vrednost", "%" + Vrednost + "%");

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                dataGridView.DataSource = dt;

            }
        }

        //Metod koji popunjava DataGridView svim podacima iz baze
        public static void Pretraga(DataGridView dataGridView)
        {
            using (SqlConnection con = new SqlConnection(conString()))
            {
                DataTable dt = new DataTable();
                //Za slucaj ako zelimo da prikazemo samo odredjene kolone
                SqlCommand cmd = new SqlCommand("SELECT PredmetID, Hitno, KlasifikacioniZnak, BrojPredmeta, PodnosilacZahteva, Adresa, Datum, Organ, OrgJedinica, Napomena, Referent, FajlPutanja  FROM Predmeti", con);
                //Za slucaj ako zelimo da prikazemo sve kolone
                //SqlCommand cmd = new SqlCommand("SELECT * FROM Predmeti", con);

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
                dataGridView.DataSource = dt;
            }
        }

        //Metod koji brise izabrani red iz tabele
        public static void Brisanje(int id)
        {
            using (SqlConnection con = new SqlConnection(conString()))
            {
                SqlCommand cmd = new SqlCommand("DELETE from Predmeti WHERE PredmetID = @id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int i = cmd.ExecuteNonQuery();

                if (i != 0)
                {
                    MessageBox.Show($"Uspešno obrisano");
                }
            }
        }

        //Ovaj metod pravi listu podataka iz baze zavsino od unetog upita vezanog za odredjenu kolonu
        public static void GetListFromDB(List<string> _lista, string cmdString, string fieldname)
        {

            using (SqlConnection con = new SqlConnection(conString()))
            {
                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            _lista.Add(reader[fieldname].ToString());
                        }
                    }
                }
            }
        }

        //Overload GetListFromDB metode koji prima parametre za upit kao dodatni argument
        public static void GetListFromDB(List<string> _lista, string cmdString, string fieldname, Dictionary<string, object> parametar)
        {

            using (SqlConnection con = new SqlConnection(conString()))
            {
                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {
                    if (parametar != null)
                    {
                        foreach (var param in parametar)
                        {
                            //dodaje paramtere upitu ako postoje
                            cmd.Parameters.AddWithValue(param.Key, param.Value);
                        }
                    }
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            _lista.Add(reader[fieldname].ToString());
                        }
                    }
                }
            }
        }

        //Metodi koji popunjavaju combobox lsitama iz baze
        public static List<String> GetKlasifikacioniZnak()
        {
            List<String> _lista = new List<string>();
            String cmdString = "SELECT * FROM KlasifikacioniZnak";
            GetListFromDB(_lista, cmdString, "KZnakID");
            return _lista;
        }

        public static List<String> GetOrgan()
        {
            List<String> _lista = new List<string>();
            String cmdString = "SELECT * FROM Organ";
            GetListFromDB(_lista, cmdString, "OrganID");
            return _lista;
        }

        public static List<String> GetOrgJed(string s)
        {
            List<String> _lista = new List<string>();
            String cmdString = "SELECT * FROM OrganizacionaJedinica WHERE OrganID = @OrganID";
            //Ovde definisemo recnik koji povezuje Organizacionu jedinicu sa Organom kojem pripada
            Dictionary<string, object> parametar = new Dictionary<string, object>
            {
                { "OrganID",s}
            };
            GetListFromDB(_lista, cmdString, "OrgJedinicaID", parametar);
            return _lista;
        }
        public static List<String> GetBrojPredmeta()
        {
            List<String> _lista = new List<string>();
            String cmdString = "SELECT * FROM Predmeti";
            GetListFromDB(_lista, cmdString, "BrojPredmeta");
            return _lista;
        }
        public static List<String> GetKime()
        {
            List<String> _lista = new List<string>();
            String cmdString = "SELECT * FROM Korisnici";
            GetListFromDB(_lista, cmdString, "KorisnickoIme");
            return _lista;
        }

        //Hvata lozinku koja se podudara sa ispravnim korisnickim imenom
        public static String GetLozinka(string s)
        {
            String query = "SELECT * FROM Korisnici WHERE KorisnickoIme = @KorisnickoIme";
            using (SqlConnection con = new SqlConnection(conString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@KorisnickoIme", s);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }
                    else
                    {
                        reader.Read();
                        return reader["Lozinka"].ToString();
                    }
                }

            }
        }

        //Uzima iz table Korisnici ime i prezime referenta vezanog za uneto korisnicko ime
        public static String GetReferent(string Ki)
        {
            String ime = "";
            String query = "SELECT Ime, Prezime FROM Korisnici WHERE KorisnickoIme = @KorisnickoIme";
            using (SqlConnection con = new SqlConnection(conString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@KorisnickoIme", Ki);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (reader.Read())
                    {
                        ime = reader["Ime"].ToString() + " ";
                        ime += reader["Prezime"].ToString();
                    }
                }
            }
            return ime;
        }

        //Metod koji plugin Pdfium Viewer da stampa fajl na dodeljenoj putanji
        public static void PrintPdf(string fajlPutanja)
        {
            try
            {
                using (var fajl = PdfDocument.Load(fajlPutanja))
                {
                    using (var fajlStampa = fajl.CreatePrintDocument())
                    {
                        using (PrintDialog pD = new PrintDialog())
                        {
                            pD.Document = fajlStampa;

                            // Otvara dijalog za izbor stampaca
                            if (pD.ShowDialog() == DialogResult.OK)
                            {
                                fajlStampa.PrinterSettings = pD.PrinterSettings;
                                fajlStampa.Print(); // Metod koji stampa
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri štampanju: " + ex.Message);
            }
        }

        //Metod koji hvata putanju prilozenog fajla
        public static string GetFajlPutanja(int id)
        {
            string query = "SELECT * FROM Predmeti WHERE PredmetID = @id";
            using (SqlConnection con = new SqlConnection(conString()))
            {
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    if (!reader.HasRows)
                    {
                        return null;
                    }
                    else
                    {
                        reader.Read();
                        return reader["FajlPutanja"].ToString();
                    }
                }
            }
        }
    }
}
