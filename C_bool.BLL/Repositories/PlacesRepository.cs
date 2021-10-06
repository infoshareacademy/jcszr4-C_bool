﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using C_bool.BLL.Places;
using Newtonsoft.Json;

namespace C_bool.BLL.Repositories
{
    public class PlacesRepository : IRepository<Place>
    {
        [JsonProperty(PropertyName = "results")]
        public List<Place> Places { get; private set; }

        public PlacesRepository()
        {
            Places = new List<Place>();
        }

        public void AddFileDataToRepository()
        {
            try
            {
                var jsonStream = new StreamReader("places.json").BaseStream;
                var streamReader = new StreamReader(jsonStream);
                var repository = JsonConvert.DeserializeObject<PlacesRepository>(streamReader.ReadToEnd());

                if (repository == null)
                {
                    Console.WriteLine("Plik jest pusty!");
                }
                else
                {
                    foreach (var place in repository.Places)
                    {
                        Places.Add(place);
                    }
                }
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

        public IEnumerable<Place> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Place> Find(Expression<Func<Place, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(Place entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Place entity)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Place> IRepository<Place>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}