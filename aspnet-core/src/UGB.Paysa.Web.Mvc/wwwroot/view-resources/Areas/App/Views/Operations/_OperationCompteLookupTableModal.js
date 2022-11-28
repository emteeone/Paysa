(function ($) {
    app.modals.CompteLookupTableModal = function () {

        var _modalManager;

        var _operationsService = abp.services.app.operations;
        var _$compteTable = $('#CompteTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$compteTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _operationsService.getAllCompteForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#CompteTableFilter').val()
                    };
                }
            },
            columnDefs: [
                {
                    targets: 0,
                    data: null,
                    orderable: false,
                    autoWidth: false,
                    defaultContent: "<div class=\"text-center\"><input id='selectbtn' class='btn btn-success' type='button' width='25px' value='" + app.localize('Select') + "' /></div>"
                },
                {
                    autoWidth: false,
                    orderable: false,
                    targets: 1,
                    data: "displayName"
                }
            ]
        });

        $('#CompteTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getCompte() {
            dataTable.ajax.reload();
        }

        $('#GetCompteButton').click(function (e) {
            e.preventDefault();
            getCompte();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getCompte();
            }
        });

    };
})(jQuery);

