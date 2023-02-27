using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace AddCounterRule
{
    public class AddCounter: iRule
    {
        //public Dictionary<string, Object> Input { get; set; }
        public int _current { get; set; }
        public int _startValue { get; set; }
        public int _step { get; set; }
        public int _numberOfDigit { get; set; }

        public string nameRule => "Add counter to the end of the file";
        //public string descriptionRule => "Remove all space from the beginning and the ending of the filename.";

        //blic string Type => "File and Folder";

        //public bool isFile => true;
        //public bool isFolder => true;
        //public event PropertyChangedEventHandler PropertyChanged;

        public AddCounter()
        {
            _startValue = 1;
            _current = _startValue;
            _step = 1;
            _numberOfDigit = 1;

        }
        public AddCounter(int start, int step, int numberOfDigit)
        {
            _startValue = start;
            _current = _startValue;
            _step = step;
            _numberOfDigit = numberOfDigit;

        }

        public iRule Parse(string line)
        {
            return new AddCounter(_startValue, _step, _numberOfDigit);
        }

        public string Rename(string originalName)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalName);
            fileName = fileName + " " + _current.ToString($"D{_numberOfDigit}");
            string newname = fileName + Path.GetExtension(originalName);
            _current += _step;
            return newname;
        }


        public void Config()
        {
            var s = new AddCounterWindow(this);
            if (s.ShowDialog() == true)
            {
                _startValue = s.addcoutner._startValue;
                _current = _startValue;
                _step = s.addcoutner._step;
                _numberOfDigit = s.addcoutner._numberOfDigit; 
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

    }
}
