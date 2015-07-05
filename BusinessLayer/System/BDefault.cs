using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using DomainModel;
using DataLayer.Repositories;

namespace BusinessLayer
{
    public class BCountry
    {
        private CountryRepository countryRepository;

        public BCountry()
        {
            countryRepository = new CountryRepository();
        }

        public void Add(Countries obj)
        {
            countryRepository.Add(obj);
        }

        public void Update(Countries obj)
        {
            countryRepository.Update(obj);
        }

        public void Remove(Countries obj)
        {
            countryRepository.Remove(obj);
        }

        public IList<Countries> GetAll()
        {
            return countryRepository.GetAll();
        }

    }

   


}
