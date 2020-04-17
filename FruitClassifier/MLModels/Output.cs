using Microsoft.ML.Data;

namespace FruitClassifier.MLModels
{
    public class Output
    {
        [ColumnName("PredictedLabel")]
        public string Prediction { get; set; }
        public float[] Score { get; set; }
    }
}
