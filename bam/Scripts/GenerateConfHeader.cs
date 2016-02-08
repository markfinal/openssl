using Bam.Core;
namespace openssl
{
    [ModuleGroup("Thirdparty/OpenSSL")]
    class GenerateConfHeader :
        C.ProceduralHeaderFile
    {
        protected override TokenizedString OutputPath
        {
            get
            {
                return this.CreateTokenizedString("$(packagebuilddir)/PublicHeaders/openssl/opensslconf.h");
            }
        }

        protected override string Contents
        {
            get
            {
                var contents = new System.Text.StringBuilder();
                if (this.BuildEnvironment.Platform.Includes(EPlatform.Windows))
                {
                    contents.AppendLine("#include <winsock2.h>"); // resolves issues with ordering of Windows.h and winsock2.h
                    contents.AppendLine("#include <Windows.h>");
                    contents.AppendLine("#define SIXTY_FOUR_BIT");
                    contents.AppendLine("#define OPENSSL_IMPLEMENTS_strncasecmp");
                    contents.AppendLine("#define OPENSSL_NO_HW"); // no hardware support
                    contents.AppendLine("#define OPENSSL_NO_ASM"); // no assembly
                    contents.AppendLine("#define OPENSSL_NO_DSO"); // no shared libraries
                    contents.AppendLine("#define OPENSSL_NO_CAPIENG"); // note: not covered by OPENSSL_NO_HW in eng_all.c, but is in specific engine code
                    contents.AppendLine("#define OPENSSL_NO_GMP");
                    contents.AppendLine("#define OPENSSL_NO_GOST");
                    contents.AppendLine("#define OPENSSL_NO_EC_NISTP_64_GCC_128");
                }
                else if (this.BuildEnvironment.Platform.Includes(EPlatform.Linux))
                {
                    contents.AppendLine("#define SIXTY_FOUR_BIT_LONG"); // TODO: I think
                    contents.AppendLine("#define OPENSSL_UNISTD \"unistd.h\"");
                }
                contents.AppendLine("#define OPENSSL_NO_KRB5");
                contents.AppendLine("#define OPENSSL_NO_SCTP");
                contents.AppendLine("#define DES_LONG unsigned long");
                contents.AppendLine("#define IDEA_INT unsigned int");
                contents.AppendLine("#define MD2_INT unsigned int");
                contents.AppendLine("#define RC2_INT unsigned int");
                contents.AppendLine("#define RC4_INT unsigned int");
                contents.AppendLine("#define ENGINESDIR \"/usr/local/lib/engines\""); // TODO: this is wrong, but until I can fix it, this will do
                contents.AppendLine("#define OPENSSLDIR \"/usr/local/ssl\""); // TODO: this is wrong, but until I can fix it, this will do
                return contents.ToString();
            }
        }
    }
}
