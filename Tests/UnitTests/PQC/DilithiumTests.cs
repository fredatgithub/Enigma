﻿using Enigma.IO;
using Enigma.PQC;
using NUnit.Framework;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTests.PQC
{
    internal class DilithiumTests
    {
        [Test]
        public void FirstTest()
        {
            Dilithium.GenerateKeyPair(out DilithiumPublicKeyParameters publicKey,
                out DilithiumPrivateKeyParameters privateKey,
                DilithiumParameters.Dilithium5Aes);

            byte[] pubKeyData = publicKey.GetEncoded();
            byte[] pKeyData = privateKey.GetEncoded();
            byte[] message = Encoding.UTF8.GetBytes("test");
            byte[] signature = Dilithium.Sign(message, privateKey);
            bool valid = Dilithium.Verify(message, signature, publicKey);

            //public: Rho, T1
            //private: rho, k, tr, s1, s2, t0

            int lenRho = 32;
            int lenK = 32;
            int lenTr = 32;
            int lenS1 = 672;
            int lenS2 = 768;
            int lenT0 = 3328;
            int lenT1 = 2560;

            byte[] rhoPub = new byte[lenRho];
            byte[] t1Pub = new byte[lenT1];
            Array.Copy(pubKeyData, 0, rhoPub, 0, lenRho);
            Array.Copy(pubKeyData, lenRho, t1Pub, 0, lenT1);

            string rhoPubStr = Hex.Encode(rhoPub); //bd77defa0f363aa8d7dffc57e7fe5dfb7d429adbba8abc4c81de2a691aa64a78
            string t1PubStr = Hex.Encode(t1Pub); //c4a137eda3ab4bc082b5c457bcb7098558c2bab584dc4983501f0ab56f442f282dd69821c5131f8e947468ade27924fa5a42bbb1cef825dd6e3ff45be21fdc0880e76443ed669d20747081f3e5fc205f1d3ad80ece7014fb5929d76a07835992c1710207cc26343978403c0f4bd09182f4d54eec520722aeeb673d2e77175d3b5323ee14722327a4c3fd4c8248cf2bc3a4979eaca2d6f2375b5b2c2b0291977917c97aa1d3607bf69c7cfb6717a0522d152188d6c766f517f728ba70e8ea9f00ae3a46e7eb676a11ed7201d092d8ba00190f5eef5cf15ba591769b3a4ee55ea7853f374c771ac39584639f6d39c193097aea39cf788c027fcf2eb1f1e4241179ab98d0f7ed7e404474c05626a1612250cde0880dbf6274ab2e0fa60afdd6ae2f43173883bea372179fdbd9b47f579b4fcb0bf8eea5f55019b39c58020512fa663fb655ee66af4b1d26fed5c4706aad7b883e7835c8bbcbf027a570670046093b9aa8e00d166f782bd2f02e7535cd682750698ca839b52d164b1cb5c2e7ec684127e69c9c8e57951b62996d941513be05acd77f7acbdb04e94c53f76cb0d823f1f30d78ac205c84c3743f8e0b24f0a87c89f35689614ed4edd03c89b440f96d1ce422726750e23612228e6fab4d478020ce29d54b8d8ab22264a69e14d02bfac27bd80537749957d03c848c2e16f78112be096ebcaa0258ec94bd77bda5bceedad57e95c9a3f5a1e56566c96f08ccdc34ee01673819c65ba1e83ac2ae0075650ef920131f4b4d26788599df2930c0f1a56ec7441ce97fd773ee08ec997d9097f0d2f4acd2425c3b69a48ca71ffb82a0fa5a38ceda30936ec6a761c2833d674368726c71d6098d73ceb4fb259ea435a9b644ef8bcc4d434552bb69aaf64c596457bdc47f09349ac28644ef44eae72e0bebf2db646e2a01493546d3056359e022b65af4893b023277023310bf425ac95eb8f16b349f4e5c58481770229de8a6878ed406f642bbc3c1790b97dba7644633985de026eaf04088fdf8ec32d6f18ec303697578be27750b37bec0abce67f989bf62a93656db8f283c1d2b14b3fdf32122187e7d64f673923c0391719fb96b7ce16f6aadf0d29703aec4bc5297a2bb2ccc66822f8f99370437e93298dcd70160dc781ade16681f1b8e7ba15f24b215e9827df9f267ac3a366d661d0bd3a270173d1cd0b556bcf63e81ceba6355a56805794e6efcffe4a140368b7ec3d7c2eb8658c9e608ca2bffc1c756527acde54b77a1a5e75b7f07349aa8f9292dd76b6ec307ddedd0f3cd770a449099c6f757ae08402ef415cc566bd29c293603fe8e9a0962d4e2ea14bc002606f41d37a42dfcbee738f6aed376fff0be2ce83060a8c376c9c81b0ba2f438ce551f6e69907ce81deb1e169d26a9f6146b9cddac39f7dec5a8eb7311f8d65c56febb5a1b839b21b0bd14178142d9c6d4a9c3d06507fbdaee56df4910402fc07c178b7afd75b8f5365bd53806a8661ca2225df71acfa4b0912cb55b8610f72c089c47357837b06af33b6b52434ff30a8afdb728a42236c7d0ba06202d1b97c1b4d97cdc7f33070710483d59cb19348a1beb1b5f4c16f782d7613bb51d1ffe2ace7fade22bacf77cb161c60376e682caf379fa3d401b9baf266c26c3f9d34f66066403359c874e8a481ae671edd6c84a8cf0890651fdccf3b35e37d5679532284a0ddd57f72d29717f4b810d7d6431e0958969447ed3ae3931de73b8c7fa8971484b28265501f37feb7486b2734bbfb2ecae511d86d8d60871464e06856229bf68a7d88e75f94269fe1376532519f94a3b0bf0c4dda22872b00272759f6df01172e18af2ed4623222604f08ab1bb0cac8ca47cee28c23c0b87c1d9f87082f046f0c29fcecbe885cc654578bf1c292c0a06af983363a8da50d803d279a2a28a6055052b255cf5cecf8cb9be0a1d814d136b34bc31f884cb1602102ed686667ce6dc060b69305379bed457500202e37ae13915c01259c8df28d4451f5872b5238703f2c695641f6dfe604240fb7245f336f9f3ae37e994548923b37ce266717bc2a4893031bc8da2b9d8733567779c7728afcc24eb7a5f111f10c61b429556cdef82cf546364c31ba5cb05980a724b1283bec07eded73733ee72f255a267b5bfaff12519b1cb487782a0dba46a0ced24e27cd9c1f0afc800a35c2014c104ed4185b5e7264ea0b05a907822b9a09e749be18e9899a42ed1eef3ccc4abe4e15635a6898bc9dd8812a367c1c4d25847e9c97cc971e7c3f37b6b80ec9e4afd5e22f243d01b085731bffe4f2afc81df920ebd976c80954862c92ad4df962bc5ab20bc45338c8ff85d8f07858729d094a37b92f499e072abe03881d2d94a8baef8226645df28bb038f248f9d3cebc042557035d9751caf0dbf206d5db80d84877f08b61143baaf22697a9cd9d7c40ab0727212a44e4f8518fff0d90c8799d58c9211060a019edea71ecb482b5331bb571e2285c798ee264f5dc2c6770b82b79cab5005d1a70c2465fdc9f89b4d300d7657b981eae90a0690a79a3a516dedce2ba474bbf520f06b80008acbd7eec3794b41fff1a60ef26164395b5d0494bb5149f6c08647140b39d11264af1f48b2422609dc2180b1457a6c6d7e734d5e4ea81c11173b280e0e6b6fae4f3d82ebe410aecb30ca8b653d83b8ddbbd4fff6a8491cb59a759a7f9acb84394c6bff2c090dcaaa9a7508241f89b25faf7b8f81768afe94025b1ed68717a05b214b370a66c1b1497ba50b8b65706704a4d18dfef46d7e1a7ed344de6453c2a19363624baaae974bff405cb87b1e2f3f9bf3371aff3c68cc7d078b92a94def3cd60979541ec6cb74ebc51fd0ddb6af317c79d3e672fbac39c51ed51dba9cc74a284410a418bcebd31e9d91b40ccca19866d31cd3810835aa7ae98b1c86dbcc8bd2570bc8023fd5f24fb79e87e93477eb38d8a5405a4d5790a52cc596f4470d12f51bb62a71e843795a2af78eda7fc2fae336074278914ac2d4ef536fa380ee5efb9ac633ff88dd512a7c71ca8510ea0499551454be1f5df794e2703d422449322051ec25d5facea92b77999664522c540518d2198f1bfabaa61b53077a3649ee9722d313aff49a9825fdf06405d75d60f9b4531b839e1f8654ad786e1ee769968e3faad0d7749266a3c9c10c114562e1e225b57faa290b64d5ad6e2b1187eb136ade10e535d6d3e899a9e6ee631ad21cdc46456c6048d4bcefaf47816e6422c50954ff74a29d420f2ba7028335f79e93240e6d540b84168a157e17e894e562999f34ec71729f05c83267550b7d21b0d43f7d4cd8fb26889d82e9a7217960825c532c17a1e1e6f339ca509356a4191091254d2d368c64276f43c9afb834a6bbc077769ac7ad14a2059b32c2b363ce303e8cdd7e17567d9778e81b0205e889d005937529087ce8e44b08bf7222d014750428d279e1fda3a673a3a6d8121f8caff1864c1875976361c20a30943298896c3e6f31a71de937b51b8cff21490c195dd7a168671867c441e801b0e8888724405a66076c2a8adc9053fce89737e89f2d39f262532fafd04f09c4cace7c0183aba8a4c6943d3ca9456547431ca7970fff7a663a05ef49

            //---

            byte[] rho = new byte[lenRho];
            byte[] k = new byte[lenK];
            byte[] tr = new byte[lenTr];
            byte[] s1 = new byte[lenS1];
            byte[] s2 = new byte[lenS2];
            byte[] t0 = new byte[lenT0];

            int index = 0;
            Array.Copy(pKeyData, index, rho, 0, lenRho);
            index += lenRho;
            Array.Copy(pKeyData, index, k, 0, lenK);
            index += lenK;
            Array.Copy(pKeyData, index, tr, 0, lenTr);
            index += lenTr;
            Array.Copy(pKeyData, index, s1, 0, lenS1);
            index += lenS1;
            Array.Copy(pKeyData, index, s2, 0, lenS2);
            index += lenS2;
            Array.Copy(pKeyData, index, t0, 0, lenT0);

            string rhoStr = Hex.Encode(rho); //bd77defa0f363aa8d7dffc57e7fe5dfb7d429adbba8abc4c81de2a691aa64a78
            string kStr = Hex.Encode(k); //dd31a5b64067f6b65a2e2d9f073321bb5ad5f867d6306bcd5e6ade108d565340
            string trStr = Hex.Encode(tr); //935b4cc80dabeab7d81a1fde387cef9f34e04152fefcd187cdc9376ba23b6a5a
            string s1Str = Hex.Encode(s1); //8b88010830066198088cc6648b18651a124d14836509190a0928910313014b42294b480284164c03b2611a23718b16419348858a266481340918a801d3408180102ca1368a08c431c910698bb411a4b66d0c260e4c92101a402814282599a4409236220aa36c1032012337700b070e4b825109978043462150a609d0b4008aa2419cb84804172a4908424c48299b8840193148d082414b34065c1872193930c9904d51c82d98a2818b2411d8280242b469c8a00c24971093b2045a842001448a4028291b176ce104528b027282a8051b128cd1280811404dcc8450091841d832485b020003062408462ac9088921c670494231db228a804272611021034944c1068251980821132c43024040404592a48c022686cc98200319888a4485513671a4247011450680029022b68450264acc36862192491432051130861132240ab208848440ca066a23a2690c052e09248913168e9b32460a8405c4466942086158462654080e903888c398454ba2115a4065e0a2692040289b360948a62c60b209cc262d141964e2469284368e5a342c948810d4324942262161268d9ca0615b161022848513480ce39208d1a62413a38552c07000158e8b2069149901041210244189622091a1922882308a09334e49c66c04b0899120610ca809c116115a460d8a284d00262a20262e994461048824dab44d83c05002b92111070444a07010978400a5442435101a86618318618c240c88922c1b412991c8245a806800992d581621d1200a93346e240770a4026893028c90a8519888614134469b4229214611528864cb926c9130414c162ca336466180210c072c64b83009900d6438891cb708e38425604665e330061222490c204914216d84061181160d029341e0b6080891109a206cc848318394650aa50d8008494a282212b68d5b384a4cb24984a665
            string s2Str = Hex.Encode(s2); //c9c4058b1488203122cbb46958a648a2c40dcbc641a248685b940420a97088b24511a72164188948c284d4c208909025d11286d19870c3a6210ba70c1ab3052080851c894dd91404d1b48da4023112214513359111372654806463882c50140254388c0c94301c0380e4a621c8486c49188918364d21b88c849690542429d22031d2c82dcb34511b36864b983002028993200201174c01440e02c104c1422c0b054d51a64c5132722199708c386c1036489b8680d41892c3263252182552a06d43461109380600490683a66d8cc2719ca08421050a04316690b44083882c14854441c8445998715a464654142292968c191826e4b029883621cc886913082840b064c9368903a2658b14601980111297019bb48dd2324e2290314bc605dc226a90488a20346062444e92022cc4b2818338610aa69054b40543a2855b288d02954404418a6126255a006240a6898a286608254ca2162209c7090a496c54a68d03164091162a8412899a928d98000ad3a401d4424a109371a1204e53b43192166dcc262041446ca4408e234509e3224e0b050218b7654b0624848605e2926ca2a461c0086e04b808649289503680d1a41010852d81088563947014408401218c61446002216a24127250b08c5c1670e4104aa0146e0a8170208761c94086cc9291e432901415628ab8410a164512192d94886999122688883194a8818392484418305b0001a3b604241061e118044a1641d92612cc060a00909098002da4a29198a23140944cd424450b326e1283714c447148c6654914520a804d22128d039151e03224d2888563103119c63061468088484d1b224d91200100a285a23668514066a0c890544004503848c186688942021c9670d1066081908c11068a51b08d9b3429103866c1260100995010364cc324209940845210241ba8200906519b089109c6709b320c53361221c41120284908b23181202a442829991801cb426213944411c48cdaa86c48262d09194e22246d49207252c485914892a0046ac0a26d214410903431da826d9188280a046e20196ac4146918c5301804415b224a210368
            string t0Str = Hex.Encode(t0); //f0b2b72d1c3d98c0f75eae5199b05a8522a60d8fd2ea1dfc3f001a28c7f105d30682ca55c72a2fbabe83a7c660df8734ee6dfafcc12b0750fda2e99a4f8f9bf1f420917b67336da7d3f844745d4a9a86776f03815626f0e6ef03c54d32d2a39ff379900e90192adf1b40a7e9b23bde75e64530dc5a6cb07c298e545eda590618452e6f244f43f1e1bbea1567e8f4f5af8e6a9db595a771d46ddf4136816d3d29eef0ba7283327fbd969849af532867fa643ed1e16ee3f68cc1098d817d5990f06f1aac60242cbfc0c89e79dbeb82d7a7729058bc85c6dfcb8c5b206b17f9bc124aba52edde139e06fd74be234be431653deae8ae9c62c356884e34714c83887cc464a47a7d994f73b303d95bc02b08ff223102d8f89d896b61a1707f2aee45eb9784d81a37d0b7055c1004e95ba3b80166609d6ed31bc4f84073f2fae9dad0fc4a60bdeb2ace51468ffcb6b7ade66b24375c68a47124a5d8f7ec229cc4feadb79421787fe4f7734dee9352904307e0bdba1320877635113c6666c489c6843342c6da6c5682ad3bc8dd7a5c1fe09a8dd9c1ca6f357ad1e52dbb018e70bb8237828cf7010d36fcf239edc6b09f66ac0cf66d7e73e8c494dd8ee3aaaa790ca4185ded466ae6d158342676e76d04f6a8587bebcb2d6e3a1826d070598ca7f7850577780f32f15a0d2aa1d579d9aed124de3cfb1ff06ea67c81a5754cdbcc793eb68450e9e7ba932245e0e9e8f5f1ffc5adfcf982f54957db9a69b230f96336ab8fef726c799bbd62e4b857d0b4efc7333910af1c0c62602a4a4902e5e29511c2006f0a6100b8f9146ae0729319d7cfeb517eace66c4d8ed3a38c395887cbd2163330ba7d61e1bcc3ee8a4cacdb3e2537ff0209f6fd3b078e9e217bc8e158afbc214c56c7cd6dc5aeecc938ecbee1c689e35dcd2a6736a6148dd96412cbb40e87d3c5d2e39ffae6cffb3386af5fb25da6c406e56ac1a4766651f3f8f3c01602c57dac47914f3aa23d22431a82c965b59753cc99546229daf07050a03def19fbbf6c672aaba19b9d3c68ee9aeea424b396a60286e16d6094befe85f094c26416fd9cc0920fe40e288e6c311fec1d116c3391c4c34abeef2f315e5d7aad607dde148bcbdd291df32c1cab0cc06bb2bef7549bbf22e103126a5511c969323dbe49af74e18794fdeb7a2c5562c7baa60a69ee973d50a7633185014904c3f65061b4ee1a4c7db120d4c980c8496f0cad0c110d9477a0beb0dfe106e36c79f571ec4e87c411f99b8c2200630c221d09346ffb2447dd722abcfe45186d3683f52c74d55dd1617c1f9525c8b41027ce4487d99d1d6f34a2b07df2cb1eb131c795900f74d33bbbf2dada0c84880ef98bf7d624005e16a6fa00d8ea3df3f9462211c2fa8f9bc4c1ae0014c41f5c10be3d03bf8a7690c02dc400f1713d1d986b300b3dcb277b6532bb7f93615a46f7687b8f56779643a654435f7169ea4de79fb97bae1db76e6de93f808663e56316f2587984f5d4051d27322e36f4417f127f7095157f7453f47736f1f2a6f3a771dad230d41f7e0bfe1bbb5f92b50b092d9a494f40799db778235ad8abb6f69fe9bed30c83d79697c1aa4afd93b6df23f033a775e2df9721331e8f5955844f95740bcad3b4e7154a6ebc0783d29a1dc3ee5f26379ccb1c2c107c0b62b91d73af67f9a22fe2f1a821be2813112d15b63d79f06a32ce90c6ef100042d8a1634754ed9796703aa90c8de270dc15548dd9f2ecb4b6ca8fc7aca6f0a7cea76e735bf82cc0f5fdfb390783e2f8b6e115957c8ad95184fbeeb0b498889d86f531093135afd1a8766393d5ea8a5686d5efd688e2fe8d37389a99cf65ce530b64fc93337bbcaf50aaa9132b13d4320b3cc551ee2fdd68b0fb5c7a6ff8c3be080d7eab621def422d8a4c2e7a4c3857e267911c9005d971835e4a9810d9d27ff2272fe2d21a01e2df3c1faead5b7ecf18474a66710848d3f24c80bf4e880092a460272a3af10f23054d949d51c32571c462b98c582082c2dce2d6591e66b9ee3606d8c8d13e08caee27209026195bec550018daa6e153023ba915324c0b88f6d6387766b845880305a3f27287a2a07416370792875bc7d80439a354265de60c4ce08c4b3544cf28957fa7af6943eee2b1763cc6b257c5f9bad8465f023c9f0971ee56a7a18ea3f900888500c675df61877934ab48f0e91c112737062472858cc9671e4668fad0d369af4f7d78b353147d40f7d9335ce08c4bd15c77e5900f03cfff72e425b730ca2a158a8aca5467ce0012d9376661e0f9bb7b2034222d4e9f7346fce99aec431b32c70acef4a5e2e8b658e5181776761983f24e85c3af1279959d0754f45d73e1f0c3c907f11b2717790b7b4e4873aaed9c903f7602d27b9ee9b54d2e1477da2dee5f8223992fc42c5959998294713d55145ebaea22a13b4b191b457ea2b9208a16a177b1dcc4fba5a9970aa68ccd8ea62fd6b777eec78ecc0140c2409caffa8546335de09fa0f1c42d74dea5bb960f7487c8d1af1620efd19530dc542fc70d03c66e324c818ce0fbe14700b363ad447ec62dc549da61dfcaf6662df418a396ec846554b81429b447c24a7b00ce0798536963ea726edc29c189706026c7ae5f66d8982ee89a4d6ecb37f0d4a4085f1125b7eb7576b4bc7a8a1da56603cbbc8fa3f045d618a8940e32c10e5363ae5f1b693d6bf49a3599125220ae144aeb2402186074c80f2d586d049f501989a4db80408aaa4b69dbe80fcec9b2338371f4aab173ffc610679b8b7adbf2870a78d46cdcdc07cb49e3910fa6dc94d3b318c60d609ef1e720ebed804ec56f8eae582d2cec9c05318796e1ea2e898610ecf73becf23267f882ea1843f68dae1fa894140d5524be986cc05c2160e5494a0fa2361c3e33657298e0d8af6be0bd74523f8be7d80f209fc4020a9b5527f941705551f261c8ae8464e137dff8a3132b89d7dd25429b01872c364ff380e661d7b3d8824c7931a9d8ebe6834c07ffc769f504ff77f19659510ad47929d046e91f8a50fed4d17c32b3ae8020bd68ba44ae25a7381dffe353cbbb9e65d725506e4bbdd888ff3f061467d9da43967751e4711da2f29fd86ba0a022d638071f88850f3ae1fc56f2862714e269831dc23112f81b13edb75ce2e7b4f39d32d6d60a703a5b18db184c496508bb8f229fc78a42e76ad1e60134c7ea4529cb7b5f4b10b3ff28d39ab55a27946b753c7ef1d79b64db8d58638cdea5b6d255c018a39ba37e50d619fe3449ea76fd65981fb9865e3c7172c0acb4b1547b998ff36299d343d30f6a955a9c0857f3ec1d22dbb2e4329fe516bdae3065c591c7623d1c0689923a8a7e872b6fe23ae347e77777f52e1880352bdc2186d993535ee7ec101aa1aaacbb0f48b6e804bbaa3362972a0e3d530d01ea9ffcf006fe760dbf971a820771ca7fd633ce51a1c7484b4532bc1d6494e07d67316698bab5be9045c25d05fa1b52552bcb14ba7670db010038dd266821b7da7893cf914a9dd019cf7499398ce1647f2f3ea420449cc0aa7a88a9787c0a48cb01848f5a113e8fb65523e93190f1e6b04b05a5e1b3e07b28e6cab22f862f2382ef74601079962bdc7e2083bb1d888f1a1dc4397ed4cc718cfca339b75537e945ca52a8371ea61bed7f21d867465ed2751bba31c1bf92ba17c07a660e832ebe26a4f377ed97645178cd819f81342bc4ae42b79693254af68f94a8e330762ad65e580db68eb933c213f0410306419cbaf215a90dd0b62d3361b3654b2a9f8201155f64db0e16102e54fb80bd707b5a6953bfb2feb1f82a963467bed80cd5a91a6db59a2de8a05e583c66ed0f646dde72ddacc7abc816f400732333d3484b8a2805d0d8752cb59be933b90218e037a938a4df096a361e5345295a95dce442ab4d39f32beae442c46135a12d0605bf5e2ef9c2cb21c7c3b3d4b0d4aa31e6315e432b980437505666faf631da3128e5005c3fe4eb6ccfa6b3d8960773ca933f7405b4e800c748a154adab3df8d369e83d8b31ae322a30f40e0beec2a76eb5606b0a7462963a86cea935d870c78930330c764b110cbfe4f66e26c9cbd4fe04cf62770fb983df5da77fb07e398fa7f2668f8b78a2e37c5aae52dbcba84c13a5e09ffdf6eb798d86940c11940c181ae406bb0f6d94bcd1e1f64d7ad3c3f599efbe3e32cb2316aa579e113502da351f5d9773e85bfe5158160f7a9b5a51ad0648f653a3228e9ac00133e8902c7c17fe92966a4e4e1a51e1e735ade8f61723757336745a2e439526440c4bbb18e4ee947b88bcf860aba6fbdaf26c8cc057948338d65e9891e0bc314ca0d417255e91e7927efb14c6fe6ad62b60f487fe53841837ce0bedad8436bb2fbf5ee70ecd08048e64de4d59e5f4f60380e2075d2bf4bc9ab9062bc9a6f49f22294323b409843158c3231dc2c7caa271a6577bdf8c47ddd5505fe96b0d875160e4a5162186ac73f54d77b9cac0b35705721b5a9881335d33695e635f88f0f1f784e31acf45a82cc21c5769d4a41de2d150a69b5938aa57e82245e25e9a8895cd96cdb3c757b65552ca49adfa09e0be202de7cb1846d416e305ba9e8cce0eea5aeaabbdb93b161f79be7ecff73f5ee34366dab166941abd2d96fb9527c204dd196da621080f95f8393439391d9d937e2c512712c40d045b5ddeb36535ef935400a78cbf1db92168c0b5

            DilithiumPrivateKeyParameters newPrivate = new DilithiumPrivateKeyParameters(DilithiumParameters.Dilithium5Aes,
                Hex.Decode("bd77defa0f363aa8d7dffc57e7fe5dfb7d429adbba8abc4c81de2a691aa64a78"),
                Hex.Decode("dd31a5b64067f6b65a2e2d9f073321bb5ad5f867d6306bcd5e6ade108d565340"),
                Hex.Decode("935b4cc80dabeab7d81a1fde387cef9f34e04152fefcd187cdc9376ba23b6a5a"),
                Hex.Decode("8b88010830066198088cc6648b18651a124d14836509190a0928910313014b42294b480284164c03b2611a23718b16419348858a266481340918a801d3408180102ca1368a08c431c910698bb411a4b66d0c260e4c92101a402814282599a4409236220aa36c1032012337700b070e4b825109978043462150a609d0b4008aa2419cb84804172a4908424c48299b8840193148d082414b34065c1872193930c9904d51c82d98a2818b2411d8280242b469c8a00c24971093b2045a842001448a4028291b176ce104528b027282a8051b128cd1280811404dcc8450091841d832485b020003062408462ac9088921c670494231db228a804272611021034944c1068251980821132c43024040404592a48c022686cc98200319888a4485513671a4247011450680029022b68450264acc36862192491432051130861132240ab208848440ca066a23a2690c052e09248913168e9b32460a8405c4466942086158462654080e903888c398454ba2115a4065e0a2692040289b360948a62c60b209cc262d141964e2469284368e5a342c948810d4324942262161268d9ca0615b161022848513480ce39208d1a62413a38552c07000158e8b2069149901041210244189622091a1922882308a09334e49c66c04b0899120610ca809c116115a460d8a284d00262a20262e994461048824dab44d83c05002b92111070444a07010978400a5442435101a86618318618c240c88922c1b412991c8245a806800992d581621d1200a93346e240770a4026893028c90a8519888614134469b4229214611528864cb926c9130414c162ca336466180210c072c64b83009900d6438891cb708e38425604665e330061222490c204914216d84061181160d029341e0b6080891109a206cc848318394650aa50d8008494a282212b68d5b384a4cb24984a665"),
                Hex.Decode("c9c4058b1488203122cbb46958a648a2c40dcbc641a248685b940420a97088b24511a72164188948c284d4c208909025d11286d19870c3a6210ba70c1ab3052080851c894dd91404d1b48da4023112214513359111372654806463882c50140254388c0c94301c0380e4a621c8486c49188918364d21b88c849690542429d22031d2c82dcb34511b36864b983002028993200201174c01440e02c104c1422c0b054d51a64c5132722199708c386c1036489b8680d41892c3263252182552a06d43461109380600490683a66d8cc2719ca08421050a04316690b44083882c14854441c8445998715a464654142292968c191826e4b029883621cc886913082840b064c9368903a2658b14601980111297019bb48dd2324e2290314bc605dc226a90488a20346062444e92022cc4b2818338610aa69054b40543a2855b288d02954404418a6126255a006240a6898a286608254ca2162209c7090a496c54a68d03164091162a8412899a928d98000ad3a401d4424a109371a1204e53b43192166dcc262041446ca4408e234509e3224e0b050218b7654b0624848605e2926ca2a461c0086e04b808649289503680d1a41010852d81088563947014408401218c61446002216a24127250b08c5c1670e4104aa0146e0a8170208761c94086cc9291e432901415628ab8410a164512192d94886999122688883194a8818392484418305b0001a3b604241061e118044a1641d92612cc060a00909098002da4a29198a23140944cd424450b326e1283714c447148c6654914520a804d22128d039151e03224d2888563103119c63061468088484d1b224d91200100a285a23668514066a0c890544004503848c186688942021c9670d1066081908c11068a51b08d9b3429103866c1260100995010364cc324209940845210241ba8200906519b089109c6709b320c53361221c41120284908b23181202a442829991801cb426213944411c48cdaa86c48262d09194e22246d49207252c485914892a0046ac0a26d214410903431da826d9188280a046e20196ac4146918c5301804415b224a210368"),
                Hex.Decode("f0b2b72d1c3d98c0f75eae5199b05a8522a60d8fd2ea1dfc3f001a28c7f105d30682ca55c72a2fbabe83a7c660df8734ee6dfafcc12b0750fda2e99a4f8f9bf1f420917b67336da7d3f844745d4a9a86776f03815626f0e6ef03c54d32d2a39ff379900e90192adf1b40a7e9b23bde75e64530dc5a6cb07c298e545eda590618452e6f244f43f1e1bbea1567e8f4f5af8e6a9db595a771d46ddf4136816d3d29eef0ba7283327fbd969849af532867fa643ed1e16ee3f68cc1098d817d5990f06f1aac60242cbfc0c89e79dbeb82d7a7729058bc85c6dfcb8c5b206b17f9bc124aba52edde139e06fd74be234be431653deae8ae9c62c356884e34714c83887cc464a47a7d994f73b303d95bc02b08ff223102d8f89d896b61a1707f2aee45eb9784d81a37d0b7055c1004e95ba3b80166609d6ed31bc4f84073f2fae9dad0fc4a60bdeb2ace51468ffcb6b7ade66b24375c68a47124a5d8f7ec229cc4feadb79421787fe4f7734dee9352904307e0bdba1320877635113c6666c489c6843342c6da6c5682ad3bc8dd7a5c1fe09a8dd9c1ca6f357ad1e52dbb018e70bb8237828cf7010d36fcf239edc6b09f66ac0cf66d7e73e8c494dd8ee3aaaa790ca4185ded466ae6d158342676e76d04f6a8587bebcb2d6e3a1826d070598ca7f7850577780f32f15a0d2aa1d579d9aed124de3cfb1ff06ea67c81a5754cdbcc793eb68450e9e7ba932245e0e9e8f5f1ffc5adfcf982f54957db9a69b230f96336ab8fef726c799bbd62e4b857d0b4efc7333910af1c0c62602a4a4902e5e29511c2006f0a6100b8f9146ae0729319d7cfeb517eace66c4d8ed3a38c395887cbd2163330ba7d61e1bcc3ee8a4cacdb3e2537ff0209f6fd3b078e9e217bc8e158afbc214c56c7cd6dc5aeecc938ecbee1c689e35dcd2a6736a6148dd96412cbb40e87d3c5d2e39ffae6cffb3386af5fb25da6c406e56ac1a4766651f3f8f3c01602c57dac47914f3aa23d22431a82c965b59753cc99546229daf07050a03def19fbbf6c672aaba19b9d3c68ee9aeea424b396a60286e16d6094befe85f094c26416fd9cc0920fe40e288e6c311fec1d116c3391c4c34abeef2f315e5d7aad607dde148bcbdd291df32c1cab0cc06bb2bef7549bbf22e103126a5511c969323dbe49af74e18794fdeb7a2c5562c7baa60a69ee973d50a7633185014904c3f65061b4ee1a4c7db120d4c980c8496f0cad0c110d9477a0beb0dfe106e36c79f571ec4e87c411f99b8c2200630c221d09346ffb2447dd722abcfe45186d3683f52c74d55dd1617c1f9525c8b41027ce4487d99d1d6f34a2b07df2cb1eb131c795900f74d33bbbf2dada0c84880ef98bf7d624005e16a6fa00d8ea3df3f9462211c2fa8f9bc4c1ae0014c41f5c10be3d03bf8a7690c02dc400f1713d1d986b300b3dcb277b6532bb7f93615a46f7687b8f56779643a654435f7169ea4de79fb97bae1db76e6de93f808663e56316f2587984f5d4051d27322e36f4417f127f7095157f7453f47736f1f2a6f3a771dad230d41f7e0bfe1bbb5f92b50b092d9a494f40799db778235ad8abb6f69fe9bed30c83d79697c1aa4afd93b6df23f033a775e2df9721331e8f5955844f95740bcad3b4e7154a6ebc0783d29a1dc3ee5f26379ccb1c2c107c0b62b91d73af67f9a22fe2f1a821be2813112d15b63d79f06a32ce90c6ef100042d8a1634754ed9796703aa90c8de270dc15548dd9f2ecb4b6ca8fc7aca6f0a7cea76e735bf82cc0f5fdfb390783e2f8b6e115957c8ad95184fbeeb0b498889d86f531093135afd1a8766393d5ea8a5686d5efd688e2fe8d37389a99cf65ce530b64fc93337bbcaf50aaa9132b13d4320b3cc551ee2fdd68b0fb5c7a6ff8c3be080d7eab621def422d8a4c2e7a4c3857e267911c9005d971835e4a9810d9d27ff2272fe2d21a01e2df3c1faead5b7ecf18474a66710848d3f24c80bf4e880092a460272a3af10f23054d949d51c32571c462b98c582082c2dce2d6591e66b9ee3606d8c8d13e08caee27209026195bec550018daa6e153023ba915324c0b88f6d6387766b845880305a3f27287a2a07416370792875bc7d80439a354265de60c4ce08c4b3544cf28957fa7af6943eee2b1763cc6b257c5f9bad8465f023c9f0971ee56a7a18ea3f900888500c675df61877934ab48f0e91c112737062472858cc9671e4668fad0d369af4f7d78b353147d40f7d9335ce08c4bd15c77e5900f03cfff72e425b730ca2a158a8aca5467ce0012d9376661e0f9bb7b2034222d4e9f7346fce99aec431b32c70acef4a5e2e8b658e5181776761983f24e85c3af1279959d0754f45d73e1f0c3c907f11b2717790b7b4e4873aaed9c903f7602d27b9ee9b54d2e1477da2dee5f8223992fc42c5959998294713d55145ebaea22a13b4b191b457ea2b9208a16a177b1dcc4fba5a9970aa68ccd8ea62fd6b777eec78ecc0140c2409caffa8546335de09fa0f1c42d74dea5bb960f7487c8d1af1620efd19530dc542fc70d03c66e324c818ce0fbe14700b363ad447ec62dc549da61dfcaf6662df418a396ec846554b81429b447c24a7b00ce0798536963ea726edc29c189706026c7ae5f66d8982ee89a4d6ecb37f0d4a4085f1125b7eb7576b4bc7a8a1da56603cbbc8fa3f045d618a8940e32c10e5363ae5f1b693d6bf49a3599125220ae144aeb2402186074c80f2d586d049f501989a4db80408aaa4b69dbe80fcec9b2338371f4aab173ffc610679b8b7adbf2870a78d46cdcdc07cb49e3910fa6dc94d3b318c60d609ef1e720ebed804ec56f8eae582d2cec9c05318796e1ea2e898610ecf73becf23267f882ea1843f68dae1fa894140d5524be986cc05c2160e5494a0fa2361c3e33657298e0d8af6be0bd74523f8be7d80f209fc4020a9b5527f941705551f261c8ae8464e137dff8a3132b89d7dd25429b01872c364ff380e661d7b3d8824c7931a9d8ebe6834c07ffc769f504ff77f19659510ad47929d046e91f8a50fed4d17c32b3ae8020bd68ba44ae25a7381dffe353cbbb9e65d725506e4bbdd888ff3f061467d9da43967751e4711da2f29fd86ba0a022d638071f88850f3ae1fc56f2862714e269831dc23112f81b13edb75ce2e7b4f39d32d6d60a703a5b18db184c496508bb8f229fc78a42e76ad1e60134c7ea4529cb7b5f4b10b3ff28d39ab55a27946b753c7ef1d79b64db8d58638cdea5b6d255c018a39ba37e50d619fe3449ea76fd65981fb9865e3c7172c0acb4b1547b998ff36299d343d30f6a955a9c0857f3ec1d22dbb2e4329fe516bdae3065c591c7623d1c0689923a8a7e872b6fe23ae347e77777f52e1880352bdc2186d993535ee7ec101aa1aaacbb0f48b6e804bbaa3362972a0e3d530d01ea9ffcf006fe760dbf971a820771ca7fd633ce51a1c7484b4532bc1d6494e07d67316698bab5be9045c25d05fa1b52552bcb14ba7670db010038dd266821b7da7893cf914a9dd019cf7499398ce1647f2f3ea420449cc0aa7a88a9787c0a48cb01848f5a113e8fb65523e93190f1e6b04b05a5e1b3e07b28e6cab22f862f2382ef74601079962bdc7e2083bb1d888f1a1dc4397ed4cc718cfca339b75537e945ca52a8371ea61bed7f21d867465ed2751bba31c1bf92ba17c07a660e832ebe26a4f377ed97645178cd819f81342bc4ae42b79693254af68f94a8e330762ad65e580db68eb933c213f0410306419cbaf215a90dd0b62d3361b3654b2a9f8201155f64db0e16102e54fb80bd707b5a6953bfb2feb1f82a963467bed80cd5a91a6db59a2de8a05e583c66ed0f646dde72ddacc7abc816f400732333d3484b8a2805d0d8752cb59be933b90218e037a938a4df096a361e5345295a95dce442ab4d39f32beae442c46135a12d0605bf5e2ef9c2cb21c7c3b3d4b0d4aa31e6315e432b980437505666faf631da3128e5005c3fe4eb6ccfa6b3d8960773ca933f7405b4e800c748a154adab3df8d369e83d8b31ae322a30f40e0beec2a76eb5606b0a7462963a86cea935d870c78930330c764b110cbfe4f66e26c9cbd4fe04cf62770fb983df5da77fb07e398fa7f2668f8b78a2e37c5aae52dbcba84c13a5e09ffdf6eb798d86940c11940c181ae406bb0f6d94bcd1e1f64d7ad3c3f599efbe3e32cb2316aa579e113502da351f5d9773e85bfe5158160f7a9b5a51ad0648f653a3228e9ac00133e8902c7c17fe92966a4e4e1a51e1e735ade8f61723757336745a2e439526440c4bbb18e4ee947b88bcf860aba6fbdaf26c8cc057948338d65e9891e0bc314ca0d417255e91e7927efb14c6fe6ad62b60f487fe53841837ce0bedad8436bb2fbf5ee70ecd08048e64de4d59e5f4f60380e2075d2bf4bc9ab9062bc9a6f49f22294323b409843158c3231dc2c7caa271a6577bdf8c47ddd5505fe96b0d875160e4a5162186ac73f54d77b9cac0b35705721b5a9881335d33695e635f88f0f1f784e31acf45a82cc21c5769d4a41de2d150a69b5938aa57e82245e25e9a8895cd96cdb3c757b65552ca49adfa09e0be202de7cb1846d416e305ba9e8cce0eea5aeaabbdb93b161f79be7ecff73f5ee34366dab166941abd2d96fb9527c204dd196da621080f95f8393439391d9d937e2c512712c40d045b5ddeb36535ef935400a78cbf1db92168c0b5"),
                //Hex.Decode("c4a137eda3ab4bc082b5c457bcb7098558c2bab584dc4983501f0ab56f442f282dd69821c5131f8e947468ade27924fa5a42bbb1cef825dd6e3ff45be21fdc0880e76443ed669d20747081f3e5fc205f1d3ad80ece7014fb5929d76a07835992c1710207cc26343978403c0f4bd09182f4d54eec520722aeeb673d2e77175d3b5323ee14722327a4c3fd4c8248cf2bc3a4979eaca2d6f2375b5b2c2b0291977917c97aa1d3607bf69c7cfb6717a0522d152188d6c766f517f728ba70e8ea9f00ae3a46e7eb676a11ed7201d092d8ba00190f5eef5cf15ba591769b3a4ee55ea7853f374c771ac39584639f6d39c193097aea39cf788c027fcf2eb1f1e4241179ab98d0f7ed7e404474c05626a1612250cde0880dbf6274ab2e0fa60afdd6ae2f43173883bea372179fdbd9b47f579b4fcb0bf8eea5f55019b39c58020512fa663fb655ee66af4b1d26fed5c4706aad7b883e7835c8bbcbf027a570670046093b9aa8e00d166f782bd2f02e7535cd682750698ca839b52d164b1cb5c2e7ec684127e69c9c8e57951b62996d941513be05acd77f7acbdb04e94c53f76cb0d823f1f30d78ac205c84c3743f8e0b24f0a87c89f35689614ed4edd03c89b440f96d1ce422726750e23612228e6fab4d478020ce29d54b8d8ab22264a69e14d02bfac27bd80537749957d03c848c2e16f78112be096ebcaa0258ec94bd77bda5bceedad57e95c9a3f5a1e56566c96f08ccdc34ee01673819c65ba1e83ac2ae0075650ef920131f4b4d26788599df2930c0f1a56ec7441ce97fd773ee08ec997d9097f0d2f4acd2425c3b69a48ca71ffb82a0fa5a38ceda30936ec6a761c2833d674368726c71d6098d73ceb4fb259ea435a9b644ef8bcc4d434552bb69aaf64c596457bdc47f09349ac28644ef44eae72e0bebf2db646e2a01493546d3056359e022b65af4893b023277023310bf425ac95eb8f16b349f4e5c58481770229de8a6878ed406f642bbc3c1790b97dba7644633985de026eaf04088fdf8ec32d6f18ec303697578be27750b37bec0abce67f989bf62a93656db8f283c1d2b14b3fdf32122187e7d64f673923c0391719fb96b7ce16f6aadf0d29703aec4bc5297a2bb2ccc66822f8f99370437e93298dcd70160dc781ade16681f1b8e7ba15f24b215e9827df9f267ac3a366d661d0bd3a270173d1cd0b556bcf63e81ceba6355a56805794e6efcffe4a140368b7ec3d7c2eb8658c9e608ca2bffc1c756527acde54b77a1a5e75b7f07349aa8f9292dd76b6ec307ddedd0f3cd770a449099c6f757ae08402ef415cc566bd29c293603fe8e9a0962d4e2ea14bc002606f41d37a42dfcbee738f6aed376fff0be2ce83060a8c376c9c81b0ba2f438ce551f6e69907ce81deb1e169d26a9f6146b9cddac39f7dec5a8eb7311f8d65c56febb5a1b839b21b0bd14178142d9c6d4a9c3d06507fbdaee56df4910402fc07c178b7afd75b8f5365bd53806a8661ca2225df71acfa4b0912cb55b8610f72c089c47357837b06af33b6b52434ff30a8afdb728a42236c7d0ba06202d1b97c1b4d97cdc7f33070710483d59cb19348a1beb1b5f4c16f782d7613bb51d1ffe2ace7fade22bacf77cb161c60376e682caf379fa3d401b9baf266c26c3f9d34f66066403359c874e8a481ae671edd6c84a8cf0890651fdccf3b35e37d5679532284a0ddd57f72d29717f4b810d7d6431e0958969447ed3ae3931de73b8c7fa8971484b28265501f37feb7486b2734bbfb2ecae511d86d8d60871464e06856229bf68a7d88e75f94269fe1376532519f94a3b0bf0c4dda22872b00272759f6df01172e18af2ed4623222604f08ab1bb0cac8ca47cee28c23c0b87c1d9f87082f046f0c29fcecbe885cc654578bf1c292c0a06af983363a8da50d803d279a2a28a6055052b255cf5cecf8cb9be0a1d814d136b34bc31f884cb1602102ed686667ce6dc060b69305379bed457500202e37ae13915c01259c8df28d4451f5872b5238703f2c695641f6dfe604240fb7245f336f9f3ae37e994548923b37ce266717bc2a4893031bc8da2b9d8733567779c7728afcc24eb7a5f111f10c61b429556cdef82cf546364c31ba5cb05980a724b1283bec07eded73733ee72f255a267b5bfaff12519b1cb487782a0dba46a0ced24e27cd9c1f0afc800a35c2014c104ed4185b5e7264ea0b05a907822b9a09e749be18e9899a42ed1eef3ccc4abe4e15635a6898bc9dd8812a367c1c4d25847e9c97cc971e7c3f37b6b80ec9e4afd5e22f243d01b085731bffe4f2afc81df920ebd976c80954862c92ad4df962bc5ab20bc45338c8ff85d8f07858729d094a37b92f499e072abe03881d2d94a8baef8226645df28bb038f248f9d3cebc042557035d9751caf0dbf206d5db80d84877f08b61143baaf22697a9cd9d7c40ab0727212a44e4f8518fff0d90c8799d58c9211060a019edea71ecb482b5331bb571e2285c798ee264f5dc2c6770b82b79cab5005d1a70c2465fdc9f89b4d300d7657b981eae90a0690a79a3a516dedce2ba474bbf520f06b80008acbd7eec3794b41fff1a60ef26164395b5d0494bb5149f6c08647140b39d11264af1f48b2422609dc2180b1457a6c6d7e734d5e4ea81c11173b280e0e6b6fae4f3d82ebe410aecb30ca8b653d83b8ddbbd4fff6a8491cb59a759a7f9acb84394c6bff2c090dcaaa9a7508241f89b25faf7b8f81768afe94025b1ed68717a05b214b370a66c1b1497ba50b8b65706704a4d18dfef46d7e1a7ed344de6453c2a19363624baaae974bff405cb87b1e2f3f9bf3371aff3c68cc7d078b92a94def3cd60979541ec6cb74ebc51fd0ddb6af317c79d3e672fbac39c51ed51dba9cc74a284410a418bcebd31e9d91b40ccca19866d31cd3810835aa7ae98b1c86dbcc8bd2570bc8023fd5f24fb79e87e93477eb38d8a5405a4d5790a52cc596f4470d12f51bb62a71e843795a2af78eda7fc2fae336074278914ac2d4ef536fa380ee5efb9ac633ff88dd512a7c71ca8510ea0499551454be1f5df794e2703d422449322051ec25d5facea92b77999664522c540518d2198f1bfabaa61b53077a3649ee9722d313aff49a9825fdf06405d75d60f9b4531b839e1f8654ad786e1ee769968e3faad0d7749266a3c9c10c114562e1e225b57faa290b64d5ad6e2b1187eb136ade10e535d6d3e899a9e6ee631ad21cdc46456c6048d4bcefaf47816e6422c50954ff74a29d420f2ba7028335f79e93240e6d540b84168a157e17e894e562999f34ec71729f05c83267550b7d21b0d43f7d4cd8fb26889d82e9a7217960825c532c17a1e1e6f339ca509356a4191091254d2d368c64276f43c9afb834a6bbc077769ac7ad14a2059b32c2b363ce303e8cdd7e17567d9778e81b0205e889d005937529087ce8e44b08bf7222d014750428d279e1fda3a673a3a6d8121f8caff1864c1875976361c20a30943298896c3e6f31a71de937b51b8cff21490c195dd7a168671867c441e801b0e8888724405a66076c2a8adc9053fce89737e89f2d39f262532fafd04f09c4cace7c0183aba8a4c6943d3ca9456547431ca7970fff7a663a05ef49"));
                null);

            DilithiumPublicKeyParameters newPublic = new DilithiumPublicKeyParameters(DilithiumParameters.Dilithium5Aes,
                Hex.Decode("bd77defa0f363aa8d7dffc57e7fe5dfb7d429adbba8abc4c81de2a691aa64a78"),
                Hex.Decode("c4a137eda3ab4bc082b5c457bcb7098558c2bab584dc4983501f0ab56f442f282dd69821c5131f8e947468ade27924fa5a42bbb1cef825dd6e3ff45be21fdc0880e76443ed669d20747081f3e5fc205f1d3ad80ece7014fb5929d76a07835992c1710207cc26343978403c0f4bd09182f4d54eec520722aeeb673d2e77175d3b5323ee14722327a4c3fd4c8248cf2bc3a4979eaca2d6f2375b5b2c2b0291977917c97aa1d3607bf69c7cfb6717a0522d152188d6c766f517f728ba70e8ea9f00ae3a46e7eb676a11ed7201d092d8ba00190f5eef5cf15ba591769b3a4ee55ea7853f374c771ac39584639f6d39c193097aea39cf788c027fcf2eb1f1e4241179ab98d0f7ed7e404474c05626a1612250cde0880dbf6274ab2e0fa60afdd6ae2f43173883bea372179fdbd9b47f579b4fcb0bf8eea5f55019b39c58020512fa663fb655ee66af4b1d26fed5c4706aad7b883e7835c8bbcbf027a570670046093b9aa8e00d166f782bd2f02e7535cd682750698ca839b52d164b1cb5c2e7ec684127e69c9c8e57951b62996d941513be05acd77f7acbdb04e94c53f76cb0d823f1f30d78ac205c84c3743f8e0b24f0a87c89f35689614ed4edd03c89b440f96d1ce422726750e23612228e6fab4d478020ce29d54b8d8ab22264a69e14d02bfac27bd80537749957d03c848c2e16f78112be096ebcaa0258ec94bd77bda5bceedad57e95c9a3f5a1e56566c96f08ccdc34ee01673819c65ba1e83ac2ae0075650ef920131f4b4d26788599df2930c0f1a56ec7441ce97fd773ee08ec997d9097f0d2f4acd2425c3b69a48ca71ffb82a0fa5a38ceda30936ec6a761c2833d674368726c71d6098d73ceb4fb259ea435a9b644ef8bcc4d434552bb69aaf64c596457bdc47f09349ac28644ef44eae72e0bebf2db646e2a01493546d3056359e022b65af4893b023277023310bf425ac95eb8f16b349f4e5c58481770229de8a6878ed406f642bbc3c1790b97dba7644633985de026eaf04088fdf8ec32d6f18ec303697578be27750b37bec0abce67f989bf62a93656db8f283c1d2b14b3fdf32122187e7d64f673923c0391719fb96b7ce16f6aadf0d29703aec4bc5297a2bb2ccc66822f8f99370437e93298dcd70160dc781ade16681f1b8e7ba15f24b215e9827df9f267ac3a366d661d0bd3a270173d1cd0b556bcf63e81ceba6355a56805794e6efcffe4a140368b7ec3d7c2eb8658c9e608ca2bffc1c756527acde54b77a1a5e75b7f07349aa8f9292dd76b6ec307ddedd0f3cd770a449099c6f757ae08402ef415cc566bd29c293603fe8e9a0962d4e2ea14bc002606f41d37a42dfcbee738f6aed376fff0be2ce83060a8c376c9c81b0ba2f438ce551f6e69907ce81deb1e169d26a9f6146b9cddac39f7dec5a8eb7311f8d65c56febb5a1b839b21b0bd14178142d9c6d4a9c3d06507fbdaee56df4910402fc07c178b7afd75b8f5365bd53806a8661ca2225df71acfa4b0912cb55b8610f72c089c47357837b06af33b6b52434ff30a8afdb728a42236c7d0ba06202d1b97c1b4d97cdc7f33070710483d59cb19348a1beb1b5f4c16f782d7613bb51d1ffe2ace7fade22bacf77cb161c60376e682caf379fa3d401b9baf266c26c3f9d34f66066403359c874e8a481ae671edd6c84a8cf0890651fdccf3b35e37d5679532284a0ddd57f72d29717f4b810d7d6431e0958969447ed3ae3931de73b8c7fa8971484b28265501f37feb7486b2734bbfb2ecae511d86d8d60871464e06856229bf68a7d88e75f94269fe1376532519f94a3b0bf0c4dda22872b00272759f6df01172e18af2ed4623222604f08ab1bb0cac8ca47cee28c23c0b87c1d9f87082f046f0c29fcecbe885cc654578bf1c292c0a06af983363a8da50d803d279a2a28a6055052b255cf5cecf8cb9be0a1d814d136b34bc31f884cb1602102ed686667ce6dc060b69305379bed457500202e37ae13915c01259c8df28d4451f5872b5238703f2c695641f6dfe604240fb7245f336f9f3ae37e994548923b37ce266717bc2a4893031bc8da2b9d8733567779c7728afcc24eb7a5f111f10c61b429556cdef82cf546364c31ba5cb05980a724b1283bec07eded73733ee72f255a267b5bfaff12519b1cb487782a0dba46a0ced24e27cd9c1f0afc800a35c2014c104ed4185b5e7264ea0b05a907822b9a09e749be18e9899a42ed1eef3ccc4abe4e15635a6898bc9dd8812a367c1c4d25847e9c97cc971e7c3f37b6b80ec9e4afd5e22f243d01b085731bffe4f2afc81df920ebd976c80954862c92ad4df962bc5ab20bc45338c8ff85d8f07858729d094a37b92f499e072abe03881d2d94a8baef8226645df28bb038f248f9d3cebc042557035d9751caf0dbf206d5db80d84877f08b61143baaf22697a9cd9d7c40ab0727212a44e4f8518fff0d90c8799d58c9211060a019edea71ecb482b5331bb571e2285c798ee264f5dc2c6770b82b79cab5005d1a70c2465fdc9f89b4d300d7657b981eae90a0690a79a3a516dedce2ba474bbf520f06b80008acbd7eec3794b41fff1a60ef26164395b5d0494bb5149f6c08647140b39d11264af1f48b2422609dc2180b1457a6c6d7e734d5e4ea81c11173b280e0e6b6fae4f3d82ebe410aecb30ca8b653d83b8ddbbd4fff6a8491cb59a759a7f9acb84394c6bff2c090dcaaa9a7508241f89b25faf7b8f81768afe94025b1ed68717a05b214b370a66c1b1497ba50b8b65706704a4d18dfef46d7e1a7ed344de6453c2a19363624baaae974bff405cb87b1e2f3f9bf3371aff3c68cc7d078b92a94def3cd60979541ec6cb74ebc51fd0ddb6af317c79d3e672fbac39c51ed51dba9cc74a284410a418bcebd31e9d91b40ccca19866d31cd3810835aa7ae98b1c86dbcc8bd2570bc8023fd5f24fb79e87e93477eb38d8a5405a4d5790a52cc596f4470d12f51bb62a71e843795a2af78eda7fc2fae336074278914ac2d4ef536fa380ee5efb9ac633ff88dd512a7c71ca8510ea0499551454be1f5df794e2703d422449322051ec25d5facea92b77999664522c540518d2198f1bfabaa61b53077a3649ee9722d313aff49a9825fdf06405d75d60f9b4531b839e1f8654ad786e1ee769968e3faad0d7749266a3c9c10c114562e1e225b57faa290b64d5ad6e2b1187eb136ade10e535d6d3e899a9e6ee631ad21cdc46456c6048d4bcefaf47816e6422c50954ff74a29d420f2ba7028335f79e93240e6d540b84168a157e17e894e562999f34ec71729f05c83267550b7d21b0d43f7d4cd8fb26889d82e9a7217960825c532c17a1e1e6f339ca509356a4191091254d2d368c64276f43c9afb834a6bbc077769ac7ad14a2059b32c2b363ce303e8cdd7e17567d9778e81b0205e889d005937529087ce8e44b08bf7222d014750428d279e1fda3a673a3a6d8121f8caff1864c1875976361c20a30943298896c3e6f31a71de937b51b8cff21490c195dd7a168671867c441e801b0e8888724405a66076c2a8adc9053fce89737e89f2d39f262532fafd04f09c4cace7c0183aba8a4c6943d3ca9456547431ca7970fff7a663a05ef49"));

            byte[] newMessage = Encoding.UTF8.GetBytes("this is a secret message...");
            byte[] newSignature = Dilithium.Sign(newMessage, newPrivate);
            bool newValid = Dilithium.Verify(Encoding.UTF8.GetBytes("this is a secret message..."), newSignature, newPublic);

            using (FileStream fs = new FileStream(@"C:\Temp\DilithiumPublic.pem", FileMode.Create, FileAccess.Write))
            {
                Dilithium.SavePublicKeyToPEM(publicKey, "dilithium5-aes", fs);
            }
            using (FileStream fs = new FileStream(@"C:\Temp\DilithiumPublic.pem", FileMode.Open, FileAccess.Read))
            {
                DilithiumPublicKeyParameters publicFromPem = Dilithium.LoadPublicKeyFromPEM(fs);
            }

            using (FileStream fs = new FileStream(@"C:\Temp\DilithiumPrivate.pem", FileMode.Create, FileAccess.Write))
            {
                Dilithium.SavePrivateKeyToPEM(privateKey, "dilithium5-aes", fs);
            }
            using (FileStream fs = new FileStream(@"C:\Temp\DilithiumPrivate.pem", FileMode.Open, FileAccess.Read))
            {
                DilithiumPrivateKeyParameters privateFromPem = Dilithium.LoadPrivateKeyFromPEM(fs);
            }

            using (FileStream fs = new FileStream(@"C:\Temp\DilithiumPrivateEnc.pem", FileMode.Create, FileAccess.Write))
            {
                Dilithium.SavePrivateKeyToPEM(privateKey, "dilithium5-aes", fs, "test1234");
            }
            using (FileStream fs = new FileStream(@"C:\Temp\DilithiumPrivateEnc.pem", FileMode.Open, FileAccess.Read))
            {
                DilithiumPrivateKeyParameters privateFromPem = Dilithium.LoadPrivateKeyFromPEM(fs, "test1234");
            }
        }
    }
}
