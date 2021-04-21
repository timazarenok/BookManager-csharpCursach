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
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Data;
using BookManager.Models;

namespace BookManager
{
    /// <summary>
    /// Логика взаимодействия для Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        public Books()
        {
            InitializeComponent();
            SetCategories();
            SetBooks();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void SetBooks()
        {
            DataTable dt = DB.Select("select Books.[name], BookCategories.[name] as category, author, [description], price from Books JOIN BookCategories on BookCategories.id = id_category");
            List<Book> books = new List<Book>();
            foreach (DataRow dr in dt.Rows)
            {
                books.Add(new Book() { Name = dr["name"].ToString(), Category = dr["category"].ToString(),
                    Author = dr["author"].ToString(), Description = dr["description"].ToString(), Price = dr["price"].ToString() });
            }
            Table.ItemsSource = books;
        }
        public void SetCategories()
        {
            DataTable dt = DB.Select("select * from BookCategories");
            List<string> categories = new List<string>();
            foreach(DataRow dr in dt.Rows)
            {
                categories.Add(dr["name"].ToString());
            }
            Categories.ItemsSource = categories;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int category_id = DB.GetId($"select id from BookCategories where [name] = '{Categories.SelectedItem}'");
            if (DB.Command($"insert into Books values({category_id}, '{Name.Text}', '{Author.Text}', '{Description.Text}', {Price.Text})"))
            {
                SetBooks();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DB.Command($"delete from Books where [name]='{Name.Text}'"))
            {
                SetBooks();
            }
        }
    }
}
