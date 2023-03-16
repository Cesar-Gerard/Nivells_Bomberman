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
        //Elements Globals
        int start_point;
        int finish_point;


        //Constructor
        public LevelView()
        {
            this.InitializeComponent();
            carregarGrid();

            start_point = 0; 
            finish_point=0;
        }

        
        /// <summary>
        /// Crea i assigna els fills del grid per crear la graella del nivell
        /// </summary>
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

        
        /// <summary>
        /// Prepara els elements i l'estat del UserControl quan es carrega
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //Carreguem les imatges dels elements disponibles 
            lsvBlocs.ItemsSource = Casella.llistacasellas();
            lsvBlocs.SelectedItem = lsvBlocs.Items.First();
            carregarComboTemps();
        }


        /// <summary>
        /// Retorna com a Casella l'item de la ListView seleccionat
        /// </summary>
        /// <returns></returns>
        public Casella pregunta_Item()
        {
           
            Casella seleccionat =(Casella)lsvBlocs.SelectedItem;
            return seleccionat;
            
            
        }


        /// <summary>
        /// Controla els canvis de selecció del listview de caselles
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsvBlocs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pregunta_Item();
        }


        /// <summary>
        /// Crea el nivell, el guarda a la BD y també la graella de nivell corresponent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        /// <summary>
        /// Carrega el valor de temps dels combobox del nivell
        /// </summary>
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


        /// <summary>
        /// Recorre totes les columnes i files de la graella y guarda a la BD el tipus de casella y la posicio de cada cel·la 
        /// </summary>
        /// <param name="nou"></param>
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


        /// <summary>
        /// Comprova el nombre mínim i màxim de les caselles de inici i final 
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Llegeix el valor actualitzat de totes les cel·les per fer la actualització de la graella a la BD
        /// </summary>
        /// <param name="actualitzat"></param>
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


        #region Accés a elements externs
        /// <summary>
        /// Ens dona accès als atributs del nivell seleccionat a la DataGrid de Editor
        /// </summary>
        public Level ElmeuLevel { get; set; }

        /// <summary>
        /// Dona accès als métodes de la pantalla Editor
        /// </summary>
        public Editor ConnexioEditor { get; set; }
        #endregion


        /// <summary>
        /// ctualitza els elements de informació per la del Nivellseleccionat al DataGrid
        /// </summary>
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

            desbloquejarActualització();
        }


        /// <summary>
        /// Neteja tota la informació presentada
        /// </summary>
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


        /// <summary>
        /// Neteja a graella de cel·les del nivell i les retorna al seu valor per defecte
        /// </summary>
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
                        fill.rebre_casella(new Casella("", "", 1));
                        i++;
                    }

                }

            }
        }


        /// <summary>
        /// Activa la nateja de informació del UserControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btncancelar_Click(object sender, RoutedEventArgs e)
        {
            netejarInfoLevel();

        }


        /// <summary>
        /// Omple la graella amb les cel·les guardades a la BD corresponent al nivell seleccionat al DataGrid
        /// </summary>
        /// <param name="recuperar"></param>
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


        /// <summary>
        /// DEsbloqueja el boto de actualitzacio
        /// </summary>
        public void desbloquejarActualització()
        {
            btnActualitzar.IsEnabled = true;
        }

        /// <summary>
        /// Bloqueja el boto de actualitzacio
        /// </summary>
        public void BloquejarActualització()
        {
            btnActualitzar.IsEnabled = false;
        }

        /// <summary>
        /// Crea un nou nivell amb la informació amb la que es vol substituir al seleccionat al DataGrid de Editor
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Actualitza la informació del nivell seleccionat per la nova informació proporcionada per el nou objecte nivell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
