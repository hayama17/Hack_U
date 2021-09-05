using System;
using System.Collections.Generic;
using System.Data;
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

public class class_info　//作ったけどどう使うか分かってない
{
    /*public string Mon_classID { get; set; }
    public string Mon_classPass { get; set; }
    public string Tue_classID{ get; set; }
    public string Tue_classPass { get; set; }
    public string Wed_classID { get; set; }
    public string Wed_classPass { get; set; }
    public string Thu_classID { get; set; }
    public string Thu_classPass { get; set; }
    public string Fri_classID { get; set; }
    public string Fri_classPass { get; set; }
    public string Sat_classID { get; set; }
    public string Sat_classPass { get; set; }
    */

    public string Mon_classURL { get; set; }
    public string Tue_classURL { get; set; }
    public string Wed_classURL { get; set; }
    public string Thu_classURL { get; set; }
    public string Fri_classURL { get; set; }
    public string Sat_classURL { get; set; }
}

namespace Test3
{
    /// <summary>
    /// TimeLine.xaml の相互作用ロジック
    /// </summary>
    public partial class TimeLine : Window
    {
        public TimeLine()
        {
            InitializeComponent();
            EnableRowNum(TimeLineTable);//行番号を表示
            TimeLineTable.DataContext = CreateData();//セルの中身を作成
        }

        private void EnableRowNum(DataGrid timeLine)
        {
            timeLine.LoadingRow += (sender, e) =>
            {
                e.Row.Header = e.Row.Header = (e.Row.GetIndex() + 1).ToString().PadLeft(5);
            };
        }

        public DataTable CreateData()
        {
            DataTable dt = new DataTable();
           
            string[] columns = new string[] { "月", "火", "水", "木", "金", "土" };
            columns.Select(i => dt.Columns.Add(i)).ToArray();
            for(int i = 0;i < 6; i++)
            {
                DataRow dr = dt.NewRow();
                /*dr[0] = "月" + i.ToString();
                dr[1] = "火" + i.ToString();
                dr[2] = "水" + i.ToString();
                dr[3] = "木" + i.ToString();
                dr[4] = "金" + i.ToString();
                dr[5] = "土" + i.ToString();*/
                dr[i] = "";
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }

   
}
