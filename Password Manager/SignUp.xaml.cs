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
    public partial class SignUp : Window
    {
        public SignUp()
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
            SignUpPagemain.WindowState = WindowState.Minimized;
        }

        private void Buttonsignup(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = "INSERT INTO USERDETAILS(USERNAME, EMAIL, PASS) VALUES(@USERNAME, @EMAIL, @PASS)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Email", emailcreate.Text);
                sqlCmd.Parameters.AddWithValue("@Pass", passwordcreate.Text);
                sqlCmd.Parameters.AddWithValue("@Username", namecreate.Text);
                int count = sqlCmd.ExecuteNonQuery();


                if (count == 1)
                {
                    MessageBox.Show("Account Created");
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
        }

        private void Buttonreturnlogin(object sender, RoutedEventArgs e)
        {
            LoginPage dashboard = new LoginPage();
            dashboard.Show();
            this.Close();
        }

    }
}
