using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;

namespace App1
{
    public sealed partial class MainPage : Page
    {
        private IHubProxy hub;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            InitSignalR();
        }

        private async void InitSignalR()
        {
            slider.IsEnabled = false;
            textbl.Text = "Connecting to SignalR Server";

            await Task.Run(async () =>
            {
                SignalRClient signalR = new SignalRClient();
                signalR.InitializeSignalR();
                signalR.ValueChanged += ValueChanged;
                hub = signalR.SignalRHub;

                await Dispatcher.RunAsync(
                    CoreDispatcherPriority.Normal, () =>
                    {
                        slider.ValueChanged += (s, e) => textbl.Text = e.NewValue.ToString();
                        slider.PointerMoved += (s, e) => SendMessage("SLIDER", slider.Value);

                        textbl.Text = "Connected...!";
                        slider.IsEnabled = true;
                    });
            });
        }

        private async void ValueChanged(object sender, ValueChangedEventArgs e)
        {
            await Dispatcher.RunAsync(
                CoreDispatcherPriority.Normal, () =>
                {
                    slider.Value = e.State;
                    textbl.Text = $"{e.Command}: {e.State}";
                });
        }

        private async void SendMessage(string command, double state)
        {
            try
            {
                await hub?.Invoke("newUpdate",
                    new object[] { command, state });
            }
            catch { }
        }

    }
}
