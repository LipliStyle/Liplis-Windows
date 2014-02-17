//=======================================================================
//  ClassName : LpsSqlServerBalkCopy
//  概要      : SQLServerのバルクコピーを実行する。
//
//  SatelliteServer
//  Copyright(c) 2009-2012 sachin. All Rights Reserved. 
//=======================================================================

namespace Liplis.Common
{
    public static class LpsSqlServerBalkCopy
    {
        /// <summary>
        /// runningBalkCopy
        /// バルクコピーを実行する。
        /// </summary>
        /// <param name="dbName">データベース名</param>
        /// <param name="path">パス</param>
        #region runningBalkCopy
        public static void runningBalkCopy(string dbName, string path, BCPDirection direction, string option, string remoteDb)
        {
            //ディレクション
            string strDirection = "in";
            if (direction == BCPDirection.BCP_OUT)
            {
                strDirection = "out";
            }
            else 
            {
                strDirection = "in";
            }

            //リモートDB
            string strRemoteDb = "";
            if (remoteDb != "")
            {
                strRemoteDb = " -S " + remoteDb;
            }

            using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
            {
                proc.StartInfo.FileName = "bcp";
                proc.StartInfo.Arguments = dbName + " " + strDirection + " \"" + path + "\" " + option + strRemoteDb;
                proc.Start();
                proc.WaitForExit();
            }
            
        }
        #endregion
    }

    public enum BCPDirection
    {
        BCP_IN = 0,
        BCP_OUT = 1,
    }
}
