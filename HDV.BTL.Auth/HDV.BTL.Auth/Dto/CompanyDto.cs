using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HDV.BTL.Auth.Dto
{
    public class CompanyDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "tax_number")]
        public string TaxNumber { get; set; }
    }
}
