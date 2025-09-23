namespace RekenMachineBackEnd
{
    public class Service
    {
        public float ProcessCalculation(string equationString)
        {
            List<string> splitEquationString = SplitEquationString(equationString);

            splitEquationString = calcMultDiv(splitEquationString);

            float result = calcAddSub(splitEquationString);

            return result;
        }

        //method for splitting string
        public List<string> SplitEquationString(string equationString)
        {
            Array equationArray = equationString.ToCharArray();
            List<string> splitEquationArray = new List<string>();

            string lastChars = "";

            foreach (char character in equationArray)
            {
                int output;

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

            if (lastChars != "")
            {
                splitEquationArray.Add(lastChars);
                lastChars = "";
            }

            return splitEquationArray;
        }

        public List<string> calcMultDiv(List<string> splitEquationString)
        {
            while (splitEquationString.Contains("*") || splitEquationString.Contains("/"))
            {
                int index = splitEquationString.FindIndex(s => s == "*" || s == "/");

                float tempResult = 0;

                if (splitEquationString[index] == "*")
                {
                    tempResult = float.Parse(splitEquationString[index - 1]) * float.Parse(splitEquationString[index + 1]);
                }
                else if (splitEquationString[index] == "/")
                {
                    tempResult = float.Parse(splitEquationString[index - 1]) / float.Parse(splitEquationString[index + 1]);
                }

                splitEquationString[index] = tempResult.ToString();

                splitEquationString.RemoveAt(index + 1);
                splitEquationString.RemoveAt(index - 1);
            }

            return splitEquationString;
        }

        public float calcAddSub(List<string> splitEquationString)
        {
            while (splitEquationString.Contains("+") || splitEquationString.Contains("-"))
            {
                int index = splitEquationString.FindIndex(s => s == "+" || s == "-");

                float tempResult = 0;

                if (splitEquationString[index] == "+")
                {
                    tempResult = float.Parse(splitEquationString[index - 1]) + float.Parse(splitEquationString[index + 1]);
                }
                else if (splitEquationString[index] == "-")
                {
                    tempResult = float.Parse(splitEquationString[index - 1]) - float.Parse(splitEquationString[index + 1]);
                }

                splitEquationString[index] = tempResult.ToString();

                splitEquationString.RemoveAt(index + 1);
                splitEquationString.RemoveAt(index - 1);
            }

            return float.Parse(splitEquationString[0]);
        }

        //method for going through split string and calculating each part with correct priority
    }
}
