<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Parametri.aspx.vb" Inherits="Contratti_ContCalore_Parametri" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 782px;
        }
        .style1
        {
            font-family: Arial;
            font-size: 8pt;
            color: #990000;
        }
    </style>
    <script type="text/javascript">

        var Selezionato;

        function ModificaCalcoloContrCalore() {
            if (document.getElementById('txtidContCalore').value <= 0) alert('Selezionare un elemento dalla lista!');
            else {
                document.getElementById('divVisibile').value = 1;
                document.getElementById('ConfNuovo').value = 0;
            }
        };
        
    </script>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Definizione
                    calcolo Contributo Calore</span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                <strong>Elenco delle operazioni</strong>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="vertical-align: top; text-align: left">
                            <div style="border: thin solid #ccccff; width: 746px; height: 407px; overflow: auto;">
                                <asp:DataGrid ID="dgvContCalore" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                    Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105; left: 8px;
                                    top: 32px" Width="800px">
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="VALORE" HeaderText="VALORE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="AREA" HeaderText="AREA LIMITE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ANNO" HeaderText="ANNO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="TIPO_DIMENSIONI" HeaderText="TIPO DIMENSIONE"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="ARROTONDAMENTO" HeaderText="DEC. ARROTONDAMENTO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                        ForeColor="#0000C0" />
                                    <PagerStyle Mode="NumericPages" />
                                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:DataGrid>
                            </div>
                        </td>
                        <td style="vertical-align: top; text-align: left">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        <img alt="Nuovo Contributo Calore" src="../../Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png"
                                            onclick="document.getElementById('divContCalore').style.visibility = 'visible';document.getElementById('DivVisibile').value = 1;document.getElementById('ConfNuovo').value = 1;document.getElementById('txtidContCalore').value = 0; document.getElementById('txtAnno').value = '';"
                                            style="cursor: pointer" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="../../Condomini/Immagini/pencil-icon.png"
                                            Style="z-index: 102; left: 392px; top: 387px" CausesValidation="False" OnClientClick="ModificaCalcoloContrCalore()"
                                            ToolTip="Modifica Elemento Selezionato" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
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
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                OnClientClick="parent.main.location.replace('../pagina_home.aspx');return false;" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:HiddenField ID="txtModificato" runat="server" />
                            <asp:HiddenField ID="ConfNuovo" runat="server" Value="0" />
                            <asp:HiddenField ID="DivVisibile" runat="server" Value="0" />
                            <asp:HiddenField ID="txtidContCalore" runat="server" Value="0" />
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="divContCalore" style="border: thin none #3366ff; left: 0px; width: 802px;
        position: absolute; top: 0px; height: 582px; background-color: #dedede; vertical-align: top;
        text-align: left; z-index: 201; background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
        margin-right: 10px; visibility: hidden;">
        <table style="width: 100%;">
            <tr>
                <td style="font-family: Arial; font-size: 8pt">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">&nbsp;&nbsp;
                        Definizione di un nuovo Contributo Calore</span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%; font-family: Arial; font-size: 8pt;">
                        <tr>
                            <td>
                                VALORE
                            </td>
                            <td>
                                <asp:TextBox ID="txtValore" runat="server" Font-Names="Arial" Font-Size="8pt" Width="80px"
                                    Style="text-align: right"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                ANNO VALIDITA&#39;
                            </td>
                            <td>
                                &nbsp;<asp:TextBox ID="txtAnno" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="80px" Style="text-align: right" MaxLength="4"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                AREA LIMITE
                            </td>
                            <td colspan="2">
                                <asp:DropDownList ID="cmbArea" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="200px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                N° DECIMALI
                            </td>
                            <td>
                                <asp:TextBox ID="txtNDecimali" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="80px" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td style="text-align: right">
                                MENSILITA&#39;
                            </td>
                            <td>
                                <asp:TextBox ID="txtMensilita" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="40px" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                TIPO DIMENSIONE
                            </td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbTipoDim" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    Width="350px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="2">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td colspan="3">
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                NOTE
                            </td>
                            <td colspan="6">
                                <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="8pt" Width="654px"
                                    Height="77px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: center">
                                <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="../../NuoveImm/Img_SalvaVal.png"
                                    TabIndex="31" ToolTip="Salva" OnClientClick="ConfCreazione();" />
                            </td>
                            <td style="text-align: center">
                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../NuoveImm/Img_AnnullaVal.png"
                                    TabIndex="31" ToolTip="Annulla" OnClientClick="document.getElementById('divContCalore').style.visibility = 'hidden';document.getElementById('DivVisibile').value =0;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
<script language="javascript" type="text/javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\,]/g,
        'onlynumber': /[^\d]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        //        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';
        return true;

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
    function ConfCreazione() {

        if ((document.getElementById("ConfNuovo").value == 1) || (document.getElementById("txtidContCalore").value == 0)) {
            if (window.confirm('Procedere con la definizione dei parametri per il calcolo del contributo calore?')) {
                document.getElementById("ConfNuovo").value = 1;
            }
            else {
                document.getElementById("ConfNuovo").value = 0;
            }
        }



    };
    if (document.getElementById("DivVisibile").value == 1) {
        document.getElementById('divContCalore').style.visibility = 'visible';

    }
    else {
        document.getElementById('divContCalore').style.visibility = 'hidden';

    }

</script>
</html>
