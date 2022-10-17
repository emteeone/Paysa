(function ($) {
    app.modals.CreateOrEditTerminalModal = function () {

        var _terminauxService = abp.services.app.terminaux;

        var _modalManager;
        var _$terminalInformationForm = null;

		
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$terminalInformationForm = _modalManager.getModal().find('form[name=TerminalInformationsForm]');
            _$terminalInformationForm.validate();
        };

		  

        this.save = function () {
            if (!_$terminalInformationForm.valid()) {
                return;
            }

            

            var terminal = _$terminalInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _terminauxService.createOrEdit(
				terminal
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditTerminalModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);