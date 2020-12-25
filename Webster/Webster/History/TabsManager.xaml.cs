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

            Tabs = new List<Tab>();

            var info = JsonConvert.DeserializeObject<List<TabInfo>>(File.ReadAllText(FileName));
            
            foreach (var tabInfo in info)
            {
                var tab = new Tab(this, tabInfo);
                
                Tabs.Add(tab);
                TabsList.Children.Add(tab);
            }
        }

        public void AddTab(string url)
        {
            if (Tabs.Any(x => x.Info.Url == url))
            {
                return;
            }
            
            Tab tab = new Tab(this, new TabInfo(url, url, null, DateTime.Now));
            
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

            var data = JsonConvert.SerializeObject(Tabs.Select(x => x.Info));
            
            File.WriteAllText(FileName, data);
        }
    }
}