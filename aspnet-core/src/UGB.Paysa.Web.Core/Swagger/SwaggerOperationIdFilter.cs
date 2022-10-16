using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UGB.Paysa.Web.Swagger
{
    public class SwaggerOperationIdFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.OperationId = FriendlyId(context.ApiDescription);
        }

        private static string FriendlyId(ApiDescription apiDescription)
        {
            var parts = (RelativePathSansQueryString(apiDescription) + "/" + apiDescription.HttpMethod.ToLower())
                .Split('/');

            var builder = new StringBuilder();
            foreach (var part in parts)
            {
                var trimmed = part.Trim('{', '}');
                builder.AppendFormat("{0}{1}",
                    (part.StartsWith("{") ? "By" : string.Empty),
                    CultureInfo.InvariantCulture.TextInfo.ToTitleCase(trimmed)
                );
            }

            return builder.ToString();
        }

        private static string RelativePathSansQueryString(ApiDescription apiDescription)
        {
            return apiDescription.RelativePath.Split('?').First();
        }
    }

    public class CustomSwaggerFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var routes = swaggerDoc.Paths.Where(x => !x.Key.ToLower().Contains("chambres")
                                                       && !x.Key.ToLower().Contains("cartes")
                                                       && !x.Key.ToLower().Contains("etudiants")
                                                       && !x.Key.ToLower().Contains("comptes")
                                                       && !x.Key.ToLower().Contains("terminal")
                                                       && !x.Key.ToLower().Contains("points")
                                                       && !x.Key.ToLower().Contains("gerants")
                                                       && !x.Key.ToLower().Contains("operation")).ToList();
            routes.ForEach(x => { swaggerDoc.Paths.Remove(x.Key); });
        }
    }
}
