using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security.Cryptography;


namespace protect_of_information
{
    /// <summary>
    /// Логика взаимодействия для code.xaml
    /// </summary>
    /// 
    
    public partial class code : Window
    {

        public code()
        {
            InitializeComponent();
        }

        public bool ok = false;
        public string codeEnc = "";

        string way = @"database.txt";
        string way2 = @"database2.txt";

        bool adminF = false;

        private void okB_Click(object sender, RoutedEventArgs e)
        {
            codeEnc = codeT.Password;

            if (codeEnc != "" && codeEnc.Length>4 && codeEnc.Length<17)
            {
                StreamReader encReader = new StreamReader(way2);
                string encrypted = encReader.ReadToEnd();
                encReader.Close();
                RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
                
                byte[] key = Encoding.Default.GetBytes(codeT.Password);
                rc2CSP.Key = key;
                byte[] IV =  { 156, 158, 224, 153, 115, 56, 171, 196 };
                rc2CSP.IV = IV;

                ICryptoTransform decryptor = rc2CSP.CreateDecryptor(key, rc2CSP.IV);

                byte[] toEncrypt = Encoding.Default.GetBytes(encrypted);

                MemoryStream msDecrypt = new MemoryStream(toEncrypt);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                StringBuilder roundtrip = new StringBuilder();

                int b = 0;
                try
                {
                    do
                    {
                        b = csDecrypt.ReadByte();

                        if (b != -1)
                        {
                            roundtrip.Append((char)b);
                        }

                    } while (b != -1);
                    msDecrypt.Close();
                    csDecrypt.Close();
                }
                catch(Exception a)
                { }

                FileInfo fileInf = new FileInfo(way);
                if (fileInf.Exists)
                {
                    fileInf.Delete();
                }
                FileStream wtf = fileInf.Create();
                wtf.Dispose();
                wtf.Close();

                StreamWriter myWriter = new StreamWriter(way);
                myWriter.Write(roundtrip.ToString());
                myWriter.Close();

                StreamReader myReader = new StreamReader(way);
                string line = myReader.ReadLine();
                string[] data;
                do
                {
                    data = line.Split('|');
                    if (data[0] == "admin")
                    {
                        adminF = !adminF;
                        break;
                    }
                    line = myReader.ReadLine();
                } while (line != null);

                myReader.Close();

                if (adminF)
                {
                    MessageBox.Show("Код верен!", "Добро пожаловать!");
                    this.Close();
                }
                else
                {
                    fileInf.Delete();
                    MessageBox.Show("Код неверен!", "Ошибка!");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Код неверен!", "Ошибка!");
                this.Close();
            }
        }

        private void exitB_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if(adminF)ok = !ok;
        }
    }
}
