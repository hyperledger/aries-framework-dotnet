namespace BlazorHosted.Features.Schemas
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using BlazorHosted.Features.Bases;

  public class GetSchemaEndpoint : BaseEndpoint<GetSchemaRequest, GetSchemaResponse>
  {
    /// <summary>
    /// Your summary these comments will show in the Open API Docs
    /// </summary>
    /// <param name="aGetSchemaRequest"><see cref="GetSchemaRequest"/></param>
    /// <returns><see cref="GetSchemaResponse"/></returns>
    [HttpGet(GetSchemaRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(GetSchemaResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process(GetSchemaRequest aGetSchemaRequest) => await Send(aGetSchemaRequest);
  }
}
