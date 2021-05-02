using System;
using System.Collections.Generic;
using ElevatorApp.Models;
using Microsoft.AspNetCore.Mvc;
using ElevatorApp.Helpers;
using ElevatorApp.Controllers;

namespace ElevatorApp
{
    [Route("api/[controller]")]
    public class ElevatorController : BaseController
    {
        private static readonly List<Elevator> _elevators = new List<Elevator> { };

        /// <summary>
        ///  Intialize elevators along with floors
        /// </summary>
        /// <param name="count"></param>
        /// <param name="floors"></param>
        /// <returns></returns>
        // POST api/elevator/init
        [HttpPost()]
        [Route("init")]
        public ActionResult<ApiResponse<IEnumerable<Elevator>>>  IntiateElevator(int count, int floors)
        {
            try
            {
                Logger.Info($"Elevators initiation requested - {count} with floors  - {floors}");
                int i = 0;
                while (i < count) {
                    var item = new Elevator
                    {
                        Guid = Guid.NewGuid(),
                        CurrentState = State.Idle,
                        CurrentFloor = 0,
                        MaxFloor = floors
                    };
                    _elevators.Add(item);
                    i += 1;
                }
                Logger.Info($"Elevators initiated - {count} with floors  - {floors}");
                var apiResponse = new ApiResponse<IEnumerable<Elevator>>()
                {
                    ResponseCode = ResponseCodeType.Success,
                    ResponseObject = _elevators
                };
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                Logger.Error($"initiate elevator failed - {ex.Message}");
                return Ok(new ApiResponse<IEnumerable<Elevator>>()
                {
                    ResponseCode = ResponseCodeType.Error,
                    Message = Resources.Resources.ErrorMessage,
                });
            }
        }

        /// <summary>
        /// Get the elevators and theirs status 
        /// </summary>
        /// <returns></returns>
        // GET: api/elevator
        [HttpGet]
        public ActionResult<IEnumerable<Elevator>> Get()
        {
            try
            {
                Logger.Info("Elevators info requested");
                var apiResponse = new ApiResponse<IEnumerable<Elevator>>()
                {
                    ResponseCode = ResponseCodeType.Success,
                    ResponseObject = _elevators
                };
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                Logger.Error($"GetElevators failed - {ex.Message}");
                return Ok(new ApiResponse<Elevator>()
                {
                    ResponseCode = ResponseCodeType.Error,
                    Message = Resources.Resources.ErrorMessage,
                });
            }

        }

        /// <summary>
        /// Call elevator to floor
        /// </summary>
        /// <param name="floor"></param>
        /// <returns></returns>
        // Get api/elevator/call/3
        [HttpGet]
        [Route("call/{floor}")]
        public ActionResult<ApiResponse<Elevator>> GetElevator(int floor)
        {
            try
            {
                Logger.Info($"Elevators called from floor - {floor}");
                var elevator = ElevatorHelper.GetNearestElevator(_elevators, floor);

                if (floor > elevator.MaxFloor)
                {
                    Logger.Warn($"Elevators called from floor - {floor} - invalid request");
                    return Ok(new ApiResponse<Elevator>()
                    {
                        ResponseCode = ResponseCodeType.Error,
                        Message = Resources.Resources.ExceedsLimit,
                    });
                }
                if (elevator.CurrentState == State.Idle)
                {
                    elevator.RunElevator(floor);
                    Logger.Warn($"Elevator served - {elevator.Guid}");
                    var apiResponse = new ApiResponse<Elevator>()
                    {
                        ResponseCode = ResponseCodeType.Success,
                        ResponseObject = elevator
                    };
                    return Ok(apiResponse);
                }
                Logger.Warn($"Elevators called from floor - {floor} - all busy");
                return Ok(new ApiResponse<Elevator>()
                {
                    ResponseCode = ResponseCodeType.Error,
                    Message = Resources.Resources.Waiting,
                });
            }
            catch (Exception ex)
            {
                Logger.Error($"call Elevator failed - {ex.Message}");
                return Ok(new ApiResponse<Elevator>()
                {
                    ResponseCode = ResponseCodeType.Error,
                    Message = Resources.Resources.ErrorMessage,
                });
            }
            
        }
    }
}
