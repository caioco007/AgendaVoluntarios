// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function _MaskData() {
    $('.quota').mask('#,0', { reverse: true });
    $('._date').mask('00/00/0000');
    $('.date').mask('00/00/0000');
    $('.date').datepicker({ format: 'dd/mm/yyyy', language: 'pt-BR', autoclose: true });
    $('.time').mask('00:00:00');
    $('.date-time').mask('00/00/0000 00:00:00');
    $('.monthYear').mask('00/0000', { clearIfNotMatch: true });
    $('.monthYear').datepicker({ format: 'mm/yyyy', startView: "year", minViewMode: "months", language: 'pt-BR', autoclose: true });
    $('.cep').mask('00000-000');
    $('.mobile').mask('(00) #0000-0000');
    $('.phone').mask('(00) 0000-0000');
    $('.percent5').mask('##0,00000', { reverse: true });
    $('.percent2').mask('##0,00', { reverse: true });
    $('.cnpj').mask('00.000.000/0000-00', { reverse: true, clearIfNotMatch: true });
    $('.cpf').mask('000.000.000-00', { reverse: true, clearIfNotMatch: true });
    $('._cpf').mask('#000.000.000-00', { reverse: true, clearIfNotMatch: true });
    $('.money').mask('#.##0,00', { reverse: true });
    $('.placa').mask('SSS-0000');
    $('.fipe').mask('000000-0');
    $('.renavam').mask('00000000-0');
    $('.year').mask('0000');
    $('.rg').mask('00.000.000-00');
    $('.number').mask('#');

    var CPMaskBehavior = function (val) {
        return val.replace(/\D/g, '').length === 11 ? '(00) 00000-0000' : '(00) 0000-00009';
    },
        cpOptions = {
            onKeyPress: function (val, e, field, options) {
                field.mask(CPMaskBehavior.apply({}, arguments), options);
            }
        };

    $('.celphones').mask(CPMaskBehavior, cpOptions);
}
$(function () {
    _MaskData();

    //$('.dataTables_length select').select2({ minimumResultsForSearch: Infinity });
});
