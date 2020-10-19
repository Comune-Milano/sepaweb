<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagamScadenza.aspx.vb" Inherits="Condomini_PagamScadenza" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
    <title>Eventi Patrimoniali</title>
    <style type="text/css">
        #form1
        {
            width: 784px;
        }
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
        }
        .style2
        {
            font-family: Arial;
            font-size: 14pt;
            color: #CC3300;
        }
        .style3
        {
            font-family: Arial;
            font-size: 3pt;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat">
    <script type="text/javascript">


        function AutoDecimal(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(4)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        function AutoDecimal2(obj) {
            if (obj.value.replace(',', '.') > 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        function PaymentConfirm() {
            if (document.getElementById('idGestione').value != 0 && document.getElementById('nRata').value != 0) {
                //document.getElementById('DivDescrizione').style.visibility = 'visible'
                myOpacity.toggle();
                if (document.getElementById('tipoG').value == 'S') {
                    document.getElementById('txtDescrizione').value = 'Rata ' + document.getElementById('nRata').value + ' Periodo Gestione Straordinaria: ' + document.getElementById('Periodo').value + '   con scadenza rata : ' + document.getElementById('txtScadenza').value
                } else {
                    document.getElementById('txtDescrizione').value = 'Rata ' + document.getElementById('nRata').value + ' Periodo Gestione: ' + document.getElementById('Periodo').value + '   con scadenza rata : ' + document.getElementById('txtScadenza').value
                };
                document.getElementById('VisDesc').value = 1
                document.getElementById('txtDScadenza').value = document.getElementById('txtScadenza').value;
                //par.FormattaData(txtScadenza.Value) 

                //var Conferma
                //Conferma = window.confirm("Attenzione...Confermi di voler emettere un pagamento?");
                //if (Conferma == false) {
                //    document.getElementById('txtConferma').value = '0';
                //    //document.getElementById('idGestione').value = '0';
                //    document.getElementById('idCond').value = '0';
                //    document.getElementById('Importo').value = '0';
                //
                //}
                //else {
                //    document.getElementById('txtConferma').value = '1';
                //    document.getElementById('dvvvPre').style.visibility = 'visible';
                //
                //}
            }
            else {
                alert('Selezionare una riga per continuare!');
            }
        }
        function AskConfirm() {
            var Conferma
            Conferma = window.confirm("Attenzione...Confermi di voler emettere un pagamento?");
            if (Conferma == false) {
                document.getElementById('txtConferma').value = '0';
                //document.getElementById('idGestione').value = '0';
                document.getElementById('idCond').value = '0';
                document.getElementById('Importo').value = '0';

            }
            else {
                myOpacity.toggle();
                document.getElementById('VisDesc').value = 0;

                document.getElementById('txtConferma').value = '1';
                document.getElementById('dvvvPre').style.visibility = 'visible';

            }


        };
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
        };
    </script>
    <form id="form1" runat="server">
    <asp:HiddenField ID="txtConferma" runat="server" Value="0" />
    <asp:HiddenField ID="txtNRata" runat="server" Value="0" />
    <asp:HiddenField ID="txtScadenza" runat="server" Value="0" />
    <asp:HiddenField ID="idGestione" runat="server" Value="0" />
    <table style="width: 100%;">
        <tr>
            <td class="style2">
                <strong style="color: #801f1c">Pagamenti in Scadenza:</strong>
            </td>
        </tr>
        <tr>
            <td >
                <table>
                    <tr>
                        <td style="font-weight: 700" class="style3">
                            &nbsp;</td>
                        <td class="style3">
                            &nbsp;</td>
                        <td class="style3">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="font-family: Arial; font-size: 8pt; font-weight: 700">
                            Filtro per Scadenza:</td>
                        <td>
                            <asp:TextBox ID="txtFiltrScad" runat="server" Font-Names="Arial" 
                                Font-Size="8pt" Width="70px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnFiltra" runat="server" 
                                ImageUrl="~/Condomini/Immagini/Search_16x16.png" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div style="left: 11px; overflow: auto; width: 768px; height: 452px">
                    <asp:DataGrid ID="DataGridPagScadenza" runat="server" AutoGenerateColumns="False"
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                        PageSize="20" Style="z-index: 105;" Width="98%" CellPadding="1"
                        CellSpacing="1">
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle ForeColor="Black" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_GESTIONE" HeaderText="ID_GESTIONE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_CONDOMINIO" HeaderText="ID_CONDOMINIO" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="CONDOMINIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="N_RATA" HeaderText="N_RATA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PERIODO" HeaderText="PERIODO" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPO" HeaderText="TIPO" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                    Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                    ReadOnly="True" Width="572px">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td width="10%">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="Immagini/Img_Export_Grande.png"
                                    Style="z-index: 10;" ToolTip="Esporta in Excel" />
                            </span></strong>
                        </td>
                        <td width="60%">
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="Immagini/Img_Stampa.png"
                                    Style="z-index: 10;" ToolTip="Stampa in PDF" />
                            </span></strong>
                        </td>
                        <td>
                            <asp:ImageButton ID="btnPrePagamento" runat="server" ImageUrl="~/Condomini/Immagini/Img_Pagamento.png"
                                OnClientClick="PaymentConfirm();" />
                        </td>
                        <td>
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                                    Style="z-index: 10;" ToolTip="Home" />
                            </span></strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Height="18px" Visible="False" Width="548px"></asp:Label>
                &nbsp;
            </td>
        </tr>
    </table>
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
    </strong>&nbsp;
    <asp:HiddenField ID="VisDesc" runat="server" Value="0" />
    <asp:HiddenField ID="idCond" runat="server" Value="0" />
    <asp:HiddenField ID="Periodo" runat="server" />
    <asp:HiddenField ID="Importo" runat="server" Value="0" />
    <asp:HiddenField ID="nRata" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    <div style="border: thin solid #FF0000; position: absolute; z-index: 10; top: 48px;
        left: 9px; height: 525px; width: 766px; visibility: hidden; background-color: #E8EAEC;"
        id="DivDescrizione">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <table style="width: 100%;">
            <tr>
                <td class="style1">
                    Data Scadenza
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:TextBox ID="txtDScadenza" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    IBAN del Fornitore
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:DropDownList ID="cmbIbanFornitore" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="350px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    Descrizione del pagamento che si sta per emettere
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Height="68px" TextMode="MultiLine" Width="98%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDescrizione"
                                    ErrorMessage="E' possibile inserire al massimo 150 caratteri in questo campo di testo"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ToolTip="E' possibile inserire al massimo 150 caratteri in questo campo di testo!"
                                    ValidationExpression="^[\s\S]{0,150}$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td align="right">
                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                    <asp:ImageButton ID="btnPagamento" runat="server" ImageUrl="~/NuoveImm/Img_Conferma1.png"
                                        ToolTip="Emissione del Pagamento" OnClientClick="AskConfirm();" />
                                </span></strong>
                            </td>
                            <td align="right">
                                <img alt="Annulla" src="../NuoveImm/Img_AnnullaVal.png" onclick="myOpacity.toggle();document.getElementById('VisDesc').value = 0;"
                                    style="cursor: pointer;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField runat="server" ID="tipoG" Value="" />
    </form>
</body>
<script type="text/javascript">
    myOpacity = new fx.Opacity('DivDescrizione', { duration: 200 });
    if (document.getElementById('VisDesc').value == 0) {
        myOpacity.hide();
    }
</script>
</html>
