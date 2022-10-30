(function () {
    $(function () {

        var _$etudiantsTable = $('#EtudiantsTable');
        var _etudiantsService = abp.services.app.etudiants;
		var _entityTypeFullName = 'UGB.Paysa.Wallet.Etudiants.Etudiant';
        
        $('.date-picker').datetimepicker({
            locale: abp.localization.currentLanguage.name,
            format: 'L'
        });

        var _permissions = {
            create: abp.auth.hasPermission('Pages.Etudiants.Create'),
            edit: abp.auth.hasPermission('Pages.Etudiants.Edit'),
            'delete': abp.auth.hasPermission('Pages.Etudiants.Delete')
        };

         var _createOrEditModal = new app.ModalManager({
                    viewUrl: abp.appPath + 'App/Etudiants/CreateOrEditModal',
                    scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Etudiants/_CreateOrEditModal.js',
                    modalClass: 'CreateOrEditEtudiantModal'
                });
                   

		 var _viewEtudiantModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Etudiants/ViewetudiantModal',
            modalClass: 'ViewEtudiantModal'
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

        var dataTable = _$etudiantsTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _etudiantsService.getAll,
                inputFilter: function () {
                    return {
					filter: $('#EtudiantsTableFilter').val(),
					iNEFilter: $('#INEFilterId').val(),
					codeEtudiantFilter: $('#CodeEtudiantFilterId').val(),
					prenomFilter: $('#PrenomFilterId').val(),
					nomFilter: $('#NomFilterId').val(),
					sexeFilter: $('#SexeFilterId').val(),
					minDateDeNaissanceFilter:  getDateFilter($('#MinDateDeNaissanceFilterId')),
					maxDateDeNaissanceFilter:  getMaxDateFilter($('#MaxDateDeNaissanceFilterId')),
					lieuDeNaissanceFilter: $('#LieuDeNaissanceFilterId').val(),
					situationMatrimonialeFilter: $('#SituationMatrimonialeFilterId').val(),
					cINFilter: $('#CINFilterId').val(),
					adresseFilter: $('#AdresseFilterId').val(),
					villeFilter: $('#VilleFilterId').val(),
					regionFilter: $('#RegionFilterId').val(),
					paysFilter: $('#PaysFilterId').val(),
					emailFilter: $('#EmailFilterId').val(),
					telephoneFilter: $('#TelephoneFilterId').val(),
					chambreReferenceFilter: $('#ChambreReferenceFilterId').val(),
					userNameFilter: $('#UserNameFilterId').val()
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
                                    _viewEtudiantModal.open({ id: data.record.etudiant.id });
                                }
                        },
						{
                            text: app.localize('Edit'),
                            iconStyle: 'far fa-edit mr-2',
                            visible: function () {
                                return _permissions.edit;
                            },
                            action: function (data) {
                            _createOrEditModal.open({ id: data.record.etudiant.id });                                
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
                                    entityId: data.record.etudiant.id
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
                                deleteEtudiant(data.record.etudiant);
                            }
                        }]
                    }
                },
					{
						targets: 2,
						 data: "etudiant.ine",
						 name: "ine"   
					},
					{
						targets: 3,
						 data: "etudiant.codeEtudiant",
						 name: "codeEtudiant"   
					},
					{
						targets: 4,
						 data: "etudiant.prenom",
						 name: "prenom"   
					},
					{
						targets: 5,
						 data: "etudiant.nom",
						 name: "nom"   
					},
					{
						targets: 6,
						 data: "etudiant.sexe",
						 name: "sexe"   ,
						render: function (sexe) {
							return app.localize('Enum_Sexe_' + sexe);
						}
			
					},
					{
						targets: 7,
						 data: "etudiant.dateDeNaissance",
						 name: "dateDeNaissance" ,
					render: function (dateDeNaissance) {
						if (dateDeNaissance) {
							return moment(dateDeNaissance).format('L');
						}
						return "";
					}
			  
					},
					{
						targets: 8,
						 data: "etudiant.lieuDeNaissance",
						 name: "lieuDeNaissance"   
					},
					{
						targets: 9,
						 data: "etudiant.situationMatrimoniale",
						 name: "situationMatrimoniale"   ,
						render: function (situationMatrimoniale) {
							return app.localize('Enum_SituationMatrimoniale_' + situationMatrimoniale);
						}
			
					},
					{
						targets: 10,
						 data: "etudiant.cin",
						 name: "cin"   
					},
					{
						targets: 11,
						 data: "etudiant.adresse",
						 name: "adresse"   
					},
					{
						targets: 12,
						 data: "etudiant.ville",
						 name: "ville"   
					},
					{
						targets: 13,
						 data: "etudiant.region",
						 name: "region"   
					},
					{
						targets: 14,
						 data: "etudiant.pays",
						 name: "pays"   
					},
					{
						targets: 15,
						 data: "etudiant.email",
						 name: "email"   
					},
					{
						targets: 16,
						 data: "etudiant.telephone",
						 name: "telephone"   
					},
					{
						targets: 17,
						 data: "chambreReference" ,
						 name: "chambreFk.reference" 
					},
					{
						targets: 18,
						 data: "userName" ,
						 name: "userFk.name" 
					}
            ]
        });

        function getEtudiants() {
            dataTable.ajax.reload();
        }

        function deleteEtudiant(etudiant) {
            abp.message.confirm(
                '',
                app.localize('AreYouSure'),
                function (isConfirmed) {
                    if (isConfirmed) {
                        _etudiantsService.delete({
                            id: etudiant.id
                        }).done(function () {
                            getEtudiants(true);
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

        $('#CreateNewEtudiantButton').click(function () {
            _createOrEditModal.open();
        });        

		$('#ExportToExcelButton').click(function () {
            _etudiantsService
                .getEtudiantsToExcel({
				filter : $('#EtudiantsTableFilter').val(),
					iNEFilter: $('#INEFilterId').val(),
					codeEtudiantFilter: $('#CodeEtudiantFilterId').val(),
					prenomFilter: $('#PrenomFilterId').val(),
					nomFilter: $('#NomFilterId').val(),
					sexeFilter: $('#SexeFilterId').val(),
					minDateDeNaissanceFilter:  getDateFilter($('#MinDateDeNaissanceFilterId')),
					maxDateDeNaissanceFilter:  getMaxDateFilter($('#MaxDateDeNaissanceFilterId')),
					lieuDeNaissanceFilter: $('#LieuDeNaissanceFilterId').val(),
					situationMatrimonialeFilter: $('#SituationMatrimonialeFilterId').val(),
					cINFilter: $('#CINFilterId').val(),
					adresseFilter: $('#AdresseFilterId').val(),
					villeFilter: $('#VilleFilterId').val(),
					regionFilter: $('#RegionFilterId').val(),
					paysFilter: $('#PaysFilterId').val(),
					emailFilter: $('#EmailFilterId').val(),
					telephoneFilter: $('#TelephoneFilterId').val(),
					chambreReferenceFilter: $('#ChambreReferenceFilterId').val(),
					userNameFilter: $('#UserNameFilterId').val()
				})
                .done(function (result) {
                    app.downloadTempFile(result);
                });
        });

        abp.event.on('app.createOrEditEtudiantModalSaved', function () {
            getEtudiants();
        });

		$('#GetEtudiantsButton').click(function (e) {
            e.preventDefault();
            getEtudiants();
        });

		$(document).keypress(function(e) {
		  if(e.which === 13) {
			getEtudiants();
		  }
		});
		
		
		
    });
})();
