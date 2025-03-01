using Stocks.Services;

namespace Stocks
{
    public partial class App : Application
    {
        public static IDataLoader DataLoader;

        public App(System.IServiceProvider services)
        {
            DataLoader = services.GetService<IDataLoader>();

            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}