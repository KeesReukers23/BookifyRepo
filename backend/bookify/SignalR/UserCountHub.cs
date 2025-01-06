using Microsoft.AspNetCore.SignalR;

namespace bookifyWEBApi.SignalR
{
    public class UserCountHub : Hub
    {
        private static int _userCount = 0;

        public override async Task OnConnectedAsync()
        {
            Interlocked.Increment(ref _userCount); //Interlocked zodat niet meerdere threads tegelijkertijd de usercount kunnen aanpassen, waardoor verkeerde waardes kunnen ontstaan
            await Clients.All.SendAsync("UpdateUserCount", _userCount);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Interlocked.Decrement(ref _userCount);
            await Clients.All.SendAsync("UpdateUserCount", _userCount);
            await base.OnDisconnectedAsync(exception);
        }
    }
}