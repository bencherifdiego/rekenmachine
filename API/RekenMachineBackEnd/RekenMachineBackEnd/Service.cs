using System.Text.Json;

namespace RekenMachineBackEnd
{
    public class Service
    {
        //Receive equation in form of string, process it and return the results to the controller
        public string ProcessCalculation(string equationString)
        {
            //Split the equation string into workable parts
            List<string> splitEquationString = SplitEquationString(equationString);

            //Handle multiplication and division
            splitEquationString = calcMultDiv(splitEquationString);

            //Handle addition and subtraction
            float result = calcAddSub(splitEquationString);

            //Prepare results for return
            EquationModel model = new EquationModel();
            model.equation = result.ToString();

            //Return results
            return JsonSerializer.Serialize(model);
        }

        //method for splitting string
        public List<string> SplitEquationString(string equationString)
        {
            //Turn the equation string into a char array
            Array equationArray = equationString.ToCharArray();
            List<string> splitEquationArray = new List<string>();

            string lastChars = "";

            //Loop through the char array
            foreach (char character in equationArray)
            {
                int output;

                //Check what the character is, what the last characters were and take the appropriate actions
                    //This is done to split the equation string up in multiple parts that can be used for calculating the result. i.e. "99+2*16" would be split into "99", "+", "2", "*", "16"
                if (int.TryParse(character.ToString(), out output))
                {
                    lastChars = lastChars + character;
                } else if (character.ToString() == ".")
                {
                    lastChars = lastChars + character;
                } else if (character == '-')
                {
                    if (lastChars == "" && splitEquationArray.Count == 0)
                    {
                        lastChars = lastChars + character;
                    } else if (lastChars == "" && "+-*/".Contains(splitEquationArray.Last()))
                    {
                        lastChars = lastChars + character;
                    } else if (lastChars == "-" && "+-*/".Contains(splitEquationArray.Last()))
                    {
                        lastChars = "";
                    } else if (lastChars != "" && !"+-*/".Contains(splitEquationArray.Last()))
                    {
                        splitEquationArray.Add(lastChars);
                        lastChars = "";

                        splitEquationArray.Add(character.ToString());
                    }
                } else if ("+*/".Contains(character))
                {
                    if (lastChars != "")
                    {
                        splitEquationArray.Add(lastChars);
                        lastChars = "";
                    }

                    splitEquationArray.Add(character.ToString());
                }
            }

            //If there is still something in lastChars add it to the split equation list
            if (lastChars != "")
            {
                splitEquationArray.Add(lastChars);
                lastChars = "";
            }

            return splitEquationArray;
        }

        //Handle multiplication and division
        public List<string> calcMultDiv(List<string> splitEquationString)
        {
            //While the list contains "*" or "/" perform said multiplication or division from left to right
            while (splitEquationString.Contains("*") || splitEquationString.Contains("/"))
            {
                //Find the first "*" or "/"
                int index = splitEquationString.FindIndex(s => s == "*" || s == "/");

                float tempResult = 0;

                if (splitEquationString[index] == "*")
                {
                    //Multiply the numbers before and after the first "*"
                    tempResult = float.Parse(splitEquationString[index - 1]) * float.Parse(splitEquationString[index + 1]);
                }
                else if (splitEquationString[index] == "/")
                {
                    //Divide the numbers before and after the first "/"
                    tempResult = float.Parse(splitEquationString[index - 1]) / float.Parse(splitEquationString[index + 1]);
                }

                //Add the result of multiplication or division to the list at the index of "*" or "/"
                splitEquationString[index] = tempResult.ToString();

                //Remove the numbers used in multiplication or division
                splitEquationString.RemoveAt(index + 1);
                splitEquationString.RemoveAt(index - 1);
            }

            //Return the left over list
            return splitEquationString;
        }

        //Handle addition and subtraction
        public float calcAddSub(List<string> splitEquationString)
        {
            //While the list contains "+" or "-" perform said addition or subtraction from left to right
            while (splitEquationString.Contains("+") || splitEquationString.Contains("-"))
            {
                //Find the first "+" or "-"
                int index = splitEquationString.FindIndex(s => s == "+" || s == "-");

                float tempResult = 0;

                if (splitEquationString[index] == "+")
                {
                    //Add the numbers before and after the first "+"
                    tempResult = float.Parse(splitEquationString[index - 1]) + float.Parse(splitEquationString[index + 1]);
                }
                else if (splitEquationString[index] == "-")
                {
                    //Subtract the numbers before and after the first "-"
                    tempResult = float.Parse(splitEquationString[index - 1]) - float.Parse(splitEquationString[index + 1]);
                }

                //Add the result of addition or subtraction to the list at the index of "+" or "-"
                splitEquationString[index] = tempResult.ToString();

                //Remove the numbers used in addition or subtraction
                splitEquationString.RemoveAt(index + 1);
                splitEquationString.RemoveAt(index - 1);
            }

            //return the remaining number
            return float.Parse(splitEquationString[0]);
        }
    }
}
