using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using UGB.Paysa.Wallet.Operations.Exporting;
using UGB.Paysa.Wallet.Operations.Dtos;
using UGB.Paysa.Dto;
using Abp.Application.Services.Dto;
using UGB.Paysa.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using UGB.Paysa.Storage;

namespace UGB.Paysa.Wallet.Operations
{
    [AbpAuthorize(AppPermissions.Pages_TypeOperations)]
    public class TypeOperationsAppService : PaysaAppServiceBase, ITypeOperationsAppService
    {
        private readonly IRepository<TypeOperation, string> _typeOperationRepository;
        private readonly ITypeOperationsExcelExporter _typeOperationsExcelExporter;

        public TypeOperationsAppService(IRepository<TypeOperation, string> typeOperationRepository, ITypeOperationsExcelExporter typeOperationsExcelExporter)
        {
            _typeOperationRepository = typeOperationRepository;
            _typeOperationsExcelExporter = typeOperationsExcelExporter;

        }

        public async Task<PagedResultDto<GetTypeOperationForViewDto>> GetAll(GetAllTypeOperationsInput input)
        {

            var filteredTypeOperations = _typeOperationRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Reference.Contains(input.Filter) || e.Nom.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReferenceFilter), e => e.Reference == input.ReferenceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NomFilter), e => e.Nom == input.NomFilter)
                        .WhereIf(input.MinPrixFilter != null, e => e.Prix >= input.MinPrixFilter)
                        .WhereIf(input.MaxPrixFilter != null, e => e.Prix <= input.MaxPrixFilter);

            var pagedAndFilteredTypeOperations = filteredTypeOperations
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var typeOperations = from o in pagedAndFilteredTypeOperations
                                 select new
                                 {

                                     o.Reference,
                                     o.Nom,
                                     o.Prix,
                                     Id = o.Id
                                 };

            var totalCount = await filteredTypeOperations.CountAsync();

            var dbList = await typeOperations.ToListAsync();
            var results = new List<GetTypeOperationForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTypeOperationForViewDto()
                {
                    TypeOperation = new TypeOperationDto
                    {

                        Reference = o.Reference,
                        Nom = o.Nom,
                        Prix = o.Prix,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTypeOperationForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTypeOperationForViewDto> GetTypeOperationForView(string id)
        {
            var typeOperation = await _typeOperationRepository.GetAsync(id);

            var output = new GetTypeOperationForViewDto { TypeOperation = ObjectMapper.Map<TypeOperationDto>(typeOperation) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TypeOperations_Edit)]
        public async Task<GetTypeOperationForEditOutput> GetTypeOperationForEdit(EntityDto<string> input)
        {
            var typeOperation = await _typeOperationRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTypeOperationForEditOutput { TypeOperation = ObjectMapper.Map<CreateOrEditTypeOperationDto>(typeOperation) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTypeOperationDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TypeOperations_Create)]
        protected virtual async Task Create(CreateOrEditTypeOperationDto input)
        {
            var typeOperation = ObjectMapper.Map<TypeOperation>(input);

            if (AbpSession.TenantId != null)
            {
                typeOperation.TenantId = (int?)AbpSession.TenantId;
            }

            if (typeOperation.Id.IsNullOrWhiteSpace())
            {
                typeOperation.Id = Guid.NewGuid().ToString();
            }

            await _typeOperationRepository.InsertAsync(typeOperation);

        }

        [AbpAuthorize(AppPermissions.Pages_TypeOperations_Edit)]
        protected virtual async Task Update(CreateOrEditTypeOperationDto input)
        {
            var typeOperation = await _typeOperationRepository.FirstOrDefaultAsync((string)input.Id);
            ObjectMapper.Map(input, typeOperation);

        }

        [AbpAuthorize(AppPermissions.Pages_TypeOperations_Delete)]
        public async Task Delete(EntityDto<string> input)
        {
            await _typeOperationRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetTypeOperationsToExcel(GetAllTypeOperationsForExcelInput input)
        {

            var filteredTypeOperations = _typeOperationRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Reference.Contains(input.Filter) || e.Nom.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ReferenceFilter), e => e.Reference == input.ReferenceFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.NomFilter), e => e.Nom == input.NomFilter)
                        .WhereIf(input.MinPrixFilter != null, e => e.Prix >= input.MinPrixFilter)
                        .WhereIf(input.MaxPrixFilter != null, e => e.Prix <= input.MaxPrixFilter);

            var query = (from o in filteredTypeOperations
                         select new GetTypeOperationForViewDto()
                         {
                             TypeOperation = new TypeOperationDto
                             {
                                 Reference = o.Reference,
                                 Nom = o.Nom,
                                 Prix = o.Prix,
                                 Id = o.Id
                             }
                         });

            var typeOperationListDtos = await query.ToListAsync();

            return _typeOperationsExcelExporter.ExportToFile(typeOperationListDtos);
        }

    }
}