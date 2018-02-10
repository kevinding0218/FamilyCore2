using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApi.Resource.Member.Jwt;

namespace WebApi.Persistent.Member.JWTAuth
{
    public class JwtRepository : IJwtRepository
    {
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly JwtHeader _jwtHeader;

        public JwtRepository(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
            _jwtHeader = new JwtHeader(_jwtOptions.SigningCredentials);
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            // Creates a JwtSecurityToken with a combination of registered claims (from the jwt spec) Sub, Jti, Iat and two specific to our app: Rol and Id.
            var claims = new[]
            {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64),
                 //new Claim(JwtRegisteredClaimNames.Iat, _jwtOptions.IssuedAt.ToLocalTime().ToLongTimeString()),
                 new Claim(JwtRegisteredClaimNames.Exp, ToUnixEpochDate(DateTime.Now.AddMinutes(3)).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol),
                 identity.FindFirst(Helpers.Constants.Strings.JwtClaimIdentifiers.Id),
             };

            //var payload = new JwtPayload
            //{
            //    {"iss", _jwtOptions.Issuer},
            //    {"sub", _jwtOptions.Subject},
            //    {"aud", _jwtOptions.Audience},
            //    {"exp", _jwtOptions.Expiration},
            //    {"nbf", _jwtOptions.NotBefore},
            //    {"iat", _jwtOptions.IssuedAt},
            //    {"valid_for", _jwtOptions.ValidFor},
            //    {"unique_name", userName}
            //};
            //var jwt = new JwtSecurityToken(_jwtHeader, payload);
            //var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                //expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            jwt.Payload["issueAt"] = _jwtOptions.IssuedAt.ToString();
            jwt.Payload["expiredOn"] = _jwtOptions.Expiration.ToString();
            jwt.Payload["customIssueAt"] = DateTime.Now.ToString();
            jwt.Payload["customExpiredOn"] = DateTime.Now.AddMinutes(3).ToString();

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() -
                               new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                              .TotalSeconds);

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }
    }
}
