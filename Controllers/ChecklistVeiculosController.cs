using System.Runtime.CompilerServices;
using ChecklistVeiculos.Dto.Request;
using ChecklistVeiculos.Dto.Response;
using ChecklistVeiculos.Models;
using ChecklistVeiculos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChecklistVeiculos.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class ChecklistVeiculosController(
        ChecklistVeiculosService veiculosService,
        ILogger<ChecklistVeiculosController> logger
    ) : ControllerBase
    {
        private readonly ChecklistVeiculosService veiculosService = veiculosService;

        public ILogger<ChecklistVeiculosController> Logger { get; } = logger;

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListCreatedDTO>> CreateChecklist(
            [FromBody] NewCheckListDTO newCheckListDTO
        )
        {
            var created = await veiculosService.CreateChecklist(newCheckListDTO, GetLoggedUser());
            if (created == null)
            {
                return BadRequest(new { message = "Erro ao criar checklist" });
            }
            return Created(string.Empty, created);
        }

        [HttpGet]
        [Route("get/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListCreatedDTO>> GetChecklist([FromRoute] int id)
        {
            var checklist = await veiculosService.GetChecklist(id);
            if (checklist == null)
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
        public async Task<ActionResult<CheckListCreatedDTO>> UpdateChecklist(
            [FromRoute] int id,
            [FromBody] UpdateCheckListDTO updateCheckListDTO
        )
        {
            var updated = await veiculosService.UpdateChecklist(id, updateCheckListDTO);
            if (updated == null)
            {
                return NotFound($"Checklist Id {id} não encontrado");
            }
            return Ok(updated);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListCreatedDTO>> DeleteChecklist([FromRoute] int id)
        {
            var deleted = await veiculosService.DeleteChecklist(id);
            if (deleted == null)
            {
                return NotFound($"Checklist Id {id} não encontrado");
            }
            return Accepted();
        }

        [HttpPost]
        [Route("item/create/{checklistId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListItemCreatedDTO>> CreateChecklistItem(
            [FromRoute] int checklistId,
            [FromBody] NewCheckListItemDTO newCheckListItemDTO
        )
        {
            var created = await veiculosService.AddChecklistItem(
                checklistId,
                newCheckListItemDTO,
                GetLoggedUser()
            );
            if (created == null)
            {
                return BadRequest(new { message = "Erro ao criar item de checklist" });
            }
            return Ok(created);
        }

        [HttpPatch]
        [Route("item/update/{checklistId}/{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateChecklistItem(
            [FromRoute] int checklistId,
            [FromRoute] int itemId,
            [FromBody] UpdateCheckListItemDTO updateCheckListItemDTO
        )
        {
            var updated = await veiculosService.UpdateChecklistItem(
                checklistId,
                itemId,
                updateCheckListItemDTO
            );
            if (updated == null)
            {
                return NotFound($"Item de checklist Id {itemId} não encontrado");
            }
            return Ok(updated);
        }

        [HttpDelete]
        [Route("item/delete/{checklistId}/{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteChecklistItem(
            [FromRoute] int checklistId,
            [FromRoute] int itemId
        )
        {
            var deleted = await veiculosService.DeleteChecklistItem(checklistId, itemId);
            if (deleted == null)
            {
                return NotFound($"Item de checklist Id {itemId} não encontrado");
            }
            return Ok(deleted);
        }

        [HttpPost]
        [Route("item/observacao/create/{checklistId}/{itemId}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CheckListObservacaoCreatedDTO>> CreateChecklistObservacao(
            [FromRoute] int checklistId,
            [FromRoute] int itemId,
            [FromBody] NewCheckListItemObservacaoDTO newCheckListObservacaoDTO
        )
        {
            var created = await veiculosService.AddChecklistItemObservacao(
                checklistId,
                itemId,
                newCheckListObservacaoDTO,
                GetLoggedUser()
            );
            if (created == null)
            {
                return BadRequest(
                    new { message = "Erro ao criar observação de item de checklist" }
                );
            }
            return Ok(created);
        }

        [HttpPatch]
        [Route("item/observacao/update/{checklistId}/{itemId}/{observacaoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateChecklistObservacao(
            [FromRoute] int checklistId,
            [FromRoute] int itemId,
            [FromRoute] int observacaoId,
            [FromBody] UpdateCheckListObservacaoDTO updateCheckListObservacaoDTO
        )
        {
            var updated = await veiculosService.UpdateChecklistItemObservacao(
                checklistId,
                itemId,
                observacaoId,
                updateCheckListObservacaoDTO
            );
            if (updated == null)
            {
                return NotFound(
                    $"Observação de item de checklist Id {observacaoId} não encontrada"
                );
            }
            return Ok(updated);
        }

        [HttpDelete]
        [Route("item/observacao/delete/{checklistId}/{itemId}/{observacaoId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteChecklistObservacao(
            [FromRoute] int checklistId,
            [FromRoute] int itemId,
            [FromRoute] int observacaoId
        )
        {
            var deleted = await veiculosService.DeleteChecklistItemObservacao(
                checklistId,
                itemId,
                observacaoId
            );
            if (deleted == null)
            {
                return NotFound(
                    $"Observação de item de checklist Id {observacaoId} não encontrada"
                );
            }
            return Ok(deleted);
        }

        [HttpGet]
        [Route("list-by-status")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CheckListCreatedDTO>>> GetChecklistByStatus(
            [FromQuery] ChecklistStatusEnum? status,
            [FromQuery] int? takeLast = null
        )
        {
            var checklists = await veiculosService.GetChecklists(status, takeLast);
            return Ok(checklists);
        }

        [HttpGet]
        [Route("list-by-placa/{placa}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CheckListCreatedDTO>>?> GetChecklistByPlaca(
            [FromRoute] string placa
        )
        {
            var checklists = await veiculosService.GetChecklistsByPlaca(placa);
            return Ok(checklists);
        }

        [HttpGet]
        [Route("list-by-placa-and-executor/{placa}/{executor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CheckListCreatedDTO>>?> GetChecklistByPlacaAndExecutor(
            [FromRoute] string placa,
            [FromRoute] string executor
        )
        {
            var checklists = await veiculosService.GetChecklistsByPlacaAndExecutor(placa, executor);
            return Ok(checklists);
        }

        [HttpGet]
        [Route("list-by-executor/{executor}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<CheckListCreatedDTO>>?> GetChecklistByExecutor(
            [FromRoute] string executor
        )
        {
            var checklists = await veiculosService.GetChecklistsByExecutor(executor);
            return Ok(checklists);
        }

        [HttpPost]
        [Route("change-status/{id}/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> ChangeChecklistStatus(
            [FromRoute] int id,
            [FromRoute] ChecklistStatusEnum status
        )
        {
            var changed = await veiculosService.ChangeChecklistStatus(id, status);
            if (changed == null)
            {
                return NotFound($"Checklist Id {id} não encontrado");
            }
            return Ok(changed);
        }

        /**** Utilitiy methods ****/
        /// <summary>
        ///   Retornar o usuário logado
        ///   Forma provisória de obter o usuário logado
        ///   Enquanto a forma JWT não é implementada
        /// </summary>
        /// <returns></returns>
        private string GetLoggedUser()
        {
            var user =
                User?.Identity?.Name
                ?? Environment.UserName
                ?? Environment.GetEnvironmentVariable("USERNAME")
                ?? "Unknown";
            Logger.LogInformation("Usuário logado: {User}", user);
            return user;
        }
    }
}
