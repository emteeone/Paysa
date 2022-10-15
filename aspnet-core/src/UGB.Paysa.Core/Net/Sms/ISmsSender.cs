using System.Threading.Tasks;

namespace UGB.Paysa.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}