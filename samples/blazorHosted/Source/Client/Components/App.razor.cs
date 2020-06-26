namespace BlazorHosted.Components
{
  using BlazorHosted.Features.Bases;
  using BlazorHosted.Features.ClientLoaders;
  using BlazorState.Features.JavaScriptInterop;
  using BlazorState.Features.Routing;
  using BlazorState.Pipeline.ReduxDevTools;
  using Microsoft.AspNetCore.Components;
  using System.Threading.Tasks;
  using static BlazorHosted.Features.Connections.ConnectionState;
  using static BlazorHosted.Features.CredentialDefinitions.CredentialDefinitionState;
  using static BlazorHosted.Features.Credentials.CredentialState;
  using static BlazorHosted.Features.Schemas.SchemaState;
  using static BlazorHosted.Features.Wallets.WalletState;

  public partial class App : BaseComponent
  {
    [Inject] private ClientLoader ClientLoader { get; set; }
    [Inject] private JsonRequestHandler JsonRequestHandler { get; set; }
#if ReduxDevToolsEnabled
    [Inject] private ReduxDevToolsInterop ReduxDevToolsInterop { get; set; }
#endif
    [Inject] private RouteManager RouteManager { get; set; }

    protected override async Task OnAfterRenderAsync(bool aFirstRender)
    {
#if ReduxDevToolsEnabled
      await ReduxDevToolsInterop.InitAsync();
#endif
      await JsonRequestHandler.InitAsync();
      await ClientLoader.InitAsync();

      var tasks = new Task[]
      {
         Mediator.Send(new FetchWalletAction()),
         Mediator.Send(new FetchSchemasAction()),
         Mediator.Send(new FetchCredentialDefinitionsAction()),
         Mediator.Send(new FetchConnectionsAction()),
         //Mediator.Send(new FetchCredentialsAction())
      };

      await Task.WhenAll(tasks);
    }
  }
}
