using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using AndroidX.AppCompat.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.FloatingActionButton;
using Google.Android.Material.Snackbar;
using Xamarin.Google.MLKit.Vision.Objects.Defaults;
using Xamarin.Google.MLKit.Vision.Objects;
using Xamarin.Google.MLKit.Vision.Common;
using Android.Content.Res;
using System.IO;
using Android.Graphics;
using Android.Content;
using AndroidX.Annotations;
using Android.Gms.Extensions;
using System.Threading.Tasks;

namespace objectdetection
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClickAsync;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private async void FabOnClickAsync(object sender, EventArgs eventArgs)
        {
            var picture = BitmapFactory.DecodeResource(this.Resources, Resource.Raw.IMG_4385);
            var image = InputImage.FromBitmap(picture, 0);

            try
            {
                ObjectDetectorOptions.Builder builder = (ObjectDetectorOptions.Builder)new ObjectDetectorOptions.Builder().SetDetectorMode(ObjectDetectorOptions.SingleImageMode);
                var options = builder.Build();
                var detector = ObjectDetection.GetClient(options);

                var results = await detector.Process(image);
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
            
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
