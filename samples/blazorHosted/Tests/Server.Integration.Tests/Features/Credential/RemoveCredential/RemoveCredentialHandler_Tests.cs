﻿namespace RemoveCredentialHandler
{
  using System.Threading.Tasks;
  using System.Text.Json;
  using Microsoft.AspNetCore.Mvc.Testing;
  using BlazorHosted.Server.Integration.Tests.Infrastructure;
  using BlazorHosted.Features.Credentials;
  using BlazorHosted.Server;
  using FluentAssertions;

  public class Handle_Returns : BaseTest
  {
    private readonly RemoveCredentialRequest RemoveCredentialRequest;

    public Handle_Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerOptions aJsonSerializerOptions
    ) : base(aWebApplicationFactory, aJsonSerializerOptions)
    {
      RemoveCredentialRequest = new RemoveCredentialRequest { Days = 10 };
    }

    public async Task RemoveCredentialResponse()
    {
      RemoveCredentialResponse RemoveCredentialResponse = await Send(RemoveCredentialRequest);

      ValidateRemoveCredentialResponse(RemoveCredentialResponse);
    }

    private void ValidateRemoveCredentialResponse(RemoveCredentialResponse aRemoveCredentialResponse)
    {
      aRemoveCredentialResponse.CorrelationId.Should().Be(RemoveCredentialRequest.CorrelationId);
      // check Other properties here
    }

  }
}