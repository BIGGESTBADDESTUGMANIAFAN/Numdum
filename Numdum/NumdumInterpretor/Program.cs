using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NumdumInterpretor { 
    static class Interpretor
    {
        static void Main(string[] args)
        {
            //verifying someone opened the Interpretor with a file.
            if (args.Length == 0)
            {
                Console.WriteLine("Open interpreter with a file! Drag file on top of interpreter, or enter directory into the console as an argument. \nPress any key to close.");
                _ = Console.ReadKey();
                return;
            }
            //CodeTemp is the raw text of the script the user wants to run.
            List<string> CodeTemp;
            try
            {
                //Read text from file, split it into individual lines.
                CodeTemp = System.IO.File.ReadAllText(args[0]).Split('\n').ToList();
            }
            catch
            {
                //Read the user an error if they gave an incorrect directory. 
                Console.WriteLine("Open interpreter with a valid file!\nPress any key to close.");
                _ = Console.ReadKey();
                return;
            }
            //reverse the order of the script, so it reads bottom to top.
            CodeTemp.Reverse();
            List<List<dynamic>> InterpretedCode = new List<List<dynamic>>(); //InterpretedCode is tokenised code. Every entry should be of type Variable, Keyword or int. the int entries are GOTO destination targets.
            
            foreach (string Line in new List<string>(CodeTemp)) // I modify CodeTemp in this loop, so I need to use the new List<T>(old) method
            {
                if (Line.Contains(' ') || Line.Contains('\t'))
                {
                    //Throw error immediately if any whitespace is picked up
                    Console.WriteLine("ERRORONLINE" + Line.IndexOf(Line).ToString() + ":WHITESPACEISFORTHEWEAK.LINE" + ">>" + Line);
                    _ = Console.ReadKey();
                    return;
                }

                //Remove whilespace lines, and lines starting with '*'.
                if (Line == "" || Line.StartsWith("*"))
                {
                    CodeTemp.Remove(Line);
                }
            }
            //Interpret the raw string code into tokenised code.
            for(int IDX = 0; IDX < CodeTemp.Count; IDX++)
            {
                
                InterpretedCode.Add(new List<dynamic>());
                //Line is the raw text line that is currently being converted into Tokens
                var Line = CodeTemp[IDX];
                //Initialise the line of tokens
                InterpretedCode[IDX] = new List<dynamic>();

                //Want to check for 69 first, as I want it to have priority over GOTO destinations.
                if (Line.StartsWith("69"))
                {
                    InterpretedCode[IDX].Add(Keyword.END);
                    continue;
                }
                //The following checks if the line is a GOTO destination
                try
                {
                    InterpretedCode[IDX].Add(Keyword.DEST); //It adds Keyword.DEST immediately. Keyword.DEST is implied, and is not explicitly stated in the string code.
                    InterpretedCode[IDX].Add(Variable.GetVariableInLine(Line).Value); //Variable.GetVariableInLine throws an exception if the character at index zero of line is not: [*,-,0,1,2,3,4,5,6,7,8,9]. As I previously deleted any lines containing '*', only numbers will get picked up.
                    continue;
                }
                catch
                {
                    InterpretedCode[IDX].Remove(Keyword.DEST); //Remove keyword.DEST if it fails.
                }
                try
                {
                    if (Line.StartsWith("ADD"))
                    {
                        InterpretedCode[IDX].Add(Keyword.ADD); //If the line starts with ADD, append Keyword.ADD to InterpretedCode
                        Line = Line.Replace("ADD", ""); //Remove the word ADD from the string line
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable); //Assumes the next character is a vaild number, then adds the result of Variable.GetVariableInLine(Line) to InterpretedCode
                        if (Line.StartsWith("*")) { Line = Line.Remove(0, 1); } //Special exception for pointers
                        Line = Line.Remove(0, variable.Value.ToString().Length); //Remove the number.
                        if (Line.StartsWith("AND"))
                        {
                            Line = Line.Replace("AND", ""); //If line does have AND keyword, remove AND from line
                            InterpretedCode[IDX].Add(Keyword.AND); //Add Keyword.AND to interpreted code.
                            variable = Variable.GetVariableInLine(Line); //Get the number after the AND keyword.
                            InterpretedCode[IDX].Add(variable); //Add this to the interpreted code.
                        }
                        else 
                        {
                            //Yeet line if does not have AND keyword.
                            InterpretedCode[IDX].Clear();
                        }
                        continue;
                    } 
                    //NOTE: I found no reason in commenting the other operators. They are all carbon copies of ADD, except check for a different initial word and have a differect Keyword.
                    if (Line.StartsWith("SUB"))
                    {
                        InterpretedCode[IDX].Add(Keyword.SUB);
                        Line = Line.Replace("SUB", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable);
                        if (Line.StartsWith("*")) { Line = Line.Remove(0, 1); }
                        Line = Line.Remove(0, variable.Value.ToString().Length);
                        if (Line.StartsWith("AND"))
                        {
                            Line = Line.Replace("AND", "");
                            InterpretedCode[IDX].Add(Keyword.AND);
                            variable = Variable.GetVariableInLine(Line);
                            InterpretedCode[IDX].Add(variable);
                        }
                        else
                        {
                            InterpretedCode[IDX].Clear();
                        }
                        continue;
                    }
                    if (Line.StartsWith("MUL"))
                    {
                        InterpretedCode[IDX].Add(Keyword.MUL);
                        Line = Line.Replace("MUL", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable);
                        if (Line.StartsWith("*")) { Line = Line.Remove(0, 1); }
                        Line = Line.Remove(0, variable.Value.ToString().Length);
                        if (Line.StartsWith("AND"))
                        {
                            Line = Line.Replace("AND", "");
                            InterpretedCode[IDX].Add(Keyword.AND);
                            variable = Variable.GetVariableInLine(Line);
                            InterpretedCode[IDX].Add(variable);
                        }
                        else
                        {
                            InterpretedCode[IDX].Clear();
                        }
                        continue;
                    }
                    if (Line.StartsWith("DIV"))
                    {
                        InterpretedCode[IDX].Add(Keyword.DIV);
                        Line = Line.Replace("DIV", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable);
                        if (Line.StartsWith("*")) { Line = Line.Remove(0, 1); }
                        Line = Line.Remove(0, variable.Value.ToString().Length);
                        if (Line.StartsWith("AND"))
                        {
                            Line = Line.Replace("AND", "");
                            InterpretedCode[IDX].Add(Keyword.AND);
                            variable = Variable.GetVariableInLine(Line);
                            InterpretedCode[IDX].Add(variable);
                        }
                        else
                        {
                            InterpretedCode[IDX].Clear();
                        }
                        continue;
                    }
                    //Set is slightly different:
                    if (Line.StartsWith("SET"))
                    {
                        InterpretedCode[IDX].Add(Keyword.SET);
                        Line = Line.Replace("SET", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable);
                        if (Line.StartsWith("*")) { Line = Line.Remove(0, 1); }
                        Line = Line.Remove(0, variable.Value.ToString().Length);
                        if (Line.StartsWith("TO")) //Checks for "TO" rather than "AND"
                        {
                            Line = Line.Replace("TO", "");
                            InterpretedCode[IDX].Add(Keyword.TO); //And then adds the keyword "TO" to Interpreted code. Neither TO nor AND actually get used on runtime though.
                            variable = Variable.GetVariableInLine(Line);
                            InterpretedCode[IDX].Add(variable);
                        }
                        else
                        {
                            InterpretedCode[IDX].Clear();
                        }
                        continue;
                    }
                    
                    //NOTES: INPUTR and OUTPUTR are short for INPUT RAW and OUTPUT RAW. All user inputs and GOTO statements use the same syntax, with <Keyword><variable>
                    // INPUTR and OUTPUTR are queried first, as the word "INPUTR" or "OUTPUTR" begins with "INPUT"/"OUTPUT" respectively and thus can be mistaken by the interpreter.
                    if (Line.StartsWith("INPUTR"))
                    {
                        InterpretedCode[IDX].Add(Keyword.INPUTR); //Add Keyword.INPUTR to InterpretedCode if the string Line starts with INPUTR
                        Line = Line.Replace("INPUTR", "");
                        var variable = Variable.GetVariableInLine(Line); 
                        InterpretedCode[IDX].Add(variable); //Then Add the following variable to InterpretedCode
                        continue;
                    }

                    
                    if (Line.StartsWith("OUTPUTR"))
                    {
                        InterpretedCode[IDX].Add(Keyword.OUTPUTR); //Add Keyword.OUTPUTR to InterpretedCode if the string Line starts with OUTPUTR
                        Line = Line.Replace("OUTPUTR", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable); //Then Add the following variable to InterpretedCode
                        continue;
                    }
                    if (Line.StartsWith("INPUT"))
                    {
                        InterpretedCode[IDX].Add(Keyword.INPUT);//Add Keyword.INPUT to InterpretedCode if the string Line starts with INPUT
                        Line = Line.Replace("INPUT", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable); //Then Add the following variable to InterpretedCode
                        continue;
                    }

                    if (Line.StartsWith("OUTPUT"))
                    {
                        InterpretedCode[IDX].Add(Keyword.OUTPUT); //Add Keyword.OUTPUT to InterpretedCode if the string Line starts with OUTPUT
                        Line = Line.Replace("OUTPUT", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable); //Then Add the following variable to InterpretedCode
                        continue;
                    }
                    if (Line.StartsWith("GOTO"))
                    {
                        InterpretedCode[IDX].Add(Keyword.GOTO); //Add Keyword.GOTO to InterpretedCode if the string Line starts with GOTO
                        Line = Line.Replace("GOTO", "");
                        var variable = Variable.GetVariableInLine(Line);
                        InterpretedCode[IDX].Add(variable); //Then Add the following variable to InterpretedCode
                        continue;
                    }
                }
                catch
                {
                    InterpretedCode[IDX].Clear(); //If any exceptions occured due to incorrect syntax, delete line.
                }
                InterpretedCode[IDX].Clear(); //If no keywords were detected, delete line.
            }
            //Clean up any deleted lines. for my own sanity, I do this here rather than the loop.
            foreach (var i in new List<List<dynamic>>(InterpretedCode))
            {
                if (i.Count == 0)
                {
                    InterpretedCode.Remove(i);
                }
            }

            //If no code got interpreted, tell this to the user and close.
            if(InterpretedCode.Count == 0)
            {
                Console.WriteLine("File selected contains no valid code. \nPress any key to close.");
                _ = Console.ReadKey();
                return;
            }

            //PTR is the line currently being read by the interpretor. 
            int PTR = 0;
            
            while(true) //Begin infinite loop
            {

                var Line = InterpretedCode[PTR]; //make a temporary variable Line, this is a List of : Keywords, variables and ints.
                switch (Line[0]) //query the first keyword in the line:
                {
                    case Keyword.SET: //If it is a set statement, set Variable 1 (located as the second entry in line) To variable 2
                        Line[1].Value = Line[3].Value;
                        break;
                    case Keyword.ADD: //If it is an add statement, set Variable 1 (located as the second entry in line) To variable 2 + variable 1
                        Line[1].Value += Line[3].Value;
                        break;
                    case Keyword.MUL: //If it is a mul statement, set Variable 1 (located as the second entry in line) To variable 2 * variable 1
                        Line[1].Value *= Line[3].Value;
                        break;
                    case Keyword.DIV: //If it is a div statement, set Variable 1 (located as the second entry in line) To variable 2 / variable 1
                        if (Line[3].Value != 0) //Special exceptionf or if it would be a divisoin by zero, do nothing.
                        {
                            Line[1].Value /= Line[3].Value;
                        }
                        break;
                    case Keyword.SUB: //If it is a sub statement, set Variable 1 (located as the second entry in line) To variable 2 - variable 1
                        Line[1].Value -= Line[3].Value;
                        break;
                    case Keyword.GOTO: //If it is a GOTO statement, set PTR to the line starting with the DEST keyword with an int equal to this Line's Variable.Value.
                        foreach (var Line2 in InterpretedCode)
                        { 
                            if(Line2[0] == Keyword.DEST && Line2[1] == Line[1].Value)
                            {
                                PTR = InterpretedCode.IndexOf(Line2);
                            }
                            //If it never finds a matching destination, it gets ignored.
                        }
                        break;
                    case Keyword.OUTPUT:
                        Console.Write(((char)Line[1].Value).ToString()); //Output statements write 1 character equal to the ASCII code of Line[1].Value (Line[1] is guaranteed to be a variable type.)
                        break;
                    case Keyword.OUTPUTR:
                        Console.Write(Line[1].Value); //Output statements just write Line[1].Value (Line[1] is guaranteed to be a variable type.)
                        break;
                    case Keyword.INPUT:
                        Line[1].Value = (float)Encoding.ASCII.GetBytes(Console.ReadKey().KeyChar.ToString())[0]; //Read the next key the user inputs to Line[1]'s value (Line[1] is guaranteed to be a variable type.)
                        if (Line[1].Value == 13) 
                        {
                            Console.CursorTop += 1; //move the cursor up to compensate for if the user presses enter (ascii code 13 is the Carriage Return)
                        }
                        break;
                    case Keyword.INPUTR:
                        float V;
                        while (!float.TryParse(Console.ReadLine(), out V)) //Repeatedly ask the user for a number, until they enter something that can parse to a float
                        {
                            Console.WriteLine("Input a valid number");
                        }
                        Line[1].Value = V; //Then assign this to Line[1].Value
                        Console.CursorTop -= 1; //Move the cursor up to compensate for the enter press required to finish inputting your number.
                        break;
                    case Keyword.END: //End script if it reaches a Keyword.END
                        _ = Console.ReadLine(); //Stall until user presses enter, so that you amy see output of a program.
                        return;
                }
                PTR++; //Increment line on
                if(PTR >= InterpretedCode.Count) //Loop pointer, so that when execution moves past final line it loops back to line 1.
                {
                    PTR = 0;
                }
            }
        }
    }
}
