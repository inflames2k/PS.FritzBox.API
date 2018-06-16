namespace PS.FritzBox.API
{
    /// <summary>
    /// app message receiver config
    /// </summary>
    public class AppMessageReceiverConfig
    {
        /// <summary>
        /// gets or sets Identifier of the app instance the message belongs to.
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// Comma separated list of additional crypt algorithms the app understands beside AES128-CBC-HMAC-SHA-256.
        /// If no other crypt algorithms are supported this parametercan be left blank.
        /// Naming according to RFC7518(JWA).
        /// </summary>
        public string CryptAlgos { get; set; }

        /// <summary>
        /// “App-AVM-Address” of the app Instance.
        /// An empty string means that the app instance will no longer receive any messages from this box (message receiver deleteoperation). 
        /// The app gets this value from the AVMmessage relay web service
        /// </summary>
        public string AppAVMAddress { get; set; }

        /// <summary>
        /// BASE64URL encoding (without padding) of first 16 Bytes of the SHA-256 hash of the app's “App-AVM-Password”. 
        /// The app gets this value from the AVMmessage relay web service.
        /// </summary>
        public string AppAVMPasswordHash { get; set; }

        /// <summary>
        /// Shared secret used to build the crypt key for encryption and authentication for messages from the box to the app.
        /// </summary>
        public string EncryptionSecret { get; set; }
    }
}