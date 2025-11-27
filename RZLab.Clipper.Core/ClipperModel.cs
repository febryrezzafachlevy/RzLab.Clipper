using System;

public record Segment(TimeSpan Start, TimeSpan End, string Reason = "");
