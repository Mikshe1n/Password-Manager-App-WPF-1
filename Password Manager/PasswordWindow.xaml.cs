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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        public string username = LoginPage.username;

        public string usernamemethod(string name) //Just to change go
        {
            username = name;
            return name;
        }
        public PasswordWindow(string name)
        {
            InitializeComponent();
            usernamemethod(name);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Buttonclose(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow dashboard = new MainWindow(username);
            dashboard.Show();

        }

        private void Buttonminimise(object sender, RoutedEventArgs e)
        {
            PasswordPageMain.WindowState = WindowState.Minimized;
        }


    }

}
