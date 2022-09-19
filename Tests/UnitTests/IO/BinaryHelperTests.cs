﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Enigma.IO;
using NUnit.Framework;

namespace CryptoToolkitUnitTests.IO
{
    public class BinaryHelperTests
    {
        [Test]
        public void Read()
        {
            using (FileStream fs = StreamHelper.GetFileStreamOpen(@"data\IO\binaryhelper.dat"))
            {
                Assert.Multiple(() =>
                {
                    byte b = BinaryHelper.ReadByte(fs);
                    Assert.AreEqual((byte)0xfe, b);
                    bool bo = BinaryHelper.ReadBool(fs);
                    Assert.AreEqual(true, bo);
                    Int16 i16 = BinaryHelper.ReadInt16(fs);
                    Assert.AreEqual(-12, i16);
                    UInt16 ui16 = BinaryHelper.ReadUInt16(fs);
                    Assert.AreEqual(12, ui16);
                    Int32 i32 = BinaryHelper.ReadInt32(fs);
                    Assert.AreEqual(-120, i32);
                    UInt32 ui32 = BinaryHelper.ReadUInt32(fs);
                    Assert.AreEqual(120, ui32);
                    Int64 i64 = BinaryHelper.ReadInt64(fs);
                    Assert.AreEqual(-1200, i64);
                    UInt64 ui64 = BinaryHelper.ReadUInt64(fs);
                    Assert.AreEqual(1200, ui64);
                    float fl = BinaryHelper.ReadFloat(fs);
                    Assert.AreEqual(12.0f, fl);
                    double db = BinaryHelper.ReadDouble(fs);
                    Assert.AreEqual(120.0, db);
                });
            }
        }

        [Test]
        public void ReadAsync()
        {
            using (FileStream fs = StreamHelper.GetFileStreamOpen(@"data\IO\binaryhelper.dat"))
            {
                Assert.Multiple(async () =>
                {
                    byte b = await BinaryHelper.ReadByteAsync(fs).ConfigureAwait(false);
                    Assert.AreEqual((byte)0xfe, b);
                    bool bo = await BinaryHelper.ReadBoolAsync(fs).ConfigureAwait(false);
                    Assert.AreEqual(true, bo);
                    Int16 i16 = await BinaryHelper.ReadInt16Async(fs).ConfigureAwait(false);
                    Assert.AreEqual(-12, i16);
                    UInt16 ui16 = await BinaryHelper.ReadUInt16Async(fs).ConfigureAwait(false);
                    Assert.AreEqual(12, ui16);
                    Int32 i32 = await BinaryHelper.ReadInt32Async(fs).ConfigureAwait(false);
                    Assert.AreEqual(-120, i32);
                    UInt32 ui32 = await BinaryHelper.ReadUInt32Async(fs).ConfigureAwait(false);
                    Assert.AreEqual(120, ui32);
                    Int64 i64 = await BinaryHelper.ReadInt64Async(fs).ConfigureAwait(false);
                    Assert.AreEqual(-1200, i64);
                    UInt64 ui64 = await BinaryHelper.ReadUInt64Async(fs).ConfigureAwait(false);
                    Assert.AreEqual(1200, ui64);
                    float fl = await BinaryHelper.ReadFloatAsync(fs).ConfigureAwait(false);
                    Assert.AreEqual(12.0f, fl);
                    double db = await BinaryHelper.ReadDoubleAsync(fs).ConfigureAwait(false);
                    Assert.AreEqual(120.0, db);
                });
            }
        }

        [Test]
        public void Write()
        {
            byte[] fileData;
            using (FileStream fs = StreamHelper.GetFileStreamOpen(@"data\IO\binaryhelper.dat"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    StreamHelper.WriteStream(fs, ms);
                    fileData = ms.ToArray();
                }
            }

            byte[] generated;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryHelper.Write(ms, (byte)0xfe);
                BinaryHelper.Write(ms, true);
                BinaryHelper.Write(ms, (Int16)(-12));
                BinaryHelper.Write(ms, (UInt16)(12));
                BinaryHelper.Write(ms, (Int32)(-120));
                BinaryHelper.Write(ms, (UInt32)(120));
                BinaryHelper.Write(ms, (Int64)(-1200));
                BinaryHelper.Write(ms, (UInt64)(1200));
                BinaryHelper.Write(ms, (float)(12.0f));
                BinaryHelper.Write(ms, (double)(120.0));
                generated = ms.ToArray();
            }

            Assert.AreEqual(fileData, generated);
        }

        [Test]
        public async Task WriteAsync()
        {
            byte[] fileData;
            using (FileStream fs = StreamHelper.GetFileStreamOpen(@"data\IO\binaryhelper.dat"))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await StreamHelper.WriteStreamAsync(fs, ms).ConfigureAwait(false);
                    fileData = ms.ToArray();
                }
            }

            byte[] generated;
            using (MemoryStream ms = new MemoryStream())
            {
                await BinaryHelper.WriteAsync(ms, (byte)0xfe).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, true).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (Int16)(-12)).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (UInt16)(12)).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (Int32)(-120)).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (UInt32)(120)).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (Int64)(-1200)).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (UInt64)(1200)).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (float)(12.0f)).ConfigureAwait(false);
                await BinaryHelper.WriteAsync(ms, (double)(120.0)).ConfigureAwait(false);
                generated = ms.ToArray();
            }

            Assert.AreEqual(fileData, generated);
        }
    }
}
