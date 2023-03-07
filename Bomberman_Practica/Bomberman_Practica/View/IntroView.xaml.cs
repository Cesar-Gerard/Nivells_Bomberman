﻿using System;
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
            }
            else
            {
                string name = (DateTime.Now).ToString("yyyyMMddhhmmss") + "_" + sf.Name;
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

            BitmapImage bitMap = imgfons.Source as BitmapImage;
            Uri uri = bitMap?.UriSource;


            String nom=txbNom.Text;
            String des=txbDesc.Text;
            int hores=Int32.Parse(cbmHores.SelectedItem.ToString());
            int minuts= Int32.Parse(cbmMinuts.SelectedItem.ToString()); 
            int segons= Int32.Parse(cbmSegons.SelectedItem.ToString());
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
           
            

            Intro nou = new Intro( nom, des,  hores, minuts, segons,  estat, imatge);

            Intro.Inserir( nou );

            


        }

        public Intro LamevaIntro { get; set; }

        

        

        public Intro info
        {
            get { return (Intro)GetValue(infoProperty); }
            set { SetValue(infoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for info.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty infoProperty =
            DependencyProperty.Register("info", typeof(Intro), typeof(Intro), new PropertyMetadata(null));


    }
}
