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
using Windows.UI.Popups;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bomberman_Practica
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Editor : Page
    {

        List<Level> nivells = ConnexioBD.Level.getNivell();
        List<Level> intro = ConnexioBD.Intro.getIntro();

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

            


            if (GRDLevel.SelectedItem == null)
            {
                return;
            }



            if (GRDLevel.SelectedItem.GetType().Name=="Intro")
            {
                rdoIntro.IsChecked = true;
                Intro.LamevaIntro = (Intro)GRDLevel.SelectedItem;
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

            obtenir_intro_nivell();
            

                
        }

        public void obtenir_intro_nivell()
        {
            List<Level> nivells = ConnexioBD.Level.getNivell();
            List<Level> intro = ConnexioBD.Intro.getIntro();
            nivells.AddRange(intro);


            GRDLevel.ItemsSource = nivells;// ConnexioBD.Intro.getIntro();

        }

        private void btnRefrescar_Click(object sender, RoutedEventArgs e)
        {
           
            
            obtenir_intro_nivell();
            
        }



        private void btnRepresentar_Click(object sender, RoutedEventArgs e)
        {

           



        }

        private void btnEsborrar_Click(object sender, RoutedEventArgs e)
        {
            if (GRDLevel.SelectedItem == null)
            {
                var messageDialog = new MessageDialog("Has de seleccionar un element de la llista per eliminar");
                messageDialog.ShowAsync();
            }



            else if (GRDLevel.SelectedItem.GetType().Name == "Intro")
            {

                Intro eliminar = (Intro) GRDLevel.SelectedItem;

                ConnexioBD.Intro.eliminarIntro(eliminar);



            }
            else if (GRDLevel.SelectedItem.GetType().Name == "Level")
            {
                Level eliminar = (Level)GRDLevel.SelectedItem;

                ConnexioBD.Level.eliminarLevel(eliminar);
            }


            obtenir_intro_nivell();


        }
    }
}
