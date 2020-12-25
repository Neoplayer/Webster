using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Webster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabsManager : ContentView
    {
        
        string FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "story.json");


        private List<Tab> Tabs;
        
        public TabsManager()
        {
            InitializeComponent();

            Tabs = File.Exists(FileName) ? JsonConvert.DeserializeObject<List<Tab>>(File.ReadAllText(FileName)) : new List<Tab>();
        }

        public void AddTab(string url)
        {
            if (Tabs.FirstOrDefault(x => x.Url == url) != null)
            {
                return;
            }
            
            Tab tab = new Tab(this, url, url, null);
            
            Tabs.Add(tab);
            TabsList.Children.Add(tab);

            UpdateStorage();
        }
        
        public void RemoveTab(Tab tab)
        {
            Tabs.Remove(tab);
            TabsList.Children.Remove(tab);

            UpdateStorage();
        }


        private void UpdateStorage()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }

            File.WriteAllText(FileName, JsonConvert.SerializeObject(Tabs));
        }
    }
}