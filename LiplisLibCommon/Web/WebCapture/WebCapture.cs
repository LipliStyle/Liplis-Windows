//=======================================================================
//  ClassName : WebCapture
//  概要      : web画面をキャプチャーする
//
//  Ocinちゃんシステム      
//  Copyright(c) 2009-2009 10kgRocket. All Rights Reserved. 
//=======================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Liplis.Common;
using Liplis.Control;

namespace Liplis.Web.WebCapture
{
    public class WebCapture
    {
        ///============================
        ///クラス
        protected LpsLogController lc;
        protected Form parrentForm;

        ///============================
        ///ダーン
        public bool done { get; set; }
        public TimeSpan timeout = new TimeSpan(0, 0, 10);
        ///============================
        ///所持変数
        public Image resultBmp { get; set; }
        public string saveFileName { get; set; }

        public WebCapture(Form parrentForm)
        {
            lc = new LpsLogController();
            this.parrentForm = parrentForm;
        }

        /// <summary>
        /// Webページをキャプチャするメソッド
        /// </summary>
        public bool capture(string uri)
        {
            try
            {
                //uriチェック
                NonDispBrowser webBrowser = new NonDispBrowser();

                //ウェブブラウザを取得する
                if (!getWebBrowser(ref webBrowser, uri))
                {
                    //取得に失敗したら何もしない
                    return false;
                }

                // TODO: 下記の方法では、フレーム（FRAMESETタグ）を使っているWebページはキャプチャできない。
                HtmlDocument htmlDocument = webBrowser.Document;
                HtmlElement htmlElement = htmlDocument.Body;
                // キャプチャするWebページ全体のサイズを、HtmlElement.ScrollRectangleプロパティで取得
                //Rectangle rectangle = new Rectangle(new Point(0, 0), htmlElement.ScrollRectangle.Size);
                Rectangle rectangle = new Rectangle(new Point(0, 0), new Size(1024, 768));

                // そのサイズとデスクトップと同じ解像度を持つビットマップを生成
                using (Bitmap image = new Bitmap(rectangle.Size.Width, rectangle.Size.Height, Graphics.FromHwnd(parrentForm.Handle)))
                {
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        // HtmlDocument.DomDocumentプロパティからIOleObjectインタフェースを取得（キャスト）
                        IOleObject oleObject = (IOleObject)htmlDocument.DomDocument;
                        if (oleObject != null)
                        {
                            // 生成したビットマップのデバイス・コンテキストを取得
                            IntPtr imageDC = graphics.GetHdc();
                            // WebBrowser.ActiveXInstanceプロパティからインタフェース（IUnknown）へのポインタを取得
                            IntPtr pUnk = Marshal.GetIUnknownForObject(webBrowser.ActiveXInstance);
                            try
                            {
                                Size currentSize = new Size();
                                // IOleObject.GetExtent()メソッドで、ブラウザの現在のエクステント・サイズを保存
                                oleObject.GetExtent(System.Runtime.InteropServices.ComTypes.DVASPECT.DVASPECT_CONTENT, out currentSize);
                                // キャプチャするサイズ（単位はPixel）をHIMETRIC単位に変換
                                Size drawingSize = convertPixelToHIMETRIC(rectangle.Size, imageDC);
                                // 新しいエクステント・サイズをIOleObject.SetExtent()メソッドで設定
                                oleObject.SetExtent(System.Runtime.InteropServices.ComTypes.DVASPECT.DVASPECT_CONTENT, ref drawingSize);

                                // OleDraw()メソッドを使用してビットマップのデバイス・コンテキストに描画
                                LpsWindowsApi.OleDraw(pUnk, DVASPECT.DVASPECT_CONTENT, imageDC, ref rectangle);
                                // ブラウザのエクステント・サイズを保存していたサイズに再設定
                                oleObject.SetExtent(System.Runtime.InteropServices.ComTypes.DVASPECT.DVASPECT_CONTENT, ref currentSize);
                            }
                            finally
                            {
                                // 確保していたデバイス・コンテキストやポインタは忘れず解放
                                Marshal.Release(pUnk);
                                graphics.ReleaseHdc(imageDC);
                            }
                            // メモリ上のビットマップにはWebページ全体がキャプチャ済みなので、煮るなり焼くなり処理
                            //image.Save(pc.getTempPath() + uri + DateTime.Now.ToString().Replace("/","").Replace(":",""), ImageFormat.Png);
                            saveImage(uri, image);
                            // MEMO: キャプチャしたイメージは、このあたりで使う。実際のプログラムでは、インスタンス変数にしておいたほうが扱いが楽チン。
                        }
                        graphics.Dispose();
                    }
                    image.Dispose();
                }
                htmlDocument = null;
                htmlElement = null;
                webBrowser.Dispose();
                webBrowser = null;
                return true;
            }
            catch (Exception err)
            {
                lc.writingLog("WebCapture : capture\n" + err.ToString());
                return false;
            }
        }

        /// <summary>
        /// ウェブブラウザを取得する
        /// </summary>
        /// <param name="webBrowser"></param>
        /// <returns></returns>
        private bool getWebBrowser(ref NonDispBrowser webBrowser, string uri)
        {
            if (string.IsNullOrEmpty(uri) || uri.Equals("about:blank"))
            {
                return false;
            }

            if (!uri.StartsWith("http://"))
            {
                uri = "http://" + uri;
            }

            try
            {
                webBrowser.NavigateAndWait(uri);
                return true;
            }
            catch (UriFormatException)
            {
                return false;
            }
        }

        /// <summary>
        /// 取得したイメージファイルをセーブする
        /// </summary>
        /// <returns></returns>
        private void saveImage(string uri, Bitmap image)
        {
            try
            {
                //セーブファイル名、ビットマップイメージを保持する
                //saveFileName = pc.getTempPath() + DateTime.Now.ToString().Replace("/", "").Replace(":", "") + ".jpg";
                resultBmp = (Image)image.Clone();
                //image.Save(saveFileName, ImageFormat.Jpeg);


            }
            catch (Exception err)
            {
                lc.writingLog("WebCapture : saveImage\n" + err.ToString());
            }
        }

        /// <summary>
        /// イメージのインスタンスを解放する
        /// </summary>
        public void webCaptureDispose()
        {
            resultBmp.Dispose();
            resultBmp = null;
        }

        /// <summary>
        /// Pixel単位をHIMETRIC単位に変換するメソッド
        /// </summary>
        /// <param name="size">Pixel単位のサイズ構造体</param>
        /// <param name="hdc">デバイス・コンテキスト（デスクトップの解像度とコンパチであれば無問題？）</param>
        /// <returns>HIMETRIC単位のサイズ構造体</returns>
        private Size convertPixelToHIMETRIC(Size size, IntPtr hdc)
        {
            const int HIMETRIC_PER_INCH = 2540;

            Size newSize = new Size();

            newSize.Width = (int)((double)size.Width * HIMETRIC_PER_INCH / LpsWindowsApi.GetDeviceCaps(hdc, DEVICECAPS.LOGPIXELSX) + 0.5);
            newSize.Height = (int)((double)size.Height * HIMETRIC_PER_INCH / LpsWindowsApi.GetDeviceCaps(hdc, DEVICECAPS.LOGPIXELSY) + 0.5);

            return newSize;
        }

    }
}
