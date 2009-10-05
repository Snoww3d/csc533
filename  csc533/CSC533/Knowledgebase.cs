using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CSC533
{
    //Knowledgebase class to store a conjunction of rules
    //Can add rules to or remove them from the knowledgebase
    //Can query symbols with forward or backward chaining
    public class Knowledgebase
    {
        private List<Rule> rules;

        //Constructor
        public Knowledgebase()
        {
            rules = new List<Rule>();
        }

        //Add a rule to the knowledgebase
        public void Tell(Rule rule)
        {
            if (!containsRule(rule))
                rules.Add(rule);
            else
                MessageBox.Show("That rule already exists.", "Oops!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        //Remove a rule from the knowledgebase
        public void Remove(Rule rule)
        {
            for (int i = 0; i < rules.Count; i++)
            {
                if (rules[i].Equals(rule))
                {
                    rules.RemoveAt(i);
                }
            }
        }
        
        //Use forward chaining to determine the truth of a symbol
        public bool AskForward(string symbol)
        {
            throw new NotImplementedException();
            
        }

        //Use backward chaining to determine the truth of a symbol
        public bool AskBackward(string symbol)
        {
            throw new NotImplementedException();
            
        }

        //Override toString() method to output all rules as a string
        public string ToString()
        {
            string result = "";
            foreach (Rule rule in rules)
            {
                result += rule.ToString();
                result += "\n\r";
            }

            return result;
        }

        //Helper method. Returns true if the knowledgebase contains the rule specified
        //in the parameter.
        private bool containsRule(Rule rule)
        {
            foreach (Rule comparedRule in rules)
            {
                if (comparedRule.Equals(rule))
                    return true;
            }

            return false;
        }
    }
}
