using BikesForRentWPF.Models;
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
using Microsoft.EntityFrameworkCore;


namespace BikesForRentWPF
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public BikesDbContext db = new BikesDbContext();
        public Login()
        {
            InitializeComponent();
            //frame.Navigate(new Login());
        }
        private bool LoginUser(string email, string password)
        {
            var user = db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email);

            if (user == null)
                return false;

            string hashedPassword = Encoding.UTF8.GetString(user.Password);

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        private void btnInloggen_Click(object sender, RoutedEventArgs e)
        {
            string emailUser = tbx_user_email.Text;
            string password = pwb_user_wachtwoord.Password;
            var user = db.Users.FirstOrDefault(u => u.Email == emailUser);


            if (LoginUser(emailUser, password))
            {
                MessageBox.Show("Login succesvol!");

                Session.UserId = user.Id;
                Session.Role = user.Role.Rolename;

                if (user.RoleId == 1 || user.RoleId == 3)
                {
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.frame.Navigate(new AdminHomepage()); //voor een clean page
                    
                    
                }
                else if (user.RoleId == 2)
                {
                    //navigate to medewerker homepage
                }
                else
                {
                    //navigate to guest homepage
                }
                // NavigationService.Navigate(new HomePage());
            }
            else
            {
                MessageBox.Show("Login mislukt.");
            }
        }

    }  
}
