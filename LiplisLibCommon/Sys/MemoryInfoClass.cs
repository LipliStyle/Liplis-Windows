//=======================================================================
//  ClassName : MemoryInfoClass
//  概要      : メモリ情報を取得する
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//　※要System.Management
//=======================================================================
using System;

namespace Liplis.Sys
{
    public class MemoryInfoClass
    {
        ///=============================
        ///メモリ取得用クラス
        ///System.Management.dllを参照設定する必要あり。
        System.Management.ManagementClass mc = new System.Management.ManagementClass("Win32_OperatingSystem");

        ///=============================
        ///メモリ変数
        private double physicalAll;
        private double physicalUseabel;
        private double virtualAll;
        private double virtualUseable;


        /// <summary>
        /// コンストラクター
        /// </summary>
        public MemoryInfoClass()
        {
        }


        public void getMemoryInfo()
        {
            //マネジメントオブジェクトコレクション
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //合計物理メモリ
                physicalAll = Convert.ToDouble(mo["TotalVisibleMemorySize"]);
                //利用可能な物理メモリ
                physicalUseabel = Convert.ToDouble(mo["FreePhysicalMemory"]);
                //合計仮想メモリ
                virtualAll = Convert.ToDouble(mo["TotalVirtualMemorySize"]);
                //利用可能な仮想メモリ
                virtualUseable = Convert.ToDouble(mo["FreeVirtualMemory"]);
            }
        }

        /// <summary>
        /// 物理合計メモリを取得する
        /// </summary>
        /// <returns></returns>
        public double getPhysicalAll()
        {
            double result = (int)(physicalAll)/10000;
            return (result / 100);
        }

        /// <summary>
        /// 物理仕様可能メモリを取得する
        /// </summary>
        /// <returns></returns>
        public double getPhysicalUseabel()
        {
            double result = (int)(physicalAll - physicalUseabel) / 10000;
            return (result / 100);
        }


        /// <summary>
        /// 物理メモリー使用率を返す
        /// </summary>
        /// <returns></returns>
        public double getPhysicalRate()
        {
            return ((physicalAll - physicalUseabel) / physicalAll) * 100;
        }


        /// <summary>
        /// 物理仕様可能メモリの生データ取得する
        /// </summary>
        /// <returns></returns>
        public double getPhysicalUseabelNonFix()
        {
            return physicalUseabel;
        }
    }
}
