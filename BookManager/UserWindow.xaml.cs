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
        public List<Product> products = new List<Product>();
        public UserWindow()
        {
            InitializeComponent();
            SetAllProducts();
            UpdateIDs();
            SetSortParams();
        }
        private void SetSortParams()
        {
            List<string> sortparams = new List<string>();
            sortparams.Add("Мин");
            sortparams.Add("Макс");
            sortparams.Add(" ");
            Sort.ItemsSource = sortparams;
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = Search.Text;
            AllProducts.ItemsSource = products.FindAll(item => item.Name.Contains(text));
        }
        private void SetAllProducts()
        {
            DataTable dt = DB.Select("select Products.id, Products.[name], [description], Categories.[name] as category, price from Products join Categories on Categories.id = Products.id_category");
            products = new List<Product>();
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
                DB.Command($"update Orders set result+={ChangeComa(selected.Price)} where id = {id_order}");
            }
            else
            {
                MessageBox.Show("Что-то пошло не так попробуйте позже");
            }
        }
        private void UpdateIDs()
        {
            id_pr_or = DB.GetId("select top 1 * from OrdersProducts order by id desc");
        }
        private void AllProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AllProducts.SelectedItem = sender;
        }
        private string ChangeComa(string text) => text.Replace(',', '.');

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string id = IDDelete.Text;
            List<string> dt = DB.GetDataOneAttribute($"select id_product from OrdersProducts where number={id}", "id_product");
            List<string> product = DB.GetDataOneAttribute($"select price from Products where id = {dt[0]}", "price");
            if (DB.Command($"delete from OrdersProducts where number = {id}"))
            {
                SetAllInOrder();
                DB.Command($"update Orders set result-={ChangeComa(product[0])} where id = {id_order}");
            }
        }
        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Sort.SelectedItem)
            {
                case "Макс":
                    {
                        AllProducts.ItemsSource = products.OrderByDescending(el => Convert.ToDouble(el.Price)).ToList();
                        break;
                    }
                case "Мин":
                    {
                        AllProducts.ItemsSource = products.OrderBy(el => Convert.ToDouble(el.Price)).ToList();
                        break;
                    }
                case " ":
                    {
                        AllProducts.ItemsSource = products;
                        break;
                    }
            }
        }

        private void BookShopButton_Click(object sender, RoutedEventArgs e)
        {
            BookShop shop = new BookShop();
            shop.Show();
            Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }

        private void CloseOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ваш заказ отправлен на сервер");
            LoginWindow login = new LoginWindow();
            login.Show();
            Close();
        }
    }
}
