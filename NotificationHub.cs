using Microsoft.AspNetCore.SignalR;

// Hub do obsługi powiadomień
public class NotificationHub : Hub
{
    // Metoda wysyłająca powiadomienie do wszystkich klientów
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}
