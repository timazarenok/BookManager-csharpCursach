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
    /// Логика взаимодействия для Products.xaml
    /// </summary>
    public partial class Products : Window
    {
        public Products()
        {
            InitializeComponent();
            SetCategories();
            SetProducts();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void SetProducts()
        {
            DataTable dt = DB.Select("select Products.[name], Categories.[name] as category , [description], price from Products JOIN Categories on Categories.id = id_category");
            List<Product> books = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                books.Add(new Product()
                {
                    Name = dr["name"].ToString(),
                    Category = dr["category"].ToString(),
                    Description = dr["description"].ToString(),
                    Price = dr["price"].ToString()
                });
            }
            Table.ItemsSource = books;
        }
        public void SetCategories()
        {
            DataTable dt = DB.Select("select * from Categories");
            List<string> categories = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                categories.Add(dr["name"].ToString());
            }
            Categories.ItemsSource = categories;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int category_id = DB.GetId($"select id from Categories where [name] = '{Categories.SelectedItem}'");
            if (DB.Command($"insert into Products values({category_id}, '{Name.Text}', '{Description.Text}', {Price.Text})"))
            {
                SetProducts();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DB.Command($"delete from Products where [name]='{Name.Text}'"))
            {
                SetProducts();
            }
        }
    }
}
