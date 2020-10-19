<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RiepGestione.aspx.vb" Inherits="Condomini_RiepGestione" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <script type="text/javascript" language="javascript">
        window.name="modal";
            function CompletaData(e, obj) {
        
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
//        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';

    }

    function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) {
                var decimali = a.substring(a.length, a.length - 2);
                var dascrivere = a.substring(a.length - 3, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {
                    risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato 
                    dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                }
                risultato = dascrivere + risultato + ',' + decimali
                //document.getElementById(obj.id).value = a.replace('.', ',')
                document.getElementById(obj.id).value = risultato
            }
            else {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }

        }
    }
    
    function UpdateSituazPat() {
    var idGestione = '<%=vIdGestione %>'
        if (idGestione != '') {
            var Conferma
            Conferma = window.confirm("Attenzione...Desidera aggiornare anche la Situazione Patrimoniale?");
            if (Conferma == false) {
                document.getElementById('AggSitPat').value = '0';
            }
            else {
                document.getElementById('AggSitPat').value = '1';

            }
        }
    }

    function ConfermaUscita() {
        var sess = '<%=Session("MODIFYMODAL")%>'
        if (document.getElementById('txtModificato').value == 1 ) {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.Uscire senza salvare?");
            if (chiediConferma == true) {
                document.getElementById('txtesci').value = '1';
//                '<%=Session("MODIFYMODAL")%>' = '1';
//  
            }

        }
        else {
            document.getElementById('txtesci').value = '1';
        }

    }
    function CDataPrec(e,obj) {
        
            var dbDateFormat = obj.value.substring(6, 10) + obj.value.substring(3, 5) + obj.value.substring(0, 2)
            var dbDateFormatPrec
            var dbDateFormatSucc
            if (dbDateFormat > 0) {
//            & dbDateFormat <= document.getElementById("TxtAnnoFine").value + document.getElementById("TxtFineGest").value.substring(3, 5) + document.getElementById("TxtFineGest").value.substring(0, 2)  
                if (dbDateFormat >= document.getElementById("txtAnnoInizio").value + document.getElementById("txtInizioGest").value.substring(3, 5) + document.getElementById("txtInizioGest").value.substring(0, 2) ) {
                    if (parseInt(obj.id.substring(11, 12)) > 1) {
                        var dataPrec = document.getElementById(obj.id.replace(obj.id.substring(11, 12), (parseInt(obj.id.substring(11, 12)) - 1))).value
                        if (obj.name.indexOf('6') <= 0) {
                            var dataSucc = document.getElementById(obj.id.replace(obj.id.substring(11, 12), (parseInt(obj.id.substring(11, 12)) + 1))).value
                        }
                        else {
                            var dataSucc = document.getElementById(obj.id.replace(obj.id.substring(11, 12), (parseInt(obj.id.substring(11, 12))))).value
                        }
                        dbDateFormatPrec = dataPrec.substring(6, 10) + dataPrec.substring(3, 5) + dataPrec.substring(0, 2)
                        dbDateFormatSucc = dataSucc.substring(6, 10) + dataSucc.substring(3, 5) + dataSucc.substring(0, 2)
                        if (dbDateFormatPrec != '') {
                            if (dbDateFormat <= dbDateFormatPrec) {
                                alert('La data non può essere inferiore alla precedente!')
                                obj.value = ''
                            }
                        }
                        if (dbDateFormatSucc != '') {
                            if (dbDateFormat > dbDateFormatSucc) {
                                alert('La data non può essere superiore alla successiva!')
                                obj.value = ''
                            }
                        }
                    }
                }
                else {
                    alert('La data non può essere al di fuori del Periodo di Gestione dell\'Esercizio!')
                    obj.value = ''
                }
            }

        }


        function CallConsuntivo() { 
        var idGest = <%=vIdGestione %>
          if (idGest != '' || idGest != '0'){
              var dialogResults = window.showModalDialog('ConsGestione.aspx?IDCONDOMINIO= '+ document.getElementById('idCondominio').value +'&IDCON=<%=vIdConnModale %>&IDGEST=<%=vIdGestione %>&IDVISUAL='+ document.getElementById('idVisual').value ,'window', 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
              if ((dialogResults != undefined) && (dialogResults == '1') && (dialogResults != false)) {
                  document.getElementById('txtModificato').value = '1';
              }
          }
          else {
              alert('Salvare il preventivo prima di procedere!');
          }
        }

        function CallLiberi() { 
        var idGest = <%=vIdGestione %>
          if (idGest != '' || idGest != '0'){
              var dialogResults = window.showModalDialog('LiberiAbusivi.aspx?IDCONDOMINIO= '+ document.getElementById('idCondominio').value +'&IDCON=<%=vIdConnModale %>&IDGEST=<%=vIdGestione %>&IDVISUAL='+ document.getElementById('idVisual').value +'&CHIAMA=P' ,'window', 'status:no;dialogWidth:900px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
              if ((dialogResults != undefined) && (dialogResults == '1') && (dialogResults != false)) {
                  document.getElementById('txtModificato').value = '1';
              }
          }
          else {
              alert('Salvare il preventivo prima di procedere!');
          }
        }
        function CallSitPatr() { 
        var idGest = <%=vIdGestione %>
          if (idGest != '' || idGest != '0'){
               window.open('RptSituazPat.aspx?IDCONDOMINIO= '+ document.getElementById('idCondominio').value +'&CHIAMA=PREV&IDCON=<%=vIdConnModale %>&IDGESTIONE=<%=vIdGestione %>&IDVISUAL='+ document.getElementById('idVisual').value ,'', '');
          }
          else {
              alert('Salvare il preventivo prima di procedere!');
          }
        }

        
        function ApriInqInd() {
                var idGest = <%=vIdGestione %>
          if (idGest != 0 ){

            if (document.getElementById('idCondominio').value != 0) {
             var w = screen.width ;
             w = w - 20;
               window.showModalDialog('ContabIndiretta.aspx?IDCON=<%=vIdConnModale %>&IDGESTIONE=<%=vIdGestione %>&IdCond=' + document.getElementById('idCondominio').value, 'ContIndiretta', 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;');
            }
            }
                      else {
              alert('Salvare il preventivo prima di procedere!');
          }

        }

    </script>
    <title>Riepilogo Gestione Condominiale</title>
    <style type="text/css">
        .style3
        {
            width: 98px;
        }
        .style4
        {
            width: 14px;
        }
    </style>
    <link href="../Standard/Style/css/smoothness/jquery-ui-1.10.4.custom.min.css" rel="stylesheet"
        type="text/css" />
    <script src="../Standard/Scripts/jquery/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery-ui-1.9.0.custom.min.js" type="text/javascript"></script>
    <script src="../Standard/Scripts/jquery/jquery.ui.datepicker-it.js" type="text/javascript"></script>
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoContratto.png);
    background-repeat: no-repeat">
    <form id="form1" runat="server" target="modal">
    <div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <asp:Label ID="lblTitolo" runat="server" Style="position: absolute; top: 22px; left: 13px;"
                Text="Gestione Condominio : nomeCond"></asp:Label>
        </span></strong>
        <asp:Label ID="lblStatoPreventivo" runat="server" Style="position: absolute; top: 28px;
            left: 742px; width: 149px;" Text="STATO: BOZZA" Font-Bold="True" Font-Names="Arial"
            Font-Size="8pt"></asp:Label>
    </div>
    <table style="width: 879px; position: absolute; top: 53px; height: 80%; left: 10px;">
        <tr>
            <td style="vertical-align: top; text-align: left; width: 877px; height: 310px;">
                <table width="100%" style="border-right: gainsboro thin solid; border-top: gainsboro thin solid;
                    border-left: gainsboro thin solid; border-bottom: gainsboro thin solid; vertical-align: top;
                    text-align: left;">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="INIZIO GESTIONE"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="FINE GESTIONE"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="TIPO*"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="N° RATE*"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label28" runat="server" Font-Names="Arial" Font-Size="8pt" Text="NOTE"></asp:Label>
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnConvalida" runat="server" ImageUrl="~/Condomini/Immagini/Img_Convalida.png"
                                TabIndex="-1" ToolTip="Convalida" Style="height: 16px" />
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:TextBox ID="txtInizioGest" runat="server" Width="55px" BackColor="White" TabIndex="-1"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="9pt" Text="/"></asp:Label>
                        </td>
                        <td style="text-align: left; vertical-align: top;">
                            <asp:TextBox ID="txtAnnoInizio" runat="server" Width="60px" BackColor="White" TabIndex="-1"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="9pt" Style="text-align: center"
                                Text="-" Width="10px"></asp:Label>
                        </td>
                        <td style="text-align: left; vertical-align: top;">
                            <asp:TextBox ID="TxtFineGest" runat="server" Width="55px" BackColor="White" TabIndex="3"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                            <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="9pt" Text="/"></asp:Label>
                            &nbsp;
                        </td>
                        <td style="text-align: left; vertical-align: top;">
                            <asp:TextBox ID="TxtAnnoFine" runat="server" Width="60px" BackColor="White" TabIndex="4"
                                MaxLength="10" Font-Names="Arial" Font-Size="9pt" Style="text-align: left" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="text-align: left; vertical-align: top;">
                            <asp:DropDownList ID="cmbTipoGest" runat="server" Style="top: 109px; left: 9px; right: 481px;"
                                Font-Names="Arial" Font-Size="9pt" TabIndex="1" Width="120px" BackColor="White"
                                Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td style="vertical-align: top; text-align: left;">
                            <asp:DropDownList ID="cmbNumRate" runat="server" Style="top: 109px; left: 9px; right: 481px;"
                                Font-Names="Arial" Font-Size="9pt" TabIndex="2" Width="60px" BackColor="White"
                                AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td style="vertical-align: top; text-align: left;" colspan="2">
                            <asp:TextBox ID="txtNote" runat="server" Width="318px" BackColor="White" TabIndex="3"
                                MaxLength="10" Font-Names="Arial" Font-Size="8pt" Style="text-align: left" TextMode="MultiLine"
                                Height="22px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div style="display: none; width: 700px; overflow: auto;">
                    <%--<table cellpadding="0" cellspacing="0" style="width: 100%; text-align: right">
                        <tr>
                            <td colspan="" style="vertical-align: top; text-align: left; height: 53px;">
                                <table style="vertical-align: bottom; text-align: left; height: 40px;">
                                    <tr>
                                        <td style="text-align: center;" width="290px">
                                            <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Style="text-align: center" Text="VOCE"></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="70px">
                                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Style="text-align: center" Text="CONG. PREC"></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="70px">
                                            <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="PREVENT."></asp:Label>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 1°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata1" runat="server" BackColor="White" MaxLength="10" TabIndex="4"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataRata1"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 2°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata2" runat="server" BackColor="White" MaxLength="10" TabIndex="5"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataRata2"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label24" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 3°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata3" runat="server" BackColor="White" MaxLength="10" TabIndex="6"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataRata3"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="70px">
                                            <asp:Label ID="Label25" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 4°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata4" runat="server" BackColor="White" MaxLength="10" TabIndex="7"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataRata4"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="70px">
                                            <asp:Label ID="Label26" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 5°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata5" runat="server" BackColor="White" MaxLength="10" TabIndex="8"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataRata5"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label27" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 6°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata6" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataRata6"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 7°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata7" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataRata7"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 8°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata8" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataRata8"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 9°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata9" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtDataRata9"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label29" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 10°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata10" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtDataRata10"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 11°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata11" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtDataRata11"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label31" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 12°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata12" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtDataRata12"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label32" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 13°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata13" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtDataRata13"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 14°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata14" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtDataRata14"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label34" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 15°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata15" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtDataRata15"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label35" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 16°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata16" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtDataRata16"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label36" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 17°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata17" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtDataRata17"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label37" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 18°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata18" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtDataRata18"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label38" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 19°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata19" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator19" runat="server" ControlToValidate="txtDataRata19"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label39" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 20°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata20" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator20" runat="server" ControlToValidate="txtDataRata20"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label40" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 21°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata21" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator21" runat="server" ControlToValidate="txtDataRata21"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label41" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 22°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata22" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator22" runat="server" ControlToValidate="txtDataRata22"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label42" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 23°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata23" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator23" runat="server" ControlToValidate="txtDataRata23"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                        <td style="text-align: center;" width="60px">
                                            <asp:Label ID="Label43" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="MediumBlue" Text="RATA 24°"></asp:Label>
                                            <asp:TextBox ID="txtDataRata24" runat="server" BackColor="White" MaxLength="10" TabIndex="9"
                                                Width="55px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator24" runat="server" ControlToValidate="txtDataRata24"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>--%>
                </div>
                <table border="0" cellpadding="0" cellspacing="0" width="879px;">
                    <tr>
                        <td style="vertical-align: top; width: 459px">
                            <asp:DataGrid ID="DataGridVociSpesaRiepilogo" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                PageSize="1" Style="z-index: 105; left: 8px; top: 32px" HorizontalAlign="Left"
                                TabIndex="10">
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                <AlternatingItemStyle Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="DESCRIZIONE" ReadOnly="True" HeaderText="VOCE">
                                        <HeaderStyle ForeColor="MediumBlue" Font-Bold="true" HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle Wrap="true" Width="335px" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="CONG. PREC.">
                                        <HeaderStyle ForeColor="MediumBlue" Font-Bold="true" HorizontalAlign="Center"  Wrap="false" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCongPrec" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.CONGUAGLIO_GP") %>'
                                                Width="60px" Style="text-align: right"></asp:TextBox>
                                            <asp:Label ID="Label2" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="PREVENT.">
                                        <HeaderStyle ForeColor="MediumBlue" Font-Bold="true" HorizontalAlign="Center" Wrap="false" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtPreventivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.PREVENTIVO") %>' Width="60px"
                                                Style="text-align: right"></asp:TextBox>
                                            <asp:Label ID="Label4" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="FL_TOTALE" HeaderText="FL_TOTALE" Visible="False"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPO_RIGA" HeaderText="TIPO_RIGA" Visible="False"></asp:BoundColumn>
                                </Columns>
                                <EditItemStyle Wrap="False" />
                                <SelectedItemStyle Wrap="False" />
                                <ItemStyle Wrap="False" />
                            </asp:DataGrid>
                        </td>
                        <td style="vertical-align: top; width: 400px">
                            <div style="overflow: auto; width: 400px; height: 300px;" id="DivVociSpesa">
                                <asp:DataGrid ID="DataGridVociSpesa" runat="server" AutoGenerateColumns="False" Width="150px">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Width="50px" HorizontalAlign="Center" />
                                    <Columns>
                                        <asp:BoundColumn DataField="IDVOCE" HeaderText="IDVOCE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_GESTIONE" HeaderText="ID_GESTIONE" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="CONGUAGLIO_GP" HeaderText="CONGUAGLIO_GP" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="PREVENTIVO" HeaderText="PREVENTIVO" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_1" HeaderText="RATA_1" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_2" HeaderText="RATA_2" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_3" HeaderText="RATA_3" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_4" HeaderText="RATA_4" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_5" HeaderText="RATA_5" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_6" HeaderText="RATA_6" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_7" HeaderText="RATA_7" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_8" HeaderText="RATA_8" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_9" HeaderText="RATA_9" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_10" HeaderText="RATA_10" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_11" HeaderText="RATA_11" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_12" HeaderText="RATA_12" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_13" HeaderText="RATA_13" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_14" HeaderText="RATA_14" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_15" HeaderText="RATA_15" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_16" HeaderText="RATA_16" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_17" HeaderText="RATA_17" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_18" HeaderText="RATA_18" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_19" HeaderText="RATA_19" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_20" HeaderText="RATA_20" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_21" HeaderText="RATA_21" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_22" HeaderText="RATA_22" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_23" HeaderText="RATA_23" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="RATA_24" HeaderText="RATA_24" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCRIZIONE" ReadOnly="True" Visible="false"></asp:BoundColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtCongPrec" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.CONGUAGLIO_GP") %>'
                                                    Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPreventivo" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Text='<%# DataBinder.Eval(Container, "DataItem.PREVENTIVO") %>' Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 1°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata1" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_1") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator1" runat="server" ControlToValidate="txtRata1"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 2°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true"
                                            Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata2" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_2") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator2" runat="server" ControlToValidate="txtRata2"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 3°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata3" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_3") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator3" runat="server" ControlToValidate="txtRata3"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 4°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata4" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_4") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator4" runat="server" ControlToValidate="txtRata4"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 5°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata5" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_5") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator5" runat="server" ControlToValidate="txtRata5"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 6°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata6" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_6") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator6" runat="server" ControlToValidate="txtRata6"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 7°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata7" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_7") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator7" runat="server" ControlToValidate="txtRata7"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 8°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata8" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_8") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator8" runat="server" ControlToValidate="txtRata8"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 9°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata9" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_9") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator9" runat="server" ControlToValidate="txtRata9"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 10°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata10" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_10") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator10" runat="server" ControlToValidate="txtRata10"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 11°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata11" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_11") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator11" runat="server" ControlToValidate="txtRata11"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 12°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata12" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_12") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator12" runat="server" ControlToValidate="txtRata12"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 13°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata13" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_13") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator13" runat="server" ControlToValidate="txtRata13"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 14°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata14" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_14") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator14" runat="server" ControlToValidate="txtRata14"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 15°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata15" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_15") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator15" runat="server" ControlToValidate="txtRata15"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 16°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata16" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_16") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator16" runat="server" ControlToValidate="txtRata16"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 17°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata17" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_17") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator17" runat="server" ControlToValidate="txtRata17"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 18°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata18" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_18") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator18" runat="server" ControlToValidate="txtRata18"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 19°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata19" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_19") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator19" runat="server" ControlToValidate="txtRata19"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 20°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata20" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_20") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator20" runat="server" ControlToValidate="txtRata20"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 21°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata21" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_21") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator21" runat="server" ControlToValidate="txtRata21"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 22°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata22" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_22") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator22" runat="server" ControlToValidate="txtRata22"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 23°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata23" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_23") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator23" runat="server" ControlToValidate="txtRata23"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="RATA 24°" HeaderStyle-ForeColor="MediumBlue" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtRata24" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_24") %>'
                                                    Style="text-align: right" Width="60"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="Validator24" runat="server" ControlToValidate="txtRata24"
                                                    Display="Dynamic" ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    Height="1px" Style="z-index: 2; left: 683px; top: 67px" ToolTip="Inserire una data valida"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="1px"></asp:RegularExpressionValidator>
                                            </ItemTemplate>
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Center" Width="60px" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="FL_TOTALE" HeaderText="FL_TOTALE" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_VOCE_PF" HeaderText="ID_VOCE_PF" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ID_VOCE_PF_IMPORTO" HeaderText="ID_VOCE_PF_IMPORTO" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPO_RIGA" HeaderText="TIPO_RIGA" Visible="False"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                </table>
                <div style="display: none; width: 800px; overflow: auto;">
                    <%--<table style="width: 98%; vertical-align: bottom; text-align: left; z-index: 200;
                        top: 449px; left: 95px;" id="pepp" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="text-align: center;">
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totCongPrec" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totPreventivo" runat="server" BackColor="White" Enabled="False"
                                    Font-Names="Arial" Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1"
                                    Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata1" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata2" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata3" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata4" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata5" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata6" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata7" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata8" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata9" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata10" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata11" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata12" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata13" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata14" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata15" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata16" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata17" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata18" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata19" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata20" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata21" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata22" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata23" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                            <td style="width: 9%; text-align: center;">
                                <asp:TextBox ID="totRata24" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                    Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" Width="60px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>--%>
                </div>
                <div style="overflow: auto; width: 100%; height: 65px; display: none" id="DivVociSpesa0">
                    <%--<asp:DataGrid ID="DataGridVociSpMor" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="1" Style="z-index: 105;
                        left: 8px; top: 32px" HorizontalAlign="Left" Width="856px" TabIndex="10">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <AlternatingItemStyle Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="IDVOCE" HeaderText="IDVOCE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_GESTIONE" HeaderText="ID_GESTIONE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="CONGUAGLIO_GP" HeaderText="CONGUAGLIO_GP" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PREVENTIVO" HeaderText="PREVENTIVO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_1" HeaderText="RATA_1" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_2" HeaderText="RATA_2" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_3" HeaderText="RATA_3" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_4" HeaderText="RATA_4" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_5" HeaderText="RATA_5" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_6" HeaderText="RATA_6" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_7" HeaderText="RATA_7" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_8" HeaderText="RATA_8" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_9" HeaderText="RATA_9" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_10" HeaderText="RATA_10" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_11" HeaderText="RATA_11" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_12" HeaderText="RATA_12" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_13" HeaderText="RATA_13" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_14" HeaderText="RATA_14" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_15" HeaderText="RATA_15" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_16" HeaderText="RATA_16" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_17" HeaderText="RATA_17" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_18" HeaderText="RATA_18" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_19" HeaderText="RATA_19" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_20" HeaderText="RATA_20" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_21" HeaderText="RATA_21" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_22" HeaderText="RATA_22" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_23" HeaderText="RATA_23" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RATA_24" HeaderText="RATA_24" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" ReadOnly="True">
                                <HeaderStyle Width="26%" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtCongPrecMor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.CONGUAGLIO_GP") %>' Width="60px"
                                        Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtPreventivoMor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.PREVENTIVO") %>' Width="60px"
                                        Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata1Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_1") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata2Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_2") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata3Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_3") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata4Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_4") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                    Font-Strikeout="False" Font-Underline="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata5Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_5") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata6Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_6") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata7Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_7") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label44" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata8Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_8") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label45" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata9Mor" runat="server" Font-Names="Arial" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.RATA_9") %>'
                                        Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label46" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata10Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_10") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label47" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata11Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_11") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label48" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata12Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_12") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label49" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata13Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_13") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label50" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata14Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_14") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label51" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata15Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_15") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label52" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata16Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_16") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label53" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata17Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_17") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label54" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata18Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_18") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label55" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata19Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_19") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label56" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata20Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_20") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label57" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata21Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_21") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label58" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata22Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_22") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label59" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata23Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_23") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label60" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRata24Mor" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RATA_24") %>' Width="60px" Style="text-align: right"></asp:TextBox>
                                    <asp:Label ID="Label61" runat="server"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="9%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="FL_TOTALE" HeaderText="FL_TOTALE" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_VOCE_PF" HeaderText="ID_VOCE_PF" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_VOCE_PF_IMPORTO" HeaderText="ID_VOCE_PF_IMPORTO" Visible="False">
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle Wrap="False" />
                        <SelectedItemStyle Wrap="False" />
                        <ItemStyle Wrap="False" />
                    </asp:DataGrid>--%>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: text-top; text-align: center; width: 877px; height: 22px;">
                <table style="width: 100%;">
                    <tr>
                        <td width="10%">
                            <asp:ImageButton ID="btnSommatoria" runat="server" ImageUrl="~/Condomini/Immagini/Img_Totale.png"
                                TabIndex="11" ToolTip="Somma delle colonne" />
                        </td>
                        <td width="15%">
                            <asp:ImageButton ID="btnElInquilini" runat="server" ImageUrl="~/Condomini/Immagini/ImgElInquilini.png"
                                TabIndex="14" ToolTip="Visualizza l'elenco degli inquilini per il cariacmento delle informazioni contabili"
                                OnClientClick="ApriInqInd();" Visible="False" />
                        </td>
                        <td width="15%">
                            <asp:ImageButton ID="btnLiberiAbusivi" runat="server" ImageUrl="~/Condomini/Immagini/Img_LibeAbusivi.png"
                                TabIndex="14" ToolTip="Visualizza l'elenco delle unità libere o occupate abusivamente"
                                OnClientClick="CallLiberi();return false;" />
                        </td>
                        <td width="15%">
                            <asp:ImageButton ID="btnSituazPatr" runat="server" ImageUrl="~/Condomini/Immagini/Img_SituazPatr.png"
                                TabIndex="14" ToolTip="Visualizza la situazione patrimoniale" OnClientClick="CallSitPatr();return false;" />
                        </td>
                        <td width="15%">
                            <asp:ImageButton ID="btnConsuntivi" runat="server" ImageUrl="~/Condomini/Immagini/Img_Consuntivo.png"
                                TabIndex="14" ToolTip="Elabora i Consuntivi del Condominio" OnClientClick="CallConsuntivo();" />
                        </td>
                        <td width="15%">
                            <asp:ImageButton ID="btnSalvaCambioAmm" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                TabIndex="12" OnClientClick="UpdateSituazPat();" ToolTip="Salva" Style="height: 16px" />
                        </td>
                        <td width="15%">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                TabIndex="15" ToolTip="Esci" OnClientClick="ConfermaUscita();" Style="height: 16px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: text-top; text-align: left; width: 877px; height: 22px;">
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Height="18px" Style="z-index: 104; left: 9px; top: 222px" Visible="False"
                    Width="100%"></asp:Label>
                <br />
                <br />
                <br />
                <br />
                <asp:HiddenField ID="txtesci" runat="server" Value="0" />
                <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
                <asp:HiddenField ID="txtSalvato" runat="server" Value="0" />
                <asp:HiddenField ID="AggSitPat" runat="server" Value="0" />
                <asp:HiddenField ID="nRate" runat="server" Value="0" />
                <asp:HiddenField ID="idPianoF" runat="server" Value="0" />
                <asp:HiddenField ID="idCondominio" runat="server" Value="0" />
                <asp:HiddenField ID="idVisual" runat="server" Value="" />
                <asp:HiddenField ID="NewEs" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField4" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField5" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField6" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField7" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField8" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField9" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField10" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField11" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField12" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField13" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField14" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField15" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField16" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField17" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField18" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField19" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField20" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField21" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField22" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField23" runat="server" Value="0" />
                <asp:HiddenField ID="HiddenField24" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata1" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata2" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata3" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata4" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata5" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata6" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata7" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata8" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata9" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata10" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata11" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata12" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata13" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata14" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata15" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata16" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata17" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata18" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata19" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata20" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata21" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata22" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata23" runat="server" Value="0" />
                <asp:HiddenField ID="totaleRata24" runat="server" Value="0" />
                <asp:HiddenField ID="totCongPrec" runat="server" Value="0" />
                <asp:HiddenField ID="totPreventivo" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:HiddenField ID="TextBox1" runat="server" />
    </span></strong>
    </form>
</body>
<script type="text/javascript">
    $(function () {
        $("#DataGridVociSpesa_ctl02_txtRata1").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata2").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata3").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata4").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata5").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata6").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata7").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata8").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata9").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata10").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata11").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata12").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata13").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata14").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata15").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata16").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata17").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata18").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata19").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata20").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata21").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata22").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata23").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        $("#DataGridVociSpesa_ctl02_txtRata24").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
    });
</script>
</html>
