//=======================================================================
//  ClassName : TabPageManager
//  概要      : タブページマネージャ
//
//  Liplis2.0
//  Copyright(c) 2010-2012 LipliStyle. All Rights Reserved. 
//=======================================================================
using System.Windows.Forms;
using System.Collections.Generic;

namespace Liplis.Control
{
    public class TabPageManager
    {
        private List<TabPageInfo> _tabPageInfos = new List<TabPageInfo>();
        private TabControl _tabControl = null;
        private Dictionary<string,int> _tabPageIndex =  new Dictionary<string,int>();

        /// <summary>
        /// TabPageInfoクラス
        /// </summary>
        #region TabPageInfo
        private class TabPageInfo
        {
            public TabPage TabPage;
            public bool Visible;
            public TabPageInfo(TabPage page, bool v)
            {
                TabPage = page;
                Visible = v;
            }
        }
        #endregion
        
        /// <summary>
        /// TabPageManagerクラスのインスタンスを作成する
        /// </summary>
        /// <param name="crl">基になるTabControlオブジェクト</param>
        #region TabPageManager
        public TabPageManager(TabControl crl)
        {
            //タブページコントロール
            _tabControl = crl;

            //タブページインフォ
            _tabPageInfos = new List<TabPageInfo>(_tabControl.TabPages.Count);

            //回して登録
            for (int i = 0; i < _tabControl.TabPages.Count; i++)
            {
                //インフォの追加
                _tabPageInfos.Add(new TabPageInfo(_tabControl.TabPages[i], true));

                //インデックスの追加
                _tabPageIndex.Add(_tabControl.TabPages[i].Name,i);
            }
        }
        #endregion
        
        /// <summary>
        /// TabPageの表示・非表示を変更する
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// このメソッドで指定するべきindexは「_tabPageInfos」のインデックス。「_tabControl.TabPages」でないことに注意する。
        /// 個別に閉じる場合は必ずインデックスなしのメソッドを使用する！
        /// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        /// <param name="index">変更するTabPageのIndex番号</param>
        /// <param name="v">表示するときはTrue。
        /// 非表示にするときはFalse。</param>
        #region ChangeTabPageVisible
        public void ChangeTabPageVisible(int index, bool v)
        {
            try
            {
                if (_tabPageInfos[index].Visible == v)
                {
                    return;
                }

                _tabPageInfos[index].Visible = v;
                _tabControl.SuspendLayout();
                _tabControl.TabPages.Clear();
                for (int i = 0; i < _tabPageInfos.Count; i++)
                {
                    if (_tabPageInfos[i].Visible)
                    {
                        _tabControl.TabPages.Add(_tabPageInfos[i].TabPage);
                    }
                }
                _tabControl.ResumeLayout();
            }
            catch
            {
                if (index > 0)
                {
                    ChangeTabPageVisible(index - 1, v);
                }
            }
        }
        #endregion

        /// <summary>
        /// 現在選択タブから取得
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        #region ChangeTabPageVisible
        public void ChangeTabPageVisible(bool v)
        {
            int i = 0;

            //セレクティッドタブを探す
            for (i = 0; i < _tabPageInfos.Count; i++)
            {
                if(_tabPageInfos[i].TabPage.Text.Equals(_tabControl.SelectedTab.Text))
                {
                    //タブちぇんじ
                    ChangeTabPageVisible(i, v);
                    break;
                }
            }
        }
        #endregion

        /// <summary>
        /// タブを追加する
        /// </summary>
        /// <param name="tabName"></param>
        #region addTab
        public void addTab(TabPage page)
        {
            //インデックスの追加
            if (getTabIndexFromName(page.Text) < 0)
            {
                //インフォの追加
                _tabPageInfos.Add(new TabPageInfo(page, true));

                _tabPageIndex.Add(page.Text, _tabPageInfos.Count - 1);

            }
            else 
            {
                ChangeTabPageVisible(getTabIndexFromName(page.Text), true);
                _tabPageIndex[page.Text] = _tabPageInfos.Count - 1;
            }

        }
        #endregion

        /// <summary>
        /// タブを開く
        /// </summary>
        /// <param name="tabName"></param>
        #region openTab
        public void openTab(string tabName)
        {
            ChangeTabPageVisible(getTabIndexFromName(tabName), true);
        }
        #endregion
        
        /// <summary>
        /// タブ名からインデックスを引く
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        #region getTabIndexFromName
        public int getTabIndexFromName(string tabName)
        {
            try
            {
                return _tabPageIndex[tabName];
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        #region updateTab
        public void updateTab(TabPage page)
        {
            try
            {
                _tabPageInfos[getTabIndexFromName(page.Text)].TabPage = page;
            }
            catch
            {
                
            }
        }
        #endregion

        /// <summary>
        /// タブ名から現在開かれているタブを検索し、インデックスを引く
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        #region getTabPageIndexFromName
        public int getTabPageIndexFromName(string tabName)
        {
            int idx = 0;
            try
            {
                foreach (TabPage tabP in this._tabControl.TabPages)
                {
                    if (tabP.Text.Equals(tabName) || tabP.Name.Equals(tabName))
                    {
                        return idx;
                    }
                    idx++;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        /// <summary>
        /// タブ名から現在開かれているタブを検索する
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        #region getTabPageFromName
        public TabPage getTabPageFromName(string tabName)
        {
            int idx = 0;
            try
            {
                foreach (TabPage tabP in this._tabControl.TabPages)
                {
                    if (tabP.Name.Equals(tabName))
                    {
                        return tabP;
                    }
                    idx++;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion       

        /// <summary>
        /// すべてのタブを閉じる
        /// </summary>
        #region allClose
        public void allClose()
        {
            foreach (TabPageInfo tpi in _tabPageInfos)
            {
                tpi.Visible = false;
            }

            _tabControl.TabPages.Clear();
        }
        #endregion

    }
}
