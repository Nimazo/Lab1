using System.CodeDom;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Organizer
{
    public abstract class AbstractRecord
    {
        protected static readonly string[] RECORD_FIELD_SEPARATORS = new string[] {"|hPQ.zBq,sO|"};

        public abstract void FillFromString(string s);
        public abstract string ConvertToString();

        public abstract List<ErrorMessage> Validate();
    }
}