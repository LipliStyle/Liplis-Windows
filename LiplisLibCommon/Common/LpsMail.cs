//=======================================================================
//  ClassName : LpsMail
//  概要      : メールAPI
//
//  SatelliteServer
//  Copyright(c) 2009-2013 sachin.Sachin
//=======================================================================
using System;
using System.Configuration;
using System.Net.Mail;
using System.Collections.Generic;


namespace Liplis.Common
{
    public static class LpsMail
    {

        /// <summary>
        /// メール送信
        /// (サーバー設定を使用しない)
        /// </summary>
        /// <param name="fromAddress">送信元アドレス</param>
        /// <param name="name">名前</param>
        /// <param name="message">内容</param>
        /// <param name="toAddress">送信先アドレス</param>
        /// <param name="smtpSrv">送信用SMTPサーバー</param>
        /// <returns></returns>
        #region sendMail
        public static bool sendMail(string fromAddress, string toAddress, string name, string title, string message, string smtpSrv)
        {
            try
            {
                MailAddress addrFrom = new MailAddress(fromAddress, name);
                MailAddress addrTo = new MailAddress(toAddress);
                MailMessage msg = new MailMessage(addrFrom, addrTo);

                msg.Subject = title;
                msg.Body = message;

                SmtpClient client = new SmtpClient(smtpSrv);
                client.Send(msg);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool sendMail(string fromAddress, List<string> toAddress, string name, string title, string message, string smtpSrv)
        {
            try
            {
                foreach(string address in toAddress)
                {
                    sendMail(fromAddress, address, name, title, message, smtpSrv);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion


    }
}
