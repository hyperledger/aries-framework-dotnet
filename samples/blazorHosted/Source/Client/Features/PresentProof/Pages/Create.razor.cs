﻿namespace BlazorHosted.Features.PresentProofs.Pages
{
  using BlazorHosted.Features.Bases;
  using Hyperledger.Aries.Features.DidExchange;
  using Hyperledger.Aries.Features.PresentProof;
  using Hyperledger.Aries.Models.Records;
  using Microsoft.AspNetCore.Components;
  using Microsoft.JSInterop;
  using Newtonsoft.Json;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Threading.Tasks;
  using static BlazorHosted.Features.Connections.ConnectionState;
  using static BlazorHosted.Features.CredentialDefinitions.CredentialDefinitionState;
  using static BlazorHosted.Features.PresentProofs.PresentProofState;
  using static BlazorHosted.Features.Schemas.SchemaState;
  using static BlazorState.Features.Routing.RouteState;

  public partial class Create: BaseComponent
  {
    public const string RouteTemplate = "/proofs/create";

    private IReadOnlyList<ConnectionRecord> Connections => ConnectionState.ConnectionsAsList;
    public string CredentialDefinitionId { get; set; }
    public CreateProofRequestRequest CreateProofRequestRequest { get; set; }
    [Inject] protected IJSRuntime JSRuntime { get; set; }
    private IReadOnlyList<DefinitionRecord> CredentialDefintions => CredentialDefinitionState.CredentialDefinitionsAsList;

    public static string GetRoute() => RouteTemplate;

    protected async Task CancelClick() =>
      _ = await Mediator.Send(new ChangeRouteAction { NewRoute = Pages.Index.RouteTemplate });

    protected async Task CopyToClipboardAsync()
    {
      await JSRuntime.InvokeAsync<object>("navigator.clipboard.writeText", PresentProofState.ProofRequestUrl);
    }

    protected async Task HandleValidSubmit()
    {
      _ = await Mediator.Send(new CreateProofRequestAction { CreateProofRequestRequest = CreateProofRequestRequest });
      _ = await Mediator.Send(new ChangeRouteAction { NewRoute = Pages.Index.RouteTemplate });
    }

    protected void OnCredentialDefintionSelect()
    {
      CreateProofRequestRequest.ProofRequest.RequestedAttributes.Clear();

      if (!string.IsNullOrEmpty(CredentialDefinitionId))
      {
        Console.WriteLine("Adding All Attributes");
        DefinitionRecord definitionRecord = CredentialDefinitionState.CredentialDefinitions[CredentialDefinitionId];
        string schemaId = definitionRecord.SchemaId;
        SchemaRecord schemaRecord = SchemaState.Schemas[schemaId];
        CreateProofRequestRequest.ProofRequest.Name = schemaRecord.Name;
        CreateProofRequestRequest.ProofRequest.Version = schemaRecord.Version;
        foreach (string name in schemaRecord.AttributeNames)
        {
          var restrictions = new List<AttributeFilter>();

          var attributeFilter = new AttributeFilter
          {
            CredentialDefinitionId = definitionRecord.Id
          };

          restrictions.Add(attributeFilter);

          var proofAttributeInfo = new ProofAttributeInfo()
          {
            Name = name,
            Restrictions = restrictions,
          };

          CreateProofRequestRequest.ProofRequest
            .RequestedAttributes
            .Add($"0_{name}_uuid", proofAttributeInfo);
        }

        Console.WriteLine($"Added:{CreateProofRequestRequest.ProofRequest.RequestedAttributes.Count}");
      }
    }

    protected override async Task OnInitializedAsync()
    {
      CreateProofRequestRequest = new CreateProofRequestRequest
      {
        ProofRequest = new ProofRequest { RequestedAttributes = new Dictionary<string, ProofAttributeInfo>() }
      };

      _ = await Mediator.Send(new FetchSchemasAction());
      _ = await Mediator.Send(new FetchConnectionsAction());
      _ = await Mediator.Send(new FetchCredentialDefinitionsAction());

      CreateProofRequestRequest.ConnectionId = Connections.FirstOrDefault()?.Id;
      CredentialDefinitionId = CredentialDefintions.FirstOrDefault()?.Id;
      if (!string.IsNullOrEmpty(CredentialDefinitionId))
      {
        OnCredentialDefintionSelect();
      }

      await base.OnInitializedAsync();
    }
  }
}
