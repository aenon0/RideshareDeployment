using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Rideshare.Application.Contracts.Infrastructure;
public class SmsService : ISmsService
{
    private readonly HttpClient _client;
    private readonly string _smsUrl = Environment.GetEnvironmentVariable("SMS_URL");
    private readonly string _smsToken = Environment.GetEnvironmentVariable("SMS_TOKEN");

    public SmsService(HttpClient client)
    {
        _client = client;
    }

    public async Task SendSMS(string phone, string msg)
    {
        var data = new
        {
            msg = msg,
            phone = phone,
            token = _smsToken
        };

        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        try
        {
            var response = await _client.PostAsync(_smsUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
            Console.WriteLine("message sent");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}