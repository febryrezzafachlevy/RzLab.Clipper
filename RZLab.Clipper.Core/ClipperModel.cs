using System;

public record Segment(TimeSpan Start, TimeSpan End, string Reason = "");

public class SummaryPoint
{
    public string Timestamp { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Scene { get; set; } = string.Empty;
    public string Highlight { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
}
public class AppSettingModel
{
    public OpenAIModel OpenAI { get; set; } = new();
    public PathsModel Paths { get; set; } = new();
}
public class OpenAIModel
{
    public string ApiKey { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
}
public class PathsModel
{
    public string WhisperModelPath { get; set; } = string.Empty;
    public string FFmpegPath { get; set; } = string.Empty;
}