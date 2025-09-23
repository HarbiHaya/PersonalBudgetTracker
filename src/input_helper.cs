using System;
// Shahad Alamoudi 
namespace PersonalBudgetTracker
{
    public class InputHelper
    {
        //Ask the user to enter a date in dd/MM/yyyy format
        //and keeps asking until a valid date is entered
        public static DateTime GetValidDate(string promptMessage)
        {
            DateTime validDate;
            bool inputIsValid = false;

            
            Console.Write(promptMessage);

            while (inputIsValid == false)
            {
                try
                {
                    string userInput = Console.ReadLine();

                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.Write("Please enter a date. Try again: ");
                        continue;
                    }

                    // convert the input text to a date in dd/MM/yyyy format
                    validDate = DateTime.ParseExact(userInput, "dd/MM/yyyy", null);

                    //At this point, the entered date is valid
                    inputIsValid = true;
                    return validDate;
                }
                catch (FormatException)
                {
                    Console.Write("Date format is wrong. Please use dd/MM/yyyy (e.g., 05/02/2023): ");
                }
                catch (Exception)
                {
                    Console.Write("Something went wrong. Please enter date as dd/MM/yyyy: ");
                }
            }
            return DateTime.Now; 
        }




        // Asks the user to enter a valid decimal amount
        // and keeps trying until the input is correct
        public static decimal GetValidAmount(string promptMessage)
        {
            decimal validAmount;
            bool inputIsValid = false;

            Console.Write(promptMessage);

            while (inputIsValid == false)
            {
                try
                {
                    string userInput = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.Write("Please enter an amount. Try again: ");
                        continue;
                    }

                    // Convert the input to a decimal number
                    validAmount = decimal.Parse(userInput); 

                    // Check if the amount is positive
                    if (validAmount <= 0)
                    {
                        Console.Write("Amount must be greater than zero. Please try again: ");
                        continue;
                    }

                    //At this point, the entered amount  is valid
                    inputIsValid = true;
                    return validAmount;
                }
                catch (FormatException)
                {
                    Console.Write("That's not a valid number. Please enter a decimal amount: "); 
                }
                catch (OverflowException)
                {
                    Console.Write("That number is too big. Please enter a smaller amount: "); 
                }
                catch (Exception)
                {
                    Console.Write("Something went wrong. Please enter a valid amount: ");
                }
            }
            return 0;
        }




        //Asks the user to enter non-empty text
        //and keeps trying until valid input is provided
        public static string GetValidText(string promptMessage)
        {
            string validText;
            bool inputIsValid = false;

            Console.Write(promptMessage);

            while (inputIsValid == false)
            {
                validText = Console.ReadLine();

                // Check if user entered something meaningful
                if (string.IsNullOrEmpty(validText))
                {
                    Console.Write("Please enter some text. Try again: ");
                    continue;
                }

                // Remove extra spaces from the beginning and end
                validText = validText.Trim(); //

                // Check if there's still something left after removing spaces
                if (validText.Length == 0)
                {
                    Console.Write("Please enter some actual text: ");
                    continue;
                }
                //At this point,  the text is valid
                inputIsValid = true;
                return validText;
            }
            return "";
        }




        // Check if the user's choice is in range
        public static int GetValidMenuChoice(string promptMessage, int lowestNumber, int highestNumber) // 
        {
            int validChoice;
            bool inputIsValid = false;

            Console.Write(promptMessage);

            while (inputIsValid == false)
            {
                try
                {
                    string userInput = Console.ReadLine();
                    
                    // Check if user entered something
                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.Write($"Please enter a number between {lowestNumber} and {highestNumber}: ");
                        continue;
                    }

                    // Try to convert the input to an integer
                    validChoice = int.Parse(userInput);

                    // Check if the choice is between lowest and highest

                    if (validChoice < lowestNumber || validChoice > highestNumber)
                    {
                        Console.Write($"Please choose a number between {lowestNumber} and {highestNumber}: ");
                        continue;
                    }

                    // At this point, the choice is valid
                    inputIsValid = true;
                    return validChoice;
                }
                catch (FormatException)
                {
                    Console.Write($"That's not a valid number. Please enter {lowestNumber}-{highestNumber}: ");
                }
                catch (OverflowException)
                {
                    Console.Write($"That number is too big. Please enter {lowestNumber}-{highestNumber}: ");
                }
                catch (Exception)
                {
                    Console.Write($"Something went wrong. Please enter {lowestNumber}-{highestNumber}: ");
                }
            }
            return 0;
        }


        // Get a valid year 
        public static int GetValidYear(string promptMessage)
        {
           
            Console.Write(promptMessage);

            while (true)
            {
                try
                {
                    string userInput = Console.ReadLine();

                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.Write("Please enter a valid year: ");
                        continue;
                    }

                    int year = int.Parse(userInput);

                    return year;
                }
                catch (Exception)
                {
                    Console.Write($"something went wrong. Please enter a valid year: ");
                   
                }
            }
        }

        // Get a valid month from the user (1-12)
        public static int GetValidMonth(string promptMessage)
        {
            Console.Write(promptMessage);

            while (true)
            {
                try
                {
                    string userInput = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(userInput))
                    {
                        Console.Write("Please enter a month number (1-12): ");
                        continue;
                    }

                    int month = int.Parse(userInput);

                    if (month < 1 || month > 12)
                    {
                        Console.Write("Month must be 1-12. Try again: ");
                        continue;
                    }

                    return month;
                }
                catch (Exception)
                {
                    Console.Write("Invalid month. Please enter 1-12: ");
                }
            }
        }

        // Ask user a yes/no question and get a clear answer
        public static bool GetYesNoAnswer(string questionText)
        {
            Console.Write($"{questionText} (y/n): ");

            while (true)
            {
                string userInput = Console.ReadLine();
                
                if (string.IsNullOrEmpty(userInput))
                {
                    Console.Write("Please enter 'y' for yes or 'n' for no: ");
                    continue;
                }

                // Convert to lowercase and check first character 
                string answer = userInput.ToLower().Trim(); 
                
                if (answer == "y" || answer == "yes")
                {
                    return true;
                }
                else if (answer == "n" || answer == "no")
                {
                    return false;
                }
                else
                {
                    Console.Write("Please enter 'y' for yes or 'n' for no: ");
                }
            }
        }

        // Pause the program and wait for user to press Enter
        public static void PauseForUser(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }

        // Clear the screen in a safe way
        public static void ClearScreen()
        {
            try
            {
                Console.Clear();
            }
            catch (Exception)
            {
                // If clearing doesn't work, just add some space
                Console.WriteLine(new string('\n', 5));
            }
        }
    }
}