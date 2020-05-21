using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using AutoMapper;

using MammalAPI.HATEOAS;
using MammalAPI.Models;
using MammalAPI.DTO;

namespace MammalAPI.Controllers
{
    public class HateoasControllerBase : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;
        //private readonly IMapper _mapper;

        public HateoasControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;
            //_mapper = mapper;
        }

        internal Link UrlLink(string relation, string routeName, object values)
        {
            var route = _routes.FirstOrDefault(f => f.AttributeRouteInfo.Name == routeName);
            var method = route.ActionConstraints.OfType<HttpMethodActionConstraint>().First().HttpMethods.First();
            var url = Url.Link(routeName, values).ToLower();
            return new Link(url, relation, method);
        }

        internal MammalDTO RestfulClient(MammalDTO mammal)
        {
            MammalDTO mammalDto = mammal;

            mammalDto.Links.Add(UrlLink("all", "GetClients", null));
            mammalDto.Links.Add(UrlLink("_self", "GetClientAsync", mammalDto.MammalID));

            //mammalDto.Links.Add(
            //    UrlLink("addresses",
            //            "GetAddressesByClient",
            //            new { id = mammalDto.MammalID }));

            return mammalDto;
        }
    }
}
