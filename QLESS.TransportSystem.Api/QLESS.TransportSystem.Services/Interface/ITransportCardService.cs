using QLESS.TransportSystem.Models;

namespace QLESS.TransportSystem.Services.Interface
{
    public interface ITransportCardService
    {
        Task<bool> AddLoadAmount(TransportCard transportCard);
        Task<bool> Delete(TransportCard transportCard);
        Task<bool> EnterStation(TransportCard transportCard);
        Task<bool> ExitStation(TransportCard transportCard, bool? isCount);
        Task<IEnumerable<TransportCard>> Get(TransportCard transportCard);
        Task<List<TransportCard>> GetTransportCard(TransportCard transportCard, int? pageNumber, int? pageSize);
        Task<List<TransportCard>> GetTransportCardEntryCount(TransportCard transportCard, int? pageNumber, int? pageSize);
        Task<int> Insert(TransportCard transportCard);
        Task<decimal> LimitLoadAmount(TransportCard transportCard);
        Task<bool> Update(TransportCard transportCard);
    }
}