using System.Diagnostics;
using FluentAssertions;
using Xunit;

public class IntegrationTest
{
  [Fact]
  public void BintLang_Should_Output_ExpectedResult()
  {
    var psi = new ProcessStartInfo("dotnet", "run --project BintLang/BintLang.fsproj")
    {
      RedirectStandardInput = true,
      RedirectStandardOutput = true,
      UseShellExecute = false,
      WorkingDirectory = "/Users/yuchiki/ghq/github.com/yuchiki/BintLang"
    };
    using var process = Process.Start(psi);
    process.StandardInput.WriteLine("テスト入力");
    process.StandardInput.Close();
    string output = process.StandardOutput.ReadToEnd();
    process.WaitForExit();

    output.Should().Contain("Hello");
  }
}
