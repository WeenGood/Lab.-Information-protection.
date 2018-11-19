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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace protect_of_information
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    
        //ЛОГИН|ПАРОЛЬ|БЛОКИРОВКА|ОГРАНИЧЕНИЕ

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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


        private void loginT_GotFocus(object sender, RoutedEventArgs e)
        {
            loginT.Text = "";
        }

        string way = @"database.txt";

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {    
            FileInfo fileInf = new FileInfo(way);
            if(fileInf.Exists)
            {
                fileInf.Delete();
            }
            FileStream wtf = fileInf.Create();
            wtf.Dispose();
            StreamWriter myWriter = fileInf.AppendText();
            myWriter.WriteLine("admin||-|-|0");
            myWriter.Close();
        }

        List<MyTable> createMyList()
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
            myReader.Close();
            return result;
        }
        int countMiss = 0;
        private void enter_Click(object sender, RoutedEventArgs e)
        {
            

            List<MyTable> result = createMyList();

            bool flag = false, adminF = false, okay = false, newPass = false;
            string login = loginT.Text;
            string pass = passT.Password;

            StreamReader myReader = new StreamReader(way);

            string line;
            string[] data;

            line = myReader.ReadLine();

            do
            {
                data = line.Split('|');
                if (data[1] == "") newPass = true;
                if (login == data[0] && (pass == data[1]||newPass) && data[2] != "+" && data[0].IndexOf('|') == -1 && data[1].IndexOf('|') == -1 && (data[3] == "-" ||data[3]=="+" && pass.Length>= Convert.ToInt32(data[4]))) 
                {
                    okay = true;
                    if (pass != "")
                    {
                        if(newPass)
                        {
                            accept a = new accept(pass);
                            a.ShowDialog();
                            if (!a.flag)
                            {
                                MessageBox.Show("Неправильный пароль!", "Ошибка");
                                break;
                            }
                        }
                        flag = true;
                        if (data[0] == "admin") adminF = true;
                        if (null != result.Find(x => x.password == "" && x.login == login))
                        {
                            result.Remove(result.Find(x => x.password == "" && x.login == login));
                            result.Add(new MyTable(data[0], pass, data[2],data[3],data[4]));
                        }
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Введите пароль", "Ошибка");
                        break;
                    }
                }
                else if((pass == data[1] || newPass) && data[3] == "+" && pass.Length < Convert.ToInt32(data[4]))
                {
                    MessageBox.Show("Длина пароля должна быть не меньше "+data[4], "Ошибка");

                    if (!newPass)
                    {
                        myReader.Close();
                        user user = new user(data);
                        user.ShowDialog();
                    }
                    break;
                }
            } while ((line = myReader.ReadLine()) !=null);

            myReader.Close();
            if (newPass)
            {
                StreamWriter myWriter = new StreamWriter(way);

                foreach (var a in result)
                {
                    myWriter.WriteLine(a.login + "|" + a.password + "|" + a.ban + "|" + a.limit + "|" + a.minLength);
                }
                myWriter.Close();
            }
            if (flag && adminF)
            {
                flag = !flag;
                countMiss = 0;
                admin admin = new admin();
                admin.ShowDialog();
            }
            else if(flag && !adminF)
            {
                countMiss = 0;
                user user = new user(data);
                user.ShowDialog();
            }
            else 
            if(data[2] == "+")
            {
                MessageBox.Show("Вы пытаетесь зайти в заблокированный аккаунт!", "Ошибка!");
            }
            else
            {
                countMiss++;
                if (countMiss == 3) this.Close();
                if (!okay) MessageBox.Show("Проверьте логин и пароль", "Ошибка");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Андрианов Илья, Вариант 1: Длина не меньше минимальной длины, устанавливаемой администратором и сохраняемой в учетной записи пользователя.", "О программе");
        }
    }
}
