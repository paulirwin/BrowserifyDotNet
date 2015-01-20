using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace BrowserifyDotNet
{
    public class BrowserifyBundleTransform : IBundleTransform
    {
        public void Process(BundleContext context, BundleResponse response)
        {
            var inputFiles = response.Files.Where(i => i.Transforms.Count > 0 && i.Transforms[0] is BrowserifyInputFile).ToList();
            var dependencyFiles = response.Files.Where(i => i.Transforms.Count > 0 && i.Transforms[0] is BrowserifyDependency).ToList();

            var sb = new StringBuilder();

            sb.Append("(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require==\"function\"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error(\"Cannot find module '\"+o+\"'\");throw f.code=\"MODULE_NOT_FOUND\",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require==\"function\"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({");

            int index = inputFiles.Count;
            var inputIndices = new List<int>();
            var dependencies = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var requires = new List<IList<string>>();

            var sources = new List<string>();

            foreach (var file in inputFiles.Concat(dependencyFiles))
            {
                string js = file.ApplyTransforms();

                sources.Add(js);
                requires.Add(FindRequires(js));
            }

            foreach (var dep in dependencyFiles)
            {
                ++index;

                var transform = (BrowserifyDependency)dep.Transforms[0];

                dependencies[transform.Name] = index;
            }

            index = 0;

            foreach (var input in inputFiles.Concat(dependencyFiles))
            {
                ++index;

                if (input.Transforms[0] is BrowserifyInputFile)
                    inputIndices.Add(index);

                if (index > 1)
                    sb.Append(',');

                sb.Append(index).Append(":[");

                var source = sources[index - 1];
                sb.Append(source);

                sb.Append(",{");

                var reqs = requires[index - 1];
                sb.Append(string.Join(",", dependencies.Where(i => reqs.Contains(i.Key)).Select(i => "\"" + i.Key + "\":" + i.Value)));

                sb.Append("}]");
            }

            sb.Append("},{},[");
            sb.Append(string.Join(",", inputIndices));
            sb.Append("]);");

            response.Content = sb.ToString();
        }

        private IList<string> FindRequires(string source)
        {
            try
            {
                var parser = new JSParser(source);
                var block = parser.Parse(new CodeSettings());

                var list = new List<string>();

                FindRequiresInChildren(block, list);

                return list;
            }
            catch
            {
                return new string[0];
            }
        }

        private void FindRequiresInChildren(AstNode node, IList<string> list)
        {
            foreach (var child in node.Children)
            {
                var callNode = child as CallNode;

                if (callNode != null)
                {
                    var lookup = callNode.LeftHandSide as Lookup;

                    if (lookup != null && lookup.Name == "require")
                        list.Add(callNode.Arguments[0].ToString());
                }
                else
                {
                    FindRequiresInChildren(child, list);
                }
            }
        }
    }
}
