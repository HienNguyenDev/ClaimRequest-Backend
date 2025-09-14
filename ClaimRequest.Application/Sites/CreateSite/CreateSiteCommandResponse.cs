using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.Sites
{
    public class CreateSiteCommandResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
