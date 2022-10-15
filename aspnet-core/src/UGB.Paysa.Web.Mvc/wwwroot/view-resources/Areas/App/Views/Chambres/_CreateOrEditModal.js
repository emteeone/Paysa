(function ($) {
    app.modals.CreateOrEditChambreModal = function () {

        var _chambresService = abp.services.app.chambres;

        var _modalManager;
        var _$chambreInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$chambreInformationForm = _modalManager.getModal().find('form[name=ChambreInformationsForm]');
            _$chambreInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$chambreInformationForm.valid()) {
                return;
            }

            

            var chambre = _$chambreInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _chambresService.createOrEdit(
				chambre
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditChambreModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);