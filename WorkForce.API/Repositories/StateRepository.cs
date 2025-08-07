using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using WorkForce.API.Mappers;
using WorkForce.API.Models.State;

namespace WorkForce.API.Repositories
{
    public class StateRepository : IStateRepository
    {

        private readonly string _connectionString;

        public StateRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("Connection string not found");
        }


        public async Task<IEnumerable<StateResponse>> GetActiveStatesAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Active", 1, DbType.Boolean);

            var states = await connection.QueryAsync<State>(
                "SP_States_GetStates",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return states.Select(s => s.ToResponseDto());
        }



        public async Task<StateResponse?> GetStateByIdAsync(int id)
        {
            if (id <= 0) return null;

            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            var state = await connection.QueryFirstOrDefaultAsync<State>(
                "SP_States_GetState",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return state?.ToResponseDto();
        }

        public async Task<StateResponse> CreateStateAsync(StateRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Code", request.Code, DbType.String);
            parameters.Add("@Name", request.Name, DbType.String);
            parameters.Add("@Active", request.Active, DbType.Boolean);
            parameters.Add("@NewId", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@ResultMessage", dbType: DbType.String, direction: ParameterDirection.Output, size: 500);

            await connection.ExecuteAsync(
                "sp_CreateState",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var newId = parameters.Get<int>("@NewId");
            var resultMessage = parameters.Get<string>("@ResultMessage");

            if (newId == 0)
            {
                throw new InvalidOperationException(resultMessage ?? "Error creating state");
            }

            // Retornar el estado creado
            return await GetStateByIdAsync(newId)
                ?? throw new InvalidOperationException("Error retrieving created state");
        }

        public async Task<StateResponse?> UpdateStateAsync(int id, StateRequest request)
        {
            if (id <= 0 || request == null) return null;

            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);
            parameters.Add("@Code", request.Code, DbType.String);
            parameters.Add("@Name", request.Name, DbType.String);
            parameters.Add("@Active", request.Active, DbType.Boolean);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_UpdateState",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            var rowsAffected = parameters.Get<int>("@RowsAffected");

            return rowsAffected > 0 ? await GetStateByIdAsync(id) : null;
        }

        public async Task<bool> DeleteStateAsync(int id)
        {
            if (id <= 0) return false;

            using var connection = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);
            parameters.Add("@RowsAffected", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_DeleteState",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<int>("@RowsAffected") > 0;
        }
    }
}









