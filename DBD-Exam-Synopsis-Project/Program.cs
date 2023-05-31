
using DBD_Exam_Synopsis_Project;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Diagnostics;

IStoredProcedures storedProcedures = new StoredProcedures();
bool run = true;

while (run)
{
    Console.WriteLine("\nWelcome to the Friends Database!");
    Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");

    bool runProcedures = true;
    while (runProcedures)
    {
        Console.WriteLine("\nHere's a list of all available procedures:");
        Console.WriteLine("1. Create a new person");
        Console.WriteLine("2. Create a new friendship");
        Console.WriteLine("3. Update person name");
        Console.WriteLine("4. Delete a person");
        Console.WriteLine("5. Get all persons");
        Console.WriteLine("6. Get a person");
        Console.WriteLine("7. Get all friends of person");
        Console.WriteLine("8. Get all friends of person reciprocal");
        Console.WriteLine("9. Get all friends of friends of person");

        Console.WriteLine("\nInput the corresponding number then press the enter key to invoke it");
        Console.WriteLine("Or press \"q\" then enter to quit");

        string? userInput = Console.ReadLine();

        if (userInput == "q" || userInput == "Q" || userInput == "quit" || userInput == "Quit")
        {
            run = false;
            break;
        }

        if (userInput != "1" && userInput != "2" && userInput != "3" && userInput != "4" && userInput != "5" && userInput != "6" && userInput != "7" && userInput != "8" && userInput != "9")
        {
            Console.WriteLine("\nInvalid input. Please input a valid procedure number or press \"q\" or input \"quit\" then press the enter key to quit out");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        switch (userInput)
        {
            case "1":
                ConsoleCreatePerson();
                runProcedures = false;
                break;
            case "2":
                ConsoleCreatePersonFriend();
                runProcedures = false;
                break;

            case "3":
                ConsoleUpdatePersonName();
                runProcedures = false;
                break;

            case "4":
                ConsoleDeletePerson();
                runProcedures = false;
                break;

            case "5":
                ConsoleGetAllPersons();
                runProcedures = false;
                break;

            case "6":
                ConsoleGetPerson();
                runProcedures = false;
                break;

            case "7":
                ConsoleGetAllFriendsOfPerson();
                runProcedures = false;
                break;
            case "8":
                ConsoleGetAllFriendsOfPersonReciprocal();
                runProcedures = false;
                break;

            case "9":
                ConsoleGetAllFriendsOfFriendsPerson();
                runProcedures = false;
                break;

            default:
                Console.WriteLine("\nReached unreachable code, which means something went wrong");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
                break;
        }


    }
}

void ConsoleCreatePerson()
{
    Console.WriteLine("\ncreate a Person");
    string? PName = null;

    while (PName == null)
    {
        Console.WriteLine("Please enter a name for the person and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }
        
        PName = userInput;

        if (PName == null)
        {
            Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

       
        try
        {
            Stopwatch sw = Stopwatch.StartNew();
            storedProcedures.CreatePerson(PName);
            long time = sw.ElapsedMilliseconds;
            Console.WriteLine("\nSucces");
            Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        catch (SqlException e)
        {
            Console.WriteLine("\nAn SQl error occured: " + e.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();

        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occured: " + ex.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }




    }
}

void ConsoleCreatePersonFriend()
{
    Console.WriteLine("\ncreate a Person and friend relation");
    int? PId1 = null;
    int? PId2 = null;

    while (PId1 == null)
    {
        Console.WriteLine("Please enter the id of the first person and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }

        var isNumeric = int.TryParse(userInput, out int number);
        if (!isNumeric)
        {
            Console.WriteLine("\nInvalid input. Please enter a valid id for the first person or press b or back to go back to the procedure select menu");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        PId1 = number;




    }

    while (PId2 == null)
    {
        Console.WriteLine("Please enter the id of the second person and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }

        var isNumeric = int.TryParse(userInput, out int number);
        if (!isNumeric)
        {
            Console.WriteLine("\nInvalid input. Please enter a valid id for the second person or press b or back to go back to the procedure select menu");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        PId2 = number;
    }
    try
    {
        Stopwatch sw = Stopwatch.StartNew();
        int affectedRows = storedProcedures.CreatePersonFriend((int)PId1, (int)PId2);
        long time = sw.ElapsedMilliseconds;
        Console.WriteLine("\nSucces");
        Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
        Console.WriteLine("Affected rows: " + affectedRows);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (SqlException e)
    {
        Console.WriteLine("\nAn SQl error occured: " + e.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("\nAn error occured: " + ex.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
}

void ConsoleDeletePerson()
{
    Console.WriteLine("\nDelete a person");
    int? PId = null;

    while (PId == null)
    {
        Console.WriteLine("Please enter the id of the person you want to delete and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }

        var isNumeric = int.TryParse(userInput, out int number);
        if (!isNumeric)
        {
            Console.WriteLine("\nInvalid input. Please enter a valid id for the person you want to delete or press b or back to go back to the procedure select menu");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        PId = number;
    }
    try
    {

        Stopwatch sw = Stopwatch.StartNew();
        int affectedRows = storedProcedures.DeletePerson((int)PId);
        long time = sw.ElapsedMilliseconds;
        Console.WriteLine("\nSucces");
        Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
        Console.WriteLine("Affected rows: " + affectedRows);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (SqlException e)
    {
        Console.WriteLine("\nAn SQl error occured: " + e.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("\nAn error occured: " + ex.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
}

void ConsoleGetAllPersons()
{
    try
    {

        Stopwatch sw = Stopwatch.StartNew();
        storedProcedures.GetAllPersons();
        long time = sw.ElapsedMilliseconds;
        Console.WriteLine("\nSucces");
        Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (SqlException e)
    {
        Console.WriteLine("\nAn SQl error occured: " + e.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("\nAn error occured: " + ex.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

}

void ConsoleGetPerson()
{

    Console.WriteLine("\nGet a person");
    int? PId = null;

    while (PId == null)
    {
        Console.WriteLine("Please enter the id of the person you want to get and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }

        var isNumeric = int.TryParse(userInput, out int number);
        if (!isNumeric)
        {
            Console.WriteLine("\nInvalid input. Please enter a valid id for the person you want to get or press b or back to go back to the procedure select menu");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        PId = number;
    }
    try
    {

        Stopwatch sw = Stopwatch.StartNew();
        storedProcedures.GetPerson((int)PId);
        long time = sw.ElapsedMilliseconds;
        Console.WriteLine("\nSucces");
        Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (SqlException e)
    {
        Console.WriteLine("\nAn SQl error occured: " + e.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("\nAn error occured: " + ex.Message);
        Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
    }

}

void ConsoleGetAllFriendsOfPerson()
{
    Console.WriteLine("\n Get all friends of person");
    string? PName = null;

    while (PName == null)
    {
        Console.WriteLine("Please enter a name for the person and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }
        
        PName = userInput;

        if (PName == null)
        {
            Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        
        try
        {

            Stopwatch sw = Stopwatch.StartNew();
            storedProcedures.GetAllFriendsOfPerson(PName);
            long time = sw.ElapsedMilliseconds;
            Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
            Console.WriteLine("\nSucces");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        catch (SqlException e)
        {
            Console.WriteLine("\nAn SQl error occured: " + e.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();

        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occured: " + ex.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

    }
}

void ConsoleGetAllFriendsOfPersonReciprocal()
{
    Console.WriteLine("\n Get all friends of person reciprical");
    string? PName = null;

    while (PName == null)
    {
        Console.WriteLine("Please enter a name for the person and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }
        
        PName = userInput;

        if (PName == null)
        {
            Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

       
        try
        {

            Stopwatch sw = Stopwatch.StartNew();
            storedProcedures.GetAllFriendsOfPersonReciprocal(PName);
            long time = sw.ElapsedMilliseconds;
            Console.WriteLine("\nSucces");
            Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        catch (SqlException e)
        {
            Console.WriteLine("\nAn SQl error occured: " + e.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();

        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occured: " + ex.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

    }
}

void ConsoleGetAllFriendsOfFriendsPerson()
{
    Console.WriteLine("\n Get all friends of friends of person");
    string? PName = null;

    while (PName == null)
    {
        Console.WriteLine("Please enter a name for the person and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }

        PName = userInput;

        if (PName == null)
        {
            Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        
        try
        {

            Stopwatch sw = Stopwatch.StartNew();
            storedProcedures.usp_GetAllFriendsOfFriendsPerson(PName);
            long time = sw.ElapsedMilliseconds;
            Console.WriteLine("\nSucces");
            Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        catch (SqlException e)
        {
            Console.WriteLine("\nAn SQl error occured: " + e.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();

        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occured: " + ex.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

    }
}

void ConsoleUpdatePersonName()
{

    Console.WriteLine("\n Update person name");
    int? PId = null;
    string? PName = null;

    while (PId == null)
    {
        Console.WriteLine("Please enter the id of the person you want to update and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }

        var isNumeric = int.TryParse(userInput, out int number);
        if (!isNumeric)
        {
            Console.WriteLine("\nInvalid input. Please enter a valid id for the person you want to get or press b or back to go back to the procedure select menu");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        PId = number;
    }
    while (PName == null)
    {
        Console.WriteLine("Please enter a name and press enter:");
        Console.WriteLine("Or press \"b\" or input \"back\" then press the enter key to to go back to the procedure select menu");

        string? userInput = Console.ReadLine();

        if (userInput == "b" || userInput == "B" || userInput == "back" || userInput == "Back")
        {
            return;
        }

        PName = userInput;

        if (PName == null)
        {
            Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        try
        {

            Stopwatch sw = Stopwatch.StartNew();
            storedProcedures.UpdatePersonName((int)PId, PName);
            long time = sw.ElapsedMilliseconds;
            Console.WriteLine("\nSucces");
            Console.WriteLine("Time elapsed: " + time / 1000.0 + " seconds");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        catch (SqlException e)
        {
            Console.WriteLine("\nAn SQl error occured: " + e.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nAn error occured: " + ex.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

    }

}





