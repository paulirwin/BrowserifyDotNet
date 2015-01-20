using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrowserifyDotNet;
using System.Web.Optimization;

namespace Example
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.AddBrowserifyBundle(
                new ScriptBundle("~/bundles/browserified")
                .IncludeDependency("~/scripts/uniq.js", "uniq")
                .IncludeDependency("~/scripts/jquery-{version}.js", "jquery") // supports the {version} tag too!
                .IncludeInputFile("~/scripts/main.js")
            );
        }
    }
}
