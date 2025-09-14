using ClaimRequest.Apis.Extensions;
using ClaimRequest.Apis.Requests;
using ClaimRequest.Application.CompanySettings.CreateCompanySetting;
using ClaimRequest.Application.CompanySettings.GetCompanySettings;
using ClaimRequest.Application.CompanySettings.UpdateCompanySettings;
using ClaimRequest.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimRequest.Apis.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CompanySettingsController : ControllerBase
    {
        private readonly ISender _mediator;

        public CompanySettingsController(ISender mediator)
        {
            _mediator = mediator;
        }

        //UPDATE
        [Authorize(Roles = "Admin")]
        [HttpPut("companysettings/update")]
        public async Task<IResult> UpdateCompanySettings([FromBody] UpdateCompanySettingsRequest request,
            CancellationToken cancellationToken)
        {
            var command = new UpdateCompanySettingsCommand
            {
                Id = request.Id,
                LimitDayOff = request.LimitDayOff,
                WorkStartTime = request.WorkStartTime,
                WorkEndTime = request.WorkEndTime,
                FinePerAbnormalCase = request.FinePerAbnormalCase,
                SalaryPerOvertimeHour = request.SalaryPerOvertimeHour,
                FinePerLateEarlyCase = request.FinePerLateEarlyCase,
                
            };
            Result result = await _mediator.Send(command, cancellationToken);

            return result.MatchOk();
        }

        //CREATE
        [Authorize(Roles = "Admin")]
        [HttpPost("companysettings/create")]
        public async Task<IResult> CreateCompanySetting([FromBody] CreateCompanySettingRequest request,
            CancellationToken cancellationToken)
        {
            CreateCompanySettingCommand command = new CreateCompanySettingCommand
            {
                LimitDayOff = request.LimitDayOff,
                WorkStartTime = request.WorkStartTime,
                WorkEndTime = request.WorkEndTime,
                FinePerAbnormalCase = request.FinePerAbnormalCase,
                SalaryPerOvertimeHour = request.SalaryPerOvertimeHour,
                FinePerLateEarlyCase = request.FinePerLateEarlyCase,
                
                
            };
            Result result = await _mediator.Send(command, cancellationToken);

            return result.MatchOk();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("companysettings/get-setting-data")]
        public async Task<IResult> GetListCompanySettings(CancellationToken cancellationToken)
        {
            Result<GetCompanySettingQueryResponse> result = await _mediator.Send(new GetCompanySettingQuery(), cancellationToken);

            return result.MatchOk();
        }
    }
}