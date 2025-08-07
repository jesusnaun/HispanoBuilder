using WorkForce.API.Models.State;

namespace WorkForce.API.Repositories
{
    public interface IStateRepository
    {
        Task<IEnumerable<StateResponse>> GetActiveStatesAsync();
        Task<StateResponse?> GetStateByIdAsync(int id);
        Task<StateResponse> CreateStateAsync(StateRequest request);
        Task<StateResponse?> UpdateStateAsync(int id, StateRequest request);
        Task<bool> DeleteStateAsync(int id);
    }
}
