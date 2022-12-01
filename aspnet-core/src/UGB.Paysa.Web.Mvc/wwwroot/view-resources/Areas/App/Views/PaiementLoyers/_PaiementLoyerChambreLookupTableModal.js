(function ($) {
    app.modals.ChambreLookupTableModal = function () {

        var _modalManager;

        var _paiementLoyersService = abp.services.app.paiementLoyers;
        var _$chambreTable = $('#ChambreTable');

        this.init = function (modalManager) {
            _modalManager = modalManager;
        };


        var dataTable = _$chambreTable.DataTable({
            paging: true,
            serverSide: true,
            processing: true,
            listAction: {
                ajaxFunction: _paiementLoyersService.getAllChambreForLookupTable,
                inputFilter: function () {
                    return {
                        filter: $('#ChambreTableFilter').val()
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

        $('#ChambreTable tbody').on('click', '[id*=selectbtn]', function () {
            var data = dataTable.row($(this).parents('tr')).data();
            _modalManager.setResult(data);
            _modalManager.close();
        });

        function getChambre() {
            dataTable.ajax.reload();
        }

        $('#GetChambreButton').click(function (e) {
            e.preventDefault();
            getChambre();
        });

        $('#SelectButton').click(function (e) {
            e.preventDefault();
        });

        $(document).keypress(function (e) {
            if (e.which === 13) {
                getChambre();
            }
        });

    };
})(jQuery);

