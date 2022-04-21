using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    public class InventorUrlNavigation : UrlNavigation
    {
        public InventorUrlNavigation(SymbolInfo symbolInfo) : base(symbolInfo)
        {
        }

        public override bool TryGetUrl(out string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
