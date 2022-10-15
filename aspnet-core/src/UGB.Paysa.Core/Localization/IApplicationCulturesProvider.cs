using System.Globalization;

namespace UGB.Paysa.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}