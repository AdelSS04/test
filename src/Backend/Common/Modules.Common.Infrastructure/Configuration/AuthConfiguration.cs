namespace Modules.Common.Infrastructure.Configuration;

public record AuthConfiguration
{
	public required string ClientId { get; init; }
	public required string Domain { get; init; }
	public required string Audience { get; init; }
}
