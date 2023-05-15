using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLESS.TransportSystem.Api.Models;
using QLESS.TransportSystem.Models;
using QLESS.TransportSystem.Models.Enums;
using QLESS.TransportSystem.Services.Interface;

namespace QLESS.TransportSystem.Api.Controllers
{
    [Route("api/transport-card")]
    [ApiController]
    public class TransportCardController : ControllerBase
    {
        private readonly ITransportCardService _transportCardService;

        public TransportCardController(ITransportCardService transportCardService)
        {
            _transportCardService = transportCardService;
        }

        [Route("add"), HttpPost]
        public async Task<IActionResult> Add(TransportCard transportCard)
        {
            var result = await Task.Run(() => _transportCardService.Insert(transportCard));
            if (result == 0)
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.BadRequest.GetDescription()
                });
            }

            return Ok(new BaseResponse<string>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription(),
                Data = result.ToString()
            });
        }

        [Route("{id:int}/update"), HttpPut]
        public async Task<IActionResult> Update(int id, TransportCard transportCard)
        {
            transportCard.Id = id;
            var result = await Task.Run(() => _transportCardService.Update(transportCard));

            if (!result)
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.NotFound.GetDescription()
                });
            }

            return Ok(new BaseResponse<string>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription()
            });

        }

        [Route("{id:int}/delete"), HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var transportCard = new TransportCard()
            {
                Id = id
            };

            var result = await Task.Run(() => _transportCardService.Delete(transportCard));

            if (!result)
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.NotFound.GetDescription()
                });
            }

            return Ok(new BaseResponse<string>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription()
            });
        }

        [Route("get"), HttpGet]
        public async Task<IActionResult> Get(int? id = null)
        {
            var transportCard = new TransportCard()
            {
                Id = id
            };
            var result = await Task.Run(() => _transportCardService.Get(transportCard));
            if (!result.Any())
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.NotFound.GetDescription()
                });
            }

            return Ok(new BaseResponse<IEnumerable<TransportCard>>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription(),
                Rows = result.Count(),
                Data = result
            });
        }

        [Route("discounted/get"), HttpGet]
        public async Task<IActionResult> GetTransportCard(bool? isDiscounted = null, int pageNumber = 1, int pageSize = 10)
        {
            var transportCard = new TransportCard()
            {
                IsDiscounted = isDiscounted
            };
            var result = await Task.Run(() => _transportCardService.GetTransportCard(transportCard, pageNumber, pageSize));
            if (!result.Any())
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.NotFound.GetDescription()
                });
            }

            return Ok(new BaseResponse<IEnumerable<TransportCard>>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription(),
                Rows = (await Task.Run(() => _transportCardService.GetTransportCard(transportCard, null, null))).Count(),
                Data = result
            });
        }

        [Route("entry-count/get"), HttpGet]
        public async Task<IActionResult> GetTransportCardEntryCount(bool? isDiscounted = null, int pageNumber = 1, int pageSize = 10)
        {
            var transportCard = new TransportCard()
            {
                IsDiscounted = isDiscounted
            };
            var result = await Task.Run(() => _transportCardService.GetTransportCardEntryCount(transportCard, pageNumber, pageSize));
            if (!result.Any())
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.NotFound.GetDescription()
                });
            }

            return Ok(new BaseResponse<IEnumerable<TransportCard>>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription(),
                Rows = (await Task.Run(() => _transportCardService.GetTransportCard(transportCard, null, null))).Count(),
                Data = result
            });
        }

        [Route("{id:int}/add-load-amount"), HttpPut]
        public async Task<IActionResult> AddLoadAmount(int id, TransportCard transportCard)
        {
            transportCard.Id = id;
            var result = await Task.Run(() => _transportCardService.AddLoadAmount(transportCard));

            if (!result)
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.NotFound.GetDescription()
                });
            }

            return Ok(new BaseResponse<string>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription()
            });
        }


        [Route("{id:int}/enter-station"), HttpPut]
        public async Task<IActionResult> EnterStation(int id, TransportCard transportCard)
        {
            var result = await Task.Run(() => _transportCardService.EnterStation(transportCard));
            if (!result)
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.BadRequest.GetDescription()
                });
            }

            return Ok(new BaseResponse<string>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription()
            });
        }


        [Route("{id:int}/exit-station"), HttpPut]
        public async Task<IActionResult> ExitStation(int id, TransportCard transportCard, bool? isCount = null)
        {
            var result = await Task.Run(() => _transportCardService.ExitStation(transportCard, isCount));
            if (!result)
            {
                return Ok(new BaseResponse<IEnumerable<string>>
                {
                    IsSuccess = false,
                    Message = HttpResponseCode.BadRequest.GetDescription()
                });
            }

            return Ok(new BaseResponse<string>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription()
            });
        }

        [Route("{id:int}/limit-load-amount"), HttpPut]
        public async Task<IActionResult> LimitLoadAmount(int id, TransportCard transportCard)
        {
            transportCard.Id = id;
            var result = await Task.Run(() => _transportCardService.LimitLoadAmount(transportCard));

            return Ok(new BaseResponse<string>
            {
                IsSuccess = true,
                Message = HttpResponseCode.OK.GetDescription(),
                Data = result.ToString()
            });
        }
    }
}
