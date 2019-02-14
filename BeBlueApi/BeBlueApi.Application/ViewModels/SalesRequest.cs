using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Application.ViewModels
{
    public class SalesRequest
    {
        public List<SalesLineRequest> Lines { get; set; }
    }
}
