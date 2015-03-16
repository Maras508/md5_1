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
using System.Security.Cryptography;


namespace md5_1
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

        public string wczytany_tekst;
        public StreamReader sr;
        RSACryptoServiceProvider rsa;
        //public MD5 generator_MD5;
        public string skrot_wiadomosci;
        

        private void textbox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void but1_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sr = new StreamReader(openFileDialog1.FileName);
                wczytany_tekst = sr.ReadToEnd();
                textbox1.AppendText(wczytany_tekst);
            }
            
        }

        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void but2_Click(object sender, RoutedEventArgs e)
        {
            if (md5_checkbox.IsChecked == true)
            {
                MessageBox.Show("Generowanie skrótu...");

                if (wczytany_tekst == null)
                {
                    

                }
                using(MD5 generator_MD5 = MD5.Create())
                {

                    skrot_wiadomosci = GetMd5Hash(generator_MD5, wczytany_tekst);

                }

                textbox2.AppendText(skrot_wiadomosci);

            }

            if (sha256_checkbox.IsChecked == true)
            {
                MessageBox.Show("Generowanie skrótu...");

                //ggggggggggggg

                using (SHA256 generator_Sha256 = SHA256.Create())
                {

                    skrot_wiadomosci = GetSha256Hash(generator_Sha256, wczytany_tekst);

                }

                textbox2.AppendText(skrot_wiadomosci);
            }


        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        static string GetSha256Hash(SHA256 sha256Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void but3_Click(object sender, RoutedEventArgs e)
        {
            rsa = new RSACryptoServiceProvider();

            File.WriteAllText(@"C:\klucze\privateKey.xml", rsa.ToXmlString(true));  // Private Key
            File.WriteAllText(@"C:\klucze\publicKey.xml", rsa.ToXmlString(false));  // Public Key
        }

    }
}
