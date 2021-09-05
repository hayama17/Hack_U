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

namespace CS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> Py_PATH = new();
        public MainWindow()
        {
            InitializeComponent();
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
            MessageBox.Show("インポートが選択されました");
        }
        void export_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("エクスポートが選択されました");
        }
        void edit_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("時間割を開きます");
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
