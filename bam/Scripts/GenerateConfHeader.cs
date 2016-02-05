using Bam.Core;
namespace openssl
{
    [ModuleGroup("Thirdparty/OpenSSL")]
    sealed class GenerateConfHeader :
        C.CModule
    {
        private static Bam.Core.PathKey Key = Bam.Core.PathKey.Generate("OpenSSL conf header");

        protected override void
        Init(
            Module parent)
        {
            base.Init(parent);
            this.GeneratedPaths.Add(Key, this.CreateTokenizedString("$(packagebuilddir)/openssl/opensslconf.h"));
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
                if (this.BuildEnvironment.Platform.Includes(EPlatform.Windows))
                {
                    writeFile.WriteLine("#include <winsock2.h>"); // resolves issues with ordering of Windows.h and winsock2.h
                    writeFile.WriteLine("#include <Windows.h>");
                    writeFile.WriteLine("#define SIXTY_FOUR_BIT");
                    writeFile.WriteLine("#define NO_WINDOWS_BRAINDEATH"); // stops buildinf.h from being included, which appears to be procedurally generated
                    writeFile.WriteLine("#define OPENSSL_IMPLEMENTS_strncasecmp");
                    writeFile.WriteLine("#define RC4_INT unsigned int");
                    writeFile.WriteLine("#define ENGINESDIR \"/usr/local/lib/engines\""); // TODO: this is wrong, but until I can fix it, this will do
                    writeFile.WriteLine("#define OPENSSLDIR \"/usr/local/ssl\""); // TODO: this is wrong, but until I can fix it, this will do
                    writeFile.WriteLine("#define DES_LONG unsigned long");
                    writeFile.WriteLine("#define IDEA_INT unsigned int");
                    writeFile.WriteLine("#define RC2_INT unsigned int");
                    writeFile.WriteLine("#define MD2_INT unsigned int");
                    writeFile.WriteLine("#define OPENSSL_NO_HW"); // no hardware support
                    writeFile.WriteLine("#define OPENSSL_NO_ASM"); // no assembly
                    writeFile.WriteLine("#define OPENSSL_NO_DSO"); // no shared libraries
                    writeFile.WriteLine("#define OPENSSL_NO_KRB5");
                    writeFile.WriteLine("#define OPENSSL_NO_SCTP");
                    writeFile.WriteLine("#define OPENSSL_NO_CAPIENG"); // note: not covered by OPENSSL_NO_HW in eng_all.c, but is in specific engine code
                    writeFile.WriteLine("#define OPENSSL_NO_GMP");
                    writeFile.WriteLine("#define OPENSSL_NO_GOST");
                    writeFile.WriteLine("#define OPENSSL_NO_EC_NISTP_64_GCC_128");
                }
                else if (this.BuildEnvironment.Platform.Includes(EPlatform.Linux))
                {
                    writeFile.WriteLine("#define SIXTY_FOUR_BIT_LONG"); // TODO: I think
                }
            }
            Log.Info("Writing OpenSSL configuration header : {0}", destPath);
        }

        protected override void
        GetExecutionPolicy(
            string mode)
        {
            // TODO: do nothing
        }
    }
}
