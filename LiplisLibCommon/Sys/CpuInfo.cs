//=======================================================================
//  ClassName : CpuInfo
//  概要      : CPU情報を取得する
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================
using System;

namespace Liplis.Sys
{
    public class CpuInfo
    {
        ///=============================
        ///PC名、カウンタ名、カテゴリ名、インスタンス名
        string machineName = ".";                   //コンピュータ名
        string categoryName = "Processor";          //カテゴリ名
        string counterName = "% Processor Time";    //カウンタ名
        string instanceName = "_Total";             //インスタンス名
        System.Diagnostics.PerformanceCounter pc;

        /// <summary>
        /// コンストラクター
        /// </summary>
        public CpuInfo()
        {
            //カテゴリが存在するか確かめる
            if (!System.Diagnostics.PerformanceCounterCategory.Exists(
                categoryName, machineName))
            {
                Console.WriteLine("登録されていないカテゴリです。");
                return;
            }

            //カウンタが存在するか確かめる
            if (!System.Diagnostics.PerformanceCounterCategory.CounterExists(
                counterName, categoryName, machineName))
            {
                Console.WriteLine("登録されていないカウンタです。");
                return;
            }

            //PerformanceCounterオブジェクトの作成
            pc = new System.Diagnostics.PerformanceCounter(
                categoryName, counterName, instanceName, machineName);
            
        }

        /// <summary>
        /// getCpuRate
        /// CPU使用率取得
        /// </summary>
        /// <returns></returns>
        public double getCpuRate()
        {
            double result = 0.0f;
            result = (int)(pc.NextValue()*100);
            return  result / 100;
        }
    }
}
