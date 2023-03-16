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
using System.Collections.ObjectModel;
using Org.BouncyCastle.Utilities;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=234238

namespace Bomberman_Practica
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class Editor : Page
    {
        //Declaració d'elements globals
        ObservableCollection<Level> nivells = null;
        ObservableCollection<Intro> intro = null;

        public Editor()
        {
            this.InitializeComponent();
        }


        /// <summary>
        /// Comportament dels RadioButtons en relació als UserControls de Intro i Level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdoNivellLloc_Click(object sender, RoutedEventArgs e)
        {
           
            
            
            Intro.Visibility = rdoIntro.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
            Intro.ConnexioEditro = this;
            Intro.netejarInfo();

            Level.Visibility = Intro.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            Level.ConnexioEditor = this;
            Level.netejarInfoLevel();
            Level.netejargraella();


            GRDLevel.SelectedItem = null;


        }

        /// <summary>
        /// Comportament del canvi de item selecionat en el DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                Intro.canviarText();
                Intro.ConnexioEditro = this;
                Intro.Visibility = rdoIntro.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                Level.Visibility = Intro.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

                

                
                
               
            }
            else if(GRDLevel.SelectedItem.GetType().Name == "Level")
            {

                rdoNivellLloc.IsChecked = true;
                Level.ElmeuLevel = (Level)GRDLevel.SelectedItem;
                Level.canviarText();
                Level.ConnexioEditor = this;
                Intro.Visibility = rdoIntro.IsChecked.Value ? Visibility.Visible : Visibility.Collapsed;
                Level.Visibility = Intro.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            }
          

        }

        /// <summary>
        /// Loaded de la pantalla Editor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            obtenir_intro_nivell();
            

                
        }
         
        /// <summary>
        /// Connexió amb la BD per obtenir els nivells i les intro registrades
        /// </summary>
        public void obtenir_intro_nivell()
        {
            nivells = ConnexioBD.Level.getNivell();
            intro = ConnexioBD.Intro.getIntro();
            

            foreach (var item in intro)
            {
                nivells.Add(item);
            }


            GRDLevel.ItemsSource = nivells;// ConnexioBD.Intro.getIntro();

        }

        /// <summary>
        /// Funcionament del botó de eliminar un element de la BD y del DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                Intro.netejarInfo();
                Intro.BloquejarActualització();


            }
            else if (GRDLevel.SelectedItem.GetType().Name == "Level")
            {
                Level eliminar = (Level)GRDLevel.SelectedItem;


                ConnexioBD.Level.DeleteBlocsNivell(eliminar.Id);

                ConnexioBD.Level.eliminarLevel(eliminar);
                Level.netejarInfoLevel();
                Level.BloquejarActualització();
                
            }


            obtenir_intro_nivell();
            
            //Evitar out of index Exception
            if (nivells.Count > 0)
            {

                GRDLevel.SelectedItem = nivells[0];
            }

        }
    }
}
