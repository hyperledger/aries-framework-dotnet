namespace BlazorHosted.Features.XXXs
{
  using System;

  /// <summary>
  /// The object that is passed back and forth from the Server to the client.
  /// </summary>
  /// <remarks>TODO: This should be an immutable class
  /// but serialization doesn't work with no setter or private setter yet</remarks>
  public class XXXDto
  {
    public DateTime Date { get; set; }

    public string Summary { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public XXXDto() { }

    public XXXDto(DateTime aDate, string aSummary, int aTemperatureC)
    {
      Date = aDate;
      Summary = aSummary;
      TemperatureC = aTemperatureC;
    }
  }
}
