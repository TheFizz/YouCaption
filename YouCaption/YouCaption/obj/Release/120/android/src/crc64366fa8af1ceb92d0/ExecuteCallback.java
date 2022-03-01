package crc64366fa8af1ceb92d0;


public class ExecuteCallback
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.arthenica.mobileffmpeg.ExecuteCallback,
		com.arthenica.mobileffmpeg.StatisticsCallback,
		com.arthenica.mobileffmpeg.LogCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_apply:(JI)V:GetApply_JIHandler:Laerdal.FFmpeg.Android.IExecuteCallbackInvoker, Laerdal.FFmpeg\n" +
			"n_apply:(Lcom/arthenica/mobileffmpeg/Statistics;)V:GetApply_Lcom_arthenica_mobileffmpeg_Statistics_Handler:Laerdal.FFmpeg.Android.IStatisticsCallbackInvoker, Laerdal.FFmpeg\n" +
			"n_apply:(Lcom/arthenica/mobileffmpeg/LogMessage;)V:GetApply_Lcom_arthenica_mobileffmpeg_LogMessage_Handler:Laerdal.FFmpeg.Android.ILogCallbackInvoker, Laerdal.FFmpeg\n" +
			"";
		mono.android.Runtime.register ("YouCaptionBase.Droid.ExecuteCallback, YouCaption", ExecuteCallback.class, __md_methods);
	}


	public ExecuteCallback ()
	{
		super ();
		if (getClass () == ExecuteCallback.class)
			mono.android.TypeManager.Activate ("YouCaptionBase.Droid.ExecuteCallback, YouCaption", "", this, new java.lang.Object[] {  });
	}


	public void apply (long p0, int p1)
	{
		n_apply (p0, p1);
	}

	private native void n_apply (long p0, int p1);


	public void apply (com.arthenica.mobileffmpeg.Statistics p0)
	{
		n_apply (p0);
	}

	private native void n_apply (com.arthenica.mobileffmpeg.Statistics p0);


	public void apply (com.arthenica.mobileffmpeg.LogMessage p0)
	{
		n_apply (p0);
	}

	private native void n_apply (com.arthenica.mobileffmpeg.LogMessage p0);

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
