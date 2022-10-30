(function ($) {
    app.modals.CreateOrEditEtudiantModal = function () {

        var _etudiantsService = abp.services.app.etudiants;

        var _modalManager;
        var _$etudiantInformationForm = null;

		        var _EtudiantchambreLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Etudiants/ChambreLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Etudiants/_EtudiantChambreLookupTableModal.js',
            modalClass: 'ChambreLookupTableModal'
        });        var _EtudiantuserLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Etudiants/UserLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Etudiants/_EtudiantUserLookupTableModal.js',
            modalClass: 'UserLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$etudiantInformationForm = _modalManager.getModal().find('form[name=EtudiantInformationsForm]');
            _$etudiantInformationForm.validate();
        };

		          $('#OpenChambreLookupTableButton').click(function () {

            var etudiant = _$etudiantInformationForm.serializeFormToObject();

            _EtudiantchambreLookupTableModal.open({ id: etudiant.chambreId, displayName: etudiant.chambreReference }, function (data) {
                _$etudiantInformationForm.find('input[name=chambreReference]').val(data.displayName); 
                _$etudiantInformationForm.find('input[name=chambreId]').val(data.id); 
            });
        });
		
		$('#ClearChambreReferenceButton').click(function () {
                _$etudiantInformationForm.find('input[name=chambreReference]').val(''); 
                _$etudiantInformationForm.find('input[name=chambreId]').val(''); 
        });
		
        $('#OpenUserLookupTableButton').click(function () {

            var etudiant = _$etudiantInformationForm.serializeFormToObject();

            _EtudiantuserLookupTableModal.open({ id: etudiant.userId, displayName: etudiant.userName }, function (data) {
                _$etudiantInformationForm.find('input[name=userName]').val(data.displayName); 
                _$etudiantInformationForm.find('input[name=userId]').val(data.id); 
            });
        });
		
		$('#ClearUserNameButton').click(function () {
                _$etudiantInformationForm.find('input[name=userName]').val(''); 
                _$etudiantInformationForm.find('input[name=userId]').val(''); 
        });
		


        this.save = function () {
            if (!_$etudiantInformationForm.valid()) {
                return;
            }
            if ($('#Etudiant_ChambreId').prop('required') && $('#Etudiant_ChambreId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Chambre')));
                return;
            }
            if ($('#Etudiant_UserId').prop('required') && $('#Etudiant_UserId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('User')));
                return;
            }

            

            var etudiant = _$etudiantInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _etudiantsService.createOrEdit(
				etudiant
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditEtudiantModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);