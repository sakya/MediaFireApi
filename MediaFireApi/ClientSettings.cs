namespace MediaFireApi
{
    /// <summary>
    /// The client settings
    /// </summary>
    public class ClientSettings
    {
        /// <summary>
        /// The timeout of the session keep alive
        /// </summary>
        public int SessionKeepAliveTimeoutMs { get; set; } = 30000;
    }
}