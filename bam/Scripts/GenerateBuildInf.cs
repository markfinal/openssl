using Bam.Core;
namespace openssl
{
    [ModuleGroup("Thirdparty/OpenSSL")]
    class GenerateBuildInf :
        C.ProceduralHeaderFile
    {
        protected override TokenizedString OutputPath
        {
            get
            {
                return this.CreateTokenizedString("$(packagebuilddir)/$(moduleoutputdir)/buildinf.h");
            }
        }

        protected override string Contents
        {
            get
            {
                return string.Empty;
            }
        }
    }
}
