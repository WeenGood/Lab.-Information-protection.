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
    /// Логика взаимодействия для add_user.xaml
    /// </summary>
    public partial class add_user : Window
    {
        public add_user()
        {
            InitializeComponent();
        }

        string way = @"database.txt";

        private void acceptB_Click(object sender, RoutedEventArgs e)
        {
            if (loginT.Text.IndexOf('|') == -1)
            {
                StreamReader myReader = new StreamReader(way);

                string line = myReader.ReadLine();
                string[] data;
                bool flag = false;
                do
                {
                    data = line.Split('|');
                    if (data[0] == loginT.Text)
                    {
                        flag = true;
                        MessageBox.Show("Такой пользователь уже существует!");
                        break;
                    }
                    line = myReader.ReadLine();
                } while (line != null);
                
                myReader.Close();
                if (!flag)
                {
                    StreamWriter myWriter = new StreamWriter(way, true);

                    string newLine = loginT.Text + "||-|-";

                    myWriter.WriteLine(newLine);

                    myWriter.Close();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Уберите символ | из поля ввода!", "Ошибка");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
