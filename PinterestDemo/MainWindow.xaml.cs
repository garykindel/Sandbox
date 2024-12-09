using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace PinterestDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string ClientId = "1490788";
        private const string ClientSecret = "58087bb1415c4208da2ed00ba11b701a241db072";
        private const string RedirectUri = "https://592d-2600-1700-6a60-5bc0-d436-4ec6-ea64-556c.ngrok-free.app/CallBack";
        private SelfHostedServer _server;
        public string _authorizationCode;

        public MainWindow()
        {
            InitializeComponent();
            _server = new SelfHostedServer(this);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _server.Start("http://localhost:5000");
            //_server.Start("https://592d-2600-1700-6a60-5bc0-d436-4ec6-ea64-556c.ngrok-free.app");
            StartPinterestAuthenticationAsync();
        }

        private async Task StartPinterestAuthenticationAsync()
        {
             var authUrl = $"https://api.pinterest.com/v5/oauth/?response_type=code&client_id={ClientId}&redirect_uri={RedirectUri}&scope=read_public";
             Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });                   
        }





        public async void GetAccessToken(string code)
        {
            var client = new RestClient("https://api.pinterest.com/v5/oauth/token");
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("client_id", ClientId);
            request.AddParameter("client_secret", ClientSecret);
            request.AddParameter("code", code);
            request.AddParameter("redirect_uri", RedirectUri);

            var response = await client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                MessageBox.Show("Access token obtained successfully.");
            }
            else
            {
                MessageBox.Show("Failed to obtain access token.");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _server.Stop();
        }

        private void FetchTokenButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_authorizationCode))
            {
                GetAccessToken(_authorizationCode);
            }
            else
            {
                MessageBox.Show("Authorization code not available. Please authenticate first.");
            }
        }
    }
}
