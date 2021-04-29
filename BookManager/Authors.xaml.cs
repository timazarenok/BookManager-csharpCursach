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
    /// Логика взаимодействия для Authors.xaml
    /// </summary>
    public partial class Authors : Window
    {
        public Authors()
        {
            InitializeComponent();
            SetAuthors();
        }

        public void SetAuthors()
        {
            DataTable dt = DB.Select("select * from Authors");
            List<NameClass> authors = new List<NameClass>();
            foreach (DataRow dr in dt.Rows)
            {
                authors.Add(new NameClass() { Name = dr["full_name"].ToString() });
            }
            Table.ItemsSource = authors;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (DB.Command($"insert into Authors values('{Name.Text}')"))
            {
                SetAuthors();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DB.Command($"delete from Authors where [full_name]='{Name.Text}'"))
            {
                SetAuthors();
            }
        }
    }
}
