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

        public HateoasControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _routes = actionDescriptorCollectionProvider.ActionDescriptors.Items;
        }

        internal Link UrlLink(string relation, string routeName, object values)
        {
            var route = _routes.FirstOrDefault(f => f.AttributeRouteInfo.Name == routeName);
            var method = route.ActionConstraints.OfType<HttpMethodActionConstraint>().First().HttpMethods.First();
            var url = Url.Link(routeName, values).ToLower();
            return new Link(url, relation, method);
        }

        internal MammalDTO HateoasMammalLink(MammalDTO mammal)
        {
            MammalDTO mammalDto = mammal;

            mammalDto.Links.Add(UrlLink("all", "GetAll", mammalDto.MammalID));
            mammalDto.Links.Add(UrlLink("_self", "GetMammalAsync", new { id = mammalDto.MammalID }));

            //mammalDto.Links.Add(
            //    UrlLink("addresses",
            //            "GetAddressesByClient",
            //            new { id = mammalDto.MammalID }));

            return mammalDto;
        }
    }
}
