using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using AndroidAPIConsumer.Helpers.Classes;
using AndroidAPIConsumer.Helpers.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using ImageViews.Photo;
using Android.Support.Transitions;

namespace AndroidAPIConsumer
{

    [Activity(Label = "JrelozComicAPI", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        //Variables
        private int MaxNum = 0;
        private int CurrentNum = 0;


        //Declare object instances
        private ImageView Img;
        private Button BtnNext,BtnPrev;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Base App
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            //Security protocol
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            StartApp();

            ////Initialized objects
            Img = FindViewById<PhotoView>(Resource.Id.Img);
            BtnNext = FindViewById<Button>(Resource.Id.BtnNext);
            BtnPrev = FindViewById<Button>(Resource.Id.BtnPrev);


            ////Add Event handler
            BtnNext.Click += BtnNext_Click;
            BtnPrev.Click += BtnPrev_Click;
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private async void StartApp()
        {
            await LoadImage();
        }


        private async Task LoadImage(int ImageNum = 0)
        {

            var comic = await Processor.LoadData(ImageNum);

            if (ImageNum == 0)
            {
                MaxNum = comic.Num;
            }

            CurrentNum = comic.Num;
            var urisource = new Uri(comic.Img, UriKind.Absolute);

            var imageBitmap = Processor.GetImageBitmapFromUrl(comic.Img);
            Img.SetImageBitmap(imageBitmap);
        }

        private async void BtnNext_Click(object sender, EventArgs e)
        {

            if (CurrentNum > 1)
            {
                CurrentNum -= 1;
                BtnNext.Enabled = true;
                await LoadImage(CurrentNum);

                //if (CurrentNum == 1)
                //{
                //    BtnPrev.Enabled = false;
                //}
                //else
                //    BtnPrev.Enabled = true;
            }
        }

        private async void BtnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentNum < MaxNum)
            {
                CurrentNum += 1;
                BtnPrev.Enabled = false;
                await LoadImage(CurrentNum);

                //if (CurrentNum == MaxNum)
                //{
                //    BtnNext.Enabled = true;
                //}
                //else
                //    BtnNext.Enabled = false;
            }
        }




    }
}