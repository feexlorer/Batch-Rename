using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace LowerCaseAndRemoveAllSpacesRule
{
    public class LowerCaseAndRemoveAllSpace: iRule
    {
        //public Dictionary<string, Object> Input { get; set; }

        public string nameRule => "Convert all characters to lowercase, remove all spaces";
        //public string descriptionRule => "Remove all space from the beginning and the ending of the filename.";

        //blic string Type => "File and Folder";

        //public bool isFile => true;
        //public bool isFolder => true;
        //public event PropertyChangedEventHandler PropertyChanged;

        public LowerCaseAndRemoveAllSpace()
        {
            //Input = new Dictionary<string, object>();
        }

        public iRule Parse(string line)
        {
            return new LowerCaseAndRemoveAllSpace();
        }

        public string Rename(string originalName)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalName);
            string extension = Path.GetExtension(originalName);
            fileName = fileName.Replace(" ", "");

            string newname = fileName.ToLower() + extension;
            return newname;
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
