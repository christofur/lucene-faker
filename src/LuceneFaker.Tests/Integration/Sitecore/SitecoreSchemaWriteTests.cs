using LuceneFaker.Repository.Reader;
using Xunit;

namespace LuceneFaker.Tests.Integration.Sitecore
{
    public class SitecoreSchemaWriteTests
    {
        private string _sutIndexPath = @"D:\code\github\lucene-faker\docs\test-data\sitecore_web_index";

        [Fact]
        public void CanDuplicateItemOneTime()
        {
            var reader = new LuceneSchemaReader(_sutIndexPath);
            var randomDoc = reader.GetRandomDocumentFromIndex();

            var writer = new LuceneSchemawriter(_sutIndexPath);
        }
    }
}
