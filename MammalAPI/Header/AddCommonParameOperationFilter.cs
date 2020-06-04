using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace MammalAPI.Header
{
    public class AddCommonParameOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.HttpMethod.Equals("POST") && context.ApiDescription.RelativePath.Contains("login"))
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "UserId",
                    In = ParameterLocation.Header,
                    Schema = new OpenApiSchema
                    {
                        Type = "String",
                    },
                    Required = true
                });
            }
        }
    }
}
