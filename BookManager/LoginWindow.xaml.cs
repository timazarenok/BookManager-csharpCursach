using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookManager
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RegistrationWindow reg = new RegistrationWindow();
            reg.Show();
            Close();
        }
        public bool RegexLogin(string login)
        {
            return new Regex("[A-Za-z0-9]{4,15}").IsMatch(login);
        }
        public bool RegexPassword(string password)
        {
            return new Regex("[A-Za-z0-9]{8,20}").IsMatch(password);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (RegexLogin(LoginBox.Text))
            {
                if (RegexPassword(Password.Password))
                {
                    DataTable find = DB.Select($"select * from [Users] where login='{LoginBox.Text}' and password='{Password.Password}'");
                    if (find.Rows.Count > 0)
                    {
                        MainWindow mw = new MainWindow();
                        mw.Show();
                        Close();
                        MessageBox.Show("Пользователь авторизовался");
                    }
                    else MessageBox.Show("Такого пользователя не существует");
                }
                else MessageBox.Show("Введите пароль");
            }
            else MessageBox.Show("Введите логин");
        }
    }
}
