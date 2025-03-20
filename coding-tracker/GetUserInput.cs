using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using Coding_Tracker;

internal class GetUserinput
{
    CodingController codingController = new CodingController();
    internal void MainMenu()
    {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("\n\nMAIN MENU");
                System.Console.WriteLine("\nWhat would you like to do?");
                System.Console.WriteLine("\nType 0 to Close Application");
                System.Console.WriteLine("Type 1 to View All Records");
                System.Console.WriteLine("Type 2 to Insert Record");
                System.Console.WriteLine("Type 3 to Update Record");
                System.Console.WriteLine("Type 4 to Delete Record");
                System.Console.WriteLine("---------------------------------");

                string commandInput = Console.ReadLine();
                
                while (string.IsNullOrEmpty(commandInput))
                {
                    System.Console.WriteLine("\nPlease re-enter your command.");
                    commandInput = Console.ReadLine();
                }

                switch (commandInput)
                {
                    case "0":
                        System.Console.WriteLine("\nBye now\n");
                        Environment.Exit(0);
                        break;
                    case "1":
                        codingController.Get();
                        break;
                    case "2":
                        ProcessAdd();
                        break;
                    case "3":
                        Update();    
                        break;
                    case "4":
                        Delete();
                        break;
                    default:
                        System.Console.WriteLine("\n Invalid Command");
                        break;
                }
            }

    }

    private void Update()
    {
        codingController.Get();

        System.Console.WriteLine("\nPlease specify the id you want to update.");
        string commandInput = Console.ReadLine();

        while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
        {
            System.Console.WriteLine("\nThis is not a valid Id format");
            commandInput = Console.ReadLine();
        }  

        var id = Int32.Parse(commandInput);

        var coding = codingController.GetById(id);

        while (coding.Id == 0)
        {
            System.Console.WriteLine("\n\nInvalid Id");
            Update();   
        }

        bool updating = true;
        while (updating == true)
        {
            System.Console.WriteLine($"\nType d for Date");
            System.Console.WriteLine($"\nType t for Duration");
            System.Console.WriteLine($"\nType s for save the update");

            var updateInput = Console.ReadLine();

            switch (updateInput)
            {
                case "d":
                    coding.Date = GetDateInput();
                    break;
                case "t":
                    coding.Duration = GetDurationInput();
                    break;
                case "s":
                    updating = false;
                    break;                
                default:
                    System.Console.WriteLine("\nPlease type a valid input");
                    break;
            }

        }
            codingController.Update(coding);
            MainMenu();   

    }

    private void Delete()
    {
        codingController.Get();

        System.Console.WriteLine("\nPlease specify the id you want to delete.");
        string commandInput = Console.ReadLine();

        while (!Int32.TryParse(commandInput, out _) || string.IsNullOrEmpty(commandInput) || Int32.Parse(commandInput) < 0)
        {
            System.Console.WriteLine("\nThis is not a valid Id format");
            commandInput = Console.ReadLine();
        }  

        var id = Int32.Parse(commandInput);

        var coding = codingController.GetById(id);

        while (coding.Id == 0)
        {
            System.Console.WriteLine("\n\nInvalid Id");
            Delete();   
        }

        codingController.Delete(id);
    }

    private  void ProcessAdd()
    {
        var date = GetDateInput();
        var duration = GetDurationInput();
        
        Coding coding = new();

        coding.Date = date;
        coding.Duration = duration; 

        codingController.Post(coding);
    }

    private  string GetDurationInput()
    {
        System.Console.WriteLine("\n\nPlease insert the duration (hh:mm)");

        string durationInput = Console.ReadLine();

        if (durationInput == "0") 
            MainMenu();

        while (!TimeSpan.TryParseExact(durationInput, "h\\:mm", CultureInfo.InvariantCulture, out _))
        {
            System.Console.WriteLine("\nNot a valid format");
            durationInput = Console.ReadLine(); 
        }            

        return durationInput;

    }

    internal string GetDateInput()
    {            
        System.Console.WriteLine("\n\nPlease insert the date (dd-mm-yy)");

        string dateInput = Console.ReadLine();

        if (dateInput == "0") 
            MainMenu();

        while (!DateTime.TryParseExact(dateInput, "dd-MM-yy", new CultureInfo("en-US"), DateTimeStyles.None, out _))
        {
            System.Console.WriteLine("\nNot a valid format");
            dateInput = Console.ReadLine(); 
        }            

        return dateInput;
    }
}
