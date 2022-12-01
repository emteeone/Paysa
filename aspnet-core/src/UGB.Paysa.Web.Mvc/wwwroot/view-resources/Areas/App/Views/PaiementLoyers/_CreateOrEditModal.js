(function ($) {
    app.modals.CreateOrEditPaiementLoyerModal = function () {

        var _paiementLoyersService = abp.services.app.paiementLoyers;

        var _modalManager;
        var _$paiementLoyerInformationForm = null;

		        var _PaiementLoyerchambreLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PaiementLoyers/ChambreLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/PaiementLoyers/_PaiementLoyerChambreLookupTableModal.js',
            modalClass: 'ChambreLookupTableModal'
        });        var _PaiementLoyeroperationLookupTableModal = new app.ModalManager({
            viewUrl: abp.appPath + 'App/PaiementLoyers/OperationLookupTableModal',
            scriptUrl: abp.appPath + 'view-resources/Areas/App/Views/PaiementLoyers/_PaiementLoyerOperationLookupTableModal.js',
            modalClass: 'OperationLookupTableModal'
        });
		
		

        this.init = function (modalManager) {
            _modalManager = modalManager;

			var modal = _modalManager.getModal();
            modal.find('.date-picker').datetimepicker({
                locale: abp.localization.currentLanguage.name,
                format: 'L'
            });

            _$paiementLoyerInformationForm = _modalManager.getModal().find('form[name=PaiementLoyerInformationsForm]');
            _$paiementLoyerInformationForm.validate();
        };

		          $('#OpenChambreLookupTableButton').click(function () {

            var paiementLoyer = _$paiementLoyerInformationForm.serializeFormToObject();

            _PaiementLoyerchambreLookupTableModal.open({ id: paiementLoyer.chambreId, displayName: paiementLoyer.chambreReference }, function (data) {
                _$paiementLoyerInformationForm.find('input[name=chambreReference]').val(data.displayName); 
                _$paiementLoyerInformationForm.find('input[name=chambreId]').val(data.id); 
            });
        });
		
		$('#ClearChambreReferenceButton').click(function () {
                _$paiementLoyerInformationForm.find('input[name=chambreReference]').val(''); 
                _$paiementLoyerInformationForm.find('input[name=chambreId]').val(''); 
        });
		
        $('#OpenOperationLookupTableButton').click(function () {

            var paiementLoyer = _$paiementLoyerInformationForm.serializeFormToObject();

            _PaiementLoyeroperationLookupTableModal.open({ id: paiementLoyer.operationId, displayName: paiementLoyer.operationCodeOperation }, function (data) {
                _$paiementLoyerInformationForm.find('input[name=operationCodeOperation]').val(data.displayName); 
                _$paiementLoyerInformationForm.find('input[name=operationId]').val(data.id); 
            });
        });
		
		$('#ClearOperationCodeOperationButton').click(function () {
                _$paiementLoyerInformationForm.find('input[name=operationCodeOperation]').val(''); 
                _$paiementLoyerInformationForm.find('input[name=operationId]').val(''); 
        });
		


        this.save = function () {
            if (!_$paiementLoyerInformationForm.valid()) {
                return;
            }
            if ($('#PaiementLoyer_ChambreId').prop('required') && $('#PaiementLoyer_ChambreId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Chambre')));
                return;
            }
            if ($('#PaiementLoyer_OperationId').prop('required') && $('#PaiementLoyer_OperationId').val() == '') {
                abp.message.error(app.localize('{0}IsRequired', app.localize('Operation')));
                return;
            }

            

            var paiementLoyer = _$paiementLoyerInformationForm.serializeFormToObject();
            
            
            
			
			 _modalManager.setBusy(true);
			 _paiementLoyersService.createOrEdit(
				paiementLoyer
			 ).done(function () {
               abp.notify.info(app.localize('SavedSuccessfully'));
               _modalManager.close();
               abp.event.trigger('app.createOrEditPaiementLoyerModalSaved');
			 }).always(function () {
               _modalManager.setBusy(false);
			});
        };
        
        
    };
})(jQuery);