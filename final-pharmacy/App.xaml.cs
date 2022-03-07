using final_pharmacy.DbHandler.DbServices;
using final_pharmacy.ViewModels;
using final_pharmacy.Views;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Windows;

namespace final_pharmacy
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseFacade DbFacade = new DatabaseFacade(new DataBaseContext());
            DbFacade.EnsureCreated();
            using(AuthLogDataContext context = new AuthLogDataContext())
            {
                if (context.IsLogedIn())
                {
                    Window window = new MainView();
                    window.DataContext = new MainViewModel();
                    window.Show();
                }
                else
                {
                    Window window = new LoginView();
                    window.DataContext = new LoginViewModel();
                    window.Show();
                }
            }
            base.OnStartup(e);
        }
    }
}
