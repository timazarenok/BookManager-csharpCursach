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
        public List<OrderProduct> cart_items = new List<OrderProduct>();
        public BookShop()
        {
            InitializeComponent();
            SetAllBooks();
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
            AllBooks.ItemsSource = products.Where(item => item.Name.Contains(text) && item.Author.Contains(text)).ToList();
        }
        private void SetAllBooks()
        {
            DataTable dt = DB.Select("select Books.id, Books.[name], Authors.full_name, [description], BookCategories.[name] as category, price from Books join BookCategories on BookCategories.id = Books.id_category join Authors on Authors.id = Books.id_author");
            products = new List<Book>();
            foreach (DataRow dr in dt.Rows)
            {
                products.Add(new Book
                {
                    ID = dr["id"].ToString(),
                    Name = dr["name"].ToString(),
                    Author = dr["full_name"].ToString(),
                    Category = dr["category"].ToString(),
                    Description = dr["description"].ToString(),
                    Price = dr["price"].ToString()
                });
            }
            AllBooks.ItemsSource = products;
        }
        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            Book selected = (Book)AllBooks.SelectedItem;
            DataTable dt = DB.Select($"select amount from Books where id={selected.ID}");
            int amount = Convert.ToInt32(dt.Rows[0]["amount"]);
            if (amount-1 > 0)
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
                if (DB.Command($"insert into OrdersBooks values({id_bk_or + 1}, {id_order}, {selected.ID})"))
                {
                    DB.Command($"Update Books set amount=amount-1 where id={selected.ID}");
                    SetAllInOrder();
                    UpdateIDs();
                    DB.Command($"update Orders set result+={ChangeComa(selected.Price)} where id = {id_order}");
                }
                else
                {
                    MessageBox.Show("Что-то пошло не так попробуйте позже");
                }
            }
            else
            {
                MessageBox.Show("Этой книги на складе больше не осталось \n Извините за неудобства");
            }
            
        }
        private string ChangeComa(string text) => text.Replace(',', '.');
        private void UpdateIDs()
        {
            id_bk_or = DB.GetId("select top 1 * from OrdersBooks order by id desc");
        }
        private void SetAllInOrder()
        {
            DataTable dt = DB.Select($"SELECT Books.id, number, name, price FROM OrdersBooks join Books on Books.id = id_book WHERE id_order = {id_order} ");
            cart_items = new List<OrderProduct>();
            foreach (DataRow dr in dt.Rows)
            {
                cart_items.Add(new OrderProduct
                {
                    ID = dr["number"].ToString(),
                    BookId = dr["id"].ToString(),
                    Name = dr["name"].ToString(),
                    Price = dr["price"].ToString()
                });
            }
            Cart.ItemsSource = cart_items;
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
            foreach(OrderProduct product in cart_items)
            {
                DB.Command($"Update Books set amount=amount+1 where id = {product.BookId}");
            }
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

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (Sort.SelectedItem)
            {
                case "Макс":
                {
                        AllBooks.ItemsSource = products.OrderByDescending(el => Convert.ToDouble(el.Price)).ToList();
                        break;
                }
                case "Мин":
                {
                        AllBooks.ItemsSource = products.OrderBy(el => Convert.ToDouble(el.Price)).ToList();
                        break;
                }
                case " ":
                {
                        AllBooks.ItemsSource = products;
                        break;
                }
            }
        }
    }
}
