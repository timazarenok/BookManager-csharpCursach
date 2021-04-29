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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
            SetOrders();
        }
        private void SetOrders()
        {
            DataTable dt = DB.Select("select * from Orders");
            List<Order> orders = new List<Order>();
            foreach(DataRow dr in dt.Rows){
                orders.Add(new Order { ID = dr["id"].ToString(), Date = dr["date"].ToString(), Result = dr["result"].ToString() });
            }
            OrdersAll.ItemsSource = orders;
        }
        private void SetOrderProducts(Order order)
        {
            DataTable dt = DB.Select($"select [date], Books.[name], Books.[price], OrdersBooks.amount from Orders join OrdersBooks on OrdersBooks.id_order = Orders.id join Books on Books.id = id_book where Order.id = {order.ID}");
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            foreach (DataRow dr in dt.Rows)
            {
                orderProducts.Add(new OrderProduct { Name = dr["name"].ToString(), Price = dr["price"].ToString()});
            }
            DataTable dt2 = DB.Select("select [date], Products.[name], Products.[price], OrdersProducts.amount from Orders join OrdersProducts on OrdersProducts.id_order = Orders.id join Products on Products.id = id_product");
            foreach (DataRow dr in dt2.Rows)
            {
                orderProducts.Add(new OrderProduct { Name = dr["name"].ToString(), Price = dr["price"].ToString()});
            }
            OrderProducts.ItemsSource = orderProducts;
        }
        private void Orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Order order = (Order)sender;
            SetOrderProducts(order);
        }
    }
}
