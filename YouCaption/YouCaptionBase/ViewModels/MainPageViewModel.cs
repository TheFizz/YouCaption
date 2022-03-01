using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using YouSub.Models;
using YouCaptionBase.Interfaces;
using YoutubeExplode.Videos.Streams;
using YouCaptionBase.Services;

namespace YouCaptionBase.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        bool isSearching = false;
        bool isReady = true;
        bool hasSubs = false;
        bool hasContent = false;
        bool burnSubs = true;

        double downloadProgress;
        string link;
        string displayDuration;
        string downloadProgressText;

        List<VideoWrapper> videos;
        VideoWrapper selectedVideo;

        List<CaptionsWrapper> captions;
        CaptionsWrapper selectedCaption;

        VideoInfoPack pack;
        UriImageSource thumbnail;

        public ICommand GetVideoInfoCommand { get; }
        public ICommand StartVideoDownloadCommand { get; }

        public double DownloadProgress
        {
            set { SetProperty(ref downloadProgress, value); }
            get { return downloadProgress; }
        }

        public VideoWrapper SelectedVideo
        {
            set { SetProperty(ref selectedVideo, value); }
            get { return selectedVideo; }
        }
        public List<VideoWrapper> Videos
        {
            set { SetProperty(ref videos, value); }
            get { return videos; }
        }
        public CaptionsWrapper SelectedCaption
        {
            set { SetProperty(ref selectedCaption, value); }
            get { return selectedCaption; }
        }
        public List<CaptionsWrapper> Captions
        {
            set { SetProperty(ref captions, value); }
            get { return captions; }
        }
        public bool BurnSubs
        {
            set { SetProperty(ref burnSubs, value); }
            get { return burnSubs; }
        }
        public bool HasContent
        {
            set { SetProperty(ref hasContent, value); }
            get { return hasContent; }
        }
        public bool IsSearching
        {
            set { SetProperty(ref isSearching, value); }
            get { return isSearching; }
        }
        public bool IsReady
        {
            set { SetProperty(ref isReady, value); }
            get { return isReady; }
        }
        public bool HasSubs
        {
            set { SetProperty(ref hasSubs, value); }
            get { return hasSubs; }
        }
        public string Link
        {
            set { SetProperty(ref link, value); }
            get { return link; }
        }
        public string DisplayDuration
        {
            set { SetProperty(ref displayDuration, value); }
            get { return displayDuration; }
        }
        public string DownloadProgressText
        {
            set { SetProperty(ref downloadProgressText, value); }
            get { return downloadProgressText; }
        }
        public UriImageSource Thumbnail
        {
            set { SetProperty(ref thumbnail, value); }
            get { return thumbnail; }
        }
        public VideoInfoPack Pack
        {
            set { SetProperty(ref pack, value); }
            get { return pack; }
        }
        public MainPageViewModel()
        {
            Title = "YouCaption";
            GetVideoInfoCommand = new Command(GetVideoInfo);
            StartVideoDownloadCommand = new Command(StartVideoDownload);
            DependencyService.RegisterSingleton(this);
        }
        private async void StartVideoDownload()
        {
            if (await CheckAndRequestStorageWritePermission() != PermissionStatus.Granted)
                return;
            bool consent = true;
            if (burnSubs)
                consent = await App.Current.MainPage.DisplayAlert("Warning!", "Burning subtitles into a video is a CPU intensive task." +
                    " It may take a huge amount of time depending on the source video quality. Would you like to proceed?", "Yes", "No");
            if (!consent)
                return;

            var title = Pack.VideoInfo.Title;
            var video = SelectedVideo.VideoStream;
            var audio = (AudioOnlyStreamInfo)pack.Manifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            var captions = SelectedCaption?.CaptionTrack;
            var duration = Pack.VideoInfo.Duration.Value;

            DependencyService.Get<IVideoProcessor>().ProcessFromStreams(title, video, audio, captions, duration, burnSubs);
        }
        private async void GetVideoInfo()
        {
            IsSearching = true;
            try
            {
                Pack = await YouSub.Search.GetInfoPackAsync(link);
                Thumbnail = new UriImageSource()
                {
                    Uri = new Uri(pack.ThumbnailLink),
                    CachingEnabled = true,
                    CacheValidity = new TimeSpan(5, 0, 0, 0)
                };
                Videos = VideoWrapper.WrapList(pack.Manifest.GetVideoOnlyStreams().Where(v => v.Container.Name != "webm").ToList());
                if (Pack.CaptionsManifest.Tracks.Count > 0)
                {
                    Captions = CaptionsWrapper.WrapList(Pack.CaptionsManifest.Tracks.ToList());
                    HasSubs = true;
                    BurnSubs = true;
                }
                else
                {
                    HasSubs = false;
                    BurnSubs = false;
                }
                DisplayDuration = Pack.VideoInfo.Duration.Value.ToString();
                if (DisplayDuration.StartsWith("00:"))
                    DisplayDuration = DisplayDuration.Remove(0, 3);
                HasContent = true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                IsSearching = false;
            }
        }
        public void SetProgress(double progress, string progressText)
        {
            DownloadProgress = progress;
            DownloadProgressText = progressText;
        }
        public async Task<PermissionStatus> CheckAndRequestStorageWritePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if (status == PermissionStatus.Granted)
                return status;

            if (Permissions.ShouldShowRationale<Permissions.StorageWrite>())
            {
                await App.Current.MainPage.DisplayAlert("Attention!", "Storage access permission is required for the app to work properly", "Ok");
            }

            status = await Permissions.RequestAsync<Permissions.StorageWrite>();

            return status;
        }
    }
}
