using System;
using Jingtum.API.Core.Crypto.Ecdsa;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jingtum.API.Test
{
    [TestClass]
    public class Test_Wallet_Account
    {
        [TestMethod]
        public void TestWallet()
        {
            var secret = Seed.GenerateSecret();
            var address = Seed.ComputeAddress(secret);
            var wallet = new Wallet(address, secret);

            Assert.AreEqual("j", (wallet.Address).Substring(0, 1));
            Assert.AreEqual("s", (wallet.Secret).Substring(0, 1));
        }

        [TestMethod]
        public void TestParametersWallet()
        {
            //create wallet normal
            Wallet wallet = new Wallet("js4UaG1pjyCEi9f867QHJbWwD3eo6C5xsa", "snqFcHzRe22JTM8j7iZVpQYzxEEbW");
            Assert.AreEqual("js4UaG1pjyCEi9f867QHJbWwD3eo6C5xsa", wallet.Address);
            Assert.AreEqual("snqFcHzRe22JTM8j7iZVpQYzxEEbW", wallet.Secret);
        }

        [TestMethod]
        [DataRow("", "snqFcHzRe22JTM8j7iZVpQYzxEEbW", DisplayName = "address is empty")]
        [DataRow("js4UaG1pjyCEi9f867QHJbWwD3eo6C5xsa", "", DisplayName = "secret is empty")]
        [DataRow("", "", DisplayName = "both secret and address are empty")]
        [DataRow("js4UaG1pjyCEi9f867QHJbWwD3eo6C5xsa", null, DisplayName = "secret is null")]
        [DataRow(null, "snqFcHzRe22JTM8j7iZVpQYzxEEbW", DisplayName = "address is null")]
        [DataRow(null, null, DisplayName = "both secret and address are null")]
        [DataRow("js4UaG1pjyCEi9f867QHJbWwD3eo6C5xsa", "snwjtucx9vEP7hCazriMbVz8hFiK9", DisplayName = "secret and address not match")]
        [DataRow("111ssssssss", "snqFcHzRe22JTM8j7iZVpQYzxEEbW", DisplayName = "address is invalid")]
        [DataRow("js4UaG1pjyCEi9f867QHJbWwD3eo6C5xsa", "aaaaaa1111", DisplayName = "secret is invalid")]
        [DataRow("***aaa1111", "@@@@bbbb2222ssssssssssssssssssssssssss001", DisplayName = "secret and address are both invalid")]
        public void TestParametersWalletWithException(string address, string secret)
        {
            Wallet wallet = null;
            try
            {
                wallet = new Wallet(address, secret);
            }
            catch (InvalidParameterException ex)
            {
                // has exception
                Assert.AreEqual(JingtumMessage.INVALID_JINGTUM_ADDRESS_OR_SECRET, ex.Message);
            }
            // no instance created
            Assert.IsNull(wallet);
        }

        [TestMethod]
        public void TestAddressWallet()
        {
            //create wallet with valid address
            Wallet wallet = new Wallet("jfCiWtSt4juFbS3NaXvYV9xNYxakm5yP9S");
            Assert.AreEqual("jfCiWtSt4juFbS3NaXvYV9xNYxakm5yP9S", wallet.Address);
        }

        [TestMethod]
        [DataRow("", DisplayName = "address is empty")]
        [DataRow(null, DisplayName = "address is null")]
        [DataRow("1111111111111ssssssssaaaaa22222", DisplayName = "invalid address")]
        public void TestAddressWalletWithException(string address)
        {
            Wallet wallet = null;
            try
            {
                wallet = new Wallet(address);
            }
            catch (InvalidParameterException ex)
            {
                // has exception
                Assert.AreEqual(JingtumMessage.INVALID_JINGTUM_ADDRESS, ex.Message);
            }
            // no instance created
            Assert.IsNull(wallet);
        }
    }
}