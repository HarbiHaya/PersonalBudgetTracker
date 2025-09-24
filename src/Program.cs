/*
Mayar Abdullah Mahfouz
2306450
Section [AA]
COCS-308 | Assignment 1
هدا main حق الكود 
وظيفه الملف هدا يعرض واجهه المستخدم ويستقبل الاوامر من المستخدم وينفذ العمليات 
 */
 
using System;

namespace PersonalBudgetTracker
{
    public class Program
    {
        // هنا أنا أتعامل مع كل شيء له علاقة بحساب الميزانية وتنظيمها
        private static BudgetManager budgetManager;
        public static void Main()
        {
            // هنا بنعرض رسالة ترحيب للمستخدم
            ShowWelcomeMessage();

            
            budgetManager = new BudgetManager();
        
            RunMainProgramLoop(); // ميثود لوب البرنامح 
            
            // يعرضلي رساله توديعيه لمن اطلع من البرنامج
            ShowGoodbyeMessage();
        }
        // أرحب بالمستخدم وأشرح له فكرة البرنامج قبل يبدأ يستخدمه
        private static void ShowWelcomeMessage()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("PERSONAL BUDGET TRACKER");
            Console.WriteLine("-------------------------");
            Console.WriteLine("Welcome to your Personal Budget Tracker!");
            Console.WriteLine();
            
            InputHelper.PauseForUser("Loading your budget data...");
        }
        // هنا يقعد البرنامج يشتغل ويعرض القوائم لين المستخدم يقرر يطلع
        private static void RunMainProgramLoop()
        {
            bool userWantsToExit = false;

            while (userWantsToExit == false)
            {
                ShowMainMenu();

                int userChoice = InputHelper.GetValidMenuChoice("Enter your choice (1-10): ", 1, 10);
                
                ProcessMenuChoice(userChoice);
                
                // هنا اليوزر ادا اختار 10 يخرج
                if (userChoice == 10)
                {
                    userWantsToExit = true;
                }
                else
                {
                    // عشان يرجعني لي القائمه enter ينتظر من اليوزر 
                    InputHelper.PauseForUser("\nOperation completed.");
                }
            }
        }
        // اعرض خيارات القائمة اللي يختار منها المستخدم
        private static void ShowMainMenu()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("               MAIN MENU");
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. View Transactions by Date Range");
            Console.WriteLine("4. Monthly Category Summary");
            Console.WriteLine("5. Set Monthly Budget Limit");
            Console.WriteLine("6. View Current Balance");
            Console.WriteLine("7. View All Transactions ");
            Console.WriteLine("8. Clear and exit ");
            Console.WriteLine("9. Save all data");
            Console.WriteLine("10.Backup, Exit");
            Console.WriteLine("---------------------------------------");
        }
        // اسوي الشي الي طلبه المستخدم الي اختاره من القائمه 
        private static void ProcessMenuChoice(int choice)
        {
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
                HandleClearAllData();
            }
            else if (choice == 9)
            {
                HandleSave();
            }
            else if (choice == 10)
            {
                HandleExit();
            }
        }
        // المستخدم يدخل معلومات الدخل
        private static void HandleAddIncome()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== ADD NEW INCOME ===");
            Console.WriteLine();
            DateTime incomeDate = InputHelper.GetValidDate("Enter date (dd/MM/yyyy): ");
            string incomeDescription = InputHelper.GetValidText("Enter description (e.g., 'Salary', 'Gift'): ");
            decimal incomeAmount = InputHelper.GetValidAmount("Enter amount ($): ");
            string incomeCategory = InputHelper.GetValidText("Enter category (e.g., 'Job', 'Family', 'Investment'): ");
            budgetManager.AddNewIncome(incomeDate, incomeDescription, incomeAmount, incomeCategory);
        }
        // المستخدم يدخل معلومات المصاريف 
        private static void HandleAddExpense()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== ADD NEW EXPENSE ===");
            Console.WriteLine();
            DateTime expenseDate = InputHelper.GetValidDate("Enter date (dd/MM/yyyy): ");
            string expenseDescription = InputHelper.GetValidText("Enter description (e.g., 'Groceries', 'Gas'): ");
            decimal expenseAmount = InputHelper.GetValidAmount("Enter amount ($): ");
            string expenseCategory = InputHelper.GetValidText("Enter category (e.g., 'Food', 'Transport', 'Entertainment'): "); 
            budgetManager.AddNewExpense(expenseDate, expenseDescription, expenseAmount, expenseCategory);
        }
        // اغرض العمليات في فترة يحددها المستخذم
        private static void HandleViewTransactionsByDate()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== VIEW TRANSACTIONS BY DATE RANGE ===");
            Console.WriteLine();
            DateTime startDate = InputHelper.GetValidDate("Enter start date (dd/MM/yyyy): ");
            DateTime endDate = InputHelper.GetValidDate("Enter end date (dd/MM/yyyy): ");
            
            if (endDate < startDate)
            {
                Console.WriteLine("End date cannot be earlier than start date!");
                return;
            }
            
            // نعرض العمليات اللي صارت في الفترة هذي
            budgetManager.ShowTransactionsInDateRange(startDate, endDate);
        }

        // نوري المستخدم ملخص الصرفيات حقت الشهر
        private static void HandleMonthlyCategorySummary()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== MONTHLY CATEGORY SUMMARY ===");
            Console.WriteLine();
            int year = InputHelper.GetValidYear("Enter year: ");
            int month = InputHelper.GetValidMonth("Enter month (1-12): ");
            budgetManager.ShowMonthlyCategorySummary(year, month);
        }
        // هنا نخلي المستخدم يحدد كم يصرف بالشهر
        private static void HandleSetBudgetLimit()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== SET MONTHLY BUDGET LIMIT ===");
            Console.WriteLine();
            decimal currentLimit = budgetManager.GetCurrentBudgetLimit();
            if (currentLimit > 0)
            {
                Console.WriteLine($"Current monthly budget limit: ${currentLimit:F2}");
                Console.WriteLine();
            }
            // نخلي المستخدم يكتب كم يبي يحدد ميزانيته
            decimal newBudgetLimit = InputHelper.GetValidAmount("Enter monthly budget limit ($): ");
            budgetManager.SetMonthlyBudget(newBudgetLimit);
        }
        // نوري المستخدم رصيده الحالي
        private static void HandleViewCurrentBalance()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            
            budgetManager.ShowCurrentBalance();
        }

        // نعرض كل العمليات اللي صارت
        private static void HandleViewAllTransactions()
        {
            InputHelper.ClearScreen();
            Console.WriteLine();
            
            budgetManager.ShowAllTransactions();
        }
        private static void HandleClearAllData()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== CLEAR ALL DATA ===");
            Console.WriteLine();

            bool confirmClear = InputHelper.GetYesNoAnswer("Are you sure you want to clear all data? This action cannot be undone.");

            if (confirmClear == true)
            {
                budgetManager.ClearAllData();            }
            else
            {
                Console.WriteLine("Clear data operation cancelled.");
            }
        }

        // نحفظ البيانات ونقفل البرنامج
        private static void HandleSave()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== SAVING ===");
            Console.WriteLine();

            Console.WriteLine("Saving your budget data...");
            budgetManager.SaveAllData();

            Console.WriteLine("All data saved successfully!");
            
        }
        private static void HandleExit()
        {
            InputHelper.ClearScreen();
            Console.WriteLine("=== EXITING ===");
            Console.WriteLine();

            bool createBackup = InputHelper.GetYesNoAnswer("Would you like to create a backup before exiting?");

            if (createBackup == true)
            {
                budgetManager.CreateDataBackup();
            }

            Console.WriteLine("Saving your budget data...");

        }

        // نهاية البرنامج

        private static void ShowGoodbyeMessage() 
        {
            Console.WriteLine("THANK YOU FOR USING BUDGET TRACKER!");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

    }
}