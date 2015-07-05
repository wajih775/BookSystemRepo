using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using DataLayer.Interfaces;
using DomainModel;



namespace DataLayer.Repositories
{
    public class CountryRepository : GenericDataRepository<Countries>, ICountryRepository
    {
        
    }


    public class UserProfileRepository : GenericDataRepository<UserProfile>, IUserProfileRepository
    {

    }

    public class SchoolRepository : GenericDataRepository<School>, ISchoolRepository
    {

    }

    
    public class webpages_MembershipRepository : GenericDataRepository<webpages_Membership>, Iwebpages_MembershipRepository
    {
        public virtual webpages_Membership GetMember(string USERNAME)
        {
            using (var context = new BooksEntities())
            {
                var result = from m in context.webpages_Membership
                             join up in context.UserProfiles on m.UserId equals up.UserId
                             where up.UserName == USERNAME
                             select m;
                return result.FirstOrDefault();
            }

        }
    }



}
