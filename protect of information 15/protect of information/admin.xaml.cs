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
using System.Security.Cryptography;

namespace protect_of_information
{
    /// <summary>
    /// Логика взаимодействия для admin.xaml
    /// </summary>
    public partial class admin : Window
    {
        public string myCodeEnc;
        public admin(string codeE)
        {
            myCodeEnc = codeE;
            InitializeComponent();
        }

        string way = @"database.txt";


        private void updateB_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_Loaded(sender,e);
        }

        MainWindow cD = new MainWindow();
        private void saveB_Click(object sender, RoutedEventArgs e)
        {
            string line;
            string[] data;
            int flag = 5;
            for (int i = 0; i<myDataGrid.Items.Count; i++)
            {
                line = myDataGrid.Items[i].ToString();
                data = line.Split('|');
                if (data[0].IndexOf('|') != -1)
                {
                    flag = 0;
                    break;
                }
                
                if(cD.checkData(data[1]) && data[3]=="+" && data[1].IndexOf('|') != -1)
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
            cD.Close();
            if (flag == 5)
            {
                StreamWriter myWriter = new StreamWriter(way, false);
                for (int i = 0; i < myDataGrid.Items.Count; i++)
                {
                    line = myDataGrid.Items[i].ToString();
                    myWriter.WriteLine(line);
                }
                myWriter.Dispose();
                myWriter.Close();
            }
            else
            {
                MessageBox.Show("Проверьте поля!", "Ошибка!");
            }


        }

        class MyTable
        {
            public MyTable(string login, string password, string ban, string limit)
            {
                this.login = login;
                this.password = password;
                this.ban = ban;
                this.limit = limit;
            }

            public override string ToString()
            {
                return login + "|" + password + "|" + ban + "|" + limit;
            }

            public string login { get; set; }
            public string password { get; set; }
            public string ban { get; set; }
            public string limit { get; set; }
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
                result.Add(new MyTable(data[0], data[1], data[2], data[3]));
                line = myReader.ReadLine();
            } while (line != null && line!="");
            myDataGrid.ItemsSource = result;
            myReader.Dispose();
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

            myReader.Dispose();
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

     

        private void Window_Activated(object sender, EventArgs e)
        {
            StreamReader myReader = new StreamReader(way);

            string line;
            string[] data;
            List<MyTable> result = new List<MyTable>();

            line = myReader.ReadLine();
            do
            {
                data = line.Split('|');
                result.Add(new MyTable(data[0], data[1], data[2], data[3]));
                line = myReader.ReadLine();
            } while (line != null && line!="");
            myDataGrid.ItemsSource = result;
            myReader.Dispose();
            myReader.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (codeEnc.Text.Length > 4 && codeEnc.Text.Length < 17)
            {
                myCodeEnc = codeEnc.Text;
                MessageBox.Show("Новый код установлен!", "Сообщение!");
            }
            else
            {
                MessageBox.Show("Такой код не подходит!", "Ошибка");
            }
        }

        private void decrB_Click(object sender, RoutedEventArgs e)
        {
         
        }
    }

}
