using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Input;

namespace PhamMemQuanLy.ViewModel
{
    public class ControlBarViewModel : BaseViewModel
    {
        #region commands
        public ICommand closeWindowCommand  { get; set; }
        public ICommand maximizeWindowCommand { get; set; }
        public ICommand minimizeWindowCommand { get; set; }
        public ICommand mouseMoveWindowCommand { get; set; }
        #endregion

        public ControlBarViewModel() 
        {
            closeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null? false : true; }, (p) => { 
                FrameworkElement window = GetWindowParent(p); var w = window as Window;
                if (w != null)
                {
                    w.Close();
                }
            }); //p là đại diện của thằng UserControl truyền vào

            maximizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p); var w = window as Window;
                if (w != null)
                {
                    if(w.WindowState != WindowState.Maximized)
                    w.WindowState = WindowState.Maximized;
                    else w.WindowState = WindowState.Normal;
                }
            });

            minimizeWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p); var w = window as Window;
                if (w != null)
                {
                    if (w.WindowState != WindowState.Minimized)
                        w.WindowState = WindowState.Minimized;
                    else w.WindowState = WindowState.Maximized;
                }
            });

            mouseMoveWindowCommand = new RelayCommand<UserControl>((p) => { return p == null ? false : true; }, (p) => {
                FrameworkElement window = GetWindowParent(p); var w = window as Window;
                if (w != null)
                {
                    w.DragMove();
                }
            });

        }
        FrameworkElement GetWindowParent (UserControl p)
        {
            FrameworkElement parent = p.Parent as FrameworkElement;
            while (parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }
            return parent;
        }
    }
}
