
﻿using MammalAPI.HATEOAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
﻿using System.Collections.Generic;

namespace MammalAPI.DTO
{
    public class FamilyDTO : HateoasLinkBase
    {
        public int FamilyID { get; set; }
        public string Name { get; set; }
        public ICollection<MammalDTO> Mammals { get; set; }
    }
}
