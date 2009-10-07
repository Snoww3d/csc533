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
        private List<Rule> rules;                           //main list of rules
        //public List<Rule> Rules { get { return rules; } }
        private Dictionary<Rule, bool> inferredRules;       //list of rules proved true -- used during forward chaining
        private Dictionary<string, bool> knownSymbols;      //list of symbols proven true or false -- used during backward chaining
        private List<Rule> visitedRules;                    //list of rules visited -- used during backward chaining
        private string log;
        public string Log { get { return log; } }

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
            log = "";
            return entails(symbol);
        }


        //Use backward chaining to determine the truth of a symbol
        public bool AskBackward(string symbol)
        {
            visitedRules = new List<Rule>();
            knownSymbols = new Dictionary<string, bool>();
            log = "";
            return check(symbol);
        }


        //Forward Chaining Algorithm
        private bool entails(string symbol)
        {

            Dictionary<Rule, int> count;        // a table indexed by clause, initially the number of permises
            Dictionary<string, bool> inferred;  // a table indexed by symbol, each entry intially false;
            Stack<string> agenda;               // a list of symbols, initially the symbols known to be true in KB

            count = new Dictionary<Rule, int>();
            inferred = new Dictionary<string, bool>();
            agenda = new Stack<string>();

            foreach (Rule rule in rules)
            {
                foreach (string s in rule.Premises)
                    if (!inferred.ContainsKey(s))
                        inferred.Add(s, false);

                if (rule.IsSymbol())
                {
                    log += "Add "+ rule + " to the knownledge base\r\n";

                    agenda.Push(rule.Conclusion);
                }
                else
                    count.Add(rule, rule.Premises.Count());
            }


            while (agenda.Count != 0)
            {
                string P = agenda.Pop();
                log += "Checking all rules that contain "+P + "\r\n";

                if (P == symbol) return true;

                if (inferred.ContainsKey(P) && !inferred[P])
                {
                    inferred[P] = true;
                    foreach (Rule rule in rules)
                    {
                        if (rule.Premises.Contains(P))
                        {
                            count[rule]--;
                            if (count[rule] == 0)
                            {
                                agenda.Push(rule.Conclusion);
                                log += rule + "\r\n";
                            }
                        }
                    }
                }

            }

            return false;
        }



        //Backward chaining algorithm
        private bool check(string conclusion)
        {
            log += "Checking symbol " + conclusion + ".\r\n";

            //Avoid unnecessary work by checking if the symbol is already known true or false            
            if (knownSymbols.ContainsKey(conclusion))
            {
                if (knownSymbols[conclusion])
                {
                    log += conclusion + " has been proven true.\r\n";
                    return true;
                }
                else
                {
                    log += conclusion + " has been proven false.\r\n";
                    return false;
                }
            }

            //Evaluate all rules with this conclusion until either a rule is true
            //or we run out of rules.
            List<Rule> rulesWithConclusion = findRulesWithConclusion(conclusion);
            bool cycle = false;
            
            foreach (Rule rule in rulesWithConclusion)
            {
                log += "Found rule " + rule.ToString() + ".\r\n";

                //If the rule is a symbol, it is a known true.
                if (rule.IsSymbol())
                {
                    knownSymbols[conclusion] = true;
                    log += conclusion + " is true.\r\n";
                    return true;
                }

                //Check if rule has already been visited
                if (visitedRules.Contains(rule))
                {
                    log += "Rule " + rule.ToString() + " depends on its own conclusion...skipping rule.\r\n";
                    cycle = true;
                    continue;
                }
                else
                {
                    visitedRules.Add(rule);
                }

                //If the rule is not known true or false and hasn't been visited, 
                //recursively evaluate its premises
                bool result = true;
                foreach (string term in rule.Premises)
                {
                    result = check(term);
                    if (!result)
                    {
                        log += "Rule " + rule.ToString() + " is false.\r\n";
                        break;
                    }
                }

                //If all terms are true, we can return true; otherwise, go to the next rule.
                if (result)
                {
                    if (!knownSymbols.ContainsKey(conclusion))
                        knownSymbols.Add(conclusion, true);
                    
                    log += conclusion + " is true by rule " + rule.ToString() + ".\r\n";
                    return true;
                }
            }

            //We have run out of rules to evaluate. The conclusion is false.
            log += "No further rules conclude " + conclusion;
            if (cycle)
                log += ".\r\n";
            else
            {
                if (!knownSymbols.ContainsKey(conclusion))
                    knownSymbols.Add(conclusion, false);
                log += ". " + conclusion + " is false.\r\n";
            }

            return false;
        }


        //Creates a list of all rules that have the specified conclusion
        private List<Rule> findRulesWithConclusion(string symbol)
        {
            List<Rule> result = new List<Rule>();

            foreach (Rule rule in rules)
            {
                if (rule.Conclusion == symbol)
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
                result += "\r\n";
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
