using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using VaultSharp;
using VaultSharp.V1.Commons;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class DefaultConfigurationController : ControllerBase
{
	private readonly IVaultClient _vaultClient;
	private readonly IOptionsSnapshot<DefaultConfiguration> _defaultConfigurationSnapshot;

	public DefaultConfigurationController(
		IVaultClient vaultClient,
		IOptionsSnapshot<DefaultConfiguration> defaultConfigurationSnapshot)
	{
		_vaultClient = vaultClient;
		_defaultConfigurationSnapshot = defaultConfigurationSnapshot;
	}

	[HttpGet("FromVaultApi")]
	public async Task<Secret<SecretData>> GetDefaultConfigurationFromVaultApi()
	{
		return await _vaultClient.V1.Secrets.KeyValue.V2.ReadSecretAsync(Configuration.DefaultSectionName);
	}

	[HttpPost("ToVaultApi")]
	public async Task PostDefaultConfigurationToVaultApi(string value)
	{
		await _vaultClient.V1.Secrets.KeyValue.V2.WriteSecretAsync(
			Configuration.DefaultSectionName,
			new Dictionary<string, object>
			{
				{ Configuration.DefaultKeyName, value }
			});
	}

	[HttpGet("FromLocal")]
	public DefaultConfiguration GetDefaultConfigurationFromLocal()
	{
		return _defaultConfigurationSnapshot.Value;
	}
}