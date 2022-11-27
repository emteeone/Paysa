(function () {
    $(function () {

        var _$typeOperationsTable = $('#TypeOperationsTable');
        var _typeOperationsService = abp.services.app.typeOperations;
		var _entityTypeFullName = 'UGB.Paysa.Wallet.Operations.TypeOperation';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.TypeOperations.Create'),
            edit: abp.auth.hasPermission('Pages.TypeOperations.Edit'),
            'delete': abp.auth.hasPermission('Pages.TypeOperations.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/TypeOperations/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/TypeOperations/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditTypeOperationModal'
                });
                   

		 var _viewTypeOperationModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/TypeOperations/ViewtypeOperationModal',
            modalClass: 'ViewTypeOperationModal'
        });

		        var _entityTypeHistoryModal = app.modals.EntityTypeHistoryModal.create();
		        function entityHistoryIsEnabled() {
            return abp.auth.hasPermission('Pages.Administration.AuditLogs') &&
                abp.custom.EntityHistory &&
                abp.custom.EntityHistory.IsEnabled &&
                _.filter(abp.custom.EntityHistory.EnabledEntities, entityType => entityType === _entityTypeFullName).length === 1;
        }

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

        var dataTable = _$typeOperationsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _typeOperationsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#TypeOperationsTableFilter').val(),
					referenceFilter: $('#ReferenceFilterId').val(),
					nomFilter: $('#NomFilterId').val(),
					minPrixFilter: $('#MinPrixFilterId').val(),
					maxPrixFilter: $('#MaxPrixFilterId').val()
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
                                    _viewTypeOperationModal.open({ id: data.record.typeOperation.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.typeOperation.id });                                
                            }
                        },
                        {
                            text: app.localize('History'),
                            iconStyle: 'fas fa-history mr-2',
                            visible: function () {
                                return entityHistoryIsEnabled();
                            },
                            action: function (data) {
                                _entityTypeHistoryModal.open({
                                    entityTypeFullName: _entityTypeFullName,
                                    entityId: data.record.typeOperation.id
                                });
                            }
						}, 
						{
                            text: app.localize('Delete'),
                            iconStyle: 'far fa-trash-alt mr-2',
                            visible: function () {
                                return _permissions.delete;
                            },
                            action: function (data) {
                                deleteTypeOperation(data.record.typeOperation);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "typeOperation.reference",
						 name: "reference"   
					},
					{
						targets: 3,
						 data: "typeOperation.nom",
						 name: "nom"   
					},
					{
						targets: 4,
						 data: "typeOperation.prix",
						 name: "prix"   
					}
            ]
        });

        function getTypeOperations() {
            dataTable.ajax.reload();
        }

        function deleteTypeOperation(typeOperation) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _typeOperationsService.delete({
                            id: typeOperation.id
                        }).done(function () {
                            getTypeOperations(true);
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

        $('#CreateNewTypeOperationButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _typeOperationsService
                .getTypeOperationsToExcel({
				filter : $('#TypeOperationsTableFilter').val(),
					referenceFilter: $('#ReferenceFilterId').val(),
					nomFilter: $('#NomFilterId').val(),
					minPrixFilter: $('#MinPrixFilterId').val(),
					maxPrixFilter: $('#MaxPrixFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditTypeOperationModalSaved', function () {
            getTypeOperations();
        });

		$('#GetTypeOperationsButton').click(function (e) {
            e.preventDefault();
            getTypeOperations();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getTypeOperations();
		  }
		});
		
		
		
    });
})();
