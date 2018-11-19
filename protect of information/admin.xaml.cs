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
using System.IO;

namespace protect_of_information
{
    /// <summary>
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        public admin()
        {
            InitializeComponent();
        }

        string way = @"database.txt";


        private void updateB_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_Loaded(sender,e);
        }

        private void saveB_Click(object sender, RoutedEventArgs e)
        {
            string line;
            string[] data;
            int flag = 5;
            int res;
            for (int i = 0; i<myDataGrid.Items.Count; i++)
            {
                line = myDataGrid.Items[i].ToString();
                data = line.Split('|');
                if (!Int32.TryParse(data[4], out res))
                {
                    flag = 4;
                    break;
                }
                if (data[0].IndexOf('|') != -1)
                {
                    flag = 0;
                    break;
                }
                if(data[1].Length < Convert.ToInt32(data[4]) && data[3]=="+" && data[1].IndexOf('|') != -1)
                {
                    flag = 1;
                    break;
                }
                if (data[2] != "+" && data[2] != "-")
                {
                    flag = 2;
                    break;
                }
                if (data[3] != "+" && data[3] != "-")
                {
                    flag = 3;
                    break;
                }  
            }
            if (flag == 5)
            {
                StreamWriter myWriter = new StreamWriter(way, false);
                for (int i = 0; i < myDataGrid.Items.Count; i++)
                {
                    line = myDataGrid.Items[i].ToString();
                    myWriter.WriteLine(line);
                }
                myWriter.Close();
            }
            else
            {
                MessageBox.Show("Проверьте поля!", "Ошибка!");
            }
        }

        class MyTable
        {
            public MyTable(string login, string password, string ban, string limit, string minLength)
            {
                this.login = login;
                this.password = password;
                this.ban = ban;
                this.limit = limit;
                this.minLength = minLength;
            }

            public override string ToString()
            {
                return login + "|" + password + "|" + ban + "|" + limit + "|" + minLength;
            }

            public string login { get; set; }
            public string password { get; set; }
            public string ban { get; set; }
            public string limit { get; set; }
            public string minLength { get; set; }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            StreamReader myReader = new StreamReader(way);

            string line;
            string[] data;
            List<MyTable> result = new List<MyTable>();

            line = myReader.ReadLine();
            do
            {
                data = line.Split('|');
                result.Add(new MyTable(data[0], data[1], data[2], data[3], data[4]));
                line = myReader.ReadLine();
            } while (line != null);
            myDataGrid.ItemsSource = result;
            myReader.Close();
        }

        private void newB_Click(object sender, RoutedEventArgs e)
        {
            add_user window = new add_user();
            window.ShowDialog();
            
        }

        private void myDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void changePwd_Click(object sender, RoutedEventArgs e)
        {
            StreamReader myReader = new StreamReader(way);

            string line;
            string[] data;
            bool flag = false;

            line = myReader.ReadLine();
            do
            {
                data = line.Split('|');
                if(data[0]=="admin")
                {
                    flag = true;
                    break;
                }
                line = myReader.ReadLine();
            } while (line != null);

            myReader.Close();
            if (flag)
            {
                user user = new user(data, myDataGrid);
                user.ShowDialog();
            }
            else
            {
                MessageBox.Show("Где админ????", "Ошибка!");
            }
            
        }
    }

}
