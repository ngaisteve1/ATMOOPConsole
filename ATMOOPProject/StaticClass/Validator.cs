using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace ATMOOPProject.StaticClass
{
    public static class Validator
    {
        // This class in charge of user input validation.
        // Validation here will not include business rules validation.

        public static T Convert<T>(this string input)
        {
            bool valid = false;
            string rawInput;            

            // Get user's input input type is valid
            while (!valid)
            {
                rawInput = Utility.GetRawInput(input);

                try
                {
                    var converter = TypeDescriptor.GetConverter(typeof(T));
                    if (converter != null)
                    {
                        // Cast ConvertFromString(string text) : object to (T)
                        return (T)converter.ConvertFromString(rawInput);
                    }
                    return default(T);
                }
                catch
                {
                    Utility.PrintMessage("Invalid input. Try again.", false);
                }
            }
            return default(T);

        }

    }

}
