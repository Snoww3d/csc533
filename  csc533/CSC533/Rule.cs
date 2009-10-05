using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSC533
{
    //Class to represent rules in Horn form:
    //Either a proposition symbol or a (conjunction of symbols) => symbol
    //Proposition symbols are represented by a rule with an empty left side
    public class Rule
    {
        public List<string> Antecedents;    //list of left-hand symbols
        public string Consequent;           //right-hand symbol

        //Constructor
        public Rule(List<string> antecedents, string consequent)
        {
            this.Antecedents = antecedents;
            this.Consequent = consequent;

            antecedents.Sort();
        }

        //Returns true if the rule has no antecedents
        //This means it is a proposition symbol
        public bool IsSymbol()
        {
            return (Antecedents.Count == 0);
        }

        //Checks if this rule is equivalent to another rule
        //Note: works because antecedents are sorted
        public bool Equals(Rule rule)
        {
            if (rule.Consequent != this.Consequent)
                return false;

            if (rule.Antecedents.Count != this.Antecedents.Count)
                return false;
            
            int i = 0;
            foreach (string term in Antecedents)
            {
                if (term != rule.Antecedents[i])
                    return false;

                i++;
            }

            return true;

        }

        //Override ToString() method to output the rule in a fairly pretty format
        public override string ToString()
        {
            string result = "";
            if (IsSymbol())
            {
                result = Consequent;
            }
            else
            {
                foreach (string term in Antecedents)
                {
                    result += term;
                    if (term != Antecedents.Last())
                        result += " ˄ ";
                }

                result += " => " + Consequent;
            }
            
            return result;
        }
    }
}
