using System;

namespace Hack.Domain
{
    public sealed class HackConfig : IHackConfig
    {
        private static IHackConfig _instance;

        public static IHackConfig Instance
        {
            get { return _instance; }
            set
            {
                if (_instance is null)
                {
                    _instance = value;
                }
            }
        }

        public AuthenticationConfig Auth { get; }

        public HackConfig(AuthenticationConfig auth)
        {
            Auth = auth ?? throw new ArgumentNullException(nameof(auth));
        }
    }
}