using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainModel;

namespace DataLayer.Interfaces
{


    public interface ISchoolRepository : IGenericDataRepository<School>
    {

    }

    public interface ICountryRepository : IGenericDataRepository<Countries>
    {

    }

   
    public interface IUserProfileRepository : IGenericDataRepository<UserProfile>
    {

    }


    public interface Iwebpages_MembershipRepository : IGenericDataRepository<webpages_Membership>
    {

    }





}
