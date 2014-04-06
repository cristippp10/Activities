﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Renci.SshNet.Security.Cryptography.Ciphers;
using Renci.SshNet.Tests.Common;

namespace Renci.SshNet.Tests.Classes.Security.Cryptography.Ciphers
{
    /// <summary>
    /// Implements DES cipher algorithm.
    /// </summary>
    [TestClass]
    public class DesCipherTest : TestBase
    {
        /// <summary>
        ///A test for DesCipher Constructor
        ///</summary>
        [TestMethod()]
        public void DesCipherConstructorTest()
        {
            byte[] key = null; // TODO: Initialize to an appropriate value
            CipherMode mode = null; // TODO: Initialize to an appropriate value
            CipherPadding padding = null; // TODO: Initialize to an appropriate value
            DesCipher target = new DesCipher(key, mode, padding);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for DecryptBlock
        ///</summary>
        [TestMethod()]
        public void DecryptBlockTest()
        {
            byte[] key = null; // TODO: Initialize to an appropriate value
            CipherMode mode = null; // TODO: Initialize to an appropriate value
            CipherPadding padding = null; // TODO: Initialize to an appropriate value
            DesCipher target = new DesCipher(key, mode, padding); // TODO: Initialize to an appropriate value
            byte[] inputBuffer = null; // TODO: Initialize to an appropriate value
            int inputOffset = 0; // TODO: Initialize to an appropriate value
            int inputCount = 0; // TODO: Initialize to an appropriate value
            byte[] outputBuffer = null; // TODO: Initialize to an appropriate value
            int outputOffset = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DecryptBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EncryptBlock
        ///</summary>
        [TestMethod()]
        public void EncryptBlockTest()
        {
            byte[] key = null; // TODO: Initialize to an appropriate value
            CipherMode mode = null; // TODO: Initialize to an appropriate value
            CipherPadding padding = null; // TODO: Initialize to an appropriate value
            DesCipher target = new DesCipher(key, mode, padding); // TODO: Initialize to an appropriate value
            byte[] inputBuffer = null; // TODO: Initialize to an appropriate value
            int inputOffset = 0; // TODO: Initialize to an appropriate value
            int inputCount = 0; // TODO: Initialize to an appropriate value
            byte[] outputBuffer = null; // TODO: Initialize to an appropriate value
            int outputOffset = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.EncryptBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}