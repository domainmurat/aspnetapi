using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace UserInfo.WebApi
{
    public class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/token", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> { "Auth" },
                    consumes = new List<string>
                {
                    "application/x-www-form-urlencoded"
                },
                    parameters = new List<Parameter> {
                    new Parameter
                    {
                        type = "string",
                        name = "grant_type",
                        required = true,
                        @in = "formData",
                        description="password"
                    },
                    new Parameter
                    {
                        type = "string",
                        name = "username",
                        required = false,
                        @in = "formData",
                        description="soyhanbeyazit or soyhan.beyazit@mobilion.com.tr"
                    },
                    new Parameter
                    {
                        type = "string",
                        name = "password",
                        required = false,
                        @in = "formData",
                        description="admin1"
                    }
                }
                }
            });
        }

        //public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        //{
        //    // Determine if the operation has the Authorize attribute
        //    var authorizeAttributes = apiDescription
        //        .ActionDescriptor.GetCustomAttributes<AuthorizeAttribute>();

        //    if (!authorizeAttributes.Any())
        //        return;

        //    // Initialize the operation.security property
        //    if (operation.security == null)
        //        operation.security = new List<IDictionary<string, IEnumerable<string>>>();

        //    // Add the appropriate security definition to the operation
        //    var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
        //{
        //    { "oauth2", Enumerable.Empty<string>() }
        //};

        //    operation.security.Add(oAuthRequirements);
        //}

        //public void Apply(Operation operation, OperationFilterContext context)
        //{
        //    var filterPipeline = context.ApiDescription.ActionDescriptor.FilterDescriptors;
        //    var isAuthorized = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
        //    var allowAnonymous = filterPipeline.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

        //    if (isAuthorized && !allowAnonymous)
        //    {
        //        if (operation.Parameters == null)
        //            operation.Parameters = new List<IParameter>();

        //        operation.Parameters.Add(new NonBodyParameter
        //        {
        //            Name = "Authorization",
        //            In = "header",
        //            Description = "access token",
        //            Required = true,
        //            Type = "string"
        //        });
        //    }
        //}
    }
}