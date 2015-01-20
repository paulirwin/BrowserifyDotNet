using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace BrowserifyDotNet
{
    public class BrowserifyDependency : BrowserifyBaseItemTransform
    {
        public BrowserifyDependency(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}
