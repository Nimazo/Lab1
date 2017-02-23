namespace Organizer
{
    public class PhoneBookRecordFactory : AbstractRecordFactory
    {
        public override AbstractRecord Create()
        {
            return new PhoneBookRecord();
        }

    }
}