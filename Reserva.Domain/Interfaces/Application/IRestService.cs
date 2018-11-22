using System;
using RestSharp;

namespace Reserva.Domain.Interfaces.Application
{
    public interface IRestService
    {
        Uri Url { get; set; }
        IRestResponse<T> ExecutePost<T>(object jsonBody = null) where T : new();
        IRestResponse<T> ExecuteGet<T>() where T : new();
    }
}