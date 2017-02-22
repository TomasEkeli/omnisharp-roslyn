public static class PathHelper
{
    public static string Combine(params string[] paths) =>
        System.IO.Path.Combine(paths);

    public static string GetFullPath(string path) =>
        System.IO.Path.Combine(path);
}

string CombinePaths(params string[] paths)
{
    return PathHelper.Combine(paths);
}

public class Folders
{
    public string DotNetSdk { get; }
    public string Tools { get; }

    public string MSBuild { get; }
    public string Source { get; }
    public string Tests { get; }
    public string TestAssets { get; }

    public string Artifacts { get; }
    public string ArtifactsPublish { get; }
    public string ArtifactsLogs { get; }
    public string ArtifactsPackage { get; }
    public string ArtifactsScripts { get; }

    public Folders(string workingDirectory)
    {
        this.DotNetSdk = PathHelper.Combine(workingDirectory, ".dotnet");
        this.Tools = PathHelper.Combine(workingDirectory, "tools");

        this.MSBuild = PathHelper.Combine(workingDirectory, "msbuild");
        this.Source = PathHelper.Combine(workingDirectory, "src");
        this.Tests = PathHelper.Combine(workingDirectory, "tests");
        this.TestAssets = PathHelper.Combine(workingDirectory, "test-assets");

        this.Artifacts = PathHelper.Combine(workingDirectory, "artifacts");
        this.ArtifactsPublish = PathHelper.Combine(this.Artifacts, "publish");
        this.ArtifactsLogs = PathHelper.Combine(this.Artifacts, "logs");
        this.ArtifactsPackage = PathHelper.Combine(this.Artifacts, "package");
        this.ArtifactsScripts = PathHelper.Combine(this.Artifacts, "scripts");
    }
}

public class BuildEnvironment
{
    public string WorkingDirectory { get; }
    public Folders Folders { get; }

    public string DotNetCommand { get; }

    public BuildEnvironment(bool useGlobalDotNetSdk)
    {
        this.WorkingDirectory = PathHelper.GetFullPath(
            System.IO.Directory.GetCurrentDirectory());
        this.Folders = new Folders(this.WorkingDirectory);

        this.DotNetCommand = useGlobalDotNetSdk
            ? "dotnet"
            : PathHelper.Combine(this.Folders.DotNetSdk, "dotnet");
    }
}
