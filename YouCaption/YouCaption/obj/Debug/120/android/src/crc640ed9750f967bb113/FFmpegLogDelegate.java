package crc640ed9750f967bb113;


public abstract class FFmpegLogDelegate
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.arthenica.mobileffmpeg.LogCallback
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_apply:(Lcom/arthenica/mobileffmpeg/LogMessage;)V:GetApply_Lcom_arthenica_mobileffmpeg_LogMessage_Handler:Laerdal.FFmpeg.Android.ILogCallbackInvoker, Laerdal.FFmpeg\n" +
			"";
		mono.android.Runtime.register ("Laerdal.FFmpeg.FFmpegLogDelegate, Laerdal.FFmpeg", FFmpegLogDelegate.class, __md_methods);
	}


	public FFmpegLogDelegate ()
	{
		super ();
		if (getClass () == FFmpegLogDelegate.class)
			mono.android.TypeManager.Activate ("Laerdal.FFmpeg.FFmpegLogDelegate, Laerdal.FFmpeg", "", this, new java.lang.Object[] {  });
	}


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
