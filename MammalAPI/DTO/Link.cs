using System;
using System.Collections.Generic;
using System.Text;

namespace MammalAPI.DTO
{
    public class Link
    {        
        public string Href { get; private set; }
        public string Rel { get; private set; }
        public string Type { get; private set; }

        public Link(string href, string rel, string type)
        {
            Href = href;
            Rel = rel;
            Type = type;
        }
    }
}
