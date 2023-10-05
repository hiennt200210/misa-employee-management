using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class EmployeeExportExcelDto
    {
        /// <summary>
        /// Số thứ tự
        /// </summary>
        [DisplayName("STT")]
        public int? NumericalOrder { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [DisplayName("Mã nhân viên")]
        public String? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [DisplayName("Tên nhân viên")]
        public string? FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [DisplayName("Giới tính")]
        public String? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        [DisplayName("Ngày sinh")]
        public String? DateOfBirth { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        [DisplayName("Chức danh")]
        public string? PositionName { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        [DisplayName("Tên đơn vị")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [DisplayName("Số tài khoản")]
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [DisplayName("Tên ngân hàng")]
        public string? BankName { get; set; }
    }
}
