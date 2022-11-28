(function ($) {
    app.modals.CreateOrEditOperationModal = function () {

        var _operationsService = abp.services.app.operations;

        var _modalManager;
        var _$operationInformationForm = null;

		        var _OperationcompteLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Operations/CompteLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Operations/_OperationCompteLookupTableModal.js',
            modalClass: 'CompteLookupTableModal'
        });        var _OperationtypeOperationLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/Operations/TypeOperationLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/Operations/_OperationTypeOperationLookupTableModal.js',
            modalClass: 'TypeOperationLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$operationInformationForm = _modalManager.getModal().find('form[name=OperationInformationsForm]');
            _$operationInformationForm.validate();
        };

		          $('#OpenCompteLookupTableButton').click(function () {

            var operation = _$operationInformationForm.serializeFormToObject();

            _OperationcompteLookupTableModal.open({ id: operation.compteId, displayName: operation.compteNumeroCompte }, function (data) {
                _$operationInformationForm.find('input[name=compteNumeroCompte]').val(data.displayName); 
                _$operationInformationForm.find('input[name=compteId]').val(data.id); 
            });
        });
		
		$('#ClearCompteNumeroCompteButton').click(function () {
                _$operationInformationForm.find('input[name=compteNumeroCompte]').val(''); 
                _$operationInformationForm.find('input[name=compteId]').val(''); 
        });
		
        $('#OpenTypeOperationLookupTableButton').click(function () {

            var operation = _$operationInformationForm.serializeFormToObject();

            _OperationtypeOperationLookupTableModal.open({ id: operation.typeProductionId, displayName: operation.typeOperationNom }, function (data) {
                _$operationInformationForm.find('input[name=typeOperationNom]').val(data.displayName); 
                _$operationInformationForm.find('input[name=typeProductionId]').val(data.id); 
            });
        });
		
		$('#ClearTypeOperationNomButton').click(function () {
                _$operationInformationForm.find('input[name=typeOperationNom]').val(''); 
                _$operationInformationForm.find('input[name=typeProductionId]').val(''); 
        });
		


        this.save = function () {
            if (!_$operationInformationForm.valid()) {
                return;
            }
            if ($('#Operation_CompteId').prop('required') && $('#Operation_CompteId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Compte')));
                return;
            }
            if ($('#Operation_TypeProductionId').prop('required') && $('#Operation_TypeProductionId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('TypeOperation')));
                return;
            }

            

            var operation = _$operationInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _operationsService.createOrEdit(
				operation
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditOperationModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);