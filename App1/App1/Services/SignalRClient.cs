using Microsoft.AspNet.SignalR.Client;
using System;

namespace App1
{
    public class SignalRClient
    {
        private IHubProxy _hub;
        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        public IHubProxy SignalRHub { get { return _hub; } }

        public async void InitializeSignalR()
        {
            var hubConnection = new HubConnection("http://[replace with your web app].azurewebsites.net/signalr");
            _hub = hubConnection.CreateHubProxy("updater");

            _hub.On<string, double>("newUpdate",
                (command, state) => ValueChanged?.Invoke(this, new ValueChangedEventArgs(command, state)));

            await hubConnection.Start();

        }
    }

}
