using EFCore.Domain;
using System.Threading.Tasks;

namespace EFCore.Repository
{
    public interface IEFCoreRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangeAsync();
        Task<Heroi[]> GetAllHerois(bool incluirBatalhas = false);
        Task<Heroi> GetHeroiById(int id, bool incluirBatalhas = false);
        Task<Heroi[]> GetHeroisByNome(string nome, bool incluirBatalhas = false);

        Task<Batalha[]> GetAllBatalhas(bool incluirHerois = false);
        Task<Batalha> GetBatalhaById(int id, bool incluirHerois = false);
    }
}
