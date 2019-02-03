using System;
using System.Threading.Tasks;
using TaskList.Core.Logging;

namespace TaskList.Infrastructure.Logging
{
	public class Logger : ILogger
	{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

		public async Task LogError(string message)
		{
		}

		public async Task LogException(Exception exception)
		{
		}

		public async Task LogInfo(string message)
		{
		}

		public async Task LogWarning(string message)
		{
		}

#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
	}
}
