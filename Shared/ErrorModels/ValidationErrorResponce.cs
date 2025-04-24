using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ErrorModels
{
    public class ValidationErrorResponce
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public IEnumerable<ValidationError>? Errors { get; set; }
    }

    public class ValidationError
    {
        public string Key { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}

