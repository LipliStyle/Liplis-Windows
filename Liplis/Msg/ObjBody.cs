using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Liplis.Common;
using System.Windows.Forms;

namespace Liplis.Msg
{
    public abstract class  ObjBody
    {


        /// <summary>
        /// getBody
        /// ゲットボディ
        /// 子クラスで実装
        /// </summary>
        /// <returns></returns>
        #region getBody
        public abstract Bitmap getBody11();
        public abstract Bitmap getBody12();
        public abstract Bitmap getBody21();
        public abstract Bitmap getBody22();
        public abstract Bitmap getBody31();
        public abstract Bitmap getBody32();
        public Bitmap getBody(int eye, int mouth, int direction)
        {
            if (direction == 0)
            {
                return getBody(eye, mouth);
            }
            else if (direction == 1)
            {
                return rotateFlip(getBody(eye, mouth));
            }
            else
            {
                return getBody(eye, mouth);
            }

        }
        private Bitmap getBody(int eye, int mouth)
        {
            if (mouth == 0)
            {
                if (eye == 1)
                {
                    return getBody11();
                }
                else if (eye == 2)
                {
                    return getBody21();
                }
                else if (eye == 3)
                {
                    return getBody31();
                }
                else
                {
                    return getBody11();
                }
            }
            else if (mouth == 1)
            {
                if (eye == 1)
                {
                    return getBody12();
                }
                else if (eye == 2)
                {
                    return getBody22();
                }
                else if (eye == 3)
                {
                    return getBody32();
                }
                else
                {
                    return getBody12();
                }
            }
            else
            {
                if (eye == 1)
                {
                    return getBody11();
                }
                else if (eye == 2)
                {
                    return getBody21();
                }
                else if (eye == 3)
                {
                    return getBody31();
                }
                else
                {
                    return getBody11();
                }
            }
        }
        #endregion

        /// <summary>
        /// ビットマップのパスをチェックした上で、
        /// 存在する場合はそのパスの画像を返す。
        /// 存在しない場合は透明１ドットのインスタンスを返す
        /// </summary>
        /// <returns></returns>
        #region getBitmap
        public Bitmap getBitmap(string path)
        {
            if (LpsPathControllerCus.checkFileExist(path))
            {
                return new Bitmap(path);
            }
            else
            {
                MessageBox.Show("画像の読み込みに失敗しました","Liplis");
                return null;
            }
        }
        #endregion

        /// <summary>
        /// 反転
        /// </summary>
        #region rotateFlip
        public Bitmap rotateFlip(Bitmap pic)
        {
            pic.RotateFlip(RotateFlipType.Rotate180FlipY);
            return pic;
        }
        #endregion

        /// <summary>
        /// タッチリストの取得
        /// </summary>
        /// <returns></returns>
        #region getLstTouch
        public abstract List<string> getLstTouch();
        #endregion
        
    }
}
