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
        IGenericRepository<ChecklistVeiculoObservacaoModel> itemObservacaoRepo
    ) : ServiceBase<ApplicationDbContext>(context, logger)
    {
        public async Task<CheckListCreatedDTO> CreateChecklist(
            NewCheckListDTO newCheckListDTO,
            string loggedUser
        )
        {
            var checklist = new ChecklistVeiculoModel
            {
                DescricaoVeiculo = newCheckListDTO.DescricaoVeiculo,
                PlacaVeiculo = newCheckListDTO.PlacaVeiculo,
                Executor = newCheckListDTO.Executor,
                Status = ChecklistStatusEnum.Pendente,
                UsuarioCriacao = loggedUser,
                UsuarioAtualizacao = loggedUser,
                Itens = new List<ChecklistVeiculoItemModel>()
            };
            foreach (var item in newCheckListDTO.Itens ?? [])
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

            return new()
            {
                Id = created.Id,
                DescricaoVeiculo = created.DescricaoVeiculo,
                PlacaVeiculo = created.PlacaVeiculo,
                Executor = created.Executor,
                Status = created.Status,
                Itens = created
                    .Itens!.Select(i => new CheckListCreatedDTO.CheckListItemDTO()
                    {
                        Descricao = i.Descricao,
                        Observacoes = i
                            .Observacoes?.Select(
                                o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO()
                                {
                                    Observacao = o.Observacao
                                }
                            )
                            .ToList()
                    })
                    .ToList()
            };
        }

        public async Task<bool?> DeleteChecklist(int id)
        {
            return await checklistRepo.Delete(id);
        }

        public async Task<CheckListCreatedDTO?> GetChecklist(int id)
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }
            return new CheckListCreatedDTO()
            {
                Id = checklist.Id,
                DescricaoVeiculo = checklist.DescricaoVeiculo,
                PlacaVeiculo = checklist.PlacaVeiculo,
                Executor = checklist.Executor,
                Status = checklist.Status,
                Itens = checklist
                    .Itens?.Select(i => new CheckListCreatedDTO.CheckListItemDTO()
                    {
                        Descricao = i.Descricao,
                        Observacoes = i
                            .Observacoes?.Select(
                                o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO()
                                {
                                    Observacao = o.Observacao
                                }
                            )
                            .ToList()
                    })
                    .ToList()
            };
        }

        public async Task<bool?> UpdateChecklist(
            int id,
            UpdateCheckListDTO updateCheckListDTO,
            string loggedUser = "unknown"
        )
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            ChecklistVeiculoModel newValues = new ChecklistVeiculoModel()
            {
                Id = id,
                DescricaoVeiculo = updateCheckListDTO.Descricao,
                Executor = checklist.Executor,
                PlacaVeiculo = updateCheckListDTO.Placa,
                UsuarioAtualizacao = loggedUser
            };

            var success = await checklistRepo.Update(checklist, newValues);
            return true;
        }

        public async Task<bool?> UpdateChecklistItem(
            int id,
            int itemId,
            UpdateCheckListItemDTO updateCheckListItemDTO,
            string userLogged = "unknown"
        )
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return null;
            }

            ChecklistVeiculoItemModel newValues = new ChecklistVeiculoItemModel()
            {
                Id = itemId,
                Descricao = updateCheckListItemDTO.Descricao,
                UsuarioAtualizacao = userLogged
            };

            var success = await checklistItemRepo.Update(item, newValues);
            return true;
        }

        public async Task<bool?> UpdateChecklistItemObservacao(
            int id,
            int itemId,
            int observacaoId,
            UpdateCheckListObservacaoDTO updateCheckListObservacaoDTO,
            string userLogged = "unknown"
        )
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return null;
            }

            var observacao = item.Observacoes?.FirstOrDefault(o => o.Id == observacaoId);
            if (observacao == null)
            {
                return null;
            }

            ChecklistVeiculoObservacaoModel newValues = new ChecklistVeiculoObservacaoModel()
            {
                Id = observacaoId,
                UsuarioAtualizacao = userLogged,
                Observacao = updateCheckListObservacaoDTO.Observacao
            };

            var success = await itemObservacaoRepo.Update(observacao, newValues);
            return true;
        }

        public async Task<bool?> DeleteChecklistItem(int id, int itemId)
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return null;
            }

            return await checklistItemRepo.Delete(itemId);
        }

        public async Task<bool?> DeleteChecklistItemObservacao(int id, int itemId, int observacaoId)
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return null;
            }

            var observacao = item.Observacoes?.FirstOrDefault(o => o.Id == observacaoId);
            if (observacao == null)
            {
                return null;
            }

            return await itemObservacaoRepo.Delete(observacaoId);
        }

        public async Task<IEnumerable<CheckListCreatedDTO>?> GetChecklists(
            ChecklistStatusEnum? status = null,
            int? takeLast = null
        )
        {
            // TODO: Implementar default para takeLast pela leitura do appsettings.json ou por variável de ambiente
            var checklists = await checklistRepo.GetByStatus(status, takeLast ?? 100);

            return checklists?.Select(c => new CheckListCreatedDTO()
            {
                Id = c.Id,
                DescricaoVeiculo = c.DescricaoVeiculo,
                PlacaVeiculo = c.PlacaVeiculo,
                Executor = c.Executor,
                Status = c.Status,
                Itens = c
                    .Itens?.Select(i => new CheckListCreatedDTO.CheckListItemDTO()
                    {
                        Descricao = i.Descricao,
                        Observacoes = i
                            .Observacoes?.Select(
                                o => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO()
                                {
                                    Observacao = o.Observacao
                                }
                            )
                            .ToList()
                    })
                    .ToList()
            });
        }

        public async Task<bool?> UpdateChecklistStatus(
            int id,
            ChecklistStatusEnum status,
            string loggedUser = "unknown"
        )
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            ChecklistVeiculoModel newValues = new ChecklistVeiculoModel()
            {
                Id = id,
                DescricaoVeiculo = checklist.DescricaoVeiculo,
                Executor = checklist.Executor,
                PlacaVeiculo = checklist.PlacaVeiculo,
                Status = status,
                UsuarioAtualizacao = loggedUser
            };

            await checklistRepo.Update(checklist, newValues);

            return true;
        }

        public async Task<bool?> AddChecklistItem(
            int id,
            NewCheckListItemDTO newItem,
            string loggedUser = "unknown"
        )
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            var item = new ChecklistVeiculoItemModel()
            {
                UsuarioAtualizacao = loggedUser,
                UsuarioCriacao = loggedUser,
                Descricao = newItem.Descricao,
                Observacoes = new List<ChecklistVeiculoObservacaoModel>()
            };

            checklist.Itens?.Add(item);
            await checklistRepo.Update(checklist, checklist);

            return true;
        }

        public async Task<CheckListItemCreatedDTO?> GetChecklistItem(int id, int itemId)
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return null;
            }

            return new CheckListItemCreatedDTO()
            {
                Id = item.Id,
                Descricao = item.Descricao,
                Status = item.Status,
                ChecklistVeiculoId = item.ChecklistVeiculoId,
                Observacoes = item
                    .Observacoes?.Select(o => new CheckListObservacaoCreatedDTO()
                    {
                        Id = o.Id,
                        Observacao = o.Observacao
                    })
                    .ToList(),
            };
        }

        public async Task<bool?> AddChecklistItemObservacao(
            int id,
            int itemId,
            NewCheckListItemObservacaoDTO newObservacao,
            string loggedUser = "unknown"
        )
        {
            var checklist = await checklistRepo.GetById(id);
            if (checklist == null)
            {
                return null;
            }

            var item = checklist.Itens?.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                return null;
            }

            var observacao = new ChecklistVeiculoObservacaoModel()
            {
                UsuarioAtualizacao = loggedUser,
                UsuarioCriacao = loggedUser,
                Observacao = newObservacao.Observacao
            };

            item.Observacoes?.Add(observacao);
            await checklistRepo.Update(checklist, checklist);

            return true;
        }

        public async Task<IList<CheckListCreatedDTO>?> GetChecklistsByPlaca(string placa)
        {
            var checklists = await checklistRepo.GetByPlaca(placa);
            if (checklists == null || checklists.Count == 0)
            {
                return null;
            }

            return checklists
                .Select(checklist => new CheckListCreatedDTO()
                {
                    Id = checklist.Id,
                    DescricaoVeiculo = checklist.DescricaoVeiculo,
                    PlacaVeiculo = checklist.PlacaVeiculo,
                    Executor = checklist.Executor,
                    Status = checklist.Status,
                    Itens = checklist
                        .Itens?.Select(item => new CheckListCreatedDTO.CheckListItemDTO()
                        {
                            Descricao = item.Descricao,
                            Observacoes = item
                                .Observacoes?.Select(
                                    observacao => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO()
                                    {
                                        Observacao = observacao.Observacao
                                    }
                                )
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }

        public async Task<IList<CheckListCreatedDTO>?> GetChecklistsByPlacaAndExecutor(
            string placa,
            string executor
        )
        {
            var checklists = await checklistRepo.GetByPlacaAndExecutor(placa, executor);
            if (checklists == null || checklists.Count == 0)
            {
                return null;
            }

            return checklists
                .Select(checklist => new CheckListCreatedDTO()
                {
                    Id = checklist.Id,
                    DescricaoVeiculo = checklist.DescricaoVeiculo,
                    PlacaVeiculo = checklist.PlacaVeiculo,
                    Executor = checklist.Executor,
                    Status = checklist.Status,
                    Itens = checklist
                        .Itens?.Select(item => new CheckListCreatedDTO.CheckListItemDTO()
                        {
                            Descricao = item.Descricao,
                            Observacoes = item
                                .Observacoes?.Select(
                                    observacao => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO()
                                    {
                                        Observacao = observacao.Observacao
                                    }
                                )
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }

        public async Task<IList<CheckListCreatedDTO>?> GetChecklistsByExecutor(string executor)
        {
            var checklists = await checklistRepo.GetByExecutor(executor);
            if (checklists == null || checklists.Count == 0)
            {
                return null;
            }

            return checklists
                .Select(checklist => new CheckListCreatedDTO()
                {
                    Id = checklist.Id,
                    DescricaoVeiculo = checklist.DescricaoVeiculo,
                    PlacaVeiculo = checklist.PlacaVeiculo,
                    Executor = checklist.Executor,
                    Status = checklist.Status,
                    Itens = checklist
                        .Itens?.Select(item => new CheckListCreatedDTO.CheckListItemDTO()
                        {
                            Descricao = item.Descricao,
                            Observacoes = item
                                .Observacoes?.Select(
                                    observacao => new CheckListCreatedDTO.CheckListItemDTO.CheckListObservacaoDTO()
                                    {
                                        Observacao = observacao.Observacao
                                    }
                                )
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();
        }

        internal async Task<bool?> ChangeChecklistStatus(int id, ChecklistStatusEnum status)
        {
            var newValues = new { Status = status };

            await checklistRepo.Update(id, newValues);

            return true;
        }
    }
}
