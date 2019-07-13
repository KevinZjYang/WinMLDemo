using System;

using Windows.UI.Xaml.Controls;

using WinMLDemo.ViewModels;
using Windows.AI.MachineLearning;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Media;
using Windows.Graphics.Imaging;
using System.Numerics;
using System.Collections.Generic;

namespace WinMLDemo.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return ViewModelLocator.Current.MainViewModel; }
        }

        private resnet100Model modelGen;
        private resnet100Input mnistInput = new resnet100Input();
        private resnet100Output mnistOutput;

        public MainPage()
        {
            InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await LoadModelAsync();
        }

        private async Task LoadModelAsync()
        {
            //Load a machine learning model
            StorageFile modelFile = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/resnet100.onnx"));
            modelGen = await resnet100Model.CreateFromStreamAsync(modelFile as IRandomAccessStreamReference);
        }

        private async void ChoseButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");

            var file = await picker.PickSingleFileAsync();
            if (file == null) return;

            SoftwareBitmap softwareBitmap;

            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }

            if (softwareBitmap.BitmapPixelFormat != BitmapPixelFormat.Bgra8 ||
    softwareBitmap.BitmapAlphaMode == BitmapAlphaMode.Straight)
            {
                softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            }

            var source = new SoftwareBitmapSource();
            await source.SetBitmapAsync(softwareBitmap);

            // Set the source of the Image control
            displayImage.Source = source;

            //VideoFrame vf = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);

            //var imageFV = ImageFeatureValue.CreateFromVideoFrame(vf);

            //TensorFloat tensor = TensorFloat.Create();

            //mnistInput = new resnet100Input
            //{
            //    data = tensor
            //};
            //mnistOutput = await modelGen.EvaluateAsync(mnistInput);

            
        }
    }
}
