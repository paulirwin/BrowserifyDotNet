# BrowserifyDotNet
PROTOTYPE! Browserify-like behavior for adding require() functionality to ASP.NET bundles.

## Usage

Add a script tag referencing the bundle path to your HTML:

`<script type="text/javascript" src="/bundles/browserified"></script>`

Then add to your BundleConfig.cs file:

```
bundles.AddBrowserifyBundle(
  new ScriptBundle("~/bundles/browserified")
  .IncludeDependency("~/scripts/uniq.js", "uniq")
  .IncludeDependency("~/scripts/jquery-{version}.js", "jquery") // supports the {version} tag too!
  .IncludeInputFile("~/scripts/main.js")
);
```

Current limitation is that you must call IncludeDependency manually for any dependencies your input files or dependencies require. Call IncludeInputFile for any of your main app files.

Example main.js:

```
var unique = require('uniq');
var jQuery = require('jquery');

var data = [1, 2, 2, 3, 4, 5, 5, 5, 6];

jQuery(function () {
    alert("jQuery is loaded by browserify!");
});

console.log(unique(data));
```

## WARNING

This code is not production-ready! This is currently a prototype only. Pull requests and issues accepted.
