//=======================================================================
//  ClassName : BaseDragDrop
//  概要      : ドラッグ&ドロップを行う
//               各所に記述する方向に変更
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Liplis.Common;

namespace Liplis.Fct
{
    public static class FctDragData
    {
        ///=====================================
        /// 定数
        private const string URL = "UniformResourceLocator";
        private const string URLW = "UniformResourceLocatorW";


        /// <summary>
        /// ドラッグ内容を受け取り、処理する
        /// </summary>
        /// <param name="e"></param>
        #region getDrag
        public static void getDrag(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(URL) || e.Data.GetDataPresent(URLW))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //未実装のため、ファイルは受け付けない
                //e.Effect = DragDropEffects.Copy;
                e.Effect = DragDropEffects.None;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        #endregion


        /// <summary>
        /// ドロップ内容を受け取り、処理する
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region getDrop
        public static void getDrop(DragEventArgs e)
        {
            List<string> formatList = new List<string>(e.Data.GetFormats());

            if (e.Data.GetDataPresent(URL) || e.Data.GetDataPresent(URLW))
            {
                dropCheck(e.Data.GetData(DataFormats.Text).ToString());
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //ファイルチェックは未実装
                //fileCheck(new List<string>((string[])e.Data.GetData(DataFormats.FileDrop)));
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                dropCheck(e.Data.GetData(DataFormats.Text).ToString());
            }
        }
        #endregion

        /// <summary>
        /// ドロップ内容を受け取り、処理する
        /// </summary>
        /// <param name="url"></param>
        /// <returns>有効ならリスト、無効ならNULLを返す</returns>
        #region getDropTextList
        public static List<string> getDropTextList(DragEventArgs e)
        {
            List<string> formatList = new List<string>(e.Data.GetFormats());

            if (e.Data.GetDataPresent(URL) || e.Data.GetDataPresent(URLW))
            {
                return new List<string>(e.Data.GetData(DataFormats.Text).ToString().Split(Environment.NewLine.ToCharArray()));
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //ファイルチェックは未実装
                //fileCheck(new List<string>((string[])e.Data.GetData(DataFormats.FileDrop)));
                return null;
            }
            else if (e.Data.GetDataPresent(DataFormats.Text))
            {
                return new List<string>(e.Data.GetData(DataFormats.Text).ToString().Split(Environment.NewLine.ToCharArray()));
            }
            else
            {
                return null;
            }
        }
        #endregion


        /// <summary>
        /// ドロップされたURLをチェックする
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        #region dropCheck
        public static bool dropCheck(string url)
        {
            //rssチェック
            if (LpsLiplisUtil.checkRssConnect(url) != null)
            {
                //登録
                return true;
            }
            else
            {
                //登録しない
                return false;
            }
        }
        #endregion

        /// <summary>
        /// ドロップされたのがファイルパスだった場合、ファイルチェックを行い、準じた処理を行う
        /// 未実装
        /// </summary>
        #region fileCheck
        private static void fileCheck(List<string> fileList)
        {
            foreach (string file in fileList)
            {
                //ファイルの存在チェック
                if (LpsPathControllerCus.checkFileExist(file))
                {
                    //if (file.Substring(file.Length - 3, 3).Equals(LiplisDefine.EXTENTION_DEFINE_RSS_SETTING))
                    //{
                    //    loadRssSettingFile(file);
                    //}
                }
            }
        }
        #endregion
    }
}
