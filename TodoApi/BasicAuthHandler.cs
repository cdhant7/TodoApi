using System.Text;

namespace TodoApi
{
    public class BasicAuthHandler
    {
        private readonly RequestDelegate next;
        private readonly string relm;
        public BasicAuthHandler(RequestDelegate next, string relm)
        {
            this.next = next;
            this.relm = relm;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            //Basic userid password
            var header = context.Request.Headers["Authorization"].ToString();
            var encodedcreds = header.Substring(6);
            var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedcreds));
            string[] uidpwd = creds.Split(':');
            var uid = uidpwd[0];
            var password = uidpwd[1];

            if (uid != "Sidhant" || password != "password")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            await next(context);

        }

    }
}
