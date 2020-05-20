using System;
using System.Collections.Generic;
using System.Text;

namespace MammalAPI.HATEOAS
{
    public abstract class HateoasModelBase
    {
        public List<LinkDTO> Links { get; set; } = new List<LinkDTO>();
    }
}
