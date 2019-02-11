using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Infra.CrossCutting.Identity.Autorization
{

    public class ClaimRequirement : IAuthorizationRequirement
    {
        public ClaimRequirement(string claimName, string claimValue)
        {
            ClaimName = claimName;
            ClaimValue = claimValue;
        }

        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }
    }
}
