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
    /// Interaction logic for AddPage.xaml
    /// </summary>

    
    public partial class AddPage : Window
    {
        public string username = LoginPage.username;
        public AddPage(string name)
        {
            InitializeComponent();
            var username = LoginPage.username;
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
            AddPageMain.WindowState = WindowState.Minimized;
        }

        private void Buttoncreate(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source= localhost\sqlexpress; Initial Catalog=PasswordManager; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            
            try
            {
                if (sqlCon.State == System.Data.ConnectionState.Closed)
                    sqlCon.Open();
                string query = "INSERT INTO MainData (USERID, Name, USERNAME, PASSWORD, Description, Type) VALUES(@Us, @Name, @Username,  @Password, @Description, @Type)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.CommandType = System.Data.CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Name", namecreate.Text);
                sqlCmd.Parameters.AddWithValue("@Username", usernamecreate.Text);
                sqlCmd.Parameters.AddWithValue("@Password", passwordcreate.Text);
                sqlCmd.Parameters.AddWithValue("@Description", descriptioncreate.Text);
                sqlCmd.Parameters.AddWithValue("@Us", username);
                if (type.Text == "")
                    type.Text = "Other";
                sqlCmd.Parameters.AddWithValue("@Type", type.Text);
                int count = sqlCmd.ExecuteNonQuery();
                 
                if (count == 1)
                {
                    MessageBox.Show("Item Added");
                    this.Close();
                    MainWindow dashboard = new MainWindow(username);
                    dashboard.Show();
                }
                else
                {
                    MessageBox.Show("Can't create item!");
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
    }
}
