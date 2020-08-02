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

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for PasswordPage.xaml
    /// </summary>
    public partial class PasswordPage : Page
    {
        public string title;
        public string username;
        public string itemname;
        public string password;
        public string itemtype;
        public PasswordPage(string titlenameget,string usernameget, string passwordget, string itemtypeget)
        {
            InitializeComponent();
            password = passwordget;
            username = usernameget;
            title = titlenameget;
            itemtype = itemtypeget;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            titlename.Text = title;
            usern.Text = username;
            passwordn.Text = password;

            switch (itemtype)
            {
                case "Website":
                    itemcolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                    break;

                case "Email":
                    itemcolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 151, 255));
                    break;

                case "Mobile":
                    itemcolor.Fill = new SolidColorBrush(Color.FromArgb(255, 133, 84, 255));
                    break;

                case "Official":
                    itemcolor.Fill = new SolidColorBrush(Color.FromArgb(255, 144, 255, 159));
                    break;

                case "Bank":
                    itemcolor.Fill = new SolidColorBrush(Color.FromArgb(255, 254, 255, 132));
                    break;

                case "Other":
                    itemcolor.Fill = new SolidColorBrush(Color.FromArgb(255, 232, 94, 94));
                    break;
            }
        }



        private void Backbtn(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
