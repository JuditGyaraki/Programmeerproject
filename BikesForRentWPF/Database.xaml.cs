using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using BC = BCrypt.Net.BCrypt;
using BikesForRentWPF.Models;



namespace BikesForRentWPF;

/// <summary>
/// Interaction logic for Database.xaml
/// </summary>
public partial class Database : Page
{   
    public BikesDbContext db = new BikesDbContext();
    public Database()
    {
        InitializeComponent();
    }

    private void btnUpdatePassword_Click(object sender, EventArgs e)
    {
        var usersList = db.Users.OrderBy(s => s.Id).ToList();


        string emailUser = tbx_user_email.Text;

        var gekozenUser = db.Users
                            .FirstOrDefault(u => u.Email == emailUser);

        if (gekozenUser == null)
        {
            MessageBox.Show("User niet gevonden!");
            return;
        }


        string nieuwWachtwoord = tbx_user_wachtwoord.Text;
        string passwordHash = BC.HashPassword(nieuwWachtwoord);
        gekozenUser.Password = Encoding.UTF8.GetBytes(passwordHash);

        db.SaveChanges();

        MessageBox.Show("Wachtwoord aangepast!");

    }


    //public static string Hoofdletter(string input)
    //{
    //    if (string.IsNullOrWhiteSpace(input))
    //        return input;

    //    return char.ToUpper(input[0]) + input.Substring(1).ToLower();
    //}

}
