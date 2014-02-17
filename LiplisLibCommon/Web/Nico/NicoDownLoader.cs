//=======================================================================
//  ClassName : NicoDownLoader
//  概要      : ニコニコダウンローダー
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Liplis.Common;


namespace Liplis.Web.Nico
{
    public class NicoDownLoader
    {

        /////===========================================
        ///// id.pass
        //private string id;
        //private string pass;

        /////===========================================
        ///// 設定
        //private string skinPath;
        //private string txtColor;
        //private string lnkColor;

        /////===========================================
        ///// フラグ
        //private bool cancelFlug;

        /////===========================================
        ///// ファイル名
        //Thread t;
        //private string title;
        //private string filter;
        //private string fileName;
        //private string fromPath;

        ///// <summary>
        ///// コンストラクタ
        ///// </summary>
        //#region NicoDownLoader
        //public NicoDownLoader(string nicoId, string nicoPass)
        //{
        //    this.id = nicoId;
        //    this.pass = nicoPass;
        //}
        //#endregion

        ///// <summary>
        ///// ニコニコ動画ダウンロード
        ///// </summary>
        //#region callTeaPot
        //public int callTeaPot(string url)
        //{
        //    return callTeaPot(url, this.id, this.pass);
        //}
        //public int callTeaPot(string url, int mode)
        //{
        //    return callTeaPot(url, this.id, this.pass, mode);
        //}
        //public int callTeaPot(string url, string id, string pass)
        //{
        //    string command = "";
        //    string saveName = createSaveFileName(url, "flv");
        //    //ファイル名チェック
        //    if (saveName.Equals(""))
        //    {
        //        //ユーザーキャンセルもしくはファイル名なし
        //        return 4;
        //    }
        //    Point p = ComLiplisUtil.getPoint(lips.getRect(), Environment.TickCount);
        //    command = p.X + " " + p.Y + " " + 0 + " \"" + skinPath + "\" \"" + id + "\" \"" + pass + "\" \"" + url + "\" \"\" \"" + saveName + "\"";
        //    doProcess(command);
        //    return 0;
        //}
        //public int callTeaPot(string url, string id, string pass, int mode)
        //{
        //    string command = "";

        //    if (mode == 0)
        //    {
        //        fromPath = "";
        //        fileName = createSaveFileName(url, "flv");
        //    }
        //    else if (mode >= 10 && mode < 20)
        //    {
        //        fromPath = lips.pc.getTempPath() + ComLiplisUtil.getName(8);
        //        fileName = createSaveFileName(url, "mp3");
        //    }
        //    else if (mode == 20)
        //    {
        //        fromPath = lips.pc.getTempPath() + ComLiplisUtil.getName(8);
        //        fileName = createSaveFileName(url, "mp4");
        //    }
        //    else
        //    {
        //        fromPath = lips.pc.getTempPath() + ComLiplisUtil.getName(8);
        //        fileName = createSaveFileName(url, "flv");
        //    }

        //    //ファイル名チェック
        //    if (fileName.Equals(""))
        //    {
        //        //ユーザーキャンセルもしくはファイル名なし
        //        return 4;
        //    }

        //    //ニコIDチェック
        //    if (lips.so.nicoIdSave != 1)
        //    {
        //        winDlgNicoId wdn = new winDlgNicoId(lips);
        //        wdn.Show();
        //        wdn.faidin();


        //        while (!wdn.respons)
        //        {
        //            Thread.Sleep(10);
        //            Application.DoEvents();
        //        }

        //        if (wdn.yesNo)
        //        {
        //            //戻りを取得
        //            id = wdn.id;
        //            pass = wdn.pass;

        //            //チェックボックスがONならセーブする
        //            if (wdn.save)
        //            {
        //                lips.so.nicoIdSave = 1;
        //                lips.so.nicoId = id;
        //                lips.so.nicoPass = pass;
        //            }
        //        }
        //        else
        //        {
        //            //ユーザーキャンセル
        //            return 4;
        //        }
        //    }

        //    Point p = ComLiplisUtil.getPoint(lips.getRect(), Environment.TickCount);
        //    command = p.X + " " + p.Y + " " + mode + " \"" + skinPath + "\" \"" + id + "\" \"" + pass + "\" \"" + url + "\" \"" + fromPath + "\" \"" + fileName + "\"";
        //    doProcess(command);
        //    return 0;
        //}
        //#endregion

        ///// <summary>
        ///// ファイル名を作成する
        ///// </summary>
        //#region createSaveFileName
        //private string createSaveFileName(string url, string extension)
        //{
        //    string saveName = "";    //保存パス

        //    if (lips.so.dlPathSave == 1)
        //    {
        //        string savePath;
        //        string nicoVideoId = ComLiplisUtil.getNicoVideoId(url);
        //        objNicoInfo n = new objNicoInfo(nicoVideoId, ComDefine.NICO_INFO);

        //        //からチェック
        //        savePath = lips.so.dlPath;
        //        if (savePath.Equals(""))
        //        {
        //            if (ComLiplisUtil.nullCheck(n.title) != "")
        //            {
        //                saveName = ComPathController.getDownPath() + "\\" + n.title + "." + extension;
        //            }
        //            else
        //            {
        //                saveName = ComPathController.getDownPath() + "\\" + nicoVideoId + "." + extension;
        //            }
        //        }
        //        else
        //        {
        //            if (!ComPathController.checkDir(savePath))
        //            {
        //                //ダイアログスレッド
        //                getInputFileName("名前をつけて動画を保存", extension);

        //                if (fileName != "")
        //                {
        //                    saveName = fileName;
        //                }
        //                else
        //                {
        //                    //ユーザーキャンセル
        //                    return "";
        //                }
        //            }
        //            else
        //            {
        //                if (ComLiplisUtil.nullCheck(n.title) != "")
        //                {
        //                    saveName = savePath + "\\" + n.title + "." + extension;
        //                }
        //                else
        //                {
        //                    saveName = savePath + "\\" + nicoVideoId + "." + extension;
        //                }
        //            }
        //        }


        //        //一応解放
        //        n = null;
        //    }
        //    else
        //    {
        //        //ダイアログスレッド
        //        getInputFileName("名前をつけて動画を保存", extension);

        //        if (fileName != "")
        //        {
        //            saveName = fileName;
        //        }
        //        else
        //        {
        //            //ユーザーキャンセル
        //            return "";
        //        }
        //    }
        //    return saveName;
        //}
        //#endregion

        ///// <summary>
        ///// ファイル名指定ダイアログのスレッドを実行する
        ///// </summary>
        //#region getInputFileNameDlg
        //private void getInputFileName(string title, string extension)
        //{
        //    this.title = "名前をつけて動画を保存";
        //    this.filter = extension + " files (*." + extension + ")|*." + extension + "";

        //    //スレッドでダイアログを実行
        //    t = new Thread(new ThreadStart(getInputFileNameDlg));  // スレッドの宣言
        //    t.SetApartmentState(ApartmentState.STA);
        //    t.Start();
        //    //t.Join();
        //    while (t.ThreadState == System.Threading.ThreadState.Running)
        //    {
        //        Thread.Sleep(10);
        //        Application.DoEvents();
        //    }
        //}
        //#endregion

        ///// <summary>
        ///// ファイルネーム指定ダイアログ
        ///// </summary>
        //#region getInputFileNameDlg
        //private void getInputFileNameDlg()
        //{
        //    SaveFileDialog dialog = new SaveFileDialog();
        //    dialog.Title = this.title;
        //    dialog.Filter = this.filter;
        //    dialog.RestoreDirectory = true;
        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {
        //        fileName = dialog.FileName;
        //    }
        //    else
        //    {
        //        //ユーザーキャンセル
        //        fileName = "";
        //    }
        //}
        //#endregion

        ///// <summary>
        ///// プロセス起動のスレッド
        ///// </summary>
        //#region doProcess
        //private void doProcess(string command)
        //{
        //    try
        //    {
        //        ProcessStartInfo pi = new ProcessStartInfo();
        //        pi.FileName = ComDefine.EXE_TEAPOT;
        //        pi.Arguments = command;
        //        Process.Start(pi);
        //    }
        //    catch (System.ComponentModel.Win32Exception fileNotFoundErr)
        //    {
        //        Console.Write(fileNotFoundErr);
        //    }
        //    catch (System.Exception err)
        //    {
        //        Console.Write(err);
        //    }
        //}
        //#endregion

    }
}
