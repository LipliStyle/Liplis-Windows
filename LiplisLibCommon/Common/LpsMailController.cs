//=======================================================================
//  ClassName : LiplisUtil
//  概要      : ユーティリティクラス
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System.Net.Mail;
using System.Collections.Generic;
using Liplis.Xml;

namespace Liplis.Common
{
    public static class LpsMailController
    {
        /// <summary>
        /// センドメール
        /// </summary>
        /// <param name="fromAddress">送信元アドレス</param>
        /// <param name="fromName">送信者名</param>
        /// <param name="toAddress">送信先アドレスリスト</param>
        /// <param name="subject">題名</param>
        /// <param name="body">本文</param>
        /// <param name="tempFilePathList">添付ファイルリスト</param>
        /// <param name="smtp">SMTPサーバー</param>
        /// <param name="domain">ドメイン</param>
        /// <param name="account">アカウント</param>
        /// <param name="pass">パスワード</param>
        /// <returns>結果</returns>
        public static bool sendMail(
            string fromAddress, 
            string fromName, 
            List<string> toAddress, 
            string subject, 
            string body, 
            List<Attachment>tempFilePathList,
            string smtp,
            string domain,
            string account,
            string pass
            )
        {
            MailMessage message = new MailMessage();

            SmtpClient client;

            try
            {
                //文字エンコード
                message.BodyEncoding = System.Text.Encoding.GetEncoding("iso-2022-jp");
                
                //HTMLメールかどうか
                message.IsBodyHtml = false;

                //メールプロパティ
                message.Priority = System.Net.Mail.MailPriority.Normal;

                //題名
                message.Subject = subject;

                //本文
                message.Body = body;

                //送信元アドレス
                message.From = new MailAddress(fromAddress, fromName);

                //送信先アドレス
                foreach(string address in toAddress)
                {
                    message.To.Add(address);
                }

                //ファイルを添付する。
                foreach(Attachment item in tempFilePathList)
                {
                    message.Attachments.Add(item);
                }
                
                //SMTPサーバを指定する。
                client = new SmtpClient(smtp);

                //SMTP認証情報を設定する。(認証が必要な場合のみ)
                client.UseDefaultCredentials = false;

                //タイムアウト
                client.Timeout = 20000;

                System.Net.NetworkCredential cred = new System.Net.NetworkCredential();
                cred.Domain = domain;
                cred.UserName = account;
                cred.Password = pass;
                client.Credentials = cred;

                //メールを送信する。
                client.Send(message);

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                //アタッチメントの開放
                foreach(Attachment item in tempFilePathList)
                {
                    item.Dispose();
                }
                tempFilePathList.Clear();

                //メッセージの開放
                message.Dispose();
            }
        }

        public static bool sendMail(
            string fromAddress,
            string fromName,
            string toAddress,
            string subject,
            string body,
            string smtp,
            string domain,
            string account,
            string pass
            )
        {
            List<string> toAddressList = new List<string>();
            List<Attachment> tempFilePathList = new List<Attachment>();
            toAddressList.Add(toAddress);
            return sendMail(fromAddress, fromName, toAddressList, subject, body, tempFilePathList,smtp,domain,account,pass);
        }
        public static bool sendMail(string settingFile, string subject,string body)
        {
            mailSetting s = new mailSetting(settingFile);
            List<string> toAddressList = new List<string>();
            List<Attachment> tempFilePathList = new List<Attachment>();
            toAddressList = s.toAddress;
            return sendMail(s.fromAddress, s.fromName, toAddressList, subject, body, tempFilePathList, s.smtp, s.domain, s.account, s.pass);
        }



    }
}
