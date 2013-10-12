using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Sqrl.NET;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace Sqrl.NET_WP8 {
	public partial class MainPage : PhoneApplicationPage {
		private readonly ObservableCollection<string> _matches;
		private readonly DispatcherTimer _timer;

		private PhotoLuminanceSource _luminance;
		private PhotoCamera _photoCamera;
		private QRCodeReader _reader;

		public MainPage() {
			InitializeComponent();

			_matches = new ObservableCollection<string>();
			foundList.ItemsSource = _matches;

			_timer = new DispatcherTimer();
			_timer.Interval = TimeSpan.FromMilliseconds(250);
			_timer.Tick += (o, arg) => ScanPreviewBuffer();
		}

		protected override void OnNavigatedTo(NavigationEventArgs e) {
			var app = Application.Current as App;
//			app.ScannedValue = string.Empty;

			_photoCamera = new PhotoCamera();
			_photoCamera.Initialized += OnPhotoCameraInitialized;
			_previewVideo.SetSource(_photoCamera);

			CameraButtons.ShutterKeyHalfPressed += (o, arg) => _photoCamera.Focus();

			base.OnNavigatedTo(e);
		}

		private void DisplayResult(string text) {
			if (!_matches.Contains(text)) {
				_matches.Add(text);
			}
		}

		private void OnPhotoCameraInitialized(object sender, CameraOperationCompletedEventArgs e) {
			var width = Convert.ToInt32(_photoCamera.PreviewResolution.Width);
			var height = Convert.ToInt32(_photoCamera.PreviewResolution.Height);

			_luminance = new PhotoLuminanceSource(width, height);
			_reader = new QRCodeReader();

			Dispatcher.BeginInvoke(() => {
				_previewTransform.Rotation = _photoCamera.Orientation;
				_timer.Start();
			});
		}

		private void ScanPreviewBuffer() {
			try {
				_photoCamera.GetPreviewBufferY(_luminance.PreviewBufferY);
				var binarizer = new HybridBinarizer(_luminance);
				var binBitmap = new BinaryBitmap(binarizer);
				var result = _reader.decode(binBitmap);
				if (result != null) {
					Dispatcher.BeginInvoke(() => DisplayResult(result.Text));
				}
			} catch {
			}
		}

		private void foundList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
		}
	}
}