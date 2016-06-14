using System.Collections;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using NUnit.Framework;
using Toscana.Engine;

namespace Toscana.Tests
{
    public class GithubRepositoryTests
    {
        private const string GithubRepositoryZip = "https://github.com/borismod/tosca/archive/master.zip";

        private class GithubRepositoryTestCasesFactory
        {
            public static IEnumerable TestCases
            {
                get
                {
                    using (var tempFile = new TempFile(Path.GetTempPath()))
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(GithubRepositoryZip, tempFile.FilePath);

                        var zipToOpen = new FileStream(tempFile.FilePath, FileMode.Open);
                        var archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
                        foreach (var archiveEntry in archive.Entries.Where(a =>
                            Path.GetExtension(a.Name).EqualsAny(".yaml", ".yml")))
                        {
                            yield return new TestCaseData(archiveEntry);
                        }
                    }
                }
            }
        }

        [Test, TestCaseSource(typeof (GithubRepositoryTestCasesFactory), "TestCases")]
        public void Validate_Tosca_Files_In_Github_Repository_Of_Quali(ZipArchiveEntry zipArchiveEntry)
        {
            var toscaNetAnalyzer = new ToscaNetAnalyzer();

            toscaNetAnalyzer.Analyze(new StreamReader(zipArchiveEntry.Open()));
        }
    }
}