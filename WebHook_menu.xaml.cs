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
    public partial class WebHook_menu: Window
    {
        public string Json_PATH;
        public WebHook_menu(string post_url)
        {
            InitializeComponent();
            url.Text = post_url;
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Clicked_Upload_Bt(object sender, RoutedEventArgs e)
        {
            
        }
    }

}
