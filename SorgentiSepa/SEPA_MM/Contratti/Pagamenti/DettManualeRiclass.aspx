<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettManualeRiclass.aspx.vb"
    Inherits="Contratti_Pagamenti_DettManualeRiclass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" language="javascript">
        window.name = "modal";
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {

                e.preventDefault();
            }
        }

        function $onkeydown() {
            if (event.keyCode == 13) {
                event.keyCode = '9';
            }
        }


        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');
            document.getElementById('txtModificato').value = '1';
            return true;

        }

        function ChiediConferma() {
            if (document.getElementById("cmbTipoPagamento").value != '-1') {
                if (document.getElementById('txtImpPagamento')) {
                    if (parseFloat(document.getElementById('txtImpPagamento').value.replace(/\./g, '').replace(',', '.')) > parseFloat(document.getElementById('totResiduo').value.replace(/\./g, '').replace(',', '.'))) {
                        document.getElementById('isCredito').value = 1;
                        alert('Inserire un importo pari o inferiore al pagabile!');
                        document.getElementById("confCredito").value = 0;

                    }
                }
            }
        }


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
        function SostPuntVirg(e, obj) {
            var keyPressed;

            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {

                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        }

        function AutoDecimal2(obj) {

            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a != 'NaN') {
                    if (a.substring(a.length - 3, 0).length >= 4) {
                        var decimali = a.substring(a.length, a.length - 2);
                        var dascrivere = a.substring(a.length - 3, 0);
                        var risultato = '';
                        while (dascrivere.replace('-', '').length >= 4) {
                            risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                        }
                        risultato = dascrivere + risultato + ',' + decimali;
                        //document.getElementById(obj.id).value = a.replace('.', ',')
                        document.getElementById(obj.id).value = risultato;
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace('.', ',');
                    }

                }
                else
                    document.getElementById(obj.id).value = '';
            }
        }

        function ApriIncassiNonAttribuiti() {
            alert('Si sta per utilizzare la maschera per la gestione degli incassi non\nattribuiti.Una volta selezionato l\'assegno non sarà possibile\n modificare nè il suo importo nè il metodo di pagamento.\nPer modificare uno di questi due campi sarà necessario\nuscire dalla maschera cliccando sul tasto \"Esci\"!');
            window.showModalDialog('IncassiNonAttribuiti.aspx', 'IncassiNonAttribuiti', 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
        }
    </script>
    <style type="text/css">
        #form1
        {
            width: 800px;
            height: 400px;
        }
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 8pt;
        }
    </style>
    <title>Dettaglio Bolletta Riclassificata</title>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 780px; height: 480px; visibility: hidden;
        vertical-align: top; line-height: normal; top: 70px; left: 12px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" target="modal">
    <table style="width: 98%; position: absolute; top: 21px; left: 6px;">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                    Bolletta non Pagata N°&nbsp; <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:Label ID="lblNumBolletta" runat="server"></asp:Label>
                    </span></span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="Label" Visible="False" Width="580px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <table>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            Text="Data Pagamento*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataPagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="10" Width="65px" ReadOnly="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataPagamento"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                            width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            SetFocusOnError="True" Display="Dynamic" ValidationGroup="Man"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            Text="Data Valuta*"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDataValuta" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="10" Width="65px" ReadOnly="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataValuta"
                                            ErrorMessage="!" Font-Bold="True" Font-Names="Arial" Font-Size="11pt" Style="z-index: 2;
                                            width: 13px; height: 18px;" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                            SetFocusOnError="True" Display="Dynamic" ValidationGroup="Man"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rdbTipoPagam" runat="server" Font-Bold="True" Font-Names="Arial"
                                Font-Size="8pt" RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Value="0">Imp. Semi-Automatica</asp:ListItem>
                                <asp:ListItem Value="1">Imp. Manuale</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td align="right">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                            Text="Pagamento €"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtImpPagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            MaxLength="20" Width="75px" Style="text-align: right"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:CheckBox ID="chkEcludeQS" runat="server" AutoPostBack="True" BackColor="#FFFF99"
                                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="#FF3300" Text="Escludi Quote Sind."
                                            Enabled="False" Style="visibility: hidden;" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="width: 52%">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        &nbsp
                                    </td>
                                    <td>
                                        <asp:Label ID="lblIncassNonAtt" runat="server" Font-Names="Arial" Font-Size="8pt"
                                            Text="Label" Width="200px" Style="visibility: hidden;"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="labelNumeroAssegno" runat="server"
                                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" Text="Numero assegno" Visible="False"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNumeroPagamento" runat="server"
                                Font-Names="Arial" Font-Size="8pt" MaxLength="20" Width="75px" Style="text-align: right;"
                                Visible="False" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="style1" style="width: 40%">
                            TIPOLOGIA PAGAMENTO PARZIALE
                        </td>
                        <td class="style1">
                            NOTE
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="cmbTipoPagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="97%" AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNotePagamento" runat="server" Font-Names="Arial" Font-Size="8pt"
                                MaxLength="500" Width="97%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td class="style1">
                Voci Bolletta
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div id="DivVociBol" style="overflow: auto; width: 765px; height: 190px;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:DataGrid ID="DataGridBollette" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;
                            left: 193px; top: 54px" Width="740px" BorderColor="Gray" BorderWidth="1px">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_VOCE" HeaderText="ID_VOCE" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IMP_PAGATO" HeaderText="IMP. PAGATO €.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO €.">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="PAGAMENTO €.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPaga" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                            Width="75px" Enabled="False"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid>
                    </span></strong>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td style="text-align: center">
                <span id="testo" runat="server" visible="false" clientidmode="Static"><b><span style="color: #ec483e;
                    font-family: Arial; font-size: 12pt;">QUOTE SINDACALI ESCLUSE&nbsp;&nbsp;&nbsp;</span></b></span>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right">
                <table style="width: 100%;">
                    <tr>
                        <td width="67%">
                            &nbsp;&nbsp;
                        </td>
                        <td style="text-align: right; vertical-align: bottom">
                            <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="~/NuoveImm/Img_Conferma.png"
                                ToolTip="Conferma" ValidationGroup="Man" TabIndex="-1" OnClientClick="if (Page_ClientValidate('Man')) {document.getElementById('splash').style.visibility = 'visible';ChiediConferma();}" />
                        </td>
                        <td style="text-align: right; vertical-align: bottom">
                            <img id="exit" alt="Esci" src="../../NuoveImm/Img_EsciCorto.png" title="Esci" style="cursor: pointer"
                                onclick="window.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="clickConferma" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="totBolletta" runat="server" Value="0" />
    <asp:HiddenField ID="totPagato" runat="server" Value="0" />
    <asp:HiddenField ID="idContratto" runat="server" Value="0" />
    <asp:HiddenField ID="totResiduo" runat="server" Value="0" />
    <asp:HiddenField ID="credito" runat="server" Value="0" />
    <asp:HiddenField ID="confCredito" runat="server" Value="0" />
    <asp:HiddenField ID="isCredito" runat="server" Value="0" />
    <asp:HiddenField ID="callEpi" runat="server" Value="0" />
    <asp:HiddenField ID="stopImpSuperiore" runat="server" Value="0" />
    <asp:HiddenField ID="tipoVocPag" runat="server" Value="2" />
    <asp:HiddenField ID="vIdConnessione" runat="server" Value="0" />
    <asp:HiddenField ID="ImportoIncasso" runat="server" Value="0" />
    <asp:HiddenField ID="PagatoDaChiamata" runat="server" Value="0" />
    <asp:HiddenField ID="TotStoPerInserire" runat="server" Value="0" />
    <asp:HiddenField ID="BolloPagParz" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        document.getElementById('splash').style.visibility = 'hidden';
        var c1 = '#0c2238';
        var c2 = '#ec483e';
        function colore1() {
            codice = '<B><SPAN style="FONT-SIZE: 12pt; COLOR: ' + c1 + '; FONT-FAMILY: Arial">QUOTE SINDACALI ESCLUSE&nbsp;&nbsp;&nbsp;</SPAN></B>';
            if (document.getElementById("testo")) {
                document.getElementById("testo").innerHTML = codice;
            };
            attesa = window.setTimeout("colore2()", 500);
        };
        function colore2() {
            codice = '<B><SPAN style="FONT-SIZE: 12pt; COLOR: ' + c2 + '; FONT-FAMILY: Arial">QUOTE SINDACALI ESCLUSE&nbsp;&nbsp;&nbsp;</SPAN></B>';
            if (document.getElementById("testo")) {
                document.getElementById("testo").innerHTML = codice;
            };
            attesa = window.setTimeout("colore1()", 500);
        };
        function avvia() {
            attesa = window.setTimeout("colore1()", 500);
        };
        avvia();
    </script>
    </form>
</body>
</html>
