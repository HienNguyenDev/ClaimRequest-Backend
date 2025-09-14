using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimRequest.Application.SitesAndRooms.GetAllSite
{
    public sealed record GetAllSiteResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
