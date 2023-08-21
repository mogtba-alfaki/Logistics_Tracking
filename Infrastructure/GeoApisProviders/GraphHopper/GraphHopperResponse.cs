using System.Text.Json.Serialization;

public class GraphHopperResponse
    {
        [JsonPropertyName("hints")]
        public Hints Hints { get; set; }

        [JsonPropertyName("info")]
        public Info Info { get; set; }

        [JsonPropertyName("paths")]
        public Path[] Paths { get; set; }
    }

    public partial class Hints
    {
        [JsonPropertyName("visited_nodes.sum")]
        public long VisitedNodesSum { get; set; }

        [JsonPropertyName("visited_nodes.average")]
        public double VisitedNodesAverage { get; set; }
    }

    public partial class Info
    {
        [JsonPropertyName("copyrights")]
        public string[] Copyrights { get; set; }

        [JsonPropertyName("took")]
        public long Took { get; set; }
    }

    public partial class Path
    {
        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("transfers")]
        public long Transfers { get; set; }

        [JsonPropertyName("points_encoded")]
        public bool PointsEncoded { get; set; }

        [JsonPropertyName("bbox")]
        public double[] Bbox { get; set; }

        [JsonPropertyName("points")]
        public string Points { get; set; }

        [JsonPropertyName("instructions")]
        public Instruction[] Instructions { get; set; }

        [JsonPropertyName("legs")]
        public object[] Legs { get; set; }

        [JsonPropertyName("details")]
        public Details Details { get; set; }

        [JsonPropertyName("ascend")]
        public double Ascend { get; set; }

        [JsonPropertyName("descend")]
        public double Descend { get; set; }

        [JsonPropertyName("snapped_waypoints")]
        public string SnappedWaypoints { get; set; }
    }

    public partial class Details
    {
    }

    public partial class Instruction
    {
        [JsonPropertyName("distance")]
        public double Distance { get; set; }

        [JsonPropertyName("heading")]
        public double? Heading { get; set; }

        [JsonPropertyName("sign")]
        public long Sign { get; set; }

        [JsonPropertyName("interval")]
        public long[] Interval { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }

        [JsonPropertyName("street_name")]
        public string StreetName { get; set; }

        [JsonPropertyName("last_heading")]
        public double? LastHeading { get; set; }
    }