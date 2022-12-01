(function () {
    $(function () {

        var _$paiementLoyersTable = $('#PaiementLoyersTable');
        var _paiementLoyersService = abp.services.app.paiementLoyers;
		var _entityTypeFullName = 'UGB.Paysa.UGB.Paysa.Chambres.PaiementLoyer';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.PaiementLoyers.Create'),
            edit: abp.auth.hasPermission('Pages.PaiementLoyers.Edit'),
            'delete': abp.auth.hasPermission('Pages.PaiementLoyers.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/PaiementLoyers/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/PaiementLoyers/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditPaiementLoyerModal'
                });
                   

		 var _viewPaiementLoyerModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PaiementLoyers/ViewpaiementLoyerModal',
            modalClass: 'ViewPaiementLoyerModal'
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

        var dataTable = _$paiementLoyersTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _paiementLoyersService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#PaiementLoyersTableFilter').val(),
					moisFilter: $('#MoisFilterId').val(),
					minAnneeFilter: $('#MinAnneeFilterId').val(),
					maxAnneeFilter: $('#MaxAnneeFilterId').val(),
					codePaiementFilter: $('#CodePaiementFilterId').val(),
					chambreReferenceFilter: $('#ChambreReferenceFilterId').val(),
					operationCodeOperationFilter: $('#OperationCodeOperationFilterId').val()
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
                                    _viewPaiementLoyerModal.open({ id: data.record.paiementLoyer.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.paiementLoyer.id });                                
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
                                    entityId: data.record.paiementLoyer.id
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
                                deletePaiementLoyer(data.record.paiementLoyer);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "paiementLoyer.mois",
						 name: "mois"   
					},
					{
						targets: 3,
						 data: "paiementLoyer.annee",
						 name: "annee"   
					},
					{
						targets: 4,
						 data: "paiementLoyer.codePaiement",
						 name: "codePaiement"   
					},
					{
						targets: 5,
						 data: "chambreReference" ,
						 name: "chambreFk.reference" 
					},
					{
						targets: 6,
						 data: "operationCodeOperation" ,
						 name: "operationFk.codeOperation" 
					}
            ]
        });

        function getPaiementLoyers() {
            dataTable.ajax.reload();
        }

        function deletePaiementLoyer(paiementLoyer) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _paiementLoyersService.delete({
                            id: paiementLoyer.id
                        }).done(function () {
                            getPaiementLoyers(true);
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

        $('#CreateNewPaiementLoyerButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _paiementLoyersService
                .getPaiementLoyersToExcel({
				filter : $('#PaiementLoyersTableFilter').val(),
					moisFilter: $('#MoisFilterId').val(),
					minAnneeFilter: $('#MinAnneeFilterId').val(),
					maxAnneeFilter: $('#MaxAnneeFilterId').val(),
					codePaiementFilter: $('#CodePaiementFilterId').val(),
					chambreReferenceFilter: $('#ChambreReferenceFilterId').val(),
					operationCodeOperationFilter: $('#OperationCodeOperationFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditPaiementLoyerModalSaved', function () {
            getPaiementLoyers();
        });

		$('#GetPaiementLoyersButton').click(function (e) {
            e.preventDefault();
            getPaiementLoyers();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getPaiementLoyers();
		  }
		});
		
		
		
    });
})();
