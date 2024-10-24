

namespace Ipstatuschecker.DomainEntity
{
    public class WorkSchedule
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int BreakDuration { get; set; } 
}

}