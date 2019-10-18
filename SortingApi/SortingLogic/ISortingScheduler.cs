namespace SortingApi.SortingLogic
{
    /// <summary>
    /// Interface for the sorting scheduler (sorting engine)
    /// Normally, this would be another service, but for the purpose of this exercise, it's a class
    /// </summary>
    public interface ISortingScheduler
    {
        void ScheduleSorting();
    }
}
