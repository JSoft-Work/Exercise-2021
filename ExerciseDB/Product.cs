using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDB
{
    public class Product : ConfigurationElement
    {

        [ConfigurationProperty("productType", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string ProductType
        {
            get { return ((string)(base["productType"])); }
            set { base["productType"] = value; }
        }

        [ConfigurationProperty("hind", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Hind
        {
            get { return ((string)(base["hind"])); }
            set { base["hind"] = value; }
        }

        [ConfigurationProperty("kogus", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string Kogus
        {
            get { return ((string)(base["kogus"])); }
            set { base["kogus"] = value; }
        }

    }
}
