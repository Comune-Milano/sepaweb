<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiDatiChiusura.aspx.vb"
    Inherits="Condomini_RisultatiDatiChiusura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Risultati dati chiusura</title>
    <script type="text/javascript">


        var Selezionato;
        function ApriComSloggio() {
            window.showModalDialog('ComunicazSloggio.aspx?ID=' + document.getElementById('txtid').value + '&IDCONT=' + document.getElementById('idContratto').value + '&IDAVVISO=' + document.getElementById('idAvviso').value, 'window', 'status:no;dialogWidth:500px;dialogHeight:300px;dialogHide:true;help:no;scroll:no');
        }

    </script>
    <style type="text/css">
        #form1
        {
            width: 785px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat;">
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 100%; height: 95%; vertical-align: top;
        line-height: normal; top: 22px; left: 10px; background-color: #FFFFFF; visibility: visible;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='Immagini/load.gif' alt='caricamento in corso' /><br />
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
    <form id="form1" runat="server">
    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Elenco dei
        dati per Chiusura R.U. </span></strong>
    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
        Font-Bold="True" Font-Names="Arial" Font-Size="10pt" MaxLength="100" ReadOnly="True"
        Style="z-index: 2; left: 13px; position: absolute; top: 528px" Width="632px">Nessuna Selezione</asp:TextBox>
    <table style="width: 99%;">
        <tr>
            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                &nbsp;</td>
            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 3px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 8pt">
                        <asp:RadioButtonList ID="rdbFiltro" runat="server" AutoPostBack="True" 
                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" 
                            RepeatDirection="Horizontal">
                            <asp:ListItem Value="S">DA SOLLECITARE</asp:ListItem>
                            <asp:ListItem Value="T">TUTTI</asp:ListItem>
                        </asp:RadioButtonList>
            </td>
            <td style="font-family: Arial, Helvetica, sans-serif; font-size: 3px">
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <div style="left: 582px; overflow: auto; width: 100%; height: 446px">
                        <asp:DataGrid ID="DataGridChiusi" runat="server" AutoGenerateColumns="False" BackColor="White"
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                            PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="99%"
                            CellPadding="1" CellSpacing="1">
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle ForeColor="Black" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_COND_AVVISO" HeaderText="ID_COND_AVVISO" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="INTE" HeaderText="INQUILINO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CONDOMINIO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_SLOGGIO" HeaderText="DATA SLOGGIO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_INVIO_AMM" HeaderText="DATA INVIO AMM."></asp:BoundColumn>
                                <asp:BoundColumn DataField="DEBITO" HeaderText="DEBITO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CREDITO" HeaderText="CREDITO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SCADUTA" HeaderText="SCADUTA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DA_SOLLECITARE" HeaderText="DA SOLLECITARE">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                        </asp:DataGrid>
                    </div>
                </span></strong>
            </td>
            <td style="vertical-align: top; text-align: left">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:ImageButton ID="btnVisualizza" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_VisualizzaPiccolo.png"
                                    ToolTip="Visualizza dettaglio" OnClientClick ="ApriComSloggio();return false;"  /> </strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: right" colspan="2">
                <table style="width:100%;">
                    <tr>
                        <td style="text-align: left">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Condomini/Immagini/Img_Export_Grande.png"
         ToolTip="Esporta in Excel" />
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                <asp:ImageButton 
        ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                ToolTip="Home" />

    </span></strong>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    &nbsp;<strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">&nbsp;<asp:HiddenField ID="txtid" runat="server" Value="0" />
     <asp:HiddenField ID="idAvviso" runat="server" Value="0" />
          <asp:HiddenField ID="idContratto" runat="server" Value="0" />

    </span></strong>

        <script language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility = 'hidden';
            document.getElementById('splash').style.visibility = 'hidden';

    </script>

    </form>
</body>
</html>
