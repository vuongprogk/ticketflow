using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.ResponseObj
{
    public class DefaultResponse
    {
        public bool IsSuccess {get;set;} = false;
        public string Message { get; set; } = string.Empty;
        public Object? Result {get;set;} = default;
    }
}