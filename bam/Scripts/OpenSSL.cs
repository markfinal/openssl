using Bam.Core;
using System.Linq;
namespace openssl
{
    [ModuleGroup("Thirdparty/OpenSSL")]
    sealed class OpenSSL :
        C.StaticLibrary
    {
        protected override void
        Init(
            Module parent)
        {
            base.Init(parent);

            // ssl_task.c does not compile on Windows - due to iodef.h not being found
            // TODO: may add it back on other platforms
            var source = this.CreateCSourceContainer("$(packagedir)/ssl/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*ssl_task)(?!.*test\.).*)$"));

            source.AddFiles("$(packagedir)/crypto/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*LPdir_*)(?!.*ppc*)(?!.*s390xcap)(?!.*sparc*)(?!.*test\.)(?!.*armcap).*)$"));
            source.AddFiles("$(packagedir)/crypto/aes/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*_x86core).*)$")); // avoids duplicate symbols
            source.AddFiles("$(packagedir)/crypto/asn1/*.c");
            source.AddFiles("$(packagedir)/crypto/bio/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*bss_rtcp).*)$"));
            source.AddFiles("$(packagedir)/crypto/bn/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*exp)(?!.*test\.)(?!.*speed\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/bn/bn_exp.c"); // TODO: not sure how to exclude exp.c from above, but not bn_exp.c - need more RegEx fu
            source.AddFiles("$(packagedir)/crypto/bn/bn_exp2.c"); // TODO: not sure how to exclude exp.c from above, but not bn_exp2.c - need more RegEx fu
            source.AddFiles("$(packagedir)/crypto/buffer/*.c");
            source.AddFiles("$(packagedir)/crypto/cmac/*.c");
            source.AddFiles("$(packagedir)/crypto/cms/*.c");
            source.AddFiles("$(packagedir)/crypto/conf/conf_*.c");
            source.AddFiles("$(packagedir)/crypto/des/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*des\.c)(?!.*des_opts)(?!.*read_pwd)(?!.*speed)(?!.*test\.)(?!.*rpw)(?!.*ncbc_enc).*)$")); // ncbc_enc is #included in source
            source.AddFiles("$(packagedir)/crypto/dh/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*p512)(?!.*p192)(?!.*p1024)(?!.*dhtest).*)$"));
            source.AddFiles("$(packagedir)/crypto/dsa/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*dsagen)(?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/dso/*.c");
            source.AddFiles("$(packagedir)/crypto/ec/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*ecp_nistz256)(?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/ecdh/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/ecdsa/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/engine/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/err/*.c");
            source.AddFiles("$(packagedir)/crypto/evp/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*e_dsa)(?!.*test\.).*)$")); // TODO: e_dsa not compiling on Windows yet
            source.AddFiles("$(packagedir)/crypto/hmac/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/lhash/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/md5/md5_dgst.c");
            source.AddFiles("$(packagedir)/crypto/md5/md5_one.c");
            source.AddFiles("$(packagedir)/crypto/modes/*.c");
            source.AddFiles("$(packagedir)/crypto/objects/*.c");
            source.AddFiles("$(packagedir)/crypto/ocsp/*.c");
            source.AddFiles("$(packagedir)/crypto/pem/*.c");
            source.AddFiles("$(packagedir)/crypto/pkcs7/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*_enc).*)$"));
            source.AddFiles("$(packagedir)/crypto/pkcs12/*.c");
            source.AddFiles("$(packagedir)/crypto/rand/rand_lib.c");
            source.AddFiles("$(packagedir)/crypto/rand/md_rand.c");
            if (this.BuildEnvironment.Platform.Includes(EPlatform.Windows))
            {
                source.AddFiles("$(packagedir)/crypto/rand/rand_win.c");
            }
            source.AddFiles("$(packagedir)/crypto/rsa/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/sha/sha_dgst.c");
            source.AddFiles("$(packagedir)/crypto/sha/sha1dgst.c");
            source.AddFiles("$(packagedir)/crypto/sha/sha1_one.c");
            source.AddFiles("$(packagedir)/crypto/sha/sha256.c");
            source.AddFiles("$(packagedir)/crypto/sha/sha512.c");
            source.AddFiles("$(packagedir)/crypto/stack/*.c");
            source.AddFiles("$(packagedir)/crypto/ui/*.c");
            source.AddFiles("$(packagedir)/crypto/x509/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.).*)$"));
            source.AddFiles("$(packagedir)/crypto/x509v3/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*test\.)(?!.*v3conf)(?!.*v3prin).*)$"));

            source.AddFiles("$(packagedir)/engines/*.c", filter: new System.Text.RegularExpressions.Regex(@"^((?!.*gmp).*)$"));

            source.PrivatePatch(settings =>
                {
                    var compiler = settings as C.ICommonCompilerSettings;
                    compiler.IncludePaths.AddUnique(this.CreateTokenizedString("$(packagedir)"));
                    compiler.IncludePaths.AddUnique(this.CreateTokenizedString("$(packagedir)/crypto"));
                    compiler.IncludePaths.AddUnique(this.CreateTokenizedString("$(packagedir)/crypto/asn1"));
                    compiler.IncludePaths.AddUnique(this.CreateTokenizedString("$(packagedir)/crypto/modes"));
                    compiler.IncludePaths.AddUnique(this.CreateTokenizedString("$(packagedir)/crypto/evp"));

                    if (this.BuildEnvironment.Platform.Includes(EPlatform.Linux))
                    {
                        var cCompiler = settings as C.ICOnlyCompilerSettings;
                        cCompiler.LanguageStandard = C.ELanguageStandard.GNU89; // in order to compile asm statements
                    }

                    var vcCompiler = settings as VisualCCommon.ICommonCompilerSettings;
                    if (null != vcCompiler)
                    {
                        vcCompiler.WarningLevel = VisualCCommon.EWarningLevel.Level2; // will not compile at a higher warning level
                    }
                });

            // note these dependencies are on SOURCE, as the headers are needed for compilation
            var copyStandardHeaders = Graph.Instance.FindReferencedModule<CopyStandardHeaders>();
            var generateConfig = Graph.Instance.FindReferencedModule<GenerateConfHeader>();
            source.DependsOn(copyStandardHeaders, generateConfig);

            this.Requires(copyStandardHeaders); // this is for IDE projects, which require a different level of granularity

            this.PublicPatch((settings, appliedTo) =>
                {
                    var compiler = settings as C.ICommonCompilerSettings;
                    if (null != compiler)
                    {
                        compiler.IncludePaths.AddUnique(copyStandardHeaders.GeneratedPaths[Publisher.Collation.Key]);
                    }
                });

            if (this.BuildEnvironment.Platform.Includes(EPlatform.Windows))
            {
                if (this.Librarian is VisualCCommon.Librarian)
                {
                    this.CompileAgainstPublicly<WindowsSDK.WindowsSDK>(source);
                }

                source.Children.Where(item => item.InputPath.Parse().EndsWith("cversion.c")).ToList().ForEach(item =>
                    {
                        item.PrivatePatch(settings =>
                            {
                                // stops buildinf.h from being included - see GenerateBuildInf for non-Windows platforms
                                var compiler = settings as C.ICommonCompilerSettings;
                                compiler.PreprocessorDefines.Add("NO_WINDOWS_BRAINDEATH");
                            });
                    });
            }
            else
            {
                source.Children.Where(item => item.InputPath.Parse().EndsWith("cversion.c")).ToList().ForEach(item =>
                    {
                        var generateBuildInf = Graph.Instance.FindReferencedModule<GenerateBuildInf>();
                        item.DependsOn(generateBuildInf);
                        item.UsePublicPatches(generateBuildInf);
                    });
            }
        }
    }
}