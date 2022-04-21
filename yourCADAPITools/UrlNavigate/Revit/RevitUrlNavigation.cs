using System.Windows;
using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    public class RevitUrlNavigation : UrlNavigation
    {
        public RevitUrlNavigation(SymbolInfo symbolInfo) :
            base(symbolInfo)
        {
        }

        public override bool TryGetUrl(out string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
