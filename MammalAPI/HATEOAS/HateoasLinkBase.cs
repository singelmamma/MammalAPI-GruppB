using System;
using System.Collections.Generic;
using System.Text;

namespace MammalAPI.HATEOAS
{
        public abstract class HateoasLinkBase
        {
            public List<Link> Links { get; set; } = new List<Link>();
        }
}
