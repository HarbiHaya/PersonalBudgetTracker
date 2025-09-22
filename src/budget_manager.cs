/*
 Jana Ashraf Alharbi
 2305762
 Section [AA]

 */

using System;

namespace PersonalBudgetTracker
{
    public class BudgetManager
    {
        // Array to store all transactions
        private Transaction[] allTransactions;
        private int transactionCount;
        private int maxTransactions;
        
        // Budget limit for expenses per month
        private decimal monthlyBudgetLimit;
        
        // File helper for saving and loading data
        private FileHelper fileHelper;


        // Constructor - sets up the budget manager
        public BudgetManager()
        {
            // Set maximum number of transactions we can store
            maxTransactions = 500;
            allTransactions = new Transaction[maxTransactions]; 
            transactionCount = 0;
            monthlyBudgetLimit = 0;
            
            // Create file helper for data storage
            fileHelper = new FileHelper();
            
            // Try to load existing data
            LoadExistingTransactions();
        }

        // Load transactions from file when program starts
        private void LoadExistingTransactions()
        {
            allTransactions = fileHelper.LoadAllTransactions(out transactionCount);
            Console.WriteLine("Budget Manager initialized with existing data.");
        }

        // Add a new income transaction
        public void AddNewIncome(DateTime date, string description, decimal amount, string category)
        {
            // Check if we have space for more transactions
            if (transactionCount >= maxTransactions)
            {
                Console.WriteLine("Cannot add more transactions - storage is full!");
                return;
            }

            // Create new income transaction
            Income newIncome = new Income(date, description, amount, category);
            
            // Add it to our array
            allTransactions[transactionCount] = newIncome;
            transactionCount++;
            
            Console.WriteLine($"Added income: ${amount:F2} for {description}");
        }

        // Add a new expense transaction
        public void AddNewExpense(DateTime date, string description, decimal amount, string category)
        {
            // Check if we have space for more transactions
            if (transactionCount >= maxTransactions)
            {
                Console.WriteLine("Cannot add more transactions - storage is full!");
                return;
            }

            // Create new expense transaction
            Expense newExpense = new Expense(date, description, amount, category);
            
            // Add it to our array
            allTransactions[transactionCount] = newExpense;
            transactionCount++;
            
            Console.WriteLine($"Added expense: ${amount:F2} for {description}");
            
            // Check if this expense puts us over budget for the month
            CheckBudgetForMonth(date.Year, date.Month);
        }

        // Show all transactions within a specific date range
        public void ShowTransactionsInDateRange(DateTime startDate, DateTime endDate)
        {
            Console.WriteLine($"\n=== TRANSACTIONS FROM {startDate:dd/MM/yyyy} TO {endDate:dd/MM/yyyy} ===");
            
            bool foundAnyTransactions = false;
            decimal totalIncome = 0;
            decimal totalExpenses = 0;

            // Look through all transactions
            for (int i = 0; i < transactionCount; i++)
            {
                Transaction currentTransaction = allTransactions[i];
                
                // Check if this transaction is in our date range
                if (currentTransaction.IsInDateRange(startDate, endDate))
                {
                    currentTransaction.ShowTransaction();
                    foundAnyTransactions = true;
                    
                    // Add to our totals
                    if (currentTransaction.TransactionType == "Income")
                    {
                        totalIncome = totalIncome + currentTransaction.Amount;
                    }
                    else if (currentTransaction.TransactionType == "Expense")
                    {
                        totalExpenses = totalExpenses + currentTransaction.Amount;
                    }
                }
            }

            // Show summary if we found transactions
            if (foundAnyTransactions == true)
            {
                Console.WriteLine("\n--- SUMMARY FOR DATE RANGE ---");
                Console.WriteLine($"Total Income: ${totalIncome:F2}");
                Console.WriteLine($"Total Expenses: ${totalExpenses:F2}");
                Console.WriteLine($"Net Amount: ${(totalIncome - totalExpenses):F2}");
            }
            else
            {
                Console.WriteLine("No transactions found in this date range.");
            }
        }

        // Show a summary of all categories for a specific month
        public void ShowMonthlyCategorySummary(int year, int month)
        {
            Console.WriteLine($"\n=== CATEGORY SUMMARY FOR {month:D2}/{year} ===");
            
            // Arrays to store category names and amounts
            string[] categoryNames = new string[50];  // Maximum 50 different categories
            decimal[] categoryIncomes = new decimal[50];
            decimal[] categoryExpenses = new decimal[50];
            int numberOfCategories = 0;

            // Look through all transactions for this month
            for (int i = 0; i < transactionCount; i++)
            {
                Transaction currentTransaction = allTransactions[i];
                
                // Check if transaction is from the month we want
                if (currentTransaction.IsFromMonth(month, year))
                {
                    string transactionCategory = currentTransaction.Category;
                    
                    // Find this category in our arrays (or add it if new)
                    int categoryIndex = FindCategoryIndex(categoryNames, numberOfCategories, transactionCategory);
                    
                    if (categoryIndex == -1)
                    {
                        // This is a new category
                        categoryNames[numberOfCategories] = transactionCategory;
                        categoryIndex = numberOfCategories;
                        numberOfCategories++;
                    }

                    // Add the amount to the right category
                    if (currentTransaction.TransactionType == "Income")
                    {
                        categoryIncomes[categoryIndex] = categoryIncomes[categoryIndex] + currentTransaction.Amount;
                    }
                    else if (currentTransaction.TransactionType == "Expense")
                    {
                        categoryExpenses[categoryIndex] = categoryExpenses[categoryIndex] + currentTransaction.Amount;
                    }
                }
            }

            // Show the results
            if (numberOfCategories == 0)
            {
                Console.WriteLine($"No transactions found for {month:D2}/{year}.");
                return;
            }

            decimal totalIncome = 0;
            decimal totalExpenses = 0;

            Console.WriteLine("Category          | Income     | Expenses   | Net Amount");
            Console.WriteLine("------------------|------------|------------|------------");

            // Show each category
            for (int i = 0; i < numberOfCategories; i++)
            {
                decimal netAmount = categoryIncomes[i] - categoryExpenses[i];
                Console.WriteLine($"{categoryNames[i],-17} | ${categoryIncomes[i],8:F2} | ${categoryExpenses[i],8:F2} | ${netAmount,8:F2}");
                
                totalIncome = totalIncome + categoryIncomes[i];
                totalExpenses = totalExpenses + categoryExpenses[i];
            }

            // Show totals
            Console.WriteLine("------------------|------------|------------|------------");
            Console.WriteLine($"{"TOTAL",-17} | ${totalIncome,8:F2} | ${totalExpenses,8:F2} | ${(totalIncome - totalExpenses),8:F2}");

            // Check budget status
            CheckBudgetForMonth(year, month);
        }

        // Helper method to find a category in our array
        private int FindCategoryIndex(string[] categoryNames, int numberOfCategories, string targetCategory)
        {
            for (int i = 0; i < numberOfCategories; i++)
            {
                if (categoryNames[i] == targetCategory)
                {
                    return i;
                }
            }
            return -1; // Not found
        }

        // Set the monthly budget limit
        public void SetMonthlyBudget(decimal budgetAmount)
        {
            if (budgetAmount <= 0)
            {
                Console.WriteLine("Budget must be greater than zero.");
                return;
            }

            monthlyBudgetLimit = budgetAmount;
            Console.WriteLine($"Monthly budget limit set to ${budgetAmount:F2}");
        }

        // Check if we're over budget for a specific month
        private void CheckBudgetForMonth(int year, int month)
        {
            // Only check if we have a budget set
            if (monthlyBudgetLimit <= 0)
            {
                return;
            }

            decimal monthlyExpenses = CalculateMonthlyExpenses(year, month);

            if (monthlyExpenses > monthlyBudgetLimit)
            {
                Console.WriteLine("!!!! MONTHLY BUDGET EXCEEDED !!!!");
                Console.WriteLine($"Budget Limit: ${monthlyBudgetLimit:F2}");
                Console.WriteLine($"Current Expenses: ${monthlyExpenses:F2}");
                Console.WriteLine($"Over Budget by: ${(monthlyExpenses - monthlyBudgetLimit):F2}");
            }
            else
            {
                decimal remainingBudget = monthlyBudgetLimit - monthlyExpenses;
                Console.WriteLine($"Budget Status: ${remainingBudget:F2} remaining for {month:D2}/{year}");
            }
        }

        // Calculate total expenses for a specific month
        private decimal CalculateMonthlyExpenses(int year, int month)
        {
            decimal totalExpenses = 0;

            // Look through all transactions
            for (int i = 0; i < transactionCount; i++)
            {
                Transaction currentTransaction = allTransactions[i];
                
                // Check if it's an expense from the right month
                if (currentTransaction.IsFromMonth(month, year) && currentTransaction.TransactionType == "Expense")
                {
                    totalExpenses = totalExpenses + currentTransaction.Amount;
                }
            }

            return totalExpenses;
        }

        // Show the current overall balance
        public void ShowCurrentBalance()
        {
            decimal totalIncome = 0;
            decimal totalExpenses = 0;

            // Add up all income and expenses
            for (int i = 0; i < transactionCount; i++)
            {
                Transaction currentTransaction = allTransactions[i];
                
                if (currentTransaction.TransactionType == "Income")
                {
                    totalIncome = totalIncome + currentTransaction.Amount;
                }
                else if (currentTransaction.TransactionType == "Expense")
                {
                    totalExpenses = totalExpenses + currentTransaction.Amount;
                }
            }

            decimal currentBalance = totalIncome - totalExpenses;

            Console.WriteLine("\n=== CURRENT BALANCE SUMMARY ===");
            Console.WriteLine($"Total Income: ${totalIncome:F2}");
            Console.WriteLine($"Total Expenses: ${totalExpenses:F2}");
            Console.WriteLine($"Current Balance: ${currentBalance:F2}");

            // Give user feedback about their financial status
            if (currentBalance > 0)
            {
                Console.WriteLine("Great! You have a positive balance.");
            }
            else if (currentBalance < 0)
            {
                Console.WriteLine("Warning! You have a negative balance!");
            }
            else
            {
                Console.WriteLine("Your balance is exactly zero.");
            }
        }

        // Show all transactions (for debugging or full review)
        public void ShowAllTransactions()
        {
            Console.WriteLine("\n=== ALL TRANSACTIONS ===");
            
            if (transactionCount == 0)
            {
                Console.WriteLine("No transactions recorded yet.");
                return;
            }

            // Show each transaction
            for (int i = 0; i < transactionCount; i++)
            {
                Console.WriteLine($"Transaction #{i + 1}:");
                allTransactions[i].ShowTransaction();
            }

            Console.WriteLine($"\nTotal number of transactions: {transactionCount}");
        }

        // Save all current data to file
        public void SaveAllData()
        {
            fileHelper.SaveAllTransactions(allTransactions, transactionCount);
        }

        // Get the current budget limit (for display purposes)
        public decimal GetCurrentBudgetLimit()
        {
            return monthlyBudgetLimit;
        }

        // Get the number of transactions we have
        public int GetTransactionCount()
        {
            return transactionCount;
        }

        // Create a backup of current data
        public void CreateDataBackup()
        {
            fileHelper.CreateBackup();
        }

        // Clear all data (start fresh)
        public bool ClearAllData()
        {
            // Reset the arrays and counters
            allTransactions = new Transaction[maxTransactions];
            transactionCount = 0;
            monthlyBudgetLimit = 0;
            
            // Delete the data file
            fileHelper.DeleteDataFile();
            
            Console.WriteLine("All data cleared successfully.");
            return true;
        }
    }
}