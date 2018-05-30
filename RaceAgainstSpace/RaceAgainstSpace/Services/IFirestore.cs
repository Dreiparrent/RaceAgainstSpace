using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace RaceAgainstSpace.Services
{
    public interface IFirestore<T>
    {
        Task<IEnumerable<T>> GetCardsAsync(bool forceRefresh = false);
        Task<bool> AddCardAsync(T card);
    }
}
