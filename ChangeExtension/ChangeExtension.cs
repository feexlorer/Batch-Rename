using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace ChangeExtensionRule
{
    public class ChangeExtension: iRule
    {
        //public Dictionary<string, Object> Input { get; set; }
        public string _extension { get; set; }

        public string nameRule => "Change the extension to another extension";
        //public string descriptionRule => "Remove all space from the beginning and the ending of the filename.";

        //blic string Type => "File and Folder";

        //public bool isFile => true;
        //public bool isFolder => true;
        //public event PropertyChangedEventHandler PropertyChanged;

        public ChangeExtension()
        {
            _extension = "";

        }
        public ChangeExtension(string extension)
        {
            _extension = extension;

        }

        public iRule Parse(string line)
        {
            return new ChangeExtension(_extension);
        }

        public string Rename(string originalName)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalName);
            string extension = Path.GetExtension(originalName);
            if (extension == "")
            {
                return "";
            }
            string newname = fileName + "." + _extension;

            return newname;
        }

        

        public void Config()
        {
            var s = new ChangeExtensionWindow(this);
            if(s.ShowDialog() ==true)
            {
                _extension = s.extInput.Text;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
