using System.Threading.Tasks;
using UGB.Paysa.Views;
using Xamarin.Forms;

namespace UGB.Paysa.Services.Modal
{
    public interface IModalService
    {
        Task ShowModalAsync(Page page);

        Task ShowModalAsync<TView>(object navigationParameter) where TView : IXamarinView;

        Task<Page> CloseModalAsync();
    }
}
