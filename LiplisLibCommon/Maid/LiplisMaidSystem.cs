//=======================================================================
//  ClassName : LiplisMaidSystem
//  概要      : リプリスメイドシステム
//
//  Liplisシステム      
//  Copyright(c) 2010-2012 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Liplis.Msg;

namespace Liplis.Maid
{
    public class LiplisMaidSystem
    {
        /// <summary>
        /// お料理
        /// バイトコードに変換したパスワードを投入する
        /// メイドオブジェクトを取得する
        /// </summary>
        /// <param name="otama">パスワード(バイトコード)</param>
        /// <returns>メイドオブジェクト</returns>
        #region cooking
        public msgMaid cooking(byte[] otama)
        {
            //メイドオブジェクト
            msgMaid m = new msgMaid();

            //DESC
            TripleDESCryptoServiceProvider frill = new TripleDESCryptoServiceProvider();

            // Triple DES のサービス プロバイダを生成します 
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            //取得
            m.houshinokokoro = frill.Key;
            m.zettaifukujyu = frill.IV;

            // source 配列から cryptData 配列へ変換 
            // 文字列を byte 配列に変換します 
            //byte[] source = Encoding.Unicode.GetBytes(cachusha);

            // 入出力用のストリームを生成します 
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(m.houshinokokoro, m.zettaifukujyu), CryptoStreamMode.Write))
                {
                    // ストリームに暗号化するデータを書き込みます 
                    cs.Write(otama, 0, otama.Length);
                }

                // 暗号化されたデータを byte 配列で取得します 
                m.zenryokushugo = ms.ToArray();
            }

            // byte 配列を文字列に変換して表示します 
            return m;
        }
        #endregion


        /// <summary>
        /// テーブルセッティング
        /// メイドオブジェクトの戻り値とパスワードを照合する
        /// </summary>
        /// <param name="houshinokokoro">key</param>
        /// <param name="zettaifukujyu">code</param>
        /// <param name="zenryokushugo">word</param>
        /// <param name="atafuta">wordRaw</param>
        /// <returns></returns>
        #region tableSetting
        public bool tableSetting(byte[] houshinokokoro, byte[] zettaifukujyu, byte[] zenryokushugo, byte[] atafuta)
        {
            try
            {
                // cryptData 配列から destination 配列へ変換 
                byte[] destination;

                // Triple DES のサービス プロバイダを生成します 
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

                // 入出力用のストリームを生成します 
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(houshinokokoro, zettaifukujyu), CryptoStreamMode.Write))
                    {
                        // ストリームに暗号化されたデータを書き込みます 
                        cs.Write(zenryokushugo, 0, zenryokushugo.Length);
                    }

                    // 復号化されたデータを byte 配列で取得します 
                    destination = ms.ToArray();
                }

                // byte 配列を文字列に変換して表示します 
                //textBox3.Text = Encoding.Unicode.GetString(destination); 


                //ハッシュの計算
                byte[] hashuDestination = new MD5CryptoServiceProvider().ComputeHash(destination);
                byte[] hashuAatafuta = new MD5CryptoServiceProvider().ComputeHash(atafuta);

                if (hashuDestination.Length == hashuAatafuta.Length)
                {
                    int i = 0;
                    while ((i < hashuDestination.Length) && (hashuDestination[i] == hashuAatafuta[i]))
                    {
                        i += 1;
                    }
                    if (i == hashuDestination.Length)
                    {
                        return true;
                    }
                }

                //照合
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
        

        /// <summary>
        /// メイドメッセージ
        /// </summary>
        #region msgMaid
        public class msgMaid
        {
            public byte[] houshinokokoro { get; set; }
            public byte[] zettaifukujyu { get; set; }
            public byte[] zenryokushugo { get; set; }
        }
        #endregion
        

    }
}
