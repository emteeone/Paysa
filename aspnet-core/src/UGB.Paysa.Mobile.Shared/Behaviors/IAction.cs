using Xamarin.Forms.Internals;

namespace UGB.Paysa.Behaviors
{
    [Preserve(AllMembers = true)]
    public interface IAction
    {
        bool Execute(object sender, object parameter);
    }
}