using System;
using System.Collections.Generic;
using System.Text;

namespace MammalAPI.HATEOAS
{
    public class LinkedCollectionResourceWrapperDto<T> : HateoasModelBase
        where T : HateoasModelBase
    {
        public IEnumerable<T> Value { get; set; }

        public LinkedCollectionResourceWrapperDto(IEnumerable<T> value)
        {
            this.Value = value;
        }
    }
}
