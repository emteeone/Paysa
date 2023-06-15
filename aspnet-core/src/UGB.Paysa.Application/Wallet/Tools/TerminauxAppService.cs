using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using UGB.Paysa.Wallet.Tools.Exporting;
using UGB.Paysa.Wallet.Tools.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;
using Abp.Domain.Repositories;

namespace UGB.Paysa.Wallet.Tools
{
    [AbpAuthorize(AppPermissions.Pages_Terminaux)]
    public class TerminauxAppService : PaysaAppServiceBase, ITerminauxAppService
    {
        private readonly IRepository<Terminal, string> _terminalRepository;
        private readonly ITerminauxExcelExporter _terminauxExcelExporter;

        public TerminauxAppService(IRepository<Terminal, string> terminalRepository, ITerminauxExcelExporter terminauxExcelExporter)
        {
            _terminalRepository = terminalRepository;
            _terminauxExcelExporter = terminauxExcelExporter;
        }

        public async Task<PagedResultDto<GetTerminalForViewDto>> GetAll(GetAllTerminauxInput input)
        {

            var filteredTerminaux = _terminalRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Uid_Device.Contains(input.Filter) || e.Matricule.Contains(input.Filter) || e.Position.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Uid_DeviceFilter), e => e.Uid_Device == input.Uid_DeviceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MatriculeFilter), e => e.Matricule == input.MatriculeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter), e => e.Position == input.PositionFilter);

            var pagedAndFilteredTerminaux = filteredTerminaux
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var terminaux = from o in pagedAndFilteredTerminaux
                            select new
                            {

                                o.Uid_Device,
                                o.Matricule,
                                o.Position,
                                Id = o.Id
                            };

            var totalCount = await filteredTerminaux.CountAsync();

            var dbList = await terminaux.ToListAsync();
            var results = new List<GetTerminalForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTerminalForViewDto()
                {
                    Terminal = new TerminalDto
                    {

                        Uid_Device = o.Uid_Device,
                        Matricule = o.Matricule,
                        Position = o.Position,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTerminalForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTerminalForViewDto> GetTerminalForView(string id)
        {
            var terminal = await _terminalRepository.GetAsync(id);

            var output = new GetTerminalForViewDto { Terminal = ObjectMapper.Map<TerminalDto>(terminal) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Terminaux_Edit)]
        public async Task<GetTerminalForEditOutput> GetTerminalForEdit(EntityDto<string> input)
        {
            var terminal = await _terminalRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTerminalForEditOutput { Terminal = ObjectMapper.Map<CreateOrEditTerminalDto>(terminal) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTerminalDto input)
        {
            if (input.Id.IsNullOrWhiteSpace())
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Terminaux_Create)]
        protected virtual async Task Create(CreateOrEditTerminalDto input)
        {
            var terminal = ObjectMapper.Map<Terminal>(input);

            if (AbpSession.TenantId != null)
            {
                terminal.TenantId = (int?)AbpSession.TenantId;
            }

            if (terminal.Id.IsNullOrWhiteSpace())
            {
                terminal.Id = Guid.NewGuid().ToString();
            }

            await _terminalRepository.InsertAsync(terminal);

        }

        [AbpAuthorize(AppPermissions.Pages_Terminaux_Edit)]
        protected virtual async Task Update(CreateOrEditTerminalDto input)
        {
            var terminal = await _terminalRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, terminal);

        }

        [AbpAuthorize(AppPermissions.Pages_Terminaux_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _terminalRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetTerminauxToExcel(GetAllTerminauxForExcelInput input)
        {

            var filteredTerminaux = _terminalRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Uid_Device.Contains(input.Filter) || e.Matricule.Contains(input.Filter) || e.Position.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Uid_DeviceFilter), e => e.Uid_Device == input.Uid_DeviceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MatriculeFilter), e => e.Matricule == input.MatriculeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PositionFilter), e => e.Position == input.PositionFilter);

            var query = (from o in filteredTerminaux
                         select new GetTerminalForViewDto()
                         {
                             Terminal = new TerminalDto
                             {
                                 Uid_Device = o.Uid_Device,
                                 Matricule = o.Matricule,
                                 Position = o.Position,
                                 Id = o.Id
                             }
                         });

            var terminalListDtos = await query.ToListAsync();

            return _terminauxExcelExporter.ExportToFile(terminalListDtos);
        }


    }
}