using Microsoft.AspNetCore.Mvc;
using Sikoia.ApiGateway.Dtos;
using Sikoia.Application.Enums;
using Sikoia.Application.Queries;
using Sikoia.Application.Queries.Company;
using Sikoia.Application.ReadModels;

namespace Sikoia.ApiGateway.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IAsyncQueryHandler<FindCompanyByJurisdictionQuery, CompanyReadModel> findCompanyQueryHandler;

        public CompanyController(IAsyncQueryHandler<FindCompanyByJurisdictionQuery, CompanyReadModel> findCompanyQueryHandler)
        {
            this.findCompanyQueryHandler = findCompanyQueryHandler;
        }

        /// <summary>
        /// Retrieves a single company record
        /// </summary>
        /// <param name="jurisdictionCode"></param>
        /// <param name="companyNumber"></param>
        /// <returns>A single company record inside a <see cref="CompanyReadModel"/> instance</returns>
        [HttpGet]
        [Route("{jurisdictionCode}/{companyNumber}")]
        public async Task<IActionResult> Get(JurisdictionCode jurisdictionCode, int companyNumber)
        {
            var query = new FindCompanyByJurisdictionQuery(jurisdictionCode, companyNumber);
            var readModel = await findCompanyQueryHandler.HandleAsync(query);

            if (readModel.HasError)
            {
                return BadRequest(new ErrorResponseDto(readModel.ErrorMessage!));
            }

            return Ok(readModel.Company);
        }
    }
}