using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
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


namespace CS
{
    /// <summary>
    /// TimeLine.xaml の相互作用ロジック
    /// </summary>
    public partial class Upload : Window
    {
        public string Json_PATH;
        public Upload()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Clicked_Reference_Bt(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "Json(.json)|*.json|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();
            if (result == true)
            {
                Json_PATH = openFileDialog.FileName;//fileの絶対パスを表示
                Path_Text.Text = Json_PATH;
            }

        }

        private void Clicked_Upload_Bt(object sender, RoutedEventArgs e)
        {
            var myProcess = new Process
            {
                StartInfo = new ProcessStartInfo("python.exe")
                {
                    UseShellExecute = false,//呼び出し時にシェル使うか
                    RedirectStandardOutput = true,//C#の出力にリダイレクトするか
                    Arguments = "Python/test_mysql.py" + " "+ mail_Text.Text + " "+Path_Text.Text

                }
            };
            var CurrentDirectory = Directory.GetCurrentDirectory();
            MessageBox.Show(CurrentDirectory);
            myProcess.Start();
            myProcess.WaitForExit();
            myProcess.Close();
        }
    }

}
