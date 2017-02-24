using System.Net;
using System.Web.Http;
using VirtoCommerce.LoyaltyModule.Data.Model;
using VirtoCommerce.LoyaltyModule.Data.Services;

namespace VirtoCommerce.LoyaltyModule.Web.Controllers.Api
{
    [RoutePrefix("api/customer/loyalties")]
    public class LoyaltyModuleController : ApiController
    {
        private readonly ICustomerLoyaltyService loyaltyService;

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var loyalties = loyaltyService.GetAll();

            return Ok(loyalties);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Update([FromBody] LoyaltyStatus[] statuses)
        {
            loyaltyService.UpdateStatuses(statuses);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("")]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            loyaltyService.DeleteStatuses(ids);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
