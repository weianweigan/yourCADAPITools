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
            url = string.Empty;
            var fullName = _symbolInfo.Symbol.ToString();

            switch (_symbolInfo.Symbol.Kind)
            {
                case SymbolKind.Property:
                    fullName = $"P:{fullName}";
                    break;
                case SymbolKind.Method:
                    fullName = $"M:{fullName}";
                    break;
                case SymbolKind.NamedType:
                    fullName = $"T:{fullName}";
                    break;
                case SymbolKind.Namespace:
                    fullName = $"N:{fullName}";
                    break;
                default:
                    return false;
            }

            url = RevitInfoManager.FindLink(fullName);
            return !string.IsNullOrEmpty(url);
        }
    }
}
