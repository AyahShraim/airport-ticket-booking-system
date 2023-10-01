namespace AirportTicketBookingSystemApp.Interfaces
{
    public interface ISearchCriteria <T>
    {
        public bool ApplyFilter(T filter);
        
    }
}
