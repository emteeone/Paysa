(function ($) {
    app.modals.CreateOrEditCompteModal = function () {

        var _comptesService = abp.services.app.comptes;

        var _modalManager;
        var _$compteInformationForm = null;

		        var _CompteetudiantLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Comptes/EtudiantLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Comptes/_CompteEtudiantLookupTableModal.js',
            modalClass: 'EtudiantLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$compteInformationForm = _modalManager.getModal().find('form[name=CompteInformationsForm]');
            _$compteInformationForm.validate();
        };

		          $('#OpenEtudiantLookupTableButton').click(function () {

            var compte = _$compteInformationForm.serializeFormToObject();

            _CompteetudiantLookupTableModal.open({ id: compte.etudiantId, displayName: compte.etudiantCodeEtudiant }, function (data) {
                _$compteInformationForm.find('input[name=etudiantCodeEtudiant]').val(data.displayName); 
                _$compteInformationForm.find('input[name=etudiantId]').val(data.id); 
            });
        });
		
		$('#ClearEtudiantCodeEtudiantButton').click(function () {
                _$compteInformationForm.find('input[name=etudiantCodeEtudiant]').val(''); 
                _$compteInformationForm.find('input[name=etudiantId]').val(''); 
        });
		


        this.save = function () {
            if (!_$compteInformationForm.valid()) {
                return;
            }
            if ($('#Compte_EtudiantId').prop('required') && $('#Compte_EtudiantId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Etudiant')));
                return;
            }

            

            var compte = _$compteInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _comptesService.createOrEdit(
				compte
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditCompteModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);