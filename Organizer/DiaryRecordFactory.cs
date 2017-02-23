namespace Organizer
{
    public class DiaryRecordFactory : AbstractRecordFactory
    {
        public override AbstractRecord Create()
        {
            return new DiaryRecord();
        }        
    }
}