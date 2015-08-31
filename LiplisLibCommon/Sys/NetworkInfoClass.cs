

using System.Net.NetworkInformation;
using Liplis.Common;
using System.Collections.Generic;

namespace Liplis.Sys
{
    public class NetworkInfoClass
    {

        /// <summary>
        /// コンストラクター
        /// インスタンス化と同時にコンソールに情報をはき出す
        /// データを持って、表示するようにすることもできるが・・・
        /// </summary>
        #region NetworkInfoClass
        public NetworkInfoClass()
        {
        }
        #endregion


        /// <summary>
        /// getTargetNicMaxSpeed
        /// 対象のNICの最大通信速度を取得する
        /// </summary>
        /// <param name="interfaseNum"></param>
        /// <returns></returns>
        #region getTargetNicMaxSpeed
        public long getTargetNicMaxSpeed(int interfaseNum)
        {
            long result = 0;
            int i = 0;
            //すべてのネットワークインターフェイスを取得する
            NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in nis)
            {
                if (interfaseNum == i)
                {
                    //最大速度を取得する。
                    result = (ni.Speed / 1000);
                }
                i++;
            }
            return result;
        }
        #endregion

        /// <summary>
        /// getNicList
        /// リストをインデックスとのペアで返す
        /// </summary>
        /// <param name="interfaseNum"></param>
        /// <returns></returns>
        #region getNicList
        public Dictionary<int,string> getNicList()
        {
            //すべてのネットワークインターフェイスを取得する
            NetworkInterface[] nis = NetworkInterface.GetAllNetworkInterfaces();
            Dictionary<int, string> res = new Dictionary<int, string>(nis.Length);
            int idx = 0;

            foreach (NetworkInterface ni in nis)
            {
                res.Add(idx, ni.Name);
                idx++;
            }
            return res;
        }
        #endregion

        /// <summary>
        /// checkInterNetConnection
        /// インターネット接続されているか確認する
        /// </summary>
        /// <returns></returns>
        #region checkInterNetConnection
        public bool checkInterNetConnection()
        {
            int flags;
            if (LpsWindowsApi.InternetGetConnectedState(out flags, 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion


    }
}
