﻿using Common.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly MemoryDistributedCache _cache;
        private IHttpContextAccessor _httpContextAccessor;
        public TokenRepository()
        {
            _cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));
            _httpContextAccessor = new HttpContextAccessor();
        }

        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());

        public async Task DeactivateCurrentAsync()
            => await DeactivateAsync(GetCurrentAsync());

        public async Task<bool> IsActiveAsync(string token)
            => await _cache.GetStringAsync(GetKey(token)) == null;

        public async Task DeactivateAsync(string token)
        {
            await _cache.SetStringAsync(GetKey(token),
                  " ", new DistributedCacheEntryOptions
                  {
                      AbsoluteExpirationRelativeToNow =
                          TimeSpan.FromMinutes(int.Parse(ConfigHelper.Get("TOKEN_EXPIRATION_TIME")))
                  });
        }
        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"].ToString();

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Split(' ').Last();
        }

        private static string GetKey(string token)
            => $"tokens:{token}:deactivated";
    }
}
