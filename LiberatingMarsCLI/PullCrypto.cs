using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiberatingMarsCLI
{
    class PullCrypto
    {
        Program prog = new Program();

        public byte[] AESKey = new byte[32];
        public byte[] AESIV = new byte[16];

        static Dictionary<string, (long, long, byte[])> ChituRecovery = new()
        {
            { "zGoUXYV2LnzeIAIYK3xzxQ==", (0x4b5290, 0x11A22E0, new byte[] { 0x02, 0x2b, 0xbf, 0x87, 0x0e, 0x5b, 0xaf, 0x46, 0x54, 0x5c, 0xb6, 0xac, 0xb8, 0x0b, 0xc1, 0x69 }) },
            { "eUgbW9/VLyYl8jvn1yruLw==", (0x76c6b0, 0x11A22E0, new byte[] { 0x02, 0x2b, 0xbf, 0x87, 0x0e, 0x5b, 0xaf, 0x46, 0x54, 0x5c, 0xb6, 0xac, 0xb8, 0x0b, 0xc1, 0x69 }) }
        };

        public void pullCrypto(String chituLocation)
        {
            Console.WriteLine(chituLocation);

            var chituHash = Convert.ToBase64String(MD5.HashData(File.ReadAllBytes(chituLocation)));
            var keyOffset = ChituRecovery[chituHash].Item1;
            var ivOffset = ChituRecovery[chituHash].Item2;
            var ivMask = ChituRecovery[chituHash].Item3;

            FileStream chituStream = File.OpenRead(chituLocation);
            chituStream.Seek(keyOffset, SeekOrigin.Begin);
            chituStream.Read(AESKey, 0, 0x20);

            chituStream.Seek(ivOffset, SeekOrigin.Begin);
            chituStream.Read(AESIV, 0, 0x10);
            chituStream.Close();
            for (var x = 0; x < AESIV.Length; x++)
            {
                AESIV[x] = (byte)(AESIV[x] ^ ivMask[x]);
            }
        }
    }
}
