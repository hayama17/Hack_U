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
        void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("チェックされました");
        }
        void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("チェック外れました");
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
    }
}
