/*
 * CSC 533 Artificial Intelligence
 * Project 1 - Forward and Backward Chaining
 * Authors: Shanon Clemmons and Chris Townsend
 * Date: 10/8/09
 * 
 * Knowledgebase.cs
 * 
 */

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
        private Dictionary<Rule, bool> inferredRules;       //list of rules proved true -- used during forward chaining
        private List<string> knownTrueSymbols;              //list of symbols proven true -- used during backward chaining
        private List<string> unknownSymbols;                //list of symbols under evaluation -- used during backward chaining
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
            knownTrueSymbols = new List<string>();
            unknownSymbols = new List<string>();
            log = "";
            if (check(symbol))
                return true;
            else
            {
                log += symbol + " is false.\r\n";
                return false;
            }
        }


        //Forward Chaining Algorithm
        private bool entails(string symbol)
        {

            Dictionary<Rule, int> count;        // a table indexed by clause, initially the number of premises
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

                    log += "Add " + rule + " to the knowledge base\r\n";
                    if (rule.Conclusion == symbol)
                    {
                        log += "Found " + symbol + " to be true\r\n";
                        return true;
                    }
                    agenda.Push(rule.Conclusion);
                }
                else
                    count.Add(rule, rule.Premises.Count());
            }


            while (agenda.Count != 0)
            {
                string P = agenda.Pop();
                log += "Checking all rules that contain " + P + ".\r\n";

                if (P == symbol)
                {
                    log += "Found " + symbol + " to be true.\r\n";
                    return true;
                }

                if (inferred.ContainsKey(P) && !inferred[P])
                {
                    inferred[P] = true;
                    foreach (Rule rule in rules)
                    {
                        if (rule.Premises.Contains(P))
                        {
                            log += "The count of " + rule + " is reduced.\r\n";
                            count[rule]--;
                            if (count[rule] == 0)
                            {
                                agenda.Push(rule.Conclusion);
                                log += "Add " + rule.Conclusion + " to the knowledge base.\r\n";
                            }
                        }
                    }
                }
            }
            log += symbol + " is false.\r\n";
            return false;
        }


        //Backward chaining algorithm
        private bool check(string conclusion)
        {
            log += "Checking symbol " + conclusion + ".\r\n";

            //Avoid unnecessary work by checking if the symbol is already known true           
            if (knownTrueSymbols.Contains(conclusion))
            {
                log += conclusion + " has been proven true.\r\n";
                return true;
            }

            //Evaluate all rules with this conclusion until either a rule is true
            //or we run out of rules.
            List<Rule> rulesWithConclusion = findRulesWithConclusion(conclusion);
            bool cycle = true;

            foreach (Rule rule in rulesWithConclusion)
            {
                log += "Found rule " + rule.ToString() + ".\r\n";

                //If the rule is a symbol, it is a known true.
                if (rule.IsSymbol())
                {
                    knownTrueSymbols.Add(conclusion);
                    log += conclusion + " is true.\r\n";
                    return true;
                }

                //Add this conclusion to the list of symbols currently being evaluated
                if (!unknownSymbols.Contains(conclusion))
                    unknownSymbols.Add(conclusion);

                //If a premise is still in the process of being evaluated, there is a cycle in the graph--skip the rule
                cycle = false;
                foreach (string term in rule.Premises)
                {
                    if (unknownSymbols.Contains(term))
                    {
                        log += "Rule " + rule.ToString() + " depends on its own conclusion...skipping rule.\r\n";
                        cycle = true;
                        break;
                    }
                }

                if (cycle)
                    continue;

                //If the rule is not known true or false, recursively evaluate its premises
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
                    knownTrueSymbols.Add(conclusion);
                    unknownSymbols.Remove(conclusion);
                    log += conclusion + " is true by rule " + rule.ToString() + ".\r\n";
                    return true;
                }
            }

            //There are no remaining rules to evaluate. The conclusion is false.
            if (rulesWithConclusion.Count == 0)
                log += "No rules conclude " + conclusion + ".\r\n";
            else
                log += "No further rules conclude " + conclusion + ".\r\n";

            unknownSymbols.Remove(conclusion);
            return false;
        }


        //Creates a list of all rules that have the specified conclusion
        private List<Rule> findRulesWithConclusion(string symbol)
        {
            List<Rule> result = new List<Rule>();

            foreach (Rule rule in rules)
                if (rule.Conclusion == symbol)
                    result.Add(rule);

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
