using Microsoft.AspNetCore.Mvc;
using VaultSharp;
using VaultSharp.V1.Commons;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class ConfigurationController : ControllerBase
{
	private readonly IVaultClient _vaultClient;

	public ConfigurationController(
		IVaultClient vaultClient)
	{
		_vaultClient = vaultClient;
	}

	[HttpGet("FromVaultApi")]
	public async Task<Secret<SecretData>> GetFromVaultApi(string path)
	{
		return await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(path);
	}

	[HttpPost("ToVaultApi")]
	public async Task PostToVaultApi([FromBody] Configuration configuration)
	{
		await _vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(
			configuration.Section,
			new Dictionary<string, object>
			{
				{ configuration.Key, configuration.Value }
			});
	}
}