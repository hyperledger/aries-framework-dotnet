﻿namespace BlazorHosted.Features.Wallets.Pages
{
  using BlazorHosted.Features.Bases;
  using System.Threading.Tasks;
  using static BlazorHosted.Features.Wallets.WalletState;

  public partial class WalletPage : BaseComponent
  {
    public const string RouteTemplate = "/wallet";

    public static string GetRoute() => RouteTemplate;

    protected override async Task OnInitializedAsync() =>
     _ = await Mediator.Send(new FetchWalletAction());

  }
}