﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhoamI.Business.Contracts.DTO.Project
{
    public class updateProjectRequest : addProjectRequest
    {
        public int Id { get; set; }
    }
}
