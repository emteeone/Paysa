(function () {
    $(function () {

        var _$terminauxTable = $('#TerminauxTable');
        var _terminauxService = abp.services.app.terminaux;
		var _entityTypeFullName = 'UGB.Paysa.Wallet.Tools.Terminal';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Terminaux.Create'),
            edit: abp.auth.hasPermission('Pages.Terminaux.Edit'),
            'delete': abp.auth.hasPermission('Pages.Terminaux.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Terminaux/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Terminaux/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditTerminalModal'
                });
                   

		 var _viewTerminalModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Terminaux/ViewterminalModal',
            modalClass: 'ViewTerminalModal'
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

        var dataTable = _$terminauxTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _terminauxService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#TerminauxTableFilter').val(),
					uid_DeviceFilter: $('#Uid_DeviceFilterId').val(),
					matriculeFilter: $('#MatriculeFilterId').val(),
					positionFilter: $('#PositionFilterId').val()
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
                                    _viewTerminalModal.open({ id: data.record.terminal.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.terminal.id });                                
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
                                    entityId: data.record.terminal.id
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
                                deleteTerminal(data.record.terminal);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "terminal.uid_Device",
						 name: "uid_Device"   
					},
					{
						targets: 3,
						 data: "terminal.matricule",
						 name: "matricule"   
					},
					{
						targets: 4,
						 data: "terminal.position",
						 name: "position"   
					}
            ]
        });

        function getTerminaux() {
            dataTable.ajax.reload();
        }

        function deleteTerminal(terminal) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _terminauxService.delete({
                            id: terminal.id
                        }).done(function () {
                            getTerminaux(true);
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

        $('#CreateNewTerminalButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _terminauxService
                .getTerminauxToExcel({
				filter : $('#TerminauxTableFilter').val(),
					uid_DeviceFilter: $('#Uid_DeviceFilterId').val(),
					matriculeFilter: $('#MatriculeFilterId').val(),
					positionFilter: $('#PositionFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditTerminalModalSaved', function () {
            getTerminaux();
        });

		$('#GetTerminauxButton').click(function (e) {
            e.preventDefault();
            getTerminaux();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getTerminaux();
		  }
		});
		
		
		
    });
})();
