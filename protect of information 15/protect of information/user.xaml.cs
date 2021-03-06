﻿using System;
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
    /// Логика взаимодействия для user.xaml
    /// </summary>
    public partial class user : Window
    {

        string way = @"database.txt";
        string[] data;
        DataGrid myDG;
        public user(string[] Data, DataGrid myDataGrid)
        {
            data = Data;
            myDG = myDataGrid;
            InitializeComponent();
        }

        public user(string[] Data)
        {
            data = Data;
            InitializeComponent();
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
        MainWindow cD = new MainWindow();

        public static string HashPassword(string input)
        {
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (data[3] == "-")
            {
                if (VerifyMd5Hash(lastPwdT.Password, data[1]) && newPwdT.Password == secondNewPwdT.Password)
                {
                    StreamReader myReader = new StreamReader(way);

                    string line;
                    string[] data2;
                    List<MyTable> result = new List<MyTable>();

                    line = myReader.ReadLine();
                    do
                    {
                        data2 = line.Split('|');
                        result.Add(new MyTable(data2[0], data2[1], data2[2], data2[3]));
                        line = myReader.ReadLine();
                    } while (line != null);
                    myReader.Close();



                    StreamWriter myWriter = new StreamWriter(way);
                    result.Remove(result.Find(x => x.login == data[0]));
                    MyTable newPwd = new MyTable(data[0], HashPassword(newPwdT.Password), data[2],data[3]);
                    result.Add(newPwd);

                    foreach(var a in result)
                    {
                        myWriter.WriteLine(a.ToString());
                    }

                    myWriter.Close();
                    this.Close();
                }
                else
                if (cD.checkData(newPwdT.Password) && data[3] == "+")
                {
                    MessageBox.Show("В пароле должны присутствовать латинские буквы, символы кириллицы и знаки арифметических операций!", "Ошибка");
                }
                else if (!VerifyMd5Hash(lastPwdT.Password, data[1]))
                {
                    MessageBox.Show("Проверьте текущий пароль!", "Ошибка");
                }
                else if (newPwdT.Password != secondNewPwdT.Password)
                {
                    MessageBox.Show("Пароль подтверждение был вверден неправильно!", "Ошибка!");
                }
            }
            else
            {
                if ((data[1] == "" || VerifyMd5Hash(lastPwdT.Password, data[1])) && newPwdT.Password == secondNewPwdT.Password && (cD.checkData(newPwdT.Password)))
                {
                    List<MyTable> result = new List<MyTable>();
                    StreamReader myReader = new StreamReader(way);

                    string line;
                    string[] data2;

                    line = myReader.ReadLine();
                    do
                    {
                        data2 = line.Split('|');
                        result.Add(new MyTable(data2[0], data2[1], data2[2], data2[3]));
                        line = myReader.ReadLine();
                    } while (line != null);
                    myReader.Close();

                    StreamWriter myWriter = new StreamWriter(way);
                    result.Remove(result.Find(x => x.login == data2[0]));
                    MyTable newPwd = new MyTable(data[0], HashPassword(newPwdT.Password), data[2], data[3]);
                    result.Add(newPwd);

                    foreach (var a in result)
                    {
                        myWriter.WriteLine(a.ToString());
                    }

                    myWriter.Close();
                    cD.Close();
                    this.Close();
                }
                else
                if (!cD.checkData(newPwdT.Password) && data[3] == "+") 
                {
                    MessageBox.Show("В пароле должны присутствовать латинские буквы, символы кириллицы и знаки арифметических операций!", "Ошибка");
                }
                else if(VerifyMd5Hash(lastPwdT.Password, data[1]) && data[1] != "")
                {
                    MessageBox.Show("Проверьте текущий пароль!", "Ошибка");
                }
                else if(newPwdT.Password != secondNewPwdT.Password)
                {
                    MessageBox.Show("Пароль подтверждение был вверден неправильно!", "Ошибка!");
                }
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
