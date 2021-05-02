using System;
using System.Collections.Generic;
using ElevatorApp.Models;
using System.Linq;

namespace ElevatorApp.Helpers
{
    public class ElevatorHelper
    {
        public ElevatorHelper()
        {
        }
        public static Elevator GetNearestElevator(List<Elevator> elevators, int floor)
        {
            if (elevators.Count < 1)
            {
                return null;
            }
            var choosenElevators = new SortedDictionary<int, Elevator>();
            foreach(Elevator elevator in elevators)
            {
                if (elevator.CurrentState == State.Idle)
                {
                    choosenElevators.TryAdd(Math.Abs(floor - elevator.CurrentFloor), elevator);
                }
            }

            return choosenElevators[choosenElevators.Keys.First()];
        }
    }
}
