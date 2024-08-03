namespace TradingEngineServer.Logging
{
    public record LogInformation(LogLevel logLevel, string module, string message, DateTime now, int ThreadId, string ThreadName);
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}
