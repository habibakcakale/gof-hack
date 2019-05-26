using Microsoft.ML.Data;

namespace Hack.Service.Search
{
    internal class ModelOutput
    {
        [ColumnName("Score")]
        public float Estimate { get; set; }
    }
}