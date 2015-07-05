using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel
{
    [MetadataType(typeof(mWebpages_Roles))]
    public partial class webpages_Roles
    { }

    public class mWebpages_Roles
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}
