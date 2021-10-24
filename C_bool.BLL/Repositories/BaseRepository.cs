using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace C_bool.BLL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        public abstract List<T> Repository { get; protected set; }
        public abstract string FileName { get; }

        public void Add(T row)
        {
            Repository.Add(row);
        }

        public void Delete(T row)
        {
            Repository.Remove(row);
        }

        public void Update(T oldRow, T newRow)
        {
            var index = Repository.IndexOf(oldRow);
            Repository[index] = newRow;
        }

        public abstract T SearchById(string searchId);

        public abstract List<T> SearchByName(string searchName);
        
        protected virtual string ConvertFileJsonToString()
        {
            var jsonStream = new StreamReader(FileName).BaseStream;
            var streamReader = new StreamReader(jsonStream);
            var convertedToString = streamReader.ReadToEnd();

            return convertedToString;
        }

        protected virtual string ConvertApiJsonToString(WebRequest webRequest)
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
                Repository = JsonConvert.DeserializeObject<List<T>>(ConvertFileJsonToString());
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Nie znaleziono pliku: " + ex.Message);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Błąd dostępu do pliku: " + ex.Message);
            }
        }
    }
}
