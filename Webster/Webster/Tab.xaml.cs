using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Webster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tab : ContentView
    {
        public string Url;

        private TabsManager Parent;

        public Tab(TabsManager manager, string url, string title, Uri imageUri)
        {
            InitializeComponent();

            Parent = manager;
            
            Url = url;
            TabName.Text = title;
            if (imageUri != null)
            {
                Image.Source = ImageSource.FromUri(imageUri);
            }
            
            var trigger = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            trigger.TappedCallback = (sender, args) =>
            {
                MainPage.MainBrowser.Source = Url;
            };
            MainGrid.GestureRecognizers.Add(trigger);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Parent.RemoveTab(this);
        }
    }
}