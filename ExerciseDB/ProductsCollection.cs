using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseDB
{
    [ConfigurationCollection(typeof(Product))]
    public class ProductsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Product();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Product)(element)).ProductType;
        }

        public Product this[int idx]
        {
            get { return (Product)BaseGet(idx); }
        }
    }
}
