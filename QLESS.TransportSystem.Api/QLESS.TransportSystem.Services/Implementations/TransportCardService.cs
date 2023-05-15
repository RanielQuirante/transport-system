using QLESS.TransportSystem.Models;
using QLESS.TransportSystem.Repositories.Implementations;
using QLESS.TransportSystem.Repositories.Interface;
using QLESS.TransportSystem.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Transactions;

namespace QLESS.TransportSystem.Services.Implementations
{
    public class TransportCardService : ITransportCardService
    {
        private readonly ITransportCardRepository _transportCardRepository;
        private readonly ITransportCardHistoryRepository _transportCardHistoryRepository;

        public TransportCardService(ITransportCardRepository transportCardRepository, ITransportCardHistoryRepository transportCardHistoryRepository)
        {
            _transportCardRepository = transportCardRepository;
            _transportCardHistoryRepository = transportCardHistoryRepository;
        }

        public async Task<bool> Delete(TransportCard transportCard)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await Task.Run(() => _transportCardRepository.Delete(transportCard));
                transactionScope.Complete();
                return result;
            }
        }

        public async Task<IEnumerable<TransportCard>> Get(TransportCard transportCard)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await Task.Run(() => _transportCardRepository.Get(transportCard));
                transactionScope.Complete();
                return result;
            }
        }

        public async Task<List<TransportCard>> GetTransportCard(TransportCard transportCard, int? pageNumber, int? pageSize)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = new List<TransportCard>();
                if (pageSize != null && pageNumber != null)
                {
                    int offset = (pageSize.Value * (pageNumber.Value - 1));
                    int fetch = pageSize.Value;
                    result = await Task.Run(() => _transportCardRepository.GetTransportCard(transportCard, offset, fetch));
                }
                else
                {
                    result = await Task.Run(() => _transportCardRepository.GetTransportCard(transportCard, null, null));
                }
                transactionScope.Complete();
                return result;
            }
        }


        public async Task<List<TransportCard>> GetTransportCardEntryCount(TransportCard transportCard, int? pageNumber, int? pageSize)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = new List<TransportCard>();
                if (pageSize != null && pageNumber != null)
                {
                    int offset = (pageSize.Value * (pageNumber.Value - 1));
                    int fetch = pageSize.Value;
                    result = await Task.Run(() => _transportCardRepository.GetTransportCardEntryCount(transportCard, offset, fetch));
                }
                else
                {
                    result = await Task.Run(() => _transportCardRepository.GetTransportCardEntryCount(transportCard, null, null));
                }
                transactionScope.Complete();
                return result;
            }
        }


        public async Task<int> Insert(TransportCard transportCard)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                transportCard.CreatedDate = DateTime.Now;

                if (transportCard.IsDiscounted == true)
                {
                    transportCard.LoadAmount = 500;
                    transportCard.ExpirationDate = DateTime.Now.AddYears(3);
                    transportCard.SeniorCitizenId = transportCard.SeniorCitizenId != null ? Regex.Replace(transportCard.SeniorCitizenId.ToUpper(), @"^(..)(....)(....)$", "$1-$2-$3") : null;
                    transportCard.PwdId = transportCard.PwdId != null ? Regex.Replace(transportCard.PwdId.ToUpper(), @"^(....)(....)(....)$", "$1-$2-$3") : null;
                }
                else
                {
                    transportCard.LoadAmount = 100;
                    transportCard.ExpirationDate = DateTime.Now.AddYears(5);
                }

                var result = await Task.Run(() => _transportCardRepository.Insert(transportCard));
                transactionScope.Complete();
                return result;
            }
        }

        public async Task<bool> Update(TransportCard transportCard)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await Task.Run(() => _transportCardRepository.Update(transportCard));
                transactionScope.Complete();
                return result;
            }
        }

        public async Task<bool> AddLoadAmount(TransportCard transportCard)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                transportCard.LoadAmount += transportCard.AddLoadAmount;
                var result = await Task.Run(() => _transportCardRepository.Update(transportCard));
                transactionScope.Complete();
                return result;
            }
        }

        public async Task<bool> EnterStation(TransportCard transportCard)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var transportFee = transportCard.IsDiscounted == true ? 10 : 15;
                if (transportCard.LoadAmount > transportFee)
                {
                    var transportCardHistory = new TransportCardHistory()
                    {
                        TransportCardId = transportCard.Id,
                        CreatedDate = DateTime.Now
                    };

                    transportCard.IsInside = true;
                    var result = await Task.Run(() => _transportCardRepository.EnterStation(transportCard));
                    await Task.Run(() => _transportCardHistoryRepository.Insert(transportCardHistory));
                    transactionScope.Complete();
                    return result;
                } 
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> ExitStation(TransportCard transportCard, bool? isCount = null)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (transportCard.IsDiscounted != null)
                {
                    var originalLoadAmount = transportCard.LoadAmount;
                    transportCard.LoadAmount += transportCard.IsDiscounted == true ? -10 : -15;

                    var transportCardHistory = new TransportCardHistory()
                    {
                        TransportCardId = transportCard.Id
                    };

                    if (isCount == true)
                    {
                        var entryFee = -15.00m;
                        var countEntry = await Task.Run(() => _transportCardHistoryRepository.GetEntryCount(transportCardHistory));

                        if (countEntry >= 2 && countEntry <= 5)
                        {
                            originalLoadAmount += entryFee - (entryFee * .23m);
                        }
                        else
                        {
                            originalLoadAmount += entryFee - (entryFee * .20m);
                        }

                        transportCard.LoadAmount = originalLoadAmount;
                    }
                }

                transportCard.IsInside = false;
                var result = await Task.Run(() => _transportCardRepository.ExitStation(transportCard));
                transactionScope.Complete();
                return result;
            }
        }

        public async Task<decimal> LimitLoadAmount(TransportCard transportCard)
        {
            using (TransactionScope transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = 0.00m;
                var maximumBalance = 10000.00m;
                var loadAmount = transportCard.LoadAmount != null ? transportCard.LoadAmount : 0.00m;
                var addLoadAmount = transportCard.AddLoadAmount != null ? transportCard.AddLoadAmount : 0.00m;
                var customerMoney = transportCard.CustomerMoney != null ? transportCard.CustomerMoney : 0.00m;
                
                var changeAmount = customerMoney - addLoadAmount;
                var totalAmount = loadAmount + addLoadAmount;
                
                if (totalAmount >= maximumBalance)
                {
                    totalAmount -= maximumBalance;
                    transportCard.LoadAmount = maximumBalance;
                    await Task.Run(() => _transportCardRepository.AddLoadAmount(transportCard));
                    result = (decimal)(totalAmount + changeAmount);
                } else
                {
                    transportCard.LoadAmount += addLoadAmount;
                    await Task.Run(() => _transportCardRepository.AddLoadAmount(transportCard));
                    result = (decimal)changeAmount;
                }
                transactionScope.Complete();

                return result;
            }
        }
    }
}
