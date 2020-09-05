using Kavenegar;
using Kavenegar.Exceptions;
using System;
using System.Threading.Tasks;

namespace WebFramework.Services
{
    public class SmsService
    {
        static public async Task<bool> SendSms(string phoneNumber, string message)
        {
            try
            {
                var api = new KavenegarApi("6733627055695371726A4B384337657057474333726B34304E6765706F444A784D726A4F634E52762B43633D");
                var result = api.Send("1000596446", phoneNumber, message);
                return true;
            }
            catch (ApiException ex)
            {
                // در صورتی که خروجی وب سرویس 200 نباشد این خطارخ می دهد.
                Console.Write("Message : " + ex.Message);
                return false;
            }
            catch (HttpException ex)
            {
                // در زمانی که مشکلی در برقرای ارتباط با وب سرویس وجود داشته باشد این خطا رخ می دهد
                Console.Write("Message : " + ex.Message);
                return false;
            }
        }
    }
}
