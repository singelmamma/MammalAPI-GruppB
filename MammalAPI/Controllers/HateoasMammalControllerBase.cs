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
    public class HateoasMammalControllerBase : ControllerBase
    {
        private readonly IReadOnlyList<ActionDescriptor> _routes;

        public HateoasMammalControllerBase(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
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

        /// <summary>
        /// Use this method for your mainlinks. Eg CRUD, just add
        /// more links to your CRUD methods inside
        /// Wollter
        /// </summary>
        /// <param name="mammal"></param>
        /// <returns></returns>
        internal MammalDTO HateoasMainLinks(MammalDTO mammal)
        {
            MammalDTO mammalDto = mammal;

            mammalDto.Links.Add(UrlLink("all", "GetAll", null));
            mammalDto.Links.Add(UrlLink("_self", "GetMammalAsync", new { id = mammalDto.MammalID }));

            return mammalDto;
        }
    }
}
