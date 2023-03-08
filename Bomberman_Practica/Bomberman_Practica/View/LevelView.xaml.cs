using Bomberman_Practica.Model;
using ConnexioBD;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Policy;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace Bomberman_Practica.View
{
    
   
    public sealed partial class LevelView : UserControl
    {
        public LevelView()
        {
            this.InitializeComponent();
            carregarGrid();
        }

        
        
        private void carregarGrid()
        {
            for(int i =0; i<20; i++)
            {
                grdNivell.ColumnDefinitions.Add(new ColumnDefinition());
                grdNivell.RowDefinitions.Add(new RowDefinition());

            }

            for(int j =0; j < grdNivell.ColumnDefinitions.Count; j++)
            {
                for(int x = 0; x < grdNivell.RowDefinitions.Count; x++)
                {
                    UI_GRID casella = new UI_GRID(this);

                    
                    grdNivell.Children.Add(casella);
                    Grid.SetColumn(casella,x);
                    Grid.SetRow(casella, j);
                }
            }
            
            
        }

        public Level Nivell
        {
            get { return (Level)GetValue(NivellProperty); }
            set { SetValue(NivellProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Nivell.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NivellProperty =
            DependencyProperty.Register("Nivell", typeof(Level), typeof(LevelView), new PropertyMetadata(null));


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Carreguem les imatges dels elements disponibles 
            lsvBlocs.ItemsSource = Casella.llistacasellas();
            lsvBlocs.SelectedItem = lsvBlocs.Items.First();
            carregarComboTemps();
        }




        


        private async void btnImatge_Click_1(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fp = new FileOpenPicker();
            fp.FileTypeFilter.Add(".jpg");
            fp.FileTypeFilter.Add(".png");

            StorageFile sf = await fp.PickSingleFileAsync();
            // Cerca la carpeta de dades de l'aplicació, dins de ApplicationData
            var folder = ApplicationData.Current.LocalFolder;
            // Dins de la carpeta de dades, creem una nova carpeta "icons"
            var iconsFolder = await folder.CreateFolderAsync("icons", CreationCollisionOption.OpenIfExists);
            // Creem un nom usant la data i hora, de forma que no poguem repetir noms.
            string name = (DateTime.Now).ToString("yyyyMMddhhmmss") + "_" + sf.Name;
            // Copiar l'arxiu triat a la carpeta indicada, usant el nom que hem muntat
            StorageFile copiedFile = await sf.CopyAsync(iconsFolder, name);
            // Crear una imatge en memòria (BitmapImage) a partir de l'arxiu copiat a ApplicationData
            BitmapImage tmpBitmap = new BitmapImage(new Uri(copiedFile.Path));

          
            
        }

        public Casella pregunta_Item()
        {
            Casella entrada =(Casella)lsvBlocs.SelectedItem;

            
            return entrada;
        }


        private void lsvBlocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pregunta_Item();
        }

        private void btnCrearNIvell_Click(object sender, RoutedEventArgs e)
        {

            String nom = txbNom.Text;
            String des = txbDesc.Text;
            int hores = Int32.Parse(cbmHores.SelectedItem.ToString());
            int minuts = Int32.Parse(cbmMinuts.SelectedItem.ToString());
            int segons = Int32.Parse(cbmSegons.SelectedItem.ToString());
            String imatge = "";
            bool estat = false;

            if (chEstat.IsChecked == true)
            {
                estat = true;
            }
            else
            {
                estat = false;
            }



            Level nou = new Level(nom, des, hores, minuts, segons, estat, imatge);

            Level.InserirNivell(nou);

        }


        private void carregarComboTemps()
        {
            for (int i = 0; i < 60; i++)
            {
                cbmHores.Items.Add(i);
                cbmMinuts.Items.Add(i);
                cbmSegons.Items.Add(i);
            }

            cbmHores.SelectedItem = 0; cbmMinuts.SelectedItem = 0; cbmSegons.SelectedItem = 0;
        }

        private void btnCrearImatge_Click(object sender, RoutedEventArgs e)
        {
            
        }


        public Level ElmeuLevel { get; set; }



        public void canviarText()
        {
            txbNom.Text = ElmeuLevel.Nom;
            txbDesc.Text = ElmeuLevel.Descripcio;
            txbImatge.Text = ElmeuLevel.Url;
            cbmHores.SelectedItem = ElmeuLevel.Hores;
            cbmMinuts.SelectedItem = ElmeuLevel.Minuts;
            cbmSegons.SelectedItem = ElmeuLevel.Segons;

        }












    }
}
