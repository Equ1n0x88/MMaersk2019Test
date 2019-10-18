using System.Collections.Generic;

namespace SortingApi.Repository
{
    /// <summary>
    /// Interface for the repository
    /// Normally, we would use a DB, like SQL or Mongo.
    /// However, since as per the specifications, I cannot assume the machine this will be running on, will have SQl or Mongo Installed,
    /// I am using the file system as a repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        int GetNewExecutionId();

        bool Store(T execution);

        T Get(int id);

        List<T> GetAll();
    }
}
