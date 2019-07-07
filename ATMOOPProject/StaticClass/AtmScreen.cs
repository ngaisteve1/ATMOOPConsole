using System;
using System.Collections.Generic;
using System.Text;

namespace ATMOOPProject.StaticClass
{
    internal static class AtmScreen
    {
        // This class in charge of printing out text in user interface.

        internal const string cur = "RM ";

        internal static void WelcomeATM()
        {
            Console.Title = "Meybank ATM System.";
            Console.WriteLine("Welcome to Meybank ATM.\n");
            Console.WriteLine("Please insert your ATM card.");
            PrintEnterMessage();
        }

        internal static void WelcomeCustomer()
        {
            Utility.PrintUserInputLabel("Welcome back, ");
        }

        internal static void PrintLockAccount()
        {
            Console.Clear();
            Utility.PrintMessage("Your account is locked. Please go to " +
                "the nearest branch to unlocked your account. Thank you.", true);

            PrintEnterMessage();
            Environment.Exit(1);
        }

        internal static void LoginProgress()
        {
            Console.Write("\nChecking card number and card pin.");
            Utility.printDotAnimation();
            Console.Clear();
        }

        internal static void LogoutProgress()
        {
            Console.WriteLine("Thank you for using Meybank ATM system.");
            Utility.printDotAnimation();
            Console.Clear();
        }


        internal static void DisplaySecureMenu()
        {
            Console.Clear();
            Console.WriteLine(" ---------------------------");
            Console.WriteLine("| Meybank ATM Secure Menu    |");
            Console.WriteLine("|                            |");
            Console.WriteLine("| 1. Balance Enquiry         |");
            Console.WriteLine("| 2. Cash Deposit            |");
            Console.WriteLine("| 3. Withdrawal              |");
            Console.WriteLine("| 4. Third Party Transfer    |");
            Console.WriteLine("| 5. Transactions            |");
            Console.WriteLine("| 6. Logout                  |");
            Console.WriteLine("|                            |");
            Console.WriteLine(" ---------------------------");

            // The menu selection is tied to Enum:SecureMenu.
        }

        internal static void PrintCheckBalanceScreen()
        {
            Console.Write("Account balance amount: ");
        }

        internal static void PrintMakeWithdrawalScreen()
        {
            Console.Write("Enter amount: ");
        }

        internal static void PrintEnterMessage()
        {
            Console.WriteLine("\nPress enter to continue.");
            Console.ReadKey();
        }
    }
}
