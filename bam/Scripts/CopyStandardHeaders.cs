using Bam.Core;
namespace openssl
{
    [ModuleGroup("Thirdparty/OpenSSL")]
    sealed class CopyStandardHeaders :
        Publisher.Collation
    {
        protected override void
        Init(
            Module parent)
        {
            base.Init(parent);

            // the build mode depends on whether this path has been set or not
            if (this.GeneratedPaths.ContainsKey(Key))
            {
                this.GeneratedPaths[Key].Aliased(this.CreateTokenizedString("$(packagebuilddir)"));
            }
            else
            {
                this.RegisterGeneratedFile(Key, this.CreateTokenizedString("$(packagebuilddir)"));
            }

            var generateConfig = Graph.Instance.FindReferencedModule<GenerateConfHeader>();
            this.DependsOn(generateConfig);

            var sslHeader = this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/ssl.h"), "openssl");
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/dtls1.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/kssl.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/srtp.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/ssl2.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/ssl23.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/ssl3.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/ssl/tls1.h"), ".", sslHeader);

            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/e_os2.h"), ".", sslHeader);

            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/crypto.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/opensslv.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ossl_typ.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/symhacks.h"), ".", sslHeader);

            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/aes/aes.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/asn1/asn1.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/asn1/asn1t.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/asn1/asn1_mac.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/bf/blowfish.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/bio/bio.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/bn/bn.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/buffer/buffer.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/cast/cast.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/camellia/camellia.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/cmac/cmac.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/cms/cms.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/comp/comp.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/conf/conf.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/conf/conf_api.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/des/des.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/des/des_old.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/dh/dh.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/dsa/dsa.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/dso/dso.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ec/ec.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ecdh/ecdh.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ecdsa/ecdsa.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/engine/engine.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/err/err.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/evp/evp.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/hmac/hmac.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/idea/idea.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/jpake/jpake.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/lhash/lhash.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/krb5/krb5_asn.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/objects/objects.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/objects/obj_mac.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/md2/md2.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/md4/md4.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/mdc2/mdc2.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/md5/md5.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/modes/modes.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/pem/pem.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/pem/pem2.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/pkcs7/pkcs7.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/pkcs12/pkcs12.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/pqueue/pqueue.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ocsp/ocsp.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/rand/rand.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ripemd/ripemd.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/rc2/rc2.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/rc4/rc4.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/rc5/rc5.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/rsa/rsa.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/seed/seed.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/sha/sha.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/srp/srp.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/stack/safestack.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/stack/stack.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ts/ts.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ui/ui.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/ui/ui_compat.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/whrlpool/whrlpool.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/x509/x509.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/x509/x509_vfy.h"), ".", sslHeader);
            this.IncludeFile(this.CreateTokenizedString("$(packagedir)/crypto/x509v3/x509v3.h"), ".", sslHeader);
        }
    }
}
