using Microsoft.CodeAnalysis;

namespace yourCADAPITools
{
    /// <summary>
    /// <see href="https://developer.rhino3d.com/api/RhinoCommon/html/R_Project_RhinoCommon.htm"/>
    /// </summary>
    public class RhinoCommonUrlNavigation : UrlNavigation
    {
        public RhinoCommonUrlNavigation(SymbolInfo symbolInfo) : base(symbolInfo)
        {
        }

        public override bool TryGetUrl(out string url)
        {
            throw new System.NotImplementedException();
        }
    }
}
