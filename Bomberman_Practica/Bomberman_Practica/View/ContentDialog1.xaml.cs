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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using System.Threading;

// La plantilla de elemento del cuadro de diálogo de contenido está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bomberman_Practica.View
{
    public sealed partial class ContentDialog1 : ContentDialog
    {
        private String titol;
        private ImageSource imatge_source = null;
        private String descripcio;
        private List<Int32> temps;

       
        public ContentDialog1(IntroView entrada)
        {
            this.InitializeComponent();
            titol = entrada.recuperarTitol();
            descripcio = entrada.recuperarDescripcio();
            imatge_source = entrada.recuperarImatge();
            temps = entrada.recuperarTemps();
            

            comptador_temps();
        }

       
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
           
        }

        private void ContentDialog_Loaded(object sender, RoutedEventArgs e)
        {
            TitolPreview.Text = titol;
            IMGPreview.Source = imatge_source;
            DescripcioPreview.Text = descripcio;

            
        }

        private void comptador_temps()
        {
            var timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(temps[0], temps[1], temps[2]);
            
            timer.Start();
            timer.Tick += (s, e) =>
            {
                timer.Stop();
                this.Hide();
            };
            timer.Start();
        }


    }
}
