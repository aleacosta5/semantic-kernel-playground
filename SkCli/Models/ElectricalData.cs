using System.Text.Json.Serialization;

namespace SkCli.Models;

/// <summary>
/// Sample class to represent electrical data with a timestamp, machine ID, and measurement.
/// </summary>
public class ElectricalData
{
    [JsonPropertyName("timestamp")]
    public string TimeStamp { get; set; }

    [JsonPropertyName("machine_id")]
    public string MachineId { get; set; }

    [JsonPropertyName("measurement")]
    public double Measurement { get; set; }
}