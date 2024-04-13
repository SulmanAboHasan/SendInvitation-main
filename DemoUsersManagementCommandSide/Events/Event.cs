using DemoUsersManagementCommandSide.Events;
using System.Text;

namespace DemoUsersManagementCommandSide.Events
{
    public abstract record Event(
        int Id,
        string aggregateId,
        int sequence,
        DateTime DateTime,
        string userId,
        int version

        )
    {
        public string Type => GetType().Name;
    }

    public abstract record Event<T>(      
            string aggregateId,
            int sequence,            
            DateTime dateTime,
            T data,
            string userId,
            int version
        ) :  Event(Id: default, aggregateId, sequence, dateTime, userId, version);
       
    


}
