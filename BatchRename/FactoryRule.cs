using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact;

namespace BatchRename
{
    public class FactoryRule
    {
        static Dictionary<string, iRule> _prototypes = new Dictionary<string, iRule>();
        public static void Register(iRule prototype)
        {
            if (_prototypes.ContainsKey(prototype.nameRule))
            {
                // do nothing
            }
            else
            {
                _prototypes.Add(prototype.nameRule, prototype);
            }
        }
        private static FactoryRule? _instance = null;
        public static FactoryRule Instance()
        {
            if (_instance == null)
            {
                _instance = new FactoryRule();
            }

            return _instance;
        }
        public static List<string> ShowName()
        {
            List<string> names = new List<string>();
            
            names = _prototypes.Keys.ToList();

            return names;
        }
        public iRule? Parse(string data)
        {
           
            iRule? result = null;

            if (_prototypes.ContainsKey(data))
            {
                iRule prototype = _prototypes[data];
                prototype.Config();
                result = prototype.Parse(data);
            }
            return result;
        }
    }
}
