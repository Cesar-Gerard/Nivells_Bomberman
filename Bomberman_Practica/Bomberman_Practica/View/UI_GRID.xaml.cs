using Bomberman_Practica.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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


        public UI_GRID(LevelView level)
        {   
            this.InitializeComponent();
            this.level = level;
        }




        public BitmapSource MyProperty
        {
            get { return (BitmapSource)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(BitmapSource), typeof(LevelView), new PropertyMetadata(new BitmapImage(new Uri("ms-appx:///Assets/bomb.png"))));






        private void grdFonsNivell_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Casella item = this.level.pregunta_Item();
            BitmapImage Imatge_Item = new BitmapImage(new Uri("ms-appx://"+item.Img));

            imgNIvell.Source = Imatge_Item;

        }

        private void UserControl_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            bFonsNivell.Background = new SolidColorBrush(Colors.WhiteSmoke);

            
        }

        private void UserControl_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            bFonsNivell.Background = new SolidColorBrush(Colors.Transparent);
        }
    }
}
