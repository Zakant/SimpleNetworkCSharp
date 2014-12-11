using SimpleNetwork.Package.Packages;

namespace SimpleNetwork.Package.History
{
    public interface IPackageHistoryEntry<T> where T : IPackage
    {
        T Package { get; }
        PackageSource Source { get; }
    }
    public enum PackageSource : int
    {
        Remote,
        Local
    }
}
