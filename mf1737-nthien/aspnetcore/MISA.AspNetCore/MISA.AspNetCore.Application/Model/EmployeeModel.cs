using MISA.AspNetCore.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class EmployeeModel
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Tạo bởi
        /// /// </summary>
        [MaxLength(255)]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Sửa đổi bởi
        /// </summary>
        [MaxLength(255)]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Định danh của nhân viên
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [MaxLength(20)]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [MaxLength(100)]
        public string? FullName { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        [MaxLength(255)]
        public string? PositionName { get; set; }

        /// <summary>
        /// Định danh của phòng ban
        /// </summary>
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        [MaxLength(50)]
        public string? LandlineNumber { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        [MaxLength(100)]
        public string? Email { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        [MaxLength(255)]
        public string? Address { get; set; }

        /// <summary>
        /// Số căn cước công dân
        /// </summary>
        [MaxLength(25)]
        public string? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp
        /// </summary>
        public DateTime? IdentityDate { get; set; }

        /// <summary>
        /// Nơi cấp
        /// </summary>
        [MaxLength(255)]
        public string? IdentityPlace { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        [MaxLength(25)]
        public string? BankAccount { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        [MaxLength(255)]
        public string? BankName { get; set; }

        /// <summary>
        /// Chi nhánh ngân hàng
        /// </summary>
        [MaxLength(255)]
        public string? BankBranch { get; set; }
    }
}
