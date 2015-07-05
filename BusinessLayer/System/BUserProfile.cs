using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using DomainModel;
using DataLayer.Repositories;

namespace BusinessLayer
{
    public class BUserProfile
    {
        private UserProfileRepository userProfileRepository;

        public BUserProfile()
        {
            userProfileRepository = new UserProfileRepository();
        }

        public void Add(UserProfile obj)
        {
            userProfileRepository.Add(obj);
        }

        public void Update(UserProfile obj)
        {
            userProfileRepository.Update(obj);
        }

        public void Remove(UserProfile obj)
        {
            userProfileRepository.Remove(obj);
        }

        public IList<UserProfile> GetAll()
        {
            return userProfileRepository.GetAll();
        }

        public UserProfile GetSingle(string USERNAME)
        {
            return userProfileRepository.GetSingle(obj => obj.UserName == USERNAME);
        }
        
    }
}
