namespace Rideshare.Application.Contracts.Infrastructure;

public interface ISmsService 
{
     public Task SendSMS(string phone, string msg);
}
