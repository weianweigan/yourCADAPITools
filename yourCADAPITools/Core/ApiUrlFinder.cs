using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;

namespace yourCADAPITools
{
    public class ApiUrlFinder
    {
        private readonly IServiceProvider _serviceProvider;

        public ApiUrlFinder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TextViewSelection FindTextSelection()
        {
            var textMgr = _serviceProvider.GetService(typeof(SVsTextManager)) as IVsTextManager2;
            Assumes.Present(textMgr);
            _ = textMgr.GetActiveView2(1, null, (uint)_VIEWFRAMETYPE.vftCodeWindow, out var textView);

            textView.GetBuffer(out var lines);
            textView.GetSelection(out var startLine, out var startColumn, out var endLine, out var endColumn);
            lines.GetPositionOfLineIndex(startLine, startColumn, out int startPostion);
            lines.GetPositionOfLineIndex(endLine, endColumn, out int endPostion);

            var start = new TextViewPosition(startLine, startColumn, startPostion);
            var end = new TextViewPosition(endLine, endColumn, endPostion);
            textView.GetSelectedText(out var selectedText);

            var selection = new TextViewSelection(start, end, selectedText);

            return selection;
        }

        public void FindSymbolAndNavigate(TextViewSelection selection)
        {
            var dte = _serviceProvider.GetService(typeof(EnvDTE.DTE)) as EnvDTE80.DTE2;
            Assumes.Present(dte);
            var doc = dte.ActiveDocument;
            if (doc == null)
            {
                return;
            }
            var projectName = doc?.ProjectItem?.ContainingProject.Name;
            if (string.IsNullOrEmpty(projectName))
            {
                return;
            }

            var vsDoc = yourCADAPIToolsPackage.VSWorkspace.CurrentSolution.Projects
                .FirstOrDefault(p => p.Name == projectName)
                ?.Documents
                .FirstOrDefault(p =>p.FilePath.Trim() == doc.FullName.Trim());

            if (vsDoc == null)
            {
                return;
            }
            var model = vsDoc.GetSemanticModelAsync().GetAwaiter().GetResult();
            var root = model.SyntaxTree.GetRootAsync().GetAwaiter().GetResult();
            var node = root.FindNode(new Microsoft.CodeAnalysis.Text.TextSpan(
                selection.StartPosition.Postion,
                selection.EndPosition.Postion - selection.StartPosition.Postion));

            if (node == null)
            {
                return;
            }

            var symbolInfo = model.GetSymbolInfo(node);

            var navigation = UrlNavigation.Create(symbolInfo);
            if (navigation == null)
            {
                return;
            }

            if (navigation.TryGetUrl(out var url))
            {
                UrlNavigation.OpenInVs(dte,url);
            }
        }
    }
}
