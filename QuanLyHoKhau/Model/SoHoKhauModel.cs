﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyHoKhau.Model
{
    public partial class SOHOKHAU
    {
        public SOHOKHAU(SOHOKHAU shk)
        {
            if(shk == null)
            {
                MaSHK = "";
                CMNDChuHo = null;
                MaCongAn = null;
                MaSoLuuNhanKhau = null;
                DiaChi = "";
                NgayTaoSo = DateTime.Now;
                IsDeleted = false;
            }
            else
            { 
                this.MaSHK = shk.MaSHK;
                this.CMNDChuHo = shk.CMNDChuHo;
                this.MaCongAn = shk.MaCongAn;
                this.MaSoLuuNhanKhau = shk.MaSoLuuNhanKhau;
                this.DiaChi = shk.DiaChi;
                this.NgayTaoSo = shk.NgayTaoSo;
                this.IsDeleted = shk.IsDeleted;
            }
        }
    }
}