using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace API
{
    public class FileHttpResponse : HttpResponse
    {
        private readonly string _path;
        public FileHttpResponse(HttpContext httpContext, string path)
        {
            this.HttpContext = httpContext;
            this._path = path;
        }

        public override HttpContext HttpContext { get; }

        public override int StatusCode { get; set; }

        public override IHeaderDictionary Headers { get; } 

        public override Stream Body { get; set; } = new MemoryStream();
        public override long? ContentLength { get; set; }
        public override string ContentType { get; set; }

        public override IResponseCookies Cookies { get; }

        public override bool HasStarted { get; }

        public override void OnCompleted(Func<object, Task> callback, object state)
        {
            using(var reader = new StreamReader(this.Body))
            {
                this.Body.Position = 0;
                var text = reader.ReadToEnd();
                File.WriteAllText(_path, $"{this.StatusCode} - {text}");
                this.Body.Flush();
                this.Body.Dispose();
            }
        }

        public override void OnStarting(Func<object, Task> callback, object state)
        {
            throw new NotImplementedException();
        }

        public override void Redirect(string location, bool permanent)
        {
            throw new NotImplementedException();
        }
    }
}