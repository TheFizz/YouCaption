using System.Collections.Generic;
using YoutubeExplode.Videos.ClosedCaptions;


namespace YouCaptionBase.Services
{
    public class CaptionsWrapper
    {
        public ClosedCaptionTrackInfo CaptionTrack { get; set; }
        public string Title { get; set; }
        public static List<CaptionsWrapper> WrapList(List<ClosedCaptionTrackInfo> tracks)
        {
            List<CaptionsWrapper> tmp = new List<CaptionsWrapper>();
            foreach (var track in tracks)
            {
                tmp.Add(new CaptionsWrapper(track));
            }
            return tmp;
        }
        public CaptionsWrapper(ClosedCaptionTrackInfo track)
        {
            CaptionTrack = track;
            Title = $"{CaptionTrack.Language.Name}";
        }
    }
}
