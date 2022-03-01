using System;
using System.Collections.Generic;
using System.Text;
using YoutubeExplode.Videos.ClosedCaptions;
using YoutubeExplode.Videos.Streams;

namespace YouCaptionBase.Interfaces
{
    public interface IVideoProcessor
    {
        void ProcessFromStreams(string title, VideoOnlyStreamInfo video, AudioOnlyStreamInfo audio, ClosedCaptionTrackInfo captions, TimeSpan duration, bool burnSubs);
    }
}
