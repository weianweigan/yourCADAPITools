using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    public class SolidWorksUrlNavigation : UrlNavigation
    {
        public SolidWorksUrlNavigation(SymbolInfo symbolInfo):base(symbolInfo)
        {
            
        }

        public const string UrlBase = "http://help.solidworks.com/{0}/english/api/sldworksapi/";

        public int Version { get; private set; } = 2018;

        public override bool TryGetUrl(out string url)
        {
            url = string.Format(UrlBase,Version);
            switch (_symbolInfo.Symbol.Kind)
            {
                case SymbolKind.Event:
                    url = CombinePropertyAndMethodUrl(url);
                    break;

                case SymbolKind.Field:
                    url = CombineFieldUrl(url);
                    break;

                case SymbolKind.Method:
                    url = CombinePropertyAndMethodUrl(url);
                    break;

                case SymbolKind.NamedType:
                    url =CombineNameTypeUrl(url);
                    break;

                case SymbolKind.Namespace:
                    url = CombineNameTypeUrl(url);
                    break;

                case SymbolKind.Property:
                    url = CombinePropertyAndMethodUrl(url);
                    break;

                default:
                    return false;
            }
            return true;
        }

        private string CombinePropertyAndMethodUrl(string urlBase)
        {
            string nameSpace = _symbolInfo.Symbol.ContainingAssembly.Name;
            string typeName = _symbolInfo.Symbol.ContainingType.Name;
            string definiationName = _symbolInfo.Symbol.OriginalDefinition.Name;

            return $"{urlBase}{nameSpace}^{nameSpace}^{typeName}^{definiationName}.html";
        }

        private string CombineFieldUrl(string urlBase)
        {
            string nameSpace = _symbolInfo.Symbol.ContainingAssembly.Name;
            string typeName = _symbolInfo.Symbol.ContainingType.Name;

            return $"{urlBase}{nameSpace}^{nameSpace}.{typeName}.html";
        }

        private string CombineNameTypeUrl(string urlBase)
        {
            string nameSpace = _symbolInfo.Symbol.ContainingAssembly.Name;
            string className = _symbolInfo.Symbol.Name;

            if (className.EndsWith("_e"))
            {
                //Enum Type
                return $"{urlBase}{nameSpace}~{nameSpace}.{className}.html";
            }
            else if (!className.StartsWith("I") || className.StartsWith("Import") || className.StartsWith("Inter"))
            {
                className = "I"+ className;
            }            
            return $"{urlBase}{nameSpace}~{nameSpace}.{className}_members.html";
        }

        private string CombineNameSpaceUrl(string urlBase)
        {
            string nameSpace = _symbolInfo.Symbol.ContainingAssembly.Name;

            return $"{urlBase}{nameSpace}~{nameSpace}_namespace.html";
        }
    }
}
