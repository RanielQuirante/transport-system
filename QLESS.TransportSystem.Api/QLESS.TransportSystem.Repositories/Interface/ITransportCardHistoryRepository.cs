using QLESS.TransportSystem.Models;

namespace QLESS.TransportSystem.Repositories.Interface
{
    public interface ITransportCardHistoryRepository
    {
        Task<bool> Delete(TransportCardHistory tModel);
        Task<IEnumerable<TransportCardHistory>> Get(TransportCardHistory tModel);
        Task<int> GetEntryCount(TransportCardHistory transportCardHistory);
        Task<int> Insert(TransportCardHistory transportCardHistory);
        Task<bool> Update(TransportCardHistory tModel);
    }
}