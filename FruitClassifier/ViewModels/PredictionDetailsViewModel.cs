using FruitClassifier.Models;

namespace FruitClassifier.ViewModels
{
    public class PredictionDetailsViewModel : ViewModelBase
    {
        private string path;
        public string Path
        {
            get => path;
            set { Set(ref path, value, nameof(Path)); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { Set(ref name, value, nameof(Name)); }
        }

        private string folder;
        public string Folder
        {
            get => folder;
            set { Set(ref folder, value, nameof(Folder)); }
        }

        private PredictionResult predictionResult;
        public PredictionResult PredictionResult
        {
            get => predictionResult;
            set { Set(ref predictionResult, value, nameof(PredictionResult)); }
        }
    }
}
