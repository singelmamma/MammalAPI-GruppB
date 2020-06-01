<<<<<<< HEAD
<<<<<<< HEAD
=======

﻿using MammalAPI.HATEOAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> 8250539f60ad285279dda3a90ef1546651631051
﻿using System.Collections.Generic;
=======
﻿using MammalAPI.HATEOAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> @MirkoPralica Tested HateOas

namespace MammalAPI.DTO
{
    public class FamilyDTO : HateoasLinkBase
    {
        public int FamilyID { get; set; }
        public string Name { get; set; }
        public ICollection<MammalDTO> Mammals { get; set; }
    }
}
