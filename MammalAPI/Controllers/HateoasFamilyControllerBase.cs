using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MammalAPI.HATEOAS;
using MammalAPI.DTO;

namespace MammalAPI.Controllers
{
    public class HateoasFamilyControllerBase : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;

        public HateoasFamilyControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
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

        internal FamilyDTO HateoasMainLinks(FamilyDTO family)
        {
            FamilyDTO familyDto = family;

            familyDto.Links.Add(UrlLink("all", "GetAllFamily", null));
            familyDto.Links.Add(UrlLink("_self", "GetFamilyAsync", new { id = familyDto.FamilyID }));

            return familyDto;
        }
    }
}
