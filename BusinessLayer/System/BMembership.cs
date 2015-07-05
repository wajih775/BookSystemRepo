using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.ConstrainedExecution;
using System.Text;
using DataLayer.Repositories;
using DomainModel;

namespace BusinessLayer
{
    public class Bwebpages_Membership 
    {
        private webpages_MembershipRepository webpagesMembershipRepository;

        public Bwebpages_Membership()
        {
            webpagesMembershipRepository = new webpages_MembershipRepository();
        }

        public webpages_Membership GetSingle(string USERNAME)
        {
            return webpagesMembershipRepository.GetMember(USERNAME);
        }
        
    }
}
