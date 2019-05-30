namespace Hack.Service
{
    public sealed class UpdateEstimateRequest
    {
        public string IssueIdOrKey { get; set; }
        /// <summary>
        /// Estimates in minutes
        /// </summary>
        public float Estimate { get; set; }
    }
}