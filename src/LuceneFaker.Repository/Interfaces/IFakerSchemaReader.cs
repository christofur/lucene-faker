using Lucene.Net.Documents;
using LuceneFaker.Models;
using System;
using System.Collections.Generic;

namespace LuceneFaker.Repository.Interfaces
{
    public interface IFakerSchemaReader
    {
        LuceneSchema ReadSchemaFromIndex();
        Document GetRandomDocumentFromIndex();
    }
}
