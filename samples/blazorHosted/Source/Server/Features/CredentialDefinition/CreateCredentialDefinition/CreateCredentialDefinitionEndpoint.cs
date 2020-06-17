namespace BlazorHosted.Features.CredentialDefinitions
{
  using Microsoft.AspNetCore.Mvc;
  using Swashbuckle.AspNetCore.Annotations;
  using System.Net;
  using System.Threading.Tasks;
  using BlazorHosted.Features.Bases;

  public class CreateCredentialDefinitionEndpoint : BaseEndpoint<CreateCredentialDefinitionRequest, CreateCredentialDefinitionResponse>
  {
    /// <summary>
    /// Sends a credential definition to the ledger
    /// </summary>
    /// <param name="aCreateCredentialDefinitionRequest"><see cref="CreateCredentialDefinitionRequest"/></param>
    /// <returns><see cref="CreateCredentialDefinitionResponse"/></returns>
    [HttpPost(CreateCredentialDefinitionRequest.Route)]
    [SwaggerOperation(Tags = new[] { FeatureAnnotations.FeatureGroup })]
    [ProducesResponseType(typeof(CreateCredentialDefinitionResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Process([FromBody]CreateCredentialDefinitionRequest aCreateCredentialDefinitionRequest) => await Send(aCreateCredentialDefinitionRequest);
  }
}
