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
                var api = new KavenegarApi("73393651453634306A70506B56466376546F73745A6A3148704E4548534459695571706942534756754E413D");
                var result = api.Send("10008663", phoneNumber, message);
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
