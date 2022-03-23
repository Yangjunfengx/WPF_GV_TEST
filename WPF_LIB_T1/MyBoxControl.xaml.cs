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
using System.Data;
using System.ComponentModel;

namespace WPF_LIB_T1
{
    /// <summary>
    /// MyBoxControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyBoxControl : UserControl, INotifyPropertyChanged
    {
        public MyBoxControl()
        {
            InitializeComponent();
        }
        public MyBoxControl(string boxName)
        {
            InitializeComponent();
            this.BoxName = boxName;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private string _BoxName;

        public string BoxName
        {
            get { return _BoxName; }
            set 
            {
                _BoxName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BoxName"));
            }
        }

    }
}
