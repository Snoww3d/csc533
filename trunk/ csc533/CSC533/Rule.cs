/*
 * CSC 533 Artificial Intelligence
 * Project 1 - Forward and Backward Chaining
 * Authors: Shanon Clemmons and Chris Townsend
 * Date: 10/8/09
 * 
 * Rule.cs
 * 
 */ 

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
        public List<string> Premises;       //list of left-hand symbols
        public string Conclusion;           //right-hand symbol

        //Constructor
        public Rule(List<string> premises, string conclusion)
        {
            this.Premises = premises;
            this.Conclusion = conclusion;

            this.Premises.Sort();
        }

        //Returns true if the rule has no premises
        //This means it is a proposition symbol
        public bool IsSymbol()
        {
            return (Premises.Count == 0);
        }

        //Checks if this rule is equivalent to another rule
        //Note: works because premises are sorted
        public bool Equals(Rule rule)
        {
            if (rule.Premises.Count != this.Premises.Count)
                return false;
            
            if (rule.Conclusion != this.Conclusion)
                return false;
                       
            int i = 0;
            foreach (string term in this.Premises)
            {
                if (term != rule.Premises[i])
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
                result = Conclusion;
            }
            else
            {
                foreach (string term in Premises)
                {
                    result += term;
                    if (term != Premises.Last())
                        result += " ^ ";
                }

                result += " => " + Conclusion;
            }
            
            return result;
        }
    }
}
