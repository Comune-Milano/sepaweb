<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettRiclassificate.aspx.vb"
    Inherits="Contratti_Pagamenti_DettRiclassificate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bollette Riclassificate</title>
    <style type="text/css">
        .style1
        {
            font-family: ARIAL;
            font-weight: bold;
            font-size: 12pt;
            color: black;
            text-align: center;
        }
        .style2
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 8pt;
            text-align: center;
        }
        
        .style3
        {
            border-right-style: solid;
            border-right-width: 1px;
            border-right-color: #AA3700;
            font-family: Arial;
            font-weight: bold;
            color: white;
            background-color: #507CD1;
            font-size: 8pt;
            text-align: center;
        }
        .style4
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
        }
    </style>
    <script type="text/javascript" language="javascript">
        window.name = "modal";
    </script>
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <table width="100%">
        <tr>
            <td class="style1">
                RIPARTIZIONE PAGAMENTO RICLASSIFICATE
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="center">
                <table id="tblAutomatica" runat="server" class="style2" style="border: 1px solid #507CD1;">
                    <tr>
                        <td>
                            PAGAMENTO €
                        </td>
                        <td>
                            <b>
                                <asp:TextBox ID="txtPagResoconto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    MaxLength="20" Width="80px" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                            </b>
                        </td>
                        <td>
                            SELEZIONE.€.
                        </td>
                        <td>
                            <b>
                                <asp:TextBox ID="txtSommaSel" runat="server" Font-Names="Arial" Font-Size="8pt" MaxLength="20"
                                    Width="80px" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                            </b>
                        </td>
                        <td>
                            CREDITO€.
                        </td>
                        <td>
                            <b>
                                <asp:TextBox ID="txtResResoconto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                    MaxLength="20" Width="80px" Style="text-align: right" ReadOnly="True"></asp:TextBox>
                            </b>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td class="style4" style="padding-left: 10px;">
                Info bolletta di morosità
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <asp:DataGrid ID="DataGridMorosita" runat="server" AutoGenerateColumns="False" BackColor="#FFFFBF"
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="1" Style="z-index: 105;
                    left: 193px; top: 54px" Width="100%" BorderColor="Gray" BorderWidth="0px" GridLines="None">
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUM.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_TIPOBOLL" HeaderText="ID_TIPOBOLL" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPOBOLL" HeaderText="TIPO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="N_RATA" HeaderText="N.RATA" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RIFERIMENTO" HeaderText="RIFERIMENTO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_TOTALE" HeaderText="IMPORTO €.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO_PAGATO" HeaderText="PAGATO €.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RESIDUO" HeaderText="RESIDUO €.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#990000" />
                </asp:DataGrid>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td class="style4" style="padding-left: 10px;">
                Elenco bollette riclassificate
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <div style="width: 910px;">
                    <table style="width: 100%;">
                        <tr>
                            <td nowrap="nowrap" class="style3" width="10%">
                                NUM. BOLL.
                            </td>
                            <td nowrap="nowrap" class="style3" width="7%">
                                TIPO
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                PERIODO DAL
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                PERIODO AL
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                IMPORTO €
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                PAGATO €
                            </td>
                            <td nowrap="nowrap" class="style3" width="10%">
                                RESIDUO €
                            </td>
                            <td nowrap="nowrap" class="style3" width="11%">
                                DATA EMISS.
                            </td>
                            <td nowrap="nowrap" class="style3" width="11%">
                                DATA SCAD.
                            </td>
                            <td nowrap="nowrap" class="style3" width="40%">
                                DATA PAGAM.
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <div style="width: 910px; height: 320px; overflow: auto;">
                    <asp:DataGrid ID="DgvBolRiclass" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        PageSize="15" Style="border-collapse: separate" Width="100%" CellPadding="1"
                        CellSpacing="1" ShowHeader="false">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn HeaderText="ID" Visible="False" DataField="ID"></asp:BoundColumn>
                            <asp:BoundColumn DataField="NUM_BOLLETTA" HeaderText="NUM_BOLLETTA">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="10%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ACRONIMO" HeaderText="TIPO" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Width="7%" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RIFERIMENTO_DA" HeaderText="PERIODO DAL" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RIFERIMENTO_A" HeaderText="PERIODO AL" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_TOTALE1" HeaderText="IMPORTO €" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_PAGATO1" HeaderText="PAGATO €" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="IMPORTO_RESIDUO" HeaderText="RESIDUO €" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_EMISSIONE" HeaderText="DATA EMISSIONE" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="11%" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_SCADENZA" HeaderText="DATA SCADENZA" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="11%" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATAPAG" HeaderText="DATA PAGAMENTO" HeaderStyle-BorderStyle="None">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" Height="0px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Width="11%" Wrap="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#AA3700" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="White" Wrap="False" Height="0px" />
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td style="padding-left: 10px;">
                <asp:TextBox ID="txtmiaBol" runat="server" BorderStyle="None" Font-Bold="True" Font-Names="Arial"
                    Font-Size="10pt" ReadOnly="True" Width="75%" BackColor="#FBFBFB" Font-Italic="True">Nessuna Selezione</asp:TextBox>
                <asp:Label ID="lblNumBoll" runat="server" Font-Names="Arial" Font-Size="10pt" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="../../NuoveImm/Img_Procedi.png"
                                ToolTip="Conferma la selezione, e procedi con il trasferimento" OnClientClick="ConfermaProcedi();document.getElementById('divLoading').style.visibility = 'visible';" />
                            <img id="exit" alt="Esci" src="../../NuoveImm/Img_Esci_AMM.png" title="Esci" style="cursor: pointer"
                                onclick="self.close();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="idContratto" runat="server" Value="0" />
    <asp:HiddenField ID="idBolletta" runat="server" Value="0" />
    <asp:HiddenField ID="idMor" runat="server" Value="0" />
    <asp:HiddenField ID="idRiclass" runat="server" Value="0" />
    <asp:HiddenField ID="numBolletta" runat="server" Value="0" />
    <asp:HiddenField ID="numSelezionati" runat="server" Value="0" />
    <asp:HiddenField ID="dataPagamento" runat="server" Value="0" />
    <asp:HiddenField ID="tipoPagamento" runat="server" Value="0" />
    <asp:HiddenField ID="numAssegno" runat="server" Value="0" />
    <asp:HiddenField ID="notePagam" runat="server" Value="0" />
    <asp:HiddenField ID="dataRegistr" runat="server" Value="0" />
    <asp:HiddenField ID="vIdConnessione" runat="server" Value="0" />
    <asp:HiddenField ID="idIncasso" runat="server" Value="0" />
    <asp:HiddenField ID="inModifica" runat="server" Value="0" />
    <asp:HiddenField ID="idIncEseguito" runat="server" />
    <asp:HiddenField ID="ImportoIncasso" runat="server" Value="0" />
    <asp:HiddenField ID="TotPagato" runat="server" Value="0" />
        <asp:HiddenField ID="oldPagInMor" runat="server" Value="0" />

    </form>
    <script language="javascript" type="text/javascript">
        var Selezionato;

        if (document.getElementById('inModifica').value == '1') {
            document.getElementById('tblAutomatica').style.display = 'none';
        }
        else{
            document.getElementById('tblAutomatica').style.display = 'block';
        }
        if (document.getElementById('divLoading') != null) {
            document.getElementById('divLoading').style.visibility = 'hidden';
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
        function ConfermaProcedi() {
            if (document.getElementById('idBolletta').value != '' && document.getElementById('idBolletta').value != 0) {

                chiediConferma = window.confirm("Sei sicuro di voler procedere con il pagamento della bolletta selezionata?");

                if (chiediConferma == false) {
                    return false;
                } else {

                    window.showModalDialog('DettManualeRiclass.aspx?IDINC=' + document.getElementById('idIncasso').value + '&IDBOL=' + document.getElementById('idBolletta').value + '&IDMOR=' + document.getElementById('idMor').value + '&NUMBOL=' + document.getElementById('numBolletta').value + '&CONN=' + document.getElementById('vIdConnessione').value + '&DATAREG=' + document.getElementById('dataRegistr').value + '&DATAPAG=' + document.getElementById('dataPagamento').value + '&TIPOPAG=' + document.getElementById('tipoPagamento').value + '&ASS=' + document.getElementById('numAssegno').value + '&NOTE=' + document.getElementById('notePagam').value.replace("'", "\'") + '&MODIF=' + document.getElementById('inModifica').value + '&IDINCESEGUITO=' + document.getElementById('idIncEseguito').value + '&IMPORTO=' + document.getElementById('ImportoIncasso').value + '&TOTPAGATO=' + document.getElementById('TotPagato').value, 'windowMan', 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');

                }

            }
            else {
                alert('Impossibile procedere. Selezionare un documento!');
            }
        }
      
    </script>
</body>
</html>
