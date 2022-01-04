using Microsoft.AspNetCore.SignalR;
using Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddSignalR();

builder.Services.AddCors(options =>
                        {
                            options.AddDefaultPolicy( builder =>
                            {
                                builder.AllowAnyOrigin()
                                       .AllowAnyHeader()
                                       .AllowAnyMethod();
                            });
                        });

var application = builder.Build();

application.UseSwagger()
           .UseSwaggerUI()
           .UseHttpsRedirection();

application.UseCors();
application.MapHub<CryptoHub>("/crypto-hub");

application.Run();

class CryptoHub : Hub
{
    Crypto[] cryptos = new[]
    {
        new Crypto("BTC", "Bitcoin", 200_000, 0, DateTime.Now ),
        new Crypto("ETH", "Ethereum", 18_000, 0, DateTime.Now ),
        new Crypto("BNB", "Binance", 5_000, 0, DateTime.Now ),
        new Crypto("MANA", "Decentraland", 20, 0, DateTime.Now ),
        new Crypto("SAND", "The Sandbox", 10, 0, DateTime.Now ),
    };

    public async IAsyncEnumerable<Crypto> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            yield return ChangePrice(cryptos[Random.Shared.Next(cryptos.Length)]);
            await Task.Delay(1000, cancellationToken);
        }
    }

    private static Crypto ChangePrice(Crypto crypto)
    {
        crypto.Price *= Random.Shared.NextDouble();

        return crypto;
    }
}