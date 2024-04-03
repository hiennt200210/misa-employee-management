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
        /// Tính tổng các số nguyên
        /// </summary>
        /// 
        /// <param name="operands">Một chuỗi các số nguyên phân tách với nhau bởi dấu phẩy</param>
        /// 
        /// <returns>
        /// Chuỗi rỗng - Trả về 0
        /// Chuỗi chứa giá trị âm - Ném ra ngoại lệ FormatException
        /// Chuỗi hợp lệ - Trả về tổng các số nguyên
        /// </returns>
        /// 
        /// CreatedBy: hiennt200210 (12/09/2023)
        public int Add(string operands)
        {
            // Nếu chuỗi rỗng thì trả về 0
            if (string.IsNullOrEmpty(operands))
            {
                return 0;
            }

            // Cắt chuỗi thành mảng các chuỗi toán hạng
            string[] numbers = operands.Split(",");

            bool valid = true;
            var sum = 0;
            var message = "Không chấp nhận toán hạng âm:";

            // Kiểm tra từng chuỗi toán hạng
            foreach (var item in numbers)
            {
                int value;

                try
                {
                    value = int.Parse(item);
                }
                catch (FormatException)
                {
                    throw new FormatException("Chuỗi không hợp lệ!");
                }

                if (value < 0)
                {
                    message += item.ToString();
                    message += ",";
                    valid = false;
                }
                else
                {
                    sum += value;
                }
            }

            // Nếu có chuỗi toán hạng chứa giá trị âm thì ném ra ngoại lệ với thông báo
            if (!valid)
            {
                message = message.TrimEnd(',');
                throw new FormatException(message);
            }
            return sum;
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
