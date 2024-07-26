using ChecklistVeiculos.Dto.Request;
using ChecklistVeiculos.Dto.Response;
using ChecklistVeiculos.Models;
using ChecklistVeiculos.Persistence.Repositories;

namespace ChecklistVeiculos.Services
{
    public class ChecklistVeiculosService(
        ApplicationDbContext context,
        ILogger<ChecklistVeiculosService> logger,
        ChecklistRepository checklistRepo,
        IGenericRepository<ChecklistVeiculoItemModel> checklistItemRepo,
        IGenericRepository<ChecklistVeiculoObservacaoModel> itemObservacaoRepo) : ServiceBase<ApplicationDbContext>(context, logger)
    {
        public async Task<CheckListCreatedDTO> CreateChecklist(NewCheckListDTO newCheckListDTO) {
            var checklist = new ChecklistVeiculoModel
            {
                DescricaoVeiculo = newCheckListDTO.DescricaoVeiculo,
                PlacaVeiculo = newCheckListDTO.PlacaVeiculo,
                Executor = newCheckListDTO.Executor,
                Status = ChecklistStatusEnum.Pendente,
                Itens = new List<ChecklistVeiculoItemModel>()
            };
            foreach (var item in newCheckListDTO.Itens?? [])
            {
                var checklistItem = new ChecklistVeiculoItemModel
                {
                    Descricao = item.Descricao,
                    Observacoes = new List<ChecklistVeiculoObservacaoModel>()
                };
                checklist.Itens.Add(checklistItem);
            }
            var created = await checklistRepo.Create(checklist);
            Logger.LogInformation("Checklist criado: {ChecklistId}", created.Id);

            return new() {
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
            
        }

        public async Task<bool?> DeleteChecklist(int id)
        {
            return await checklistRepo.Delete(id);
        }

        public async Task<CheckListCreatedDTO?> GetChecklist(int id)
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
                Itens = checklist.Itens?.Select(i => new CheckListCreatedDTO.CheckListItemDTO() {
                    Descricao = i.Descricao,
                    Observacoes = i.Observacoes?.Select(o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO() {
                        Observacao = o.Observacao
                    }).ToList()
                }).ToList()
            };
        }

        public async Task<bool?> UpdateChecklist(int id, UpdateCheckListDTO updateCheckListDTO)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            ChecklistVeiculoModel newValues = new ChecklistVeiculoModel()
            {
                Id = id,
                DescricaoVeiculo = updateCheckListDTO.Descricao,
                Executor = checklist.Executor,
                PlacaVeiculo = updateCheckListDTO.Placa

            };
            
            var success = await checklistRepo.Update(checklist, newValues);
            return true;
        }

        public async Task<bool?> UpdateChecklistItem(int id, int itemId, UpdateCheckListItemDTO updateCheckListItemDTO)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                return null;
            }

            ChecklistVeiculoItemModel newValues = new ChecklistVeiculoItemModel()
            {
                Id = itemId,
                Descricao = updateCheckListItemDTO.Descricao
            };
            
            var success = await checklistItemRepo.Update(item, newValues);
            return true;
        }

        public async Task<bool?> UpdateChecklistItemObservacao(int id, int itemId, int observacaoId, UpdateCheckListObservacaoDTO updateCheckListObservacaoDTO)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                return null;
            }

            var observacao = item.Observacoes?.FirstOrDefault(o => o.Id == observacaoId);
            if(observacao == null)
            {
                return null;
            }

            ChecklistVeiculoObservacaoModel newValues = new ChecklistVeiculoObservacaoModel()
            {
                Id = observacaoId,
                Observacao = updateCheckListObservacaoDTO.Observacao
            };
            
            var success = await itemObservacaoRepo.Update(observacao, newValues);
            return true;
        }

        public async Task<bool?> DeleteChecklistItem(int id, int itemId)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                return null;
            }

            return await checklistItemRepo.Delete(itemId);
        }

        public async Task<bool?> DeleteChecklistItemObservacao(int id, int itemId, int observacaoId)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                return null;
            }

            var observacao = item.Observacoes?.FirstOrDefault(o => o.Id == observacaoId);
            if(observacao == null)
            {
                return null;
            }

            return await itemObservacaoRepo.Delete(observacaoId);
        }

        public async Task<IEnumerable<CheckListCreatedDTO>?> GetChecklists()
        {
            var checklists = await checklistRepo.GetAll();

            return checklists?.Select(c => new CheckListCreatedDTO() {
                Id = c.Id,
                DescricaoVeiculo = c.DescricaoVeiculo,
                PlacaVeiculo = c.PlacaVeiculo,
                Executor = c.Executor,
                Status = c.Status,
                Itens = c.Itens?.Select(i => new CheckListCreatedDTO.CheckListItemDTO() {
                    Descricao = i.Descricao,
                    Observacoes = i.Observacoes?.Select(o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO() {
                        Observacao = o.Observacao
                    }).ToList()
                }).ToList()
            });
        }

        public async Task<bool?> UpdateChecklistStatus(int id, ChecklistStatusEnum status)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            ChecklistVeiculoModel newValues = new ChecklistVeiculoModel()
            {
                Id = id,
                DescricaoVeiculo = checklist.DescricaoVeiculo,
                Executor = checklist.Executor,
                PlacaVeiculo = checklist.PlacaVeiculo,
                Status = status
            };
            
            await checklistRepo.Update(checklist, newValues);

            return true;
        }

        public async Task<bool?> AddChecklistItem(int id, NewCheckListItemDTO newItem)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            var item = new ChecklistVeiculoItemModel()
            {
                Descricao = newItem.Descricao,
                Observacoes = new List<ChecklistVeiculoObservacaoModel>()
            };

            checklist.Itens?.Add(item);
            await checklistRepo.Update(checklist, checklist);

            return true;
        }

        public async Task<bool?> AddChecklistItemObservacao(int id, int itemId, NewCheckListItemObservacaoDTO newObservacao)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                return null;
            }

            var observacao = new ChecklistVeiculoObservacaoModel()
            {
                Observacao = newObservacao.Observacao
            };

            item.Observacoes?.Add(observacao);
            await checklistRepo.Update(checklist, checklist);

            return true;
        }

        public async Task<bool?> UpdateCheckListItemObservacao(int id, int itemId, int observacaoId, UpdateCheckListObservacaoDTO updateCheckListObservacaoDTO)
        {
            var checklist = await checklistRepo.GetById(id);
            if(checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if(item == null)
            {
                return null;
            }

            var observacao = item.Observacoes?.FirstOrDefault(o => o.Id == observacaoId);
            if(observacao == null)
            {
                return null;
            }

            ChecklistVeiculoObservacaoModel newValues = new ChecklistVeiculoObservacaoModel()
            {
                Id = observacaoId,
                Observacao = updateCheckListObservacaoDTO.Observacao
            };
            
            var success = await itemObservacaoRepo.Update(observacao, newValues);
            return true;
        }

        public async Task<CheckListCreatedDTO?> GetChecklistByPlaca(string placa)
        {
            var checklist = await checklistRepo.GetByPlaca(placa);
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
                Itens = checklist.Itens?.Select(i => new CheckListCreatedDTO.CheckListItemDTO() {
                    Descricao = i.Descricao,
                    Observacoes = i.Observacoes?.Select(o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO() {
                        Observacao = o.Observacao
                    }).ToList()
                }).ToList()
            };
        }        
    }
    
}