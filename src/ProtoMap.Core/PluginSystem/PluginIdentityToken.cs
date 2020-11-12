namespace ProtoMap.Core.PluginSystem
{
    ///-------------------------------------------------------------------------------------------------
    /// <summary>
    ///     A plugin identity token which is used to track registrations of plugins in various
    ///     services in case they do not unload themselves as expected.
    /// </summary>
    ///-------------------------------------------------------------------------------------------------
    public readonly struct PluginIdentityToken
    {
        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Gets the token. </summary>
        ///
        /// <value> The token.  </value>
        ///-------------------------------------------------------------------------------------------------
        public ushort Token { get; }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Constructor.    </summary>
        ///
        /// <param name="crc">  The CRC.    </param>
        ///-------------------------------------------------------------------------------------------------
        private PluginIdentityToken(ushort crc)
        {
            Token = crc;
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Creates a new PluginIdentityToken for the specified plugin.    </summary>
        ///
        /// <param name="plugin">   The plugin. </param>
        ///
        /// <returns>   The new for.    </returns>
        ///-------------------------------------------------------------------------------------------------
        public static PluginIdentityToken CreateFor(IPlugin plugin)
        {
            byte[] data = plugin.Identifier.ToByteArray();
 
            ushort crc = 0x0000;
            for (int i = 0; i < data.Length; i++)
            {
                crc ^= (ushort)(data[i] << 8);
                for (int j = 0; j < 8; j++)
                {
                    if ((crc & 0x8000) > 0)
                        crc = (ushort)((crc << 1) ^ 0x8005);
                    else
                        crc <<= 1;
                }
            }

            return new PluginIdentityToken(crc);
        }

        public static implicit operator ushort(PluginIdentityToken token)
        {
            return token.Token;
        }

        public static bool operator ==(PluginIdentityToken a, PluginIdentityToken b)
        {
            return a.Token == b.Token;
        }

        public static bool operator ==(PluginIdentityToken a, ushort value)
        {
            return a.Token == value;
        }

        public static bool operator ==(ushort value, PluginIdentityToken b)
        {
            return value == b.Token;
        }

        public static bool operator !=(PluginIdentityToken a, PluginIdentityToken b)
        {
            return a.Token != b.Token;
        }

        public static bool operator !=(PluginIdentityToken a, ushort value)
        {
            return a.Token != value;
        }

        public static bool operator !=(ushort value, PluginIdentityToken b)
        {
            return value != b.Token;
        }

        public bool Equals(PluginIdentityToken other)
        {
            return Token == other.Token;
        }

        public override bool Equals(object? obj)
        {
            return obj is PluginIdentityToken other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Token.GetHashCode();
        }
    }
}
