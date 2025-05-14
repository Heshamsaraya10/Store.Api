using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
   public interface ICachService
   {
        Task<string?> GetAsync(string cachkey);

        Task SetAsync(string cachkey, object value, TimeSpan timeToLive);
    }
}
