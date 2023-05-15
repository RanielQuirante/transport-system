using QLESS.TransportSystem.Models;

namespace QLESS.TransportSystem.Repositories.Interface
{
    public interface ITransportCardRepository
    {
        Task<bool> AddLoadAmount(TransportCard transportCard);
        Task<bool> Delete(TransportCard transportCard);
        Task<bool> EnterStation(TransportCard transportCard);
        Task<bool> ExitStation(TransportCard transportCard);
        Task<IEnumerable<TransportCard>> Get(TransportCard transportCard);
        Task<List<TransportCard>> GetTransportCard(TransportCard transportCard, int? offset, int? fetch);
        Task<List<TransportCard>> GetTransportCardEntryCount(TransportCard transportCard, int? offset, int? fetch);
        Task<int> Insert(TransportCard transportCard);
        Task<bool> Update(TransportCard transportCard);
    }
}