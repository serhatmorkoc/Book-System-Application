using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Extension.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;


namespace Web.UI.Controllers
{
    public class BaseController : Controller
    {
        public const string ApiBaseUrl = "http://localhost:50925/api/";
        public const string UserName = "U1";
        public const string Password = "P1";

        public static ListModelResponse<T> GetResponseListAsync<T>(RestClient client, RestRequest req)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(req, res =>
            {
                tcs.SetResult(res);
            });

            var response = JsonConvert.DeserializeObject<ListModelResponse<T>>(tcs.Task.Result.Content);
            return response;
        }

        public static SingleModelResponse<T> GetResponseSingleAsync<T>(RestClient client, RestRequest req)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            client.ExecuteAsync(req, res =>
            {
                tcs.SetResult(res);
            });

            var response = JsonConvert.DeserializeObject<SingleModelResponse<T>>(tcs.Task.Result.Content);
            return response;
        }

        protected virtual void SuccessNotification(string message)
        {
            AddNotification("Success", message);
        }

        protected virtual void ErrorNotification(string message)
        {
            AddNotification("Error", message);
        }

        protected virtual void WarningNotification(string message)
        {
            AddNotification("Warning", message);
        }

        protected virtual void AddNotification(string type, string message)
        {
            var dataKey = string.Format("notifications.{0}", type);
            if (TempData[dataKey] == null)
                TempData[dataKey] = new List<string>();
            ((List<string>)TempData[dataKey]).Add(message);

        }
    }
}