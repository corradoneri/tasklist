using System;
using System.Threading.Tasks;

namespace TaskList.Core.Logging
{
	public interface ILogger
	{
		Task LogError(string message);
		Task LogException(Exception exception);
		Task LogInfo(string message);
		Task LogWarning(string message);
	}
}
