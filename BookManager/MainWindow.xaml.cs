using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BooksCategories_Click(object sender, RoutedEventArgs e)
        {
            BookCategories book = new BookCategories();
            book.Show();
        }

        private void ProductsCategories_Click(object sender, RoutedEventArgs e)
        {
            Categories categories = new Categories();
            categories.Show();
        }

        private void Books_Click(object sender, RoutedEventArgs e)
        {
            Books window = new Books();
            window.Show();
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            Products window = new Products();
            window.Show();
        }

        private void Authors_Click(object sender, RoutedEventArgs e)
        {
            Authors window = new Authors();
            window.Show();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            Orders window = new Orders();
            window.Show();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }
    }
}
