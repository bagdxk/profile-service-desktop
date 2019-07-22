using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bagdxk.Profile.Service.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        protected async void OnLoad(object sender, RoutedEventArgs e)
        {

            await RunAsync();
        }

        protected async void OnClosing(object sender, CancelEventArgs e)
        {
            try
            {
                var layout = new Layout
                {
                    id = "tianqiac.default",
                    WindowState = this.WindowState.ToString(),
                    Top = this.Top,
                    Left = this.Left,
                    Width = this.Width,
                    Height = this.Height
                };

                // await Task.Run(async() =>
                //{
                    await SaveLayoutAsync(layout);
                    //await Task.Delay(1000);
                //});
                    
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task SaveLayoutAsync(Layout layout)
        {
            try
            {
                var task = client.PutAsJsonAsync<Layout>("api/layouts/tianqiac.default", layout);
                var result = task.GetAwaiter().GetResult();

                //Debug.WriteLine(response.StatusCode);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async Task RunAsync()
        {
            client.BaseAddress = new Uri("http://localhost:49924/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync("api/layouts/tianqiac.default");
                if (response.IsSuccessStatusCode)
                {
                    var layout = await response.Content.ReadAsAsync<Layout>();
                    if (layout != null)
                    {
                        WindowState state;
                        if (Enum.TryParse<WindowState>(layout.WindowState, out state))
                        {
                            this.WindowState = state;
                        }
                        this.Top = layout.Top;
                        this.Width = layout.Width;
                        this.Left = layout.Left;
                        this.Height = layout.Height;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }
    }
}
