using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace ReplaceCharacterRule
{
    public class ReplaceCharater: iRule
    {
        public List<string> SpecialChars { get; set; }
        public string Replacement { get; set; }

        public string nameRule => "Replace certain characters into one character";

        public ReplaceCharater()
        {
            SpecialChars = new List<string>();
            Replacement = " ";
        }

        public ReplaceCharater(List<string> specialChars, string replacement)
        {
            SpecialChars = specialChars;
            Replacement = replacement;
        }

        public iRule Parse(string line)
        {
            return new ReplaceCharater(SpecialChars, Replacement);
        }

        public string Rename(string originalName)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalName);

            StringBuilder builder = new StringBuilder();
            foreach (var c in fileName)
            {
                if (SpecialChars.Contains($"{c}"))
                {
                    builder.Append(Replacement);
                }
                else
                {
                    builder.Append(c);
                }
            }

            string result = builder.ToString();
            return result + Path.GetExtension(originalName); ;
        }

        public void Config()
        {
            var s = new ReplaceWindow(this);
            if (s.ShowDialog() == true)
            {
                SpecialChars = s.Replace.SpecialChars;
                Replacement = s.Replace.Replacement;    
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
