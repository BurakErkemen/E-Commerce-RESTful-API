using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Repository.UserInfo.Token
{
    public interface IRefreshTokenRepository : IGenericRepository<RefreshTokenModel>
    {
        Task<RefreshTokenModel?> GetByTokenAsync(string token);
    }
}
