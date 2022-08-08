using VaultSharp;
using VaultSharp.Extensions.Configuration;
using VaultSharp.V1.AuthMethods.Token;

namespace backend;

internal static class Program
{
	private static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		const string mountPoint = "secret";
		builder.Configuration.AddVaultConfiguration(
			() => new VaultOptions(
				builder.Configuration.GetConnectionString("vault"),
				// TODO: Should actually be app/role authentication.
				builder.Configuration["VAULT_ROOT_TOKEN"],
				reloadOnChange: true,
				reloadCheckIntervalSeconds: 1),
			nameof(backend),
			mountPoint,
			LoggerFactory
				.Create(config => config
					.SetMinimumLevel(LogLevel.Debug)
					.AddConsole()
					.AddDebug())
				.CreateLogger("VaultSharp.Extensions.Configuration"));

		builder.Services.AddSingleton(
			new VaultClientSettings(
				builder.Configuration.GetConnectionString("vault"),
				new TokenAuthMethodInfo(builder.Configuration["VAULT_ROOT_TOKEN"]))
			{
				SecretsEngineMountPoints = { KeyValueV2 = mountPoint }
			});

		builder.Services.AddTransient<IVaultClient, VaultClient>();

		builder.Services.AddHostedService<VaultChangeWatcher>();

		builder.Services
			.AddOptions<DefaultConfiguration>()
			.Bind(builder.Configuration.GetSection(nameof(DefaultConfiguration)));

		builder.Services.AddHostedService<OptionsMonitorLogger>();

		var app = builder.Build();

		app.UseSwagger();
		app.UseSwaggerUI();

		app.UseAuthorization();
		app.MapControllers();
		app.Run();
	}
}