/*
Shahad Abdullah
2207358
Section [AA]
COCS-308 | Assignment 1
 This contains the base Transaction class and its derived Income/Expense classes
 */
using System;

namespace PersonalBudgetTracker
{
    // Base class for all financial transactions
    // expense & income classes will inherit from this one
    public class Transaction
    {
        // Basic properties to store transaction details
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string TransactionType { get; set; }

        // Default constructor 
        public Transaction()
        {
            TransactionDate = DateTime.Now;
            Description = "";
            Amount = 0;
            Category = "";
            TransactionType = "";
        }

        // Constructor with parameters to let us set values when creating an object
        public Transaction(DateTime date, string description, decimal amount, string category, string type)
        {
            TransactionDate = date;
            Description = description;
            Amount = amount;
            Category = category;
            TransactionType = type;
        }

        // Method; displays transaction details in a readable format
        public void ShowTransaction()
        {
            Console.WriteLine("--- Transaction Details ---");
            Console.WriteLine($"Date: {TransactionDate:dd/MM/yyyy}");
            Console.WriteLine($"Type: {TransactionType}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Category: {Category}");
            Console.WriteLine($"Amount: ${Amount:F2}");
            Console.WriteLine("---------------------------");
        }

        // Method; converts transaction to a simple string format for saving (like in CSV)
        public string ConvertToString()
        {
            return $"{TransactionDate:dd/MM/yyyy},{TransactionType},{Description},{Amount:F2},{Category}";
        }

        // Method; checks if this transaction matches a specific month and year
        public bool IsFromMonth(int month, int year)
        {
            if (TransactionDate.Month == month && TransactionDate.Year == year)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Method; checks if a transaction is within a specific date range
        public bool IsInDateRange(DateTime startDate, DateTime endDate)
        {
            if (TransactionDate >= startDate && TransactionDate <= endDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Income class, inherits from Transaction
    // for money coming in (salary, gifts,...)
    public class Income : Transaction
    {
        // default constructor for Income
        // calls the parent constructor
        public Income() : base()
        {
            TransactionType = "Income";
        }

        // income constructor with parameters
        public Income(DateTime date, string description, decimal amount, string category) 
            : base(date, description, amount, category, "Income")
        {
            // The base constructor already sets everything we need
        }

        // method to show income transactions details
        public void ShowIncomeDetails()
        {
            Console.WriteLine($"+ INCOME: {Description} - ${Amount:F2} ({Category})");
        }
    }

    // Expense class, inherits from Transaction  
    // for money going out (food, rent, entertainment,...)
    public class Expense : Transaction
    {
        // default constructor for Expense
        // calls the parent constructor
        public Expense() : base()
        {
            TransactionType = "Expense";
        }

        // expense constructor with parameters
        public Expense(DateTime date, string description, decimal amount, string category) 
            : base(date, description, amount, category, "Expense")
        {
            // The base constructor already sets everything we need
        }

        // method to show expense transactions details
        public void ShowExpenseDetails()
        {
            Console.WriteLine($"- EXPENSE: {Description} - ${Amount:F2} ({Category})");
        }
    }
}