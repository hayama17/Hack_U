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
        // public int[,] period_times = new int[,] { { 12, 5 }, { 12, 7 }, { 12, 9 }, { 12, 10 }, { 12, 11 } };test用
        public int[,] period_times = new int[,] { { 9, 0 }, { 10, 40 }, { 13, 20 }, { 15, 10 }, { 17, 0 } };
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

            [JsonPropertyName("Sunday")]
            public Day Sunday { get; set; }

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
                this.Sunday = new Day();
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
                DateTime dt = DateTime.Now;
                for (int i = 0;i<5;i++){
                    if(dt.Hour==period_times[i,0] && dt.Minute == period_times[i,1]){
                        CBOX2.SelectedIndex = i;
                    }
                }
            }));


        }

        private void Method2(object state)
        {
            Dispatcher.Invoke((Action)(() =>
            {

                DateTime dNow = System.DateTime.Now;
                int weekNumber = (int)dNow.DayOfWeek;    // Int32型にキャストして曜日を数値に変換します 
                CBOX1.SelectedIndex = weekNumber;


            }));


        }

        static Timer timer1;
        static Timer timer2;
        static Timer timer3;
        static Timer timer4;
        static Timer timer5;
        static Timer timerday;


        void Time_scadule_1(int hour, int min)
        {
            var time1 = DateTime.Today + new TimeSpan(hour, min, 0) - DateTime.Now;
            // 8 時過ぎていれば次の日の 8 時にする
            if (time1 < TimeSpan.Zero) time1 += new TimeSpan(24, 0, 0);
            // 時間になったらそこから 24 時間毎に Method1 を呼び出すようタイマーをセット
            timer1 = new Timer(Method1, null, time1, new TimeSpan(24, 0, 0));

        }


        void Time_scadule_2(int hour, int min)
        {
            var time2 = DateTime.Today + new TimeSpan(hour, min, 0) - DateTime.Now;
            if (time2 < TimeSpan.Zero) time2 += new TimeSpan(24, 0, 0);
            timer2 = new Timer(Method1, null, time2, new TimeSpan(24, 0, 0));

        }

        void Time_scadule_3(int hour, int min)
        {
            var time3 = DateTime.Today + new TimeSpan(hour, min, 0) - DateTime.Now;
            if (time3 < TimeSpan.Zero) time3 += new TimeSpan(24, 0, 0);
            timer3 = new Timer(Method1, null, time3, new TimeSpan(24, 0, 0));
        }
        void Time_scadule_4(int hour, int min)
        {
            var time4 = DateTime.Today + new TimeSpan(hour, min, 0) - DateTime.Now;
            if (time4 < TimeSpan.Zero) time4 += new TimeSpan(24, 0, 0);
            timer4 = new Timer(Method1, null, time4, new TimeSpan(24, 0, 0));
        }
        void Time_scadule_5(int hour, int min)
        {
            var time1 = DateTime.Today + new TimeSpan(hour, min, 0) - DateTime.Now;
            if (time1 < TimeSpan.Zero) time1 += new TimeSpan(24, 0, 0);
            timer5 = new Timer(Method1, null, time1, new TimeSpan(24, 0, 0));
        }


        void Time_scadule_day(int hour, int min)
        {
            var time1 = DateTime.Today + new TimeSpan(hour, min, 0) - DateTime.Now;
            if (time1 < TimeSpan.Zero) time1 += new TimeSpan(24, 0, 0);
            timerday = new Timer(Method2, null, time1, new TimeSpan(24, 0, 0));
        }

        public MainWindow()
        {

            Day_Dic = new Dictionary<string, string>()
               {
                   {"Sunday","日曜日"},
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

            Time_scadule_1(period_times[0, 0], period_times[0, 1]);//スケジューラー5限分生成
            Time_scadule_2(period_times[1, 0], period_times[1, 1]);//スケジューラー5限分生成
            Time_scadule_3(period_times[2, 0], period_times[2, 1]);//スケジューラー5限分生成
            Time_scadule_4(period_times[3, 0], period_times[3, 1]);//スケジューラー5限分生成
            Time_scadule_5(period_times[4, 0], period_times[4, 1]);//スケジューラー5限分生成
            Time_scadule_day(12, 20);
            CBOX1.SelectedIndex = 1;//コンボボックスの初期値
            CBOX2.SelectedIndex = 0;//コンボボックスの初期値 

        }




        void CH1CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        void CH2CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        void CH3CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        void CH4CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
        void CH1CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        void CH3CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        void CH4CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }
        void CH2CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

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
                d = CBOX1.SelectedIndex;
                t = CBOX2.SelectedIndex;
                setMeeting(d, t);
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

            var win = new TimeLine();
            win.Show();

        }


        void button_clicked(object sender, RoutedEventArgs e)
        {
            if (CH1.IsChecked.Value)
            {
                DateTime dt = DateTime.Now;
                Py_PATH.Add("Python/webhook.py");
                var myProcess = new Process
                {
                    StartInfo = new ProcessStartInfo("python.exe")
                    {
                        UseShellExecute = false,//呼び出し時にシェル使うか
                        RedirectStandardOutput = false,//C#の出力にリダイレクトするか

                        Arguments = "Python/webhook.py" + " " + timeTable.Webhook.Webhook_url + " " + "今" + dt.Hour + "時" + dt.Minute + "分です"
                    }
                };
                myProcess.Start();
                myProcess.WaitForExit();
                myProcess.Close();


            }
            if (CH2.IsChecked.Value)
            {

                var myProcess = new Process
                {
                    StartInfo = new ProcessStartInfo("python.exe")
                    {
                        UseShellExecute = false,//呼び出し時にシェル使うか
                        RedirectStandardOutput = false,//C#の出力にリダイレクトするか
                        Arguments = "Python/Auto_zoom_start.py" + " " + useMeet.Zoom_id + " " + useMeet.Zoom_pwd

                    }
                };

                myProcess.Start();
                myProcess.WaitForExit();
                myProcess.Close();
            }
            //MessageBox.Show("ボタンが押されました");
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("変更");
            if(!zoomID.Text.Equals("ZoomID")){
                //setMeeting(d,t);
                useMeet.Zoom_id=zoomID.Text;
            }
            
            
        }

        public string useMeetId, useMeetPwd;//これ使って
        public Meeting useMeet=new();
        public void setMeeting(int D, int T)//何曜日何限がセットされてるかのチェック1
        {
            if (D == 1)
            {
                if (T == 0)
                {
                    //useMeetId = timeTable.Monday.First.Zoom_id;
                    //useMeetPwd = timeTable.Monday.First.Zoom_pwd;
                    useMeet =timeTable.Monday.First;
                    //useMeetId=useMeet.Zoom_id;
                    //useMeetPwd=useMeet.Zoom_pwd;
                }
                else if (T == 1)
                {
                    //useMeetId = timeTable.Monday.Second.Zoom_id;
                    //useMeetPwd = timeTable.Monday.Second.Zoom_pwd;
                    useMeet =timeTable.Monday.Second;
                }
                else if (T == 2)
                {
                    useMeet=timeTable.Monday.Third;
                    
                }
                else if (T == 3)
                {
                    useMeet = timeTable.Monday.Fourth;
                   
                }
                else if (T == 4)
                {
                    useMeet = timeTable.Monday.Fifth;
                    
                }
            }
            else if (D == 2)
            {
                if (T == 0)
                {
                    useMeet = timeTable.Tuesday.First;
                    
                }
                else if (T == 1)
                {
                    useMeet = timeTable.Tuesday.Second;
                   
                }
                else if (T == 2)
                {
                    useMeet = timeTable.Tuesday.Third;
                    
                }
                else if (T == 3)
                {
                    useMeet = timeTable.Tuesday.Fourth;
                    
                }
                else if (T == 4)
                {
                    useMeet = timeTable.Tuesday.Fifth;
                    
                }
            }
            else if (D == 3)
            {
                if (T == 0)
                {
                    useMeet = timeTable.Wednesday.First;
                    
                }
                else if (T == 1)
                {
                    useMeet = timeTable.Wednesday.Second;
                    
                }
                else if (T == 2)
                {
                    useMeet= timeTable.Wednesday.Third;
                    
                }
                else if (T == 3)
                {
                    useMeet = timeTable.Wednesday.Fourth;
                    
                }
                else if (T == 4)
                {
                    useMeet = timeTable.Wednesday.Fifth;
                    
                }
            }
            else if (D == 4)
            {
                if (T == 0)
                {
                    useMeet = timeTable.Thursday.First;
                    
                }
                else if (T == 1)
                {
                    useMeet = timeTable.Thursday.Second;
                    
                }
                else if (T == 2)
                {
                    useMeet = timeTable.Thursday.Third;
                    
                }
                else if (T == 3)
                {
                    useMeet = timeTable.Thursday.Fourth;
                    
                }
                else if (T == 4)
                {
                    useMeet = timeTable.Thursday.Fifth;
                    
                }
            }
            else if (D == 5)
            {
                if (T == 0)
                {
                    useMeet = timeTable.Friday.First;
                    
                }
                else if (T == 1)
                {
                    useMeet = timeTable.Friday.Second;
                    
                }
                else if (T == 2)
                {
                    useMeet = timeTable.Friday.Third;
                    
                }
                else if (T == 3)
                {
                    useMeet= timeTable.Friday.Fourth;
                   
                }
                else if (T == 4)
                {
                    useMeet = timeTable.Friday.Fifth;
                    
                }
            }
            else if (D == 6)
            {
                if (T == 0)
                {
                    useMeet = timeTable.Saturday.First;
                    
                }
                else if (T == 1)
                {
                    useMeet = timeTable.Saturday.Second;
                    
                }
                else if (T == 2)
                {
                    useMeet = timeTable.Saturday.Third;
                    
                }
                else if (T == 3)
                {
                    useMeet = timeTable.Saturday.Fourth;
                    
                }
                else if (T == 4)
                {
                    useMeet = timeTable.Saturday.Fifth;
                    
                }
            }
            else if (D == 0)
            {
                if (T == 0)
                {
                    useMeet = timeTable.Sunday.First;
                    
                }
                else if (T == 1)
                {
                    useMeet = timeTable.Sunday.Second;
                    
                }
                else if (T == 2)
                {
                    useMeet = timeTable.Sunday.Third;
                    
                }
                else if (T == 3)
                {
                    useMeet = timeTable.Sunday.Fourth;
                    
                }
                else if (T == 4)
                {
                    useMeet = timeTable.Sunday.Fifth;
                    
                }
            }
            
            // System.Windows.MessageBox.Show(D.ToString() + "," + T.ToString());
            
            zoomID.Text=useMeet.Zoom_id;
            zoompass.Text=useMeet.Zoom_pwd;

        }

        public int d;
        public int t;

        private void webhook_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void Zoompass_TextChanged(object sender, TextChangedEventArgs e)
        {
            useMeet.Zoom_pwd=zoompass.Text;
        }
        

        private void CBOX1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            d = CBOX1.SelectedIndex;
            setMeeting(d, t);

        }

        private void ZoomID_TextChanged(object sender, TextChangedEventArgs e)
        {
            useMeet.Zoom_id=zoomID.Text;
        }

        private void CBOX2_SelectionChanged(object sender, SelectionChangedEventArgs e)//時限
        {
            t = CBOX2.SelectedIndex;
            setMeeting(d, t);
        }







    }



}