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
    /// Логика взаимодействия для BookShop.xaml
    /// </summary>
    public partial class BookShop : Window
    {
        public bool first = true;
        public int id_order = -1;
        public int id_bk_or = DB.GetId("select top 1 * from OrdersBooks order by id desc");
        public List<Book> products = new List<Book>();
        public BookShop()
        {
            InitializeComponent();
            SetAllBooks();
        }
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = Search.Text;
            AllBooks.ItemsSource = products.FindAll(item => item.Name.Contains(text));
        }
        private void SetAllBooks()
        {
            DataTable dt = DB.Select("select Books.id, Books.[name], [description], BookCategories.[name] as category, price from Books join BookCategories on BookCategories.id = Books.id_category");
            products = new List<Book>();
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
                DB.Command($"update Orders set result+={ChangeComa(selected.Price)} where id = {id_order}");
            }
            else
            {
                MessageBox.Show("Что-то пошло не так попробуйте позже");
            }
        }
        private string ChangeComa(string text) => text.Replace(',', '.');
        private void UpdateIDs()
        {
            id_bk_or = DB.GetId("select top 1 * from OrdersBooks order by id desc");
        }
        private void SetAllInOrder()
        {
            DataTable dt = DB.Select($"SELECT number, name, price FROM OrdersBooks join Books on Books.id = id_book WHERE id_order = {id_order} ");
            List<OrderProduct> products = new List<OrderProduct>();
            foreach (DataRow dr in dt.Rows)
            {
                products.Add(new OrderProduct
                {
                    ID = dr["number"].ToString(),
                    Name = dr["name"].ToString(),
                    Price = dr["price"].ToString()
                });
            }
            Cart.ItemsSource = products;
        }
        private void AllBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            AllBooks.SelectedItem = sender;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string id = IDDelete.Text;
            List<string> dt = DB.GetDataOneAttribute($"select id_book from OrdersBooks where number={id}", "id_book");
            List<string> book = DB.GetDataOneAttribute($"select price from Books where id = {dt[0]}", "price");
            if (DB.Command($"delete from OrdersBooks where number = {id}"))
            {
                SetAllInOrder();
                DB.Command($"update Orders set result-={ChangeComa(book[0])} where id = {id_order}");
            }
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
