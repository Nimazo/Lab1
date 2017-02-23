using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Organizer
{
    public class DiaryRecord : AbstractRecord
    {
        private const string DATE_TIME_PATTERN = "dd.MM.yyyy HH:mm";

        [DisplayName(@"Дата и время")]
        public DateTime RecordDateTime { get; set; }

        [DisplayName(@"Содержание записи")]
        public String Content { get; set; }

        public DiaryRecord()
        {
            Content = string.Empty;
        }        

        public override void FillFromString(string s)
        {
            var parsedFields = s.Split(RECORD_FIELD_SEPARATORS, StringSplitOptions.None);
            RecordDateTime = DateTime.Parse(parsedFields[0]);
            Content = parsedFields[1];
        }

        public override string ConvertToString()
        {
            return 
                RecordDateTime.ToString(DATE_TIME_PATTERN) + 
                RECORD_FIELD_SEPARATORS[0] + 
                Content;            
        }

        public override List<ErrorMessage> Validate()
        {
            return new List<ErrorMessage>();
        }
    }
}