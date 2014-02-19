//=======================================================================
//  ClassName : SqlUtil
//  概要      : SQLユーテリティ
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin.Sachin
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Sql
{
    public static class SqlUtil
    {
        /// <summary>
        /// SQLの引数として成形する
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        #region convSql
        public static string convSql(string str)
        {
            string result = str;

            //シングルコーテーションのエスケープ
            result = result.Replace("'", "''");

            return " '" + result + "' ";
        }
        public static string convSql(int str)
        {
            return " " + str.ToString() + " ";
        }
        public static string convSql(DateTime d)
        {
            return " '" + d.ToString("yyyy/MM/dd HH:mm:ss") + "' ";
        }
        #endregion

        /// <summary>
        /// 対象時刻からX時間前の時刻を指定するSQL文を得る
        /// </summary>
        /// <param name="d"></param>
        /// <param name="deray"></param>
        /// <returns></returns>
        public static string convSqlBeforTime(DateTime d, int delay)
        {
            return " '" + d.AddHours(-1 * delay).ToString("yyyy/MM/dd HH:mm:ss") + "' ";
        }


    }
}
