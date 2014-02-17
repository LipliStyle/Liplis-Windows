//=======================================================================
//  ClassName : CusCtlTabX
//  概要      : カスタムコントロールタブコントロールX
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle. All Rights Reserved. 
//=======================================================================
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Liplis.Control
{
    public class CusCtlTabX : TabControl {

        //タブマネージャー
        public TabPageManager _tabPageManager = null;

        // コンストラクタ
        public CusCtlTabX()
        {

        }

        ///====================================================================
        ///
        ///                             　初期化
        ///                         
        ///====================================================================

        /// <summary>
        /// 初期化
        /// デザイナーのイニシャライズコンポーネントメソッドを呼んだあとに必ず呼ぶ！
        /// </summary>
        #region initCusCtlTabX
        public void initCusCtlTabX()
        {
            //タブ名をタブテキストで置換しておく
            foreach (TabPage tp in this.TabPages)
            {
                tp.Name = tp.Text;
            }

            //タブマネージャーの起動
            _tabPageManager = new TabPageManager(this);
        }
        #endregion

        ///====================================================================
        ///
        ///                        　イベントハンドラ
        ///                         
        ///====================================================================

        /// <summary>
        /// タブの閉じるボタンクリックイベント
        /// 2013/08/30 ver2.2.3 タブを閉じるときに未保存状態だった場合に、保存するか問う。
        /// </summary>
        /// <param name="e"></param>
        #region OnCloseButtonClick
        protected void OnCloseButtonClick(MouseEventArgs e)
        {
            _tabPageManager.ChangeTabPageVisible(false);
        }
        #endregion
        
        /// <summary>
        /// OnMouseUp
        /// </summary>
        /// <param name="e"></param>
        #region OnMouseUp
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Point pt = new Point(e.X, e.Y);
            Rectangle rect = this.GetTabCloseButtonRect(pt);
            if (rect.Contains(pt))
            {
                this.OnCloseButtonClick(e);
                this.Invalidate(rect);
            }
        }

        #endregion

        /// <summary>
        /// WndProc
        /// </summary>
        /// <param name="m"></param>
        #region WndProc
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 15:
                    this.DrawTabCloseButton();
                    break;
                default:
                    break;
            }
        }
        #endregion

        ///====================================================================
        ///
        ///                              　処理
        ///                         
        ///====================================================================

        /// <summary>
        /// タブの閉じるボタン場所を取得
        /// </summary>
        /// <param name="pt"></param>
        /// <returns></returns>
        #region GetTabCloseButtonRect
        private Rectangle GetTabCloseButtonRect(Point pt)
        {
            Rectangle rect;
            for (int i = 0; i < base.TabCount; i++)
            {
                rect = this.GetTabCloseButtonRect(i);
                if (rect.Contains(pt))
                {
                    return rect;
                }
            }
            return Rectangle.Empty;
        }

        /// <summary>
        /// タブの閉じるボタン場所を取得
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        #region GetTabCloseButtonRect
        private Rectangle GetTabCloseButtonRect(int index)
        {
            Rectangle rect = this.GetTabRect(index);
            rect.X = rect.Right - 20;
            rect.Y = rect.Top + 2;
            rect.Width = 16;
            rect.Height = 16;

            return rect;
        }
        #endregion
        #endregion

        /// <summary>
        /// タブに閉じるボタンを描画
        /// </summary>
        #region DrawTabCloseButton
        private void DrawTabCloseButton()
        {
            Graphics g = this.CreateGraphics();
            Rectangle rect = Rectangle.Empty;
            Point pt = this.PointToClient(Cursor.Position);
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                rect = this.GetTabCloseButtonRect(i);
                // 閉じるボタン描画
                ControlPaint.DrawCaptionButton(g, rect, CaptionButton.Close, ButtonState.Flat);
            }
            g = null;
        }
        #endregion

        /// <summary>
        /// タブ名指定でタブ表示名を変更する
        /// </summary>
        /// <param name="tabName"></param>
        #region changeTabText
        public void changeTabText(string tabName, string tabText)
        {
            TabPage t = _tabPageManager.getTabPageFromName(tabName);
            if (t != null)
            {
                t.Text = tabText;
            }
        }
        #endregion

        /// <summary>
        /// タブ名が存在しなければタブを追加する
        /// </summary>
        /// <param name="page"></param>
        #region addTabExists
        public void addTabExists(TabPage page)
        {
            //ページが存在しなければ追加する
            if (_tabPageManager.getTabIndexFromName(page.Text) < 0)
            {
                //タブを追加する
                addTab(page);
            }
            else
            {
                //タブを更新する
                //_tabPageManager.updateTab(page);

                //タブを開く
                openTab(page.Text);
            }
        }
        #endregion

        /// <summary>
        /// タブを追加する
        /// </summary>
        /// <param name="page"></param>
        #region addTab
        public void addTab(TabPage page)
        {
            this.TabPages.Add(page);
            this._tabPageManager.addTab(page);
            this.SelectedIndex = _tabPageManager.getTabPageIndexFromName(page.Text);
        }
        #endregion

        /// <summary>
        /// タブ名指定でタブをオープンする
        /// </summary>
        /// <param name="tabName"></param>
        #region openTab
        public void openTab(string tabName)
        {
            _tabPageManager.openTab(tabName);
            this.SelectedIndex = _tabPageManager.getTabPageIndexFromName(tabName);
        }
        #endregion

        /// <summary>
        /// インデックス指定でタブをオープンする
        /// </summary>
        /// <param name="idx"></param>
        #region openTab
        public void openTab(int idx)
        {
            _tabPageManager.ChangeTabPageVisible(idx,true);
        }
        #endregion

        /// <summary>
        /// 指定のタブをクローズする
        /// </summary>
        /// <param name="idx"></param>
        #region closeTab
        public void closeTab(string tabName)
        {
            _tabPageManager.ChangeTabPageVisible(_tabPageManager.getTabPageIndexFromName(tabName), false);
        }
        #endregion

        /// <summary>
        /// 指定のタブをクローズする
        /// </summary>
        /// <param name="idx"></param>
        #region closeTab
        public void closeTab(int idx)
        {
            _tabPageManager.ChangeTabPageVisible(idx, false);
        }
        #endregion
        
        /// <summary>
        /// すべてのタブを閉じる
        /// </summary>
        #region allTabClose
        public void allTabClose()
        {
            try
            {
                _tabPageManager.allClose();
            }
            catch
            {

            }
        }
        #endregion


        
    }
}