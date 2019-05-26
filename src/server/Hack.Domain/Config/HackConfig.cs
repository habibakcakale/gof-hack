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
        public JiraCredentialsConfig JiraCredentials { get; set; }

        public HackConfig(AuthenticationConfig auth, JiraCredentialsConfig jiraCredentials)
        {
            Auth = auth ?? throw new ArgumentNullException(nameof(auth));
            JiraCredentials = jiraCredentials ?? throw new ArgumentNullException(nameof(jiraCredentials));
        }
    }
}