using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyHoKhau.Model;

namespace QuanLyHoKhau.Utilities
{
    static class Utils
    {

        static public string GenerateNewId(DbSet dbSet, string prefix, int length)
        {
            int i = 0;
            string str = prefix + i.ToString($"D{length}");

            while(dbSet.Find(str) != null)
            {
                i++;
                str = prefix + i.ToString($"D{length}");
            }

            return str;
        }

        /// <summary>
        /// We have SoHoKhau's (a) ChuHo's SoHoKhau (b). We need (a) == (b). if not, SoHoKhau should remove this ChuHo. this function does that :^)
        /// </summary>
        static public void AdjustChuHoInSoHoKhaus()
        {
            foreach(SOHOKHAU shk in DataProvider.Ins.DB.SOHOKHAUs)
            { 
                if(shk.CMNDChuHo == null)
                    continue;

                NHANKHAU chuHo = DataProvider.Ins.DB.NHANKHAUs.Find(shk.CMNDChuHo);
                if(chuHo != null && chuHo.MASHK != shk.MaSHK)
                    shk.CMNDChuHo = null;
            }
        }
    }
}
