using System;
using System.Threading.Tasks;
using ElevatorApp.Models;

namespace ElevatorApp.Services
{
    public class ElevatorService : IElevatorService
    {

        public Task<Elevator[]> GetElevatorsAsync()
        {
            var item1 = new Elevator
            {
                CurrentState = State.Idle,
                CurrentFloor = 0,
            };

            return Task.FromResult(new[] { item1 });
        }
    }
}
