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
            string name = (DateTime.Now).ToString("yyyyMMddhhmmss") + "_" + sf.Name;
            // Copiar l'arxiu triat a la carpeta indicada, usant el nom que hem muntat
            StorageFile copiedFile = await sf.CopyAsync(iconsFolder, name);
            // Crear una imatge en memòria (BitmapImage) a partir de l'arxiu copiat a ApplicationData
             tmpBitmap = new BitmapImage(new Uri(copiedFile.Path));

            
            

            imgfons.Source = tmpBitmap;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog1 entrada = new ContentDialog1(txbNom.Text, imgfons.Source,txbDesc.Text);

            entrada.ShowAsync();
            

        }
    }
}
