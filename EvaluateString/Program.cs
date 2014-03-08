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
                string expression  = "3+4*8/2-1";
                Console.WriteLine("Result of evaluating {0} is {1}", expression, Evaluate(expression));
            }
            catch (Exception e)
            {
                Console.WriteLine("Error is {0}", e.Message);
            }
        }

        static int Evaluate(string expression)
        {
            Stack<int> operandStack = new Stack<int>();
            Stack<char> operatorStack = new Stack<char>();

            int current = 0;

            int number = 0;

            while (current < expression.Length)
            {
                if (IsNumber(expression[current], out number))
                {
                    operandStack.Push(number);
                    current++;
                }
                else if (expression[current] == '*' || expression[current] == '/')
                {
                    if (current + 1 < expression.Length)
                    {
                        int nextNumber = 0;
                        if (IsNumber(expression[current + 1], out nextNumber))
                        {
                            int top = operandStack.Pop();

                            if (expression[current] == '*')
                            {
                                operandStack.Push(top * nextNumber);
                            }
                            else
                            {
                                operandStack.Push(top / nextNumber);
                            }

                            current = current + 2;
                        }
                        else
                        {
                            throw new Exception("Invalid argument");
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Invalid input");
                    }
                }
                else
                {
                    operatorStack.Push(expression[current]);
                    current++;
                }
            }

            // Now handle the + and - operations
            while (operatorStack.Count > 0)
            {
                char oper = operatorStack.Pop();

                int operand2 = operandStack.Pop();
                int operand1 = operandStack.Pop();

                if (oper == '+')
                {
                    operandStack.Push(operand1 + operand2);
                }
                else if (oper == '-')
                {
                    operandStack.Push(operand1 - operand2);
                }
            }

            return operandStack.Peek();
        }

        static bool IsNumber(char digit, out int number)
        {
            return Int32.TryParse(digit.ToString(), out number);
        }
    }
}
