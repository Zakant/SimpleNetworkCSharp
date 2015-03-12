
namespace SimpleNetwork.Client.Secure
{
    /// <summary>
    /// Gibt an, wie der aktuelle Verbindungsstand ist.
    /// </summary>
    public enum ConnectionState : int
    {
        /// <summary>
        /// Keine Informationen ueber den Verbindungsstand vorhanden.
        /// </summary>
        Undefined,
        /// <summary>
        /// Verbindung ungesichert.
        /// </summary>
        Unsecure,
        /// <summary>
        /// Verbindung wird gesichert.
        /// </summary>
        Securing,
        /// <summary>
        /// Verbindung ist sicher.
        /// </summary>
        Secure
    }
}
