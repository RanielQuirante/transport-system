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
    public class TransportCardRepository : GenericRepository<TransportCard>, ITransportCardRepository
    {
        private readonly SqlConnection _sqlConnection;

        public TransportCardRepository(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public override async Task<bool> Delete(TransportCard transportCard)
        {
            var result = 0;

            try
            {
                var query = $@"DELETE FROM TransportCard WHERE Id = {transportCard.Id}";
                result = await Task.Run(() => _sqlConnection.Execute(query));
            }
            catch
            {
                result = 0;
            }

            return result > 0;
        }

        public override async Task<IEnumerable<TransportCard>> Get(TransportCard transportCard)
        {
            var result = new List<TransportCard>();

            try
            {
                var query = @"SELECT 
                              	 Id
                                ,LoadAmount
                                ,IsDiscounted
                                ,SeniorCitizenId
                                ,PwdId
                                ,CreatedDate
                                ,ExpirationDate
                                ,isInside
                            FROM
                                TransportCard
                              ";
                query += (transportCard.Id != null) ? $" WHERE Id = {transportCard.Id}" : string.Empty;
                result = await Task.Run(() => _sqlConnection.Query<TransportCard>(query).ToList());
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public async Task<List<TransportCard>> GetTransportCard(TransportCard transportCard, int? offset, int? fetch)
        {
            var result = new List<TransportCard>();
            int isDiscounted = transportCard.IsDiscounted == true ? 1 : 0;
            try
            {
                var query = @"SELECT 
                              	 Id
                                ,LoadAmount
                                ,IsDiscounted
                                ,SeniorCitizenId
                                ,PwdId
                                ,CreatedDate
                                ,ExpirationDate
                                ,isInside
                            FROM
                                TransportCard
                              ";

                query += (transportCard.IsDiscounted != null) ? $" WHERE IsDiscounted = {isDiscounted}" : string.Empty;
                query += (offset != null || fetch != null) ? $" ORDER BY Id DESC OFFSET {offset} ROWS FETCH NEXT {fetch} ROWS ONLY" : string.Empty;
                result = await Task.Run(() => _sqlConnection.Query<TransportCard>(query).ToList());
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public async Task<List<TransportCard>> GetTransportCardEntryCount(TransportCard transportCard, int? offset, int? fetch)
        {
            var result = new List<TransportCard>();
            int isDiscounted = transportCard.IsDiscounted == true ? 1 : 0;
            try
            {
                var query = @"SELECT 
                              	 Id
                                ,LoadAmount
                                ,IsDiscounted
                                ,SeniorCitizenId
                                ,PwdId
                                ,CreatedDate
                                ,ExpirationDate
                                ,isInside		
                                , (SELECT 
			                        COUNT(*) 
		                           FROM 
			                        TransportCardHistory 
		                           WHERE 
                                    TransportCardId = transportCard.Id AND
		                            CAST(CreatedDate AS DATE)  = CAST(GETDATE() AS DATE)) as EntryCount
                                FROM
                                    TransportCard
                              ";

                query += (transportCard.IsDiscounted != null) ? $" WHERE IsDiscounted = {isDiscounted}" : string.Empty;
                query += (offset != null || fetch != null) ? $" ORDER BY Id DESC OFFSET {offset} ROWS FETCH NEXT {fetch} ROWS ONLY" : string.Empty;
                result = await Task.Run(() => _sqlConnection.Query<TransportCard>(query).ToList());
            }
            catch
            {
                result = null;
            }

            return result;
        }

        public override async Task<int> Insert(TransportCard transportCard)
        {
            var id = 0;

            try
            {
                var query = @"INSERT INTO TransportCard
                                (
                                    LoadAmount
                                   ,IsDiscounted
                                   ,SeniorCitizenId
                                   ,PwdId
                                   ,CreatedDate
                                   ,ExpirationDate
                                )
                                VALUES
                                (
                                    @LoadAmount
                                   ,@IsDiscounted
                                   ,@SeniorCitizenId
                                   ,@PwdId
                                   ,@CreatedDate
                                   ,@ExpirationDate
                                );
                                SELECT CAST(SCOPE_IDENTITY() as int);";

                id = await Task.Run(() => _sqlConnection.Query<int>(query, transportCard).SingleOrDefault());
            }
            catch
            {
                id = 0;
            }

            return id;
        }

        public override async Task<bool> Update(TransportCard transportCard)
        {
            var count = 0;

            try
            {
                var query = @"UPDATE TransportCard
                                SET
                                    LoadAmount = @LoadAmount
                                WHERE 
                                    Id = @Id;";

                count = await Task.Run(() => _sqlConnection.Execute(query, transportCard));
            }
            catch
            {
                count = 0;
            }

            return count > 0;
        }

        public async Task<bool> AddLoadAmount(TransportCard transportCard)
        {
            var count = 0;

            try
            {
                var query = @"UPDATE TransportCard
                                SET
                                    LoadAmount = @LoadAmount
                                WHERE 
                                    Id = @Id;";

                count = await Task.Run(() => _sqlConnection.Execute(query, transportCard));
            }
            catch
            {
                count = 0;
            }

            return count > 0;
        }

        public async Task<bool> EnterStation(TransportCard transportCard)
        {
            var count = 0;

            try
            {
                var query = @"UPDATE TransportCard
                                SET
                                    IsInside = @IsInside
                                WHERE 
                                    Id = @Id;";

                count = await Task.Run(() => _sqlConnection.Execute(query, transportCard));
            }
            catch
            {
                count = 0;
            }

            return count > 0;
        }

        public async Task<bool> ExitStation(TransportCard transportCard)
        {
            var count = 0;

            try
            {
                var query = @"UPDATE TransportCard
                                SET
                                    LoadAmount = @LoadAmount,
                                    IsInside = @IsInside
                                WHERE 
                                    Id = @Id;";

                count = await Task.Run(() => _sqlConnection.Execute(query, transportCard));
            }
            catch
            {
                count = 0;
            }

            return count > 0;
        }
    }
}
