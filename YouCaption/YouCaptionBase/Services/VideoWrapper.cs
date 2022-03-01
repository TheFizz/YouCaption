using System.Collections.Generic;
using YoutubeExplode.Videos.Streams;


namespace YouCaptionBase.Services
{
    public class VideoWrapper
    {
        public VideoOnlyStreamInfo VideoStream { get; set; }
        public string Title { get; set; }
        public static List<VideoWrapper> WrapList(List<VideoOnlyStreamInfo> videos)
        {
            List<VideoWrapper> tmp = new List<VideoWrapper>();
            foreach (var video in videos)
            {
                tmp.Add(new VideoWrapper(video));
            }
            return tmp;
        }
        public VideoWrapper(VideoOnlyStreamInfo video)
        {
            VideoStream = video;
            Title = $"{VideoStream.VideoResolution} {VideoStream.Container} @{VideoStream.Bitrate} ({VideoStream.Size})";
        }
    }
}
