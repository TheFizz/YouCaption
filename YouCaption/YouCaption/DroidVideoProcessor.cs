using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using YouCaptionBase.Interfaces;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Videos.ClosedCaptions;
using System.Linq;

using FFmpegConfig = Laerdal.FFmpeg.FFmpegConfig;
using Config = Laerdal.FFmpeg.Android.Config;
using FFmpeg = Laerdal.FFmpeg.Android.FFmpeg;
using FFprobe = Laerdal.FFmpeg.Android.FFprobe;
using Xamarin.Forms;
using YouCaptionBase.ViewModels;
using System.Text.RegularExpressions;

[assembly: Xamarin.Forms.Dependency(typeof(YouCaptionBase.Droid.DroidVideoProcessor))]
namespace YouCaptionBase.Droid
{
    public class DroidVideoProcessor : IVideoProcessor
    {
        //public static MainPageViewModel MainView = MainPageViewModel.GetInstance();
        public static bool FFmpegFinished = false;
        public static int TotalFrames;
        public static MainPageViewModel MainView = DependencyService.Get<MainPageViewModel>(DependencyFetchTarget.GlobalInstance);
        private string appDir;
        public async void ProcessFromStreams(string title, VideoOnlyStreamInfo video, AudioOnlyStreamInfo audio, ClosedCaptionTrackInfo captions, TimeSpan duration, bool burnSubs)
        {
            MainView.IsReady = false;
            appDir = Android.App.Application.Context.GetExternalFilesDir(null).AbsolutePath;
            var fontsDir = Path.Combine(appDir, "Fonts");
            await CopyFontFiles(fontsDir);

            FFmpegConfig.IgnoreSignal(24);
            FFmpegConfig.SetFontDirectory(fontsDir, null);

            ExecuteCallback callback = new ExecuteCallback();
            Config.EnableLogCallback(callback);
            Config.EnableStatisticsCallback(callback);
            title = SanitizeTitle(title, "_");
            string downloadFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;

            List<string> cmd = new List<string>();
            cmd.Add("-i " + video.Url);
            cmd.Add("-i " + audio.Url);
            cmd.Add($"-c:a aac");

            if (burnSubs)
            {
                string captionsFile = Path.Combine(appDir, "cc.srt");
                await new YoutubeClient().Videos.ClosedCaptions.DownloadAsync(captions, captionsFile);
                cmd.Add($"-vf \"subtitles=\'{captionsFile}\':force_style=\'OutlineColour=0x004D4D4D,BorderStyle=3\'\"");
            }
            else
                cmd.Add($"-c:v copy");

            cmd.Add($"\"{Path.Combine(downloadFolder, $"{title}.{video.Container.Name}")}\"");
            //cmd.Add($"-threads {Environment.ProcessorCount}");
            cmd.Add($"-y");

            string cmdParams = string.Join(" ", cmd);

            var info = FFprobe.GetMediaInformation(video.Url);
            var stream = info.Streams.FirstOrDefault();
            float framerate = int.Parse(stream.AverageFrameRate.Split('/')[0]) / float.Parse(stream.AverageFrameRate.Split('/')[1]);
            TotalFrames = (int)Math.Round(float.Parse(info.Duration) * framerate);

            MainActivity.CreateProgressNtf("Download in progress", $"0/" + TotalFrames);
            FFmpeg.ExecuteAsync(cmdParams, callback);

            await Task.Run(() =>
            {
                while (!FFmpegFinished)
                {
                    Task.Delay(100);
                }
            });

            SetViewProgress(0, $"");
            MainView.IsReady = true;
            // !!! string cmdParams = $"-i {video.Url} -i {audio.Url} -c:a aac -vf \"subtitles=\'{Path.Combine(downloadFolder, "cc.srt")}\':force_style=\'Fontname=Roboto-Regular,OutlineColour=0x004D4D4D,BorderStyle=3\'\" {Path.Combine(downloadFolder, "output.mp4")} -y";
        }
        private async Task CopyFontFiles(string fontsDir)
        {
            if (!Directory.Exists(fontsDir))
                Directory.CreateDirectory(fontsDir);
            var fontList = Android.App.Application.Context.Assets.List("Fonts");
            foreach (var font in fontList)
            {
                var outFile = Path.Combine(fontsDir, font);
                if (File.Exists(outFile))
                    continue;
                using (var assetStream = Android.App.Application.Context.Assets.Open("Fonts/" + font))
                using (var fileStream = new System.IO.FileStream(outFile, System.IO.FileMode.CreateNew))
                {
                    await assetStream.CopyToAsync(fileStream);
                }
            }
        }
        private string SanitizeTitle(string title, string replacement)
        {
            Regex removeInvalidChars = new Regex($"[{Regex.Escape(new string(Path.GetInvalidFileNameChars()))}]",
                RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);
            if (String.IsNullOrEmpty(title))
                return title;
            return removeInvalidChars.Replace(title, replacement);
        }
        public static void SetViewProgress(double progress, string progressText)
        {
            MainView.SetProgress(progress, progressText);
        }
    }
}