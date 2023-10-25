using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Game2048.Pages
{
    /// <summary>
    /// Логика взаимодействия для MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Window
    {
        GamePage gamePage =new GamePage();
        public MenuPage()
        {
            InitializeComponent();
            Closing += MenuPage_Closing;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            gamePage.Show();
            Hide();
        }
       
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Спасибо за игру");
            Window.GetWindow(this)?.Close();
        }
        
        private void MenuPage_Closing(object sender, CancelEventArgs e)
        {
            gamePage.Close(); // Закрываем окно gamePage при закрытии MenuPage
        }
    }
}
