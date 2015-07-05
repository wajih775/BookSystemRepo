using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using DomainModel;
using DataLayer.Repositories;

namespace BusinessLayer
{
    public class BSchool
    {
        private SchoolRepository SchoolRepository;

        public BSchool()
        {
            SchoolRepository = new SchoolRepository();
        }

        public int Add(School obj)
        {
            return SchoolRepository.Add(obj);
        }

        public void Update(School obj)
        {
            SchoolRepository.Update(obj);
        }

        public void Remove(School obj)
        {
            SchoolRepository.Remove(obj);
        }

        public IList<School> GetAll()
        {
            return SchoolRepository.GetAll();
        }

        public School GetSingle(string USERNAME)
        {
            return SchoolRepository.GetSingle(obj => obj.Name == USERNAME);
        }
        
    }
}
