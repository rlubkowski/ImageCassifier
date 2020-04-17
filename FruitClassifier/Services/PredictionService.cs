using FruitClassifier.MLModels;
using FruitClassifier.Models;
using Microsoft.ML;
using System;
using System.Linq;

namespace FruitClassifier.Services
{
    public static class PredictionService
    {
        public static PredictionResult Predict(string imagePath)
        {
            var input = new Input
            {
                ImageSource = imagePath
            };
            // Create new MLContext
            var mlContext = new MLContext();

            // Load model & create prediction engine
            string modelPath = Environment.CurrentDirectory + @"\Assets\MLModel\MlModel.zip";
            
            DataViewSchema modelInputSchema;

            ITransformer mlModel = mlContext.Model.Load(modelPath, out modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<Input, Output>(mlModel);

            // Use model to make prediction on input data
            var prediction =  predEngine.Predict(input);

            return new PredictionResult(prediction.Score.Max(), prediction.Prediction);
        }
    }
}
