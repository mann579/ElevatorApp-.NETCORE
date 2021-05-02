using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ElevatorApp.Models;
using Xunit;

namespace ElevatorApp.Tests
{
    public class ElevatorTests
    {
        [Fact]
        public void TestElevator_Instances()
        {
            var elevators = new List<Elevator>() { };
            for (int i = 0; i < 7; i++)
            {
                var elevator = new Elevator() { CurrentFloor = 10, CurrentState = State.Idle, MaxFloor = 10 };
                elevators.Add(elevator);

            }
            Assert.Equal(7, elevators.Count);

        }

        [Fact]
        public void TestElevator_MovingUp()
        {

            var elevator = new Elevator() { CurrentFloor = 0, CurrentState = State.Idle, MaxFloor = 10 };
            new Thread(() =>
            {
                elevator.RunElevator(9);
            }).Start();
            Task.Delay(1000).Wait();
            Assert.Equal(State.MovingUp, elevator.CurrentState);

        }

        [Fact]
        public void TestElevator_MovingDown()
        {

            var elevator = new Elevator() { CurrentFloor = 10, CurrentState = State.Idle, MaxFloor = 10 };
            new Thread(() =>
            {
                elevator.RunElevator(2);
            }).Start();
            Task.Delay(1000).Wait();
            Assert.Equal(State.MovingDown, elevator.CurrentState);

        }

        [Fact]
        public void TestElevator_Idle()
        {

            var elevator = new Elevator() { CurrentFloor = 10, CurrentState = State.Idle, MaxFloor = 10 };
            new Thread(() =>
            {
                elevator.RunElevator(2);
            }).Start();
            Task.Delay(12500).Wait();
            Assert.Equal(State.Idle, elevator.CurrentState);

        }

        [Fact]
        public void TestElevator_DoorOpen()
        {

            var elevator = new Elevator() { CurrentFloor = 10, CurrentState = State.Idle, MaxFloor = 10 };
            new Thread(() =>
            {
                elevator.RunElevator(10);
            }).Start();
            Task.Delay(2500).Wait();
            Assert.Equal(State.DoorOpen, elevator.CurrentState);

        }

    }
}
