using PhamMemQuanLy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhamMemQuanLy.ViewModel
{
    internal class LoginViewModel : BaseViewModel
    {
        public bool IsLogin { get; set; }

        private string _username;
        private string _password;
        public string Username { get => _username; set { _username = value; OnPropertyChanged(); } }
        public string Password { get => _password; set { _password = value; OnPropertyChanged(); } }


        public ICommand closeCommand { get; set; }
        public ICommand loginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        //xử lý trong này
        public LoginViewModel()
        {
            IsLogin = false;
            Username = "";
            Password = "";
            loginCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { Login(p); });
            closeCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });

        }

        void Login(Window p)
        {
            if (p == null) return;

            /* admin - admin || staff - staff */

            string passEncode = MD5Hash(Base64Encode(Password));
            var accCount = DataProvider.Ins.DB.Users.Where(x => x.UserName == Username && x.Password == passEncode).Count(); // (x => true) hoặc (x => p != null)

            if (accCount > 0)
            {
                IsLogin = true;
                p.Close();
            }
            else
            {
                IsLogin = false;
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}
