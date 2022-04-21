﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string nameSpace = symbolInfo.Symbol.ContainingAssembly.Name;
            if (nameSpace.StartsWith("SolidWorks.Interop."))
            {
                return new SolidWorksUrlNavigation(symbolInfo);
            }
            else
            {
                return null;
            }
        }

        public static void OpenInVs(DTE2 dte, string url)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            dte.ItemOperations.Navigate(url);
        }
    }
}