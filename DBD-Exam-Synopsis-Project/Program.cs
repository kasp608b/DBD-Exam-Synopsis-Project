
using DBD_Exam_Synopsis_Project;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

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

        if (PName == null)
        {
            Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            continue;
        }

        PName = userInput;
        try
        {
            storedProcedures.CreatePerson(PName);
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
        catch(Exception ex)
        {
            Console.WriteLine("\nAn error occured: " + ex.Message);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
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
            int affectedRows = storedProcedures.CreatePersonFriend((int)PId1, (int)PId1);
            Console.WriteLine("\nSucces");
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
            int affectedRows = storedProcedures.DeletePerson((int)PId);
            Console.WriteLine("\nSucces");
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
            storedProcedures.GetAllPersons();
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
            storedProcedures.GetPerson((int)PId);
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

            if (PName == null)
            {
                Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
                continue;
            }

            PName = userInput;
            try
            {
                storedProcedures.GetAllFriendsOfPerson(PName);
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

    void GetAllFriendsOfPersonReciprocal()
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

            if (PName == null)
            {
                Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
                continue;
            }

            PName = userInput;
            try
            {
                storedProcedures.GetAllFriendsOfPersonReciprocal(PName);
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

    void usp_GetAllFriendsOfFriendsPerson()
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

            if (PName == null)
            {
                Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
                continue;
            }

            PName = userInput;
            try
            {
                storedProcedures.usp_GetAllFriendsOfFriendsPerson(PName);
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

            if (PName == null)
            {
                Console.WriteLine(" \nInvalid input.Please enter a valid name for the person or press b or back to go back to the procedure select ");
                Console.WriteLine("-----------------------------------------------------------------------------------------------------------------\n");
                continue;
            }

            try
        {
            storedProcedures.UpdatePersonName((int)PId,PName);
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



    

