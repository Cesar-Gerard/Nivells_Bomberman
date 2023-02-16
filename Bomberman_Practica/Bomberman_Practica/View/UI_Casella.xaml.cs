using Bomberman_Practica.Model;
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

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace Bomberman_Practica.View
{
    public sealed partial class UI_Casella : UserControl
    {
        public UI_Casella()
        {
            this.InitializeComponent();
        }



        public Casella Seleccio
        {
            get { return (Casella)GetValue(SeleccioProperty); }
            set { SetValue(SeleccioProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeleccioProperty =
            DependencyProperty.Register("Seleccio", typeof(Casella), typeof(UI_Casella), new PropertyMetadata(null));


    }
}
