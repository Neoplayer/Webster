using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Webster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tab : ContentView
    {
        public TabInfo Info;
        private TabsManager Manager;

        public Tab(TabsManager manager, TabInfo info)
        {
            InitializeComponent();

            Manager = manager;

            Info = info;
            TabName.Text = info.Title;
            if (info.ImageIri != null)
            {
                Image.Source = ImageSource.FromUri(info.ImageIri);
            }
            
            var trigger = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            trigger.TappedCallback = (sender, args) =>
            {
                MainPage.MainBrowser.Source = info.Url;
            };
            MainGrid.GestureRecognizers.Add(trigger);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Manager.RemoveTab(this);
        }
    }
}