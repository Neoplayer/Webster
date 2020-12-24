using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Webster
{
    public partial class MainPage : ContentPage
    {
        public static MainPage Instance;


        public MainPage()
        {
            Instance = this;
            
            InitializeComponent();
            
            MainBar.Init(MainBrowser);
        }
    }
}
