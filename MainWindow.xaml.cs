using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
// using System.Diagnostics;
// using System.Text.Json;
// using System.Text.Json.Serialization;
// using System.IO;
using Microsoft.Win32;
using Windows.Services.Maps;

namespace CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<string> Py_PATH = new();//pythonのスクリプトのパスのリスト
        private string[] daylist = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        public Dictionary<string, string> Day_Dic { get; set; }//コンボボックスの曜日の要素
        public Dictionary<string, string> Period_Dic { get; set; }//コンボボックスの何限の要素

        public TimeTable timeTable;//Jsonから生成するtimetabeleクラスの宣言

        public string Json_PATH = "test.json";//jsonファイルの場所


        //以下授業クラス"Meeting", 日クラス"Day", 時間割クラス"TimeTable"
        public class Meeting
        {   //授業クラス

            [JsonPropertyName("Webhook_on")]//webhookするか
            public bool Webhook_on { get; set; }

            [JsonPropertyName("Zoom_Auto_on")]//zoomを自動起動するか
            public bool Zoom_Auto { get; set; }

            [JsonPropertyName("Zoom_id")]//ZoomのID
            public string Zoom_id { get; set; }

            [JsonPropertyName("Zoom_pwd")]//zoomのパスワード
            public string Zoom_pwd { get; set; }

            [JsonPropertyName("Meeting_name")]//授業名
            public string Meeting_name { get; set; }

            [JsonPropertyName("recording_on")]//議事録を作
            public bool recording_on { get; set; }

            [JsonPropertyName("chat_on")]//授業名
            public bool chat_on { get; set; }
        }

        public class Day
        {   //日クラス


            [JsonPropertyName("First")]
            public Meeting First { get; set; }//1限

            [JsonPropertyName("Second")]
            public Meeting Second { get; set; }

            [JsonPropertyName("Third")]
            public Meeting Third { get; set; }

            [JsonPropertyName("Fourth")]
            public Meeting Fourth { get; set; }

            [JsonPropertyName("Fifth")]
            public Meeting Fifth { get; set; }

            //コンストラクタ
            public Day()
            {
                this.First = new Meeting();
                this.Second = new Meeting();
                this.Third = new Meeting();
                this.Fourth = new Meeting();
                this.Fifth = new Meeting();
            }
        }

        public class Webhook
        {
            [JsonPropertyName("Webhook_url")]
            public string Webhook_url { get; set; }

            [JsonPropertyName("Webhook_text")]
            public string Webhook_text { get; set; }
        }
        public class TimeTable
        {   //時間割クラス

            [JsonPropertyName("Monday")]
            public Day Monday { get; set; }

            [JsonPropertyName("Tuesday")]
            public Day Tuesday { get; set; }

            [JsonPropertyName("Wednesday")]
            public Day Wednesday { get; set; }

            [JsonPropertyName("Thursday")]
            public Day Thursday { get; set; }

            [JsonPropertyName("Friday")]
            public Day Friday { get; set; }

            [JsonPropertyName("Saturday")]
            public Day Saturday { get; set; }

            [JsonPropertyName("Webhook")]
            public Webhook Webhook { get; set; }

            //コンストラクタ
            public TimeTable()
            {
                this.Monday = new Day();
                this.Tuesday = new Day();
                this.Wednesday = new Day();
                this.Thursday = new Day();
                this.Friday = new Day();
                this.Saturday = new Day();
                this.Webhook = new Webhook();
            }

            public void Path_to_Class(string path)//Jsonのパスからオブジェクトを更新する関数
            {
                StreamReader JsonRead = new(path, Encoding.GetEncoding("UTF-8"));//json読み込んで
                string Json_Str = JsonRead.ReadToEnd();//stringに全文保持して

                this.Monday = JsonSerializer.Deserialize<TimeTable>(Json_Str).Monday;//timeTableに突っ込む
                this.Tuesday = JsonSerializer.Deserialize<TimeTable>(Json_Str).Tuesday;//timeTableに突っ込む
                this.Wednesday = JsonSerializer.Deserialize<TimeTable>(Json_Str).Wednesday;//timeTableに突っ込む
                this.Thursday = JsonSerializer.Deserialize<TimeTable>(Json_Str).Thursday;//timeTableに突っ込む
                this.Friday = JsonSerializer.Deserialize<TimeTable>(Json_Str).Friday;//timeTableに突っ込む
                this.Saturday = JsonSerializer.Deserialize<TimeTable>(Json_Str).Saturday;//timeTableに突っ込む
                this.Webhook = JsonSerializer.Deserialize<TimeTable>(Json_Str).Webhook;//timeTableに突っ込む
            }


        }
        //以上、Jsonを扱うためのクラス群



        





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
                { "First", "1限" },
                { "Second", "2限" },
                { "Third", "3限" },
                { "Fourth", "4限" },
                { "Fifth", "5限" }
            };

            timeTable = new();

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
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Json(.json)|*.json|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                Json_PATH = openFileDialog.FileName;//fileの絶対パスを表示
                using (Stream fileStream = openFileDialog.OpenFile())
                {
                    StreamReader sr = new StreamReader(fileStream, true);
                    timeTable.Path_to_Class(Json_PATH);
                }
            }

        }
        void export_Checked(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.Filter = "Json(.json)|*.json|All Files (*.*)|*.*";
            bool? result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                string jsonString = JsonSerializer.Serialize(timeTable);
                using Stream fileStream = saveFileDialog.OpenFile();
                using StreamWriter sr = new StreamWriter(fileStream);
                {
                    sr.Write(jsonString); //Jsonをstringにエンコードしたファイルに書き込む
                }
            }

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

        private void CBOX1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void CBOX2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}