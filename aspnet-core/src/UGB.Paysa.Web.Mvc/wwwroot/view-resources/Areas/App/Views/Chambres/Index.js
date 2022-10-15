(function () {
    $(function () {

        var _$chambresTable = $('#ChambresTable');
        var _chambresService = abp.services.app.chambres;
		var _entityTypeFullName = 'UGB.Paysa.Wallet.Chambres.Chambre';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Chambres.Create'),
            edit: abp.auth.hasPermission('Pages.Chambres.Edit'),
            'delete': abp.auth.hasPermission('Pages.Chambres.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Chambres/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Chambres/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditChambreModal'
                });
                   

		 var _viewChambreModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Chambres/ViewchambreModal',
            modalClass: 'ViewChambreModal'
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

        var dataTable = _$chambresTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _chambresService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#ChambresTableFilter').val(),
					referenceFilter: $('#ReferenceFilterId').val(),
					minNumeroChambreFilter: $('#MinNumeroChambreFilterId').val(),
					maxNumeroChambreFilter: $('#MaxNumeroChambreFilterId').val(),
					blocFilter: $('#BlocFilterId').val(),
					villageFilter: $('#VillageFilterId').val(),
					campusFilter: $('#CampusFilterId').val(),
					minMontantLocationFilter: $('#MinMontantLocationFilterId').val(),
					maxMontantLocationFilter: $('#MaxMontantLocationFilterId').val()
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
                                    _viewChambreModal.open({ id: data.record.chambre.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.chambre.id });                                
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
                                    entityId: data.record.chambre.id
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
                                deleteChambre(data.record.chambre);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "chambre.reference",
						 name: "reference"   
					},
					{
						targets: 3,
						 data: "chambre.numeroChambre",
						 name: "numeroChambre"   
					},
					{
						targets: 4,
						 data: "chambre.bloc",
						 name: "bloc"   ,
						render: function (bloc) {
							return app.localize('Enum_Bloc_' + bloc);
						}
			
					},
					{
						targets: 5,
						 data: "chambre.village",
						 name: "village"   ,
						render: function (village) {
							return app.localize('Enum_Village_' + village);
						}
			
					},
					{
						targets: 6,
						 data: "chambre.campus",
						 name: "campus"   ,
						render: function (campus) {
							return app.localize('Enum_Campus_' + campus);
						}
			
					},
					{
						targets: 7,
						 data: "chambre.montantLocation",
						 name: "montantLocation"   
					}
            ]
        });

        function getChambres() {
            dataTable.ajax.reload();
        }

        function deleteChambre(chambre) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _chambresService.delete({
                            id: chambre.id
                        }).done(function () {
                            getChambres(true);
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

        $('#CreateNewChambreButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _chambresService
                .getChambresToExcel({
				filter : $('#ChambresTableFilter').val(),
					referenceFilter: $('#ReferenceFilterId').val(),
					minNumeroChambreFilter: $('#MinNumeroChambreFilterId').val(),
					maxNumeroChambreFilter: $('#MaxNumeroChambreFilterId').val(),
					blocFilter: $('#BlocFilterId').val(),
					villageFilter: $('#VillageFilterId').val(),
					campusFilter: $('#CampusFilterId').val(),
					minMontantLocationFilter: $('#MinMontantLocationFilterId').val(),
					maxMontantLocationFilter: $('#MaxMontantLocationFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditChambreModalSaved', function () {
            getChambres();
        });

		$('#GetChambresButton').click(function (e) {
            e.preventDefault();
            getChambres();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getChambres();
		  }
		});
		
		
		
    });
})();
