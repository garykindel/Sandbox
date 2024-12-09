using Microsoft.Owin.Hosting;
using Owin;
using RestSharp;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace PinterestDemo
{
public class SelfHostedServer
    {   
        private IDisposable _webApp;
        public MainWindow _mainWindow;

        public SelfHostedServer(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void Start(string baseUri)
        {
            _webApp = WebApp.Start<Startup>(url: baseUri);
        }

        public void Stop()
        {
            _webApp?.Dispose();
        }

        public IDisposable GetBaseUrl()
        {
            return _webApp;
        }

        public class Startup
        {
            public void Configuration(IAppBuilder app)
            {               
                app.Run(async context =>
                {
                    if (context.Request.Path.Value == "/callback")
                    {
                        var code = context.Request.Query["code"];
                        if (!string.IsNullOrEmpty(code))
                        {
                            //_mainWindow._authorizationCode = code;
                           // _mainWindow.GetAccessToken(code);
                        }
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("<html><body>Authorization code received. You can close this window.</body></html>");
                    }
                });
            }
        }
    }

}
