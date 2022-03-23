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
using System.CodeDom;
using System.Collections.ObjectModel;

namespace WPF_LIB_T1
{
    /// <summary>
    /// map_test.xaml 的交互逻辑
    /// </summary>
    public partial class map_test : UserControl
    {
        public int i { get; set; }
        public map_test()
        {
            InitializeComponent();
            MyList = new ObservableCollection<MyBoxControl>();
            MyList2 = new ObservableCollection<MyBoxControl>();
            // this.SP.DataContext = MyList;
            SP.DataContext = MyList;
            SP2.DataContext = MyList2;

        }
        public ObservableCollection<MyBoxControl> MyList { get; set; }
        public ObservableCollection<MyBoxControl> MyList2 { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyList.Add(new MyBoxControl(i.ToString()));
            MyList2.Add(new MyBoxControl(i.ToString()));
            i++;
        }
    }
}
