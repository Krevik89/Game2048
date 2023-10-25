using Game2048.Pages;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Game2048
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Создание экземпляра окна "NewMainWindow"
            GamePage menuPage = new GamePage();

            
            // Отображение главного окна
            menuPage.Show();
        }
    }
}
