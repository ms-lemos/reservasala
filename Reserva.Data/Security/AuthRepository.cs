using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reserva.Data.Security
{
    public class AuthRepository : IDisposable
    {
        private readonly AuthContext _db;

        public AuthRepository()
        {
            _db = new AuthContext();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public AuthClient FindClient(string clientId)
        {
            var client = _db.AuthClient.Find(clientId);

            return client;
        }

        public async Task<bool> AddRefreshToken(AuthRefreshToken token)
        {
            //Nao pode duplicar
            foreach (var existingToken in _db.AuthRefreshToken.Where(r =>
                r.Subject == token.Subject && r.ClientId == token.ClientId)) _db.AuthRefreshToken.Remove(existingToken);
            _db.SaveChanges();

            _db.AuthRefreshToken.Add(token);

            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _db.AuthRefreshToken.FindAsync(refreshTokenId);

            if (refreshToken != null)
            {
                _db.AuthRefreshToken.Remove(refreshToken);
                return await _db.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<bool> RemoveRefreshToken(AuthRefreshToken refreshToken)
        {
            _db.AuthRefreshToken.Remove(refreshToken);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<AuthRefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _db.AuthRefreshToken.FindAsync(refreshTokenId);

            return refreshToken;
        }

        public List<AuthRefreshToken> GetAllRefreshTokens()
        {
            return _db.AuthRefreshToken.ToList();
        }
    }
}