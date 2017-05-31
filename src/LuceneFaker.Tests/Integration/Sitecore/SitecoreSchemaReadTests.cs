using FluentAssertions;
using LuceneFaker.Repository.Reader;
using LuceneFaker.Tests.Framework.Interfaces;
using Xunit;

namespace LuceneFaker.Tests.Integration.Sitecore
{
    public class SitecoreSchemaReadTests : ISchemaReadTests
    {
        private string _sutIndexPath = @"D:\code\github\lucene-faker\docs\test-data\sitecore_web_index";

        [Fact]
        public void CanReadIndexSchema()
        {
            var reader = new LuceneSchemaReader(_sutIndexPath);
            var fields = reader.ReadSchemaFromIndex();
            fields.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void CanGetRandomDocumentFromIndex()
        {
            var reader = new LuceneSchemaReader(_sutIndexPath);
            var randomDoc = reader.GetRandomDocumentFromIndex();
            randomDoc.Should().NotBeNull();
        }
    }
}
