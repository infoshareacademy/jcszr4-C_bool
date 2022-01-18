using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using C_bool.BLL.DAL.Entities;
using Newtonsoft.Json;

namespace C_bool.BLL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IEntity
    {

        protected List<T> Repository { get; } = new();
        public abstract string FileName { get; }

        public void Add(T entity)
        {
            Repository.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Repository.AddRange(entities);
        }

        public void Delete(T entity)
        {
            Repository.Remove(entity);
        }

        public void Delete(int id)
        {
            var product = GetById(id);

            Repository.Remove(product);
        }

        public void Update(T entity)
        {
            var index = Repository.IndexOf(Repository.SingleOrDefault(r => r.Id == entity.Id));
            Repository[index] = entity;
        }

        public T GetById(int id)
        {
            return Repository.FirstOrDefault(x => x.Id == id);
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

        public IQueryable<T> GetAllQueryable()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}