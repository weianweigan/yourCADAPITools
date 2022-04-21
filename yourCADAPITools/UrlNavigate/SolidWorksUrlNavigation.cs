using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    public class SolidWorksUrlNavigation : UrlNavigation
    {
        public SolidWorksUrlNavigation(
            string nameSpace,
            SymbolInfo symbolInfo,
            int version = 2022):base(symbolInfo)
        {
            NameSpace = nameSpace;
            Version = version;

            var data = nameSpace.Split('.');
            if(data[data.Length - 1] == "swconst")
            {
                UrlBase = string.Format(UrlBase, version, "swconst");
            }else
            {                
                UrlBase = string.Format(UrlBase, version, data[data.Length-1]+"api");
            }
        }

        public string UrlBase { get; private set; } = "http://help.solidworks.com/{0}/english/api/{1}/";

        public string NameSpace { get; }

        public int Version { get; private set;}

        public override bool TryGetUrl(out string url)
        {
            url = UrlBase;
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
            string typeName = _symbolInfo.Symbol.ContainingType.Name;
            string definiationName = _symbolInfo.Symbol.OriginalDefinition.Name;

            return $"{urlBase}{NameSpace}~{NameSpace}~{typeName}~{definiationName}.html";
        }

        private string CombineFieldUrl(string urlBase)
        {
            string typeName = _symbolInfo.Symbol.ContainingType.Name;

            return $"{urlBase}{NameSpace}^{NameSpace}.{typeName}.html";
        }

        private string CombineNameTypeUrl(string urlBase)
        {
            string className = _symbolInfo.Symbol.Name;

            if (className.EndsWith("_e"))
            {
                //Enum Type
                return $"{urlBase}{NameSpace}~{NameSpace}.{className}.html";
            }
            else if (!className.StartsWith("I") || className.StartsWith("Import") || className.StartsWith("Inter"))
            {
                className = "I"+ className;
            }            
            return $"{urlBase}{NameSpace}~{NameSpace}.{className}_members.html";
        }

        private string CombineNameSpaceUrl(string urlBase)
        {
            return $"{urlBase}{NameSpace}~{NameSpace}_namespace.html";
        }
    }
}
