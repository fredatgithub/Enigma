﻿using Enigma.IO;
using Enigma.PQC;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Kyber;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnitTests.PQC
{
    internal class KyberTests
    {
        [Test]
        public void FirstTest()
        {
            Kyber.GenerateKeyPair(out KyberPublicKeyParameters publicKey,
                out KyberPrivateKeyParameters privateKey,
                KyberParameters.kyber1024_aes);
            byte[] pubKeyData = publicKey.GetEncoded();
            byte[] pKeyData = privateKey.GetEncoded();
            Kyber.Generate(publicKey, out byte[] clear1, out byte[] cipher1);
            Kyber.Generate(publicKey, out byte[] clear2, out byte[] cipher2);
            byte[] dec1 = Kyber.Extract(privateKey, cipher1);
            byte[] dec2 = Kyber.Extract(privateKey, cipher2);

            int lenT = 1536;
            int lenRho = 32;
            int lenHpk = 32;
            int lenS = 1536;
            int lenNonce = 32;

            byte[] tPub = new byte[lenT];
            byte[] rhoPub = new byte[lenRho];

            Array.Copy(pubKeyData, 0, tPub, 0, lenT);
            Array.Copy(pubKeyData, lenT, rhoPub, 0, lenRho);
            string tPubStr = Hex.Encode(tPub); //fb0ba5e9840a77d214b582230f842c9c2acbd8403a3a7ba170f743507218f0ab0a775730d4143a0e5aa17b297d39f95ba5dc708232537d8b3232a442d3ea8e936996ef676a948cc3d26b4d276568f7fc9e43001d8bb479fd2310b7718df2d1ad45a2c1a89b0e02120f718a6238fc6f23ea0d0664a7bfcac779fcb740943285e4999f33ae084c351880396b4352a9f8b10d67123ac74dbc841080976f5de9b339b7141ac7ad6b550394c447d7da7623657cdf531793099b20cb171ff5119726bdee2585291392f697349fe706642c21bd36b41055554880343ad04f7f5aa483a4cee502511a4c584c50ad98844a7d899c363088a4692a7ca9574b633c0420aa54b718e9e0835e6b456fe00fce673731d81f4873a592239d250191fbc40f6975230e2b473a502b172127f872907cd45e70e10c3dd93181424f3b843e91e05832058f43a0cb45e59ad921cf6cac6185689d1014a843940387ca182929648e8786378b7d7dac3005c7064b0a7a40a14e29a85679656550b325e884cdf204c4c733421a679337dc74bec4472248a97ba76a80a847cf183a722037fc1b9d96d454e40847bb23420eec999eb934ab805273c156e0257fa4528251a000d9331bf3aca1b876c1aec96b03962ec89b79781aa98ae72b19799d7f03509d016a8585cd7d33bf6a32747986136e59c91d3612a0d517cfb54663b8c51116c785dcc4ca2684d41868c530c35844a594182643807af583050be9bbb39180ae7cc59273b3d8348886f23c99894f828c37072278157560dad1b067d616a4d4539304ac1a79a586e78a34d8156215ccce5392fb732066d84b424bc9c521b6d4302f024c4aef468d8e5b29dcfa901c9b16bec91f438838afc69f1051ba6cc51f2ddcbb62a4274eea4501646813417a5f4c5b2366b0c10b3c53e76544c72d392205c389615fd44c3aa2ce8ed91574391b6d85b359b806be3a4a86ca194d5b7871db8a3e33a458776141514bd31c394d63984b889a25f052f286c127d857d261b4c2acc352f39b66ea940bd52903727a84c60bfce6c631ab23b5c8a3f6e6278caa32f69198436b7c9acc26f4a376c96214a253460fda4fc3d7ae1e8c072134a23c93ce7c2877d83498e6b45793593202775867d3a241a34b67a14448772f8109844bf24d5a59c7b09622d1726bd0fc36614a3cac0570579a5c9de8a0dd06ac80a734ef4524bc6a445ee84190b69e0a55532e99cab86399b765cc401610e4b92b48b5ba509a5776d11416fb40d852496698b57799c1335b4d63622c86e19ea7481e8f9631057a10f87a49ad9920e74025852c158ab561fa0a1bc3095d92f21fd9f98fe1911a93670c5464a84c42b2fdd53e9f1610274c3585139d0921485a49236648b2e40950b7907473aa02be34ae8167a77f50a807b447fb8281b41b9f9404754af36939ca3ee8e63c636b554ca6a780fba9086b7ca4b01cea53884f9b59f4907732b704f2096a6f8414f072210f4ba0f0338695e370efc759ba33127e0bc3c69936297a2eaff26bcdd21de441987a8a820f2ba868f994a7526d5c6c576275b1ed86ccccf51e859b356b0b6065eb253829a23f3103c65315f32613f289a6c44c42ca30b3d42c93fbb79f197b9153d711faea5d5f7aa03cd373bb60928c9844d4067f2a5a22a542c9aa43a30927ceb983b5d61323e45bbbbf282a365438a414beca8a37330451eb6204afba44755832815ab888dabfee0c5f2b008599b127161b9d89040148479aa21a87133541269822bfa0247454b23d32c58dc17e4f2ab7f56843f5a8074434c1372c1cb5c6c967da3502029edac3141fc89b6de9376cf548b827cc1e9a0c98bb240246cee5b38d1b90649bc6b754fc3e77052e3e2c2c6d43c850c66fd0a90814a773be52c93be67147674495a651bb59b3a6f335b4fb57333073e1c68d0bf6b38f705e57040eecb124dba2a6f0726789343927c753e78a2ecc114564db5b514480c1bb56a48424e8b77114c18ea2705808d69bba3b8d91bb01d5a47778dc2e951616bd66472a6c92b86a737b777dab833c74c998fafb9b98f76551535cf61a04f44bba1b518ae0815c80b516e2a64b8cba75d0160484f200bf517fff9b64bea420107232895953b6bc09992ca89e0a994949949db42c0b669ff917030a74b109da18
            string rhoPubStr = Hex.Encode(rhoPub); //0120693bcc413dc00ef9cfcb9430f96d962cfd0eeb1ff0a09429683a878a9186

            //public: T, Rho
            //private: S, T, Rho, Hpk, Nonce

            byte[] s = new byte[lenS];
            byte[] t = new byte[lenT];
            byte[] rho = new byte[lenRho];
            byte[] hpk = new byte[lenHpk];
            byte[] nonce = new byte[lenNonce];

            int index = 0;
            Array.Copy(pKeyData, index, s, 0, lenS);
            index += lenS;
            Array.Copy(pKeyData, index, t, 0, lenT);
            index += lenT;
            Array.Copy(pKeyData, index, rho, 0, lenRho);
            index += lenRho;
            Array.Copy(pKeyData, index, hpk, 0, lenHpk);
            index += lenHpk;
            Array.Copy(pKeyData, index, nonce, 0, lenNonce);

            string sStr = Hex.Encode(s); //02578a77616433f70585a168f6a385d0091c12632985901393c30347ca9c8e01a042aa848c556bf9636d247b28a1c5b9b4f975e4a486820250413060bf040cbfc53c93e8b2f60c6c505c8fbeeb5693808926a164aa650c74743cf106b8838369a922725ab46eac67b67731a5ea53116d720c6cc13a1996385fb925d78972f130cd6f41a59f4a55fa7c6d683605f6d002200206e411b189907ed569c2063a2b90307a4bcb0376e11c80c590314c41a1dc9aee4870692a577a548c06d670cde422e403a183f41ea91a077c086e0d67473f6b535804703f603d262630438b841d18a54478894687997de33450ab808234276f473f4f09c01e40affe6675efd21ab4222f9d78a1e9367ee945ae0b5bb81f4684ec2905c9b16031602d3a14c9b5a7ab032077886b06d297112847368d80b8ed6865b48a248a7b80068641cdca3c364ab2ef29cdcbb798c0bcbfcff66c9b6a3321c0c37dd3b9e28c237412ac9512179923c0e049a1504a3d1ff1bde2300e7ad43281a64eb8f2c03992a829d4a5f6893b559b554ed80209563a95169911b944427804f4853b0fb136c4b2a5892b9d40ea3847f77fd5580692598e399370c89523051a0f830baa746acc7ca13b897904ab09451de4424d5355853b348ea11dc20aab7540859ccab281dbbfa8558a60560c3be95f21d3ae589a8543907b77091cfbb220359a0a72c76b2455354bb447f8fc9faf004a70516986830a374bc6fd8809c26cc4ba5034a4e18c7585c7736cadf2e8895a37b7945152d81208dfd54cbd6b810dc4b9806b39ca0a010993cd7c6bc375b550fd1cbbabb16f4113159940cc77dc50f032a0710c335b4840311b6dab780075621694909bf73b49e65b0a0c904a8640aa024a5767841b6712b27f4061a777c68adb9cebbb4e9bdc49f406d0c8568154382929f4295c081e22e95b7710356e8170dddb70e8ac960013514b7bad4c99c42d87c72b713fecd12be29cb25185c65cd12571ac25e911cf0a555dbdc14837185600822ad43a46924a1e87ba97e6c09d45245d2734bae1c09f2b7886ae41196c9ca8c403179b126cac5c044ba28598b979b6082e312214ea73c36da822e5d6a16ad07aa067baa45216e1c80dac297d9bf361af113a90d1a21142c6b145426766b760f6a668d8ac2f15026431cc15f827e0194dcf6b61a2588fcff3c90eb94b8eaa34f8e34b6935173c46445c16237e49c9d8f6b404168ea4d33c81c151ccb3a2146612c0d792367a828e8392dd4b4fc87b6b4821387c128b4488c27979938c0123f6a41f943705c51a52c28c12c6692db9d126d37c903bd0b0ad2b24d07c6511ebc5a53684b2a940de4a062c1887e05bcad32c0fde269dbe8133f68311652aa96cc8b86a143822b7a2c88a61693c07529c3a37c5c1ef0a65464755e1e68f67dc024f1298b558a28f8ab5b07210f5bb4f40c06285f35bc9d7b843e424c031174482908d14a2a1a914a783cae0183777d1a6b2b0a213777a7e606e8fe33d4021cdd1b14e149a3284095aabd8b6ebe2741619a4127344c0147f6437734b1b7f428236a488b8bb8a5252d98f2ea2b53da42f912963b5925c8e7312eeb9be9c6747d1875180d73a7167c6974c373308c3f86a4590c4ad7f3697a7f53c8c337ccd741f8306ba5be003c86b2f6d1a9e6b107bedb8413f04bcb267132148739597ae343b4f0e7489bb502ad0f7279f8509ba6c3213aabc4394be946960372b44f94292e1522cb523210a025468eb8ae1ba035a89441fa474eb6923b8e634c57cb8c24bbf03ac5dea9b7d3c121ac8f5592fa4a391b0436ac455ebf2c7747c4733004946cc711ee0c911aac4f7e26d4bc643b899a3a0ab37a998201406c8ec752d1e6b8b31ea531eaa4400aa8e52ac4d1a23a4cd62cb17f8acdec07cba797af10c0570454fd7c27833921e0b05be92d49d89b9c142fa56a74461499930ce2a7e46489e968a539325c314c08c19f9933cc0c0db722dc8dabf04082175453f19a113c6bc3893993cfada03e6cacc25a115986b98fa689869f43ccf8842d53a98fef93678e65b90dc4ad89bbc00e84b6b53b64976ae6c16c2d53c5f8b753d85b1b7b51c2f00f381c2c05d4b389b7963692a81c032730b2081329bd2c7ebf3b48b9966ed579f63fc786867b3370823d72a169aa93c
            string tStr = Hex.Encode(t); //fb0ba5e9840a77d214b582230f842c9c2acbd8403a3a7ba170f743507218f0ab0a775730d4143a0e5aa17b297d39f95ba5dc708232537d8b3232a442d3ea8e936996ef676a948cc3d26b4d276568f7fc9e43001d8bb479fd2310b7718df2d1ad45a2c1a89b0e02120f718a6238fc6f23ea0d0664a7bfcac779fcb740943285e4999f33ae084c351880396b4352a9f8b10d67123ac74dbc841080976f5de9b339b7141ac7ad6b550394c447d7da7623657cdf531793099b20cb171ff5119726bdee2585291392f697349fe706642c21bd36b41055554880343ad04f7f5aa483a4cee502511a4c584c50ad98844a7d899c363088a4692a7ca9574b633c0420aa54b718e9e0835e6b456fe00fce673731d81f4873a592239d250191fbc40f6975230e2b473a502b172127f872907cd45e70e10c3dd93181424f3b843e91e05832058f43a0cb45e59ad921cf6cac6185689d1014a843940387ca182929648e8786378b7d7dac3005c7064b0a7a40a14e29a85679656550b325e884cdf204c4c733421a679337dc74bec4472248a97ba76a80a847cf183a722037fc1b9d96d454e40847bb23420eec999eb934ab805273c156e0257fa4528251a000d9331bf3aca1b876c1aec96b03962ec89b79781aa98ae72b19799d7f03509d016a8585cd7d33bf6a32747986136e59c91d3612a0d517cfb54663b8c51116c785dcc4ca2684d41868c530c35844a594182643807af583050be9bbb39180ae7cc59273b3d8348886f23c99894f828c37072278157560dad1b067d616a4d4539304ac1a79a586e78a34d8156215ccce5392fb732066d84b424bc9c521b6d4302f024c4aef468d8e5b29dcfa901c9b16bec91f438838afc69f1051ba6cc51f2ddcbb62a4274eea4501646813417a5f4c5b2366b0c10b3c53e76544c72d392205c389615fd44c3aa2ce8ed91574391b6d85b359b806be3a4a86ca194d5b7871db8a3e33a458776141514bd31c394d63984b889a25f052f286c127d857d261b4c2acc352f39b66ea940bd52903727a84c60bfce6c631ab23b5c8a3f6e6278caa32f69198436b7c9acc26f4a376c96214a253460fda4fc3d7ae1e8c072134a23c93ce7c2877d83498e6b45793593202775867d3a241a34b67a14448772f8109844bf24d5a59c7b09622d1726bd0fc36614a3cac0570579a5c9de8a0dd06ac80a734ef4524bc6a445ee84190b69e0a55532e99cab86399b765cc401610e4b92b48b5ba509a5776d11416fb40d852496698b57799c1335b4d63622c86e19ea7481e8f9631057a10f87a49ad9920e74025852c158ab561fa0a1bc3095d92f21fd9f98fe1911a93670c5464a84c42b2fdd53e9f1610274c3585139d0921485a49236648b2e40950b7907473aa02be34ae8167a77f50a807b447fb8281b41b9f9404754af36939ca3ee8e63c636b554ca6a780fba9086b7ca4b01cea53884f9b59f4907732b704f2096a6f8414f072210f4ba0f0338695e370efc759ba33127e0bc3c69936297a2eaff26bcdd21de441987a8a820f2ba868f994a7526d5c6c576275b1ed86ccccf51e859b356b0b6065eb253829a23f3103c65315f32613f289a6c44c42ca30b3d42c93fbb79f197b9153d711faea5d5f7aa03cd373bb60928c9844d4067f2a5a22a542c9aa43a30927ceb983b5d61323e45bbbbf282a365438a414beca8a37330451eb6204afba44755832815ab888dabfee0c5f2b008599b127161b9d89040148479aa21a87133541269822bfa0247454b23d32c58dc17e4f2ab7f56843f5a8074434c1372c1cb5c6c967da3502029edac3141fc89b6de9376cf548b827cc1e9a0c98bb240246cee5b38d1b90649bc6b754fc3e77052e3e2c2c6d43c850c66fd0a90814a773be52c93be67147674495a651bb59b3a6f335b4fb57333073e1c68d0bf6b38f705e57040eecb124dba2a6f0726789343927c753e78a2ecc114564db5b514480c1bb56a48424e8b77114c18ea2705808d69bba3b8d91bb01d5a47778dc2e951616bd66472a6c92b86a737b777dab833c74c998fafb9b98f76551535cf61a04f44bba1b518ae0815c80b516e2a64b8cba75d0160484f200bf517fff9b64bea420107232895953b6bc09992ca89e0a994949949db42c0b669ff917030a74b109da18
            string rhoStr = Hex.Encode(rho); //0120693bcc413dc00ef9cfcb9430f96d962cfd0eeb1ff0a09429683a878a9186
            string hpkStr = Hex.Encode(hpk); //11853a176e38e4679ca12fcf3292a90b2e8960cb5bc7661d036f5e752464747a
            string nonceStr = Hex.Encode(nonce); //284c202e5ae8795084958369d56a3451395755024e14b943d3cfc1b585869218

            KyberPrivateKeyParameters newPrivate = new KyberPrivateKeyParameters(KyberParameters.kyber1024_aes,
                Hex.Decode("02578a77616433f70585a168f6a385d0091c12632985901393c30347ca9c8e01a042aa848c556bf9636d247b28a1c5b9b4f975e4a486820250413060bf040cbfc53c93e8b2f60c6c505c8fbeeb5693808926a164aa650c74743cf106b8838369a922725ab46eac67b67731a5ea53116d720c6cc13a1996385fb925d78972f130cd6f41a59f4a55fa7c6d683605f6d002200206e411b189907ed569c2063a2b90307a4bcb0376e11c80c590314c41a1dc9aee4870692a577a548c06d670cde422e403a183f41ea91a077c086e0d67473f6b535804703f603d262630438b841d18a54478894687997de33450ab808234276f473f4f09c01e40affe6675efd21ab4222f9d78a1e9367ee945ae0b5bb81f4684ec2905c9b16031602d3a14c9b5a7ab032077886b06d297112847368d80b8ed6865b48a248a7b80068641cdca3c364ab2ef29cdcbb798c0bcbfcff66c9b6a3321c0c37dd3b9e28c237412ac9512179923c0e049a1504a3d1ff1bde2300e7ad43281a64eb8f2c03992a829d4a5f6893b559b554ed80209563a95169911b944427804f4853b0fb136c4b2a5892b9d40ea3847f77fd5580692598e399370c89523051a0f830baa746acc7ca13b897904ab09451de4424d5355853b348ea11dc20aab7540859ccab281dbbfa8558a60560c3be95f21d3ae589a8543907b77091cfbb220359a0a72c76b2455354bb447f8fc9faf004a70516986830a374bc6fd8809c26cc4ba5034a4e18c7585c7736cadf2e8895a37b7945152d81208dfd54cbd6b810dc4b9806b39ca0a010993cd7c6bc375b550fd1cbbabb16f4113159940cc77dc50f032a0710c335b4840311b6dab780075621694909bf73b49e65b0a0c904a8640aa024a5767841b6712b27f4061a777c68adb9cebbb4e9bdc49f406d0c8568154382929f4295c081e22e95b7710356e8170dddb70e8ac960013514b7bad4c99c42d87c72b713fecd12be29cb25185c65cd12571ac25e911cf0a555dbdc14837185600822ad43a46924a1e87ba97e6c09d45245d2734bae1c09f2b7886ae41196c9ca8c403179b126cac5c044ba28598b979b6082e312214ea73c36da822e5d6a16ad07aa067baa45216e1c80dac297d9bf361af113a90d1a21142c6b145426766b760f6a668d8ac2f15026431cc15f827e0194dcf6b61a2588fcff3c90eb94b8eaa34f8e34b6935173c46445c16237e49c9d8f6b404168ea4d33c81c151ccb3a2146612c0d792367a828e8392dd4b4fc87b6b4821387c128b4488c27979938c0123f6a41f943705c51a52c28c12c6692db9d126d37c903bd0b0ad2b24d07c6511ebc5a53684b2a940de4a062c1887e05bcad32c0fde269dbe8133f68311652aa96cc8b86a143822b7a2c88a61693c07529c3a37c5c1ef0a65464755e1e68f67dc024f1298b558a28f8ab5b07210f5bb4f40c06285f35bc9d7b843e424c031174482908d14a2a1a914a783cae0183777d1a6b2b0a213777a7e606e8fe33d4021cdd1b14e149a3284095aabd8b6ebe2741619a4127344c0147f6437734b1b7f428236a488b8bb8a5252d98f2ea2b53da42f912963b5925c8e7312eeb9be9c6747d1875180d73a7167c6974c373308c3f86a4590c4ad7f3697a7f53c8c337ccd741f8306ba5be003c86b2f6d1a9e6b107bedb8413f04bcb267132148739597ae343b4f0e7489bb502ad0f7279f8509ba6c3213aabc4394be946960372b44f94292e1522cb523210a025468eb8ae1ba035a89441fa474eb6923b8e634c57cb8c24bbf03ac5dea9b7d3c121ac8f5592fa4a391b0436ac455ebf2c7747c4733004946cc711ee0c911aac4f7e26d4bc643b899a3a0ab37a998201406c8ec752d1e6b8b31ea531eaa4400aa8e52ac4d1a23a4cd62cb17f8acdec07cba797af10c0570454fd7c27833921e0b05be92d49d89b9c142fa56a74461499930ce2a7e46489e968a539325c314c08c19f9933cc0c0db722dc8dabf04082175453f19a113c6bc3893993cfada03e6cacc25a115986b98fa689869f43ccf8842d53a98fef93678e65b90dc4ad89bbc00e84b6b53b64976ae6c16c2d53c5f8b753d85b1b7b51c2f00f381c2c05d4b389b7963692a81c032730b2081329bd2c7ebf3b48b9966ed579f63fc786867b3370823d72a169aa93c"),
                Hex.Decode("11853a176e38e4679ca12fcf3292a90b2e8960cb5bc7661d036f5e752464747a"),
                Hex.Decode("284c202e5ae8795084958369d56a3451395755024e14b943d3cfc1b585869218"),
                Hex.Decode("fb0ba5e9840a77d214b582230f842c9c2acbd8403a3a7ba170f743507218f0ab0a775730d4143a0e5aa17b297d39f95ba5dc708232537d8b3232a442d3ea8e936996ef676a948cc3d26b4d276568f7fc9e43001d8bb479fd2310b7718df2d1ad45a2c1a89b0e02120f718a6238fc6f23ea0d0664a7bfcac779fcb740943285e4999f33ae084c351880396b4352a9f8b10d67123ac74dbc841080976f5de9b339b7141ac7ad6b550394c447d7da7623657cdf531793099b20cb171ff5119726bdee2585291392f697349fe706642c21bd36b41055554880343ad04f7f5aa483a4cee502511a4c584c50ad98844a7d899c363088a4692a7ca9574b633c0420aa54b718e9e0835e6b456fe00fce673731d81f4873a592239d250191fbc40f6975230e2b473a502b172127f872907cd45e70e10c3dd93181424f3b843e91e05832058f43a0cb45e59ad921cf6cac6185689d1014a843940387ca182929648e8786378b7d7dac3005c7064b0a7a40a14e29a85679656550b325e884cdf204c4c733421a679337dc74bec4472248a97ba76a80a847cf183a722037fc1b9d96d454e40847bb23420eec999eb934ab805273c156e0257fa4528251a000d9331bf3aca1b876c1aec96b03962ec89b79781aa98ae72b19799d7f03509d016a8585cd7d33bf6a32747986136e59c91d3612a0d517cfb54663b8c51116c785dcc4ca2684d41868c530c35844a594182643807af583050be9bbb39180ae7cc59273b3d8348886f23c99894f828c37072278157560dad1b067d616a4d4539304ac1a79a586e78a34d8156215ccce5392fb732066d84b424bc9c521b6d4302f024c4aef468d8e5b29dcfa901c9b16bec91f438838afc69f1051ba6cc51f2ddcbb62a4274eea4501646813417a5f4c5b2366b0c10b3c53e76544c72d392205c389615fd44c3aa2ce8ed91574391b6d85b359b806be3a4a86ca194d5b7871db8a3e33a458776141514bd31c394d63984b889a25f052f286c127d857d261b4c2acc352f39b66ea940bd52903727a84c60bfce6c631ab23b5c8a3f6e6278caa32f69198436b7c9acc26f4a376c96214a253460fda4fc3d7ae1e8c072134a23c93ce7c2877d83498e6b45793593202775867d3a241a34b67a14448772f8109844bf24d5a59c7b09622d1726bd0fc36614a3cac0570579a5c9de8a0dd06ac80a734ef4524bc6a445ee84190b69e0a55532e99cab86399b765cc401610e4b92b48b5ba509a5776d11416fb40d852496698b57799c1335b4d63622c86e19ea7481e8f9631057a10f87a49ad9920e74025852c158ab561fa0a1bc3095d92f21fd9f98fe1911a93670c5464a84c42b2fdd53e9f1610274c3585139d0921485a49236648b2e40950b7907473aa02be34ae8167a77f50a807b447fb8281b41b9f9404754af36939ca3ee8e63c636b554ca6a780fba9086b7ca4b01cea53884f9b59f4907732b704f2096a6f8414f072210f4ba0f0338695e370efc759ba33127e0bc3c69936297a2eaff26bcdd21de441987a8a820f2ba868f994a7526d5c6c576275b1ed86ccccf51e859b356b0b6065eb253829a23f3103c65315f32613f289a6c44c42ca30b3d42c93fbb79f197b9153d711faea5d5f7aa03cd373bb60928c9844d4067f2a5a22a542c9aa43a30927ceb983b5d61323e45bbbbf282a365438a414beca8a37330451eb6204afba44755832815ab888dabfee0c5f2b008599b127161b9d89040148479aa21a87133541269822bfa0247454b23d32c58dc17e4f2ab7f56843f5a8074434c1372c1cb5c6c967da3502029edac3141fc89b6de9376cf548b827cc1e9a0c98bb240246cee5b38d1b90649bc6b754fc3e77052e3e2c2c6d43c850c66fd0a90814a773be52c93be67147674495a651bb59b3a6f335b4fb57333073e1c68d0bf6b38f705e57040eecb124dba2a6f0726789343927c753e78a2ecc114564db5b514480c1bb56a48424e8b77114c18ea2705808d69bba3b8d91bb01d5a47778dc2e951616bd66472a6c92b86a737b777dab833c74c998fafb9b98f76551535cf61a04f44bba1b518ae0815c80b516e2a64b8cba75d0160484f200bf517fff9b64bea420107232895953b6bc09992ca89e0a994949949db42c0b669ff917030a74b109da18"),
                Hex.Decode("0120693bcc413dc00ef9cfcb9430f96d962cfd0eeb1ff0a09429683a878a9186"));

            KyberPublicKeyParameters newPublic = new KyberPublicKeyParameters(KyberParameters.kyber1024_aes,
                Hex.Decode("fb0ba5e9840a77d214b582230f842c9c2acbd8403a3a7ba170f743507218f0ab0a775730d4143a0e5aa17b297d39f95ba5dc708232537d8b3232a442d3ea8e936996ef676a948cc3d26b4d276568f7fc9e43001d8bb479fd2310b7718df2d1ad45a2c1a89b0e02120f718a6238fc6f23ea0d0664a7bfcac779fcb740943285e4999f33ae084c351880396b4352a9f8b10d67123ac74dbc841080976f5de9b339b7141ac7ad6b550394c447d7da7623657cdf531793099b20cb171ff5119726bdee2585291392f697349fe706642c21bd36b41055554880343ad04f7f5aa483a4cee502511a4c584c50ad98844a7d899c363088a4692a7ca9574b633c0420aa54b718e9e0835e6b456fe00fce673731d81f4873a592239d250191fbc40f6975230e2b473a502b172127f872907cd45e70e10c3dd93181424f3b843e91e05832058f43a0cb45e59ad921cf6cac6185689d1014a843940387ca182929648e8786378b7d7dac3005c7064b0a7a40a14e29a85679656550b325e884cdf204c4c733421a679337dc74bec4472248a97ba76a80a847cf183a722037fc1b9d96d454e40847bb23420eec999eb934ab805273c156e0257fa4528251a000d9331bf3aca1b876c1aec96b03962ec89b79781aa98ae72b19799d7f03509d016a8585cd7d33bf6a32747986136e59c91d3612a0d517cfb54663b8c51116c785dcc4ca2684d41868c530c35844a594182643807af583050be9bbb39180ae7cc59273b3d8348886f23c99894f828c37072278157560dad1b067d616a4d4539304ac1a79a586e78a34d8156215ccce5392fb732066d84b424bc9c521b6d4302f024c4aef468d8e5b29dcfa901c9b16bec91f438838afc69f1051ba6cc51f2ddcbb62a4274eea4501646813417a5f4c5b2366b0c10b3c53e76544c72d392205c389615fd44c3aa2ce8ed91574391b6d85b359b806be3a4a86ca194d5b7871db8a3e33a458776141514bd31c394d63984b889a25f052f286c127d857d261b4c2acc352f39b66ea940bd52903727a84c60bfce6c631ab23b5c8a3f6e6278caa32f69198436b7c9acc26f4a376c96214a253460fda4fc3d7ae1e8c072134a23c93ce7c2877d83498e6b45793593202775867d3a241a34b67a14448772f8109844bf24d5a59c7b09622d1726bd0fc36614a3cac0570579a5c9de8a0dd06ac80a734ef4524bc6a445ee84190b69e0a55532e99cab86399b765cc401610e4b92b48b5ba509a5776d11416fb40d852496698b57799c1335b4d63622c86e19ea7481e8f9631057a10f87a49ad9920e74025852c158ab561fa0a1bc3095d92f21fd9f98fe1911a93670c5464a84c42b2fdd53e9f1610274c3585139d0921485a49236648b2e40950b7907473aa02be34ae8167a77f50a807b447fb8281b41b9f9404754af36939ca3ee8e63c636b554ca6a780fba9086b7ca4b01cea53884f9b59f4907732b704f2096a6f8414f072210f4ba0f0338695e370efc759ba33127e0bc3c69936297a2eaff26bcdd21de441987a8a820f2ba868f994a7526d5c6c576275b1ed86ccccf51e859b356b0b6065eb253829a23f3103c65315f32613f289a6c44c42ca30b3d42c93fbb79f197b9153d711faea5d5f7aa03cd373bb60928c9844d4067f2a5a22a542c9aa43a30927ceb983b5d61323e45bbbbf282a365438a414beca8a37330451eb6204afba44755832815ab888dabfee0c5f2b008599b127161b9d89040148479aa21a87133541269822bfa0247454b23d32c58dc17e4f2ab7f56843f5a8074434c1372c1cb5c6c967da3502029edac3141fc89b6de9376cf548b827cc1e9a0c98bb240246cee5b38d1b90649bc6b754fc3e77052e3e2c2c6d43c850c66fd0a90814a773be52c93be67147674495a651bb59b3a6f335b4fb57333073e1c68d0bf6b38f705e57040eecb124dba2a6f0726789343927c753e78a2ecc114564db5b514480c1bb56a48424e8b77114c18ea2705808d69bba3b8d91bb01d5a47778dc2e951616bd66472a6c92b86a737b777dab833c74c998fafb9b98f76551535cf61a04f44bba1b518ae0815c80b516e2a64b8cba75d0160484f200bf517fff9b64bea420107232895953b6bc09992ca89e0a994949949db42c0b669ff917030a74b109da18"),
                Hex.Decode("0120693bcc413dc00ef9cfcb9430f96d962cfd0eeb1ff0a09429683a878a9186"));

            Kyber.Generate(newPublic, out byte[] newClearKey, out byte[] newEncKey);

            string newClearKeyStr = Hex.Encode(newClearKey); //a73db0b0bb57e3eef4364abcb76a84bba72b1fad9adc17abfd9d623d7bd9d474
            string newEncKeyStr = Hex.Encode(newEncKey); //c5f8c1c8bbe59885cd0ca6e6ce93cfe2fb25d3a901cf68c51c563ddb4a5bd3888f59605ea6eb8a465774aefa32efc0c22b238adc99c0c87ef8bdaed414f61a263da59c1c26585dfa774d9d870293fd78f7ed5c551082c33bb8041542980d260ba60cbcc27ae796bfcc3f05e993e93e6f4d7ffe5f2032a55c0570bdf4d3deea49220b0d5de89e47725e1db41ff3c7f003e4efaf97de9a31b6afd6f93f4bf708060978aa76300991832ddd6eec83c9433bc8785688dde5d679dc3e24df1e6a9e5201f3bbacca4ba6a70b3a9d4204bc9deb5e165eeaa6816ccf62178c4ec8fa0a8b843887bf56da937ee2d1d4c9ff9a47a378ded581c5c82dc1e707bd826e15d2cdb3a8aed09cbb124882941b73f708c9f80f3ee2619a1e7f837bbd6b419d9f71839779727fb665b19cac91d589b59ddf27340fc9b00b7f1c92a4e894e93aee6cca62f43faca12c47d2f918f9d5443a993bf78f65bee00c203f9bc0f79917df4b3b77025274170cb9252b607ddfce381cbfc35cc3ab9f081b36a68c2860da408485029cbf7bcac7c4d74df708001d351d70486cd56ba8a82c054228ab6e1580bb5d9fb7243912a456caf6c3e8cbe16cae7caf2b831b28337dad40dde96f08df6e0442ec2876235335f9bf335eb78785b2ecd5b7b67a45467e5c9fcb98cd2b6d37bf31faa56781999e3feb1a686b22a7599f6d1a9e3b7b088e52493981d40918aa03073eccad2d0135ea4d6cd84efdb615770bb3288acfe8c70f082c3593f8ba717be5fe0434009690ca7cf055ae038139c9f01ffd4d4ba5d8401c7bd931d375089488b64d6de298c97283258891659313694c3eb96b9fd086a3f4046dd1ea202742cb960e4f35a048f37e645e592efd55db4227e4a06eaae8c5a841e2d4a72a621b95b5415343236b89fc20cc5a2a6184d090a796f612b342d805b0d911da51ae2b1937d0d30703b6e8dceb3c20e6cc0c4ee1a24d27f0ff98c3f90eadf11b4091bb0092dee5feb474e8b491cbdf002fcb77494719991f0b2b8c89bda34ada2ef413602cbcbe494c1602b459b82fc2dafcc47d5c55ef6769a3415a8916de9bd152411886afa32d780f44eba4535eef52fe928627372863e10bca9ea7730ae46f52d236fdd01520176e5f6d2555dd5f39174e9d3dbbc8328dc2a265b7c81f5bbf30abe0039040c9b4f4ffe36363243ead0dddaa89f9504b6b53379efe684ea40e0090e5f3480d0291ac585bd85f2c269c9daff5c21f6f82b7c49e2868057eb86110f870a77a8a5d0efad3ace3a79e54d9053c80909592979ad94be68bf47f3943e419022a33efeb0a72b4c5e0cced46ff86f7dfbf618d005dc450ce74579125a76ebe6b4781ea765c8f4bc5384d8bcf7e276ad4f46a43cd0056cd465e1a4406af23ce4af1cc33f791965073ddfa718afb1c0242428679b42a38e4761cd689afb55e290947ee3392c38cfc10d0e36b5fafca71a969dce714a844ad362074ada7ec0d9aeee408602ba760901e8c15b72e130de4da9eaa3592f8235d537c5bf19c2b8e4481b9debbd7494b85c02fd1b8b49809bf6f61c44d93a75aaf161d31bd579b0c28ceb42b0f2c0d6e0220ddd4d3b77780f3a7c1b53ef3e5b21a27921461c2bb28865cbb0f6fa7b3eca3fa7ec81a3df2a154743d294ea7a7ac1fdc0d6b3f256488d2721a97255007aba3dbcd843fd95313b5732413fe64dea0e36c91a5f8456e5bb0701c1a22315d0b9509adebd4159c65e0ef465287a0736d5b98283e3fe215a9a33d3b209c2cc4501b36b038e23993287446ac11e88b385e94952bcb26c8f2257214ebdc7d37eff867c691394ba20e3b855b7a8bf6773d5914fc4e38c7f355f1c2db8f7577f318142d6b25c4fa1c01495bd65dd5cca4907a2804d614fc36c148ab39714647dc5a78d6f1a1f61ecab7dc9fa70c3296797889e2d21474621bcfa7e3f6bbb838414c1c0b201e64d913ac07e759c2ad78cfb6a7c9094f04c07d27982c2517e97500c19e9bbb4ca42c60e2cac1990da4bec85cb7ea34dfe985c84279918f3fcd0ce186cd3e7dc4f331475c4e6e9bef25490e561220420e694d1aaa742a4018cb66d8d582a10dc43dcea7d4e8a21cd3607cd3cd22603543d9aa2b347ad8a65ae2e60a02c93e8f5291543e56a69de34be669d58c9c9c8bad68d03f79fc0dfae47c05a3ef298c17a2245be148a8e5da2899cd4c4b9768395ef7c1c160c825

            byte[] newDec = Kyber.Extract(newPrivate, Hex.Decode("c5f8c1c8bbe59885cd0ca6e6ce93cfe2fb25d3a901cf68c51c563ddb4a5bd3888f59605ea6eb8a465774aefa32efc0c22b238adc99c0c87ef8bdaed414f61a263da59c1c26585dfa774d9d870293fd78f7ed5c551082c33bb8041542980d260ba60cbcc27ae796bfcc3f05e993e93e6f4d7ffe5f2032a55c0570bdf4d3deea49220b0d5de89e47725e1db41ff3c7f003e4efaf97de9a31b6afd6f93f4bf708060978aa76300991832ddd6eec83c9433bc8785688dde5d679dc3e24df1e6a9e5201f3bbacca4ba6a70b3a9d4204bc9deb5e165eeaa6816ccf62178c4ec8fa0a8b843887bf56da937ee2d1d4c9ff9a47a378ded581c5c82dc1e707bd826e15d2cdb3a8aed09cbb124882941b73f708c9f80f3ee2619a1e7f837bbd6b419d9f71839779727fb665b19cac91d589b59ddf27340fc9b00b7f1c92a4e894e93aee6cca62f43faca12c47d2f918f9d5443a993bf78f65bee00c203f9bc0f79917df4b3b77025274170cb9252b607ddfce381cbfc35cc3ab9f081b36a68c2860da408485029cbf7bcac7c4d74df708001d351d70486cd56ba8a82c054228ab6e1580bb5d9fb7243912a456caf6c3e8cbe16cae7caf2b831b28337dad40dde96f08df6e0442ec2876235335f9bf335eb78785b2ecd5b7b67a45467e5c9fcb98cd2b6d37bf31faa56781999e3feb1a686b22a7599f6d1a9e3b7b088e52493981d40918aa03073eccad2d0135ea4d6cd84efdb615770bb3288acfe8c70f082c3593f8ba717be5fe0434009690ca7cf055ae038139c9f01ffd4d4ba5d8401c7bd931d375089488b64d6de298c97283258891659313694c3eb96b9fd086a3f4046dd1ea202742cb960e4f35a048f37e645e592efd55db4227e4a06eaae8c5a841e2d4a72a621b95b5415343236b89fc20cc5a2a6184d090a796f612b342d805b0d911da51ae2b1937d0d30703b6e8dceb3c20e6cc0c4ee1a24d27f0ff98c3f90eadf11b4091bb0092dee5feb474e8b491cbdf002fcb77494719991f0b2b8c89bda34ada2ef413602cbcbe494c1602b459b82fc2dafcc47d5c55ef6769a3415a8916de9bd152411886afa32d780f44eba4535eef52fe928627372863e10bca9ea7730ae46f52d236fdd01520176e5f6d2555dd5f39174e9d3dbbc8328dc2a265b7c81f5bbf30abe0039040c9b4f4ffe36363243ead0dddaa89f9504b6b53379efe684ea40e0090e5f3480d0291ac585bd85f2c269c9daff5c21f6f82b7c49e2868057eb86110f870a77a8a5d0efad3ace3a79e54d9053c80909592979ad94be68bf47f3943e419022a33efeb0a72b4c5e0cced46ff86f7dfbf618d005dc450ce74579125a76ebe6b4781ea765c8f4bc5384d8bcf7e276ad4f46a43cd0056cd465e1a4406af23ce4af1cc33f791965073ddfa718afb1c0242428679b42a38e4761cd689afb55e290947ee3392c38cfc10d0e36b5fafca71a969dce714a844ad362074ada7ec0d9aeee408602ba760901e8c15b72e130de4da9eaa3592f8235d537c5bf19c2b8e4481b9debbd7494b85c02fd1b8b49809bf6f61c44d93a75aaf161d31bd579b0c28ceb42b0f2c0d6e0220ddd4d3b77780f3a7c1b53ef3e5b21a27921461c2bb28865cbb0f6fa7b3eca3fa7ec81a3df2a154743d294ea7a7ac1fdc0d6b3f256488d2721a97255007aba3dbcd843fd95313b5732413fe64dea0e36c91a5f8456e5bb0701c1a22315d0b9509adebd4159c65e0ef465287a0736d5b98283e3fe215a9a33d3b209c2cc4501b36b038e23993287446ac11e88b385e94952bcb26c8f2257214ebdc7d37eff867c691394ba20e3b855b7a8bf6773d5914fc4e38c7f355f1c2db8f7577f318142d6b25c4fa1c01495bd65dd5cca4907a2804d614fc36c148ab39714647dc5a78d6f1a1f61ecab7dc9fa70c3296797889e2d21474621bcfa7e3f6bbb838414c1c0b201e64d913ac07e759c2ad78cfb6a7c9094f04c07d27982c2517e97500c19e9bbb4ca42c60e2cac1990da4bec85cb7ea34dfe985c84279918f3fcd0ce186cd3e7dc4f331475c4e6e9bef25490e561220420e694d1aaa742a4018cb66d8d582a10dc43dcea7d4e8a21cd3607cd3cd22603543d9aa2b347ad8a65ae2e60a02c93e8f5291543e56a69de34be669d58c9c9c8bad68d03f79fc0dfae47c05a3ef298c17a2245be148a8e5da2899cd4c4b9768395ef7c1c160c825"));
            string newDecStr = Hex.Encode(newDec);

            using (FileStream fs = new FileStream(@"C:\Temp\KyberPublic.pem", FileMode.Create, FileAccess.Write))
            {
                Kyber.SavePublicKeyToPEM(publicKey, fs);
            }
            using (FileStream fs = new FileStream(@"C:\Temp\KyberPublic.pem", FileMode.Open, FileAccess.Read))
            {
                KyberPublicKeyParameters publicFromPem = Kyber.LoadPublicKeyFromPEM(fs);
            }

            using (FileStream fs = new FileStream(@"C:\Temp\KyberPrivate.pem", FileMode.Create, FileAccess.Write))
            {
                Kyber.SavePrivateKeyToPEM(privateKey, fs);
            }
            using (FileStream fs = new FileStream(@"C:\Temp\KyberPrivate.pem", FileMode.Open, FileAccess.Read))
            {
                KyberPrivateKeyParameters privateFromPem = Kyber.LoadPrivateKeyFromPEM(fs);
            }

            using (FileStream fs = new FileStream(@"C:\Temp\KyberPrivateEnc.pem", FileMode.Create, FileAccess.Write))
            {
                Kyber.SavePrivateKeyToPEM(privateKey, fs, "test1234");
            }
            using (FileStream fs = new FileStream(@"C:\Temp\KyberPrivateEnc.pem", FileMode.Open, FileAccess.Read))
            {
                KyberPrivateKeyParameters privateFromPem = Kyber.LoadPrivateKeyFromPEM(fs, "test1234");
            }
        }
    }
}
