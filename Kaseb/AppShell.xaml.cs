using Kaseb.Services;

namespace Kaseb
{
    public partial class AppShell : Shell
    {
        
        public AppShell()
        {
            
            InitializeComponent();
            PageLoader.TheParent = new Container();
            MainShellContent.Content=PageLoader.TheParent;
            TheShell.FlyoutBehavior = FlyoutBehavior.Flyout;
            TheShell.FlyoutBehavior = FlyoutBehavior.Disabled;
        }
    }
}
