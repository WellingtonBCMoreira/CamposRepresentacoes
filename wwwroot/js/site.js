$(function () {
    //introJs().start();

    $('.lds-spinner').hide();

    $('[data-tooltip="tooltip"]').tooltip();

    $('.select2').select2({});

    $('.moeda').inputmask('currency', {
        rightAlign: false,
        prefix: 'R$ ',
        radixPoint: ',',
        groupSeparator: '.'
    });


    $("#serachForm").submit(function () {
        $('.lds-spinner').show();
    });

    $(window).on('load', function () {
        $('.lds-spinner').hide();
        $("#loadResult").show();
    });

    const lista = document.getElementById("breadcrumb");
    const ultimaLi = lista.lastElementChild;
    const spanDentroLi = ultimaLi.querySelector("span");
    spanDentroLi.classList.add("breadcrumb-active");
});


function PopulateDropDown(selector, options) {
    const selectElement = document.querySelector(selector);
    options.forEach(option => {
        const optionElement = document.createElement('option');
        optionElement.value = option.value;
        optionElement.textContent = option.text;
        selectElement.appendChild(optionElement);
    });
}
$('.limpar-sessao').on("click", function () {
    $.ajax({
        type: "GET",
        url: "../Common?handler=LimparSessao",
        success: function (response) {
        },
        failure: function (response) {
            console.log(response.responseText);
        },
        error: function (response) {
            console.log(response.responseText);
        }
    });
});

$('.onlyNumbers').on('keypress', function (event) {
    const charTyped = String.fromCharCode(event.which);
    const letterRegex = /[^0-9]/;
    if (charTyped.match(letterRegex)) {
        event.preventDefault();
    }
});
function ClearForm(form) {

    var formElements = form.elements;
    for (var i = 0; i < formElements.length; i++) {
        var element = formElements[i];
        switch (element.type) {
            case "text":
            case "textarea":
            case "password":
                element.value = "";
                break;
            case "radio":
            case "checkbox":
                element.checked = false;
                break;
            case "select-one":
                if (element.classList.contains("select2")) {
                    $(element)[0].options.length = 0;
                } else {
                    element.selectedIndex = 0;
                }
                break;
            case "select-multiple":
                element.selectedIndex = 0;
                break;
        }
    }
}