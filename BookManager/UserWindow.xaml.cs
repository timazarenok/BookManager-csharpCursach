using BookManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace BookManager
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public bool first = true;
        public int id_order = -1;
        public int id_pr_or = DB.GetId("select top 1 * from OrdersProducts order by id desc");
        public int id_bk_or = DB.GetId("select top 1 * from OrdersBooks order by id desc");
        public UserWindow()
        {
            InitializeComponent();
            SetAllProducts();
            SetAllBooks();
            UpdateIDs();
        }
        private void SetAllProducts()
        {
            DataTable dt = DB.Select("select Products.id, Products.[name], [description], Categories.[name] as category, price from Products join Categories on Categories.id = Products.id_category");
            List<Product> products = new List<Product>();
            foreach(DataRow dr in dt.Rows)
            {
                products.Add(new Product {
                    ID = dr["id"].ToString(),
                    Name = dr["name"].ToString(),
                    Category = dr["category"].ToString(),
                    Description = dr["description"].ToString(),
                    Price = dr["price"].ToString()
                }); 
            }
            AllProducts.ItemsSource = products;
        }
        private void SetAllBooks()
        {
            DataTable dt = DB.Select("select Books.id, Books.[name], [description], BookCategories.[name] as category, price from Books join BookCategories on BookCategories.id = Books.id_category");
            List<Book> products = new List<Book>();
            foreach (DataRow dr in dt.Rows)
            {
                products.Add(new Book
                {
                    ID = dr["id"].ToString(),
                    Name = dr["name"].ToString(),
                    Category = dr["category"].ToString(),
                    Description = dr["description"].ToString(),
                    Price = dr["price"].ToString()
                });
            }
            AllBooks.ItemsSource = products;
        }

        private void SetAllInOrder()
        {
            DataTable dt = DB.Select($"SELECT number, name, price FROM OrdersProducts join Products on Products.id = id_product WHERE id_order = {id_order} ");
            List<OrderProduct> products = new List<OrderProduct>();
            foreach(DataRow dr in dt.Rows)
            {
                products.Add(new OrderProduct { 
                    ID = dr["number"].ToString(),
                    Name = dr["name"].ToString(),
                    Price = dr["price"].ToString()
                });
            }
            Cart.ItemsSource = products;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(first)
            {
                if(DB.Command($"insert into Orders values({DB.UserID}, (SELECT getdate()), 0)"))
                {
                    first = false;
                    id_order = DB.GetId($"select top 1 * from Orders where id_user = {DB.UserID} order by id desc");
                }
                else
                {
                    MessageBox.Show("У нас неполадки в системе попробуйте позже");
                    return;
                }
            }
            Product selected = (Product)AllProducts.SelectedItem;
            if (DB.Command($"insert into OrdersProducts values({id_pr_or + 1}, {id_order}, {selected.ID})"))
            {
                SetAllInOrder();
                UpdateIDs();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так попробуйте позже");
            }
        }
        private void UpdateIDs()
        {
            id_pr_or = DB.GetId("select top 1 * from OrdersProducts order by id desc");
            id_bk_or = DB.GetId("select top 1 * from OrdersBooks order by id desc");
            MessageBox.Show(id_pr_or + "");
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            if (first)
            {
                if (DB.Command($"insert into Orders values({DB.UserID}, (select convert(varchar(10),(select getdate()), 120)), 0)"))
                {
                    first = false;
                    id_order = DB.GetId($"select top 1 * from Orders where id_user = {DB.UserID} order by id desc");
                }
                else
                {
                    MessageBox.Show("У нас неполадки в системе попробуйте позже");
                    return;
                }
            }
            Book selected = (Book)AllBooks.SelectedItem;
            if (DB.Command($"insert into OrdersBooks values({id_bk_or + 1}, {id_order}, {selected.ID})"))
            {
                SetAllInOrder();
                UpdateIDs();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так попробуйте позже");
            }
        }

        private void AllBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AllBooks.SelectedItem = sender;
        }

        private void AllProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AllProducts.SelectedItem = sender;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string id = IDDelete.Text;
            if (DB.Command($"delete from OrdersProducts where number = {id}"))
            {
                SetAllInOrder();
            }
        }
    }
}
