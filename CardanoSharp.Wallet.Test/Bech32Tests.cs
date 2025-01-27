﻿using CardanoSharp.Wallet.Encoding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using CardanoSharp.Wallet.Extensions;

namespace CardanoSharp.Wallet.Test
{
    /// <summary>
    /// Bech32 Test vectors from BIP-0173
    /// https://github.com/bitcoin/bips/blob/master/bip-0173.mediawiki#test-vectors
    /// </summary>
    public class Bech32Tests
    {
        /// <summary>
        /// 
        /// https://github.com/bitcoin/bips/blob/master/bip-0173.mediawiki#test-vectors
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="expectedHex"></param>
        /// <param name="expectedVer"></param>
        /// <param name="expectedHrp"></param>
        [Theory]
        //@TODO Fails [InlineData("A12UEL5L", "", 0x00, "")] //@TODO verify
        //@TODO Fails [InlineData("a12uel5l", "", 0x00, "")] //@TODO verify
        //@TODO Fails [InlineData("an83characterlonghumanreadablepartthatcontainsthenumber1andtheexcludedcharactersbio1tt5tgs", "", 0x00, "")]
        //[InlineData("abcdef1qpzry9x8gf2tvdw0s3jn54khce6mua7lmqqqxw", "00443214c74254b635cf84653a56d7c675be77df", 0x00, "abcdef")]
        //[InlineData("11qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqc8247j", "000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000", 0x00, "1")]
        //@TODO Fails [InlineData("split1checkupstagehandshakeupstreamerranterredcaperred2y9e3w", "c5f38b70305f5fb6cf03058f3dde463ecd7918f2dc743918f2d", 0x00, "split")]
        //@TODO Fails [InlineData("?1ezyfcl", "", 0x00, "")]
        //@TODO Fails [InlineData("BC1QW508D6QEJXTDG4Y5R3ZARVARY0C5XW7KV8F3T4", "0014751e76e8199196d454941c45d1b3a323f1433bd6", 0x00, "BC")]
        //@TODO Fails [InlineData("tb1qrp33g0q5c5txsp9arysrx4k6zdkfs4nce4xj0gdcccefvpysxf3q0sl5k7", "00201863143c14c5166804bd19203356da136c985678cd4d27a1b8c6329604903262", 0x00, "tb")]
        //@TODO Fails [InlineData("bc1pw508d6qejxtdg4y5r3zarvary0c5xw7kw508d6qejxtdg4y5r3zarvary0c5xw7k7grplx", "5128751e76e8199196d454941c45d1b3a323f1433bd6751e76e8199196d454941c45d1b3a323f1433bd6", 0x00, "tb")]
        //@TODO Fails [InlineData("BC1SW50QA3JX3S", "6002751e", 0x00, "tb")]
        //@TODO Fails [InlineData("bc1zw508d6qejxtdg4y5r3zarvaryvg6kdaj", "5210751e76e8199196d454941c45d1b3a323", 0x00, "tb")]
        //@TODO Fails [InlineData("tb1qqqqqp399et2xygdj5xreqhjjvcmzhxw4aywxecjdzew6hylgvsesrxh6hy", "0020000000c4a5cad46221b2a187905e5266362b99d5e91c6ce24d165dab93e86433", 0x00, "tb")]
        //CIP19 Test Vectors
        [InlineData("addr1qx2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzer3n0d3vllmyqwsx5wktcd8cc3sq835lu7drv2xwl2wywfgse35a3x", "019493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8e337b62cfff6403a06a3acbc34f8c46003c69fe79a3628cefa9c47251", 0, "addr")]
        [InlineData("addr1z8phkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gten0d3vllmyqwsx5wktcd8cc3sq835lu7drv2xwl2wywfgs9yc0hh", "11c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f337b62cfff6403a06a3acbc34f8c46003c69fe79a3628cefa9c47251", 2, "addr")]
        [InlineData("addr1yx2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzerkr0vd4msrxnuwnccdxlhdjar77j6lg0wypcc9uar5d2shs2z78ve", "219493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8ec37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 4, "addr")]
        [InlineData("addr1x8phkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gt7r0vd4msrxnuwnccdxlhdjar77j6lg0wypcc9uar5d2shskhj42g", "31c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542fc37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 6, "addr")]
        [InlineData("addr1gx2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzer5pnz75xxcrzqf96k", "419493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8e8198bd431b03", 8, "addr")]
        [InlineData("addr128phkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gtupnz75xxcrtw79hu", "51c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f8198bd431b03", 10, "addr")]
        [InlineData("addr1vx2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzers66hrl8", "619493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8e", 12, "addr")]
        [InlineData("addr1w8phkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gtcyjy7wx", "71c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 14, "addr")]
        [InlineData("stake1uyehkck0lajq8gr28t9uxnuvgcqrc6070x3k9r8048z8y5gh6ffgw", "e1337b62cfff6403a06a3acbc34f8c46003c69fe79a3628cefa9c47251", 28, "stake")]
        [InlineData("stake178phkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gtcccycj5", "f1c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 30, "stake")]
        [InlineData("addr_test1qz2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzer3n0d3vllmyqwsx5wktcd8cc3sq835lu7drv2xwl2wywfgs68faae", "009493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8e337b62cfff6403a06a3acbc34f8c46003c69fe79a3628cefa9c47251", 0, "addr_test")]
        [InlineData("addr_test1zrphkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gten0d3vllmyqwsx5wktcd8cc3sq835lu7drv2xwl2wywfgsxj90mg", "10c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f337b62cfff6403a06a3acbc34f8c46003c69fe79a3628cefa9c47251", 2, "addr_test")]
        [InlineData("addr_test1yz2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzerkr0vd4msrxnuwnccdxlhdjar77j6lg0wypcc9uar5d2shsf5r8qx", "209493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8ec37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 4, "addr_test")]
        [InlineData("addr_test1xrphkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gt7r0vd4msrxnuwnccdxlhdjar77j6lg0wypcc9uar5d2shs4p04xh", "30c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542fc37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 6, "addr_test")]
        [InlineData("addr_test1gz2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzer5pnz75xxcrdw5vky", "409493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8e8198bd431b03", 8, "addr_test")]
        [InlineData("addr_test12rphkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gtupnz75xxcryqrvmw", "50c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f8198bd431b03", 10, "addr_test")]
        [InlineData("addr_test1vz2fxv2umyhttkxyxp8x0dlpdt3k6cwng5pxj3jhsydzerspjrlsz", "609493315cd92eb5d8c4304e67b7e16ae36d61d34502694657811a2c8e", 12, "addr_test")]
        [InlineData("addr_test1wrphkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gtcl6szpr", "70c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 14, "addr_test")]
        [InlineData("stake_test1uqehkck0lajq8gr28t9uxnuvgcqrc6070x3k9r8048z8y5gssrtvn", "e0337b62cfff6403a06a3acbc34f8c46003c69fe79a3628cefa9c47251", 28, "stake_test")]
        [InlineData("stake_test17rphkx6acpnf78fuvxn0mkew3l0fd058hzquvz7w36x4gtcljw6kf", "f0c37b1b5dc0669f1d3c61a6fddb2e8fde96be87b881c60bce8e8d542f", 30, "stake_test")]
        public void TestValidInputs(string addr, string expectedHex, byte expectedVer, string expectedHrp)
        {
            // Act
            var decoded = Bech32.Decode(addr, out var actualVer, out var actualHrp);
            var actualHex = decoded.ToStringHex();
            var isValid = Bech32.IsValid(addr);

            // Assert
            Assert.True(isValid);
            Assert.Equal(expectedVer, actualVer);
            Assert.Equal(expectedHrp, actualHrp);
            Assert.Equal(expectedHex, actualHex);
        }

        /// <summary>
        /// 
        /// https://github.com/bitcoin/bips/blob/master/bip-0173.mediawiki#test-vectors
        /// </summary>
        /// <param name="addr"></param>
        /// <param name="expectedError"></param>
        [Theory]
        [InlineData("1nwldj5", "HRP character out of range")]
        [InlineData("1axkwrx", "HRP character out of range")]
        [InlineData("1eym55h", "HRP character out of range")]
        //@TODO Fails [InlineData("an84characterslonghumanreadablepartthatcontainsthenumber1andtheexcludedcharactersbio1569pvx", "overall max length exceeded")]
        [InlineData("pzry9x0s0muk", "no separator")]
        [InlineData("1pzry9x0s0muk", "empty hrp")]
        [InlineData("x1b4n0q5v", "Invalid data format.")]
        [InlineData("li1dgmt3", "too short checksum")]
        [InlineData("de1lg7wt", "Invalid character in checksum.")]
        [InlineData("A1G7SGD8", "Invalid checksum.")]
        [InlineData("A1G7SGD8", "empty HRP")]
        [InlineData("1qzzfhee", "empty HRP")]
        public void TestInvalidInputs(string addr, string expectedError)
        {
            // arrange
            if(addr == "delig7wt")
            {
                addr += 0xff;
            }
            if (addr == "1nwldj5")
            {
                addr = 0x20 + addr;
            }
            if (addr == "1axkwrx")
            {
                addr = 0x7f + addr;
            }
            if (addr == "1eym55h")
            {
                addr = 0x80 + addr;
            }

            bool raised = false;
            try
            {
                var decoded = Bech32.Decode(addr, out var actualVer, out var actualHrp);
                var hex = decoded.ToStringHex();
            }
            catch (Exception ex)
            {
                raised = true;
                Console.Error.WriteLine(ex);
            }

            Assert.True(raised);
        }
    }
}
