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
using Excel = Microsoft.Office.Interop.Excel;

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
            DataTable dt = DB.Select("select Products.[name], Categories.[name] as category, [description], price, amount from Products JOIN Categories on Categories.id = id_category");
            List<Product> books = new List<Product>();
            foreach (DataRow dr in dt.Rows)
            {
                books.Add(new Product()
                {
                    Name = dr["name"].ToString(),
                    Category = dr["category"].ToString(),
                    Description = dr["description"].ToString(),
                    Price = dr["price"].ToString(),
                    Amount = dr["amount"].ToString()
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
            if (DB.Command($"insert into Products values({category_id}, '{Name.Text}', '{Description.Text}', {Price.Text}, {Amount.Text})"))
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

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "Название";
            ExcelApp.Cells[1, 2] = "Категория";
            ExcelApp.Cells[1, 3] = "Описание";
            ExcelApp.Cells[1, 4] = "Цена";
            ExcelApp.Cells[1, 5] = "Кол-во";

            var list = Table.Items.OfType<Product>().ToList();

            for (int j = 0; j < list.Count; j++)
            {
                ExcelApp.Cells[j + 2, 1] = list[j].Name;
                ExcelApp.Cells[j + 2, 2] = list[j].Category;
                ExcelApp.Cells[j + 2, 3] = list[j].Description;
                ExcelApp.Cells[j + 2, 4] = list[j].Price;
                ExcelApp.Cells[j + 2, 5] = list[j].Amount;
            }
            ExcelApp.Visible = true;
        }
    }
}
