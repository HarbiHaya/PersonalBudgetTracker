/*
 * FileHelper.cs - File Operations
 * Developer 2: File Input/Output and Data Storage
 * 
 * Responsibility: Handle all file operations for saving and loading transaction data
 * This class manages reading from and writing to CSV files
 */

using System;
using System.IO;

namespace PersonalBudgetTracker
{
    public class FileHelper
    {
        // File name and path for storing transactions
        private string dataFileName;
        private string dataFolderPath;
        private string fullFilePath;

        // Constructor - sets up the file paths
        public FileHelper()
        {
            dataFileName = "budget_data.csv";
            dataFolderPath = "BudgetData";
            fullFilePath = Path.Combine(dataFolderPath, dataFileName);
            
            // Make sure the data folder exists
            CreateDataFolder();
        }

        // Create the folder for storing data files if it doesn't exist
        private void CreateDataFolder()
        {
            try
            {
                // Check if folder exists, if not create it
                if (!Directory.Exists(dataFolderPath))
                {
                    Directory.CreateDirectory(dataFolderPath);
                    Console.WriteLine("Created data folder for storing budget information.");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error creating data folder: {error.Message}");
            }
        }

        // Create a new CSV file with column headers
        public void CreateNewFile()
        {
            try
            {
                // Create file and write the header row
                using (StreamWriter writer = new StreamWriter(fullFilePath))
                {
                    writer.WriteLine("Date,Type,Description,Amount,Category");
                }
                Console.WriteLine("Created new budget data file.");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error creating file: {error.Message}");
            }
        }

        // Load all transactions from the CSV file
        public Transaction[] LoadAllTransactions(out int numberOfTransactions)
        {
            // Create array to store transactions (maximum 500 for simplicity)
            Transaction[] loadedTransactions = new Transaction[500];
            numberOfTransactions = 0;

            try
            {
                // Check if file exists
                if (!File.Exists(fullFilePath))
                {
                    Console.WriteLine("No existing data file found. Starting with empty budget.");
                    return loadedTransactions;
                }

                // Read all lines from the file
                string[] fileLines = File.ReadAllLines(fullFilePath);

                // Process each line (skip the first line which contains headers)
                for (int i = 1; i < fileLines.Length; i++)
                {
                    try
                    {
                        // Convert the line to a transaction
                        Transaction loadedTransaction = ConvertLineToTransaction(fileLines[i]);
                        
                        if (loadedTransaction != null)
                        {
                            loadedTransactions[numberOfTransactions] = loadedTransaction;
                            numberOfTransactions++;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine($"Warning: Could not load line {i + 1} from file. Skipping...");
                    }
                }

                Console.WriteLine($"Successfully loaded {numberOfTransactions} transactions from file.");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error loading transactions: {error.Message}");
            }

            return loadedTransactions;
        }

        // Convert a CSV line into a Transaction object
        private Transaction ConvertLineToTransaction(string csvLine)
        {
            // Check if line is empty
            if (string.IsNullOrEmpty(csvLine))
            {
                return null;
            }

            // Split the line by commas           //         parts[0] = date   part[1]  part[2 ] 
            string[] parts = csvLine.Split(','); // line[1] [ 8/6/2023 , income , eee      , ee ,ee]
            
            // Make sure we have all required parts
            if (parts.Length != 5)
            {
                return null;
            }

            try
            {
                // Extract each piece of information
                DateTime date = DateTime.ParseExact(parts[0], "dd/MM/yyyy", null); 
                string type = parts[1];
                string description = parts[2];
                decimal amount = decimal.Parse(parts[3]);  
                string category = parts[4];

                // Create the right type of transaction
                if (type == "Income")
                {
                    return new Income(date, description, amount, category);
                }
                else if (type == "Expense")
                {
                    return new Expense(date, description, amount, category);
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        // Save all transactions to the CSV file
        public void SaveAllTransactions(Transaction[] transactions, int numberOfTransactions)
        {
            try
            {
                // Open file for writing
                using (StreamWriter writer = new StreamWriter(fullFilePath)) // i/o stream
                {
                    // Write header row first
                    writer.WriteLine("Date,Type,Description,Amount,Category");

                    // Write each transaction
                    for (int i = 0; i < numberOfTransactions; i++)
                    {
                        if (transactions[i] != null)
                        {
                            writer.WriteLine(transactions[i].ConvertToString());
                        }
                    }
                }

                Console.WriteLine($"Successfully saved {numberOfTransactions} transactions to file.");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error saving transactions: {error.Message}");
            }
        }

        // Check if the data file exists and is accessible
        public bool DataFileExists()
        {
            try
            {
                return File.Exists(fullFilePath);
            }
            catch
            {
                return false;
            }
        }

        // Get the full path to the data file (useful for debugging)
        public string GetDataFilePath()
        {
            return fullFilePath;
        }

        // Delete the data file (useful for starting fresh)
        public void DeleteDataFile()
        {
            try
            {
                if (File.Exists(fullFilePath))
                {
                    File.Delete(fullFilePath);
                    Console.WriteLine("Data file deleted successfully.");
                }
                else
                {
                    Console.WriteLine("No data file found to delete.");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error deleting file: {error.Message}");
            }
        }

        // Create a backup copy of the current data
        public void CreateBackup()
        {
            try
            {
                if (File.Exists(fullFilePath))
                {
                    string backupPath = Path.Combine(dataFolderPath, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.csv"); 
                    File.Copy(fullFilePath, backupPath);
                    Console.WriteLine($"Backup created: {backupPath}");
                }
                else
                {
                    Console.WriteLine("No data file found to backup.");
                }
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error creating backup: {error.Message}");
            }
        }
    }
}