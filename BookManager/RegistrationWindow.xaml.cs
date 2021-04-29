using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
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
            DataTable find = DB.Select($"select * from [Users] where login='{LoginBox.Text}' and password='{Password.Password}'");
            if (find.Rows.Count > 0)
            {
                MessageBox.Show("Пользователь с таким именем уже существует");
            }
            else
            {
                if (RegexLogin(LoginBox.Text))
                {
                    if (RegexPassword(Password.Password))
                    {
                        if (RepeatPassword.Password.Equals(Password.Password))
                        {
                            try
                            {
                                DB.Command($"insert into [Users] values('{LoginBox.Text}', '{Password.Password}')");
                                MessageBox.Show("Успешно создан");
                                UserWindow uw = new UserWindow();
                                uw.Show();
                                Close();
                            }
                            catch (Exception error)
                            {
                                MessageBox.Show(error.Message);
                            }
                        }
                        else MessageBox.Show("Пароли не совпадают");
                    }
                    else MessageBox.Show("Пароль обязан быть 8-20 символовв");
                }
                else MessageBox.Show("Логин обязан быть 4-15 символов");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindow lg = new LoginWindow();
            lg.Show();
            Close();
        }
    }
}
