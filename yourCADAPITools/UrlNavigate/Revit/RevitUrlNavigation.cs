using System.Windows;
using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    public class RevitUrlNavigation : UrlNavigation
    {
        public RevitUrlNavigation(string nameSpace,SymbolInfo symbolInfo) :
            base(symbolInfo)
        {

        }

        public override bool TryGetUrl(out string url)
        {
            url = RevitInfoManager.FindLink(_symbolInfo.Symbol.Name);
            return !string.IsNullOrEmpty(url);
        }
    }
}
