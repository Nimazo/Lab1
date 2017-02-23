using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Organizer
{
    public class RecordSerializer
    {
        protected static readonly string[] RECORD_SEPARATORS = new string[] { "|uNla}q.xI8|" };

        private AbstractRecordFactory recordFactory;        

        public RecordSerializer(AbstractRecordFactory factory)
        {
            recordFactory = factory;            
        }        

        public List<AbstractRecord> LoadFromFile(string filePath)
        {
            return File.ReadAllText(filePath)
                .Split(RECORD_SEPARATORS, StringSplitOptions.RemoveEmptyEntries)
                .Select(line => line.Trim())
                .Where(line => !String.IsNullOrEmpty(line))
                .Select(line => recordFactory.Create(line))
                .ToList();
        }

        public void SaveToFile(string filePath, IEnumerable<AbstractRecord> records)
        {
            var text = String.Join(RECORD_SEPARATORS[0], 
                records.Select(record => record.ConvertToString()).ToArray());
            File.WriteAllText(filePath, text);
        }
    }
}