using MISA.AspNetCore.Domain;
using MISA.AspNetCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AspNetCore.Application
{
    public class DepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<List<DepartmentModel>> GetAllAsync()
        {
            var employees = await _departmentRepository.GetAllAsync();

            var departmentModels = employees.Select(department => MapDepartToDepartmentModel(department)).ToList();

            return departmentModels;
        }

        /// <summary>
        /// Lấy thông tin một phòng ban
        /// </summary>
        /// <param name="employeeId">Định danh của phòng ban cần lấy thông tin</param>
        /// <returns>Thông tin phòng ban</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<DepartmentModel> GetByIdAsync(Guid departmentId)
        {
            var department = await _departmentRepository.GetByIdAsync(departmentId);

            var departmentModel = MapDepartToDepartmentModel(department);

            return departmentModel;
        }

        /// <summary>
        /// Thêm mới một phòng ban
        /// </summary>
        /// <param name="departmentModel">Thông tin phòng ban cần thêm mới</param>
        /// <returns>Thông tin phòng ban cần thêm mới</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> InsertAsync(DepartmentInsertModel departmentInsertModel)
        {
            var department = MapDepartmentInsertModelToDepartment(departmentInsertModel);

            var affectedRows = await _departmentRepository.InsertAsync(department);

            return affectedRows;
        }

        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="department">Thông tin cần cập nhật</param>
        /// <returns>Thông tin cần cập nhật</returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> UpdateAsync(DepartmentUpdateModel departmentUpdateModel)
        {
            var department = MapDepartmentUpdateModelToDepartment(departmentUpdateModel);

            var affectedRows = await _departmentRepository.UpdateAsync(department);

            return affectedRows;
        }

        /// <summary>
        /// Xóa một phòng ban
        /// </summary>
        /// <param name="departmentId">Định danh của phòng ban cần xóa</param>
        /// <returns>
        /// 1 - Xóa thành công
        /// </returns>
        /// CreatedBy: hiennt200210 (16/09/2023)
        public async Task<int> DeleteAsync(Guid departmentId)
        {
            var affectedRows = await _departmentRepository.DeleteAsync(departmentId);

            return affectedRows;
        }

        private static DepartmentModel MapDepartToDepartmentModel(Department department)
        {
            var departmentModel = new DepartmentModel()
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = department.DepartmentName,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
            };
            
            return departmentModel;
        }

        private static Department MapDepartmentInsertModelToDepartment(DepartmentInsertModel departmentInsertModel)
        {
            var department = new Department()
            {
                DepartmentId = Guid.NewGuid(),
                DepartmentName = departmentInsertModel.DepartmentName,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
            };

            return department;
        }

        private static Department MapDepartmentUpdateModelToDepartment(DepartmentUpdateModel departmentUpdateModel)
        {
            var department = new Department()
            {
                DepartmentId = departmentUpdateModel.DepartmentId,
                DepartmentName = departmentUpdateModel.DepartmentName,
                CreatedDate = DateTime.Now,
                CreatedBy = null,
                ModifiedDate = DateTime.Now,
                ModifiedBy = null,
            };

            return department;
        }
    }
}
