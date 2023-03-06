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
using ConnexioBD;
using Bomberman_Practica.View;
using Microsoft.EntityFrameworkCore.Internal;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bomberman_Practica
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Editor : Page
    {

        List<Level> llista = null;

        public Editor()
        {
            this.InitializeComponent();
        }

        private void rdoNivellLloc_Click(object sender, RoutedEventArgs e)
        {
           

            Intro.Visibility = rdoIntro.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
            Level.Visibility = Intro.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            
        }

        private void GRDLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (GRDLevel.SelectedItem.GetType().Name == "Intro")
            {
                rdoIntro.IsChecked = true;
                Intro.Visibility = rdoIntro.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                Level.Visibility = Intro.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
            else if(GRDLevel.SelectedItem.GetType().Name == "Level")
            {

                rdoNivellLloc.IsChecked = true;
                Intro.Visibility = rdoIntro.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                Level.Visibility = Intro.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
          

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            List<Level> nivells = ConnexioBD.Level.getNivell();
            List<Level> intro = ConnexioBD.Intro.getIntro();
            nivells.AddRange(intro);



            GRDLevel.ItemsSource = nivells;// ConnexioBD.Intro.getIntro();
            

                
        }
    }
}
