﻿using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    /// <summary>
    /// <see href="https://developer.rhino3d.com/api/RhinoCommon/html/R_Project_RhinoCommon.htm"/>
    /// </summary>
    public class RhinoCommonUrlNavigation : UrlNavigation
    {
        public RhinoCommonUrlNavigation(string nameSpace,SymbolInfo symbolInfo) : base(symbolInfo)
        {
            _nameSpace = nameSpace;
        }

        public string UrlBase = "https://developer.rhino3d.com/api/RhinoCommon/html/{0}_{1}.htm";

        private readonly string _nameSpace;

        public override bool TryGetUrl(out string url)
        {
            url = UrlBase;
            string fullName;
            switch (_symbolInfo.Symbol.Kind)
            {
                case SymbolKind.Property:
                    fullName = CombineNames();
                    url = string.Format(url, "P", fullName.Replace('.', '_'));
                    break;
                case SymbolKind.Method:
                    fullName = CombineNames();
                    url = string.Format(url, "M", fullName.Replace('.', '_'));
                    break;
                case SymbolKind.NamedType:
                    fullName = CombineNames();
                    url = string.Format(url, "T", fullName.Replace('.', '_'));
                    break;
                case SymbolKind.Namespace:
                    url = string.Format(url, "T", _nameSpace.Replace('.', '_'));
                    break;
                default:
                    return false;
            }
            return true;
        }

        public string CombineNames()
        {
            string typeName = _symbolInfo.Symbol.ToString().Split('(')[0];

            return typeName;
        }
    }
}
