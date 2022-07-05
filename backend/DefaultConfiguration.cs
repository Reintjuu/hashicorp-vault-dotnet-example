using System.Text.Json.Serialization;
using Microsoft.Extensions.Options;

namespace backend;

public class DefaultConfiguration : IOptions<DefaultConfiguration>
{
	public string SecretValue { get; set; }

	[JsonIgnore] public DefaultConfiguration Value => this;
}