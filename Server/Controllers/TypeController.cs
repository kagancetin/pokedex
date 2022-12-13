using BeastBattle.Server.Services.Contracts;
using BeastBattle.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotChocolate.Execution;

namespace Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TypeController : ControllerBase
    {
        private readonly IExecuteGraph executor;
        public TypeController(IExecuteGraph executor)
        {
            this.executor = executor;
        }

        [HttpGet]
        public async Task<ActionResult<PageDto<TypePageDto>>> GetTypes()
        {
            string myquery = @"
query {
  types {
    name
    color
    bgColor
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

            ";
            IExecutionResult result = await executor.Execute(myquery);
            return Ok(result.ToJson());
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<PageDto<TypePageDto>>> GetType(string name)
        {
            string myquery = @"
 query {
    type(name:""" + name + @"""){
      name
    color
    bgColor
    effectives {
      typeDetail {
        name
        color
        bgColor
      }
    }
    inEffectives {
      typeDetail {
        name
        color
        bgColor
      }
    }
    noEffects {
      typeDetail {
        name
        color
        bgColor
      }
    }
    beasts {
      id
      name
      imageThumbnail
    }
    abilities{
      id
      accuracy
      name
      power
      pP 
    }
  }
}
            ";
            IExecutionResult result = await executor.Execute(myquery);
            return Ok(result.ToJson());
        }
    }
}