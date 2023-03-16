using Bomberman_Practica.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class UI_GRID : UserControl
    {
        
        private LevelView level;

        //constructor del UserControl
        public UI_GRID(LevelView level)
        {   
            this.InitializeComponent();
            this.level = level;
        }



        /// <summary>
        /// Propdp per la imatge de la graella
        /// </summary>
        public BitmapSource MyProperty
        {
            get { return (BitmapSource)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(BitmapSource), typeof(LevelView), new PropertyMetadata(new BitmapImage(new Uri("ms-appx:///Assets/fons_grid.png"))));





        /// <summary>
        /// Comportament del recauadre al interactuar amb ell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdFonsNivell_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Casella item = this.level.pregunta_Item();
            BitmapImage Imatge_Item = new BitmapImage(new Uri("ms-appx://"+item.Img));

            Imatge_Item.DecodePixelHeight = (int) imgNIvell.ActualHeight;
            Imatge_Item.DecodePixelWidth = (int) imgNIvell.ActualWidth;

            imgNIvell.Source = Imatge_Item;


            this.Tag = item.Id;
          
        }

        /// <summary>
        /// Comportament del border de la imatge del UserControl al passar per sobre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            bFonsNivell.Background = new SolidColorBrush(Colors.WhiteSmoke);

            
        }

        /// <summary>
        /// Comportament del border de la imatge del UserControl al sortir d'ell amb el ratolí
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            bFonsNivell.Background = new SolidColorBrush(Colors.Transparent);
        }

        /// <summary>
        /// Reb cada casella de la graella de un nivell y la col·loca i guarda amb la mida adequada
        /// </summary>
        /// <param name="entrada"></param>
        public void rebre_casella(Casella entrada)
        {
            entrada.retornaImatgeString(entrada.Id);

         

            BitmapImage Imatge_Item = new BitmapImage(new Uri("ms-appx://" + entrada.Img));

            Imatge_Item.DecodePixelHeight = (int)imgNIvell.ActualHeight;
            Imatge_Item.DecodePixelWidth = (int)imgNIvell.ActualWidth;

            imgNIvell.Source = Imatge_Item;
            this.Tag = entrada.Id;
        }


    }
}
