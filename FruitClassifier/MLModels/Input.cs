using Microsoft.ML.Data;

namespace FruitClassifier.MLModels
{
    public class Input
    {
        [ColumnName("Label"), LoadColumn(0)]
        public string Label { get; set; }

        [ColumnName("ImageSource"), LoadColumn(1)]
        public string ImageSource { get; set; }
    }
}
