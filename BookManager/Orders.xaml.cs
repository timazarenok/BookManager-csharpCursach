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
using Excel = Microsoft.Office.Interop.Excel;


namespace BookManager
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public List<Order> orders = new List<Order>();
        public Orders()
        {
            InitializeComponent();
            SetOrders();
        }
        private void SetOrders()
        {
            DataTable dt = DB.Select("select * from Orders");
            orders = new List<Order>();
            foreach(DataRow dr in dt.Rows){
                orders.Add(new Order { ID = dr["id"].ToString(), Date = dr["date"].ToString(), Result = dr["result"].ToString() });
            }
            OrdersAll.ItemsSource = orders;
        }
        private void SetOrderProducts(Order order)
        {
            List<OrderProduct> orderProducts = new List<OrderProduct>();
            DataTable dt = DB.Select($"select [date], Books.[name], Books.[price] from Orders join OrdersBooks on OrdersBooks.id_order = Orders.id join Books on Books.id = id_book where Orders.id = {order.ID}");
            DataTable dt2 = DB.Select($"select Products.[name], Products.[price] from Orders join OrdersProducts on OrdersProducts.id_order = Orders.id join Products on Products.id = id_product where Orders.id = {order.ID}");
            if (dt.Rows.Count == 0)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    orderProducts.Add(new OrderProduct { Name = dr["name"].ToString(), Price = dr["price"].ToString() });
                }
            }
            else
            {
                foreach (DataRow dr in dt.Rows)
                {
                    orderProducts.Add(new OrderProduct { Name = dr["name"].ToString(), Price = dr["price"].ToString() });
                }
            }  
            OrderProducts.ItemsSource = orderProducts;
        }
        private void Orders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OrdersAll.SelectedItem = sender;
            Order order = (Order)OrdersAll.SelectedItem;
            SetOrderProducts(order);
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            Excel.Application ExcelApp = new Excel.Application();
            ExcelApp.Application.Workbooks.Add(Type.Missing);
            ExcelApp.Columns.ColumnWidth = 15;

            ExcelApp.Cells[1, 1] = "Дата";
            ExcelApp.Cells[1, 2] = "Результат";

            var list = OrdersAll.Items.OfType<Order>().ToList();

            for (int j = 0; j < list.Count; j++)
            {
                ExcelApp.Cells[j + 2, 1] = list[j].Date;
                ExcelApp.Cells[j + 2, 2] = list[j].Result;
            }
            ExcelApp.Visible = true;
        }

        private void SearchByDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime date = (DateTime)SearchByDate.SelectedDate;
            OrdersAll.ItemsSource = orders.FindAll(el => el.Date == date.ToString());
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            SetOrders();
        }
    }
}
