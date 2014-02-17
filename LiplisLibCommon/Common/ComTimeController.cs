using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;  // for DllImport

namespace Liplis.Common
{
    public class ComTimeController
    {
        //=====================================
        //パフォーマンスカウンタ仕様宣言
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceCounter(ref long x);
        [DllImport("kernel32.dll")]
        extern static short QueryPerformanceFrequency(ref long x);

        //=====================================
        //時間制御
        private long time1 = 0;
        private long time2 = 0;
        private long freq = 0;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public ComTimeController()
        {

        }

        /// <summary>
        /// タイマースタート
        /// </summary>
        public void start()
        {
            QueryPerformanceCounter(ref time1);   // 計測開始！
        }

        /// <summary>
        /// タイマー終了
        /// </summary>
        public void stop()
        {
            QueryPerformanceCounter(ref time2);   // 計測終了！
        }

        /// <summary>
        /// 結果を返す。
        /// </summary>
        /// <returns></returns>
        public double getResult()
        {
            QueryPerformanceFrequency(ref freq);
            return (1000000 * (time2 - time1) / freq);
        }

    }
}
