﻿using Newtonsoft.Json;

namespace CarespaceFinanceHelper.Dto.Digiseller
{
    internal sealed class ProductResult
    {
        public sealed class Product
        {
            [JsonProperty]
            public string Name { get; set; }
        }

        [JsonProperty("product")]
        public Product Info { get; set; }
    }
}
