using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CSC533
{
    //Knowledgebase class to store a conjunction of rules
    //Can add rules to or remove them from the knowledgebase
    //Can query symbols with forward or backward chaining
    public class Knowledgebase
    {
        private List<Rule> rules;                       //main list of rules
        //public List<Rule> Rules { get { return rules; } }
        private Dictionary<Rule, bool> inferredRules;   //list of rules proved true -- used during forward chaining
        private Dictionary<Rule, bool> visitedRules;    //list of rules visited--used during chaining


        //Constructor
        public Knowledgebase()
        {
            rules = new List<Rule>();
        }


        //Add a rule to the knowledgebase
        public void Tell(Rule rule)
        {
            if (!ContainsRule(rule))
                rules.Add(rule);
            else
                MessageBox.Show("That rule already exists.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }


        //Remove a rule from the knowledgebase
        public void Remove(Rule rule)
        {
            int limit = rules.Count;

            for (int i = 0; i < limit; i++)
            {
                if (rules[i].Equals(rule))
                {
                    rules.RemoveAt(i);
                    limit--;
                }
            }
        }


        //Use forward chaining to determine the truth of a symbol
        public bool AskForward(string symbol)
        {
            inferredRules = new Dictionary<Rule, bool>();
            return entails(symbol);
        }

       


        //Use backward chaining to determine the truth of a symbol
        public bool AskBackward(string conclusion)
        {
            visitedRules = new Dictionary<Rule, bool>();
            return check(conclusion);
        }

        //Forward Chaining Algorithm
        private bool entails(string symbol)
        {

            Dictionary<Rule, int> count;      // a table indexed by clause, initially the number of permises
            Stack<string> agenda;          // a list of symbols, initially the symbols known to be true in KB
            agenda = new Stack<string>();

            foreach(Rule rule in rules)
            {
                if (rule.IsSymbol())
                {
                    agenda.Push(rule.Consequent);           
                }
            }


            while (agenda.Count != 0)
            {
                string P = agenda.Pop();
                if (P == symbol) return true;

            }


            return false;
        }

    

        //Backward chaining algorithm
        private bool check(string conclusion)
        {
            List<Rule> rulesWithConclusion = findRulesWithConclusion(conclusion);

            //Evaluate all rules with this conclusion until either a rule is true
            //or we run out of rules.
            foreach (Rule rule in rulesWithConclusion)
            {
                MainForm.ActiveForm.Controls["outputLabel"].Text += rule + "\n";
                
                //Check if rule has already been visited
                if (visitedRules.ContainsKey(rule))
                {
                    //Already known to be true
                    if (visitedRules[rule])
                        return true;

                    //Cycle in graph; skip this rule to avoid infinite loop
                    else
                        continue;
                }
                else
                {
                    visitedRules.Add(rule, false);
                }

                //If the rule is a symbol, it is a known true. This is one base case.
                if (rule.IsSymbol())
                {
                    visitedRules[rule] = true;
                    return true;
                }

                //Otherwise we recursively evaluate the rule
                else
                {
                    bool result = true;

                    //Evaluate each term in the antecedents of this rule
                    //and accumulate the result
                    foreach (string term in rule.Antecedents)
                    {
                        result = result && check(term);
                    }

                    //If all terms are true, we can return true; otherwise, go
                    //to the next rule.
                    if (result)
                    {
                        visitedRules[rule] = true;
                        return true;
                    }
                }
            }

            //We have run out of rules to evaluate. The conclusion is false - the other base case.
            return false;
        }


        //Creates a list of all rules that have the specified conclusion
        private List<Rule> findRulesWithConclusion(string symbol)
        {
            List<Rule> result = new List<Rule>();

            foreach (Rule rule in rules)
            {
                if (rule.Consequent == symbol)
                {
                    result.Add(rule);
                }
            }

            return result;
        }


        //Override toString() method to output all rules as a string
        public override string ToString()
        {
            string result = "";
            foreach (Rule rule in rules)
            {
                result += rule.ToString();
                result += "\n\r";
            }

            return result;
        }


        //Returns true if the knowledgebase contains the rule specified
        //in the parameter.
        public bool ContainsRule(Rule rule)
        {
            foreach (Rule comparedRule in rules)
            {
                if (comparedRule.Equals(rule))
                    return true;
            }

            return false;
        }


        //Write the knowledgebase as text to the specified StreamWriter
        public void SaveToText(StreamWriter writer)
        {
            foreach (Rule rule in rules)
            {
                writer.WriteLine(rule.ToString());
            }
        }
    }
}
