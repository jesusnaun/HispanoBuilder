using WorkForce.API.Models.State;

namespace WorkForce.API.Mappers
{
    public static class StateExtension
    {
        public static StateResponse ToResponseDto(this State state)
        {
            return new StateResponse
            {
                Id = state.Id,
                Code = state.Code,
                Name = state.Name,
                Active = state.Active
            };
        }

        public static State ToEntity(this StateRequest request)
        {
            return new State
            {
                Code = request.Code,
                Name = request.Name,
                Active = request.Active
            };
        }
    }
}
