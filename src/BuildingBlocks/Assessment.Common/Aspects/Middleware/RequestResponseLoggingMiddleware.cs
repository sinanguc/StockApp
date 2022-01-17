﻿using System;
using System.IO;
using System.Threading.Tasks;
using Assessment.Common.Infrastructure.Mongo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;

namespace Assessment.Common.Aspects.Middleware
{
    /// <summary>
    /// Client tarafından gönderilen Request,
    /// Uygulama tarafından gönderilen Response,
    /// verilerini Mongo DB'ye loglamak işlemini yapar.
    /// </summary>
    class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly IMongoContext _mongoContext;

        public RequestResponseLoggingMiddleware(RequestDelegate next, IMongoContext mongoContext)
        {
            _next = next;
            _mongoContext = mongoContext;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();

            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);
            string text = $"Http Request Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Request Body: {ReadStreamInChunks(requestStream)}";
            context.Request.Body.Position = 0;

            await _mongoContext.ResponseRequestLogs.InsertOneAsync(
            new ResponseRequestLog()
            {
                Data = text,
                LogType = "Request",
                RecordTime = DateTime.Now
            });
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            string response = $"Http Response Information:{Environment.NewLine}" +
                                   $"Schema:{context.Request.Scheme} " +
                                   $"Host: {context.Request.Host} " +
                                   $"Path: {context.Request.Path} " +
                                   $"QueryString: {context.Request.QueryString} " +
                                   $"Response Body: {text}";

            await responseBody.CopyToAsync(originalBodyStream);

            await _mongoContext.ResponseRequestLogs.InsertOneAsync(
            new ResponseRequestLog()
            {
                Data = response,
                LogType = "Response",
                RecordTime = DateTime.Now
            });

        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;

            stream.Seek(0, SeekOrigin.Begin);

            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;

            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}
