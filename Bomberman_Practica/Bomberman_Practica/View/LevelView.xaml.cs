using Bomberman_Practica.Model;
using ConnexioBD;
using MySqlX.XDevAPI.Relational;
using SharpDX.WIC;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Security.Policy;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Notifications;
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
        int start_point;
        int finish_point;

        public LevelView()
        {
            this.InitializeComponent();
            carregarGrid();

            start_point = 0; 
            finish_point=0;
        }

        
        
        private void carregarGrid()
        {
            for(int i =0; i<10; i++)
            {
                grdNivell.ColumnDefinitions.Add(new ColumnDefinition());
                grdNivell.RowDefinitions.Add(new RowDefinition());

            }

            for(int j =0; j < grdNivell.ColumnDefinitions.Count; j++)
            {
                for(int x = 0; x < grdNivell.RowDefinitions.Count; x++)
                {
                    UI_GRID casella = new UI_GRID(this);
                    casella.Tag = 1;
                    
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





        public Casella pregunta_Item()
        {
           
            Casella seleccionat =(Casella)lsvBlocs.SelectedItem;
            return seleccionat;
            
            
        }


        private void lsvBlocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pregunta_Item();
        }

        private void btnCrearNIvell_Click(object sender, RoutedEventArgs e)
        {
           int hores = Int32.Parse(cbmHores.SelectedItem.ToString());
           int minuts = Int32.Parse(cbmMinuts.SelectedItem.ToString());
           int segons = Int32.Parse(cbmSegons.SelectedItem.ToString());



            if (Level.getNomNivell(txbNom.Text) || txbNom.Text == "")
            {
                var messageDialog = new MessageDialog("No pots registrar un nivell amb un nom ja existent o que el nom estugui buit");
                messageDialog.ShowAsync();
            }

            else if (hores == 0 && minuts == 0 && segons == 0)
            {
                var messageDialog = new MessageDialog("No pots registrar un nivell amb una duració menor a 1 segon");
                messageDialog.ShowAsync();
            }

            else
            {


                String nom = txbNom.Text;
                String des = txbDesc.Text;
                bool estat = false;

                if (chEstat.IsChecked == true)
                {
                    estat = true;
                }
                else
                {
                    estat = false;
                }


                Level nou = new Level(nom, des, hores, minuts, segons, estat);

                if (comprovarNoRepeticioIniciFinal())
                {
                    var messageDialog = new MessageDialog("Només es poden guardar els nivells que tinguin un punt de inici i final");
                    messageDialog.ShowAsync();
                }
                else
                {



                    Level.InserirNivell(nou);
                    int id = Level.getIdLevel(nou.Nom);
                    nou.Id = id;
                    this.guardarBlocs(nou);

                    ConnexioEditor.obtenir_intro_nivell();
                    netejarInfoLevel();
                }

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

            cbmHores.SelectedItem = 0; cbmMinuts.SelectedItem = 0; cbmSegons.SelectedItem = 0;
        }


        public void guardarBlocs(Level nou)
        {
            int i=0;
            int j;
            int x;
            String nom = nou.Nom;
            String resultat="";



                for (j = 0; j < grdNivell.ColumnDefinitions.Count; j++)
                {
                    for (x = 0; x < grdNivell.RowDefinitions.Count; x++)
                    {

                        UI_GRID fill = (UI_GRID)grdNivell.Children.ElementAt(i++);

                        int num_casella = (int)fill.Tag;



                        Level.guardarBlocs(nou.Id, x, j, num_casella);

                    }

                }
            
        }

        private Boolean comprovarNoRepeticioIniciFinal()
        {
            int i = 0;
            int j;
            int x;

            int inici = 0;
            int final = 0;


            for (j = 0; j < grdNivell.ColumnDefinitions.Count; j++)
            {
                for (x = 0; x < grdNivell.RowDefinitions.Count; x++)
                {

                    UI_GRID fill = (UI_GRID)grdNivell.Children.ElementAt(i++);

                    int num_casella = (int)fill.Tag;

                    if(num_casella == 5)
                    {
                        inici++;
                    }else if (num_casella == 6)
                    {
                        final++;
                    }

                   

                }

            }

            if(inici >1 || final > 1 || inici==0 || final ==0)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public void UpdateBlocsNivell(Level actualitzat)
        {
            int i = 0;
            int j;
            int x;
            String nom = actualitzat.Nom;
            String resultat = "";


            for (j = 0; j < grdNivell.ColumnDefinitions.Count; j++)
            {
                for (x = 0; x < grdNivell.RowDefinitions.Count; x++)
                {

                    UI_GRID fill = (UI_GRID)grdNivell.Children.ElementAt(i++);

                    int num_casella = (int)fill.Tag;

                    

                    Level.BlocsNivellUpdateBD(ElmeuLevel.Id, x, j, num_casella);

                }

            }
        }





        public Level ElmeuLevel { get; set; }

        public Editor ConnexioEditor { get; set; }

        public void canviarText()
        {
            txbNom.Text = ElmeuLevel.Nom;
            txbDesc.Text = ElmeuLevel.Descripcio;
            cbmHores.SelectedItem = ElmeuLevel.Hores;
            cbmMinuts.SelectedItem = ElmeuLevel.Minuts;
            cbmSegons.SelectedItem = ElmeuLevel.Segons;
            chEstat.IsChecked = ElmeuLevel.Actiu;


            List<Casella> recuperar = new List<Casella>();

            recuperar = Level.getBlocsNivell(ElmeuLevel.Id);
            
            omplirgraella(recuperar);


        }


        public void netejarInfoLevel()
        {
            txbNom.Text = "";
            txbDesc.Text = "";
            cbmHores.SelectedItem = cbmHores.Items.First();
            cbmMinuts.SelectedItem = cbmMinuts.Items.First();
            cbmSegons.SelectedItem = cbmSegons.Items.First();
            chEstat.IsChecked = true;
            lsvBlocs.SelectedItem = lsvBlocs.Items.First();

            netejargraella();

            
        }


        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            netejarInfoLevel();

        }

        private void omplirgraella(List<Casella> recuperar)
        {
            int i = 0;
            for (int j = 0; j < grdNivell.ColumnDefinitions.Count; j++)
            {
                for (int x = 0; x < grdNivell.RowDefinitions.Count; x++)
                {
                    UI_GRID fill = (UI_GRID)grdNivell.Children.ElementAt(i);
                    if (fill != null)
                    {
                        fill.rebre_casella(recuperar[i++]);
                    }

                }
               
            }



        }

        public void netejargraella()
        {
            int i = 0;
            for (int j = 0; j < grdNivell.ColumnDefinitions.Count; j++)
            {
                for (int x = 0; x < grdNivell.RowDefinitions.Count; x++)
                {
                    UI_GRID fill = (UI_GRID)grdNivell.Children.ElementAt(i);
                    if (fill != null)
                    {
                        fill.rebre_casella(new Casella("","",1));
                        i++;
                    }

                }

            }
        }


        public Level actualitzarLevel()
        {
            Level actualitzat = null;

            int hores = Int32.Parse(cbmHores.SelectedItem.ToString()); ;
            int minuts = Int32.Parse(cbmMinuts.SelectedItem.ToString()); ;
            int segons = Int32.Parse(cbmSegons.SelectedItem.ToString()); ;



            if (txbNom.Text == "")
            {
                var messageDialog = new MessageDialog("No pots registrar un nivell amb un nom buit");
                messageDialog.ShowAsync();

                return null;
            }

            else if (hores == 0 && minuts == 0 && segons == 0)
            {
                var messageDialog = new MessageDialog("No pots registrar un nivell amb una duració menor a 1 segon");
                messageDialog.ShowAsync();

                return null;
            }

            else
            {


                String nom = txbNom.Text;
                String des = txbDesc.Text;
                bool estat = false;

                if (chEstat.IsChecked == true)
                {
                    estat = true;
                }
                else
                {
                    estat = false;
                }


                actualitzat = new Level(nom, des, hores, minuts, segons, estat);
                actualitzat.Id = Level.getIdLevel(ElmeuLevel.Nom);


                return actualitzat;
            }
           

        }



        private void btnActualitzar_Click(object sender, RoutedEventArgs e)
        {
            Level nou = actualitzarLevel();


            if (nou != null)
            {
                if (Level.getNomNivell(nou.Nom) && Level.getIdLevel(nou.Nom)!=nou.Id)
                {
                    var messageDialog = new MessageDialog("No pots actualitzar el nivell actual actual amb el nom de una altre ja existent");
                    messageDialog.ShowAsync();
                }

                else if (comprovarNoRepeticioIniciFinal())
                {
                    var messageDialog = new MessageDialog("Només es poden guardar els nivells que tinguin un punt de inici i final");
                    messageDialog.ShowAsync();
                }
                else
                {

                    Level.UpdateLevel(nou, ElmeuLevel);
                    this.UpdateBlocsNivell(nou );
                    netejarInfoLevel();
                    ConnexioEditor.obtenir_intro_nivell();
                }


            }
        }


    }
}
