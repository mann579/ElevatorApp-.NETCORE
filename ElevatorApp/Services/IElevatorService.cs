using System;
using System.Threading.Tasks;
using ElevatorApp.Models;

namespace ElevatorApp.Services
{
    public interface IElevatorService
    {
        Task<Elevator[]> GetElevatorsAsync();
    }
}
