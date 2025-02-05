namespace Stocks
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Views.SearchPage), typeof(Views.SearchPage));
        }
    }
}
