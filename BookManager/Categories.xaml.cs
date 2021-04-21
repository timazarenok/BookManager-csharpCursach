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
    /// Логика взаимодействия для Categories.xaml
    /// </summary>
    public partial class Categories : Window
    {
        public Categories()
        {
            InitializeComponent();
            SetCategories();
        }
        public void SetCategories()
        {
            DataTable dt = DB.Select("select * from Categories");
            List<NameClass> categories = new List<NameClass>();
            foreach (DataRow dr in dt.Rows)
            {
                categories.Add(new NameClass() { Name = dr["name"].ToString() });
            }
            Table.ItemsSource = categories;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (DB.Command($"insert into Categories values('{Name.Text}')"))
            {
                SetCategories();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DB.Command($"delete from Categories where [name]='{Name.Text}'"))
            {
                SetCategories();
            }
        }
    }
}
