namespace BlazorHosted.Features.Connections
{
  using MediatR;
  using BlazorHosted.Features.Bases;

  public class GetConnectionRequest : BaseApiRequest, IRequest<GetConnectionResponse>
  {
    public const string RouteTemplate = "api/connections/{ConnectionId}";

    /// <summary>
    /// The Id of the Connection
    /// </summary>
    /// <example>Connection identifier</example>
    public string ConnectionId { get; set; } = null!;

    internal override string GetRoute()
    {
      string temp = RouteTemplate.Replace($"{{{nameof(ConnectionId)}}}", ConnectionId, System.StringComparison.Ordinal);
      return $"{temp}?{nameof(CorrelationId)}={CorrelationId}";
    }

    public GetConnectionRequest() { }
    public GetConnectionRequest(string aConnectionId)
    {
      ConnectionId = aConnectionId;
    }
  }
}