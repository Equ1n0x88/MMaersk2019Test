using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using SortingApi.Model;

namespace SortingApi.Repository
{
    // Normally, this would be a repository class that does reads and writes from the DB
    public class ExecutionRepository : IRepository<Execution>
    {
        private const string FileNamePrefix = "SortingExecution_";
        private const string FileExtension = ".json";

        private readonly string _repositoryFolderPath;

        public ExecutionRepository(string repositoryFolderPath)
        {
            try
            {
                if (!Directory.Exists(repositoryFolderPath))
                {
                    Directory.CreateDirectory(repositoryFolderPath);
                }

                _repositoryFolderPath = repositoryFolderPath;
            }
            catch (Exception e)
            {
                // Here we would normally log/handle it
                throw;
            }
        }

        public int GetNewExecutionId()
        {
            List<Execution> allExecutions = GetAll();
            if (allExecutions == null || allExecutions.Count == 0)
            {
                return 1;
            }

            return allExecutions.Select(x => x.Id).Max() + 1;
        }

        public bool Store(Execution execution)
        {
            try
            {
                File.WriteAllText(Path.Combine(_repositoryFolderPath, FileNamePrefix + execution.Id + FileExtension),
                    JsonConvert.SerializeObject(execution));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Execution Get(int id)
        {
            string executionFullFilePath = GetExecutionFullFilePath(id);
            if (!File.Exists(executionFullFilePath))
            {
                return null;
            }
            return GetExecutionFromFile(executionFullFilePath);
        }

        public List<Execution> GetAll()
        {
            var executions = new List<Execution>();
            string[] allFiles = Directory.GetFiles(_repositoryFolderPath, "*" + FileNamePrefix + "*" + FileExtension, SearchOption.TopDirectoryOnly);

            foreach (string file in allFiles)
            {
                Execution execution = GetExecutionFromFile(file);
                if (execution != null)
                {
                    executions.Add(execution);
                }
            }

            return executions;
        }

        private string GetExecutionFullFilePath(int executionId)
        {
            return Path.Combine(_repositoryFolderPath, FileNamePrefix + executionId + FileExtension);
        }
        
        private Execution GetExecutionFromFile(string filePath)
        {
            try
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    string json = r.ReadToEnd();
                    return JsonConvert.DeserializeObject<Execution>(json);
                }
            }
            catch (Exception e)
            {
                // Here is where we would log/handle the exception
                return null;
            }
        }
    }
}
