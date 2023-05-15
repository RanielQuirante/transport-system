using Microsoft.Extensions.Configuration;
using QLESS.TransportSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using QLESS.TransportSystem.Repositories.Interface;

namespace QLESS.TransportSystem.Repositories.Implementations
{
    public class TransportCardHistoryRepository : GenericRepository<TransportCardHistory>, ITransportCardHistoryRepository
    {
        private readonly SqlConnection _sqlConnection;

        public TransportCardHistoryRepository(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public override Task<bool> Delete(TransportCardHistory tModel)
        {
            throw new NotImplementedException();
        }

        public override Task<IEnumerable<TransportCardHistory>> Get(TransportCardHistory tModel)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetEntryCount(TransportCardHistory transportCardHistory)
        {
            var result = 0;

            try
            {
                var query = @"SELECT 
                                COUNT(*) 
                            FROM 
                                TransportCardHistory
                            WHERE 
                                CAST(CreatedDate AS DATE)  = CAST(GETDATE() AS DATE) AND
                                TransportCardId = @TransportCardId
                            ";

                result = await Task.Run(() => _sqlConnection.Query<int>(query, transportCardHistory).SingleOrDefault());
            }
            catch
            {
                result = 0;
            }

            return result;
        }

        public override async Task<int> Insert(TransportCardHistory transportCardHistory)
        {
            var id = 0;

            try
            {
                var query = @"INSERT INTO TransportCardHistory
                                (
                                    TransportCardId
                                   ,CreatedDate
                                )
                                VALUES
                                (
                                    @TransportCardId
                                   ,@CreatedDate
                                );
                                SELECT CAST(SCOPE_IDENTITY() as int);";

                id = await Task.Run(() => _sqlConnection.Query<int>(query, transportCardHistory).SingleOrDefault());
            }
            catch
            {
                id = 0;
            }

            return id;
        }

        public override Task<bool> Update(TransportCardHistory tModel)
        {
            throw new NotImplementedException();
        }
    }
}
