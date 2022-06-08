﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleApp.Common.Authentication.Jwt;

public static class JwtClaimTypes
{
    public const string Username = "Username";

    public const string Role = "role";


    public const string JwtId = "jti";

    public const string Audience = "aud";

    public const string Issuer = "iss";

    public const string Expiration = "exp";

    public const string NotBefore = "nbf";


    public const string Name = "name";

    public const string Email = "email";
}
