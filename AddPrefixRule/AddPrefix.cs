using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace AddPrefixRule
{
    public class AddPrefix: iRule
    {
        //public Dictionary<string, Object> Input { get; set; }
        public string prefix { get; set; }

        public string nameRule => "Adding a prefix to all the files";
        //public string descriptionRule => "Remove all space from the beginning and the ending of the filename.";

        //blic string Type => "File and Folder";

        //public bool isFile => true;
        //public bool isFolder => true;
        //public event PropertyChangedEventHandler PropertyChanged;

        public AddPrefix()
        {
            prefix = "";
            
        }
        public AddPrefix(string Prefix)
        {
            prefix = Prefix;
          
        }

        public iRule Parse(string line)
        {
            return new AddPrefix(prefix);
        }

        public string Rename(string originalName)
        {
            var newname = $"{prefix} {originalName}";
            return newname;
        }


        public void Config()
        {
            var s = new AddPrefixWindow(this);
            if(s.ShowDialog() == true)
            {
                prefix = s.prefixInput.Text;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }


    }
}
