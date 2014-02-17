//=======================================================================
//  ClassName : ComResourceController
//  概要      : リソースコントローラー
//
//  Liplisシステム      
//  Copyright(c) 2010-2010 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Resources;
using System.Drawing;

namespace Liplis.Common
{
    public static class LpsResourceController
    {
        /// <summary>
        /// ビットマップリソースを取得する
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Bitmap getResourceBitmap(string resourceName, string name)
        {
            try
            {
                Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                //ResourceManagerオブジェクトの作成
                //リソースファイル名が"Resource1.resources"だとする
                ResourceManager rm = new ResourceManager(asm.GetName().Name + resourceName, asm);

                //リソースファイルから画像を取り出す
                return (Bitmap)rm.GetObject(name);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// もじりソースを取得する
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string getResourceString(string resourceName, string name)
        {
            try
            {
                Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();

                //ResourceManagerオブジェクトの作成
                //リソースファイル名が"Resource1.resources"だとする
                ResourceManager rm = new ResourceManager(asm.GetName().Name + resourceName, asm);

                //リソースファイルからモジを取り出す
                return rm.GetString(name);
            }
            catch
            {
                return "";
            }
        }
    }
}
