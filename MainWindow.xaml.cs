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
            string myPython ="Auto_zoom_start.py";　//".pyのパス"

            var myProcess=new Process
            {
                StartInfo=new ProcessStartInfo("python.exe")
                {
                    UseShellExecute=false,//呼び出し時にシェル使うか
                    RedirectStandardOutput=false,//C#の出力にリダイレクトするか
                    Arguments=myPython

                }
            };

            myProcess.Start();
            myProcess.WaitForExit();
            myProcess.Close();
            //MessageBox.Show("ボタンが押されました");

        }
    }
}
