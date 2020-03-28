﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kleuren.Lib.Services;
using Kleuren.Lib.Entities;


namespace KleurenMixer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    enum EditModes { editing, readOnly, canSave }   

    public partial class MainWindow : Window
    {
        Kleur huidigeKleur;

        const int indexRood = 0;
        const int indexGroen = 1;
        const int indexBlauw = 2;

        public MainWindow()
        {
            InitializeComponent();
            KleurenBeheer.MaakVoorbeeldKleuren();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            KoppelStatischeLijsten();
            KoppelDynamischeLijsten();
            lstKleuren.SelectedIndex = 0;
        }

        void KoppelDynamischeLijsten()
        {
            lstKleuren.ItemsSource = KleurenBeheer.Kleuren;
            lstKleuren.Items.Refresh();
        }

        void KoppelStatischeLijsten()
        {
            for (int i = 0; i < 256; i++)
            {
                cmbGroen.Items.Add(i);
            }
        }

        void ToonDetails(Kleur gekozenKleur)
        {
            lblId.Content = gekozenKleur.Id;
            txtNaam.Text = gekozenKleur.Naam;
            txtRood.Text = gekozenKleur.Rgb[0].ToString();
            cmbGroen.SelectedItem = gekozenKleur.Rgb[1];
            sldBlauw_ValueChanged(null, null);
            sldBlauw.Value = gekozenKleur.Rgb[2];
        }

        

        private void lstKleuren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstKleuren.SelectedItem != null)
            {
                huidigeKleur = (Kleur)lstKleuren.SelectedItem;
                ToonDetails(huidigeKleur);
                ToonKleur(huidigeKleur);
            }
        }

        private void ToonKleur(Kleur teTonen)
        {
            lblKleur.Background = teTonen.GeefBrush();
            lblKleur.Content = teTonen.GeefHexCode();
        }

        private void sldBlauw_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int blauwWaarde = (int)sldBlauw.Value;
            int[] rgb = huidigeKleur.Rgb;
            lblBlauw.Content = $"Blauw\n{blauwWaarde}";
            rgb[indexBlauw] = blauwWaarde;
            huidigeKleur.Rgb = rgb;
            ToonKleur(huidigeKleur);
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            //int id = (huidigeKleur == null) ? 0 : huidigeKleur.Id;
            int id;
            string naam;
            int groenWaarde = (int)cmbGroen.SelectedItem;
            int blauwWaarde = (int)sldBlauw.Value;
            try
            {
                int roodWaarde = int.Parse(txtRood.Text);

                int[] rgb;

                if (huidigeKleur == null) id = 0;
                else id = huidigeKleur.Id;
                naam = txtNaam.Text;
                rgb = new int[] { roodWaarde, groenWaarde, blauwWaarde };
                huidigeKleur = new Kleur(naam, rgb, id);

                KleurenBeheer.SlaOp(huidigeKleur);

                lstKleuren.SelectedItem = null;
                KoppelDynamischeLijsten();
                ToonMelding("De kleur is opgeslagen", true);
            }
            catch (Exception ex)
            {
                ToonMelding("Geef geldige waarden in");
            }

        }
    }
}
