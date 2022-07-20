using Microsoft.CodeAnalysis;
using System.Linq;

namespace yourCADAPITools
{
    /// <summary>
    /// SolidEdge <see href="https://docs.sw.siemens.com/zh-CN/product/246738425/doc/PL20200701135947994.api"/>
    /// </summary>
    public class SolidEdgeUrlNavigation : UrlNavigation
    {
        public const string MethodOrProperty = "https://docs.sw.siemens.com/documentation/external/PL20200701135947994/en-US/api/{0}~{1}~{2}.html";

        public const string TypeOverview = "https://docs.sw.siemens.com/documentation/external/PL20200701135947994/en-US/api/{0}~{1}.html";
        
        public const string MembersOverview = "https://docs.sw.siemens.com/documentation/external/PL20200701135947994/en-US/api/{0}~{1}_members.html";

        public string NameSpace { get; }

        public SolidEdgeUrlNavigation(string nameSpace,SymbolInfo symbolInfo) : base(symbolInfo)
        {
            NameSpace = nameSpace;
        }

        public override bool TryGetUrl(out string url)
        {
            url = "https://docs.sw.siemens.com/documentation/external/PL20200701135947994/en-US/api/Welcome.html";
            switch (_symbolInfo.Symbol.Kind)
            {
                case SymbolKind.Field:
                    url = GetTypeUrl();
                    break;

                case SymbolKind.Method:
                    url = GetPropertyOrMethodUrl();
                    break;

                case SymbolKind.NamedType:
                    url = GetMembers();
                    break;

                case SymbolKind.Namespace:
                    url = GetMembers();
                    break;

                case SymbolKind.Property:
                    url = GetPropertyOrMethodUrl();
                    break;

                default:
                    return false;
            }

            return true;
        }

        public string GetPropertyOrMethodUrl()
        {
            string typeName = _symbolInfo.Symbol.ContainingType.Name;
            string definiationName = _symbolInfo.Symbol.OriginalDefinition.Name;

            return string.Format(MethodOrProperty, NameSpace, typeName, definiationName);
        }

        public string GetTypeUrl()
        {
            string typeName = _symbolInfo.Symbol.ToString().Split('.').Last();

            return string.Format(TypeOverview, NameSpace, typeName);
        }

        public string GetMembers()
        {
            string typeName = _symbolInfo.Symbol.ToString().Split('.').Last();

            return string.Format(MembersOverview, NameSpace, typeName);
        }
    }
}
