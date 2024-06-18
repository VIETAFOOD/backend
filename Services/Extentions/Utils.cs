using BusinessObjects.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extentions
{
    public static class Utils
    {
		public static string GenerteDefaultToken(Admin admin)
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            var issuer = config["Jwt:Issuer"];
            var audience = config["Jwt:Audience"];
            var key = config["Jwt:Key"];

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, admin.AdminKey.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, admin.Email.ToString())
            };

            var expired = DateTime.UtcNow.AddMinutes(30);

            var token = new JwtSecurityToken(issuer, audience, claims, notBefore: DateTime.UtcNow, expired, credentials);
            return jwtSecurityTokenHandler.WriteToken(token);
        }

		private static readonly string UTC_PLUS7_IN_VIETNAM = "SE Asia Standard Time";
		private static readonly TimeZoneInfo VietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById(UTC_PLUS7_IN_VIETNAM);

		public static DateTime GetDateTimeNow()
		{
			return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, VietnamTimeZone);
		}

		public static string GetDescriptionEnum<TEnum>(this TEnum value) where TEnum : Enum
		{
			var enumType = typeof(TEnum);
			var name = Enum.GetName(enumType, value);
			if (name == null) return null;

			var fieldInfo = enumType.GetField(name);
			if (fieldInfo == null) return null;

			var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));
			return attribute?.Description;
		}
	}
}
