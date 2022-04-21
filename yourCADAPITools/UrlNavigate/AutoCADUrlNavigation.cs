using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    public class AutoCADUrlNavigation : UrlNavigation
    {
        public AutoCADUrlNavigation(SymbolInfo symbolInfo) : base(symbolInfo)
        {
        }

        public override bool TryGetUrl(out string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
