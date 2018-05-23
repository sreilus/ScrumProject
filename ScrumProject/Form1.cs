using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading;

namespace ScrumProject
{
    public partial class FormScrum : Form
    {
        public FormScrum()
        {
            InitializeComponent();
        }


        //Görev GroupBox içerikleri tüm objeler burada yaratılıyor. 
        List<Personel> Personeller = new List<Personel>();
        GroupBox[,] groupOncelikliGorev = new GroupBox[15, 1];
        GroupBox[,] groupOnceliksizGorev = new GroupBox[15, 1];
        Label[,] labelGorevTipi = new Label[15, 1];
        Label[,] labelGorevliAdi = new Label[15, 1];
        ComboBox[,] comboOncelikliGorev = new ComboBox[15, 1];
        ComboBox[,] comboOncelikliGorevli = new ComboBox[15, 1];
        ComboBox[,] comboOnceliksizGorev = new ComboBox[15, 1];
        ComboBox[,] comboOnceliksizGorevli = new ComboBox[15, 1];
        RichTextBox[,] textGorevAciklama = new RichTextBox[15, 1];
        RichTextBox[,] textGorevAciklama2 = new RichTextBox[15, 1];
        DateTimePicker[,] dateSonTeslim = new DateTimePicker[15, 1];
        DateTimePicker[,] dateSonTeslim2 = new DateTimePicker[15, 1];
        JButton[,] silButonlari = new JButton[15, 1];
        JButton[,] silButonlari2 = new JButton[15, 1];
        JButton[,] kilitButonlari = new JButton[15, 1];
        ToolTip butonAciklama = new ToolTip();

        private void FormScrum_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            OncelikliGorevYukle();
            OnceliksizGorevYukle();
        }

        public void OncelikliGorevYukle()
        {
            string aciklama = "Öncelikli görevin açıklamasını giriniz..";
            string gorevTipi = "Öncelikli";
            GorevYukle(groupOncelikliGorev, comboOncelikliGorev, comboOncelikliGorevli, textGorevAciklama, dateSonTeslim, silButonlari, SilOncelikli, KilitleOncelikli, KonumOncelikli, 50, aciklama, gorevTipi, Color.Red);
        }

        public void OnceliksizGorevYukle()
        {
            string aciklama = "Önceliksiz görevin açıklamasını giriniz..";
            string gorevTipi = "Önceliksiz";
            GorevYukle(groupOnceliksizGorev, comboOnceliksizGorev, comboOnceliksizGorevli, textGorevAciklama2, dateSonTeslim2, silButonlari2, SilOnceliksiz, KilitleOnceliksiz, KonumOnceliksiz, 250, aciklama, gorevTipi, Color.Blue);
        }

        public void GorevYukle(GroupBox[,] groupGorev, ComboBox[,] comboGorev, ComboBox[,] comboGorevli, RichTextBox[,] txtGorevAciklama, DateTimePicker[,] dateSonTeslim, JButton[,] silButonlari, EventHandler Sil, EventHandler Kilitle, MouseEventHandler TasiKonum, int Top, string aciklama, string gorevTipi, Color renk)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int k = 0; k < 1; k++)
                {
                    groupGorev[i, k] = new GroupBox();
                    dateSonTeslim[i, k] = new DateTimePicker();
                    txtGorevAciklama[i, k] = new RichTextBox();
                    comboGorevli[i, k] = new ComboBox();
                    comboGorev[i, k] = new ComboBox();
                    labelGorevTipi[i, k] = new Label();
                    labelGorevliAdi[i, k] = new Label();
                    silButonlari[i, k] = new JButton();
                    kilitButonlari[i, k] = new JButton();
                    labelGorevTipi[i, k].Text = "Görev Tipi:";
                    labelGorevliAdi[i, k].Text = "Görevli Kişi";
                    groupGorev[i, k].Left = 8;
                    groupGorev[i, k].Top = Top;
                    groupGorev[i, k].Width = 220;
                    groupGorev[i, k].Height = 180;
                    comboGorevli[i, k].Width = 100;
                    dateSonTeslim[i, k].Width = 199;
                    txtGorevAciklama[i, k].Width = 200;
                    txtGorevAciklama[i, k].Height = 50;
                    txtGorevAciklama[i, k].Text = aciklama + Environment.NewLine;
                    groupGorev[i, k].Text = gorevTipi;
                    groupGorev[i, k].ForeColor = renk;
                    groupGorev[i, k].Font = new Font(this.Font.FontFamily, 9);
                    silButonlari[i, k].i = i;
                    silButonlari[i, k].k = k;
                    silButonlari[i, k].Size = new Size(20, 20);
                    silButonlari[i, k].Text = "x";
                    silButonlari[i, k].Click += Sil;
                    kilitButonlari[i, k].i = i;
                    kilitButonlari[i, k].k = k;
                    kilitButonlari[i, k].Size = new Size(20, 20);
                    kilitButonlari[i, k].Text = "ª";
                    kilitButonlari[i, k].Click += Kilitle;
                    butonAciklama.SetToolTip(silButonlari[i, k], "Bu story'yi silmek için tıklayınız.");
                    butonAciklama.SetToolTip(kilitButonlari[i, k], "Bu story'yi kilitlemek için tıklayınız.");
                    groupGorev[i, k].MouseUp += TasiKonum;

                    Point comboGorevYer = new Point(110, 30);
                    Point dateYer = new Point(10, 150);
                    Point labelGorevTipiYer = new Point(10, 33);
                    Point lblGorevliYer = new Point(10, 60);
                    Point comboGorevliYer = new Point(110, 60);
                    Point richYer = new Point(10, 90);
                    Point silButonP = new Point(200, 5);
                    Point kilitButonP = new Point(180, 5);

                    dateSonTeslim[i, k].Location = dateYer;
                    txtGorevAciklama[i, k].Location = richYer;
                    comboGorevli[i, k].Location = comboGorevliYer;
                    labelGorevTipi[i, k].Location = labelGorevTipiYer;
                    labelGorevliAdi[i, k].Location = lblGorevliYer;
                    comboGorev[i, k].Location = comboGorevYer;
                    comboGorev[i, k].Width = 100;
                    silButonlari[i, k].Location = silButonP;
                    kilitButonlari[i, k].Location = kilitButonP;

                    //GroupBox içerisine görevleri include ediyoruz.
                    groupGorev[i, k].Controls.Add(labelGorevTipi[i, k]);
                    groupGorev[i, k].Controls.Add(txtGorevAciklama[i, k]);
                    groupGorev[i, k].Controls.Add(comboGorevli[i, k]);
                    groupGorev[i, k].Controls.Add(labelGorevliAdi[i, k]);
                    groupGorev[i, k].Controls.Add(comboGorev[i, k]);
                    groupGorev[i, k].Controls.Add(dateSonTeslim[i, k]);
                    groupGorev[i, k].Controls.Add(silButonlari[i, k]);
                    groupGorev[i, k].Controls.Add(kilitButonlari[i, k]);

                    //En son GroupBox'u Forma ekleyip kaydırılabilir yapıyoruz.
                    this.Controls.Add(groupGorev[i, k]);
                    ControlExtension.Draggable(groupGorev[i, k], true);
                }
            }
        }

        private void buttonGorevliEkle_Click(object sender, EventArgs e)
        {
            GorevliEkle(textGorevli.Text);
            MessageBox.Show("Eklemek istediğiniz görevli " + textGorevli.Text + " başarıyla eklendi.");
            textGorevli.Clear();

        }

        private void buttonGorevTipiEkle_Click(object sender, EventArgs e)
        {
            GorevTipiEkle(textGorevTipi.Text);
            MessageBox.Show("Eklemek istediğiniz görevli " + textGorevTipi.Text + " başarıyla eklendi.");
            textGorevTipi.Clear();
        }

        private void GorevTipiEkle(string gorevTipi)
        {
            for (int i = 0; i < 15; i++)
            {
                for (int k = 0; k < 1; k++)
                {
                    comboOncelikliGorev[i, k].Items.Add(gorevTipi);
                    comboOnceliksizGorev[i, k].Items.Add(gorevTipi);
                }
            }
        }

        private void GorevliEkle(string gorevliIsmi)
        {
            Personel p = new Personel();
            p.Ad = gorevliIsmi;
            Personeller.Add(p);


            for (int i = 0; i < 15; i++)
            {
                for (int k = 0; k < 1; k++)
                {
                    comboOncelikliGorevli[i, k].Items.Add(p.Ad);
                    comboOnceliksizGorevli[i, k].Items.Add(p.Ad);
                }
            }
        }

        private Bitmap Screenshot() // Bitmap türünde olşuturuyoruz  fonksiyonumuzu. 
        {
            Bitmap Screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics GFX = Graphics.FromImage(Screenshot);
            GFX.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size);
            return Screenshot;
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            EkranGoruntusuAl();
        }

        private void EkranGoruntusuAl()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "jpeg|*.jpeg";
            save.FileName = "Ekran Görüntüsü";
            if (save.ShowDialog() == DialogResult.OK)
            {
                Thread.Sleep(100);
                Screenshot().Save(@save.FileName, ImageFormat.Jpeg); // görüntüyü kayıt edeceğimiz yeri seçiyoruz.
                MessageBox.Show("Ekran Görüntüsü Başarıyla Kaydedidi");
            }
        }

        private int clickCountHareket = 0;

        private int clickCountErisim = 0;
        private void GorselErisimKilitle(int i, int k, RichTextBox[,] textGorevAciklama, DateTimePicker[,] dateSonTeslim, JButton[,] silButonlar, ComboBox[,] comboGorev, ComboBox[,] comboGorevli)
        {
            clickCountErisim++;
            bool erisilebilirMi;

            if (clickCountErisim % 2 == 1)
                erisilebilirMi = true;
            else
                erisilebilirMi = false;

            if (erisilebilirMi)
            {
                comboGorev[i, k].Enabled = false;
                comboGorevli[i, k].Enabled = false;
                textGorevAciklama[i, k].Enabled = false;
                dateSonTeslim[i, k].Enabled = false;
                silButonlar[i, k].Enabled = false;
            }
            else
            {
                comboGorev[i, k].Enabled = true;
                comboGorevli[i, k].Enabled = true;
                textGorevAciklama[i, k].Enabled = true;
                dateSonTeslim[i, k].Enabled = true;
                silButonlar[i, k].Enabled = true;
            }

            if (erisilebilirMi)
                MessageBox.Show("Sistem görev gruplarının erişilebilirliği PASİF.");

            else
                MessageBox.Show("Sistem görev gruplarının erisilebilirliği AKTİF.");
        }

        public void SilOncelikli(object sender, EventArgs e)
        {
            //  MessageBox.Show(((JButton)sender).i.ToString());
            groupOncelikliGorev[((JButton)sender).i, ((JButton)sender).k].Visible = false;
        }

        public void SilOnceliksiz(object sender, EventArgs e)
        {
            //  MessageBox.Show(((JButton)sender).i.ToString());
            groupOnceliksizGorev[((JButton)sender).i, ((JButton)sender).k].Visible = false;
        }

        public void KilitleOncelikli(object sender, EventArgs e)
        {
            GorselErisimKilitle(((JButton)sender).i, ((JButton)sender).k, textGorevAciklama, dateSonTeslim, silButonlari, comboOncelikliGorev, comboOncelikliGorevli);
        }

        public void KilitleOnceliksiz(object sender, EventArgs e)
        {
            GorselErisimKilitle(((JButton)sender).i, ((JButton)sender).k, textGorevAciklama2, dateSonTeslim2, silButonlari2, comboOnceliksizGorev, comboOnceliksizGorevli);
        }

        public void KonumOncelikli(object sender, EventArgs e)
        {
            if (((GroupBox)sender).Location.X < 600)
            {
                ((GroupBox)sender).Location = new Point(330, 50);
            }
            else if (((GroupBox)sender).Location.X > 600 && ((GroupBox)sender).Location.X < 850)
            {
                ((GroupBox)sender).Location = new Point(700, 50);
            }
            if (((GroupBox)sender).Location.X > 850)
            {
                ((GroupBox)sender).Location = new Point(1028, 50);
            }
        }

        public void KonumOnceliksiz(object sender, EventArgs e)
        {
            if (((GroupBox)sender).Location.X < 600)
            {
                ((GroupBox)sender).Location = new Point(330, 250);
            }
            else if (((GroupBox)sender).Location.X > 600 && ((GroupBox)sender).Location.X < 850)
            {
                ((GroupBox)sender).Location = new Point(700, 250);
            }
            if (((GroupBox)sender).Location.X > 850)
            {
                ((GroupBox)sender).Location = new Point(1028, 250);
            }
        }
    }
}