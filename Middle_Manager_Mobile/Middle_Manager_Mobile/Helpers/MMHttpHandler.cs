using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using HttpTracer;

namespace Middle_Manager_Mobile.Helpers
{
    public class MMHttpHandler : HttpClientHandler
    {
        protected override async System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            try
            {
                var httpResponse = await base.SendAsync(request, cancellationToken);
                if (httpResponse.IsSuccessStatusCode)
                {
                    return httpResponse;
                }
                else
                {
                    //Going to catch globally with raygun or the likes. not entirely sure best way to handle all this though.
                    return httpResponse;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
