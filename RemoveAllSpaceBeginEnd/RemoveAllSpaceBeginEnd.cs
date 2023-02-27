using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Contact;

namespace RemoveAllSpaceBeginEndRule
{
    public class RemoveAllSpaceBeginEnd: iRule
    {
        //public Dictionary<string, Object> Input { get; set; }

        public string nameRule => "Remove all space at begining/ending";
        //public string descriptionRule => "Remove all space from the beginning and the ending of the filename.";

        //blic string Type => "File and Folder";

        //public bool isFile => true;
        //public bool isFolder => true;
        //public event PropertyChangedEventHandler PropertyChanged;

        public RemoveAllSpaceBeginEnd()
        {
            //Input = new Dictionary<string, object>();
        }

        public iRule Parse(string line)
        {
            return new RemoveAllSpaceBeginEnd();
        }

        public string Rename(string originalName)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalName); ;
            string extension = Path.GetExtension(originalName);

            fileName = fileName.Trim();
            return fileName + extension;
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
