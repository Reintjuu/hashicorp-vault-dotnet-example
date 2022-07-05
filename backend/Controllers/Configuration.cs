namespace backend.Controllers;

public class Configuration
{
	public const string DefaultSectionName = $"{nameof(backend)}/{nameof(DefaultConfiguration)}";
	public const string DefaultKeyName = nameof(DefaultConfiguration.SecretValue);

	public string Section { get; set; } = DefaultSectionName;
	public string Key { get; set; } = DefaultKeyName;
	public string Value { get; set; }
}