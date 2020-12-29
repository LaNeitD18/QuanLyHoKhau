using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
