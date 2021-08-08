using System.Collections;
using System.Collections.Generic;

namespace ApiForReact.Models
{
    public class BaseResult<T>
    {
        public int ResultCode { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
