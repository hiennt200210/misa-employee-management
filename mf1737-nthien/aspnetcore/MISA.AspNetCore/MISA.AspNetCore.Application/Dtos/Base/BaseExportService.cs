//using OfficeOpenXml.Style;
//using OfficeOpenXml;
//using System.Reflection;
//using MISA.Web07.GPBL.Domain.Interfaces.Repositories.Base;
//using MISA.Web07.GPBL.Application.Interfaces.Services.Base;

//namespace MISA.Web07.GPBL.Application.Services.Bases
//{
//    /// <summary>
//    /// Xuất khẩu
//    /// </summary>
//    /// <typeparam name="T">Kiểu dto muốn xuất khẩu</typeparam>
//    public class BaseExportService<TEntity, TDto> : IBaseExportService
//    {
//        private readonly IBaseReadOnlyRepository<TEntity> _baseReadOnlyRepository;
//        public BaseExportService(IBaseReadOnlyRepository<TEntity> baseReadOnlyRepository)
//        {
//            _baseReadOnlyRepository = baseReadOnlyRepository;
//        }
//        public virtual async Task<MemoryStream> ExportToExcelAsync(string search, string sort, string title)
//        {

//            var (sortType, sortField) = CommonFunction.ProcessOrderType(sort, typeof(TDto));

//            var data = await _baseReadOnlyRepository.GetByState<TDto>(search, sortField, sortType);

//            var props = typeof(TDto).GetProperties();

//            // Tạo một tệp Excel mới
//            using var package = new ExcelPackage();
//            // Tạo một sheet trong tệp Excel
//            var worksheet = package.Workbook.Worksheets.Add(title);

//            BuildExcelHeader(ref worksheet, props, data.Count, title);

//            BuildExcelData(ref worksheet, props, data);

//            //Chỉnh tiêu đề cho file excel

//            var stream = new MemoryStream(package.GetAsByteArray());

//            return stream;
//        }

//        /// <summary>
//        /// Điền dữ liệu vào excel
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="worksheet">excel</param>
//        /// <param name="props">các trường dữ liệu cần build</param>
//        /// <param name="data">Dữ liệu</param>
//        /// Created by: ntvu 29/09/2023
//        protected virtual void BuildExcelData<T>(ref ExcelWorksheet worksheet, PropertyInfo[] props, List<T> data)
//        {
//            // Điền dữ liệu
//            for (var j = 0; j < data.Count; j++)
//            {
//                var rowData = data[j];

//                var rowIndex = j + 5; // Bắt đầu từ hàng thứ 5 để tránh ghi đè header

//                worksheet.Cells[rowIndex, 1].Value = j + 1;

//                int k = 2;
//                foreach (var prop in props)
//                {
//                    // Lấy dữ liệu
//                    var propValue = prop.GetValue(rowData, null);
//                    if (propValue == null)
//                    {
//                        worksheet.Cells[rowIndex, k].Value = null;
//                    }
//                    else
//                    {
//                        Type propType = prop.PropertyType;
//                        if (propType == typeof(DateTime) || propType == typeof(DateTime?))
//                        {
//                            worksheet.Cells[rowIndex, k].Value = ((DateTime)propValue).ToString("dd-MM-yyyy");
//                        }
//                        else if (propType.FullName.Contains("MISA.Web07.GPBL.Domain.Enums"))
//                        {
//                            // Get name resource value
//                            var resoureKey = $"Enum_{propValue}";
//                            var resourceValue = CommonFunction.GetResourceString(resoureKey);
//                            worksheet.Cells[rowIndex, k].Value = resourceValue;
//                        }
//                        else
//                        {
//                            worksheet.Cells[rowIndex, k].Value = propValue;
//                        }
//                    }
//                    k++;

//                }
//            }

//            // Chỉnh lại các cột fit với content
//            var cols = props.Length + 2;
//            for (int i = 2; i <= cols; i++)
//            {
//                worksheet.Column(i).AutoFit();
//            }
//        }

//        /// <summary>
//        /// Build header excel
//        /// </summary>
//        /// <param name="worksheet"></param>
//        /// <param name="props">các trường dữ liệu cần build</param>
//        /// <param name="rows">Số dòng</param>
//        /// <param name="title">Tiêu đề</param>
//        /// Created by: ntvu 29/09/2023
//        protected virtual void BuildExcelHeader(ref ExcelWorksheet worksheet, PropertyInfo[] props, int rows, string title)
//        {

//            // CSS Background cho header
//            var headerRow = worksheet.Row(4);

//            headerRow.Style.Fill.PatternType = ExcelFillStyle.Solid;

//            headerRow.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Gray);

//            worksheet.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

//            var titleCell = worksheet.Cells[1, 1, 2, props.Length];

//            titleCell.Merge = true; // Gộp ô cho tiêu đề

//            titleCell.Style.Font.Size = 14; // Kích thước chữ to

//            titleCell.Style.Font.Bold = true; // Chữ in đậm

//            titleCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Căn giữa

//            worksheet.Cells[1, 1].Value = title.ToUpper();

//            // Thêm cột STT
//            worksheet.Cells[4, 1].Value = "STT";

//            int i = 2;
//            foreach (var prop in props)
//            {
//                // Lấy tên cột
//                var displayName = prop.Name;
//                var displayClass = (MISADisplayName[])prop.GetCustomAttributes(typeof(MISADisplayName));

//                if (displayClass.Length > 0)
//                {
//                    displayName = displayClass[0].Name;
//                }

//                worksheet.Cells[4, i].Value = displayName;

//                // Lấy format cột
//                Type propType = prop.PropertyType;
//                if (propType == typeof(DateTime) || propType == typeof(DateTime?))
//                {
//                    worksheet.Column(i).Style.Numberformat.Format = "dd-MM-yyyy";
//                    worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                }
//                else if (propType.FullName.Contains("MISA.Web07.GPBL.Domain.Enums"))
//                {
//                    worksheet.Column(i).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
//                }


//                var cellRange = worksheet.Cells[4, i, rows + 4, i]; // Từ dòng 4 đến dòng cuối cùng

//                // chỉnh kiểu bảng: có border ngăn cách giữa các cột, dòng
//                var cellBorder = cellRange.Style.Border;
//                cellBorder.Left.Style = ExcelBorderStyle.Thin;
//                cellBorder.Right.Style = ExcelBorderStyle.Thin;
//                cellBorder.Top.Style = ExcelBorderStyle.Thin;
//                cellBorder.Bottom.Style = ExcelBorderStyle.Thin;


//                i++;

//            }
//        }
//    }
//}