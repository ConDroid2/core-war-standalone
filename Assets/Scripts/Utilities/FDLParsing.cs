using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FDLParsing
{
    public static int[] FindStartEndOfFunction(string func)
    {
        int startIndex = 0;
        int endIndex = 0;

        for (int i = 0; i < func.Length; i++)
        {
            if (func[i] == '(')
            {
                startIndex = i;
                break;
            }
        }

        int parenthCount = 0;
        for (int i = startIndex + 1; i < func.Length; i++)
        {
            if (func[i] == '(')
            {
                parenthCount++;
            }
            else if (parenthCount > 0 && func[i] == ')')
            {
                parenthCount--;
            }
            else if (parenthCount == 0 && func[i] == ')')
            {
                endIndex = i;
                break;
            }
        }

        int[] startEnd = { startIndex, endIndex };
        return startEnd;
    }

    public static string GetFunctionName(string func, int[] startEnd)
    {
        return func.Substring(0, startEnd[0]);
    }

    public static string[] GetFunctionArgs(string func, int[] startEnd)
    {
        string fullStringArgs = func.Substring(startEnd[0] + 1, startEnd[1] - startEnd[0] - 1);
        fullStringArgs += ',';

        bool insideParenthesis = false;
        int startOfArg = 0;
        int parenthCount = 0;

        List<string> args = new List<string>();

        //Debug.Log("Starting to find args: " + fullStringArgs);
        for (int i = 0; i < fullStringArgs.Length; i++)
        {
            if (fullStringArgs[i] == '(' && !insideParenthesis)
            {
                insideParenthesis = true;
            }
            else if (insideParenthesis && fullStringArgs[i] == '(')
            {
                parenthCount++;
            }
            else if (insideParenthesis && fullStringArgs[i] == ')' && parenthCount > 0)
            {
                parenthCount--;
            }
            else if (insideParenthesis && fullStringArgs[i] == ')' && parenthCount == 0)
            {
                insideParenthesis = false;
            }

            if (fullStringArgs[i] == ',' && !insideParenthesis)
            {
                args.Add(fullStringArgs.Substring(startOfArg, i - startOfArg));
                startOfArg = i + 1;
            }
        }

        return args.ToArray();
    }
}
