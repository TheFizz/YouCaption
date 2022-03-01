using Java.Interop;
using Laerdal.FFmpeg.Android;
using System;
using System.Diagnostics;

namespace YouCaptionBase.Droid
{
    class ExecuteCallback : Java.Lang.Object, IExecuteCallback, IStatisticsCallback, ILogCallback
    {
        string log = "";
        public void Apply(long executionId, int returnCode)
        {
            if (returnCode == Config.ReturnCodeSuccess)
            {
                MainActivity.ShowFinishNtf("Processing finished", "Output saved to Downloads folder");
            }
            else if (returnCode == Config.ReturnCodeCancel)
            {
                MainActivity.ShowFinishNtf("Processing canceled", "Operation forcibly canceled");
            }
            else
            {
                MainActivity.ShowFinishNtf("Processing failed", "Something went wrong and operation ended prematurely");
            }
            DroidVideoProcessor.FFmpegFinished = true;
        }
        public void Apply(Statistics statistics)
        {
            var max = DroidVideoProcessor.TotalFrames;
            var cur = statistics.VideoFrameNumber;
            double progress = (double)cur / (double)max;

            MainActivity.UpdateProgressNtf($"Processed {cur}/{max} frames", max, cur);
            DroidVideoProcessor.SetViewProgress(progress, $"Processed {cur}/{max} frames");
        }
        public void Apply(LogMessage logMessage)
        {
            log += logMessage.Text;
        }
    }
}