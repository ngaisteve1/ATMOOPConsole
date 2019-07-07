using System;
using System.Collections.Generic;
using System.Text;

namespace ATMOOPProject.StaticClass
{
    public static class Validator
    {
        // This class in charge of user input validation.
        // Validation here will not include business rules validation.

        public static decimal GetValidDecimalInputAmt(string input)
        {
            bool valid = false;
            string rawInput;
            decimal amount = 0;

            // Get user's input input type is valid
            while (!valid)
            {
                rawInput = Utility.GetRawInput(input);
                valid = decimal.TryParse(rawInput, out amount);
                if (!valid)
                    Utility.PrintMessage("Invalid input. Try again.", false);
            }

            return amount;
        }

        public static long GetValidIntInputAmt(string input)
        {
            bool valid = false;
            string rawInput;
            long amount = 0;

            // Get user's input input type is valid
            while (!valid)
            {
                rawInput = Utility.GetRawInput(input);
                valid = long.TryParse(rawInput, out amount);
                if (!valid)
                    Utility.PrintMessage("Invalid input. Try again.", false);
            }

            return amount;
        }


    }
}
