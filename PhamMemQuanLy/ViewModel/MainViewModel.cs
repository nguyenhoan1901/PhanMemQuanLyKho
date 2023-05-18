using PhamMemQuanLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhamMemQuanLy.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private ObservableCollection<TonKho> _tonKhoList;
        public ObservableCollection<TonKho> TonKhoList { get=>_tonKhoList; set { _tonKhoList = value; OnPropertyChanged(); } }
        public bool Isloaded = false;
        public ICommand loadedWindowCommand { get; set; }
        public ICommand objectCommand { get; set; }
        public ICommand objectwdCommand { get; set; }
        public ICommand suplierCommand { get; set; }
        public ICommand userCommand { get; set; }
        public ICommand anyCommand { get; set; }
        public ICommand inputCommand { get; set; }
        public ICommand outputCommand { get; set; }

        //xử lý trong này
        public MainViewModel()
        {
            loadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if(loginWindow.DataContext == null) return;
                var loginVM = loginWindow.DataContext as LoginViewModel;
                if (loginVM.IsLogin)
                {
                    p.Show();
                    LoadTonKhoData();
                }
                else
                {
                    p.Close();
                }
            });

            objectCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectTarget wd = new ObjectTarget(); wd.ShowDialog(); });
            objectwdCommand = new RelayCommand<object>((p) => { return true; }, (p) => { ObjectWindow wd = new ObjectWindow(); wd.ShowDialog(); });
            suplierCommand = new RelayCommand<object>((p) => { return true; }, (p) => { SuplierWindow wd = new SuplierWindow(); wd.ShowDialog(); });
            userCommand = new RelayCommand<object>((p) => { return true; }, (p) => { UserManager wd = new UserManager(); wd.ShowDialog(); });
            anyCommand = new RelayCommand<object>((p) => { return true; }, (p) => { any wd = new any(); wd.ShowDialog(); });
            inputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { InputWindow wd = new InputWindow(); wd.ShowDialog(); });
            outputCommand = new RelayCommand<object>((p) => { return true; }, (p) => { OutputWindow wd = new OutputWindow(); wd.ShowDialog(); });

            //MessageBox.Show(DataProvider.Ins.DB.Users.First().DisplayName);
        }

        void LoadTonKhoData()
        {
            TonKhoList = new ObservableCollection<TonKho>();

            var objectList = DataProvider.Ins.DB.Objects;

            int i = 1;
            foreach (var item  in objectList)
            {
                //var inputList = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdObject == item.Id).Sum(p => p.Count); --Nếu where không có dữ liệu thì dòng này lỗi
                var inputList = DataProvider.Ins.DB.InputInfoes.Where(p => p.IdObject == item.Id);
                var outputList = DataProvider.Ins.DB.OutputInfoes.Where(p => p.IdObject == item.Id);

                int sumInput = 0;
                int sumOutput = 0;

                if (inputList != null && inputList.Count() > 0)
                {
                    sumInput = (int)inputList.Sum(p => p.Count);
                }
                if (outputList != null && outputList.Count() > 0)
                {
                    sumOutput = (int)outputList.Sum(p => p.Count);
                }

                TonKho tonKho = new TonKho();
                tonKho.STT = i;
                tonKho.Count = sumInput - sumOutput;
                tonKho.Object = item;

                TonKhoList.Add(tonKho);

                i++;

            }
            
        }
    }
}
