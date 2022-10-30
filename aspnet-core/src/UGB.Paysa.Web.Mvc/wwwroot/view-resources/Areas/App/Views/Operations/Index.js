(function () {
    $(function () {

        var _$operationsTable = $('#OperationsTable');
        var _operationsService = abp.services.app.operations;
		
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Operations.Create'),
            edit: abp.auth.hasPermission('Pages.Operations.Edit'),
            'delete': abp.auth.hasPermission('Pages.Operations.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Operations/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Operations/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditOperationModal'
                });
                   

		 var _viewOperationModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Operations/ViewoperationModal',
            modalClass: 'ViewOperationModal'
        });

		
		

        var getDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT00:00:00Z"); 
        }
        
        var getMaxDateFilter = function (element) {
            if (element.data("DateTimePicker").date() == null) {
                return null;
            }
            return element.data("DateTimePicker").date().format("YYYY-MM-DDT23:59:59Z"); 
        }

        var dataTable = _$operationsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _operationsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#OperationsTableFilter').val(),
					codeOperationFilter: $('#CodeOperationFilterId').val(),
					minDateOperationFilter:  getDateFilter($('#MinDateOperationFilterId')),
					maxDateOperationFilter:  getMaxDateFilter($('#MaxDateOperationFilterId')),
					minMontantFilter: $('#MinMontantFilterId').val(),
					maxMontantFilter: $('#MaxMontantFilterId').val(),
					discriminatorFilter: $('#DiscriminatorFilterId').val(),
					minCreationTimeFilter:  getDateFilter($('#MinCreationTimeFilterId')),
					maxCreationTimeFilter:  getMaxDateFilter($('#MaxCreationTimeFilterId')),
					minLastModificationTimeFilter:  getDateFilter($('#MinLastModificationTimeFilterId')),
					maxLastModificationTimeFilter:  getMaxDateFilter($('#MaxLastModificationTimeFilterId')),
					isDeletedFilter: $('#IsDeletedFilterId').val(),
					minDeletionTimeFilter:  getDateFilter($('#MinDeletionTimeFilterId')),
					maxDeletionTimeFilter:  getMaxDateFilter($('#MaxDeletionTimeFilterId')),
					compteNumeroCompteFilter: $('#CompteNumeroCompteFilterId').val()
                    };
                }
            },
            columnDefs: [
                {
                    className: 'control responsive',
                    orderable: false,
                    render: function () {
                        return '';
                    },
                    targets: 0
                },
                {
                    width: 120,
                    targets: 1,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: '',
                    rowAction: {
                        cssClass: 'btn btn-brand dropdown-toggle',
                        text: '<i class="fa fa-cog"></i> ' + app.localize('Actions') + ' <span class="caret"></span>',
                        items: [
						{
                                text: app.localize('View'),
                                iconStyle: 'far fa-eye mr-2',
                                action: function (data) {
                                    _viewOperationModal.open({ id: data.record.operation.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.operation.id });                                
                            }
                        }, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteOperation(data.record.operation);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "operation.codeOperation",
						 name: "codeOperation"   
					},
					{
						targets: 3,
						 data: "operation.dateOperation",
						 name: "dateOperation" ,
					render: function (dateOperation) {
						if (dateOperation) {
							return moment(dateOperation).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 4,
						 data: "operation.montant",
						 name: "montant"   
					},
					{
						targets: 5,
						 data: "operation.discriminator",
						 name: "discriminator"   
					},
					{
						targets: 6,
						 data: "operation.creationTime",
						 name: "creationTime" ,
					render: function (creationTime) {
						if (creationTime) {
							return moment(creationTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 7,
						 data: "operation.lastModificationTime",
						 name: "lastModificationTime" ,
					render: function (lastModificationTime) {
						if (lastModificationTime) {
							return moment(lastModificationTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 8,
						 data: "operation.isDeleted",
						 name: "isDeleted"  ,
						render: function (isDeleted) {
							if (isDeleted) {
								return '<div class="text-center"><i class="fa fa-check text-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 9,
						 data: "operation.deletionTime",
						 name: "deletionTime" ,
					render: function (deletionTime) {
						if (deletionTime) {
							return moment(deletionTime).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 10,
						 data: "compteNumeroCompte" ,
						 name: "compteFk.numeroCompte" 
					}
            ]
        });

        function getOperations() {
            dataTable.ajax.reload();
        }

        function deleteOperation(operation) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _operationsService.delete({
                            id: operation.id
                        }).done(function () {
                            getOperations(true);
                            abp.notify.success(app.localize('SuccessfullyDeleted'));
                        });
                    }
                }
            );
        }

		$('#ShowAdvancedFiltersSpan').click(function () {
            $('#ShowAdvancedFiltersSpan').hide();
            $('#HideAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideDown();
        });

        $('#HideAdvancedFiltersSpan').click(function () {
            $('#HideAdvancedFiltersSpan').hide();
            $('#ShowAdvancedFiltersSpan').show();
            $('#AdvacedAuditFiltersArea').slideUp();
        });

        $('#CreateNewOperationButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _operationsService
                .getOperationsToExcel({
				filter : $('#OperationsTableFilter').val(),
					codeOperationFilter: $('#CodeOperationFilterId').val(),
					minDateOperationFilter:  getDateFilter($('#MinDateOperationFilterId')),
					maxDateOperationFilter:  getMaxDateFilter($('#MaxDateOperationFilterId')),
					minMontantFilter: $('#MinMontantFilterId').val(),
					maxMontantFilter: $('#MaxMontantFilterId').val(),
					discriminatorFilter: $('#DiscriminatorFilterId').val(),
					minCreationTimeFilter:  getDateFilter($('#MinCreationTimeFilterId')),
					maxCreationTimeFilter:  getMaxDateFilter($('#MaxCreationTimeFilterId')),
					minLastModificationTimeFilter:  getDateFilter($('#MinLastModificationTimeFilterId')),
					maxLastModificationTimeFilter:  getMaxDateFilter($('#MaxLastModificationTimeFilterId')),
					isDeletedFilter: $('#IsDeletedFilterId').val(),
					minDeletionTimeFilter:  getDateFilter($('#MinDeletionTimeFilterId')),
					maxDeletionTimeFilter:  getMaxDateFilter($('#MaxDeletionTimeFilterId')),
					compteNumeroCompteFilter: $('#CompteNumeroCompteFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditOperationModalSaved', function () {
            getOperations();
        });

		$('#GetOperationsButton').click(function (e) {
            e.preventDefault();
            getOperations();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getOperations();
		  }
		});
		
		
		
    });
})();
