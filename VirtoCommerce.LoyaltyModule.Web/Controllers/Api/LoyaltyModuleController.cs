using System.Net;
using System.Web.Http;
using VirtoCommerce.LoyaltyModule.Data.Model;
using VirtoCommerce.LoyaltyModule.Data.Services;

namespace VirtoCommerce.LoyaltyModule.Web.Controllers.Api
{
    [RoutePrefix("api/loyalty")]
    public class LoyaltyModuleController : ApiController
    {
        private readonly ICustomerLoyaltyService loyaltyService;

        public LoyaltyModuleController(ICustomerLoyaltyService loyaltyService)
        {
            this.loyaltyService = loyaltyService;
        }

        [HttpGet]
        [Route("statuses")]
        public IHttpActionResult GetAll()
        {
            var loyalties = loyaltyService.GetAll();

            return Ok(new { statuses = loyalties });
        }

        [HttpGet]
        [Route("statuses/{id}")]
        public IHttpActionResult GetById([FromUri] string id)
        {
            var loyalty = loyaltyService.GetById(id);

            if (loyalty == null)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            else
            {
                return Ok(loyalty);
            }
        }

        [HttpPost]
        [Route("statuses")]
        public IHttpActionResult Create([FromBody] LoyaltyStatus status)
        {
            var result = loyaltyService.CreateStatus(status);

            return Ok(result);
        }

        [HttpPut]
        [Route("statuses")]
        public IHttpActionResult Update([FromBody] LoyaltyStatus status)
        {
            loyaltyService.UpdateStatuses(new[] {status});

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("statuses")]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            loyaltyService.DeleteStatuses(ids);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
