using System.Globalization;
using System.Net.Http.Json;
using Goodelivery.App.Models;
using Serilog;

namespace Goodelivery.App;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(args.Length > 3 ? args[3] : "logs/log.txt")
                .CreateLogger();
            Log.Information("Консольное приложение запущено...");
            Log.Information("Проверка аргументов...");

            var region = CheckRegion(args.Length > 0 ? args[0] : "");
            var firstDelivery = CheckFirstDelivery(args.Length > 1 ? args[1] : "");
            Console.WriteLine($"Первая доставка:\t\t\t\t\tРайон: {region}\tДата и время: {firstDelivery}");
            Log.Information($"Первая доставка:\t\t\t\t\tРайон: {region}\tДата и время: {firstDelivery}");

            var orderResponse = await SendRequestAsync($"{region}/{firstDelivery:yyyy-MM-dd HH:mm:ss}");
            Log.Information($"Отправлен запрос с параметрами: район {region}, дата и время первой доставки {firstDelivery}");
            if (orderResponse.IsSuccessStatusCode)
            {
                var orders = await orderResponse.Content.ReadFromJsonAsync<List<Order>>();
                if (orders is { Count: > 0 })
                {
                    Console.WriteLine($"Ожидаемые доставки: {orders.Count}");
                    Log.Information($"Ожидаемые доставки: {orders.Count}");
                    SaveResult(region, firstDelivery, orders, args.Length > 2 ? args[2] : null);
                }
            }
            else
            {
                Console.WriteLine(await orderResponse.Content.ReadAsStringAsync());
                Log.Warning(await orderResponse.Content.ReadAsStringAsync());
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Ошибка при отправке запроса на сервер: {e.Message}");
            Log.Fatal($"Ошибка при отправке запроса на сервер: {e.Message}");
        }
        finally
        {
            Console.ReadLine();
            Log.Information("Консольное приложение завершило работу...");
            await Log.CloseAndFlushAsync();
        }
    }

    static async Task<HttpResponseMessage> SendRequestAsync(string requestUrl, string baseUrl = "http://localhost:5075/orders/")
    {
        using var http = new HttpClient();
        return await http.GetAsync($"{baseUrl}{requestUrl}");
    }

    static void SaveResult(string region, DateTime firstDelivery, List<Order> orders, string? resultPath = null)
    {
        resultPath ??= $"{region}_{firstDelivery:yyyy-MM-dd_HH-mm-ss}.txt";

        try
        {
            using var stream = new StreamWriter(resultPath);

            foreach (var order in orders)
            {
                Console.WriteLine(order);
                stream.WriteLine(order.ToString());
            }

            Console.WriteLine($"Результаты записаны в файл {resultPath}");
            Log.Information($"Результаты записаны в файл {resultPath}");
        }
        catch (UnauthorizedAccessException e)
        {
            Console.WriteLine($"Ошибка с доступом к файлу {resultPath}: {e.Message}");
            Log.Fatal($"Ошибка с доступом к файлу {resultPath}: {e.Message}");
        }
        catch (IOException e)
        {
            Console.WriteLine($"Ошибка при работе с файлом {resultPath}: {e.Message}");
            Log.Fatal($"Ошибка при работе с файлом {resultPath}: {e.Message}");
        }
    }

    static string CheckRegion(string? region = null)
    {
        while (string.IsNullOrWhiteSpace(region))
        {
            Console.Write("Район доставки: ");
            region = Console.ReadLine();
        }

        return region.Trim();
    }

    static DateTime CheckFirstDelivery(string? input = null)
    {
        DateTime firstDelivery;
        var dateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        while (string.IsNullOrWhiteSpace(input) || !DateTime.TryParseExact(input.Trim(), dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out firstDelivery))
        {
            Console.Write($"Дата и время первой доставки ({dateTimeFormat}): ");
            input = Console.ReadLine();
        }

        return firstDelivery;
    }
}