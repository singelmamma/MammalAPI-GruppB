using System.Collections.Generic;

namespace MammalAPI.HATEOAS
{
        public abstract class HateoasLinkBase
        {
            public List<Link> Links { get; set; } = new List<Link>();
        }
}
