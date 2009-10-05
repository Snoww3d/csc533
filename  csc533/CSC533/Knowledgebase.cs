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
        private List<Rule> rules;
        //public List<Rule> Rules { get { return rules; } }

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
            throw new NotImplementedException();
            
        }

        //Use backward chaining to determine the truth of a symbol
        public bool AskBackward(string conclusion)
        {
            List<Rule> rulesWithConclusion = findRulesWithConclusion(conclusion);

            foreach (Rule rule in rulesWithConclusion)
            {
                if (rule.IsSymbol())
                    return true;
                else
                {
                    bool result = true; //not sure about this

                    foreach (string term in rule.Antecedents)
                        result = result && AskBackward(term);

                    if (result)
                        return true;                                        
                }
            }

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
