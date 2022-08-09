﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Simple.Common.Authentication;

namespace Simple.Common.Services;

public interface ICurrentUserService
{
    ClaimsPrincipal User { get; }
    bool IsAuthenticated { get; }
    string? Username { get; }
    string[] Roles { get; }
    string? Name { get; }
    string? Email { get; }
    Guid? TenantId { get; }

    bool IsInRole(string roleName);
    Claim? FindClaim(string claimType);
    Claim[] FindClaims(string claimType);
    string? FindClaimValue(string claimType);
}

public class CurrentUserService : ICurrentUserService
{
    protected readonly ISimpleService _simpleService;

    public CurrentUserService(ISimpleService simpleService)
    {
        _simpleService = simpleService;
    }

    public virtual ClaimsPrincipal User
    {
        get
        {
            // 如果 Identity 为 null 则抛出异常
            if (_simpleService.HttpContext.User.Identity == null)
            {
                throw new Exception("不能获取到当前用户的状态！");
            }
            return _simpleService.HttpContext.User;
        }
    }

    public virtual bool IsAuthenticated => User.Identity!.IsAuthenticated;

    public virtual string? Username => FindClaimValue(SimpleClaimTypes.UserName); // User.Identity!.Name;

    public virtual string[] Roles => FindClaims(SimpleClaimTypes.Role).Select(c => c.Value).ToArray();

    public virtual string? Name => FindClaimValue(SimpleClaimTypes.Name);

    public virtual Guid? TenantId
    {
        get
        {
            if (Guid.TryParse(FindClaimValue(SimpleClaimTypes.Tenant), out Guid result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
    }

    public virtual string? Email => FindClaimValue(SimpleClaimTypes.Email);

    public virtual bool IsInRole(string roleName)
    {
        return FindClaims(SimpleClaimTypes.Role).Any(c => c.Value == roleName);
    }

    public virtual Claim? FindClaim(string claimType)
    {
        return User.Claims.FirstOrDefault(c => c.Type == claimType);
    }

    public virtual Claim[] FindClaims(string claimType)
    {
        return User.Claims.Where(c => c.Type == claimType).ToArray();
    }

    public virtual string? FindClaimValue(string claimType)
    {
        return FindClaim(claimType)?.Value;
    }
}
