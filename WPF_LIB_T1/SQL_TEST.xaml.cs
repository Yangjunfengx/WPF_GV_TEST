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

using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace WPF_LIB_T1
{
    /// <summary>
    /// SQL_TEST.xaml 的交互逻辑
    /// </summary>
    public partial class SQL_TEST : UserControl
    {

        SQL_help SQL;
        public SQL_TEST()
        {
           
            InitializeComponent();
            SQL = new SQL_help();
            SQL.Server_ip = "127.0.0.1";
            SQL.User_Name = "sa";
            SQL.PassWord = "123";
            SQL.DataBase = "SQLPLC";
            SQL.SQL_Connection();
            x1.DataContext = SQL.DataSetData.Tables[0];


        }

       

        private void x1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //CustomColumn customColumn = new CustomColumn();

            //customColumn.CanUserSort = e.Column.CanUserSort;

            //customColumn.Header = e.Column.Header;

            //customColumn.Binding = (e.Column as DataGridBoundColumn).Binding;
            //if(customColumn.Header.ToString()=="ID")
            //{
            //    customColumn.IsPrimaryKey = true;
            //    customColumn.IsReadOnly = true;
            //}

            //e.Column = customColumn;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
          
           var x = SQL.Save_data();
        }
    }

    public class CustomColumn : DataGridTextColumn

    {

        public bool IsPrimaryKey

        {

            get { return (bool)GetValue(IsPrimaryKeyProperty); }

            set { SetValue(IsPrimaryKeyProperty, value); }

        }



        public static readonly DependencyProperty IsPrimaryKeyProperty =

            DependencyProperty.Register("IsPrimaryKey", typeof(bool), typeof(DataGridColumn), new FrameworkPropertyMetadata(false, null, null));

    }

}
