//=======================================================================
//  ClassName : msgTargetVersion
// ■概要     : メッセージターゲットバージョン
//
//■ Liplis4.0
//　2014/04/26 Liplis4.0 アップデート機能
// Copyright(c) 2014 LipliStyle さちん MITライセンス
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Liplis.Msg
{
	public class msgTargetVersion
	{
	    ///=============================
		///プロパティ
		public  string version {get;set;}
        public string enable { get; set; }
        public string option {get;set;}

		/// <summary>
		/// msgTargetVersion
		/// コンストラクター
		/// </summary>
		#region msgTargetVersion
		public msgTargetVersion(string version, string enable, string option)
		{
            this.version = version;
            this.enable = enable;
            this.option = option;

		}
		#endregion

	}
}
