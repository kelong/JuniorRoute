﻿using System;
using System.Threading.Tasks;

namespace Junior.Route.Routing.AntiCsrf.NonceRepositories
{
	public interface IAntiCsrfNonceRepository
	{
		Task AddAsync(Guid sessionId, Guid nonce, DateTime createdUtcTimestamp, DateTime expiresUtcTimestamp);
		Task<bool> ExistsAsync(Guid sessionId, Guid nonce, DateTime currentUtcTimestamp);
	}
}