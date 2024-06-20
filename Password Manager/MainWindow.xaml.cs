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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace Password_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public string username;
        public bool deletestate = false; // check if main delete button is clicked
        public bool editstate = false; // check if main edit button is clicked
        public static string itemname { get; private set; }
        public static string itempassword { get; private set; }

        public string usernamemethod(string name) //Just to change go
        {
            username = name;
            return name;
        }
        public MainWindow(string name)
        {
            InitializeComponent();
            usernamemethod(name);
        }

        private void itemnamemethod(object sender, RoutedEventArgs e)
        {
            string itemnametemp = (string)((Button)sender).Tag;
            itemname = itemnametemp;
        }

        private void itempasswordmethod(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            string CmdString = "SELECT password FROM MainData where name = @itemname";
            SqlCommand cmd = new SqlCommand(CmdString, con);
            cmd.Parameters.AddWithValue("@itemname", itemname);
        }

        public static string displaypassword { get; private set; }

        public static string itemtype { get; private set; }

        private void SortCount()
        {
            SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            //Count number of website, emails etc.
            string websearch = "SELECT Count(*) from MainData WHERE Type='Website' and UserId = @Us";
            SqlCommand webfind = new SqlCommand(websearch, con);
            webfind.Parameters.AddWithValue("@Us", username);
            int websitec = (int)webfind.ExecuteScalar();
            websitecount.Text = Convert.ToString(websitec);

            string emailsearch = "SELECT Count(*) from MainData WHERE Type='Email' and UserId = @Us";
            //This is just a test 
            SqlCommand emailfind = new SqlCommand(emailsearch, con);
            emailfind.Parameters.AddWithValue("@Us", username);
            int emailc = (int)emailfind.ExecuteScalar();
            emailcount.Text = Convert.ToString(emailc);

            string mobilesearch = "SELECT Count(*) from MainData WHERE Type='Mobile' and UserId = @Us";
            SqlCommand mobilefind = new SqlCommand(mobilesearch, con);
            mobilefind.Parameters.AddWithValue("@Us", username);
            int mobilec = (int)mobilefind.ExecuteScalar();
            mobilecount.Text = Convert.ToString(mobilec);

            string officialsearch = "SELECT Count(*) from MainData WHERE Type='Official' and UserId = @Us";
            SqlCommand officialfind = new SqlCommand(officialsearch, con);
            officialfind.Parameters.AddWithValue("@Us", username);
            int officialc = (int)officialfind.ExecuteScalar();
            officialcount.Text = Convert.ToString(officialc);

            string banksearch = "SELECT Count(*) from MainData WHERE Type='Bank' and UserId = @Us";
            SqlCommand bankfind = new SqlCommand(banksearch, con);
            bankfind.Parameters.AddWithValue("@Us", username);
            int bankc = (int)bankfind.ExecuteScalar();
            bankcount.Text = Convert.ToString(bankc);

            string othersearch = "SELECT Count(*) from MainData WHERE Type='Other' and UserId = @Us";
            SqlCommand otherfind = new SqlCommand(othersearch, con);
            otherfind.Parameters.AddWithValue("@Us", username);
            int otherc = (int)otherfind.ExecuteScalar();
            othercount.Text = Convert.ToString(otherc);
        }

        private void Window_Loaded()
        {
            buttonall.IsEnabled = false;
            buttonall.Opacity = 0;
            MyPanel.Children.Clear();
            deletestate = false;
            SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                string CmdString = "SELECT name FROM MainData where Userid = @Us";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@Us", username);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.CommandText = "SELECT (Name) from MainData WHERE UserId = @Us";
                string names = (string)cmd.ExecuteScalar();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> str = new List<string>();



                while (reader.Read())
                {
                    str.Add(reader.GetString(0));
                }

                foreach (var mon in str) // Creation of EACH item boxes
                {
                    Button namebutton = new Button();
                    StackPanel insidebutton = new StackPanel();
                    insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                    insidebutton.VerticalAlignment = VerticalAlignment.Top;
                    insidebutton.Height = 80;

                    Label itemname = new Label();
                    TextBlock itemnameblock = new TextBlock();
                    itemname.Content = itemnameblock;
                    itemnameblock.Text = mon;
                    itemnameblock.Tag = mon;
                    itemnameblock.TextWrapping = TextWrapping.Wrap;
                    itemname.Foreground = Brushes.White;
                    itemname.HorizontalAlignment = HorizontalAlignment.Center;
                    itemname.VerticalContentAlignment = VerticalAlignment.Center;
                    itemname.HorizontalContentAlignment = HorizontalAlignment.Center;
                    itemname.Height = 70;
                    itemname.Width = 80;
                    Ellipse typecolor = new Ellipse();
                    typecolor.Height = 5;
                    typecolor.Width = 5;
                    //finding the circle color
                    string typesearch = "SELECT Type from MainData WHERE UserId = @Us and Name=@Name";
                    SqlCommand typefinding = new SqlCommand(typesearch, con);
                    typefinding.Parameters.AddWithValue("@Us", username);
                    typefinding.Parameters.AddWithValue("@Name", mon);
                    string type = (string)typefinding.ExecuteScalar();


                    switch (type)
                    {
                        case "Website":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                            break;

                        case "Email":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 151, 255));
                            break;

                        case "Mobile":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 133, 84, 255));
                            break;

                        case "Official":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 144, 255, 159));
                            break;

                        case "Bank":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 254, 255, 132));
                            break;

                        case "Other":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 232, 94, 94));
                            break;
                    }


                    typecolor.Margin = new Thickness(0, 0, 65, 0);
                    typecolor.VerticalAlignment = VerticalAlignment.Top;
                    typecolor.HorizontalAlignment = HorizontalAlignment.Left;
                    namebutton.Content = insidebutton;
                    namebutton.BorderThickness = new Thickness(1);
                    namebutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                    namebutton.Margin = new Thickness(5);
                    namebutton.Width = 80;
                    namebutton.Height = 80;
                    namebutton.Tag = mon;
                    namebutton.Click += itemnamemethod;
                    namebutton.Click += PasswordPage;
                    namebutton.Width = 80;
                    namebutton.Height = 80;
                    insidebutton.Children.Add(typecolor);
                    insidebutton.Children.Add(itemname);
                    MyPanel.Children.Add(namebutton);
                    reader.Close();
                }
                reader.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            SortCount();
        }

        private void WindowStart(object sender, RoutedEventArgs e)
        {
            Window_Loaded();
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
            MainPage.WindowState = WindowState.Minimized;
        }

        private void ButtonAddPage(object sender, RoutedEventArgs e)
        {
            AddPage dashboard = new AddPage(username);
            dashboard.ShowDialog();
            this.Close();
        }

        private void Buttonreturnlogin(object sender, RoutedEventArgs e)
        {
            LoginPage dashboard = new LoginPage();
            dashboard.Show();
            this.Close();
        }

        private void PasswordWindow(object sender, RoutedEventArgs e)
        {
            PasswordWindow dashboard = new PasswordWindow(username);
            dashboard.Show();
            this.Close();
        }

        private void PasswordPage(object sender, RoutedEventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            if (con.State == System.Data.ConnectionState.Closed)
                con.Open();
            string passwordfinding = "SELECT Password from MainData WHERE Name=@Name and UserId = @Us";
            SqlCommand passwordfind = new SqlCommand(passwordfinding, con);
            passwordfind.Parameters.AddWithValue("@Us", username);
            passwordfind.Parameters.AddWithValue("@Name", itemname);
            string passwordstring = (string)passwordfind.ExecuteScalar();
            displaypassword = passwordstring;

            string typefinding = "SELECT Type from MainData WHERE Name=@Name and UserId = @Us";
            SqlCommand typefind = new SqlCommand(typefinding, con);
            typefind.Parameters.AddWithValue("@Us", username);
            typefind.Parameters.AddWithValue("@Name", itemname);
            string itemtype = (string)typefind.ExecuteScalar();

            string usernameidfinding = "SELECT username from MainData WHERE Name=@Name and UserId = @Us";
            SqlCommand usernameidfind = new SqlCommand(usernameidfinding, con);
            usernameidfind.Parameters.AddWithValue("@Us", username);
            usernameidfind.Parameters.AddWithValue("@Name", itemname);
            string usernameid = (string)usernameidfind.ExecuteScalar();

            PasswordPage dashboard = new PasswordPage(itemname, usernameid, displaypassword, itemtype);


            PasswordPanel.Content = null;
            PasswordPanel.Content = dashboard;

        }

        private void ButtonSort(object sender, RoutedEventArgs e)
        {
            buttonall.Opacity = 255;
            buttonall.IsEnabled = true;
            string buttonText = ((Button)sender).Name;
            string sortfor = "";
            switch (buttonText)
            {
                case "buttonwebsite":
                    sortfor += "Website";
                    break;
                case "buttonemail":
                    sortfor += "Email";
                    break;
                case "buttonmobile":
                    sortfor += "Mobile";
                    break;
                case "buttonofficial":
                    sortfor += "Official";
                    break;
                case "buttonbank":
                    sortfor += "Bank";
                    break;
                case "buttonother":
                    sortfor += "Other";
                    break;
            }

            MyPanel.Children.Clear();
            SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                string CmdString = "SELECT name FROM MainData where Userid = @Us and Type=@Type";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@Us", username);
                cmd.Parameters.AddWithValue("@Type", sortfor);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.CommandText = "SELECT (Name) from MainData WHERE UserId = @Us and Type=@Type";
                string names = (string)cmd.ExecuteScalar();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> str = new List<string>();



                while (reader.Read())
                {
                    str.Add(reader.GetString(0));
                }

                foreach (var mon in str)
                {
                    Button namebutton = new Button();
                    StackPanel insidebutton = new StackPanel();
                    insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                    insidebutton.VerticalAlignment = VerticalAlignment.Top;
                    insidebutton.Height = 80;

                    Label itemname = new Label();
                    TextBlock itemnameblock = new TextBlock();
                    itemname.Content = itemnameblock;
                    itemnameblock.Text = mon;
                    itemnameblock.Tag = mon;
                    itemnameblock.TextWrapping = TextWrapping.Wrap;
                    itemname.Foreground = Brushes.White;
                    itemname.HorizontalAlignment = HorizontalAlignment.Center;
                    itemname.VerticalContentAlignment = VerticalAlignment.Center;
                    itemname.HorizontalContentAlignment = HorizontalAlignment.Center;
                    itemname.Height = 70;
                    itemname.Width = 80;
                    Ellipse typecolor = new Ellipse();
                    typecolor.Height = 5;
                    typecolor.Width = 5;
                    //finding the circle color
                    string typesearch = "SELECT Type from MainData WHERE UserId = @Us and Name=@Name";
                    SqlCommand typefinding = new SqlCommand(typesearch, con);
                    typefinding.Parameters.AddWithValue("@Us", username);
                    typefinding.Parameters.AddWithValue("@Name", mon);
                    string type = (string)typefinding.ExecuteScalar();


                    switch (type)
                    {
                        case "Website":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                            break;

                        case "Email":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 151, 255));
                            break;

                        case "Mobile":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 133, 84, 255));
                            break;

                        case "Official":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 144, 255, 159));
                            break;

                        case "Bank":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 254, 255, 132));
                            break;

                        case "Other":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 232, 94, 94));
                            break;
                    }


                    typecolor.Margin = new Thickness(0, 0, 65, 0);
                    typecolor.VerticalAlignment = VerticalAlignment.Top;
                    typecolor.HorizontalAlignment = HorizontalAlignment.Left;
                    namebutton.Content = insidebutton;
                    namebutton.BorderThickness = new Thickness(1);
                    namebutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                    namebutton.Margin = new Thickness(5);
                    namebutton.Width = 80;
                    namebutton.Height = 80;
                    namebutton.Tag = mon;
                    namebutton.Click += itemnamemethod;
                    namebutton.Click += PasswordPage;
                    namebutton.Width = 80;
                    namebutton.Height = 80;
                    insidebutton.Children.Add(typecolor);
                    insidebutton.Children.Add(itemname);
                    MyPanel.Children.Add(namebutton);
                    reader.Close();
                }
                reader.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void searchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            buttonall.Opacity = 0;
            string searchtext = searchbox.Text;

            MyPanel.Children.Clear();
            SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            try
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                string CmdString = "SELECT name FROM MainData where Userid = @Us and name like @Search+ '%'";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@Us", username);
                cmd.Parameters.AddWithValue("@Search", searchtext);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.CommandText = "SELECT name FROM MainData where Userid = @Us and name like @Search + '%'";
                string names = (string)cmd.ExecuteScalar();

                SqlDataReader reader = cmd.ExecuteReader();
                List<string> str = new List<string>();



                while (reader.Read())
                {
                    str.Add(reader.GetString(0));
                }

                foreach (var mon in str)
                {
                    Button namebutton = new Button();
                    StackPanel insidebutton = new StackPanel();
                    insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                    insidebutton.VerticalAlignment = VerticalAlignment.Top;
                    insidebutton.Height = 80;

                    Label itemname = new Label();
                    TextBlock itemnameblock = new TextBlock();
                    itemname.Content = itemnameblock;
                    itemnameblock.Text = mon;
                    itemnameblock.Tag = mon;
                    itemnameblock.TextWrapping = TextWrapping.Wrap;
                    itemname.Foreground = Brushes.White;
                    itemname.HorizontalAlignment = HorizontalAlignment.Center;
                    itemname.VerticalContentAlignment = VerticalAlignment.Center;
                    itemname.HorizontalContentAlignment = HorizontalAlignment.Center;
                    itemname.Height = 70;
                    itemname.Width = 80;
                    Ellipse typecolor = new Ellipse();
                    typecolor.Height = 5;
                    typecolor.Width = 5;
                    //finding the circle color
                    string typesearch = "SELECT Type from MainData WHERE UserId = @Us and Name=@Name";
                    SqlCommand typefinding = new SqlCommand(typesearch, con);
                    typefinding.Parameters.AddWithValue("@Us", username);
                    typefinding.Parameters.AddWithValue("@Name", mon);
                    string type = (string)typefinding.ExecuteScalar();


                    switch (type)
                    {
                        case "Website":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                            break;

                        case "Email":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 151, 255));
                            break;

                        case "Mobile":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 133, 84, 255));
                            break;

                        case "Official":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 144, 255, 159));
                            break;

                        case "Bank":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 254, 255, 132));
                            break;

                        case "Other":
                            typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 232, 94, 94));
                            break;
                    }


                    typecolor.Margin = new Thickness(0, 0, 65, 0);
                    typecolor.VerticalAlignment = VerticalAlignment.Top;
                    typecolor.HorizontalAlignment = HorizontalAlignment.Left;
                    namebutton.Content = insidebutton;
                    namebutton.BorderThickness = new Thickness(1);
                    namebutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                    namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                    namebutton.Margin = new Thickness(5);
                    namebutton.Width = 80;
                    namebutton.Height = 80;
                    namebutton.Tag = mon;
                    namebutton.Click += itemnamemethod;
                    namebutton.Click += PasswordPage;
                    namebutton.Width = 80;
                    namebutton.Height = 80;
                    insidebutton.Children.Add(typecolor);
                    insidebutton.Children.Add(itemname);
                    MyPanel.Children.Add(namebutton);
                    reader.Close();
                }
                reader.Close();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            if (deletestate == true) {
                buttondelete.Background = new SolidColorBrush(Color.FromArgb(0, 232, 94, 94));
                MyPanel.Children.Clear();
                Window_Loaded();
                deletestate = false;
            }

            else
            {
                MyPanel.Children.Clear();
                buttondelete.Background = new SolidColorBrush(Color.FromArgb(200, 232, 94, 94));

                buttonall.IsEnabled = false;
                buttonall.Opacity = 0;
                MyPanel.Children.Clear();
                SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
                try
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();
                    string CmdString = "SELECT name FROM MainData where Userid = @Us";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@Us", username);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandText = "SELECT (Name) from MainData WHERE UserId = @Us";
                    string names = (string)cmd.ExecuteScalar();

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<string> str = new List<string>();



                    while (reader.Read())
                    {
                        str.Add(reader.GetString(0));
                    }

                    foreach (var mon in str)
                    {
                        Button namebutton = new Button();
                        StackPanel insidebutton = new StackPanel();
                        Grid insidebuttontop = new Grid();
                        insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                        insidebutton.VerticalAlignment = VerticalAlignment.Top;
                        insidebutton.Height = 80;

                        Label itemname = new Label();
                        TextBlock itemnameblock = new TextBlock();
                        itemname.Content = itemnameblock;
                        itemnameblock.Text = mon;
                        itemnameblock.Tag = mon;
                        itemnameblock.TextWrapping = TextWrapping.Wrap;
                        itemname.Foreground = Brushes.White;
                        itemname.HorizontalAlignment = HorizontalAlignment.Center;
                        itemname.VerticalContentAlignment = VerticalAlignment.Center;
                        itemname.HorizontalContentAlignment = HorizontalAlignment.Center;
                        itemname.Height = 70;
                        itemname.Width = 80;

                        Ellipse typecolor = new Ellipse();
                        typecolor.Height = 5;
                        typecolor.Width = 5;

                        Button delbutton = new Button();
                        delbutton.Content = "   ";
                        delbutton.Foreground = Brushes.Red;
                        delbutton.HorizontalAlignment = HorizontalAlignment.Right;
                        delbutton.Height = 4;
                        delbutton.Background = new SolidColorBrush(Color.FromArgb(200, 255, 0, 0));
                        delbutton.BorderThickness = new Thickness(0);
                        delbutton.Tag = mon;
                        delbutton.Click += Deleteitem;

                        //finding the circle color
                        string typesearch = "SELECT Type from MainData WHERE UserId = @Us and Name=@Name";
                        SqlCommand typefinding = new SqlCommand(typesearch, con);
                        typefinding.Parameters.AddWithValue("@Us", username);
                        typefinding.Parameters.AddWithValue("@Name", mon);
                        string type = (string)typefinding.ExecuteScalar();


                        switch (type)
                        {
                            case "Website":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                                break;

                            case "Email":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 151, 255));
                                break;

                            case "Mobile":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 133, 84, 255));
                                break;

                            case "Official":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 144, 255, 159));
                                break;

                            case "Bank":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 254, 255, 132));
                                break;

                            case "Other":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 232, 94, 94));
                                break;
                        }


                        typecolor.Margin = new Thickness(0, 0, 65, 0);
                        typecolor.VerticalAlignment = VerticalAlignment.Top;
                        typecolor.HorizontalAlignment = HorizontalAlignment.Left;
                        namebutton.Content = insidebutton;
                        namebutton.BorderThickness = new Thickness(1);
                        namebutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                        namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                        namebutton.Margin = new Thickness(5);
                        namebutton.Width = 80;
                        namebutton.Height = 80;
                        namebutton.Tag = mon;
                        namebutton.Click += itemnamemethod;
                        namebutton.Width = 80;
                        namebutton.Height = 80;

                        insidebuttontop.Children.Add(typecolor);
                        insidebuttontop.Children.Add(delbutton);
                        insidebutton.Children.Add(insidebuttontop);
                        insidebutton.Children.Add(itemname);
                        MyPanel.Children.Add(namebutton);
                        reader.Close();
                    }
                    reader.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                SortCount();  //Important
                deletestate = true;
            }
            
        }

        private void Deleteitem(object sender, RoutedEventArgs e)
        {

            try
            {
                string titlename = (string)(sender as Button).Tag;

                SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                string deleterow = "delete from MainData WHERE Name=@Name";
                SqlCommand delete = new SqlCommand(deleterow, con);
                delete.Parameters.AddWithValue("@Us", username);
                delete.Parameters.AddWithValue("@Name", titlename);
                delete.ExecuteNonQuery();
                buttondelete.Background = new SolidColorBrush(Color.FromArgb(0, 232, 94, 94));
                MessageBox.Show(titlename + " is deleted");
            }


            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            SortCount();
            Window_Loaded();
        }
        private void ButtonEdit(object sender, RoutedEventArgs e)
        {
            if (editstate == true)
            {
                buttonedit.Background = new SolidColorBrush(Color.FromArgb(0, 232, 94, 94));
                MyPanel.Children.Clear();
                Window_Loaded();
                editstate = false;
            }
            else
            {
                MyPanel.Children.Clear();
                buttonedit.Background = new SolidColorBrush(Color.FromArgb(200, 232, 94, 94));
                buttonall.IsEnabled = false;
                buttonall.Opacity = 0;
                MyPanel.Children.Clear();
                SqlConnection con = new SqlConnection(@"Data Source= DESKTOP-O99C3DO\SQLEXPRESS; Initial Catalog=PasswordManager; MultipleActiveResultSets=True; Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
                try
                {
                    if (con.State == System.Data.ConnectionState.Closed)
                        con.Open();
                    string CmdString = "SELECT name FROM MainData where Userid = @Us";
                    SqlCommand cmd = new SqlCommand(CmdString, con);
                    cmd.Parameters.AddWithValue("@Us", username);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    cmd.CommandText = "SELECT (Name) from MainData WHERE UserId = @Us";
                    string names = (string)cmd.ExecuteScalar();

                    SqlDataReader reader = cmd.ExecuteReader();
                    List<string> str = new List<string>();


                    while (reader.Read())
                    {
                        str.Add(reader.GetString(0));
                    }

                    foreach (var mon in str)
                    {
                        Button namebutton = new Button();
                        StackPanel insidebutton = new StackPanel();
                        Grid insidebuttontop = new Grid();
                        insidebutton.HorizontalAlignment = HorizontalAlignment.Left;
                        insidebutton.VerticalAlignment = VerticalAlignment.Top;
                        insidebutton.Height = 80;

                        Label itemname = new Label();
                        TextBlock itemnameblock = new TextBlock();
                        itemname.Content = itemnameblock;
                        itemnameblock.Text = mon;
                        itemnameblock.Tag = mon;
                        itemnameblock.TextWrapping = TextWrapping.Wrap;
                        itemname.Foreground = Brushes.White;
                        itemname.HorizontalAlignment = HorizontalAlignment.Center;
                        itemname.VerticalContentAlignment = VerticalAlignment.Center;
                        itemname.HorizontalContentAlignment = HorizontalAlignment.Center;
                        itemname.Height = 70;
                        itemname.Width = 80;

                        Ellipse typecolor = new Ellipse();
                        typecolor.Height = 5;
                        typecolor.Width = 5;

                        Button edbutton = new Button();
                        edbutton.Content = "   ";
                        edbutton.Foreground = Brushes.Red;
                        edbutton.HorizontalAlignment = HorizontalAlignment.Right;
                        edbutton.Height = 4;
                        edbutton.Background = new SolidColorBrush(Color.FromArgb(200, 255, 255, 255));
                        edbutton.BorderThickness = new Thickness(0);
                        edbutton.Tag = mon;
                        edbutton.Click += Edititem;

                        //finding the circle color
                        string typesearch = "SELECT Type from MainData WHERE UserId = @Us and Name=@Name";
                        SqlCommand typefinding = new SqlCommand(typesearch, con);
                        typefinding.Parameters.AddWithValue("@Us", username);
                        typefinding.Parameters.AddWithValue("@Name", mon);
                        string type = (string)typefinding.ExecuteScalar();


                        switch (type)
                        {
                            case "Website":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 255, 210));
                                break;

                            case "Email":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 12, 151, 255));
                                break;

                            case "Mobile":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 133, 84, 255));
                                break;

                            case "Official":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 144, 255, 159));
                                break;

                            case "Bank":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 254, 255, 132));
                                break;

                            case "Other":
                                typecolor.Fill = new SolidColorBrush(Color.FromArgb(255, 232, 94, 94));
                                break;
                        }


                        typecolor.Margin = new Thickness(0, 0, 65, 0);
                        typecolor.VerticalAlignment = VerticalAlignment.Top;
                        typecolor.HorizontalAlignment = HorizontalAlignment.Left;
                        namebutton.Content = insidebutton;
                        namebutton.BorderThickness = new Thickness(1);
                        namebutton.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
                        namebutton.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 255, 255, 255));
                        namebutton.Margin = new Thickness(5);
                        namebutton.Width = 80;
                        namebutton.Height = 80;
                        namebutton.Tag = mon;
                        namebutton.Click += itemnamemethod;
                        namebutton.Width = 80;
                        namebutton.Height = 80;

                        insidebuttontop.Children.Add(typecolor);
                        insidebuttontop.Children.Add(edbutton);
                        insidebutton.Children.Add(insidebuttontop);
                        insidebutton.Children.Add(itemname);
                        MyPanel.Children.Add(namebutton);
                        reader.Close();
                    }
                    reader.Close();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                SortCount();  //Important
                editstate = true;
            }
        }

        private void Edititem(object sender, RoutedEventArgs e)
        {
            string titlename = (string)(sender as Button).Tag;

            EditPage dashboard = new EditPage(username, titlename, titlename);
            dashboard.ShowDialog();
            this.Close();

            SortCount();
            Window_Loaded();
        }
    }
    }


