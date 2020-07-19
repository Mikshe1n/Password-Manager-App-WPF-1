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
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Window
    {
        public string username = LoginPage.username;

        public string displayname;
        public string displayusername;
        public string displaypassword;
        public string displaytype;
        public string displaydescription;

        public EditPage(string name, string itemname, string password)
        {
            InitializeComponent();
            var username = LoginPage.username;
            displayname = itemname;
            nameedit.Text = itemname;

            SqlConnection con = new SqlConnection(@"Data Source= localhost\sqlexpress; Initial Catalog=PasswordManager; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();

            //find item's username to display
            string usernamefinding = "SELECT username FROM MainData where UserId=@username and name = @itemname";
            SqlCommand usernamefind = new SqlCommand(usernamefinding, con);
            usernamefind.Parameters.AddWithValue("@itemname", itemname);
            usernamefind.Parameters.AddWithValue("@username", username);
            string usernamestring = (string)usernamefind.ExecuteScalar();
            displayusername = usernamestring;

            //find item's password to display
            string passwordfinding = "SELECT password FROM MainData where UserId=@username and name = @itemname";
            SqlCommand passwordfind = new SqlCommand(passwordfinding, con);
            passwordfind.Parameters.AddWithValue("@itemname", itemname);
            passwordfind.Parameters.AddWithValue("@username", username);
            string passwordstring = (string)passwordfind.ExecuteScalar();
            displaypassword = passwordstring;

            //find item's type to display
            string typefinding = "SELECT Type from MainData WHERE Name=@Name and UserId = @Us";
            SqlCommand typefind = new SqlCommand(typefinding, con);
            typefind.Parameters.AddWithValue("@Us", username);
            typefind.Parameters.AddWithValue("@Name", itemname);
            displaytype = (string)typefind.ExecuteScalar();

            //find item's descripton to display
            string descriptionfinding = "SELECT Type from MainData WHERE Name=@Name and UserId = @Us";
            SqlCommand descriptionfind = new SqlCommand(descriptionfinding, con);
            descriptionfind.Parameters.AddWithValue("@Us", username);
            descriptionfind.Parameters.AddWithValue("@Name", itemname);
            displaydescription = (string)typefind.ExecuteScalar();

            passwordedit.Text = displaypassword;
            usernameedit.Text = displayusername;
            type.Text = displaytype;
            descriptionedit.Text = displaydescription;

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
            EditPageMain.WindowState = WindowState.Minimized;
        }

        public EditPage()
        {
            InitializeComponent();
        }

        private void Buttonedit(object sender, RoutedEventArgs e)
        {
            try
            {
                string titlename = (string)(sender as Button).Tag;

                SqlConnection con = new SqlConnection(@"Data Source= localhost\sqlexpress; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                string updateinfo = "Update MainData SET Name=@NewName, Username=@NewUsername, Password=@NewPassword, Type=@NewType, Description=@NewDescription WHERE Name=@Name and UserId=@Us";
                SqlCommand edit = new SqlCommand(updateinfo, con);
                edit.Parameters.AddWithValue("@Us", username);
                edit.Parameters.AddWithValue("@Name", displayname);
                edit.Parameters.AddWithValue("@NewName", nameedit.Text);
                edit.Parameters.AddWithValue("@NewUsername", usernameedit.Text);
                edit.Parameters.AddWithValue("@NewPassword", passwordedit.Text);
                edit.Parameters.AddWithValue("@NewType", type.Text);
                edit.Parameters.AddWithValue("@NewDescription", descriptionedit.Text);
                edit.ExecuteNonQuery();

                MessageBox.Show("Your information is updated");
                this.Close();
                MainWindow dashboard = new MainWindow(username);
                dashboard.Show();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //SortCount();
        }
    }
}
