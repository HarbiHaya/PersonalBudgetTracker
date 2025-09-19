/*
 * Transaction.cs - Base Transaction Class
 * Developer 1: Transaction Models and Core Data Structures
 * 
 * Responsibility: Create the foundation classes for all financial transactions
 * This file contains the base Transaction class and its derived Income/Expense classes
 */

using System;

namespace PersonalBudgetTracker
{
    // Base class for all financial transactions
    // This shows inheritance - other classes will inherit from this one
    public class Transaction
    {
        // Basic properties to store transaction information
        // These are the common fields that all transactions need
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public string TransactionType { get; set; }

        // Constructor - runs when we create a new transaction
        public Transaction()
        {
            TransactionDate = DateTime.Now;
            Description = "";
            Amount = 0;
            Category = "";
            TransactionType = "";
        }

        // Constructor with parameters - allows us to set values when creating
        public Transaction(DateTime date, string description, decimal amount, string category, string type)
        {
            TransactionDate = date;
            Description = description;
            Amount = amount;
            Category = category;
            TransactionType = type;
        }

        // Method to display transaction information in a readable format
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

        // Method to convert transaction to a simple string format for saving
        public string ConvertToString()
        {
            return $"{TransactionDate:dd/MM/yyyy},{TransactionType},{Description},{Amount:F2},{Category}";
        }

        // Method to check if this transaction matches a specific month and year
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

        // Method to check if transaction is within a date range
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

    // Income class - inherits from Transaction
    // This represents money coming in (salary, gifts, etc.)
    public class Income : Transaction
    {
        // Constructor for Income - calls the parent constructor
        public Income() : base()
        {
            TransactionType = "Income";
        }

        // Constructor with parameters
        public Income(DateTime date, string description, decimal amount, string category) 
            : base(date, description, amount, category, "Income")
        {
            // The base constructor already sets everything we need
        }

        // Special method just for income transactions
        public void ShowIncomeDetails()
        {
            Console.WriteLine($"+ INCOME: {Description} - ${Amount:F2} ({Category})");
        }
    }

    // Expense class - inherits from Transaction  
    // This represents money going out (food, rent, entertainment, etc.)
    public class Expense : Transaction
    {
        // Constructor for Expense - calls the parent constructor
        public Expense() : base()
        {
            TransactionType = "Expense";
        }

        // Constructor with parameters
        public Expense(DateTime date, string description, decimal amount, string category) 
            : base(date, description, amount, category, "Expense")
        {
            // The base constructor already sets everything we need
        }

        // Special method just for expense transactions
        public void ShowExpenseDetails()
        {
            Console.WriteLine($"- EXPENSE: {Description} - ${Amount:F2} ({Category})");
        }
    }
}