using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCStore.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static bool IsDebugMode(this HtmlHelper htmlHelper)
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }

        /// <summary>
        /// Looks in <paramref name="directory"/> for JavaScript files and includes them in into the page html. If both minified and non-minified versions of
        /// a script exist, this method prefers minified scripts (ending in .min.js) in Release configuration and non-minified scripts in Debug configuration.
        /// All scripts in the specified directory are included except for those that start with "exclude." in their file names.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="directory"></param>
        /// <param name="scriptsToIncludeFirst">List of script names without file extensions to include before other scripts</param>
        /// <returns></returns>
        public static MvcHtmlString Scripts(this HtmlHelper helper, string directory, string[] scriptsToIncludeFirst)
        {
            var tagBuilders = new List<TagBuilder>();

            var server = helper.ViewContext.Controller.ControllerContext.RequestContext.HttpContext.Server;
            var scriptFiles = Directory.GetFiles(server.MapPath(directory), "*.js")
                .Where(s => !s.Contains("\\exclude."))
                .Where(s => !s.Contains(".intellisense.js"))
                .ToArray();

            // Prefer minified scripts in Release build
            var preferMinified = !helper.IsDebugMode();

            var priority1Files = scriptFiles.Where(s => preferMinified ? s.EndsWith(".min.js") : !s.EndsWith(".min.js")).ToList();
            var priority2Files = scriptFiles.Except(priority1Files);

            var scriptsToInclude = priority1Files.ToList();
            foreach (var file in priority2Files)
            {
                var sisterFile = (preferMinified) ? file.Replace(".js", ".min.js") : file.Replace(".min.js", ".js");
                if (!scriptsToInclude.Contains(sisterFile))
                {
                    scriptsToInclude.Add(file);
                }
            }

            foreach (var scriptName in scriptsToIncludeFirst.Reverse())
            {
                var index = scriptsToInclude.FindIndex(s => s.Contains("\\" + scriptName + ".js") ||
                                                            s.Contains("\\" + scriptName + ".min.js"));
                if (index < 0)
                {
                    throw new ArgumentException(String.Format("Could not find script file {0}.js or {0}.min.js", scriptName), "scriptsToIncludeFirst");
                }
                var script = scriptsToInclude[index];
                scriptsToInclude.RemoveAt(index);
                scriptsToInclude.Insert(0, script);
            }

            foreach (var file in scriptsToInclude)
            {
                var fileInfo = new FileInfo(file);
                var tb = new TagBuilder("script");
                tb.Attributes["src"] = directory + (directory.EndsWith("/") ? "" : "/") + fileInfo.Name;
                tb.Attributes["type"] = "text/javascript";
                tagBuilders.Add(tb);
            }

            return new MvcHtmlString(String.Join("\n", tagBuilders.Select(tb => tb.ToString(TagRenderMode.Normal))));
        }

    }
}