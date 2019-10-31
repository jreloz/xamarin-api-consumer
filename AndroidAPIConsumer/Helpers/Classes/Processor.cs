using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidAPIConsumer.Helpers.Models;
using AndroidAPIConsumer.Helpers.Classes;
using System.Net;
using Android.Graphics;

namespace AndroidAPIConsumer.Helpers.Classes
{
    public class Processor
    {
        public int MaxComicNumber { get; set; }

        public static async Task<Comic> LoadData(int DataNumber = 0)
        {
            string url = "";

            if (DataNumber > 0)
            {
                url = $"https://xkcd.com/{DataNumber}/info.0.json";
            }
            else
            {
                url = $"https://xkcd.com/info.0.json";
            }

            using (HttpResponseMessage response = await Helper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    Comic comic = await response.Content.ReadAsAsync<Comic>();
                    return comic;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        public static Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}