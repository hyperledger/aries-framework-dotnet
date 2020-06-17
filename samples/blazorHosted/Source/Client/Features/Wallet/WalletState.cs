﻿namespace BlazorHosted.Features.Wallets
{
  using BlazorState;
  using System;

  internal partial class WalletState : State<WalletState>
  {
    public string Name { get; private set; }
    public string Uri { get; private set; }
    public string Did { get; private set; }
    public string VerKey { get; private set; }

    public WalletState() { }

    /// <summary>
    /// Set the Initial State
    /// </summary>
    public override void Initialize()
    {
      Name = string.Empty;
      Uri = string.Empty;
      Did = string.Empty;
      VerKey = string.Empty;
    }
  }
}