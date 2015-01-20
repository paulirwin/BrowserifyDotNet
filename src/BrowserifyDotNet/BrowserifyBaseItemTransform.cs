using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace BrowserifyDotNet
{
    public abstract class BrowserifyBaseItemTransform : IItemTransform
    {
        public string Process(string includedVirtualPath, string input)
        {
            return "function(require,module,exports){" + input + "}";
        }
    }
}
