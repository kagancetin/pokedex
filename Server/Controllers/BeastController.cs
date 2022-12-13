using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Dtos;
using HotChocolate.Execution;
using Microsoft.AspNetCore.Mvc;
using AuthorizeAttribute = Microsoft.AspNetCore.Authorization.AuthorizeAttribute;

namespace BeastBattle.Server.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BeastController : ControllerBase
    {
        private readonly IExecuteGraph executor;
        public BeastController(IExecuteGraph executor)
        {
            this.executor = executor;
        }
        [HttpGet]
        public async Task<ActionResult<PageDto<BeastPageDto>>> GetBeasts()
        {
            string myquery = @"
query{
  beasts{
    id
    name
    imageThumbnail
    nextEvolution {
      id
      name
      imageThumbnail
    }       
  }
}
            ";
            IExecutionResult result = await executor.Execute(myquery);
            return Ok(result.ToJson());
        }

        [HttpGet("paging/{page}")]
        public async Task<ActionResult<PageDto<BeastPageDto>>> GetBeastsPaging(int page, [FromQuery(Name = "filterName")] string? filterName)
        {

            string filter = "";
            if (!String.IsNullOrEmpty(filterName))
            {
                filter = ", where: {name : {contains : \"" + filterName + "\"}}";
            }
            int last = 30;
            int first = page * 30;
            string pageWay = "first: " + first + ", last: " + last;
            string myquery = @"
query{
  beastsPaging(" + pageWay + filter + @"){
    totalCount
    pageInfo {
      hasNextPage
      hasPreviousPage
      startCursor
      endCursor
    }
    edges{
      cursor
      node {
        name
      }
    }
    nodes{
    id
    name
    imageThumbnail
    nextEvolution {
      id
      name
      imageThumbnail
    }
    types{
        name
        color
      } 
    }
  }
}
            ";
            IExecutionResult result = await executor.Execute(myquery);
            return Ok(result.ToJson());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PageDto<BeastPageDto>>> GetBeast(int id)
        {
            string myquery = @"
query {
beast(id: " + id + @"){
    id
    name
    hP
    attack
    defense
    spAttack
    spDefense
    speed
    species
    description
    height
    gender
    weight
    imageThumbnail
    nextEvolution {
      id
      name
      imageThumbnail
      types {
        name
        bgColor
        color
      }
      nextEvolution {
        id
        name
        imageThumbnail
        types {
          name
          bgColor
          color
        }
      }
    }
    abilities{
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
    types {
      name
      bgColor
      color
      effectives {
        typeDetail {
          name
          bgColor
          color
        }
      }
      inEffectives {
        typeDetail {
          name
          bgColor
          color
        }
      }
      noEffects {
        typeDetail {
          name
          bgColor
          color
        }
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