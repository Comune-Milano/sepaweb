<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestionePreferenze.aspx.vb"
    Inherits="ASS_GestionePreferenze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Gestione Preferenze</title>
    <script language="javascript" type="text/javascript">

        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {
                e.preventDefault();
                //document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }


        function $onkeydown() {

            if (event.keyCode == 13) {
                event.keyCode = 0;
                // document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }


        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';
        }



        function DelPointer(obj) {
            obj.value = obj.value.replace('.', '');
            document.getElementById(obj.id).value = obj.value;

        }





        function $onkeydown() {
            if ((event.keyCode == 46) || (event.keyCode == 8) || (event.keyCode == 116)) {
                event.keyCode = 0;
            }
        }

        function AutoDecimalPerc(obj) {
            obj.value = obj.value.replace(/\./gi, '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2);
                if (a > 100) {
                    a = 100;
                    a = parseFloat(a).toFixed(2);
                }
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





        function AutoDecimal2(obj) {
            obj.value = obj.value.replace(/\./gi, '');
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

        };




    </script>
    <style type="text/css">
        .style2
        {
            height: 10px;
            width: 44px;
        }
        .style3
        {
            height: 10px;
        }
    </style>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: repeat-x;
    width: 780px;" bgcolor="#fcfcfc">

    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('../Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica222" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%Response.Flush()%>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="99%" cellpadding="0" cellspacing="0">
        <tr>
            <td height="35px" valign="middle">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Preferenze
                    Utente</strong></span>
            </td>
        </tr>
        <tr>
            <td>
                <table style="border: 1px solid #996600" width="100%" cellpadding="4">
                    <tr>
                        <td>
                            <asp:Label ID="Label92" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Nominativo:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_nominativo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label98" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Tipo Bando:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_tipobando" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label42" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Data:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_datapg" runat="server" Font-Names="Arial" Font-Size="9pt" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label95" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Protocollo:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbl_protocollo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                Text="" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="25px" valign="bottom">
                <asp:Label ID="Label47" Style="margin-left: 5px;" runat="server" Font-Names="Arial"
                    Font-Size="8pt" Text=" * Sono disponibili 5 opzioni di scelta sulle posizioni geografiche di preferenza, posizioni geografiche escluse e l'intervallo dei piani di preferenza"
                    Font-Italic="True"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <table width="99%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td valign="middle" align="center" style="height: 30px">
                                    <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Posizioni geografiche di preferenza"
                                        Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:UpdatePanel ID="UpdatePanelPosPref" runat="server">
                                        <ContentTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%; height: 185px; border-width: 1px; border-style: solid solid solid solid;
                                                            border-color: #996600 #996600 #996600 #996600;" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="height: 10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600;"
                                                                    bgcolor="#99DF8C" width="15%" align="center" colspan="2">
                                                                    <asp:Label ID="Label32" runat="server" Font-Names="Arial" Font-Size="8pt" Text="1° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600;
                                                                    border-right-style: solid; border-left-style: solid; border-left-width: 1px;
                                                                    border-right-width: 1px; border-right-color: #996600; border-left-color: #996600;"
                                                                    align="center" bgcolor="#A6E49C" colspan="2">
                                                                    <asp:Label ID="Label48" runat="server" Font-Names="Arial" Font-Size="8pt" Text="2° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600;"
                                                                    align="center" bgcolor="#B3E6AA" colspan="2">
                                                                    <asp:Label ID="Label33" runat="server" Font-Names="Arial" Font-Size="8pt" Text="3° Opzione"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" bgcolor="#99DF8C" width="8%" align="center">
                                                                    <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px;" align="center" bgcolor="#99DF8C" width="24%">
                                                                    <asp:DropDownList ID="ddl_localita1" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    align="center" bgcolor="#A6E49C">
                                                                    <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#A6E49C">
                                                                    <asp:DropDownList ID="ddl_localita2" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#B3E6AA">
                                                                    <asp:Label ID="Label6" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#B3E6AA">
                                                                    <asp:DropDownList ID="ddl_localita3" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" bgcolor="#99DF8C" align="center">
                                                                    <asp:Label ID="Label3" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px;" align="center" bgcolor="#99DF8C">
                                                                    <asp:DropDownList ID="ddl_zona1" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Font-Size="8pt" AutoPostBack="True" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    align="center" bgcolor="#A6E49C">
                                                                    <asp:Label ID="Label7" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#A6E49C">
                                                                    <asp:DropDownList ID="ddl_zona2" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; z-index: 118; left: 475px; border-left: black 1px solid;
                                                                        border-bottom: black 1px solid; position: static; top: 101px" TabIndex="1" Font-Size="8pt"
                                                                        Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#B3E6AA">
                                                                    <asp:Label ID="Label9" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#B3E6AA">
                                                                    <asp:DropDownList ID="ddl_zona3" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; z-index: 118; left: 475px; border-left: black 1px solid;
                                                                        border-bottom: black 1px solid; position: static; top: 101px" TabIndex="1" Font-Size="8pt"
                                                                        Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" bgcolor="#99DF8C" align="center">
                                                                    <asp:Label ID="Label4" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px;" align="center" bgcolor="#99DF8C">
                                                                    <asp:DropDownList ID="ddl_quartiere1" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    align="center" bgcolor="#A6E49C">
                                                                    <asp:Label ID="Label8" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#A6E49C">
                                                                    <asp:DropDownList ID="ddl_quartiere2" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#B3E6AA">
                                                                    <asp:Label ID="Label10" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#B3E6AA">
                                                                    <asp:DropDownList ID="ddl_quartiere3" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" bgcolor="#99DF8C" style="height: 10px">
                                                                    <asp:Label ID="Label50" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#99DF8C" style="height: 10px;">
                                                                    <asp:DropDownList ID="ddl_complesso1" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#A6E49C" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600;">
                                                                    <asp:Label ID="Label51" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#A6E49C" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_complesso2" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#B3E6AA" style="height: 10px">
                                                                    <asp:Label ID="Label52" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#B3E6AA" style="height: 10px">
                                                                    <asp:DropDownList ID="ddl_complesso3" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" bgcolor="#99DF8C" style="height: 10px">
                                                                    <asp:Label ID="Label53" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#99DF8C" style="height: 10px;">
                                                                    <asp:DropDownList ID="ddl_edificio1" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#A6E49C" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600;">
                                                                    <asp:Label ID="Label54" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#A6E49C" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_edificio2" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#B3E6AA" style="height: 10px">
                                                                    <asp:Label ID="Label55" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#B3E6AA" style="height: 10px">
                                                                    <asp:DropDownList ID="ddl_edificio3" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" bgcolor="#99DF8C" style="height: 10px">
                                                                    <asp:Label ID="Label62" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#99DF8C" style="height: 10px;">
                                                                    <asp:DropDownList ID="ddl_indirizzo1" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#A6E49C" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600;">
                                                                    <asp:Label ID="Label63" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#A6E49C" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_indirizzo2" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#B3E6AA" style="height: 10px">
                                                                    <asp:Label ID="Label64" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#B3E6AA" style="height: 10px">
                                                                    <asp:DropDownList ID="ddl_indirizzo3" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style="height: 40px">
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%; height: 185px" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="border-width: 1px; border-style: solid; border-color: #996600; height: 10px;"
                                                                    bgcolor="#C2FFA6" width="300px" align="center" colspan="2">
                                                                    <asp:Label ID="Label11" runat="server" Font-Names="Arial" Font-Size="8pt" Text="4° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="border-style: solid solid solid none; border-color: #996600 #996600 #996600 #FFFFFF;
                                                                    height: 10px; border-bottom-width: 1px; border-right-width: 1px; border-top-width: 1px;"
                                                                    align="center" bgcolor="#D7FFC4" colspan="2" width="300px">
                                                                    <asp:Label ID="Label12" runat="server" Font-Names="Arial" Font-Size="8pt" Text="5° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    bgcolor="#C2FFA6" width="300px" align="center">
                                                                    <asp:Label ID="Label23" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#C2FFA6" width="300px">
                                                                    <asp:DropDownList ID="ddl_localita4" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#D7FFC4" width="300px">
                                                                    <asp:Label ID="Label24" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#D7FFC4" width="300px">
                                                                    <asp:DropDownList ID="ddl_localita5" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    bgcolor="#C2FFA6" align="center" width="300px">
                                                                    <asp:Label ID="Label68" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#C2FFA6" width="300px">
                                                                    <asp:DropDownList ID="ddl_zona4" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Font-Size="8pt" AutoPostBack="True" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#D7FFC4" width="300px">
                                                                    <asp:Label ID="Label69" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#D7FFC4" width="300px">
                                                                    <asp:DropDownList ID="ddl_zona5" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; z-index: 118; left: 475px; border-left: black 1px solid;
                                                                        border-bottom: black 1px solid; position: static; top: 101px" TabIndex="1" Font-Size="8pt"
                                                                        Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    bgcolor="#C2FFA6" align="center" width="300px">
                                                                    <asp:Label ID="Label71" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#C2FFA6" width="300px">
                                                                    <asp:DropDownList ID="ddl_quartiere4" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#D7FFC4" width="300px">
                                                                    <asp:Label ID="Label72" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#D7FFC4" width="300px">
                                                                    <asp:DropDownList ID="ddl_quartiere5" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="center" bgcolor="#C2FFA6" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label74" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#C2FFA6" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_complesso4" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#D7FFC4" style="height: 10px" width="300px">
                                                                    <asp:Label ID="Label75" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#D7FFC4" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_complesso5" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" style="height: 10px" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="center" bgcolor="#C2FFA6" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label77" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#C2FFA6" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_edificio4" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#D7FFC4" style="height: 10px" width="300px">
                                                                    <asp:Label ID="Label78" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#D7FFC4" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_edificio5" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" style="height: 10px" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600">
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="center" bgcolor="#C2FFA6" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600; border-bottom-style: solid;
                                                                    border-bottom-width: 1px; border-bottom-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label80" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#C2FFA6" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600; border-bottom-style: solid;
                                                                    border-bottom-width: 1px; border-bottom-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_indirizzo4" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#D7FFC4" style="border-bottom-style: solid; border-bottom-width: 1px;
                                                                    border-bottom-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label81" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#D7FFC4" style="border-right-style: solid; border-right-width: 1px;
                                                                    border-right-color: #996600; border-bottom-style: solid; border-bottom-width: 1px;
                                                                    border-bottom-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_indirizzo5" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr valign="bottom">
                                <td style="width: 60%; height: 50px;">
                                </td>
                                <td style="width: 17%;">
                                    &nbsp;
                                </td>
                                <td style="width: 15%;">
                                    <asp:ImageButton ID="btnAvanti1" runat="server" ImageUrl="../NuoveImm/Img_Avanti.png"
                                        ToolTip="Avanti" TabIndex="10" OnClientClick="document.getElementById('H1').value = 0;" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnEsci1" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                        OnClientClick="ConfermaEsci();" ToolTip="Esci" TabIndex="11" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <table width="99%" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td valign="middle" align="center" style="height: 30px">
                                    <asp:Label ID="Label26" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Posizioni geografiche escluse"
                                        Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:UpdatePanel ID="UpdatePanelPosEscl" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table style="border-style: solid; border-width: 1px; border-color: #996600; width: 100%;
                                                            height: 185px" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                                                    height: 10px; border-bottom-width: 1px; border-right-width: 1px;" bgcolor="#77BBFF"
                                                                    width="15%" align="center" colspan="2">
                                                                    <asp:Label ID="Label35" runat="server" Font-Names="Arial" Font-Size="8pt" Text="1° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="border-style: none solid solid none; border-color: #FFFFFF #996600 #996600 #FFFFFF;
                                                                    height: 10px; border-bottom-width: 1px; border-right-width: 1px;" align="center"
                                                                    bgcolor="#8CC6FF" colspan="2">
                                                                    <asp:Label ID="Label36" runat="server" Font-Names="Arial" Font-Size="8pt" Text="2° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-bottom-width: 1px; border-top-style: none; border-bottom-style: solid;
                                                                    border-left-style: none; border-top-color: #FFFFFF; border-bottom-color: #996600;
                                                                    border-left-color: #FFFFFF;" align="center" bgcolor="#9BCDFF" colspan="2">
                                                                    <asp:Label ID="Label38" runat="server" Font-Names="Arial" Font-Size="8pt" Text="3° Opzione"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" bgcolor="#77BBFF" align="center" width="8%">
                                                                    <asp:Label ID="Label14" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#77BBFF" width="24%">
                                                                    <asp:DropDownList ID="ddl_localita1ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#8CC6FF">
                                                                    <asp:Label ID="Label15" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#8CC6FF">
                                                                    <asp:DropDownList ID="ddl_localita2ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#9BCDFF">
                                                                    <asp:Label ID="Label16" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#9BCDFF">
                                                                    <asp:DropDownList ID="ddl_localita3ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" bgcolor="#77BBFF" align="center">
                                                                    <asp:Label ID="Label17" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#77BBFF">
                                                                    <asp:DropDownList ID="ddl_zona1ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; z-index: 118; left: 475px; border-left: black 1px solid;
                                                                        border-bottom: black 1px solid; position: static; top: 101px" TabIndex="1" Font-Size="8pt"
                                                                        AutoPostBack="True" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#8CC6FF">
                                                                    <asp:Label ID="Label18" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#8CC6FF">
                                                                    <asp:DropDownList ID="ddl_zona2ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; z-index: 118; left: 475px; border-left: black 1px solid;
                                                                        border-bottom: black 1px solid; position: static; top: 101px" TabIndex="1" Font-Size="8pt"
                                                                        AutoPostBack="True" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#9BCDFF">
                                                                    <asp:Label ID="Label19" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#9BCDFF">
                                                                    <asp:DropDownList ID="ddl_zona3ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; z-index: 118; left: 475px; border-left: black 1px solid;
                                                                        border-bottom: black 1px solid; position: static; top: 101px" TabIndex="1" Font-Size="8pt"
                                                                        AutoPostBack="True" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" bgcolor="#77BBFF" align="center">
                                                                    <asp:Label ID="Label20" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#77BBFF">
                                                                    <asp:DropDownList ID="ddl_quartiere1ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#8CC6FF">
                                                                    <asp:Label ID="Label21" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#8CC6FF">
                                                                    <asp:DropDownList ID="ddl_quartiere2ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#9BCDFF">
                                                                    <asp:Label ID="Label22" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#9BCDFF">
                                                                    <asp:DropDownList ID="ddl_quartiere3ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" bgcolor="#77BBFF" style="height: 10px">
                                                                    <asp:Label ID="Label56" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#77BBFF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_complesso1ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#8CC6FF" style="height: 10px">
                                                                    <asp:Label ID="Label57" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#8CC6FF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_complesso2ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#9BCDFF" style="height: 10px">
                                                                    <asp:Label ID="Label58" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#9BCDFF" style="height: 10px">
                                                                    <asp:DropDownList ID="ddl_complesso3ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" bgcolor="#77BBFF" style="height: 10px">
                                                                    <asp:Label ID="Label59" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#77BBFF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_edificio1ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#8CC6FF" style="height: 10px">
                                                                    <asp:Label ID="Label60" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#8CC6FF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_edificio2ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#9BCDFF" style="height: 10px">
                                                                    <asp:Label ID="Label61" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#9BCDFF" style="height: 10px">
                                                                    <asp:DropDownList ID="ddl_edificio3ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="center" bgcolor="#77BBFF" style="height: 10px">
                                                                    <asp:Label ID="Label65" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#77BBFF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_indirizzo1ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#8CC6FF" style="height: 10px">
                                                                    <asp:Label ID="Label66" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#8CC6FF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;">
                                                                    <asp:DropDownList ID="ddl_indirizzo2ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#9BCDFF" style="height: 10px">
                                                                    <asp:Label ID="Label67" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#9BCDFF" style="height: 10px">
                                                                    <asp:DropDownList ID="ddl_indirizzo3ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style="height: 40px">
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%; height: 185px" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="border-width: 1px; border-style: solid; border-color: #996600; height: 10px;"
                                                                    bgcolor="#BBDDFF" width="300px" align="center" colspan="2">
                                                                    <asp:Label ID="Label13" runat="server" Font-Names="Arial" Font-Size="8pt" Text="4° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="border-style: solid solid solid none; border-color: #996600 #996600 #996600 #FFFFFF;
                                                                    height: 10px; border-bottom-width: 1px; border-right-width: 1px; border-top-width: 1px;"
                                                                    align="center" bgcolor="#CCE6FF" colspan="2" width="300px">
                                                                    <asp:Label ID="Label25" runat="server" Font-Names="Arial" Font-Size="8pt" Text="5° Opzione"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    bgcolor="#BBDDFF" width="300px" align="center">
                                                                    <asp:Label ID="Label70" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#BBDDFF" width="300px">
                                                                    <asp:DropDownList ID="ddl_localita4ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#CCE6FF" width="300px">
                                                                    <asp:Label ID="Label73" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Località"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#CCE6FF" width="300px">
                                                                    <asp:DropDownList ID="ddl_localita5ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    bgcolor="#BBDDFF" align="center" width="300px">
                                                                    <asp:Label ID="Label76" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#BBDDFF" width="300px">
                                                                    <asp:DropDownList ID="ddl_zona4ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Font-Size="8pt" AutoPostBack="True" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#CCE6FF" width="300px">
                                                                    <asp:Label ID="Label79" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Zona"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#CCE6FF" width="300px">
                                                                    <asp:DropDownList ID="ddl_zona5ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; z-index: 118; left: 475px; border-left: black 1px solid;
                                                                        border-bottom: black 1px solid; position: static; top: 101px" TabIndex="1" Font-Size="8pt"
                                                                        Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px; border-left-style: solid; border-left-width: 1px; border-left-color: #996600;"
                                                                    bgcolor="#BBDDFF" align="center" width="300px">
                                                                    <asp:Label ID="Label82" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#BBDDFF" width="300px">
                                                                    <asp:DropDownList ID="ddl_quartiere4ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" bgcolor="#CCE6FF" width="300px">
                                                                    <asp:Label ID="Label83" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Quartiere"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px; border-right-style: solid; border-right-width: 1px; border-right-color: #996600;"
                                                                    align="center" bgcolor="#CCE6FF" width="300px">
                                                                    <asp:DropDownList ID="ddl_quartiere5ex" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                        border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                        TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="center" bgcolor="#BBDDFF" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label84" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#BBDDFF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_complesso4ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#CCE6FF" style="height: 10px" width="300px">
                                                                    <asp:Label ID="Label85" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Complesso"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#CCE6FF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_complesso5ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" style="height: 10px" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="center" bgcolor="#BBDDFF" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label86" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#BBDDFF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_edificio4ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#CCE6FF" style="height: 10px" width="300px">
                                                                    <asp:Label ID="Label87" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Edificio"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#CCE6FF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_edificio5ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" style="height: 10px" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr style="border-bottom-style: solid; border-bottom-width: 1px; border-bottom-color: #996600">
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                                <td align="center" bgcolor="#BBDDFF" style="height: 10px; border-left-style: solid;
                                                                    border-left-width: 1px; border-left-color: #996600; border-bottom-style: solid;
                                                                    border-bottom-width: 1px; border-bottom-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label88" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#BBDDFF" style="height: 10px; border-right-style: solid;
                                                                    border-right-width: 1px; border-right-color: #996600; border-bottom-style: solid;
                                                                    border-bottom-width: 1px; border-bottom-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_indirizzo4ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="center" bgcolor="#CCE6FF" style="border-bottom-style: solid; border-bottom-width: 1px;
                                                                    border-bottom-color: #996600;" width="300px">
                                                                    <asp:Label ID="Label89" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Indirizzo"></asp:Label>
                                                                </td>
                                                                <td align="center" bgcolor="#CCE6FF" style="border-right-style: solid; border-right-width: 1px;
                                                                    border-right-color: #996600; border-bottom-style: solid; border-bottom-width: 1px;
                                                                    border-bottom-color: #996600;" width="300px">
                                                                    <asp:DropDownList ID="ddl_indirizzo5ex" runat="server" AutoPostBack="True" Font-Size="8pt"
                                                                        Height="20px" Style="border-right: black 1px solid; border-top: black 1px solid;
                                                                        border-left: black 1px solid; border-bottom: black 1px solid;" TabIndex="1" Width="140px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="height: 10px" align="center" width="15%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table cellpadding="0" cellspacing="0" style="width: 100%;">
                            <tr valign="bottom">
                                <td style="width: 60%; height: 50px;" valign="bottom">
                                </td>
                                <td style="width: 17%;">
                                    <asp:ImageButton ID="btnIndietro2" runat="server" ImageUrl="../NuoveImm/Img_Indietro2.png"
                                        TabIndex="9" ToolTip="Salva" />
                                </td>
                                <td style="width: 15%;">
                                    <asp:ImageButton ID="btnAvanti2" runat="server" ImageUrl="../NuoveImm/Img_Avanti.png"
                                        TabIndex="13" OnClientClick="document.getElementById('H1').value = 0;" ToolTip="Avanti" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnEsci2" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                        OnClientClick="ConfermaEsci();" TabIndex="11" ToolTip="Esci" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
        <asp:View ID="View3" runat="server">
            <table width="99%" cellpadding="0" cellspacing="0" style="height: 430;">
                <tr>
                    <td>
                        <table width="100%">
                            <tr>
                                <td valign="middle" align="center" style="height: 30px;">
                                    <asp:Label ID="Label27" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Piani di preferenza"
                                        Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td valign="middle" height="97px">
                                    <asp:UpdatePanel ID="UpdatePanelDatiUI" runat="server">
                                        <ContentTemplate>
                                            <table style="width: 100%;" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <table style="width: 100%;" cellpadding="0" cellspacing="0" bgcolor="#DADADA">
                                                            <tr>
                                                                <td>
                                                                    <table style="border: 1px solid #996600; width: 100%; height: 60px;" cellpadding="0"
                                                                        cellspacing="0">
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp
                                                                            </td>
                                                                            <td style="height: 10px" width="25%">
                                                                                <asp:Label ID="Label99" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Piani di preferenza CON ascensore"
                                                                                    Width="192px"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 10px" align="center" width="5%">
                                                                                <asp:Label ID="Label31" runat="server" Font-Names="Arial" Font-Size="8pt" Text="da"
                                                                                    Width="20px"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 10px" align="center" width="20%">
                                                                                <asp:DropDownList ID="ddl_pianodaCon" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                                    border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                                    TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="height: 10px" align="center" width="5%">
                                                                                <asp:Label ID="Label34" runat="server" Font-Names="Arial" Font-Size="8pt" Text="a"
                                                                                    Width="20px"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 10px" align="center" width="20%">
                                                                                <asp:DropDownList ID="ddl_pianoaCon" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                                    border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                                    TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td align="center" style="height: 10px" width="10%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td width="1%">
                                                                                &nbsp
                                                                            </td>
                                                                            <td style="height: 10px" width="25%">
                                                                                <asp:Label ID="Label102" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Piani di preferenza SENZA ascensore"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 10px" align="center" width="5%">
                                                                                <asp:Label ID="Label39" runat="server" Font-Names="Arial" Font-Size="8pt" Text="da"
                                                                                    Width="20px"></asp:Label>
                                                                            </td>
                                                                            <td align="center" width="20%">
                                                                                <asp:DropDownList ID="ddl_pianodaSenza" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                                    border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                                    TabIndex="1" Font-Size="8pt" AutoPostBack="True" Width="140px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td style="height: 10px" align="center" width="5%">
                                                                                <asp:Label ID="Label41" runat="server" Font-Names="Arial" Font-Size="8pt" Text="a"
                                                                                    Width="20px"></asp:Label>
                                                                            </td>
                                                                            <td style="height: 10px" align="center" width="20%">
                                                                                <asp:DropDownList ID="ddl_pianoaSenza" runat="server" Height="20px" Style="border-right: black 1px solid;
                                                                                    border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;"
                                                                                    TabIndex="1" Width="140px" AutoPostBack="True" Font-Size="8pt">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                            <td align="center" style="height: 10px" width="10%">
                                                                                &nbsp;
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="height: 30px">
                                                    <td valign="middle" align="center">
                                                        <asp:Label ID="Label94" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Dati Unità Immobiliare"
                                                            Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table style="border: 1px solid #996600; width: 100%; height: 110px" cellpadding="0"
                                                            cellspacing="0" bgcolor="#FDD68C">
                                                            <tr>
                                                                <td style="width: 1%;">
                                                                </td>
                                                                <td style="height: 10px; width: 24%;">
                                                                    <asp:Label ID="Label28" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Superficie Minima"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px; width: 20%;">
                                                                    <asp:TextBox ID="txt_supMin" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                                        MaxLength="9" Width="49px"></asp:TextBox>
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px;">
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                </td>
                                                                <td style="height: 10px; width: 3%;">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="style2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 1%;">
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    <asp:Label ID="Label40" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Superficie Massima"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    <asp:TextBox ID="txt_supMax" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                                                        MaxLength="9" Width="49px"></asp:TextBox>
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px;">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="style2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px">
                                                                </td>
                                                                <td style="height: 10px">
                                                                    <asp:Label ID="Label37" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Escluso Condominio"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    <asp:CheckBox ID="chk_condominio" runat="server" Font-Names="arial" Font-Size="8pt"
                                                                        TabIndex="5" />
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="style2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px">
                                                                </td>
                                                                <td style="height: 10px">
                                                                    <asp:Label ID="Label29" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Senza Barr. Architettoniche"></asp:Label>
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    <asp:CheckBox ID="chk_barr" runat="server" Font-Names="arial" Font-Size="8pt" TabIndex="5" />
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td style="height: 10px">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="style2">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td height="25px" valign="middle" align="center">
                                    <asp:Label ID="Label30" runat="server" Font-Names="Arial" Font-Size="10pt" Text="Note"
                                        Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="border: 1px solid #996600;" valign="middle" height="70px">
                                    <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0" bgcolor="#FFFFCC">
                                        <tr>
                                            <td style="width: 1%;">
                                            </td>
                                            <td style="height: 40px" align="center" valign="middle">
                                                <asp:TextBox ID="txt_note" runat="server" Height="50px" TextMode="MultiLine" Width="740px"
                                                    TabIndex="9"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td height="70px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr valign="bottom">
                    <td>
                        <table style="width: 100%;" cellpadding="0" cellspacing="0">
                            <tr valign="bottom">
                                <td style="width: 40%; height: 50px;">
                                </td>
                                <td style="width: 13%;">
                                    &nbsp;
                                </td>
                                <td style="width: 10%;">
                                    <asp:ImageButton ID="btnIndietro3" runat="server" ImageUrl="../NuoveImm/Img_Indietro2.png"
                                        ToolTip="Salva" TabIndex="9" />
                                </td>
                                <td style="width: 20%;">
                                    <asp:ImageButton ID="btn_stampa" runat="server" OnClientClick="ApriStampa();return false;"
                                        ImageUrl="~/NuoveImm/Img_StampaContratto.png" ToolTip="Stampa" TabIndex="10" />
                                </td>
                                <td style="width: 50%;">
                                    <asp:ImageButton ID="btn_salva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                        ToolTip="Salva" TabIndex="18" OnClientClick="document.getElementById('H1').value = 0;" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btn_annulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                                        OnClientClick="ConfermaEsci();" ToolTip="Esci" TabIndex="11" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="txt_tipo" runat="server" />
    <asp:HiddenField ID="txt_idDomanda" runat="server" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" />
    <asp:HiddenField ID="Uscita" runat="server" Value="0" />
    <asp:HiddenField ID="H1" runat="server" Value="0" />
    <asp:Button ID="EsciNas" runat="server" Text="Button" Style="display: none;" />
   
    </form>
</body>
 <script language="javascript" type="text/javascript">
     if (document.getElementById('caricamento')) {
         document.getElementById('caricamento').style.visibility = 'hidden';
     }

     function ApriStampa() {


         if (document.getElementById('frmModify').value != '1') {

             window.open('Doc_Preferenze/SchedaPreferenze.aspx?TIPO=' + document.getElementById('txt_tipo').value + '&PROV=0&IDDOMANDA=' + document.getElementById('txt_idDomanda').value, 'DocPreferenze', 'resizable=yes');

         } else {


             alert('Salvare le modifiche effettuate!');

         }




     }

     function ConfermaEsci() {

         if (document.getElementById('frmModify').value == '1') {
             var chiediConferma
             chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
             if (chiediConferma == true) {
                 document.getElementById('H1').value = 0;
                 document.getElementById('Uscita').value = 1;
                 //                    self.close();
             } else {

                 document.getElementById('H1').value = 0;
                 document.getElementById('Uscita').value = 0;

             }
         }
         else {
             document.getElementById('H1').value = 0;
             document.getElementById('Uscita').value = 1;
             //                self.close();
         }
     }




     //        initialize();
     //        function initialize() {
     //            if (document.getElementById('caricamento') != null) {
     //                document.getElementById('caricamento').style.visibility = 'hidden';

     //            }
     //        }

     

    </script>
</html>
