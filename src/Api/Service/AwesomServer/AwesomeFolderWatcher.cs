using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace API
{
    public class AwesomeFolderWatcher<TContext>
    {
        private FileSystemWatcher _watcher;
        private IHttpApplication<TContext> application;
        private IFeatureCollection features;

        public AwesomeFolderWatcher(IHttpApplication<TContext> application, IFeatureCollection features)
        {
            var path = features.Get<IServerAddressesFeature>().Addresses.FirstOrDefault();
            this._watcher = new FileSystemWatcher(path);
            this._watcher.EnableRaisingEvents = true;
            this.application = application;
            this.features = features;
        }

        public void Watch()
        {
            _watcher.Created += async (sender, e) => {
                var context = (HostingApplication.Context)(object)application.CreateContext(features);
                context.HttpContext = new AwesomeHttpContext(features, e.FullPath);
                await application.ProcessRequestAsync((TContext)(object)context);
                context.HttpContext.Response.OnCompleted(null, null);
            };

            Task.Run(() => _watcher.WaitForChanged(WatcherChangeTypes.All));
        }
    }
}