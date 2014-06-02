//=======================================================================
//  ClassName : CharPanel
//  概要      : キャラクターパネル
//
//  Liplis2.3
//  2013/06/20 Liplis2.3.0 UI変更
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================
using System;
using System.Drawing;
using System.Windows.Forms;
using Liplis.Common;
using Liplis.Control;
using Liplis.Fct;
using Liplis.Msg;

namespace Liplis.Cmp.Form
{
    public class CharPanel : Panel
    {
        ///=====================================
        /// リプリスインスタンス
        private MainSystem.Liplis lips;

        ///=====================================
        /// スキンインスタンス
        private ObjSkinSetting oss;

        ///=====================================
        /// 構成要素
        private Label lblText;
        private Label lnkLbl;
        private CusCtlPictureBox pic;

        ///=====================================
        /// URL
        private bool select = false;


        ///====================================================================
        ///
        ///                           onCreate
        ///                         
        ///====================================================================

        /// <summary>
        /// コンストラクター
        /// </summary>
        #region CharPanel
        public CharPanel(MainSystem.Liplis lips, ObjSkinSetting oss, bool select)
        {
            this.lips = lips;
            this.oss  = oss;
            this.select = select;
            initDataPanel();
        }
        #endregion


        /// <summary>
        /// キャラパネルの初期化
        /// </summary>
        #region initDataPanel
        private void initDataPanel()
        {
            ObjBodyList obl = new ObjBodyList(oss.charName);

            //初期化
            this.lblText = new System.Windows.Forms.Label();
            this.lnkLbl = new System.Windows.Forms.Label();
            this.pic = new CusCtlPictureBox();

            // 
            // panel
            // 
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.lnkLbl);
            this.Controls.Add(this.pic);

            this.Location = new System.Drawing.Point(3, 3);
            this.Name = "panel";
            this.Size = new System.Drawing.Size(380, 80);
            this.TabIndex = 0;
            this.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.MouseLeave += new System.EventHandler(this.mouseLeave);

            if (!select)
            {
                this.BackColor = Color.Azure;
            }
            else
            {
                this.BackColor = Color.LightPink;
            }

            // 
            // lnkLbl
            // 
            this.lnkLbl.Location = new System.Drawing.Point(108, 17);
            this.lnkLbl.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lnkLbl.Name = "lnkLbl";
            this.lnkLbl.Size = new System.Drawing.Size(439, 16);
            this.lnkLbl.TabIndex = 1;
            this.lnkLbl.Text = oss.charName;
            this.lnkLbl.DoubleClick += new System.EventHandler(this.doubleClick);
            this.lnkLbl.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.lnkLbl.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // lblText
            // 
            this.lblText.Location = new System.Drawing.Point(108, 33);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(454, 48);
            this.lblText.Text = oss.charIntroduction.Replace("@",Environment.NewLine);
            this.lblText.TabIndex = 2;
            this.lblText.DoubleClick += new System.EventHandler(this.doubleClick);
            this.lblText.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.lblText.MouseLeave += new System.EventHandler(this.mouseLeave);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(2, 2);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(75, 75);
            this.pic.TabIndex = 0;
            this.pic.TabStop = false;
            this.pic.SizeMode = PictureBoxSizeMode.Zoom;
            this.pic.DoubleClick += new System.EventHandler(this.doubleClick);
            this.pic.MouseEnter += new System.EventHandler(this.mouseEnter);
            this.pic.MouseLeave += new System.EventHandler(this.mouseLeave);


            //イメージ
            if (LpsPathControllerCus.checkFileExist(LpsPathControllerCus.getSkinPath() + oss.charName + "\\window\\icon.png"))
            {
                this.pic.Image = new Bitmap(new Bitmap(LpsPathControllerCus.getSkinPath() + oss.charName + "\\window\\icon.png"), new Size(100, 100));
            }
            else
            {
                this.pic.Image = FctCreateFromResource.getResourceBitmap(LiplisDefine.TRANSE);
            }
        }
        #endregion

        /// <summary>
        /// アイコンビットマップを取得する
        /// </summary>
        /// <param name="obl"></param>
        /// <param name="bmp"></param>
        /// <returns></returns>
        #region getIconBmp
        private Bitmap getIconBmp(ObjBodyList obl, Bitmap bmp)
        {
            try
            {
                using (Bitmap dest = new Bitmap(32, 32))
                {
                    using (Graphics g = Graphics.FromImage(dest))
                    {
                        using (Bitmap b = new Bitmap(obl.getLiplisBody(0, 1).getBody11()).Clone(new Rectangle(0, 0, obl.width, obl.width), dest.PixelFormat))
                        {
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.DrawImage(b, 0, 0, 32, 32);
                            return (Bitmap)dest.Clone();
                        }
                    }
                }
            }
            catch
            {
                return FctCreateFromResource.getTranse();
            }
        }
        #endregion


        ///====================================================================
        ///
        ///                           onRecive
        ///                         
        ///====================================================================

        /// <summary>
        /// doubleClick
        /// ダブルクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region doubleClick
        private void doubleClick(object sender, EventArgs e)
        {
            lips.onRecive(LiplisDefine.LM_CHANGE, oss.charName);
        }
        #endregion

        /// <summary>
        /// mouseEnter
        /// マウスエンター
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseEnter
        private void mouseEnter(object sender, EventArgs e)
        {
            this.BackColor = Color.LightSkyBlue;
        }
        #endregion

        /// <summary>
        /// mouseLeave
        /// マウスリーブ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region mouseLeave
        private void mouseLeave(object sender, EventArgs e)
        {
            if (!select)
            {
                this.BackColor = Color.Azure;
            }
            else
            {
                this.BackColor = Color.LightPink;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                           onDelete
        ///                         
        ///====================================================================

        /// <summary>
        /// 破棄処理
        /// </summary>
        #region dispose
        public void dispose()
        {
            lblText.Dispose();
            lnkLbl.Dispose();
            pic.Image.Dispose();
            pic.Image = null;
            pic.Dispose();
        }
        #endregion


    }
}
