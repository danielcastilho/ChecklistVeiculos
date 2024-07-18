using ChecklistVeiculos.Controllers;
using ChecklistVeiculos.Dto.Request;
using ChecklistVeiculos.Dto.Response;
using ChecklistVeiculos.Models;
using ChecklistVeiculos.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChecklistVeiculos.Services
{
    public class ChecklistVeiculosService : ServiceBase<ApplicationDbContext>
    {
        private readonly IGenericRepository<ChecklistVeiculo> checklistRepo;
        private readonly IGenericRepository<ChecklistVeiculoItem> checklistItemRepo;
        private readonly IGenericRepository<ChecklistVeiculoObservacao> itemObservacaoRepo;

        public ChecklistVeiculosService(
            ApplicationDbContext context, 
            ILogger<ChecklistVeiculosService> logger,
            IGenericRepository<ChecklistVeiculo> checklistRepo, 
            IGenericRepository<ChecklistVeiculoItem> checklistItemRepo,
            IGenericRepository<ChecklistVeiculoObservacao> itemObservacaoRepo) : base(context, logger)
        {
            this.checklistRepo = checklistRepo;
            this.checklistItemRepo = checklistItemRepo;
            this.itemObservacaoRepo = itemObservacaoRepo;
        }
        public async Task<CheckListCreatedDTO> CreateChecklist(NewCheckListDTO newCheckListDTO) {
            var checklist = new ChecklistVeiculo();
            checklist.DescricaoVeiculo = newCheckListDTO.DescricaoVeiculo;
            checklist.PlacaVeiculo = newCheckListDTO.PlacaVeiculo;
            checklist.Executor = newCheckListDTO.Executor;
            checklist.Status = ChecklistStatusEnum.Pendente;
            checklist.Itens = new List<ChecklistVeiculoItem>();
            foreach (var item in newCheckListDTO.Itens?? new List<NewCheckListDTO.NewCheckListItemDTO>())
            {
                var checklistItem = new ChecklistVeiculoItem();
                checklistItem.Descricao = item.Descricao;
                checklistItem.Observacoes = new List<ChecklistVeiculoObservacao>();
                checklist.Itens.Add(checklistItem);
            }
            var created = await checklistRepo.Create(checklist);
            Logger.LogInformation($"Checklist criado: {created.Id}");
            CheckListCreatedDTO result = new CheckListCreatedDTO() {
                Id = created.Id,
                DescricaoVeiculo = created.DescricaoVeiculo,
                PlacaVeiculo = created.PlacaVeiculo,
                Executor = created.Executor,
                Status = created.Status,
                Itens = created.Itens!.Select(i => new CheckListCreatedDTO.CheckListItemDTO() {
                    Descricao = i.Descricao,
                    Observacoes = i.Observacoes?.Select(o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO() {
                        Observacao = o.Observacao
                    }).ToList()
                }).ToList()
            };
            
            return result;
        }

        internal async Task<CheckListCreatedDTO?> GetChecklist(int id)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }
            return new CheckListCreatedDTO() {
                Id = checklist.Id,
                DescricaoVeiculo = checklist.DescricaoVeiculo,
                PlacaVeiculo = checklist.PlacaVeiculo,
                Executor = checklist.Executor,
                Status = checklist.Status,
                Itens = checklist.Itens!.Select(i => new CheckListCreatedDTO.CheckListItemDTO() {
                    Descricao = i.Descricao,
                    Observacoes = i.Observacoes?.Select(o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO() {
                        Observacao = o.Observacao
                    }).ToList()
                }).ToList()
            };
        }

        internal async Task<bool?> UpdateChecklist(int id, UpdateCheckListDTO updateCheckListDTO)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }
            checklist.PlacaVeiculo = updateCheckListDTO.Placa;
            foreach (var item in checklist.Itens!)
            {
                item.Descricao = updateCheckListDTO.Descricao;
                item.Status = updateCheckListDTO.Concluido ? ChecklistStatusEnum.Aprovado : ChecklistStatusEnum.Reprovado;
            }
            await checklistRepo.Update(checklist);
            return true;
        }
    }
}