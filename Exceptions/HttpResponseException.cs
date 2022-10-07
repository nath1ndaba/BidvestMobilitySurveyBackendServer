﻿using BidvestMobilitySurveyBackendServer.Models;

public class HttpResponseException : Exception
{
    public Response Response { get; }

    public HttpResponseException(Response response) : base()
    {
        Response = response;
    }

    public HttpResponseException(string error) : base()
    {
        Response = new Response(error: error);
    }
}