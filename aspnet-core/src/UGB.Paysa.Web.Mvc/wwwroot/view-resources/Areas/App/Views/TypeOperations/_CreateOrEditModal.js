(function ($) {
    app.modals.CreateOrEditTypeOperationModal = function () {

        var _typeOperationsService = abp.services.app.typeOperations;

        var _modalManager;
        var _$typeOperationInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$typeOperationInformationForm = _modalManager.getModal().find('form[name=TypeOperationInformationsForm]');
            _$typeOperationInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$typeOperationInformationForm.valid()) {
                return;
            }

            

            var typeOperation = _$typeOperationInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _typeOperationsService.createOrEdit(
				typeOperation
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditTypeOperationModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);