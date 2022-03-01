package crc640ed9750f967bb113;


public abstract class FFmpegStatisticsDelegate
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.arthenica.mobileffmpeg.StatisticsCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_apply:(Lcom/arthenica/mobileffmpeg/Statistics;)V:GetApply_Lcom_arthenica_mobileffmpeg_Statistics_Handler:Laerdal.FFmpeg.Android.IStatisticsCallbackInvoker, Laerdal.FFmpeg\n" +
			"";
		mono.android.Runtime.register ("Laerdal.FFmpeg.FFmpegStatisticsDelegate, Laerdal.FFmpeg", FFmpegStatisticsDelegate.class, __md_methods);
	}


	public FFmpegStatisticsDelegate ()
	{
		super ();
		if (getClass () == FFmpegStatisticsDelegate.class)
			mono.android.TypeManager.Activate ("Laerdal.FFmpeg.FFmpegStatisticsDelegate, Laerdal.FFmpeg", "", this, new java.lang.Object[] {  });
	}


	public void apply (com.arthenica.mobileffmpeg.Statistics p0)
	{
		n_apply (p0);
	}

	private native void n_apply (com.arthenica.mobileffmpeg.Statistics p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
