﻿using System;
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

namespace CS
{
    /// <summary>
    /// TimeLine.xaml の相互作用ロジック
    /// </summary>
    public partial class TimeLine : Window
    {
        int rowIndex = 0;
        int columnIndex = 0;
        public MainWindow.TimeTable mt { get; set; }

        public TimeLine(MainWindow.TimeTable main_mt)      
        {
            InitializeComponent();
            EnableRowNum(TimeLineTable);//行番号を表示
            
            mt = main_mt;
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
            for (int i = 0; i < 5; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = mt.int_to_DAY(1).int_to_meeting(i).Meeting_name;//"月" + i.ToString();
                dr[1] = mt.int_to_DAY(2).int_to_meeting(i).Meeting_name;//"火" + i.ToString();
                dr[2] = mt.int_to_DAY(3).int_to_meeting(i).Meeting_name;//"水" + i.ToString();
                dr[3] = mt.int_to_DAY(4).int_to_meeting(i).Meeting_name;//"木" + i.ToString();
                dr[4] = mt.int_to_DAY(5).int_to_meeting(i).Meeting_name;//"金" + i.ToString();
                dr[5] = mt.int_to_DAY(6).int_to_meeting(i).Meeting_name;//"土" + i.ToString();

                dt.Rows.Add(dr);
            }
            return dt;
        }
        private void MouseDoubleClicked(object sender, MouseButtonEventArgs e)//セルをダブルクリックでメインに移動
        {
            (rowIndex, columnIndex) = ClickCellIndex(TimeLineTable, e.GetPosition(TimeLineTable));
            //MessageBox.Show($"{columnIndex}行{rowIndex}列");

            main.CBOX1.SelectedIndex=columnIndex+1;
            main.CBOX2.SelectedIndex=rowIndex;
            // var win = new MainWindow();
            // win.ShowDialog();
            this.Close();        
        }

        public (int rowIndex, int columnIndex) ClickCellIndex(DataGrid dataGrid, Point pos)
        {
            DependencyObject dep = VisualTreeHelper.HitTest(dataGrid, pos)?.VisualHit;

            while (dep != null)
            {
                dep = VisualTreeHelper.GetParent(dep);

                while (dep != null)
                {
                    if (dep is DataGridCell)
                    {
                        columnIndex = (dep == null) ? -1 : (dep as DataGridCell).Column.DisplayIndex;
                    }
                    if (dep is DataGridRow)
                    {
                        rowIndex = (dep == null) ? -1 : (int)(dep as DataGridRow).GetIndex();
                    }

                    if (rowIndex >= 0 && columnIndex >= 0)
                    {
                        break;
                    }

                    dep = VisualTreeHelper.GetParent(dep);
                }
            }
            return (rowIndex, columnIndex);
        }

        public MainWindow main{get;set;}



        private void MouseLeaved(object sender, MouseEventArgs e)
        {

        }

        private void MouseEntered(object sender, MouseEventArgs e)
        {

        }
    }


}
