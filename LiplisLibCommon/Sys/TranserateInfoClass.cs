//=======================================================================
//  ClassName : TranserateInfoClass
//  概要      : 転送情報を取得する
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================
using System.Net.NetworkInformation;

namespace Liplis.Sys
{

    public class TranserateInfoClass
    {
        ///=============================
        ///ネットワークインターフェース
        NetworkInterface[] nis;

        ///=============================
        ///送受信パケット情報
        long receiveByte;
        long sentByte;
        long prvReceiveByte;
        long prvSentByte;

        ///=============================
        ///総送信受信パケット情報
        long tensoSentByte;
        long startSentByte;

        //string receiveNum;
        //string sentNum;

        ///=============================
        ///表示ネットワーク情報
        int interFaseNum = 0;

        ///=============================
        ///カウンタ
        int i = 0;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public TranserateInfoClass(int interFaseNum)
        {
            //インターフェイスナンバーのセット
            this.interFaseNum = interFaseNum;

            //ネットワークインターフェース
            nis = NetworkInterface.GetAllNetworkInterfaces();
        }

        /// <summary>
        /// 転送量を取得する。
        /// </summary>
        public void getNetWorkIntaerfaseInfo()
        {
            i = 0;
            foreach (NetworkInterface ni in nis)
            {
                if (i == interFaseNum)
                {
                    //ネットワーク接続しているか調べる
                    if (ni.OperationalStatus == OperationalStatus.Up &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    {
                        //IPv4の統計情報を表示する
                        if (ni.Supports(NetworkInterfaceComponent.IPv4))
                        {
                            IPv4InterfaceStatistics ipv4 = ni.GetIPv4Statistics();

                            //速度の表示
                            receiveByte = (ipv4.BytesReceived - prvReceiveByte)/1000;
                            sentByte = (ipv4.BytesSent - prvSentByte)/1000;
                            tensoSentByte = (ipv4.BytesSent - startSentByte) / 1000000;

                            //前回値の設定
                            prvReceiveByte = ipv4.BytesReceived;
                            prvSentByte = ipv4.BytesSent;

                            //lblReceiveNum.Text = ipv4.UnicastPacketsReceived.ToString();
                            //lblSentNum.Text = ipv4.UnicastPacketsSent.ToString();
                        }
                    }
                }
                i++;
            }
        }

        /// <summary>
        /// 転送量を取得する。
        /// </summary>
        public void getNetWorkIntaerfaseInfo2()
        {
            i = 0;
            foreach (NetworkInterface ni in nis)
            {
                if (i == interFaseNum)
                {
                    //ネットワーク接続しているか調べる
                    if (ni.OperationalStatus == OperationalStatus.Up &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    {
                        //IPv4の統計情報を表示する
                        if (ni.Supports(NetworkInterfaceComponent.IPv4))
                        {
                            IPv4InterfaceStatistics ipv4 = ni.GetIPv4Statistics();

                            //速度の表示
                            receiveByte = (ipv4.BytesReceived - prvReceiveByte) / 1000;
                            sentByte = (ipv4.BytesSent - prvSentByte) / 1000;

                            //前回値の設定
                            prvReceiveByte = ipv4.BytesReceived;
                            prvSentByte = ipv4.BytesSent;

                            //lblReceiveNum.Text = ipv4.UnicastPacketsReceived.ToString();
                            //lblSentNum.Text = ipv4.UnicastPacketsSent.ToString();
                        }
                    }
                }
                i++;
            }
        }

        /// <summary>
        /// レシーブバイトを返す
        /// </summary>
        /// <returns></returns>
        public long getReceiveByte()
        {
            return receiveByte;
        }

        /// <summary>
        /// セントバイトを返す
        /// </summary>
        /// <returns></returns>
        public long getSentByte()
        {
            return sentByte;
        }

        /// <summary>
        /// スタートバイトをセットする
        /// </summary>
        public void setStartSentByte()
        {
            i = 0;
            foreach (NetworkInterface ni in nis)
            {
                if (i == interFaseNum)
                {
                    //ネットワーク接続しているか調べる
                    if (ni.OperationalStatus == OperationalStatus.Up &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        ni.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    {
                        //IPv4の統計情報を表示する
                        if (ni.Supports(NetworkInterfaceComponent.IPv4))
                        {
                            IPv4InterfaceStatistics ipv4 = ni.GetIPv4Statistics();
                            startSentByte = ipv4.BytesSent;
                        }
                    }
                }
                i++;
            }
        }

        /// <summary>
        /// スタートバイトをクリアする
        /// </summary>
        public void crearStartSentByte()
        {
            startSentByte = 0;
        }

        /// <summary>
        /// スタートバイトを返す
        /// </summary>
        public long getTensoSentByte()
        {
            if (startSentByte < 1)
            {
                return 0;
            }
            else
            {
                return tensoSentByte;
            }
        }

        /// <summary>
        /// ネットワークインターフェースを取得する
        /// </summary>
        //private void getNetworkInterfase()
        //{
        //    nis = NetworkInterface.GetAllNetworkInterfaces();
        //}
    }
}
