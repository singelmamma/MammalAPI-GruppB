using AutoMapper;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace MammalAPI.Controllers
{
    public class HateoasControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;
        private readonly IMapper _mapper;

        public HateoasControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider, IMapper mapper)
        {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            _mapper = mapper;
        }
    }
}
