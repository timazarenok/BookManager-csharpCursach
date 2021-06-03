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
using Excel = Microsoft.Office.Interop.Excel;

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
            SetAuthors();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        public void SetBooks()
        {
            DataTable dt = DB.Select("select Books.[name], BookCategories.[name] as category, Authors.full_name as author, [description], price, amount from Books JOIN BookCategories on BookCategories.id = id_category join Authors on Authors.id = id_author");
            List<Book> books = new List<Book>();
            foreach (DataRow dr in dt.Rows)
            {
                books.Add(new Book() { 
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
        public void SetAuthors()
        {
            DataTable dt = DB.Select("select * from Authors");
            List<string> authors = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                authors.Add(dr["full_name"].ToString());
            }
            Author.ItemsSource = authors;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            int category_id = DB.GetId($"select id from BookCategories where [name] = '{Categories.SelectedItem}'");
            int author_id = DB.GetId($"select id from Authors where [full_name]='{Author.SelectedItem}'");
            if (DB.Command($"insert into Books values({category_id}, {author_id}, '{Name.Text}', '{Description.Text}', {Price.Text}, {Amount.Text})"))
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

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "Название";
            ExcelApp.Cells[1, 2] = "Категория";
            ExcelApp.Cells[1, 3] = "Автор";
            ExcelApp.Cells[1, 4] = "Описание";
            ExcelApp.Cells[1, 5] = "Цена";
            ExcelApp.Cells[1, 6] = "Кол-во";

            var list = Table.Items.OfType<Book>().ToList();

            for (int j = 0; j < list.Count; j++)
            {
                ExcelApp.Cells[j + 2, 1] = list[j].Name;
                ExcelApp.Cells[j + 2, 2] = list[j].Category;
                ExcelApp.Cells[j + 2, 3] = list[j].Author;
                ExcelApp.Cells[j + 2, 4] = list[j].Description;
                ExcelApp.Cells[j + 2, 5] = list[j].Price;
                ExcelApp.Cells[j + 2, 6] = list[j].Amount;
            }
            ExcelApp.Visible = true;
        }
    }
}
