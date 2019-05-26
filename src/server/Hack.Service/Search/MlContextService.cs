using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.ML;

namespace Hack.Service.Search
{
    class MlContextService
    {
        private readonly string _path;
        private static Dictionary<RegressionMethod, ModelData> Transformers { get; set; } = new Dictionary<RegressionMethod, ModelData>();
        public MlContextService(string path)
        {
            _path = path;
        }

        public PredictionEngine<ModelInput, ModelOutput> CreatePredictionEngine(RegressionMethod method)
        {
            if (Transformers.ContainsKey(method))
            {
                var modelData = Transformers[method];
                return modelData.Context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(modelData.Model, inputSchema: modelData.Schema);
            }
            else
            {
                var path = $"{_path}\\ML\\Models\\{method.ToString()}.zip";
                if (File.Exists(path))
                {
                    MLContext context = new MLContext();
                    var model = context.Model.Load(path, out var schema);
                    Transformers[method] = new ModelData()
                    {
                        Context = context,
                        Schema = schema,
                        Model = model
                    };
                    var predictEngine = context.Model.CreatePredictionEngine<ModelInput, ModelOutput>(model, inputSchema: schema);
                    return predictEngine;
                }
            }

            throw new InvalidOperationException($"Regression Model ({method} couldn't be found.");
        }

        class ModelData
        {
            public MLContext Context { get; set; }
            public DataViewSchema Schema { get; set; }
            public ITransformer Model { get; set; }
        }
    }
}