namespace BlazorHosted.Features.Wallets
{
  using BlazorHosted.Features.Bases;
  using BlazorState;
  using MediatR;
  using Newtonsoft.Json;
  using System.Linq;
  using System.Net.Http;
  using System.Threading;
  using System.Threading.Tasks;

  internal partial class WalletState
  {
    public class FetchWallteHandler : BaseHandler<FetchWalletAction>
    {
      private readonly HttpClient HttpClient;

      public FetchWallteHandler(IStore aStore, HttpClient aHttpClient) : base(aStore)
      {
        HttpClient = aHttpClient;
      }

      public override async Task<Unit> Handle
      (
        FetchWalletAction aFetchWalletAction,
        CancellationToken aCancellationToken
      )
      {
        if (!WalletState.IsCached)
        {
          var getWalletRequest = new GetWalletRequest();
          HttpResponseMessage x = await HttpClient.GetAsync(getWalletRequest.GetRoute());
          string json = await x.Content.ReadAsStringAsync();
          GetWalletResponse getWalletResponse = JsonConvert.DeserializeObject<GetWalletResponse>(json);
          WalletState.Did = getWalletResponse.ProvisioningRecord.Endpoint.Did;
          WalletState.VerKey = getWalletResponse.ProvisioningRecord.Endpoint.Verkey.First();
          WalletState.Uri = getWalletResponse.ProvisioningRecord.Endpoint.Uri;
          WalletState.Name = getWalletResponse.ProvisioningRecord.Owner.Name;
        }

        return Unit.Value;
      }
    }
  }
}
