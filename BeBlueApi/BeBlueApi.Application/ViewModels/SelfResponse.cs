using System;
using System.Collections.Generic;
using System.Text;

namespace BeBlueApi.Application.ViewModels
{
    public class SelfResponse
    {
        public string Href { get; set; }
        public string[] Rel { get; set; }
        public int Size { get; set; }
        public Object Value { get; set; }
        public int Page { get; set; }
    }
}
