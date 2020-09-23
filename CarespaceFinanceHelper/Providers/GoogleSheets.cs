﻿using System;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace CarespaceFinanceHelper.Providers
{
    public sealed class GoogleSheets : IDisposable
    {
        public GoogleSheets(string credentialJson, string sheetId)
        {
            GoogleCredential credential = GoogleCredential.FromJson(credentialJson).CreateScoped(Scopes);

            var initializer = new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            };

            _service = new SheetsService(initializer);
            _sheetId = sheetId;
        }

        public void Dispose() { _service.Dispose(); }

        internal IEnumerable<IList<object>> GetValues(string range, bool parseValues = false)
        {
            SpreadsheetsResource.ValuesResource.GetRequest request = _service.Spreadsheets.Values.Get(_sheetId, range);
            request.ValueRenderOption = parseValues
                ? SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.UNFORMATTEDVALUE
                : SpreadsheetsResource.ValuesResource.GetRequest.ValueRenderOptionEnum.FORMATTEDVALUE;
            request.DateTimeRenderOption =
                SpreadsheetsResource.ValuesResource.GetRequest.DateTimeRenderOptionEnum.SERIALNUMBER;
            ValueRange response = request.Execute();
            return response.Values;
        }

        internal void AppentValues(string range, IList<IList<object>> values)
        {
            var valueRange = new ValueRange { Values = values };
            SpreadsheetsResource.ValuesResource.AppendRequest request =
                _service.Spreadsheets.Values.Append(valueRange, _sheetId, range);
            request.ValueInputOption =
                SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            request.Execute();
        }

        internal void UpdateValues(string range, IList<IList<object>> values)
        {
            var valueRange = new ValueRange { Values = values };
            SpreadsheetsResource.ValuesResource.UpdateRequest request =
                _service.Spreadsheets.Values.Update(valueRange, _sheetId, range);
            request.ValueInputOption =
                SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            request.Execute();
        }

        private static readonly string[] Scopes = { SheetsService.Scope.Drive };
        private const string ApplicationName = "CarespaceFinanceHelper";

        private readonly SheetsService _service;
        private readonly string _sheetId;
    }
}