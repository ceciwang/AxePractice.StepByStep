using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace SessionModuleClient
{
    public class AuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get; } = false;

        public bool RedirectToLoginOnChallenge { get; set; }

        public async Task AuthenticateAsync(
            HttpAuthenticationContext context,
            CancellationToken cancellationToken)
        {
            #region Please implement the following method

            /*
             * We need to create IPrincipal from the authentication token. If
             * we can retrive user session, then the structure of the IPrincipal
             * should be in the following form:
             * 
             * ClaimsPrincipal
             *   |- ClaimsIdentity (Primary)
             *        |- Claim: { key: "token", value: "$token value$" }
             *        |- Claim: { key: "userFullName", value: "$user full name$" }
             * 
             * If user session cannot be retrived, then the context principal
             * should be an empty ClaimsPrincipal (unauthenticated).
             */

            HttpRequestMessage request = context.Request;
            var token = GetSessionToken(request);
            
            IDepenedencyScope scope = request.GetDependencyScope();
            var client = (HttpClient) request.GetService(typeof(HttpClient));
            var requestUri = request.RequestUri;
            var response = await client.SendAsync($"{requestUri.Scheme}://{requestUri.UserInfo}:{requestUri.Authority}/session/{token}");
            if(response.IsSucessStatusCode)
            {
                var userSession = response.Content.ReadAsAsync<UserSessionDto>(
                context.ControllerContext.Configuration.Formatters, 
                cancellationToken);

                request.Principal = new ClaimsPrincipal(){
                    ClaimsIdentity = new Claim[]{
                        new Claim("token", userSession.Token),
                        new Claim("userFullName", userSession.UserFullName)
                    }
                }
            }else
            {
                request.Principal = new ClaimsPrincipal();
            }
                
            #endregion
        }

        string GetSessionToken(HttpRequestMessage request)
        {
            CookieState sessionCookie = GetSessionCookie(request);
            if(sessionCookie == null) { throw new HttpResponseException(HttpStatusCode.Forbidden)}
            string token = sessionCookie.Value;
            if(string.IsNullOrEmpty(token)) {throw new HttpResponseException(HttpStatusCode.Forbidden)};
            return token;
        }

        CookieState GetSessionCookie(HttpRequestMessage request)
        {
            const string sessionTokenKey = "X-Session-Token";
            var cookieHeaderValues = request.Headers.GetCookies(sessionTokenKey);
            var sessionCookie = cookieHeaderValues
            .Where(v => v.Expires == null || v.Expires > DateTimeOffset.Now)
            .SelectMany(r => r.Cookies)
            .FirstOrDefault(c => c.Name == sessionTokenKey);
            return sessionCookie;
        }


        public Task ChallengeAsync(
            HttpAuthenticationChallengeContext context,
            CancellationToken cancellationToken)
        {
            #region Please implement the following method

            /*
             * The challenge method will try checking the configuration of
             * RedirectToLoginOnChallenge property. If the value is true,
             * then it will replace the response to redirect to login page.
             * And if the value is false, then simply keeps the original
             * response.
             */

            if(RedirectToLoginOnChallenge){
                context.Result = new RedirectToLoginPageIfUnauthorizedResult(context.Request, context.Result);
            }
            return Task.FromResult(0);

            #endregion
        }
    }
}