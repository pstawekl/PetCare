using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class WebSocketNotificationHandler
{
    private static List<WebSocket> _connectedSockets = new List<WebSocket>();

    public async Task HandleWebSocket(HttpContext context)
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            _connectedSockets.Add(webSocket);

            try
            {
                await Handle(webSocket);
            }
            finally
            {
                _connectedSockets.Remove(webSocket);
            }
        }
    }

    public async Task SendNotificationToAll(string message)
    {
        var buffer = Encoding.UTF8.GetBytes(message);
        var arraySegment = new ArraySegment<byte>(buffer);

        foreach (var socket in _connectedSockets.ToList())
        {
            if (socket.State == WebSocketState.Open)
            {
                await socket.SendAsync(arraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }

    private async Task Handle(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        
        while (webSocket.State == WebSocketState.Open)
        {
            var result = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, 
                    string.Empty, CancellationToken.None);
            }
        }
    }
}
