using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;
using LuceneFaker.Models;
using LuceneFaker.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuceneFaker.Repository.Reader
{
    public class LuceneSchemaWriter : IFakerSchemaWriter
    {
        private string _indexPath;
        private FSDirectory _directoryTemp;
        private IndexWriter indexWriter;
        
        public LuceneSchemaWriter(string indexPath)
        {
            _indexPath = indexPath;
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

        public bool Duplicate(LuceneSchema schema)
        {
            var doc = new Document();

            foreach(var field in schema)
            {
                var store = field.IsStored ? Field.Store.YES : Field.Store.NO;
                var index = field.IsIndexed ? Field.Index.ANALYZED : Field.Index.NOT_ANALYZED;
                var dummyValue = ""; //TODO: Get from bogus
                var f = new Field(field.Name, dummyValue, store, index);
                doc.Add(f);
            }

            indexWriter.AddDocument(doc);

            return true;
        }
    }
}
