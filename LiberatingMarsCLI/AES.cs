using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LiberatingMarsCLI
{
    class AES
    {
        public static byte[] AESKey = new byte[32];
        public static byte[] AESIV = new byte[16];
        public static byte[] AesCryptBytes(byte[] data, bool encrypt)
        {
            PullCrypto crypto = new PullCrypto();

            AESKey = crypto.AESKey;
            AESIV = crypto.AESIV;

            if (data.Length % 0x10 != 0)
            {
                byte[] temp = new byte[((data.Length / 0x10) + 1) * 0x10];
                Array.Copy(data, 0, temp, 0, data.Length);
                data = temp;
            }

            AesManaged aes = new AesManaged();

            aes.KeySize = 256;
            aes.Key = AESKey;
            aes.Padding = PaddingMode.None;
            aes.Mode = CipherMode.CBC;
            aes.IV = AESIV;
            ICryptoTransform cryptor = encrypt ? aes.CreateEncryptor() : aes.CreateDecryptor();

            byte[] outputBuffer = null;
            using (MemoryStream msDecrypt = new MemoryStream(data))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, cryptor, CryptoStreamMode.Read))
                {
                    outputBuffer = new byte[data.Length];
                    csDecrypt.Read(outputBuffer, 0, data.Length);

                }
            }

            return outputBuffer;
        }
    }
}
