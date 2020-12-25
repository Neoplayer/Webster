using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Webster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainBar : ContentView
    {
        public BarState BarState { get; set; }
        public string SearchRequest = String.Empty;


        private WebView Browser;

        public MainBar()
        {
            InitializeComponent();
        }

        public void Init(WebView browser)
        {
            Browser = browser;
            
            
            Browser.Navigating += (sender, args) =>
            {
                UrlLine.Text = args.Url;
                
                TabsManager.AddTab(args.Url);
            };
        }
        
        private void BackButton_Clicked(object sender, EventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
        }
        private void ForwardButton_Clicked(object sender, EventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }

        private void QSettingsButton_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(SearchRequest))
            {
                Browser.Source = RequestDispatcher.CreateUrl(SearchRequest);
            }
            else
            {
                var animation = new Animation(d =>
                {
                    QSettingsGrid.HeightRequest = d;
                }, BarState == BarState.Minimized ? 0 : 200, BarState == BarState.Minimized ? 200 : 0);
                animation.Commit(this, "OpenAnim", 16, 100, Easing.Linear);

                BarState = BarState == BarState.Minimized ? BarState.Maximized : BarState.Minimized;
            }
        }

        private void UrlLine_OnFocused(object sender, FocusEventArgs e)
        {
            Show();
        }
        private void UrlLine_OnFocusedLost(object sender, FocusEventArgs e)
        {
            Hide();
        }

        private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
        {
            Hide();
        }


        private void Show()
        {
            if (BarState == BarState.Maximized)
            {
                return;
            }
            BarState = BarState.Maximized;
            
            
            var animation = new Animation(d =>
            {
                NavStack.WidthRequest = d;
            }, 100, 0);
            animation.Commit(this, "OpenAnim", 16, 100, Easing.Linear, (d, b) =>
            {
                NavStack.IsVisible = false;
            });
            
            
            TButton.Text = ">";
            SearchRequest = String.Empty;
            
            Dispatcher.BeginInvokeOnMainThread(() =>
            {
                UrlLine.CursorPosition = 0;
                UrlLine.SelectionLength = UrlLine.Text?.Length ?? 0;
            });
        }
        
        public void Hide()
        {
            if (BarState == BarState.Minimized)
            {
                return;
            }
            BarState = BarState.Minimized;
            
            NavStack.IsVisible = true;
            var animation = new Animation(d =>
            {
                NavStack.WidthRequest = d;
            }, 0, 100);
            animation.Commit(this, "CloseAnim", 16, 100, Easing.Linear);

            TButton.Text = "i";
            SearchRequest = UrlLine.Text;
        }
    }

    public enum BarState
    {
        Minimized,
        Maximized
    }
}