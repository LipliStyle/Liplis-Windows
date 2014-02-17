//=======================================================================
//  ClassName : mhtDownLoader
//  概要      : mthでダウンロードする
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================

//
//このクラスはCDO、ADODBが必要である。別途用意し、各自実装
//
//
//
//

//using CDO;
//using ADODB;

//namespace Liplis.Web
//{
//    public class WebMhtDownLoader
//    {
//        public int CreateMHT(string url, string file)
//        {

//            MessageClass msg = new MessageClass();

//            try
//            {
//                // CDO.CdoMHTMLFlags.cdoSuppressNoneは
//                // ページ内で参照しているすべてのリソースをダウンロード
//                msg.CreateMHTMLBody(url, CdoMHTMLFlags.cdoSuppressNone, "", ""); // 後ろの空2つはIDおよびパスワード
//            }
//            catch
//            {
//                return 1;
//            }
//            Stream st = msg.GetStream();
//            st.SaveToFile(file, SaveOptionsEnum.adSaveCreateOverWrite);
//            st.Close();
//            return 0;
//        }
//    }
//}
