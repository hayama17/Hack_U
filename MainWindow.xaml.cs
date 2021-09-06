﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
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
        public int[,] period_times = new int [,] { {9,0},{10,50},{13,20},{15,10},{17,0}};
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


        private void Method1(object state)
        {
            Dispatcher.Invoke((Action)(() =>
            {
                open_bt.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }));

        }

        static Timer timer1;

        void Time_scadule(int hour, int min)
        {
            var time1 = DateTime.Today + new TimeSpan(hour, min, 0) - DateTime.Now;

            // 8 時過ぎていれば次の日の 8 時にする
            if (time1 < TimeSpan.Zero) time1 += new TimeSpan(24, 0, 0);

            // 時間になったらそこから 24 時間毎に Method1 を呼び出すようタイマーをセット
            timer1 = new Timer(Method1, null, time1, new TimeSpan(24, 0, 0));
        }


        public MainWindow()
        {

            Day_Dic = new Dictionary<string, string>()
               {
                   {"Monday","月曜日" },
                   {"Tuesday","火曜日" },
                   {"Wednesday","水曜日" },
                   {"Thursday","木曜日" },
                   {"Friday","金曜日" },
                   {"Saturday","土曜日" }
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

            //最初の画面を作成
            DataContext = this;
            CBOX1.SelectedIndex = 0;//コンボボックスの初期値
            CBOX2.SelectedIndex = 0;//コンボボックスの初期値 

            for(int i = 0 ;i<5;i++){
                Time_scadule(period_times[i,0],period_times[i,1]);//スケジューラー5限分生成
            }

        }




        void CH1CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("チェックされました");
        }
        void CH2CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("自動起動をオンにしました");
        }
        void CH3CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("議事録をオンにしました");
        }
        void CH4CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("チャット監視をオンにしました");
        }
        void CH1CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("チェック外れました");
        }
        void CH3CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("自動起動をオフにしました");
        }
        void CH4CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("議事録をオフにしました");
        }
        void CH2CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("チャット監視をオフにしました");
        }
        void import_Checked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new();
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
                d=CBOX1.SelectedIndex;
                t=CBOX2.SelectedIndex;
                setMeeting(d,t);
            }


        }
        void export_Checked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
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
        void TimeLine_Checked(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("時間割を開きます");
            var win = new TimeLine();
            win.Show();

        }

        void Web_Hook_menu_Checked(object sender, RoutedEventArgs e)
        {

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
                        Arguments = Py_PATH[i]+" "+useMeetId+" "+useMeetPwd

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

        public string useMeetId,useMeetPwd;//これ使って
        public void setMeeting(int D,int T)//何曜日何限がセットされてるかのチェック
        {
            if(D==0)
            {
                if(T==0)
                {
                    useMeetId=timeTable.Monday.First.Zoom_id;
                    useMeetPwd=timeTable.Monday.First.Zoom_pwd;
                }
                else if(T==1)
                {
                    useMeetId=timeTable.Monday.Second.Zoom_id;
                    useMeetPwd=timeTable.Monday.Second.Zoom_pwd;
                }
                else if(T==2)
                {
                    useMeetId=timeTable.Monday.Third.Zoom_id;
                    useMeetPwd=timeTable.Monday.Third.Zoom_pwd;
                }
                else if(T==3)
                {
                    useMeetId=timeTable.Monday.Fourth.Zoom_id;
                    useMeetPwd=timeTable.Monday.Fourth.Zoom_pwd;
                }
                else if(T==4)
                {
                    useMeetId=timeTable.Monday.Fifth.Zoom_id;
                    useMeetPwd=timeTable.Monday.Fourth.Zoom_pwd;
                }
            }else if(D==1){
                 if(T==0)
                {
                    useMeetId=timeTable.Tuesday.First.Zoom_id;
                    useMeetPwd=timeTable.Tuesday.First.Zoom_pwd;
                }
                else if(T==1)
                {
                    useMeetId=timeTable.Tuesday.Second.Zoom_id;
                    useMeetPwd=timeTable.Tuesday.Second.Zoom_pwd;
                }
                else if(T==2)
                {
                    useMeetId=timeTable.Tuesday.Third.Zoom_id;
                    useMeetPwd=timeTable.Tuesday.Third.Zoom_pwd;
                }
                else if(T==3)
                {
                    useMeetId=timeTable.Tuesday.Fourth.Zoom_id;
                    useMeetPwd=timeTable.Tuesday.Fourth.Zoom_pwd;
                }
                else if(T==4)
                {
                    useMeetId=timeTable.Tuesday.Fifth.Zoom_id;
                    useMeetPwd=timeTable.Tuesday.Fourth.Zoom_pwd;
                }
            }else if(D==2){
                 if(T==0)
                {
                    useMeetId=timeTable.Wednesday.First.Zoom_id;
                    useMeetPwd=timeTable.Wednesday.First.Zoom_pwd;
                }
                else if(T==1)
                {
                    useMeetId=timeTable.Wednesday.Second.Zoom_id;
                    useMeetPwd=timeTable.Wednesday.Second.Zoom_pwd;
                }
                else if(T==2)
                {
                    useMeetId=timeTable.Wednesday.Third.Zoom_id;
                    useMeetPwd=timeTable.Wednesday.Third.Zoom_pwd;
                }
                else if(T==3)
                {
                    useMeetId=timeTable.Wednesday.Fourth.Zoom_id;
                    useMeetPwd=timeTable.Wednesday.Fourth.Zoom_pwd;
                }
                else if(T==4)
                {
                    useMeetId=timeTable.Wednesday.Fifth.Zoom_id;
                    useMeetPwd=timeTable.Wednesday.Fourth.Zoom_pwd;
                }
            }else if(D==3){
                 if(T==0)
                {
                    useMeetId=timeTable.Thursday.First.Zoom_id;
                    useMeetPwd=timeTable.Thursday.First.Zoom_pwd;
                }
                else if(T==1)
                {
                    useMeetId=timeTable.Thursday.Second.Zoom_id;
                    useMeetPwd=timeTable.Thursday.Second.Zoom_pwd;
                }
                else if(T==2)
                {
                    useMeetId=timeTable.Thursday.Third.Zoom_id;
                    useMeetPwd=timeTable.Thursday.Third.Zoom_pwd;
                }
                else if(T==3)
                {
                    useMeetId=timeTable.Thursday.Fourth.Zoom_id;
                    useMeetPwd=timeTable.Thursday.Fourth.Zoom_pwd;
                }
                else if(T==4)
                {
                    useMeetId=timeTable.Thursday.Fifth.Zoom_id;
                    useMeetPwd=timeTable.Thursday.Fourth.Zoom_pwd;
                }
            }else if(D==4)
            {
                 if(T==0)
                {
                    useMeetId=timeTable.Friday.First.Zoom_id;
                    useMeetPwd=timeTable.Friday.First.Zoom_pwd;
                }
                else if(T==1)
                {
                    useMeetId=timeTable.Friday.Second.Zoom_id;
                    useMeetPwd=timeTable.Friday.Second.Zoom_pwd;
                }
                else if(T==2)
                {
                    useMeetId=timeTable.Friday.Third.Zoom_id;
                    useMeetPwd=timeTable.Friday.Third.Zoom_pwd;
                }
                else if(T==3)
                {
                    useMeetId=timeTable.Friday.Fourth.Zoom_id;
                    useMeetPwd=timeTable.Friday.Fourth.Zoom_pwd;
                }
                else if(T==4)
                {
                    useMeetId=timeTable.Friday.Fifth.Zoom_id;
                    useMeetPwd=timeTable.Friday.Fourth.Zoom_pwd;
                }
            }else if(D==5)
            {
                 if(T==0)
                {
                    useMeetId=timeTable.Saturday.First.Zoom_id;
                    useMeetPwd=timeTable.Saturday.First.Zoom_pwd;
                }
                else if(T==1)
                {
                    useMeetId=timeTable.Saturday.Second.Zoom_id;
                    useMeetPwd=timeTable.Saturday.Second.Zoom_pwd;
                }
                else if(T==2)
                {
                    useMeetId=timeTable.Saturday.Third.Zoom_id;
                    useMeetPwd=timeTable.Saturday.Third.Zoom_pwd;
                }
                else if(T==3)
                {
                    useMeetId=timeTable.Saturday.Fourth.Zoom_id;
                    useMeetPwd=timeTable.Saturday.Fourth.Zoom_pwd;
                }
                else if(T==4)
                {
                    useMeetId=timeTable.Saturday.Fifth.Zoom_id;
                    useMeetPwd=timeTable.Saturday.Fourth.Zoom_pwd;
                }
            }
            System.Windows.MessageBox.Show(D.ToString()+","+T.ToString());

        }

        public int d,t;
        private void CBOX1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            d=CBOX1.SelectedIndex;
            setMeeting(d,t);

        }
        private void CBOX2_SelectionChanged(object sender, SelectionChangedEventArgs e)//時限
        {
            t=CBOX2.SelectedIndex;
            setMeeting(d,t);
        }







    }

}