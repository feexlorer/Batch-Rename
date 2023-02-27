using System;
using System.Collections.Generic;

namespace Contact
{
    public interface iRule: ICloneable
    {
        // Get input like prefix, suffix, ...
        //Dictionary<string, Object> Input { get; set; }

        // Rule's name
        string nameRule { get; }

        //// Rule's description
        //string descriptionRule { get; }

        //// Folder or file
        //string Type { get; }

        //bool isFile { get; }
        //bool isFolder { get; }
        // Perform renaming
        string Rename(string original);

        // Show Window
        void Config();
        iRule? Parse(string data);

    }
}
