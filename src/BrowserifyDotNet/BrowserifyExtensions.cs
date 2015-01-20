using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace BrowserifyDotNet
{
    public static class BrowserifyExtensions
    {
        public static void AddBrowserifyBundle(this BundleCollection bundles, Bundle bundle)
        {
            bundle.Transforms.Insert(0, new BrowserifyBundleTransform());
            bundles.Add(bundle);
        }

        public static Bundle IncludeDependency(this Bundle bundle, string virtualPath, string dependencyName)
        {
            return bundle.Include(virtualPath, new BrowserifyDependency(dependencyName));
        }

        public static Bundle IncludeInputFile(this Bundle bundle, string virtualPath)
        {
            return bundle.Include(virtualPath, new BrowserifyInputFile());
        }
    }
}
