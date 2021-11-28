using System.Collections.Generic;

namespace OQM10P_HFT_2021221.Models.ResponseObjects
{
    public class ApiResult
    {
        public bool isSuccess { get; set; }
        public List<string> errorMessages { get; set; }

        public ApiResult()
        {

        }

        public ApiResult(bool isSuccess)
        {
            this.isSuccess = isSuccess;
        }

        public ApiResult(bool isSuccess, List<string> errorMessages) : this(isSuccess)
        {
            this.errorMessages = errorMessages;
        }
    }
}
