using System;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElevatorApp.Models
{
    public class Elevator
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public State CurrentState { get; set; }
        public int CurrentFloor { get; set; }
        public int MaxFloor { get; set; }
        public Guid Guid { get; set; }

        public void RunElevator(int floor)
        {
            if (floor < CurrentFloor)
            {
                CurrentState = State.MovingDown;
            }
            else
            {
                CurrentState = State.MovingUp;
            }
            int floorDiff = Math.Abs(CurrentFloor - floor);
            for (int i = 0; i < floorDiff; i++)
            {
                Task.Delay(1000).Wait();
                CurrentFloor = NextFloor(floor, CurrentFloor);

            }
            Task.Delay(2000).Wait();
            CurrentState = State.DoorOpen;
            Task.Delay(2000).Wait();
            CurrentState = State.DoorClose;
            CurrentState = State.Idle;
        }

        private int NextFloor(int floor, int currentFloor)
        {
            if (floor == currentFloor)
            {
                return 0;
            }
            if (floor < currentFloor)
            {
                return currentFloor -= 1;
            }
            return currentFloor += 1;
        }

    }

    public enum State
    {
        Idle,
        MovingUp,
        MovingDown,
        DoorOpen,
        DoorClose,
        OutOfService
    }
}
