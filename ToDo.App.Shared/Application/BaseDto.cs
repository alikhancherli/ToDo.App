using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.App.Shared.Application
{
    public abstract class BaseDto<TId> where TId : struct, IEquatable<TId>
    {
        [Display(Name = "شناسه")]
        public TId Id { get; set; }
        [Display(Name = "تاریخ ایجاد")]
        public DateTimeOffset CreatedTimeUtc { get; set; }
        [Display(Name = "تاریخ ویرایش")]
        public DateTimeOffset? ModifiedTimeUtc { get; set; }
    }
}
