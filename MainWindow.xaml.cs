using System;
using System.IO;
using System.Reflection.Emit;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace Regesitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// MD5 encrypt a string and
        /// return as a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string valueString)
        {
            string ret = String.Empty;
            //Setup crypto
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            //Get bytes
            byte[] data = Encoding.ASCII.GetBytes(valueString);
            //Encrypt
            data = md5Hasher.ComputeHash(data);
            //Convert from byte 2 hex
            for (int i = 0; i < data.Length; i++)
            {
                ret += data[i].ToString("x2").ToLower();
            }
            //Return encoded string
            return ret;

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            string str  = this.textBox1.Text;

            if (str.Equals("") || str.Length < 12)
            {
                MessageBox.Show("请输入正确的mac地址.");
                return;
            }

            string str2 = string.Empty;
            if (str.Contains("-"))
            {
                str2 = str.Replace("-", "");
                if (str2.Equals("") || (str2.Length != 12))
                {
                    MessageBox.Show("请输入正确的mac地址.");
                    return;
                }
                
            }

            if (radioButtonUser.IsChecked != null && (bool) radioButtonUser.IsChecked)
            {
                str2 = str2 + "0";
            }

            if (radioButtonAdmin.IsChecked != null && (bool)radioButtonAdmin.IsChecked)
            {
                str2 = str2 + "1";
            }

            string encryptStr = MD5Encrypt(str2);


            if (File.Exists("license.dat"))
                File.Delete("license.dat");
            using (var file = new StreamWriter("license.dat", true))
            {
                file.WriteLine(encryptStr);
            }  

            //hasp.Logout();

            MessageBox.Show("成功生成license.dat，软件注册成功.");
        }
    }
}
