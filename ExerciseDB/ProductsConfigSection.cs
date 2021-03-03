using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDB
{
    class ProductsConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Products")]
        public ProductsCollection Products
        {
            get { return ((ProductsCollection)(base["Products"])); }
        }
    }
}
