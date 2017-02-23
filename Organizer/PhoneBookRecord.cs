using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Organizer
{
    public class PhoneBookRecord : AbstractRecord
    {
        /// <summary>
        /// Зеленый свет для:
        ///+79261234567
        ///89261234567
        ///79261234567
        ///+7 926 123 45 67
        ///8(926)123-45-67
        ///123-45-67
        ///9261234567
        ///79261234567
        ///(495)1234567
        ///(495) 123 45 67
        ///89261234567
        ///8-926-123-45-67
        ///8 927 1234 234
        ///8 927 12 12 888
        ///8 927 12 555 12
        ///8 927 123 8 123
        /// </summary>
        private const string PHONE_REGEXP = "^((8|\\+7)[\\- ]?)?(\\(?\\d{3}\\)?[\\- ]?)?[\\d\\- ]{7,10}$";


        [DisplayName(@"ФИО")]
        public String Name { get; set; }

        [DisplayName(@"Адрес")]
        public String Address { get; set; }

        [DisplayName(@"Номер телефона")]
        public String Phone { get; set; }

        public PhoneBookRecord()
        {
            Name = String.Empty;
            Address = String.Empty;
            Phone = String.Empty;
        }

        public override void FillFromString(string s)
        {
            var parsedFields = s.Split(RECORD_FIELD_SEPARATORS, StringSplitOptions.None);
            Name = parsedFields[0];
            Address = parsedFields[1];
            Phone = parsedFields[2];
        }

        public override string ConvertToString()
        {
            return Name + RECORD_FIELD_SEPARATORS[0]
                   + Address + RECORD_FIELD_SEPARATORS[0]
                   + Phone;
        }

        public override List<ErrorMessage> Validate()
        {
            var messages = new List<ErrorMessage>();
            if (String.IsNullOrEmpty(Name))
            {
                messages.Add(new ErrorMessage("ФИО не может быть пустым"));
            } else if (Name.Length > 100)
            {
                messages.Add(new ErrorMessage("ФИО должно быть меньше 100 символов"));
            }
            if (Address.Length > 100)
            {
                messages.Add(new ErrorMessage("Адрес должен быть меньше 100 символов"));
            }
            if (!Regex.IsMatch(Phone, PHONE_REGEXP))
            {
                messages.Add(new ErrorMessage("Номера телефона задан в неверном формате"));
            }
            return messages;
        }
    }
}