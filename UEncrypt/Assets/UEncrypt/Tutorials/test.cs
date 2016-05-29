/**************************
 * 文件名:test.cs;
 * 文件描述:客户端生成公钥 私钥，公钥发给服务器，服务器用公钥加密数据，客户端用私钥解密数据
 * 创建日期:2016/05/29;
 * Author:ThisisGame;
 * Page:https://github.com/ThisisGame/UEncrypt.git
 ***************************/

using UnityEngine;
using System.Collections;
using System.Security.Cryptography;


namespace ThisisGame.UEncrypt
{
    public class test : MonoBehaviour
    {
        private string privateKey = string.Empty;
        private string publicKey = string.Empty;

        void Start()
        {
            string some = "ThisisGame";
            byte[] data = System.Text.Encoding.Default.GetBytes(some);

            Debug.Log("src : " + some);


            //1、客户端 -- 生成RSA私钥 公钥，并把公钥发给服务端
            UEncrypt.RSAGenerateKey(ref privateKey, ref publicKey);

            //2、服务端 -- 产生DES密钥
            byte[] serverDesKey = new byte[] { 1, 2, 3, 4, 8, 7, 6, 5 };

            //3、服务端 -- 用RSA公钥加密 DES密钥,并发给客户端
            byte[] serverRsaEncryptDesKey= UEncrypt.RSAEncrypt(serverDesKey, publicKey);

            //4、客户端 -- 用RSA私钥解密 DES密钥
            byte[] clientRsaDecryptDesKey = UEncrypt.RSADecrypt(serverRsaEncryptDesKey, privateKey);

            //5、客户端 -- 用 DES 加密网络包
            byte[] clientDesEncryptData = UEncrypt.DESEncrypt(data, clientRsaDecryptDesKey, clientRsaDecryptDesKey);

            //6、服务端 -- 用 DES 解密网络包
            byte[] serverDesDecryptData = UEncrypt.DESDecrypt(clientDesEncryptData, serverDesKey, serverDesKey);

            some = System.Text.Encoding.Default.GetString(serverDesDecryptData);

            Debug.Log("decrypt : " + some);
        }
    }
}

