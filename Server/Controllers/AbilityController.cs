using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Dtos;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace BeastBattle.Server.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class AbilityController : ControllerBase
    {
        private readonly IExecuteGraph executor;

        public AbilityController(IExecuteGraph executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public async Task<ActionResult<PageDto<AbilityPageDto>>> GetAbilities(int size, int page, string sort = "name", string sortType = "ASC")
        {
            int last = size;
            int first = page * size;
            string pageWay = "first: " + first + ", last: " + last;
            string sortQuery = "order: {" + sort + " :" + sortType + "}";
            string myquery = @"
query {
  abilityPaging(" + pageWay + ", " + sortQuery + @") {
    pageInfo {
      hasNextPage
      hasPreviousPage
      startCursor
      endCursor
    }
    totalCount
    nodes {
      id
      accuracy
      name
      power
      pP
      type {
        name
        color
        bgColor
      }
    }
  }
}
            ";
            IExecutionResult result = await executor.Execute(myquery);
            return Ok(result.ToJson());
        }

        [HttpGet("solo/{id}")]
        public async Task<ActionResult<PageDto<AbilityDataDto>>> GetAbility(int id)
        {
            string myquery = @"
query {
  ability(id: " + id + @") {
    id
      accuracy
      name
      power
      pP
      type {
        name
        color
        bgColor
        beasts{
          id
          name
          imageThumbnail
        }
      }
  }
}            
            ";
            IExecutionResult result = await executor.Execute(myquery);
            return Ok(result.ToJson());
        }
    }

}