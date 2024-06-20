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
    /// Interaction logic for LoginPage.xaml
    /// </summary>

    
    public partial class LoginPage : Window
    {
        public static string username { get; private set; }
        public LoginPage()
        {
            InitializeComponent();

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
            Close();
        }

        private void Buttonminimise(object sender, RoutedEventArgs e)
        {
            LoginPagemain.WindowState = WindowState.Minimized;
        }

        
        private void Buttonlogin(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = "SELECT COUNT(1) FROM UserDetails WHERE Username=@Username AND Pass = @Pass";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Username", usernameinput.Text);
                sqlCmd.Parameters.AddWithValue("@Pass", passwordinput.Text);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                
                if (count == 1)
                {

                    MainWindow dashboard = new MainWindow(usernameinput.Text);
                    dashboard.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Email or Password Incorrect");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlCon.Close();
            }

            username = usernameinput.Text;
        }

        private void SignUpPage(object sender, RoutedEventArgs e)
        {
            SignUp dashboard = new SignUp();
            dashboard.Show();
            this.Close();
        }

        private void usernameinput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }


}
