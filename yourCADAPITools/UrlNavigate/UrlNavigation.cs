using EnvDTE80;
using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    public abstract class UrlNavigation
    {
        protected SymbolInfo _symbolInfo;

        public UrlNavigation(SymbolInfo symbolInfo)
        {
            _symbolInfo = symbolInfo;
        }

        public abstract bool TryGetUrl(out string url);

        public static UrlNavigation Create(SymbolInfo symbolInfo)
        {
            if (symbolInfo.Symbol == null)
            {
                throw new NotSupportedNameSpaceException($"Please select a type,property or method");
            }

            string nameSpace = symbolInfo.Symbol.ContainingNamespace.ToString();
            if (nameSpace.StartsWith("SolidWorks.Interop."))
            {
                return new SolidWorksUrlNavigation(nameSpace,symbolInfo);
            }else if(nameSpace.StartsWith("Rhino."))
            {
                return new RhinoCommonUrlNavigation(nameSpace,symbolInfo);
            }else if(nameSpace.StartsWith("Autodesk.Revit."))
            {
                return new RevitUrlNavigation(nameSpace,symbolInfo);
            }
            else
            {
                throw new NotSupportedNameSpaceException($"{nameSpace} Not Supported");
            }
        }

        public static void OpenInVs(DTE2 dte, string url)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            dte.ItemOperations.Navigate(url);
        }
    }
}
