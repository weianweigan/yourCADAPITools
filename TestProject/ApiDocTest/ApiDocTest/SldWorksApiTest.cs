using SolidWorks.Interop.sldworks;

namespace ApiDocTest
{
    public class SldWorksApiTest
    {
        private IModelDoc2 _doc;

        public SldWorks App { get; set; }

        public void Test()
        {
            App.OpenDoc7(null);
        }
    }
        
}