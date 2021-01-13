using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoKhau.ViewModel
{
    class GlobalState
    {
        #region SingleTon
        private GlobalState() { }

        private static GlobalState __instance;

        public static GlobalState Ins()
        {
            if (__instance == null)
                __instance = new GlobalState();

            return __instance;
        }
        #endregion

        public string maCongAn = "CA001";
        public int maxFreeDaysStay = 10; // so luong ngay duoc tam tru ma khong can phieu tam vang
        public string chuHo = "Chủ hộ";
    }

    class DuyetActionTypes
    {
        public const string Add = "Add";
        public const string Edit = "Edit";
        public const string Remove = "Remove";
    }
}
