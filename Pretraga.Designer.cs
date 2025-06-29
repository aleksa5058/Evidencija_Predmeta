
namespace Evidencija_Predmeta
{
    partial class Pretraga
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pretraga));
            this.txtPretraga = new System.Windows.Forms.TextBox();
            this.dataGWPretraga = new System.Windows.Forms.DataGridView();
            this.btnTrazi = new System.Windows.Forms.Button();
            this.cbParametar = new System.Windows.Forms.ComboBox();
            this.osnovniPodaciBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.bazaPodaciDataSet = new Evidencija_Predmeta.bazaPodaciDataSet();
            this.osnovniPodaciTableAdapter = new Evidencija_Predmeta.bazaPodaciDataSetTableAdapters.osnovniPodaciTableAdapter();
            this.label1 = new System.Windows.Forms.Label();
            this.btnObrisi = new System.Windows.Forms.Button();
            this.btnStampaj = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGWPretraga)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.osnovniPodaciBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bazaPodaciDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPretraga
            // 
            this.txtPretraga.Location = new System.Drawing.Point(12, 6);
            this.txtPretraga.Name = "txtPretraga";
            this.txtPretraga.Size = new System.Drawing.Size(411, 20);
            this.txtPretraga.TabIndex = 1;
            // 
            // dataGWPretraga
            // 
            this.dataGWPretraga.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGWPretraga.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGWPretraga.Location = new System.Drawing.Point(12, 70);
            this.dataGWPretraga.Name = "dataGWPretraga";
            this.dataGWPretraga.Size = new System.Drawing.Size(986, 335);
            this.dataGWPretraga.TabIndex = 29;
            // 
            // btnTrazi
            // 
            this.btnTrazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrazi.Location = new System.Drawing.Point(12, 32);
            this.btnTrazi.Name = "btnTrazi";
            this.btnTrazi.Size = new System.Drawing.Size(411, 32);
            this.btnTrazi.TabIndex = 30;
            this.btnTrazi.Text = "🔍 Pretraži";
            this.btnTrazi.UseVisualStyleBackColor = true;
            this.btnTrazi.Click += new System.EventHandler(this.btnTrazi_Click_1);
            // 
            // cbParametar
            // 
            this.cbParametar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParametar.FormattingEnabled = true;
            this.cbParametar.Items.AddRange(new object[] {
            "Klasifikacioni znak",
            "Broj predmeta",
            "Podnosilac zahteva",
            "Adresa",
            "Organ",
            "Organizaciona jedinica",
            "Referent"});
            this.cbParametar.Location = new System.Drawing.Point(604, 5);
            this.cbParametar.Name = "cbParametar";
            this.cbParametar.Size = new System.Drawing.Size(121, 21);
            this.cbParametar.TabIndex = 31;
            // 
            // osnovniPodaciBindingSource
            // 
            this.osnovniPodaciBindingSource.DataMember = "osnovniPodaci";
            this.osnovniPodaciBindingSource.DataSource = this.bazaPodaciDataSet;
            // 
            // bazaPodaciDataSet
            // 
            this.bazaPodaciDataSet.DataSetName = "bazaPodaciDataSet";
            this.bazaPodaciDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // osnovniPodaciTableAdapter
            // 
            this.osnovniPodaciTableAdapter.ClearBeforeFill = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(428, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 20);
            this.label1.TabIndex = 32;
            this.label1.Text = "Parametar pretrage:";
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnObrisi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObrisi.Location = new System.Drawing.Point(877, 32);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(121, 32);
            this.btnObrisi.TabIndex = 33;
            this.btnObrisi.Text = "🗑️ Obriši";
            this.btnObrisi.UseMnemonic = false;
            this.btnObrisi.UseVisualStyleBackColor = true;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // btnStampaj
            // 
            this.btnStampaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStampaj.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStampaj.Location = new System.Drawing.Point(648, 32);
            this.btnStampaj.Name = "btnStampaj";
            this.btnStampaj.Size = new System.Drawing.Size(223, 32);
            this.btnStampaj.TabIndex = 34;
            this.btnStampaj.Text = "🖨️ Štampaj priloženi fajl";
            this.btnStampaj.UseMnemonic = false;
            this.btnStampaj.UseVisualStyleBackColor = true;
            this.btnStampaj.Click += new System.EventHandler(this.btnStampaj_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(464, 32);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(178, 32);
            this.btnEdit.TabIndex = 35;
            this.btnEdit.Text = "Izmeni detalje predmeta";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // Pretraga
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1015, 417);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnStampaj);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbParametar);
            this.Controls.Add(this.btnTrazi);
            this.Controls.Add(this.dataGWPretraga);
            this.Controls.Add(this.txtPretraga);
            this.Name = "Pretraga";
            this.Text = "Pretraga";
            this.Load += new System.EventHandler(this.Pretraga_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGWPretraga)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.osnovniPodaciBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bazaPodaciDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtPretraga;
        private System.Windows.Forms.DataGridView dataGWPretraga;
        private System.Windows.Forms.Button btnTrazi;
        private bazaPodaciDataSet bazaPodaciDataSet;
        private System.Windows.Forms.BindingSource osnovniPodaciBindingSource;
        private bazaPodaciDataSetTableAdapters.osnovniPodaciTableAdapter osnovniPodaciTableAdapter;
        private System.Windows.Forms.ComboBox cbParametar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnObrisi;
        private System.Windows.Forms.Button btnStampaj;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnEdit;
    }
}