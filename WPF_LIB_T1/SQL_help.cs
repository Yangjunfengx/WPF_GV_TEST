using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_LIB_T1
{ }

/// <summary>
/// 表结构类
/// </summary>
  public class Table_Struct
  {
      public string Field_Name { get; set; }
      public string Type { get; set; }
      public string Comments { get; set; }
      public Table_Struct(string field_Name, string type, string comments)
      {
          Field_Name = field_Name;
          Type = type;
          Comments = comments;
      }
  }
  /// <summary>
  /// 表结构组合类
  /// </summary>
  public class Table_Binding_DATA
  {
      public string NAME { get; set; }
      public string[] DATA { get; set; }
  }

public class SQL_help
{
    #region SQL配置
    public string Server_ip { get; set; }
    public string User_Name { get; set; }
    public string PassWord { get; set; }
    public string DataBase { get; set; }
    #endregion
    public DataTable test { get; set; }

    public DataSet DataSetData { get; set; }
    public SqlDataAdapter Sqa { get; set; }
    public SqlCommandBuilder Builder_cmd { get; set; }
    public ConnectionState Connection_State { get; set; }
      SqlConnection SqlConnection { get; set; }
      // SqlCommand sqlCommand { get; set; }
      /// <summary>
      /// 表结构
      /// </summary>
      public ObservableCollection<Table_Struct> table_Structs { get; set; }
      /// <summary>
      /// 表数据(已放弃)
      /// </summary>
      public ObservableCollection<DataColumn> Old_table_data { get; set; }
      /// <summary>
      /// 连接SQL数据库
      /// </summary>
      public void SQL_Connection()
      {
          string ConStr = "server=" + Server_ip + ";uid=" + User_Name + ";pwd=" + PassWord + ";" + "database =" + DataBase + ";";
          if (SqlConnection == null)//判断是否已经生成实例，然后打开数据库
          {
              SqlConnection = new SqlConnection();
              SqlConnection.ConnectionString = ConStr;
              SqlConnection.Open();
          }
          else
          {
              SqlConnection.ConnectionString = ConStr;
              SqlConnection.Open();
          }
          Connection_State = SqlConnection.State;
          SQL_TABLE_EMU("Trace");
          Old_table_data = new ObservableCollection<DataColumn>();
          SQL_Read_DATA("SELECT * FROM  dbo.yang_test");
      }
      /// <summary>
      /// 枚举SQL数据库指定表结构
      /// </summary>
      /// <param name="TABLE_NAME">表名字</param>
      public void SQL_TABLE_EMU(String TABLE_NAME)
      {
          object[] bUFF_DATA = new object[20];
          int i = 0;
          String CMD = "SELECT      表名       = case when a.colorder=1 then d.name else '' end,     表说明     = case when a.colorder=1 then isnull(f.value,'') else '' end,     字段序号   = a.colorder,     字段名     = a.name,     标识       = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,     主键       = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (                      SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then '√' else '' end,     类型       = b.name,     占用字节数 = a.length,     长度       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),     小数位数   = isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),     允许空     = case when a.isnullable=1 then '√'else '' end,     默认值     = isnull(e.text,''),     字段说明   = isnull(g.[value],'') FROM      syscolumns a left join      systypes b  on      a.xusertype=b.xusertype inner join      sysobjects d  on      a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties' left join      syscomments e  on      a.cdefault=e.id left join  sys.extended_properties   g  on      a.id=G.major_id and a.colid=g.minor_id   left join sys.extended_properties f on      d.id=f.major_id and f.minor_id=0    where      d.name='" + TABLE_NAME + "'  order by      a.id,a.colorder ";

          if (SqlConnection?.State != ConnectionState.Open)
              SqlConnection.Open();
          using (var sqlCommand = new SqlCommand(CMD, this.SqlConnection))
          {
              SqlDataReader sqlData = sqlCommand.ExecuteReader();
              if (table_Structs == null)
                  table_Structs = new ObservableCollection<Table_Struct>();
              else
                  table_Structs.Clear();
              while (sqlData.Read())
              {

                  i = sqlData.GetValues(bUFF_DATA);
                  Table_Struct xxx = new Table_Struct(bUFF_DATA[3].ToString(), bUFF_DATA[6].ToString(), bUFF_DATA[12].ToString());
                  table_Structs.Add(xxx);
              }
              sqlData.Close();
              sqlCommand.Dispose();
          }



      }

      public void SQL_Read_DATA(String sql_cmd_read)
    {
        if(Sqa==null)
        {
            Sqa = new SqlDataAdapter();
        }
        Sqa.SelectCommand = new SqlCommand(sql_cmd_read, this.SqlConnection);
        Builder_cmd?.RefreshSchema();

        if (SqlConnection?.State != ConnectionState.Open)
              SqlConnection.Open();

        DataSetData = new DataSet();
        Sqa.Fill(DataSetData);
        if(Builder_cmd==null)
        {
            Builder_cmd = new SqlCommandBuilder(Sqa);
        }
   
      //  Builder_cmd.GetUpdateCommand();


        //test
        var XX = DataSetData.Tables[0].DefaultView;
        var xxxx = DataSetData.Tables[0].PrimaryKey;
       //
      }

      public int Save_data()
        {

       // DataSetData.AcceptChanges();
         Builder_cmd.GetUpdateCommand();
         return Sqa.Update(DataSetData.Tables[0]);
        }

  }
