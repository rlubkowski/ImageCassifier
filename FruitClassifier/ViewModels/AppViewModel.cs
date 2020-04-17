using FruitClassifier.Core;
using FruitClassifier.Services;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FruitClassifier.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        public AppViewModel()
        {
            PredictionDetailsViewModel = new PredictionDetailsViewModel();
            SelectImageCommand = new Command(CanSelectImage, SelectImage);
            EvaluateImageCommand = new AsyncCommand(CanEvaluateImage, EvaluateImage);
            SelectFolderCommand = new Command(CanSelectFolder, SelectFolder);
            RetrainModelCommand = new AsyncCommand(CanRetrainModel, RetrainModelAsync);
        }

        private bool CanRetrainModel()
        {
            if (IsBusy)
                return false;
            if (!string.IsNullOrWhiteSpace(PredictionDetailsViewModel.Folder))
                return true;
            return false;
        }

        private async Task RetrainModelAsync()
        {
            IsBusy = true;
            await Task.Run(() =>
            {
                ModelService.CreateModel(PredictionDetailsViewModel.Folder);
            });
            await ((MetroWindow)System.Windows.Application.Current.MainWindow).ShowMessageAsync("Training Complete", "Training of your model has been completed!");
            IsBusy = false;
        }

        private void SelectFolder()
        {
            IsBusy = true;
            var openFolderDialog = new Ookii.Dialogs.Wpf.VistaFolderBrowserDialog();
            openFolderDialog.RootFolder = Environment.SpecialFolder.Desktop;
            if (openFolderDialog.ShowDialog() == true)
            {
                PredictionDetailsViewModel.Folder = openFolderDialog.SelectedPath;
            }
            IsBusy = false;
        }

        private bool CanSelectFolder()
        {
            if (IsBusy)
                return false;
            return true;
        }

        private bool CanEvaluateImage()
        {
            if (IsBusy)
                return false;
            if (!string.IsNullOrWhiteSpace(PredictionDetailsViewModel.Path))
                return true;
            return false;
        }

        private async Task EvaluateImage()
        {
            IsBusy = true;
            await Task.Run(() =>
            {
                PredictionDetailsViewModel.PredictionResult = PredictionService.Predict(PredictionDetailsViewModel.Path);
            });
            IsBusy = false;
        }

        private bool CanSelectImage()
        {
            if (IsBusy)
                return false;
            return true;
        }

        private void SelectImage()
        {
            IsBusy = true;
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";            
            if (openFileDialog.ShowDialog() == true)
            {
                PredictionDetailsViewModel.Path = openFileDialog.FileName;
                PredictionDetailsViewModel.Name = Path.GetFileName(openFileDialog.FileName);
            }
            IsBusy = false;
        }

        private bool isBusy = false;
        public bool IsBusy
        {
            get => isBusy;
            set 
            { 
                Set(ref isBusy, value, nameof(IsBusy));
                SelectImageCommand.RaiseCanExecuteChanged();
                EvaluateImageCommand.RaiseCanExecuteChanged();
                RetrainModelCommand.RaiseCanExecuteChanged();
                SelectFolderCommand.RaiseCanExecuteChanged();
            }
        }

        public Command SelectFolderCommand { get; private set; }

        public Command SelectImageCommand { get; private set; }

        public AsyncCommand RetrainModelCommand { get; private set; }

        public AsyncCommand EvaluateImageCommand { get; private set; }

        public PredictionDetailsViewModel PredictionDetailsViewModel { get; private set; }
    }
}
