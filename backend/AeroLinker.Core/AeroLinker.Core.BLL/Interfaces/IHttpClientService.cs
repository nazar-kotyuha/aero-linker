﻿namespace AeroLinker.Core.BLL.Interfaces;

public interface IHttpClientService
{
    Task<TResponse> GetAsync<TResponse>(string requestUrl);
    Task<TResponse> SendAsync<TRequest, TResponse>(string requestUrl, TRequest requestData, HttpMethod method);
    Task SendAsync<TRequest>(string requestUrl, TRequest requestData, HttpMethod method);
}
