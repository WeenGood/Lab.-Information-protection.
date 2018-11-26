using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Security.Cryptography;
using System.Text;

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

        string codeEnc;

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


        private void loginT_GotFocus(object sender, RoutedEventArgs e)
        {
            loginT.Text = "";
        }

        string way = @"database.txt";
        string way2 = @"database2.txt";

        bool ok;


        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            code encWin = new code();
            encWin.ShowDialog();
            codeEnc = encWin.codeEnc;
            ok = encWin.ok;
            if (!encWin.ok)
                this.Close();

            //FileInfo fileInf = new FileInfo(way);
            //if(fileInf.Exists)
            //{
            //    fileInf.Delete();
            //}
            //FileStream wtf = fileInf.Create();
            //wtf.Dispose();
            //StreamWriter myWriter = fileInf.AppendText();
            //myWriter.WriteLine("admin||-|-");
            //myWriter.Close();
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
                result.Add(new MyTable(data[0], data[1], data[2], data[3]));
                line = myReader.ReadLine();
            } while (line != null && line != "");
            myReader.Close();
            return result;
        }
        int countMiss = 0;

        public bool checkData(string pass)
        {
            foreach(var a in pass)
                if(a == '+' || a == '-' || a=='/' || a =='*' || a=='%' || a=='^')
                    foreach (var b in pass)
                        if (Char.IsLetter(b)) return true;
            return false;
        }



        public static string HashPassword(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);
            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {

            MD5 md5Hash = MD5.Create();
            // Hash the input.
            string hashOfInput = HashPassword(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void enter_Click(object sender, RoutedEventArgs e)
        {


            List<MyTable> result = createMyList();

            bool flag = false, adminF = false, okay = false, newPass = false;
            string login = loginT.Text;
            string pass = passT.Password;

            StreamReader myReader = new StreamReader(way);

            string line;
            string[] data = {""};

            line = myReader.ReadLine();
            int i = 0;
            do
            {
                i = 0;
                foreach (var a in line)
                {
                    if (a == '|') i++;
                }
                    if (i != 3) 
                    {
                        MessageBox.Show("Уберите символ | из полей ввода!","Ошибка!");
                        break;
                    }
                data = line.Split('|');
                if (data[0] == login && data[1] == "") newPass = true;
                if (login == data[0] && (VerifyMd5Hash(pass, data[1])||newPass) && data[2] != "+" && (data[3] == "-" ||data[3]=="+") && (data[3]=="-" || checkData(pass))) 
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
                            result.Add(new MyTable(data[0], pass, data[2],data[3]));
                        }
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Введите пароль", "Ошибка");
                        break;
                    }
                }
                else if(login == data[0] && (VerifyMd5Hash(pass, data[1]) || newPass) && data[3] == "+" && !checkData(pass))
                {
                    MessageBox.Show("В пароле должны присутствовать латинские буквы, символы кириллицы и знаки арифметических операций!", "Ошибка");

                    if (!newPass)
                    {
                        myReader.Close();
                        userWindow user = new userWindow(data);
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
                    myWriter.WriteLine(a.login + "|" + HashPassword(a.password) + "|" + a.ban + "|" + a.limit);
                }
                myWriter.Close();
            }
            if (flag && adminF)
            {
                flag = !flag;
                countMiss = 0;
                admin admin = new admin(codeEnc);
                admin.ShowDialog();
                if (admin.myCodeEnc != codeEnc)
                    codeEnc = admin.myCodeEnc;
            }
            else if(flag && !adminF)
            {
                countMiss = 0;
                userWindow user = new userWindow(data);
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
            MessageBox.Show("Провоторов Иван, Вариант 15: Наличие латинских букв, символов кириллицы и знаков арифметических операций.", "О программе");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Провоторов Иван, Вариант 15: Наличие латинских букв, символов кириллицы и знаков арифметических операций.", "О программе");
        }

        void encr()
        {
            StreamReader myReader = new StreamReader(way);

            string original = myReader.ReadToEnd();

            myReader.Close();

            RC2CryptoServiceProvider rc2CSP = new RC2CryptoServiceProvider();
            

            byte[] key = Encoding.Default.GetBytes(codeEnc);//Encoding.Default.GetBytes("qwert");
            rc2CSP.Key = key;

            byte[] IV = { 156, 158, 224, 153, 115, 56, 171, 196 };
            rc2CSP.IV = IV;

            ICryptoTransform encryptor = rc2CSP.CreateEncryptor(key, rc2CSP.IV);

            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            byte[] toEncrypt = Encoding.Default.GetBytes(original);

            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            byte[] encrypted = msEncrypt.ToArray();
            msEncrypt.Close();
            csEncrypt.Close();
            StreamWriter myWriter = new StreamWriter(way2);

            myWriter.Write(Encoding.Default.GetString(encrypted));

            myWriter.Close();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
                List<string[]> hpas = new List<string[]>();
                StreamReader myReader = new StreamReader(way);
                string line = myReader.ReadLine();
                string[] data;
                do
                {
                    data = line.Split('|');
                    data[1] = HashPassword(data[1]);
                    hpas.Add(data);
                    line = myReader.ReadLine();
                } while (line != null && line != "");

                myReader.Close();
                myReader.Dispose();

                StreamWriter myWriter = new StreamWriter(way);

                foreach (var a in hpas)
                    myWriter.WriteLine(a[0] + "|" + a[1] + "|" + a[2] + "|" + a[3]);

                myWriter.Close();
                myWriter.Dispose();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ok) encr();
            FileInfo myFile = new FileInfo(way);
            if(myFile.Exists)
            {
                myFile.Delete();
            }
        }


    }
}
