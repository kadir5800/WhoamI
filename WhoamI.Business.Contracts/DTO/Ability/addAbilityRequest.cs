using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Ability
{
    public class addAbilityRequest
    {
        public int Degree { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int AbilityType { get; set; }
    }
}
