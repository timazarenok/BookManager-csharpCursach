using BookManager.Models;
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
    /// Логика взаимодействия для PereozenkaWindow.xaml
    /// </summary>
    public partial class PereozenkaWindow : Window
    {
        public PereozenkaWindow()
        {
            InitializeComponent();
            SetBooks();
        }
        public void SetBooks()
        {
            DataTable dt = DB.Select("select Books.[name], BookCategories.[name] as category, Authors.full_name as author, [description], price, amount from Books JOIN BookCategories on BookCategories.id = id_category join Authors on Authors.id = id_author");
            List<Book> books = new List<Book>();
            foreach (DataRow dr in dt.Rows)
            {
                books.Add(new Book()
                {
                    Name = dr["name"].ToString(),
                    Category = dr["category"].ToString(),
                    Author = dr["author"].ToString(),
                    Description = dr["description"].ToString(),
                    Price = dr["price"].ToString(),
                    Amount = dr["amount"].ToString()
                });
            }
            Table.ItemsSource = books;
        }

        private void NewPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex reg = new Regex("[^0-9]+");
            e.Handled = reg.IsMatch(e.Text);
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if(ID.Text.Length > 0 && NewPrice.Text.Length > 0)
            {
                if (DB.Command($"Update Books set price={NewPrice.Text} where id={ID.Text}"))
                {
                    MessageBox.Show("Успешно обновалено");
                    SetBooks();
                }
            }
        }
    }
}
