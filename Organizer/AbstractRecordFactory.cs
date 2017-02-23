namespace Organizer
{
    public abstract class AbstractRecordFactory
    {
        public abstract AbstractRecord Create();

        public AbstractRecord Create(string s)
        {
            var record = Create();
            record.FillFromString(s);
            return record;
        }
    }
}