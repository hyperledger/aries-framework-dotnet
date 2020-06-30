﻿namespace GetWalletEndpoint
{
  using BlazorHosted.Features.Wallets;
  using BlazorHosted.Server;
  using BlazorHosted.Server.Integration.Tests.Infrastructure;
  using FluentAssertions;
  using Microsoft.AspNetCore.Mvc.Testing;
  using Newtonsoft.Json;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading.Tasks;

  public class Returns : BaseTest
  {
    private readonly GetWalletRequest GetWalletRequest;

    public Returns
    (
      WebApplicationFactory<Startup> aWebApplicationFactory,
      JsonSerializerSettings aJsonSerializerSettings
    ) : base(aWebApplicationFactory, aJsonSerializerSettings)
    {
      GetWalletRequest = new GetWalletRequest();
    }

    public async Task GetWalletResponse_using_Json_Net()
    {
      GetWalletResponse getWalletResponse =
        await GetJsonAsync<GetWalletResponse>(GetWalletRequest.GetRoute());

      ValidateGetWalletResponse(GetWalletRequest, getWalletResponse);
    }

    public async Task GetWalletResponse_using_System_Text_Json()
    {
      GetWalletResponse getWalletResponse =
        await HttpClient.GetFromJsonAsync<GetWalletResponse>(GetWalletRequest.GetRoute());

      ValidateGetWalletResponse(GetWalletRequest, getWalletResponse);
    }

    // There are no validation requirements for this request
    public void ValidationError() { }

    internal static void ValidateGetWalletResponse(GetWalletRequest aGetWalletRequest, GetWalletResponse aGetWalletResponse)
    {
      aGetWalletResponse.CorrelationId.Should().Be(aGetWalletRequest.CorrelationId);

      aGetWalletResponse.ProvisioningRecord.Should().NotBeNull();
      // Marked with Json Ignore? 
      aGetWalletResponse.ProvisioningRecord.CreatedAtUtc.HasValue.Should().BeTrue();
      aGetWalletResponse.ProvisioningRecord.DefaultPaymentAddressId.Should().BeNull(); // Faber Agent is not configured with Payment Adress

      aGetWalletResponse.ProvisioningRecord.Endpoint.Should().NotBeNull();

      // Example: "NPgCqrawdq6zpHgTpza186" Changes every time provisioned so just check for length
      aGetWalletResponse.ProvisioningRecord.Endpoint.Did.Length.Should().Be(22);

      aGetWalletResponse.ProvisioningRecord.Endpoint.Uri.Should().Be("https://localhost:5551");

      // Example: "CfDDJbu6VRrKNhTAgy4uTEeRwu4WwJgmGYvqNYxnmKvg" Changes every time provisioned so just check for length
      aGetWalletResponse.ProvisioningRecord.Endpoint.Verkey[0].Length.Should().Be(44);

      aGetWalletResponse.ProvisioningRecord.Id.Should().Be("SingleRecord");
      aGetWalletResponse.ProvisioningRecord.IssuerDid.Should().Be("Th7MpTaRZVRYnPiabds81Y");
      aGetWalletResponse.ProvisioningRecord.IssuerSeed.Should().Be("000000000000000000000000Steward1");
      aGetWalletResponse.ProvisioningRecord.IssuerVerkey.Should().Be("FYmoFw55GeQH7SRFa37dkx1d2dZ3zUF8ckg7wmL7ofN4");

      // Example: "0022b6a6-799a-41ae-8142-41811625216c" Changes every time provisioned so just check for length
      aGetWalletResponse.ProvisioningRecord.MasterSecretId.Length.Should().Be(36);

      aGetWalletResponse.ProvisioningRecord.Owner.Should().NotBeNull();
      aGetWalletResponse.ProvisioningRecord.Owner.ImageUrl.Should().BeNull();
      aGetWalletResponse.ProvisioningRecord.Owner.Name.Should().Be("Faber");

      aGetWalletResponse.ProvisioningRecord.TaaAcceptance.Should().BeNull();

      aGetWalletResponse.ProvisioningRecord.TailsBaseUri.Should().Be("https://localhost:5551/tails/");
      aGetWalletResponse.ProvisioningRecord.TypeName.Should().Be("AF.ProvisioningRecord");
      aGetWalletResponse.ProvisioningRecord.UpdatedAtUtc.Should().BeNull();
    }
  }
}
