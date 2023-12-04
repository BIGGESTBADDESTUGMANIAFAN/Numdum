using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumdumInterpretor
{
    /// <summary>
    /// An enum representing every keyword the user can input.
    /// </summary>
    public enum Keyword
    {
        ADD, MUL, SUB, DIV, SET, TO, AND, OUTPUT, OUTPUTR, INPUT, INPUTR, GOTO, DEST, END, BLANKLINE
    }
    /// <summary>
    /// A structure that represents variables.
    /// </summary>
    public struct Variable
    {
        /// <summary>
        /// This stores the values of all variables, with key int (stated value) value float (actual value)
        /// </summary>
        public static Dictionary<int, float> Variables = new Dictionary<int, float>(); 
        int Statedvalue; //the stated value of the variable (The 5 in "GOTO5" would have a stated value of 5)
        public bool IsPointer; //Bool representing if this instance of a Variable is a pointer (starts with a '*')
        public override string ToString()
        {
            return (IsPointer ? "*" : "") + Statedvalue.ToString();
        }

        /// <summary>
        /// The property to get and set a variables true variable (value of the Variables Dictionary).
        /// Essentially just sets Variables[Statedvalue] To value, or returns Variables[Statedvalue].
        /// UNLESS: This variable is a pointer; then it feeds through 1 cycle of this, where it will set or get Variables[(int)Variables[Statedvalue]]
        /// See also: <seealso cref="Variables"/>
        /// </summary>
        public float Value
        {
            get
            {
                if (!Variables.ContainsKey(this.Statedvalue))
                {
                    Variables.Add(this.Statedvalue, this.Statedvalue);
                }
                if (IsPointer)
                {
                    if (!Variables.ContainsKey((int)Variables[Statedvalue]))
                    {
                        Variables.Add((int)Variables[Statedvalue], Variables[Statedvalue]);
                    }
                    return Variables[(int)Variables[Statedvalue]];
                }
                else
                {
                    return Variables[this.Statedvalue];
                }
            }
            set
            {
                if (!Variables.ContainsKey(this.Statedvalue))
                {
                    Variables.Add(this.Statedvalue, this.Statedvalue);
                }
                if (IsPointer)
                {
                    if (!Variables.ContainsKey((int)Variables[Statedvalue]))
                    {
                        Variables.Add((int)Variables[Statedvalue], Variables[Statedvalue]);
                    }
                    Variables[(int)Variables[Statedvalue]] = value;
                }
                else
                {
                    Variables[this.Statedvalue] = value;
                }
            }
        }

        /// <summary>
        /// Constructor for Variables
        /// </summary>
        /// <param name="Value"> the statedvalue of the variable</param>
        /// <param name="IsPointer">whether or not it is a pointer</param>
        public Variable(int Value, bool IsPointer)
        {
            Statedvalue = Value;
            this.IsPointer = IsPointer;
        }
        /// <summary>
        /// Gets the variable in the line, starting from character 0
        /// </summary>
        /// <param name="Line">The string 'line' that it reads the number from</param>
        /// <returns> A</returns>
        /// <exception cref="FormatException"></exception>
        public static Variable GetVariableInLine(string Line)
        {
            Variable Var = new Variable();
            List<char> Number = new List<char>();
            var TLine = Line;
            if (TLine[0] == '*')
            {
                Var.IsPointer = true;
                TLine = TLine.Replace("*", "");
            }
            else
            {
                Var.IsPointer = false;
            }
            while (TLine.Length != 0)
            {
                if ("0123456789-".Contains(TLine[0]))
                {
                    Number.Add(TLine[0]);
                    TLine = TLine.Remove(0, 1);
                }
                else
                {
                    break;
                }
            }
            Var.Statedvalue = int.Parse(new string(Number.ToArray()));
            return Var;
        }
    }
}
