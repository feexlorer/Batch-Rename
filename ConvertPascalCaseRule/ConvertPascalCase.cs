using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace ConvertPascalCaseRule
{
    public class ConvertPascalCase: iRule 
    {
        //public Dictionary<string, Object> Input { get; set; }

        public string nameRule => "Convert filename to PascalCase";
        //public string descriptionRule => "Remove all space from the beginning and the ending of the filename.";

        //blic string Type => "File and Folder";

        //public bool isFile => true;
        //public bool isFolder => true;
        //public event PropertyChangedEventHandler PropertyChanged;

        public ConvertPascalCase()
        {
            //Input = new Dictionary<string, object>();
        }

        public iRule Parse(string line)
        {
            return new ConvertPascalCase();
        }

        public string Rename(string originalName)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalName); ;
            string extension = Path.GetExtension(originalName);

            string[] listWord = fileName.Split(' ', StringSplitOptions.None);
            var result = " ";
            foreach (string word in listWord)
            {
                if (word.Length > 0)
                {
                    result = result + char.ToUpper(word[0]) + word.Substring(1) + " ";
                }
            }
            result = result.Trim();
            result = result + extension;
            return result;
        }

        public void Config()
        {
            return;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
