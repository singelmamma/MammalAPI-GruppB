using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MammalAPI.HATEOAS;
using MammalAPI.DTO;
using MammalAPI.Models;

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

        /// <summary>
        /// Overload 1, Use this method for your mainlinks. Eg CRUD, just add
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
            mammalDto.Links.Add(UrlLink("_self", "GetMammalName", new {mammalName = mammalDto.Name }));



            return mammalDto;
        }

        /// <summary>
        /// Overload 2, adds HATEOAS links to supplied object.
        /// Hampus Kjellstrand
        /// </summary>
        /// <param name="family"></param>
        /// <returns></returns>
        internal FamilyDTO HateoasMainLinks(FamilyDTO family)
        {
            FamilyDTO familyDto = family;

            familyDto.Links.Add(UrlLink("all", "GetAllFamilies", null));
            familyDto.Links.Add(UrlLink("_self", "GetFamilyByIdAsync", new { id = familyDto.FamilyID }));

            //CRUD
            familyDto.Links.Add(UrlLink(null, "PostFamily", null));
            familyDto.Links.Add(UrlLink("_self", "PutFamily", familyDto));
            familyDto.Links.Add(UrlLink("_self", "DeleteFamily", familyDto));

            return familyDto;
        }

        /// <summary>
        /// Overload 3, adds HATEOAS links to supplied object.
        /// Ted Henriksson
        /// </summary>
        /// <param name="habitat"></param>
        /// <returns></returns>
        internal HabitatDTO HateoasMainLinks(HabitatDTO habitat)
        {
            HabitatDTO habitatDTO = habitat;

            habitatDTO.Links.Add(UrlLink("all", "GetAllHabitat", null));
            habitatDTO.Links.Add(UrlLink("_self", "GetHabitatByID", new { id = habitatDTO.HabitatID }));

            return habitatDTO;
        }
    }
}
