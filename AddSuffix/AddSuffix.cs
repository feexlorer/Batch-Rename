using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace AddSuffixRule
{
    public class AddSuffix: iRule
    {
        //public Dictionary<string, Object> Input { get; set; }
        public string _suffix { get; set; }

        public string nameRule => "Adding a suffix to all the files";
        //public string descriptionRule => "Remove all space from the beginning and the ending of the filename.";

        //blic string Type => "File and Folder";

        //public bool isFile => true;
        //public bool isFolder => true;
        //public event PropertyChangedEventHandler PropertyChanged;

        public AddSuffix()
        {
            _suffix = "";

        }
        public AddSuffix(string Prefix)
        {
            _suffix = Prefix;

        }

        public iRule Parse(string line)
        {
            return new AddSuffix(_suffix);
        }

        public string Rename(string originalName)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalName);
            fileName = fileName + " " + _suffix;
            string newname = fileName + Path.GetExtension(originalName);
            return newname;
        }

 

        public void Config()
        {
            var s = new AddSuffixWindow(this);
            if (s.ShowDialog() == true)
            {
                _suffix = s.suffixInput.Text;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
