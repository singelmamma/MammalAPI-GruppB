using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MammalAPI.Authentication
{
    
    public class HeaderParameterSwagger : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;

           

            if(descriptor != null && context.ApiDescription.HttpMethod.Equals("POST") || context.ApiDescription.HttpMethod.Equals("PUT") || context.ApiDescription.HttpMethod.Equals("DELETE"))
            {
                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "APIUsername",
                    In = ParameterLocation.Header,                  
                    Required = true
                   
                });

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "APIPassword",
                    In = ParameterLocation.Header,
                    Required = true
                });
            }
        }
    }
}
