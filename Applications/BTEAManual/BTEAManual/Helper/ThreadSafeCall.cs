namespace BTEAManual
{
    using System;
    using System.ComponentModel;

    public static class ThreadSafeCall
    {
        public static TResult SafeInvoke<T, TResult>(this T isi, Func<T, TResult> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired)
                return (TResult)isi.EndInvoke(isi.BeginInvoke(call, new object[] { isi }));
            else
                return call(isi);
        }

        public static void SafeInvoke<T>(this T isi, Action<T> call) where T : ISynchronizeInvoke
        {
            if (isi.InvokeRequired)
                isi.BeginInvoke(call, new object[] { isi });
            else
                call(isi);
        }
    }
}
