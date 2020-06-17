namespace BlazorHosted.Features.Schemas
{
  using MediatR;
  using BlazorHosted.Features.Bases;

  public class GetSchemaRequest : BaseApiRequest, IRequest<GetSchemaResponse>
  {
    public const string Route = "api/Schemas/GetSchema";

    /// <summary>
    /// The Number of days of forecasts to get
    /// </summary>
    /// <example>5</example>
    public int Days { get; set; }

    internal override string RouteFactory => $"{Route}?{nameof(Days)}={Days}&{nameof(CorrelationId)}={CorrelationId}";
  }
}