/*
Haya Sultan Alharbi
2307568
Section [AA]
COCS-308 | Assignment 1
this class is handling all file operations: creating, reading, writing, and backing up the budget data file
it ensures data saving and loading between sessions
 */

using System;
using System.IO;

namespace PersonalBudgetTracker
{
    public class FileHelper
    {
        private string dataFileName;
        private string dataFolderPath; // needed for making backups files later in the right folder 
        private string fullFilePath; // combining the folder path and file name

        public FileHelper()
        {
            dataFileName = "budget_data.csv";
            dataFolderPath = "BudgetData";
            fullFilePath = Path.Combine(dataFolderPath, dataFileName);
            
            CreateDataFolder(); // start by making sure the folder exists
        }

        private void CreateDataFolder()
        {
            try
            {
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
        // create a csv file if it does not exist
        public void CreateNewFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fullFilePath))
                {
                    writer.WriteLine("Date,Type,Description,Amount,Category"); // table header
                }
                Console.WriteLine("Created new budget data file.");
            }
            catch (Exception error)
            {
                Console.WriteLine($"Error creating file: {error.Message}");
            }
        }

        // load all transactions 
        public Transaction[] LoadAllTransactions(out int numberOfTransactions)
        {
            // an array to store transactions (max of 500)
            Transaction[] loadedTransactions = new Transaction[500];
            numberOfTransactions = 0;

            try
            {
                if (!File.Exists(fullFilePath))
                {
                    Console.WriteLine("No existing data file found. Starting with empty budget.");
                    return loadedTransactions;
                }

                string[] fileLines = File.ReadAllLines(fullFilePath); // read all the lines

                
                for (int i = 1; i < fileLines.Length; i++) // read line by line, start from 1 to skip header
                {
                    try
                    {
                        // use of convert method (below in this class) to convert the line to a transaction
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

        // the method to convert a csv line into an object from type Transaction
        private Transaction ConvertLineToTransaction(string csvLine)
        {
            if (string.IsNullOrEmpty(csvLine))
            {
                return null;
            }

            // Split the line by commas           //         parts[0] = date   part[1]  part[2] 
            string[] parts = csvLine.Split(','); // line[1]=[ 8/6/2023 , income , eee      , ee ,ee]
            
            if (parts.Length != 5) // 5 is the number of attributes in the csv file
            {
                return null;
            }

            try
            {
                DateTime date = DateTime.ParseExact(parts[0], "dd/MM/yyyy", null); // datetime format for parsing
                string type = parts[1];
                string description = parts[2];
                decimal amount = decimal.Parse(parts[3]);  
                string category = parts[4];

                // create transaction object based on type 
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
                // open file for writing mode
                using (StreamWriter writer = new StreamWriter(fullFilePath)) // i/o stream
                {
                    // header row first
                    writer.WriteLine("Date,Type,Description,Amount,Category");

                    for (int i = 0; i < numberOfTransactions; i++) // loop through all transactions to write them
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

        // check if the file exists
        public bool DataFileExists()
        {
            try
            {
                return File.Exists(fullFilePath); // returns true if file exists, false otherwise
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
        public void DeleteDataFile()         // Delete the data file used later in clear all data option

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

        // create a backup copy of the current data file 
        public void CreateBackup(Transaction[] transactions, int numberOfTransactions)
        {
            try
            {
                // save current data first then backup
                SaveAllTransactions(transactions, numberOfTransactions);
                
                if (File.Exists(fullFilePath))
                {
                string backupPath = Path.Combine(dataFolderPath, $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.csv"); // new backup file name 
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