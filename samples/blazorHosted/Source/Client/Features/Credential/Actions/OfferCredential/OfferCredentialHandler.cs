﻿namespace BlazorHosted.Features.Credentials
{
  using BlazorHosted.Features.Bases;
  using BlazorHosted.Features.IssueCredentials;
  using BlazorState;
  using MediatR;
  using System.Net.Http;
  using System.Net.Http.Json;
  using System.Threading;
  using System.Threading.Tasks;

  internal partial class CredentialState
  {
    public class OfferCredentialHandler : BaseHandler<OfferCredentialAction>
    {
      private readonly HttpClient HttpClient;

      public OfferCredentialHandler
      (
        IStore aStore,
        HttpClient aHttpClient
      ) : base(aStore)
      {
        HttpClient = aHttpClient;
      }

      public override async Task<Unit> Handle
      (
        OfferCredentialAction aOfferCredentialAction,
        CancellationToken aCancellationToken
      )
      {
        OfferCredentialRequest offerCredentialRequest =
          aOfferCredentialAction.OfferCredentialRequest;
        System.Console.WriteLine("====================");
        System.Console.WriteLine(offerCredentialRequest.CredentialPreviewAttributes[0].Value);
        System.Console.WriteLine("====================");

        HttpResponseMessage httpResponseMessage =
          await HttpClient.PostAsJsonAsync<OfferCredentialRequest>
          (
            offerCredentialRequest.GetRoute(),
            offerCredentialRequest
          );

        httpResponseMessage.EnsureSuccessStatusCode();

        return Unit.Value;
      }
    }
  }
}
