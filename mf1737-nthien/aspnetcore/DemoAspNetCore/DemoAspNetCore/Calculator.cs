namespace DemoAspNetCore
{
    public class Calculator
    {
        /// <summary>
        /// Tính 2 số nguyên
        /// </summary>
        /// 
        /// <param name="a">Số nguyên thứ nhất</param>
        /// <param name="b">Số nguyên thứ hai</param>
        /// 
        /// <returns>
        /// Tổng 2 số nguyên
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        public int Add(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// Tính hiệu 2 số nguyên
        /// </summary>
        /// 
        /// <param name="a">Số nguyên thứ nhất</param>
        /// <param name="b">Số nguyên thứ hai</param>
        /// 
        /// <returns>
        /// Hiệu 2 số nguyên
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        public int Sub(int x, int y)
        {
            return x - y;
        }


        /// <summary>
        /// Tính tích 2 số nguyên
        /// </summary>
        /// 
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="y">Số nguyên thứ hai</param>
        /// 
        /// <returns>
        /// Tích 2 số nguyên
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        public int Mul(int x, int y)
        {
            return x * y;
        }

        /// <summary>
        /// Tính thương 2 số nguyên
        /// </summary>
        /// 
        /// <param name="x">Số nguyên thứ nhất</param>
        /// <param name="b">Số nguyên thứ hai</param>
        /// 
        /// <returns>
        /// Thương 2 số nguyên
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        public int Div(int a, int b)
        {
            return a / b;
        }
    }
}
