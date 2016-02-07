using Bam.Core;
namespace openssl
{
    [ModuleGroup("Thirdparty/OpenSSL")]
    sealed class GenerateBuildInf :
        C.CModule
    {
        private static Bam.Core.PathKey Key = Bam.Core.PathKey.Generate("OpenSSL buildinf header");

        protected override void
        Init(
            Module parent)
        {
            base.Init(parent);
            this.GeneratedPaths.Add(Key, this.CreateTokenizedString("$(packagebuilddir)/$(moduleoutputdir)/buildinf.h"));

            this.PublicPatch((settings, appliedTo) =>
                {
                    var compiler = settings as C.ICommonCompilerSettings;
                    if (null != compiler)
                    {
                        compiler.IncludePaths.AddUnique(this.CreateTokenizedString("$(packagebuilddir)/$(moduleoutputdir)"));
                    }
                });
        }

        public override void
        Evaluate()
        {
            this.ReasonToExecute = null;
            var outputPath = this.GeneratedPaths[Key].Parse();
            if (!System.IO.File.Exists(outputPath))
            {
                this.ReasonToExecute = Bam.Core.ExecuteReasoning.FileDoesNotExist(this.GeneratedPaths[Key]);
                return;
            }
        }

        protected override void
        ExecuteInternal(
            ExecutionContext context)
        {
            var destPath = this.GeneratedPaths[Key].Parse();
            var destDir = System.IO.Path.GetDirectoryName(destPath);
            if (!System.IO.Directory.Exists(destDir))
            {
                System.IO.Directory.CreateDirectory(destDir);
            }
            using (System.IO.TextWriter writeFile = new System.IO.StreamWriter(destPath))
            {
            }
            Log.Info("Writing OpenSSL buildinf header : {0}", destPath);
        }

        protected override void
        GetExecutionPolicy(
            string mode)
        {
            // TODO: do nothing
        }
    }
}
