using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Bomberman_Practica.Model;
using Windows.Storage.Pickers;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using System.ServiceModel.Channels;
using Windows.UI.Popups;
using static System.Net.Mime.MediaTypeNames;
using ConnexioBD;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Graphics.Canvas;
using System.Drawing;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace Bomberman_Practica.View
{
   

    public sealed partial class IntroView : UserControl
    {

       
        BitmapImage tmpBitmap = null;


        public IntroView()
        {
            this.InitializeComponent();

        }


        public Intro Introduccio
        {
            get { return (Intro)GetValue(IntroduccioProperty); }
            set { SetValue(IntroduccioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IntroduccioProperty =
            DependencyProperty.Register("Introduccio", typeof(Intro), typeof(IntroView), new PropertyMetadata(null));




        public BitmapSource UI_Fons
        {
            get { return (BitmapSource)GetValue(UI_FonsProperty); }
            set { SetValue(UI_FonsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UI_Fons.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UI_FonsProperty =
            DependencyProperty.Register("UI_Fons", typeof(BitmapSource), typeof(LevelView), new PropertyMetadata(new BitmapImage(new Uri("ms-appx:///Assets/bomb.png"))));


        private async void btnImatge_Click(object sender, RoutedEventArgs e)
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
            if (sf == null)
            {

                var messageDialog = new MessageDialog("Has de seleccionar una imatge del buscador o fer servir la ja existent");
                messageDialog.ShowAsync();

                tmpBitmap = new BitmapImage(new Uri("ms-appx:///Assets/bomb.png"));

                imgfons.Source = tmpBitmap;
                txbImatge.Text = "";


            }
            else
            {
                string name = (DateTime.Now).ToString("yyyyMMddhhmmss") + "_" +  sf.Name;
                name = name.Replace(" ", "");
                // Copiar l'arxiu triat a la carpeta indicada, usant el nom que hem muntat
                StorageFile copiedFile = await sf.CopyAsync(iconsFolder, name);
                // Crear una imatge en memòria (BitmapImage) a partir de l'arxiu copiat a ApplicationData
                tmpBitmap = new BitmapImage(new Uri(copiedFile.Path));

                imgfons.Source = tmpBitmap;
                txbImatge.Text = copiedFile.Path;
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog1 entrada = new ContentDialog1(this);

            int hores = (int)cbmHores.SelectedItem;
            int minuts = (int)cbmMinuts.SelectedItem;
            int segons = (int)cbmSegons.SelectedItem;

            

            if (hores == 0 && minuts == 0 && segons == 0)
            {
                var messageDialog = new MessageDialog("El temps ha de tenir un valor mínim de 1 segon");
                messageDialog.ShowAsync();
            }
            else
            {

                IAsyncOperation<ContentDialogResult> asyncOperation = entrada.ShowAsync();
            }

        }


        private void carregarComboTemps()
        {
            for (int i = 0; i < 60; i++)
            {
                cbmHores.Items.Add(i);
                cbmMinuts.Items.Add(i);
                cbmSegons.Items.Add(i);
            }

            cbmHores.SelectedItem = 0; cbmMinuts.SelectedItem=0; cbmSegons.SelectedItem=0;
        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            carregarComboTemps();
            
        }

        //Metodes per recuperar els elements de informació en el content view


        public String recuperarTitol()
        {
            return txbNom.Text;
        }

        public String recuperarDescripcio()
        {
            return txbDesc.Text;
        }

        public ImageSource recuperarImatge()
        {
            return imgfons.Source;
        }

        public List<Int32> recuperarTemps()
        {
            List<Int32> temps= new List<Int32>();

           

                temps.Add(Int32.Parse(cbmHores.SelectedItem.ToString()));
                temps.Add(Int32.Parse(cbmMinuts.SelectedItem.ToString()));
                temps.Add(Int32.Parse(cbmSegons.SelectedItem.ToString()));
            return temps;
        }





        private void btnCrear_Click(object sender, RoutedEventArgs e)
        {

                Intro nou= inserirIntro();

            if(nou != null)
            {
                Intro.Inserir(nou);
                netejarInfo();
                ConnexioEditro.obtenir_intro_nivell();
            }
        }



        public Intro inserirIntro()
        {


            int hores = Int32.Parse(cbmHores.SelectedItem.ToString()); ;
            int minuts = Int32.Parse(cbmMinuts.SelectedItem.ToString()); ;
            int segons = Int32.Parse(cbmSegons.SelectedItem.ToString()); ;



            if (Intro.getNom(txbNom.Text) || txbNom.Text == "")
            {
                var messageDialog = new MessageDialog("No pots registrar una introducció amb un nom ja existent o que el nom estugui buit");
                messageDialog.ShowAsync();

                return null;
            }

            else if (hores == 0 && minuts == 0 && segons == 0)
            {
                var messageDialog = new MessageDialog("No pots registrar una introducció amb una duració menor a 1 segon");
                messageDialog.ShowAsync();

                return null;
            }

            else
            {

                BitmapImage bitMap = imgfons.Source as BitmapImage;
                Uri uri = bitMap?.UriSource;


                String nom = txbNom.Text;
                String des = txbDesc.Text;
                String imatge = uri.AbsolutePath;
                bool estat = false;

                if (chEstat.IsChecked == true)
                {
                    estat = true;
                }
                else
                {
                    estat = false;
                }


                Intro nou = new Intro(nom, des, hores, minuts, segons, estat, imatge);
                int id=Intro.getIdIntro(nou);

                nou.Id = id;
                return nou;
            }

            
        }





        public Intro actualitzarIntro()
        {


                int hores = Int32.Parse(cbmHores.SelectedItem.ToString()); ;
                int minuts = Int32.Parse(cbmMinuts.SelectedItem.ToString()); ;
                int segons = Int32.Parse(cbmSegons.SelectedItem.ToString()); ;



                if ( txbNom.Text == "")
                {
                    var messageDialog = new MessageDialog("No pots registrar una introducció amb un nom buit");
                    messageDialog.ShowAsync();

                    return null;
                }

                else if (hores == 0 && minuts == 0 && segons == 0)
                {
                    var messageDialog = new MessageDialog("No pots registrar una introducció amb una duració menor a 1 segon");
                    messageDialog.ShowAsync();

                    return null;
                }

                else
                {

                    BitmapImage bitMap = imgfons.Source as BitmapImage;
                    Uri uri = bitMap?.UriSource;

                   
                    String nom = txbNom.Text;
                    String des = txbDesc.Text;
                    String imatge = uri.AbsolutePath;
                    bool estat = false;

                    if (chEstat.IsChecked == true)
                    {
                        estat = true;
                    }
                    else
                    {
                        estat = false;
                    }


                    Intro nou = new Intro(nom, des, hores, minuts, segons, estat, imatge);


                    return nou;
                }
            

        }

        




        public Editor ConnexioEditro { get; set; }

        public Intro LamevaIntro { get; set; }



        public void canviarText()
        {
            txbNom.Text = LamevaIntro.Nom;
            txbDesc.Text = LamevaIntro.Descripcio;
            txbImatge.Text = LamevaIntro.Url;
            cbmHores.SelectedItem = LamevaIntro.Hores;
            cbmMinuts.SelectedItem = LamevaIntro.Minuts;
            cbmSegons.SelectedItem = LamevaIntro.Segons;
            chEstat.IsChecked = LamevaIntro.Actiu;

            if (LamevaIntro.Url.Contains("/Assets"))
            {
                BitmapImage pred = new BitmapImage(new Uri("ms-appx:///Assets/bomb.png"));
                imgfons.Source = pred;


            }
            else
            {
                BitmapImage imatge = new BitmapImage(new Uri(LamevaIntro.Url));
                imgfons.Source = imatge;

            }



        }



        public void netejarInfo()
        {
            txbNom.Text = "";
            txbDesc.Text = "";
            txbImatge.Text = "";
            cbmHores.SelectedItem = cbmHores.Items.First();
            cbmMinuts.SelectedItem = cbmMinuts.Items.First();
            cbmSegons.SelectedItem = cbmSegons.Items.First();
            chEstat.IsChecked = true;



            BitmapImage pred = new BitmapImage(new Uri("ms-appx:///Assets/bomb.png"));
            imgfons.Source = pred;
        }

        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            netejarInfo();

        }

        private void btnActualitzar_Click(object sender, RoutedEventArgs e)
        {
            Intro actualitzar = actualitzarIntro();

            if (actualitzar != null)
            {
                if (Intro.getNom(actualitzar.Nom) && Intro.getIdIntro(actualitzar) != LamevaIntro.Id)
                {
                    var messageDialog = new MessageDialog("No pots actualitzar la introducció actual amb el nom de una altre ja existent");
                    messageDialog.ShowAsync();
                }
                else
                {

                    Intro.Update(actualitzar, LamevaIntro);
                    netejarInfo();
                    ConnexioEditro.obtenir_intro_nivell();
                }


            } 
        }
    }
}
