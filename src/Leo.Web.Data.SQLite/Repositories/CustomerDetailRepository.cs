﻿// MIT License

using System.Data;
using System.Data.Common;
using Dapper;
using Leo.Data.Domain.Entities;

namespace Leo.Web.Data.SQLite.Repositories
{
    internal sealed class CustomerDetailRepository(IDbConnectionFactory dbConnectionManager) : ICustomerDetailRepository
    {
        public async Task<Guid> CreateAsync(CustomerDetail detail)
        {
            detail.Id = Guid.NewGuid();

            var parameters = new DynamicParameters();
            parameters.Add("id", detail.Id, dbType: DbType.String);
            parameters.Add("customer_id", detail.CustomerId);
            parameters.Add("date", detail.Date);
            parameters.Add("item", detail.Item);
            parameters.Add("count", detail.Count);
            parameters.Add("height", detail.Height);
            parameters.Add("weight", detail.Weight);
            parameters.Add("created_at", detail.CreatedAt);
            parameters.Add("created_by", detail.CreatedBy);
            string commandText = "INSERT INTO customer_detail (id, customer_id, date, item, count, height, weight,"
                + "created_at, created_by) "
                + "VALUES (@id, @customer_id, @date, @item, @count, @height, @weight, "
                + "@created_at, @created_by)";
            var cmdDef = new CommandDefinition(commandText, parameters);
            using DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            await conn.ExecuteAsync(cmdDef).ConfigureAwait(false);
            return detail.Id;
        }

        public async Task<CustomerDetail?> GetByIdAsync(Guid id)
        {
            string commandText = "SELECT * FROM customer_detail WHERE id = @id";
            var parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.String);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryFirstOrDefaultAsync<CustomerDetail>(cmdDef).ConfigureAwait(false);
        }

        public async Task<IEnumerable<CustomerDetail>> GetByCustomerIdAsync(Guid customerId)
        {
            string commandText = "SELECT * FROM customer_detail WHERE customer_id = @customer_id";
            var parameters = new DynamicParameters();
            parameters.Add("customer_id", customerId, DbType.String);
            var cmdDef = new CommandDefinition(commandText, parameters);
            using DbConnection conn = await dbConnectionManager.OpenAsync().ConfigureAwait(false);
            return await conn.QueryAsync<CustomerDetail>(cmdDef).ConfigureAwait(false);
        }
    }
}
