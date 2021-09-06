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
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Win32;
using System.IO;

namespace CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //以下授業クラス"Meeting", 日クラス"Day", 時間割クラス"TimeTable"
        public class Meeting
        {   //授業クラス
            [JsonPropertyName("Zoom_id")]
            public string Zoom_id{get;set;}
            
            [JsonPropertyName("Zoom_pwd")]
            public string Zoom_pwd{get;set;}

            [JsonPropertyName("Meeting_name")]
            public string Meeting_name{get;set;}
            //public string Webhook_url{get;set;}
            //public bool Webhook_bl{get;set;}
        }

        public class Day
        {   //日クラス

            
            [JsonPropertyName("First")]
            public Meeting First{get;set;}//1限

            [JsonPropertyName("Second")]
            public Meeting Second{get;set;}

            [JsonPropertyName("Third")]
            public Meeting Third{get;set;}

            [JsonPropertyName("Fourth")]
            public Meeting Fourth{get;set;}

            [JsonPropertyName("Fifth")]
            public Meeting Fifth{get;set;}

            //コンストラクタ
            public Day(){
                this.First=new Meeting();
                this.Second=new Meeting();
                this.Third=new Meeting();
                this.Fourth=new Meeting();
                this.Fifth=new Meeting();
            }
        }

        public class TimeTable
        {   //時間割クラス
            [JsonPropertyName("Monday")]
            public Day Monday{get;set;}

            [JsonPropertyName("Tuesday")]
            public Day Tuesday{get;set;}

            [JsonPropertyName("Wednesday")]
            public Day Wednesday{get;set;}

            [JsonPropertyName("Thursday")]
            public Day Thursday{get;set;}

            [JsonPropertyName("Friday")]
            public Day Friday{get;set;}

            [JsonPropertyName("Saturday")]
            public Day Saturday{get;set;}

            //コンストラクタ
            public TimeTable(){
                this.Monday=new Day();
                this.Tuesday=new Day();
                this.Wednesday=new Day();
                this.Thursday=new Day();
                this.Friday=new Day();
                this.Saturday=new Day();
            }

        }
        //以上、Jsonを扱うためのクラス群



        private List<string> Py_PATH = new();
        public Dictionary<string, string> Day_Dic { get; set; }
        public Dictionary<string, string> Period_Dic { get; set; }
        public MainWindow()
        {

            Day_Dic = new Dictionary<string, string>()
               {
                   {"Monday","月曜日" },
                   {"Tuesday","火曜日" },
                   {"Wednesday","水曜日" },
                   {"Thursday","木曜日" },
                   {"Friday","金曜日" },
                   { "Saturday","土曜日" }
                };

            Period_Dic = new()
            {
                { "First","1限" },
                { "Second","2限" },
                { "Third","3限" },
                { "Fourth","4限" },
                { "Fifth","5限" }
            };


            InitializeComponent();
            

            DataContext = this;

        }
        
        
        
       
        void CH1CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("チェックされました");
        }
        void CH2CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("自動起動をオンにしました");
        }
        void CH3CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("議事録をオンにしました");
        }
        void CH4CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("チャット監視をオンにしました");
        }
        void CH1CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("チェック外れました");
        }
        void CH3CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("自動起動をオフにしました");
        }
        void CH4CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("議事録をオフにしました");
        }
        void CH2CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("チャット監視をオフにしました");
        }
        void import_Checked(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            OpenFileDialog openFileDialog = new();
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                webhook.Text = openFileDialog.FileName;
                using (Stream fileStream = openFileDialog.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, true);
                    webhook.Text = sr.ReadToEnd();
                }
            }

        }
            void export_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("エクスポートが選択されました");
        }
        void edit_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("時間割を開きます");
            var win = new TimeLine();
            win.Show();
            
        }
        

        void button_clicked(object sender, RoutedEventArgs e)
        {
            if (CH1.IsChecked.Value)
            {
                Py_PATH.Add("Python/Auto_zoom_start.py");
            }
            if (CH2.IsChecked.Value)
            {
                Py_PATH.Add("Python/webhook.py");
            }
            for (int i = 0; i < Py_PATH.Count; i++)
            {
                var myProcess = new Process
                {
                    StartInfo = new ProcessStartInfo("python.exe")
                    {
                        UseShellExecute = false,//呼び出し時にシェル使うか
                        RedirectStandardOutput = false,//C#の出力にリダイレクトするか
                        Arguments = Py_PATH[i]

                    }
                };

                myProcess.Start();
                myProcess.WaitForExit();
                myProcess.Close();
            }
            //MessageBox.Show("ボタンが押されました");

            Py_PATH.Clear();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        
        private void CBOX1_SelectionChanged(object sender, SelectionChangedEventArgs  e){

        }
        private void CBOX2_SelectionChanged(object sender, SelectionChangedEventArgs  e){
            
        }
    }
}