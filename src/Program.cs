/*
 * Program.cs - Main Application Controller
 * Developer 5: User Interface and Application Flow
 * 
 * Responsibility: Control the main program flow and coordinate all other components
 * This class manages the menu system and user interactions
 */

// test mayarm nj

using System;

namespace PersonalBudgetTracker
{
    public class Program
    {
        // Main budget manager that handles all the business logic
        private static BudgetManager budgetManager;

        // Main entry point - this runs when the program starts
        public static void Main()
        {
            // Show welcome message
            ShowWelcomeMessage();
            
            // Set up the budget manager
            budgetManager = new BudgetManager();
            
            // Run the main program loop
            RunMainProgramLoop();
            
            // Show goodbye message when user exits
            ShowGoodbyeMessage();
        }

        // Display welcome message and program information
        private static void ShowWelcomeMessage()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("╔" + new string('═', 60) + "╗");
            Console.WriteLine("║" + " PERSONAL BUDGET TRACKER".PadLeft(41).PadRight(60) + "║");
            Console.WriteLine("║" + " Student Team Project - C# Programming".PadLeft(48).PadRight(60) + "║");
            Console.WriteLine("╚" + new string('═', 60) + "╝");
            Console.WriteLine();
            Console.WriteLine("Welcome to your Personal Budget Tracker!");
            Console.WriteLine("This program helps you manage your money by tracking income and expenses.");
            Console.WriteLine();
            
            InputHelper.PauseForUser("Loading your budget data...");
        }

        // Main program loop - keeps running until user chooses to exit
        private static void RunMainProgramLoop()
        {
            bool userWantsToExit = false;

            // Keep showing menu until user decides to quit
            while (userWantsToExit == false)
            {
                // Show the main menu
                ShowMainMenu();
                
                // Get user's choice
                int userChoice = InputHelper.GetValidMenuChoice("Enter your choice (1-8): ", 1, 8);
                
                // Process the user's choice
                ProcessMenuChoice(userChoice);
                
                // Check if user wants to exit
                if (userChoice == 8)
                {
                    userWantsToExit = true;
                }
                else
                {
                    // Pause before showing menu again
                    InputHelper.PauseForUser("\nOperation completed.");
                }
            }
        }

        // Display the main menu options
        private static void ShowMainMenu()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("╔" + new string('═', 50) + "╗");
            Console.WriteLine("║" + " MAIN MENU".PadLeft(29).PadRight(50) + "║");
            Console.WriteLine("╠" + new string('═', 50) + "╣");
            Console.WriteLine("║ 1. Add Income                              ║");
            Console.WriteLine("║ 2. Add Expense                             ║");
            Console.WriteLine("║ 3. View Transactions by Date Range        ║");
            Console.WriteLine("║ 4. Monthly Category Summary                ║");
            Console.WriteLine("║ 5. Set Monthly Budget Limit                ║");
            Console.WriteLine("║ 6. View Current Balance                    ║");
            Console.WriteLine("║ 7. View All Transactions                  ║");
            Console.WriteLine("║ 8. Save and Exit                          ║");
            Console.WriteLine("╚" + new string('═', 50) + "╝");
            Console.WriteLine();
        }

        // Process the user's menu choice
        private static void ProcessMenuChoice(int choice)
        {
            // Use if-else statements to handle each menu option
            if (choice == 1)
            {
                HandleAddIncome();
            }
            else if (choice == 2)
            {
                HandleAddExpense();
            }
            else if (choice == 3)
            {
                HandleViewTransactionsByDate();
            }
            else if (choice == 4)
            {
                HandleMonthlyCategorySummary();
            }
            else if (choice == 5)
            {
                HandleSetBudgetLimit();
            }
            else if (choice == 6)
            {
                HandleViewCurrentBalance();
            }
            else if (choice == 7)
            {
                HandleViewAllTransactions();
            }
            else if (choice == 8)
            {
                HandleSaveAndExit();
            }
        }

        // Handle adding a new income transaction
        private static void HandleAddIncome()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== ADD NEW INCOME ===");
            Console.WriteLine();
            
            // Get all the required information from user
            DateTime incomeDate = InputHelper.GetValidDate("Enter date (dd/MM/yyyy): ");
            string incomeDescription = InputHelper.GetValidText("Enter description (e.g., 'Salary', 'Gift'): ");
            decimal incomeAmount = InputHelper.GetValidAmount("Enter amount ($): ");
            string incomeCategory = InputHelper.GetValidText("Enter category (e.g., 'Job', 'Family', 'Investment'): ");
            
            // Add the income using our budget manager
            budgetManager.AddNewIncome(incomeDate, incomeDescription, incomeAmount, incomeCategory);
        }

        // Handle adding a new expense transaction
        private static void HandleAddExpense()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== ADD NEW EXPENSE ===");
            Console.WriteLine();
            
            // Get all the required information from user
            DateTime expenseDate = InputHelper.GetValidDate("Enter date (dd/MM/yyyy): ");
            string expenseDescription = InputHelper.GetValidText("Enter description (e.g., 'Groceries', 'Gas'): ");
            decimal expenseAmount = InputHelper.GetValidAmount("Enter amount ($): ");
            string expenseCategory = InputHelper.GetValidText("Enter category (e.g., 'Food', 'Transport', 'Entertainment'): ");
            
            // Add the expense using our budget manager
            budgetManager.AddNewExpense(expenseDate, expenseDescription, expenseAmount, expenseCategory);
        }

        // Handle viewing transactions within a date range
        private static void HandleViewTransactionsByDate()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== VIEW TRANSACTIONS BY DATE RANGE ===");
            Console.WriteLine();
            
            // Get the date range from user
            DateTime startDate = InputHelper.GetValidDate("Enter start date (dd/MM/yyyy): ");
            DateTime endDate = InputHelper.GetValidDate("Enter end date (dd/MM/yyyy): ");
            
            // Make sure end date is not before start date
            if (endDate < startDate)
            {
                Console.WriteLine("End date cannot be earlier than start date!");
                return;
            }
            
            // Show the transactions in this date range
            budgetManager.ShowTransactionsInDateRange(startDate, endDate);
        }

        // Handle showing monthly category summary
        private static void HandleMonthlyCategorySummary()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== MONTHLY CATEGORY SUMMARY ===");
            Console.WriteLine();
            
            // Get the month and year from user
            int year = InputHelper.GetValidYear("Enter year: ");
            int month = InputHelper.GetValidMonth("Enter month (1-12): ");
            
            // Show the summary for this month
            budgetManager.ShowMonthlyCategorySummary(year, month);
        }

        // Handle setting the monthly budget limit
        private static void HandleSetBudgetLimit()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== SET MONTHLY BUDGET LIMIT ===");
            Console.WriteLine();
            
            // Show current budget limit if there is one
            decimal currentLimit = budgetManager.GetCurrentBudgetLimit();
            if (currentLimit > 0)
            {
                Console.WriteLine($"Current monthly budget limit: ${currentLimit:F2}");
                Console.WriteLine();
            }
            
            // Get new budget limit from user
            decimal newBudgetLimit = InputHelper.GetValidAmount("Enter monthly budget limit ($): ");
            
            // Set the new budget limit
            budgetManager.SetMonthlyBudget(newBudgetLimit);
        }

        // Handle viewing current balance
        private static void HandleViewCurrentBalance()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== CURRENT BALANCE ===");
            Console.WriteLine();
            
            // Show the current balance summary
            budgetManager.ShowCurrentBalance();
        }

        // Handle viewing all transactions
        private static void HandleViewAllTransactions()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== ALL TRANSACTIONS ===");
            Console.WriteLine();
            
            // Show all transactions
            budgetManager.ShowAllTransactions();
        }

        // Handle saving data and exiting
        private static void HandleSaveAndExit()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== SAVING AND EXITING ===");
            Console.WriteLine();
            
            // Ask if user wants to create a backup
            bool createBackup = InputHelper.GetYesNoAnswer("Would you like to create a backup before saving?");
            
            if (createBackup == true)
            {
                budgetManager.CreateDataBackup();
            }
            
            // Save all current data
            Console.WriteLine("Saving your budget data...");
            budgetManager.SaveAllData();
            
            Console.WriteLine("All data saved successfully!");
        }

        // Display goodbye message when program ends
        private static void ShowGoodbyeMessage()
        {
            Console.WriteLine();
            Console.WriteLine("╔" + new string('═', 50) + "╗");
            Console.WriteLine("║" + " THANK YOU FOR USING BUDGET TRACKER!".PadLeft(43).PadRight(50) + "║");
            Console.WriteLine("╚" + new string('═', 50) + "╝");
            Console.WriteLine();
            Console.WriteLine("Your financial data has been saved safely.");
            Console.WriteLine("Remember to check your budget regularly!");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Additional helper method for advanced users
        private static void ShowDeveloperInfo()
        {
            Console.WriteLine("\n=== DEVELOPMENT TEAM ===");
            Console.WriteLine("Developer 1: Transaction Models (Transaction.cs)");
            Console.WriteLine("Developer 2: File Operations (FileHelper.cs)");
            Console.WriteLine("Developer 3: Input Validation (InputHelper.cs)");
            Console.WriteLine("Developer 4: Budget Logic (BudgetManager.cs)");
            Console.WriteLine("Developer 5: User Interface (Program.cs)");
        }
    }
}