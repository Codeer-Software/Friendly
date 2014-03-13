namespace Codeer.Friendly.Dynamic.Test
{
    static class TargetPath
    {
#if DEBUG
        const string Mode = "Debug";
#else
        const string Mode = "Release";
#endif
        internal const string TestTargetPath = @"..\..\..\TestTarget\bin\" + Mode + @"\TestTarget.exe";
    }
}
