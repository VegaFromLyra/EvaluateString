using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Given a string containing digits 
// and one of the 4 mathematical operators
// calculate the result by following 
// the precedence of operators
// Example: 3 + 4 * 8 / 2 - 1 = 
// = 3 + 16 - 1 = 18
namespace EvaluateString
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Test("3 + 4 * 8 / 2 - 1");
                Test("12 * 8 - 8 + 8");
                Test("12 * 8 + 11 * 11");
                Test("2 + 7 - 3 * 15 / 3 * 6");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error is {0}", e.Message);
            }
        }

        static void Test(string expression)
        {
            Console.WriteLine("Result of evaluating {0} is {1}", expression, Evaluate(expression));
        }

        static int Evaluate(string expression)
        {
            Stack<int> operandStack = new Stack<int>();
            Stack<string> operatorStack = new Stack<string>();

            int current = 0;

            int number = 0;

            string[] tokens = expression.Split(' ');

            while (current < tokens.Length)
            {
                if (IsNumber(tokens[current], out number))
                {
                    operandStack.Push(number);
                }
                else
                {
                    while (operatorStack.Count > 0 && 
                        hasHigherPrecedence(operatorStack.Peek(), tokens[current]))
                    {
                        int operand2 = operandStack.Pop();
                        int operand1 = operandStack.Pop();
                        operandStack.Push(Operate(operand1, operand2, operatorStack.Pop()));
                    }

                    operatorStack.Push(tokens[current]);
                }

                current++;
            }

            // Now handle the remaidner operations
            while (operatorStack.Count > 0)
            {
                string oper = operatorStack.Pop();

                int operand2 = operandStack.Pop();
                int operand1 = operandStack.Pop();

                operandStack.Push(Operate(operand1, operand2, oper));
            }

            return operandStack.Peek();
        }

        static bool IsNumber(string digit, out int number)
        {
            return Int32.TryParse(digit, out number);
        }

        static int Operate(int operand1, int operand2, string oper)
        {
            if (oper.Equals("+"))
            {
                return operand1 + operand2;
            }
            else if (oper.Equals("-"))
            {
                return operand1 - operand2;
            }
            else if (oper.Equals("*"))
            {
                return operand1 * operand2;
            }
            else if (oper.Equals("/"))
            {
                return operand1 / operand2;
            }
            else
            {
                throw new Exception("Invalid operator");
            }
        }

        // Returns true if s1 has higher or equal 
        // precedence as s2
        static bool hasHigherPrecedence(string s1, string s2)
        {
            if ((s1.Equals("+") || s1.Equals("-")) &&
               (s2.Equals("*") || s2.Equals("/")))
            {
                return false;
            }

            return true;
        }
    }
}
