<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VociServizio.aspx.vb" Inherits="CicloPassivo_CicloPassivo_APPALTI_VociServizio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }


    function $onkeydown() {

        if (event.keyCode == 13) {
            event.keyCode = 0;
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
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


    function CalcolaPrezzoTotale(obj, quantita, prezzo) {
        var risultato;

        //alert("pippo");
        quantita = quantita.replace('.', '');
        quantita = quantita.replace(',', '.');

        prezzo = prezzo.replace('.', '');
        prezzo = prezzo.replace(',', '.');

        risultato = quantita * prezzo;
        risultato = risultato.toFixed(2);
        //alert(risultato);
        document.getElementById('txtTotale').value = risultato.replace('.', ',');
        document.getElementById('txtTotale2').value = risultato.replace('.', ',');

        AutoDecimal2(document.getElementById('txtTotale'))
        AutoDecimal2(document.getElementById('txtTotale2'))
    }




    function CalcolaImporti(obj, importo, perc_oneri, perc_sconto, perc_iva) {
        var oneri;
        var asta;
        var iva;
        var risultato1;
        var risultato2;
        var risultato3;
        var risultato4;


        //alert("pippo");
        importo = importo.replace('.', '');
        perc_oneri = perc_oneri.replace('.', '');
        perc_sconto = perc_sconto.replace('.', '');
        perc_iva = perc_iva.replace('.', '');

        importo = importo.replace(',', '.');
        perc_oneri = perc_oneri.replace(',', '.');
        perc_sconto = perc_sconto.replace(',', '.');
        perc_iva = perc_iva.replace(',', '.');

        //ONERI di SICUREZZA= (IMPORTO*perc_oneri)/100
        oneri = (importo * perc_oneri) / 100;

        //LORDO senza ONERI= IMPORTO-oneri
        risultato1 = importo - oneri;

        //RIBASSO ASTA= (LORDO senza oneri*perc_sconto)/100
        asta = (risultato1 * perc_sconto) / 100;

        //NETTO senza ONERI= (LORDO senza oneri-asta)
        risultato2 = risultato1 - asta;

        //NETTO con ONERI= (IMPORTO-asta)
        risultato3 = importo - asta;

        //IVA= (NETTO con oneri*perc_iva)/100
        iva = (risultato3 * perc_iva) / 100;

        //NETTO+ONERI+IVA
        risultato4 = risultato3 + iva;


        oneri = oneri.toFixed(2);
        asta = asta.toFixed(2);
        iva = iva.toFixed(2);

        risultato1 = risultato1.toFixed(2);
        risultato2 = risultato2.toFixed(2);
        risultato3 = risultato3.toFixed(2);
        risultato4 = risultato4.toFixed(2);


        //alert(risultato);
    }    


</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <%--<base target="_self"/>--%>
    <title>MODULO GESTIONE VOCI DI SERVIZIO</title>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
    <script type="text/javascript">
        document.write('<style type="text/css">.tabber{display:none;}<\/style>');
        //var tabberOptions = {'onClick':function(){alert("clicky!");}};
        var tabberOptions = {


            /* Optional: code to run when the user clicks a tab. If this
            function returns boolean false then the tab will not be changed
            (the click is canceled). If you do not return a value or return
            something that is not boolean false, */

            'onClick': function (argsObj) {

                var t = argsObj.tabber; /* Tabber object */
                var id = t.id; /* ID of the main tabber DIV */
                var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
                var e = argsObj.event; /* Event object */

                document.getElementById('txttab').value = i + 1;
            },
            'addLinkId': true
        };

    </script>
    <%--<script type="text/javascript" src="tabber.js"></script>
<link rel="stylesheet" href="example.css" type="text/css" media="screen"/>--%>
    <script language="javascript" type="text/javascript">

        //window.onbeforeunload = confirmExit;


        function ConfermaAnnulloConsuntivo() {
            if (document.getElementById('txtIdComponente').value != '') {
                var sicuro = confirm('Sei sicuro di voler cancellare questa voce?');
                if (sicuro == true) {
                    document.getElementById('txtannullo').value = '1';
                }
                else {
                    document.getElementById('txtannullo').value = '0';
                }
            }
            else {
                alert('Nessuna riga selezionata!');
            }
        }


        function ConfermaEsci() {
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                    //document.getElementById('USCITA').value='0';
                }
            }
        }

        function confirmExit() {
            if (document.getElementById("USCITA").value == '0') {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.returnValue = "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
                else {
                    return "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
                }
            }
        }

        function controlla_div() {
            if (document.getElementById('txtIdComponente').value != "") {
                document.getElementById('txtAppare2').value = '1';
                document.getElementById('DIV_C').style.visibility = 'visible';
            }
            else {
                alert('Nessuna riga selezionata!');
            }
        }
   
    </script>
    <style type="text/css">
        .style1
        {
            width: 39px;
        }
        #form1
        {
            width: 783px;
        }
    </style>
</head>
<body bgcolor="#f2f5f1" text="#ede0c0">
    <form id="form1" runat="server">
    <div id="DIV1" style="display: block; left: 0px; background-image: url(../../../../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        &nbsp;&nbsp;
        <table>
            <tr>
                <td style="width: 70px">
                    &nbsp;
                    <asp:ImageButton ID="btnIndietro" runat="server" CausesValidation="False" ImageUrl="../../../../NuoveImm/Img_Indietro.png"
                        OnClientClick="document.getElementById('USCITA').value='1';" Style="z-index: 100;
                        left: 16px; position: static; top: 32px" TabIndex="-1" ToolTip="Indietro" />
                </td>
                <td style="width: 130px">
                    <asp:ImageButton ID="btnApri1" runat="server" CausesValidation="False" Height="12px"
                        ImageUrl="~/NuoveImm/Img_Modifica.png" TabIndex="10" ToolTip="Modifica voce selezionata"
                        Width="60px" OnClientClick="controlla_div();document.getElementById('USCITA').value='1';"
                        Style="left: 734px; position: absolute; top: 152px" />
                    <asp:ImageButton ID="btnAgg1" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Aggiungi.png"
                        OnClientClick="document.getElementById('txtAppare2').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_C').style.visibility='visible';"
                        Style="left: 734px; position: absolute; top: 129px" TabIndex="9" ToolTip="Aggiunge" />
                </td>
                <td style="width: 250px">
                </td>
                <td style="width: 60px">
                    <asp:ImageButton ID="btnElimina1" runat="server" Height="12px" ImageUrl="~/NuoveImm/Img_Elimina.png"
                        TabIndex="11" ToolTip="Elimina voce selezionata" Width="60px" OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloConsuntivo();"
                        Style="left: 734px; position: absolute; top: 175px; bottom: 339px;" />
                </td>
                <td style="width: 30px">
                    &nbsp; &nbsp;&nbsp;
                </td>
                <td style="width: 60px">
                </td>
                <td style="width: 100px">
                    &nbsp; &nbsp;&nbsp;
                </td>
                <td style="width: 60px">
                    <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                        Style="z-index: 125; left: 600px; position: static; top: 29px" TabIndex="12"
                        ToolTip="Esce dalla gestione delle voci servizio" OnClientClick="document.getElementById('USCITA').value='1';"
                        CausesValidation="False" />
                </td>
            </tr>
        </table>
        <table id="TABBLE_LISTA">
            <tr>
                <td style="width: 234px">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 234px">
                    <table style="width: 100%;">
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="62px" Height="17px">Servizio</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtservizio" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="10pt" MaxLength="50" ReadOnly="True" Style="z-index: 107;
                                    left: 109px; top: 67px" TabIndex="34" Width="661px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 234px">
                    <asp:Label ID="lblELENCO_INTERVENTI" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO VOCI SERVIZI"
                        Width="248px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 234px">
                    <div style="border: thin solid #0000cc; visibility: visible; overflow: auto; width: 725px;
                        height: 386px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
                            clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                            TabIndex="8" Width="727px" BorderColor="Black" CellSpacing="4">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle Width="0%" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Width="60%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PERC_REVERSIBILITA" HeaderText="PERC_REVERSIBILITA">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IVA_CANONE" HeaderText="IVA CANONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IVA_CONSUMO" HeaderText="IVA CONSUMO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="QUOTA_PREVENTIVA" HeaderText="QUOTA PREVENTIVA">
                                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CATEGORIA" HeaderText="CATEGORIA">
                                    <HeaderStyle Width="10%" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="VOCE ASSOCIATA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CODICE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="NO_MOD" HeaderText="NO_MOD" Visible="False"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                        </asp:DataGrid>
                    </div>
                    <asp:TextBox ID="txtSel1" runat="server" BackColor="#F2F5F1" BorderColor="White"
                        BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                        ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="780px"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div id="DIV_C" style="left: 0px; background-image: url('../../../../NuoveImm/SfondoMascheraContratti.jpg');
        width: 800px; position: absolute; top: 0px; height: 550px">
        &nbsp;
        <table style="border-color: #6699ff; border-width: thin; z-index: 105; left: 37px;
            width: 699px; margin-right: 0px; position: absolute; top: 69px; height: 412px;">
            <tr>
                <td>
                    <strong><span style="color: #0000ff; font-family: Arial">Gestione Voce Servizio &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; </span>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="style3" style="vertical-align: top; text-align: left; height: 283px;">
                    <table border="0" cellpadding="0" cellspacing="5">
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106;" Width="92px">Descrizione voce*</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtdescrizione" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="Arial" Font-Size="8pt" Height="88px" MaxLength="2000" Style="z-index: 107;"
                                    TabIndex="1" TextMode="MultiLine" Width="98%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106;" Width="125px">Percentuale reversibilità</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtreversibilita" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="ARIAL" Font-Size="10pt" MaxLength="3" Style="z-index: 107; text-align: right"
                                    TabIndex="2" Width="45px"></asp:TextBox><asp:Label ID="Label15" runat="server" Font-Bold="False"
                                        Font-Names="Arial" Font-Size="8pt" ForeColor="Black" Style="text-align: right"
                                        TabIndex="61" Text="%" Width="16px"></asp:Label><asp:RegularExpressionValidator ID="RegularExpressionValidator4"
                                            runat="server" ControlToValidate="txtreversibilita" Display="Dynamic" ErrorMessage="!"
                                            Font-Bold="True" Height="16px" Style="z-index: 135;" ValidationExpression="^(100(?:\.0{1,2})?|0*?\.\d{1,2}|\d{1,2}(?:\.\d{1,2})?)$"
                                            Width="1px"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106;" Width="125px">IVA canone</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbivacorpo" runat="server" BackColor="White" Font-Names="Arial"
                                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="3" Width="47px">
                                </asp:DropDownList>
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="61" Text="%" Width="16px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106;" Width="125px">IVA consumo</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbivaconsumo" runat="server" BackColor="White" Font-Names="Arial"
                                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="4" Width="47px">
                                </asp:DropDownList>
                                <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="text-align: right" TabIndex="61" Text="%" Width="16px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106;" Width="125px">Quota preventiva</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbquota" runat="server" BackColor="White" Font-Names="Arial"
                                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="5" Width="47px">
                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                    <asp:ListItem Value="0" Selected="True">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106;" Width="125px">Categoria</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListCategorie" runat="server" BackColor="White" Font-Names="Arial"
                                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="5" Width="500px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 106;" Width="125px">Voce Associata</asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="cmbVoci" runat="server" BackColor="White" Font-Names="Arial"
                                    Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    border-left: black 1px solid; border-bottom: black 1px solid" TabIndex="5" Width="500px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; width: 352px; text-align: left">
                    <asp:HiddenField ID="txtIDV" runat="server" Value="" />
                </td>
            </tr>
            <tr>
                <td align="right" style="vertical-align: top; height: 40px; text-align: right">
                    <br />
                    <asp:ImageButton ID="btn_InserisciVoce" runat="server" ImageUrl="../Immagini/Next.png"
                        OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('txtAppare2').value='0';"
                        Style="cursor: pointer;" TabIndex="55" ToolTip="Aggiorna" />&nbsp;
                    <asp:ImageButton ID="btn_ChiudiVoce" runat="server" ImageUrl="../../../../NuoveImm/Img_Esci.png"
                        OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_C').style.visibility='hidden';document.getElementById('txtAppare2').value='0';"
                        Style="cursor: pointer" TabIndex="57" ToolTip="Esci" />
                </td>
            </tr>
        </table>
        <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="../../../../ImmDiv/DivMGrande.png"
            Style="z-index: 101; left: 14px; position: absolute; top: 52px; width: 766px;
            height: 450px;" />
    </div>
    <asp:TextBox ID="USCITA" runat="server" Style="z-index: -1; left: 0px; position: absolute;
        top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtIdComponente" runat="server" Style="z-index: -1; left: 0px; position: absolute;
        top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtannullo" runat="server" Style="z-index: -1; left: 0px; position: absolute;
        top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtModificato" runat="server" Style="z-index: -1; left: 0px; position: absolute;
        top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtidservizio" runat="server" Style="z-index: -1; left: 0px; position: absolute;
        top: 200px" TabIndex="-1"></asp:TextBox>
    <asp:TextBox ID="txtConnessione" runat="server" Style="z-index: -1; left: 0px; position: absolute;
        top: 200px" TabIndex="-1"></asp:TextBox>
    <asp:HiddenField ID="SOLO_LETTURA" runat="server" Value="0" />
    <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
        BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute;
        top: 200px" TabIndex="-1" Width="48px">0</asp:TextBox>
    <asp:HiddenField ID="txtnomod" runat="server" />
    <asp:HiddenField ID="txtAppare2" runat="server" Value="0" />
    </form>
    <script type="text/javascript">

        if (document.getElementById('txtAppare2').value != 1) {
            document.getElementById('DIV_C').style.visibility = 'hidden';
        }
          
    </script>
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
</body>
</html>
