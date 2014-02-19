//=======================================================================
//  ClassName : ObjBodyListMini
//  概要      : ボディオブジェクトリストミニ
//
//  Liplis3.0.2
//  Copyright(c) 2010-2013 LipliStyle.Sachin
//=======================================================================

using Liplis.Common;
namespace Liplis.Msg
{
    public class ObjBodyListMini : ObjBodyList
    {
        /// <summary>
        /// コンストラクター
        /// </summary>
        /// <param name="loadSkin"></param>
        #region ObjBodyListMini
        public ObjBodyListMini(string loadSkin)
            : base(loadSkin)
        {

        }
        #endregion
        
        /// <summary>
        /// サイズと位置を設定する。
        /// ひとまず、2/3の大きさにする
        /// </summary>
        #region getSizeAndLocation
        protected override void getSizeAndLocation()
        {
            this.height = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_HEIGHT)) / 3 * 2;
            this.width = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_WIDHT)) / 3 * 2;
            this.locX = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_LOCATION_X));
            this.locY = rXLMSInt(xmlDoc.SelectNodes(LiplisDefine.BODY_LOCATION_Y));
        }
        #endregion
    }
}
