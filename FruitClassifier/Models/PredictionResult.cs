namespace FruitClassifier.Models
{
    public class PredictionResult
    {
        public PredictionResult(float probability, string result)
        {
            Probability = probability;
            Result = result;
        }

        public float Probability { get; private set; }

        public string Result { get; private set; }
    }
}
