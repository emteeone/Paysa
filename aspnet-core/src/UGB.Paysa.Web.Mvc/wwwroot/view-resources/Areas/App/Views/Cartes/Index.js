(function () {
    $(function () {

        var _$cartesTable = $('#CartesTable');
        var _cartesService = abp.services.app.cartes;
		var _entityTypeFullName = 'UGB.Paysa.Wallet.Tools.Carte';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Cartes.Create'),
            edit: abp.auth.hasPermission('Pages.Cartes.Edit'),
            'delete': abp.auth.hasPermission('Pages.Cartes.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Cartes/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Cartes/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditCarteModal'
                });
                   

		 var _viewCarteModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Cartes/ViewcarteModal',
            modalClass: 'ViewCarteModal'
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

        var dataTable = _$cartesTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _cartesService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#CartesTableFilter').val(),
					uIDFilter: $('#UIDFilterId').val(),
					numeroCarteFilter: $('#NumeroCarteFilterId').val(),
					isActiveFilter: $('#IsActiveFilterId').val(),
					minDateDelivranceFilter:  getDateFilter($('#MinDateDelivranceFilterId')),
					maxDateDelivranceFilter:  getMaxDateFilter($('#MaxDateDelivranceFilterId')),
					minDateExpirationFilter:  getDateFilter($('#MinDateExpirationFilterId')),
					maxDateExpirationFilter:  getMaxDateFilter($('#MaxDateExpirationFilterId')),
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
                                    _viewCarteModal.open({ id: data.record.carte.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.carte.id });                                
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
                                    entityId: data.record.carte.id
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
                                deleteCarte(data.record.carte);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "carte.uid",
						 name: "uid"   
					},
					{
						targets: 3,
						 data: "carte.numeroCarte",
						 name: "numeroCarte"   
					},
					{
						targets: 4,
						 data: "carte.isActive",
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
						 data: "carte.dateDelivrance",
						 name: "dateDelivrance" ,
					render: function (dateDelivrance) {
						if (dateDelivrance) {
							return moment(dateDelivrance).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 6,
						 data: "carte.dateExpiration",
						 name: "dateExpiration" ,
					render: function (dateExpiration) {
						if (dateExpiration) {
							return moment(dateExpiration).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 7,
						 data: "compteNumeroCompte" ,
						 name: "compteFk.numeroCompte" 
					}
            ]
        });

        function getCartes() {
            dataTable.ajax.reload();
        }

        function deleteCarte(carte) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _cartesService.delete({
                            id: carte.id
                        }).done(function () {
                            getCartes(true);
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

        $('#CreateNewCarteButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _cartesService
                .getCartesToExcel({
				filter : $('#CartesTableFilter').val(),
					uIDFilter: $('#UIDFilterId').val(),
					numeroCarteFilter: $('#NumeroCarteFilterId').val(),
					isActiveFilter: $('#IsActiveFilterId').val(),
					minDateDelivranceFilter:  getDateFilter($('#MinDateDelivranceFilterId')),
					maxDateDelivranceFilter:  getMaxDateFilter($('#MaxDateDelivranceFilterId')),
					minDateExpirationFilter:  getDateFilter($('#MinDateExpirationFilterId')),
					maxDateExpirationFilter:  getMaxDateFilter($('#MaxDateExpirationFilterId')),
					compteNumeroCompteFilter: $('#CompteNumeroCompteFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditCarteModalSaved', function () {
            getCartes();
        });

		$('#GetCartesButton').click(function (e) {
            e.preventDefault();
            getCartes();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getCartes();
		  }
		});
		
		
		
    });
})();
