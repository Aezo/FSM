using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public class Class1
    {
        private static int[,] Graph;
        private static Regex[] values;
        private static string[] result;

        /// <summary>
        /// Entry point in program. First line of input should contain path for Finite State Machine Construction.
        /// The subsequent lines contain the input to be tested for patterns.
        /// </summary>
        public static int Main()
        {
            string fsmPath = Console.ReadLine();

            FiniteStateMachine(fsmPath);
            
            return 0;
        }

        /// <summary>
        /// Display Matched results in Console
        /// </summary>
        private static void DisplayMatchedResult()
        {
            int i;

            Console.Write(result[0]);

            for (i = 1; i < result.Length; i++)
                Console.Write(" " + result[i]);
            Console.Write("\n");
        }

        /// <summary>
        /// Finite State Machine. Finite State Machine is read from a file. Each node of finite state machine is composed of regex expressions. If the expression matches the current input, then the input is considered correct.
        /// </summary>
        /// <returns>Returns the number of sequences found in the stream of inputs</returns>
        private static int FiniteStateMachine(string fsmPath)
        {
            int c = 0, matches = 0, state = 0, n;
            string line;

            if (ConstructFSM(fsmPath, out n))
            {
                while ((line = Console.ReadLine()) != null)
                {
                    state = TransitionFunction(state, line);
                    if (state == n)
                    {
                        #if !DEBUG
                        DisplayMatchedResult();
                        #endif
                        state = 0;
                        matches++;
                    }
                    c++;
                }
            }

            #if DEBUG
            Console.WriteLine("Well I read " + c.ToString() + " lines, and I found " + matches.ToString() + " matches.");
            #endif
            Console.ReadLine();
            return 0;
        }

        /// <summary>
        /// Transition from current state to the next state depending on input
        /// </summary>
        /// <param name="state">Current state</param>
        /// <param name="currentInput"></param>
        /// <returns></returns>
        private static int TransitionFunction(int state, string currentInput)
        {
            if (currentInput != null)
            {
                if (values[Graph[state, 1]].IsMatch(currentInput))
                {
                    result[state] = currentInput;
                    return Graph[state, 1];
                }
                else
                {
                    return Graph[state, 0];
                }
            }
            return 0;
        }

        /// <summary>
        /// Create a finite state machine from the input file provided.
        /// </summary>
        /// <param name="fsmPath">Path of file that contains instructions to create a finite state machine.</param>
        private static bool ConstructFSM(string fsmPath, out int fsmSize)
        {
            int i, n=0, from, to;
            string text;
            string[] num;
            bool flag = false;
            fsmSize = 0;

            using (TextReader reader = File.OpenText(fsmPath))
            {
                //The first line contains 'n' = the number of states in finite state machine.

                if((text = reader.ReadLine()) != null)
                    flag = int.TryParse(text, out n);
                if (flag)
                {   
                    fsmSize = n;
                    //Read the next 'n' lines containing Regex for each state.
                    Graph = new int[n+1, 2];
                    values = new Regex[n+1];
                    result = new string[n];

                    for(i=1; i<=n && !string.IsNullOrEmpty(text = reader.ReadLine()); i++)
                    {
                        values[i] = new Regex(text);
                    }

                    //Until the end of input, read the directed edges.
                    while(!string.IsNullOrEmpty(text = reader.ReadLine()))
                    {
                        num = text.Split(' ');
                        if (num.Length == 3)
                        {
                            from = int.Parse(num[0]);
                            to = int.Parse(num[1]);
                            if (num[2].CompareTo("x") == 0)
                                Graph[from, 0] = to;
                            else if (num[2].CompareTo("o") == 0)
                                Graph[from, 1] = to;
                        }
                    }
                    return true;
                }
                return false;
            }
        }
    }
}
