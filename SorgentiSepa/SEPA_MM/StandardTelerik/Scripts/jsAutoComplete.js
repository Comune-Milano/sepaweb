/* FUNZIONI AUTOCOMPLETAMENTO INDIRIZZO */
function requestingComune(sender, eventArgs) {
    var context = eventArgs.get_context();
};
function OnClientEntryAddingHandlerComuneSimple(sender, eventArgs) {
    var TrovaProvincia = false;
    if (document.getElementById('HFGetProvinciaComune')) {
        if (document.getElementById('HFGetProvinciaComune').value == '1') {
            TrovaProvincia = true;
        };
    };
    var codComuneOld = $("#HFCodComuneSimple").val();
    var codComune = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codComune = sender.get_entries().getEntry(0).get_value();
    };
    codComune = codComune.replace('"', '').replace('"', '').replace('null', '');
    $("#HFCodComuneSimple").val(codComune);
    if (codComune === '' || codComune !== codComuneOld) {
        if (TrovaProvincia == true) {
            if (document.getElementById('txtProvincia')) {
                $("#txtProvincia").val('');
            };
        };
    };
    if (TrovaProvincia == true) {
        var dataToSend = { comune: eventArgs.get_text() };
        $.ajax({
            url: '../SepacomAutoComplete.asmx/GetProvinciaComune',
            dataType: "json",
            data: JSON.stringify(dataToSend),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: onSuccessProvinciaComune,
            dataFilter: function (data) { return data; },
            error: undefined
        });
    };
};
function OnClientEntryAddingHandlerComune(sender, eventArgs) {
    var TrovaProvincia = false;
    if (document.getElementById('HFGetProvinciaComune')) {
        if (document.getElementById('HFGetProvinciaComune').value == '1') {
            TrovaProvincia = true;
        };
    };
    var codComuneOld = $("#HFCodComune").val();
    var codComune = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codComune = sender.get_entries().getEntry(0).get_value();
    };
    codComune = codComune.replace('"', '').replace('"', '').replace('null', '');
    $("#txtCodComu").val(codComune);
    $("#HFCodComune").val(codComune);
    if (codComune === '' || codComune !== codComuneOld) {
        if (document.getElementById('txtCodIndirizzo')) {
            document.getElementById('txtCodIndirizzo').value = '';
        };
        if (document.getElementById('HFCodIndirizzo')) {
            document.getElementById('HFCodIndirizzo').value = '';
        };
        if (document.getElementById('HFCodCivico')) {
            document.getElementById('HFCodCivico').value = '';
        };
        if (document.getElementById('HFCodCap')) {
            document.getElementById('HFCodCap').value = '';
        };
        if ($find("acbindirizzo") !== null) {
            $find("acbindirizzo").get_entries().clear();
        };
        if ($find("acbCivico") !== null) {
            $find("acbCivico").get_entries().clear();
        };
        if ($find("acbCap") !== null) {
            $find("acbCap").get_entries().clear();
        };
        if (TrovaProvincia == true) {
            if (document.getElementById('txtProvincia')) {
                $("#txtProvincia").val('');
            };
        };
    };
    if (TrovaProvincia == true) {
        var dataToSend = { comune: eventArgs.get_text() };
        $.ajax({
            url: '../SepacomAutoComplete.asmx/GetProvinciaComune',
            dataType: "json",
            data: JSON.stringify(dataToSend),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: onSuccessProvinciaComune,
            dataFilter: function (data) { return data; },
            error: undefined
        });
    };
    if (document.getElementById('HFAggiornaDatiDaComune')) {
        if (document.getElementById('HFAggiornaDatiDaComune').value == '1') {
            if (document.getElementById('MasterPage_CPFooter_btnAggiornaDatiDaComune')) {
                document.getElementById('MasterPage_CPFooter_btnAggiornaDatiDaComune').click();
            };
        };
    };
};
function OnClientEntryAddingHandlerComuneAdvanced(sender, eventArgs) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var TrovaProvincia = false;
    if (document.getElementById('HFGetProvinciaComune')) {
        if (document.getElementById('HFGetProvinciaComune').value == '1') {
            TrovaProvincia = true;
        };
    };
    var codComuneOld = $("#" + arrayAutoCompletamento[0]).val();
    var codComune = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codComune = sender.get_entries().getEntry(0).get_value();
    };
    codComune = codComune.replace('"', '').replace('"', '').replace('null', '');
    $("#" + arrayAutoCompletamento[1]).val(codComune);
    $("#" + arrayAutoCompletamento[0]).val(codComune);
    if (codComune === '' || codComune !== codComuneOld) {
        if (document.getElementById(arrayAutoCompletamento[2])) {
            document.getElementById(arrayAutoCompletamento[2]).value = '';
        };
        if (document.getElementById(arrayAutoCompletamento[3])) {
            document.getElementById(arrayAutoCompletamento[3]).value = '';
        };
        if (document.getElementById(arrayAutoCompletamento[4])) {
            document.getElementById(arrayAutoCompletamento[4]).value = '';
        };
        if (document.getElementById(arrayAutoCompletamento[5])) {
            document.getElementById(arrayAutoCompletamento[5]).value = '';
        };
        if ($find(arrayAutoCompletamento[6]) !== null) {
            $find(arrayAutoCompletamento[6]).get_entries().clear();
        };
        if ($find(arrayAutoCompletamento[7]) !== null) {
            $find(arrayAutoCompletamento[7]).get_entries().clear();
        };
        if ($find(arrayAutoCompletamento[8]) !== null) {
            $find(arrayAutoCompletamento[8]).get_entries().clear();
        };
        if (TrovaProvincia == true) {
            if (document.getElementById(arrayAutoCompletamento[9])) {
                $("#" + arrayAutoCompletamento[9]).val('');
            };
        };
    };
    if (TrovaProvincia == true) {
        var dataToSend = { comune: eventArgs.get_text() };
        $.ajax({
            url: '../SepacomAutoComplete.asmx/GetProvinciaComune',
            dataType: "json",
            data: JSON.stringify(dataToSend),
            type: "POST",
            contentType: "application/json; charset=utf-8",
            async: false,
            cache: false,
            success: onSuccessProvinciaComuneAdvanced,
            dataFilter: function (data) { return data; },
            error: undefined
        });
    };
    if (document.getElementById('HFAggiornaDatiDaComune')) {
        if (document.getElementById('HFAggiornaDatiDaComune').value == '1') {
            if (document.getElementById('MasterPage_CPFooter_btnAggiornaDatiDaComune')) {
                document.getElementById('MasterPage_CPFooter_btnAggiornaDatiDaComune').click();
            };
        };
    };
};
function TrovaProvincia(comuneInserito) {
    var dataToSend = { comune: comuneInserito };
    $.ajax({
        url: '../SepacomAutoComplete.asmx/GetProvinciaComune',
        dataType: "json",
        data: JSON.stringify(dataToSend),
        type: "POST",
        contentType: "application/json; charset=utf-8",
        async: false,
        cache: false,
        success: onSuccessProvinciaComune,
        dataFilter: function (data) { return data; },
        error: undefined
    });
};
function onSuccessProvinciaComune(results, context, methodName) {
    var provincia = results.d;
    provincia = provincia.replace('"', '').replace('"', '').replace('null', '');
    $("#txtProvincia").val(provincia);
    if (document.getElementById('MasterPage_CPContenuto_txtProvincia')) {
        document.getElementById('MasterPage_CPContenuto_txtProvincia').value = provincia;
    };
};
function onSuccessProvinciaComuneAdvanced(results, context, methodName) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var provincia = results.d;
    provincia = provincia.replace('"', '').replace('"', '').replace('null', '');
    $("#" + arrayAutoCompletamento[9]).val(provincia);
    if (document.getElementById(arrayAutoCompletamento[10])) {
        document.getElementById(arrayAutoCompletamento[10]).value = provincia;
};
};
function requestingIndirizzo(sender, eventArgs) {
    var context = eventArgs.get_context();
    var comune = document.getElementById('txtCodComu').value;
    context["Comune"] = comune;
    if (comune === '') {
        eventArgs.set_cancel(true);
    };
};
function requestingIndirizzoAdvanced(sender, eventArgs) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var context = eventArgs.get_context();
    var comune = document.getElementById(arrayAutoCompletamento[1]).value;
    context["Comune"] = comune;
    if (comune === '') {
        eventArgs.set_cancel(true);
    };
};
function OnClientEntryAddingHandlerIndirizzo(sender, eventArgs) {
    var codIndirizzoOld = document.getElementById('HFCodIndirizzo').value;
    var codIndirizzo = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codIndirizzo = sender.get_entries().getEntry(0).get_value();
    };
    codIndirizzo = codIndirizzo.replace('"', '').replace('"', '').replace('null', '');
    document.getElementById('txtCodIndirizzo').value = codIndirizzo;
    document.getElementById('HFCodIndirizzo').value = codIndirizzo;
    if (codIndirizzo === '' || codIndirizzo !== codIndirizzoOld) {
        document.getElementById('HFCodCivico').value = '';
        $find("acbCivico").get_entries().clear();
    };
};
function OnClientEntryAddingHandlerIndirizzoAdvanced(sender, eventArgs) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var codIndirizzoOld = document.getElementById(arrayAutoCompletamento[3]).value;
    var codIndirizzo = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codIndirizzo = sender.get_entries().getEntry(0).get_value();
    };
    codIndirizzo = codIndirizzo.replace('"', '').replace('"', '').replace('null', '');
    document.getElementById(arrayAutoCompletamento[2]).value = codIndirizzo;
    document.getElementById(arrayAutoCompletamento[3]).value = codIndirizzo;
    if (codIndirizzo === '' || codIndirizzo !== codIndirizzoOld) {
        document.getElementById(arrayAutoCompletamento[4]).value = '';
        $find(arrayAutoCompletamento[7]).get_entries().clear();
    };
};
function requestingCivico(sender, eventArgs) {
    var context = eventArgs.get_context();
    var indirizzo = document.getElementById('txtCodIndirizzo').value;
    context["Indirizzo"] = indirizzo;
    if (document.getElementById('HFOggettoRicerca')) {
        if (document.getElementById('HFOggettoRicerca').value != '') {
            context["OggettoRicerca"] = document.getElementById('HFOggettoRicerca').value;
        };
    };
    if (indirizzo === '') {
        eventArgs.set_cancel(true);
    };
};
function requestingCivicoAdvanced(sender, eventArgs) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var context = eventArgs.get_context();
    var indirizzo = document.getElementById(arrayAutoCompletamento[2]).value;
    context["Indirizzo"] = indirizzo;
	if (document.getElementById('HFOggettoRicerca')) {
        if (document.getElementById('HFOggettoRicerca').value != '') {
            context["OggettoRicerca"] = document.getElementById('HFOggettoRicerca').value;
        };
    };
    if (indirizzo === '') {
        eventArgs.set_cancel(true);
    };
};
function OnClientEntryAddingHandlerCivico(sender, eventArgs) {
    var codCivico = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codCivico = sender.get_entries().getEntry(0).get_value();
    };
    codCivico = codCivico.replace('"', '').replace('"', '').replace('null', '');
    document.getElementById('HFCodCivico').value = codCivico;
};
function OnClientEntryAddingHandlerCivicoAdvanced(sender, eventArgs) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var codCivico = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codCivico = sender.get_entries().getEntry(0).get_value();
    };
    codCivico = codCivico.replace('"', '').replace('"', '').replace('null', '');
    document.getElementById(arrayAutoCompletamento[4]).value = codCivico;
};
function OnClientEntryAddingCivico(sender, eventArgs) {
    if (sender.get_entries().get_count() > 20) {
        eventArgs.set_cancel(true);
    };
};
function requestingCap(sender, eventArgs) {
    var context = eventArgs.get_context();
    var comune = document.getElementById('txtCodComu').value;
    context["Comune"] = comune;
    if (comune === '') {
        eventArgs.set_cancel(true);
    };
};
function requestingCapAdvanced(sender, eventArgs) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var context = eventArgs.get_context();
    var comune = document.getElementById(arrayAutoCompletamento[1]).value;
    context["Comune"] = comune;
    if (comune === '') {
        eventArgs.set_cancel(true);
    };
};
function OnClientEntryAddingHandlerCap(sender, eventArgs) {
    var codCap = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codCap = sender.get_entries().getEntry(0).get_value();
    };
    codCap = codCap.replace('"', '').replace('"', '').replace('null', '');
    document.getElementById('HFCodCap').value = codCap;
};
function OnClientEntryAddingHandlerCapAdvanced(sender, eventArgs) {
    var AutoCompletamento = document.getElementById('HFAutoCompletamentoAdvanced').value;
    var arrayAutoCompletamento = AutoCompletamento.split(',');
    var codCap = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        codCap = sender.get_entries().getEntry(0).get_value();
    };
    codCap = codCap.replace('"', '').replace('"', '').replace('null', '');
    document.getElementById(arrayAutoCompletamento[5]).value = codCap;
};
function OnClientEntryAddingCap(sender, eventArgs) {
    if (sender.get_entries().get_count() > 5) {
        eventArgs.set_cancel(true);
    };
};
/* FUNZIONI AUTOCOMPLETAMENTO INDIRIZZO */
/* FUNZIONI AUTOCOMPLETAMENTO FORNITORI */
function requestingFornitori(sender, eventArgs) {
    var context = eventArgs.get_context();
};
function OnClientEntryAddingHandlerFornitori(sender, eventArgs) {
    var idFornitore = '';
    if (sender.get_entries().getEntry(0) !== undefined) {
        idFornitore = sender.get_entries().getEntry(0).get_value();
    };
    idFornitore = codCap.replace('"', '').replace('"', '').replace('null', '');
    $("#HFidFornitore").val(idFornitore);
    if (idFornitore === '') {
        document.getElementById('HFidFornitore').value = '';
    };
};
/* FUNZIONI AUTOCOMPLETAMENTO FORNITORI */