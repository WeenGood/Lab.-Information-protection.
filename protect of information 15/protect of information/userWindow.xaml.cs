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

namespace protect_of_information
{
    /// <summary>
    /// Логика взаимодействия для userWindow.xaml
    /// </summary>
    public partial class userWindow : Window
    {

        string[] data;
        public userWindow(string[] a)
        {
            data = a;

            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            user user = new user(data);
            user.ShowDialog();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
