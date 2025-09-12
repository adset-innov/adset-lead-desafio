namespace adset_webapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class OptionalsController : ControllerBase
    {
        [HttpGet]
        public async Task<IResult> GetAll([FromServices] IGetAllOptionals getAllOptionals)
        {
            var result = await getAllOptionals.Execute();
            return Results.Ok(result);
        }
    }
}
