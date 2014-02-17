//=======================================================================
//  ClassName : MemoryCleenClass
//  概要      : メモリクリーナ
//
//  SatelliteServer
//  Copyright(c) 2009-2011 sachin. All Rights Reserved. 
//=======================================================================
using System;
using System.Runtime.InteropServices;

namespace Liplis.Sys
{
    public class MemoryCleenClass
    {
        [DllImport("kernel32")]
        public static extern void FillMemory(IntPtr Destination, uint Length, byte Fill);

        [DllImport("kernel32")]
        public static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, AllocationType flAllocationType, ProtectType flProtect);
        [DllImport("kernel32")]
        public static extern bool VirtualFree(IntPtr lpAddress, uint dwSize, FreeType dwFreeType);

        public enum ProtectType
        {
            EXECUTE = 0x10,
            EXECUTE_READ = 0x20,
            EXECUTE_READWRITE = 0x40,
            GUARD = 0x100,
            NOACCESS = 1,
            NOCACHE = 0x200,
            READONLY = 2,
            READWRITE = 4
        }
        public enum FreeType
        {
            DECOMMIT = 0x4000,
            RELEASE = 0x8000
        }
        public enum AllocationType
        {
            COMMIT = 0x1000,
            PHYSICAL = 0x400000,
            RESERVE = 0x2000,
            RESET = 0x80000,
            TOP_DOWN = 0x100000,
            WRITE_WATCH = 0x200000
        }


        /// <summary>
        /// コンストラクター
        /// </summary>
        public MemoryCleenClass()
        {

        }

        /// <summary>
        /// メモリークリーンメソッド
        /// </summary>
        /// <param name="cleenVal">解放量</param>
        public static void memCleen(uint cleenVal)
        {
            //#######################################################
            //この解放値計算については要検討する必要がある。
            //#######################################################
            cleenVal = cleenVal * 150;
            IntPtr lpMem = VirtualAlloc(IntPtr.Zero, cleenVal, AllocationType.TOP_DOWN | AllocationType.COMMIT, ProtectType.NOCACHE | ProtectType.READWRITE);
            FillMemory(lpMem, cleenVal, 0);
            VirtualFree(lpMem, cleenVal, FreeType.DECOMMIT);
        }

    }
}
