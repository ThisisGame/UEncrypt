/**************************
 * 文件名:UEncrypt.cs;
 * 文件描述:RSA加密 解密;
 * 创建日期:2016/05/29;
 * Author:ThisisGame;
 * Page:https://github.com/ThisisGame/UEncrypt.git
 ***************************/


using System.IO;
using System.Text;
using System.Security.Cryptography;


namespace ThisisGame.UEncrypt
{
    public class UEncrypt
    {
        /// <summary>
        /// 生成RSA私钥 公钥
        /// </summary>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        public static void RSAGenerateKey(ref string privateKey, ref string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            privateKey = rsa.ToXmlString(true);
            publicKey = rsa.ToXmlString(false);
        }

        /// <summary>
        /// 用RSA公钥 加密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="publicKey"></param>
        /// <returns></returns>
        public static byte[] RSAEncrypt(byte[] data, string publicKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);

            byte[] encryptData = rsa.Encrypt(data, false);

            return encryptData;
        }


        /// <summary>
        /// 用RSA私钥 解密
        /// </summary>
        /// <param name="data"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static byte[] RSADecrypt(byte[] data, string privateKey)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            byte[] decryptData = rsa.Decrypt(data, false);
            return decryptData;
        }

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="desrgbKey"></param>
        /// <param name="desrgbIV"></param>
        /// <returns></returns>
        public static byte[] DESEncrypt(byte[] data,byte[] desrgbKey,byte[] desrgbIV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(desrgbKey, desrgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="desrgbKey"></param>
        /// <param name="desrgbIV"></param>
        /// <returns></returns>
        public static byte[] DESDecrypt(byte[] data, byte[] desrgbKey, byte[] desrgbIV)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(desrgbKey, desrgbIV), CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();
            return memoryStream.ToArray();
        }
    }

}
