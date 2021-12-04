using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using C_bool.BLL.Models;
using Newtonsoft.Json;

namespace C_bool.BLL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {
        protected List<T> Repository { get; } = new();
        public abstract string FileName { get; }

        public void Add(T row)
        {
            Repository.Add(row);
        }

        public void AddRange(List<T> rows)
        {
            Repository.AddRange(rows);
        }

        public void Delete(T row)
        {
            Repository.Remove(row);
        }

        public void Update(T row)
        {
            var index = Repository.IndexOf(Repository.SingleOrDefault(r => r.Id == row.Id));
            Repository[index] = row;
        }

        public T SearchById(string searchId)
        {
            return Repository.FirstOrDefault(x => x.Id == searchId);
        }

        public List<T> GetAll()
        {
            return Repository;
        }

        protected virtual string ConvertFileJsonToString()
        {
            var jsonStream = new StreamReader(FileName).BaseStream;
            var streamReader = new StreamReader(jsonStream);
            var convertedToString = streamReader.ReadToEnd();

            if (convertedToString.Equals(""))
            {
                throw new ArgumentNullException();
            }

            return convertedToString;
        }

        protected string ConvertApiJsonToString(WebRequest webRequest)
        {
            webRequest.ContentType = "application/json";

            var response = webRequest.GetResponse().GetResponseStream();
            var streamReader = new StreamReader(response);
            var convertedToString = streamReader.ReadToEnd();

            return convertedToString;
        }

        public void AddFileDataToRepository()
        {
            try
            {
                AddRange(JsonConvert.DeserializeObject<List<T>>(ConvertFileJsonToString()));
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
            catch (IOException ex)
            {
                throw ex;
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
        }

        public bool IsRepositoryEmpty(List<T> repository)
        {
            return !repository.Any();
        }
    }
}