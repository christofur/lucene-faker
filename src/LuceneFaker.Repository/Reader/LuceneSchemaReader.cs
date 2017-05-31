using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using LuceneFaker.Models;
using LuceneFaker.Repository.Interfaces;
using LuceneFaker.Repository.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuceneFaker.Repository.Reader
{
    public class LuceneSchemaReader : IFakerSchemaReader
    {
        private string _indexPath;
        private FSDirectory _directoryTemp;
        private IndexReader indexReader;
        private IndexSearcher indexSearcher;

        public LuceneSchemaReader(string indexPath)
        {
            _indexPath = indexPath;
            indexReader = IndexReader.Open(_directory, false);
            indexSearcher = new IndexSearcher(indexReader);
        }

        private FSDirectory _directory
        {
            get
            {
                if (_directoryTemp == null) _directoryTemp = FSDirectory.Open(new DirectoryInfo(_indexPath));
                if (IndexWriter.IsLocked(_directoryTemp)) IndexWriter.Unlock(_directoryTemp);
                var lockFilePath = Path.Combine(_indexPath, "write.lock");
                if (File.Exists(lockFilePath)) File.Delete(lockFilePath);
                return _directoryTemp;
            }
        }

        public LuceneSchema ReadSchemaFromIndex()
        {
            var docId = indexSearcher.Search(new MatchAllDocsQuery(), 1).ScoreDocs.First().Doc;
            var doc = indexSearcher.Doc(docId);
            return (LuceneSchema)doc.GetFields();
        }
       
        private Type GetTypeFromExampleValue(string value)
        {
            DateTime dt = DateTime.Now;
            if (DateTime.TryParse(value, out dt))
                return dt.GetType();

            int i = 0;
            if (Int32.TryParse(value, out i))
                return i.GetType();

            return "".GetType();
        } 

        public Document GetRandomDocumentFromIndex()
        {
            var docs = indexSearcher.Search(new MatchAllDocsQuery(), indexSearcher.MaxDoc);
            return indexSearcher.Doc(docs.ScoreDocs.PickRandom().Doc);
         }
    }
}
