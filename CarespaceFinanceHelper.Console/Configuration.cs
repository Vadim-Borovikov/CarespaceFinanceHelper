﻿// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable MemberCanBePrivate.Global
using System.Collections.Generic;
using Newtonsoft.Json;

namespace CarespaceFinanceHelper.Console
{
    internal class Configuration
    {
        public string GoogleSheetId { get; set; }
        public string GoogleCustomRange { get; set; }
        public string GoogleFinalRange { get; set; }

        public string DigisellerProductUrlPrefix { get; set; }

        public string TaxUserAgent { get; set; }
        public string TaxSourceDeviceId { get; set; }
        public string TaxSourceType { get; set; }
        public string TaxAppVersion { get; set; }
        public string TaxRefreshToken { get; set; }
        public string TaxIncomeType { get; set; }
        public string TaxPaymentType { get; set; }
        public string TaxProductNameFormat { get; set; }
        public string TaxReceiptUrlFormat { get; set; }

        public Dictionary<string, string> GoogleCredentials { get; set; }
        public string GoogleCredentialsJson => JsonConvert.SerializeObject(GoogleCredentials);
    }
}
