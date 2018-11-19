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
    /// Логика взаимодействия для accept.xaml
    /// </summary>
    public partial class accept : Window
    {
        string newPass;
        public bool flag = false;
        public accept(string pass)
        {
            newPass = pass;
            InitializeComponent();
        }

        private void acPasB_Click(object sender, RoutedEventArgs e)
        {
            if (newPass == acPassT.Password) flag = !flag;
            this.Close();
        }
    }
}
