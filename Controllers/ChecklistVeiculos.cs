using System.Runtime.CompilerServices;
using ChecklistVeiculos.Dto.Request;
using ChecklistVeiculos.Dto.Response;
using ChecklistVeiculos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistVeiculos.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ChecklistVeiculos : ControllerBase
    {
        private readonly ChecklistVeiculosService veiculosService;

        public ChecklistVeiculos(ChecklistVeiculosService veiculosService)
        {
            this.veiculosService = veiculosService;
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListCreatedDTO>> CreateChecklist([FromBody] NewCheckListDTO newCheckListDTO)
        {
            var created = await veiculosService.CreateChecklist(newCheckListDTO);
            if(created == null)
            {
                // Created nunca deveria ser null, pois no caso de erro, 
                // uma exceção deveria ser lançada
                return BadRequest( new { message = "Erro ao criar checklist" });
            }
            return Ok(created);
        }
        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListCreatedDTO>> GetChecklist([FromRoute] int id)
        {
            var checklist = await veiculosService.GetChecklist(id);
            if(checklist == null)
            {
                return NotFound($"Checklist Id {id} não encontrado");
            }
            return Ok(checklist);
        }

        [HttpPatch]
        [Route("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListCreatedDTO>> UpdateChecklist([FromRoute] int id, [FromBody] UpdateCheckListDTO updateCheckListDTO)
        {
            var updated = await veiculosService.UpdateChecklist(id, updateCheckListDTO);
            if(updated == null)
            {
                return NotFound($"Checklist Id {id} não encontrado");
            }
            return Ok(updated);
        }
        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListCreatedDTO>> DeleteChecklist([FromRoute] int id)
        {
            var deleted = await veiculosService.DeleteChecklist(id);
            if(deleted == null)
            {
                return NotFound($"Checklist Id {id} não encontrado");
            }
            return Ok(deleted);
        }
    }
}
