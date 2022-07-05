using Microsoft.Extensions.Options;

namespace backend;

public class OptionsMonitorLogger : BackgroundService
{
	private readonly ILogger<OptionsMonitorLogger> _logger;
	private readonly IOptionsMonitor<DefaultConfiguration> _defaultConfigurationMonitor;

	public OptionsMonitorLogger(
		ILogger<OptionsMonitorLogger> logger,
		IOptionsMonitor<DefaultConfiguration> defaultConfigurationMonitor)
	{
		_logger = logger;
		_defaultConfigurationMonitor = defaultConfigurationMonitor;
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		_defaultConfigurationMonitor.OnChange(configuration =>
			_logger.LogInformation(
				"IOptionsMonitor new configuration ({Value})",
				configuration.SecretValue));

		return Task.CompletedTask;
	}
}