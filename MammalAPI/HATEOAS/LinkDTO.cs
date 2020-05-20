﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MammalAPI.HATEOAS
{
    class LinkDTO
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }
        public LinkDTO(string href, string rel, string method)
        {
            this.Href = href;
            this.Rel = rel;
            this.Method = method;
        }
    }
}
