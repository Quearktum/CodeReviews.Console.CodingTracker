using System.Configuration;

namespace Coding_Tracker
{
     internal class Program
    {
        static string connectionString = ConfigurationManager.AppSettings.Get("ConnectionString");
        static void Main(string[] args)
        {

            DatabaseManager dbManager = new();
            GetUserinput userinput = new();


            dbManager.CreateTable(connectionString);
            userinput.MainMenu();
        }
    }
}