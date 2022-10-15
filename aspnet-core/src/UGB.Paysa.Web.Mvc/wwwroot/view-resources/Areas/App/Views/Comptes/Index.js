(function () {
    $(function () {

        var _$comptesTable = $('#ComptesTable');
        var _comptesService = abp.services.app.comptes;
		var _entityTypeFullName = 'UGB.Paysa.Wallet.Comptes.Compte';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Comptes.Create'),
            edit: abp.auth.hasPermission('Pages.Comptes.Edit'),
            'delete': abp.auth.hasPermission('Pages.Comptes.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Comptes/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Comptes/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditCompteModal'
                });
                   

		 var _viewCompteModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Comptes/ViewcompteModal',
            modalClass: 'ViewCompteModal'
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

        var dataTable = _$comptesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _comptesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ComptesTableFilter').val(),
					numeroCompteFilter: $('#NumeroCompteFilterId').val(),
					minSoldeFilter: $('#MinSoldeFilterId').val(),
					maxSoldeFilter: $('#MaxSoldeFilterId').val(),
					isActiveFilter: $('#IsActiveFilterId').val(),
					minDateCreationFilter:  getDateFilter($('#MinDateCreationFilterId')),
					maxDateCreationFilter:  getMaxDateFilter($('#MaxDateCreationFilterId')),
					etudiantCodeEtudiantFilter: $('#EtudiantCodeEtudiantFilterId').val()
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
                                    _viewCompteModal.open({ id: data.record.compte.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.compte.id });                                
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
                                    entityId: data.record.compte.id
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
                                deleteCompte(data.record.compte);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "compte.numeroCompte",
						 name: "numeroCompte"   
					},
					{
						targets: 3,
						 data: "compte.solde",
						 name: "solde"   
					},
					{
						targets: 4,
						 data: "compte.isActive",
						 name: "isActive"  ,
						render: function (isActive) {
							if (isActive) {
								return '<div class="text-center"><i class="fa fa-check text-success" title="True"></i></div>';
							}
							return '<div class="text-center"><i class="fa fa-times-circle" title="False"></i></div>';
					}
			 
					},
					{
						targets: 5,
						 data: "compte.dateCreation",
						 name: "dateCreation" ,
					render: function (dateCreation) {
						if (dateCreation) {
							return moment(dateCreation).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 6,
						 data: "etudiantCodeEtudiant" ,
						 name: "etudiantFk.codeEtudiant" 
					}
            ]
        });

        function getComptes() {
            dataTable.ajax.reload();
        }

        function deleteCompte(compte) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _comptesService.delete({
                            id: compte.id
                        }).done(function () {
                            getComptes(true);
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

        $('#CreateNewCompteButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _comptesService
                .getComptesToExcel({
				filter : $('#ComptesTableFilter').val(),
					numeroCompteFilter: $('#NumeroCompteFilterId').val(),
					minSoldeFilter: $('#MinSoldeFilterId').val(),
					maxSoldeFilter: $('#MaxSoldeFilterId').val(),
					isActiveFilter: $('#IsActiveFilterId').val(),
					minDateCreationFilter:  getDateFilter($('#MinDateCreationFilterId')),
					maxDateCreationFilter:  getMaxDateFilter($('#MaxDateCreationFilterId')),
					etudiantCodeEtudiantFilter: $('#EtudiantCodeEtudiantFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditCompteModalSaved', function () {
            getComptes();
        });

		$('#GetComptesButton').click(function (e) {
            e.preventDefault();
            getComptes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getComptes();
		  }
		});
		
		
		
    });
})();
