using Ejercicio3PlatziFakeStore.ViewModels;
using Ejercicio3PlatziFakeStore.Views;
using Microsoft.Extensions.Logging;
using UraniumUI;
namespace Ejercicio3PlatziFakeStore
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Montserrat-Thin.ttf", "M");
                    fonts.AddFont("Montserrat-Regular.ttf", "A");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            
            return builder.Build();
        }
    }
}
