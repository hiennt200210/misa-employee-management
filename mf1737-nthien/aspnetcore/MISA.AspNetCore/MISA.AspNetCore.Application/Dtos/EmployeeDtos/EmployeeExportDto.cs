using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web07.GPBL.Application.DTOs.Employee
{
    public class EmployeeExportDto
    {
        #region Fields

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; } = string.Empty;

        /// <summary>
        /// Họ và tên
        /// </summary>
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Email nhân viên
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? PhoneHome { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string? PhoneMobile { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string? PositionName { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string? BankAccountNo { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        public string? BankAccountPlace { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Số CCCD/CMND
        /// </summary>
        public string? IdentityNo { get; set; }

        /// <summary>
        /// Ngày cấp CCCD/CMND
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp CCCD/CMND
        /// </summary>
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Tên phòng ban
        /// </summary>
        public string? DepartmentName { get; set; }
        #endregion
    }
}