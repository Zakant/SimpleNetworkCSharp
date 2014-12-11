
namespace SimpleNetwork.Events
{
    /// <summary>
    /// Gibt die Gründe für einen Verbindugsabbruch an.
    /// </summary>
    public enum DisconnectReason : byte
    {
        /// <summary>
        /// Verbindung wurde unerwartet geschlossen. Lese- oder Schreibvorgang ist fehlgeschlagen.
        /// </summary>
        Failed,
        /// <summary>
        /// Keine weiteren Information über die Gründe verfügbar.
        /// </summary>
        Unknown,
        /// <summary>
        /// Verbindung geschlossen.
        /// </summary>
        ConnectionShoutDown,
        /// <summary>
        /// Verbindung wurde ortnungsgemäß beendet.
        /// </summary>
        ClosedProperly,
        /// <summary>
        /// Netzwerkverbindung wurde verloren.
        /// </summary>
        LostConnection

    }
}
