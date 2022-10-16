(function ($) {
    app.modals.CreateOrEditCarteModal = function () {

        var _cartesService = abp.services.app.cartes;

        var _modalManager;
        var _$carteInformationForm = null;

		        var _CartecompteLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Cartes/CompteLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Cartes/_CarteCompteLookupTableModal.js',
            modalClass: 'CompteLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$carteInformationForm = _modalManager.getModal().find('form[name=CarteInformationsForm]');
            _$carteInformationForm.validate();
        };

		          $('#OpenCompteLookupTableButton').click(function () {

            var carte = _$carteInformationForm.serializeFormToObject();

            _CartecompteLookupTableModal.open({ id: carte.compteId, displayName: carte.compteNumeroCompte }, function (data) {
                _$carteInformationForm.find('input[name=compteNumeroCompte]').val(data.displayName); 
                _$carteInformationForm.find('input[name=compteId]').val(data.id); 
            });
        });
		
		$('#ClearCompteNumeroCompteButton').click(function () {
                _$carteInformationForm.find('input[name=compteNumeroCompte]').val(''); 
                _$carteInformationForm.find('input[name=compteId]').val(''); 
        });
		


        this.save = function () {
            if (!_$carteInformationForm.valid()) {
                return;
            }
            if ($('#Carte_CompteId').prop('required') && $('#Carte_CompteId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Compte')));
                return;
            }

            

            var carte = _$carteInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _cartesService.createOrEdit(
				carte
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditCarteModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);