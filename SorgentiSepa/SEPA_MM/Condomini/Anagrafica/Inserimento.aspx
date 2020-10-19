<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Inserimento.aspx.vb" Inherits="Condomini_Anagrafica_Inserimento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inserimento Amministratore</title>
    <style type="text/css">
        .style1
        {
            color: #990000;
            font-family: Arial;
            font-size: 10pt;
        }
        
        .CssMaiuscolo
        {
            text-transform: uppercase;
        }
        
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
        
        .style3
        {
            font-family: Arial;
            font-size: 8pt;
            color: #FF3300;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url('../Immagini/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat;">
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 95%; height: 95%; vertical-align: top;
        line-height: normal; top: 30px; left: 10px; background-color: #FFFFFF;">
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
        <img src='../Immagini/load.gif' alt='caricamento in corso' /><br />
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
    <table style="width: 53%;" class="styleTable">
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="style1">
                            <strong>Dati Anagrafici dell&#39;Amministratore Condominiale</strong>
                        </td>
                        <td class="style2">
                            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                                ToolTip="Salva i dati inseriti" Style="height: 12px" />
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png" OnClientClick="ConfermaEsci();"
                                ToolTip="Esci dalla finestra e torna alla pagina principale" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-family: Arial; font-size: 8pt">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <table style="width: 100%;">
                    <tr>

                        <td class="style2" colspan="4">
                            RAGIONE SOCIALE
                        </td>
                    </tr>
                    <tr>

                        <td class="style2" colspan="4">
                            <asp:TextBox ID="txtRagioneSociale" runat="server" Width="350px" Font-Names="Arial"
                                Font-Size="8pt" CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            COGNOME*
                        </td>
                        <td class="style2">
                            NOME*
                        </td>
                        <td class="style2">
                            COD.FISCALE
                            <asp:Label ID="lblErroreCF" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="#FF3300" Text="ERRORE" Visible="False"></asp:Label>
                        </td>
                        <td class="style2">
                            P.IVA
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:TextBox ID="txtCognome" runat="server" Width="150px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtNome" runat="server" Width="150px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCF" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" MaxLength="16"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtPiva" runat="server" Width="205px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" MaxLength="11"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            INDIRIZZO*
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td class="style2">
                            CIVICO
                        </td>
                        <td class="style2">
                            CAP*
                        </td>
                        <td class="style2">
                            COMUNE
                        </td>
                        <td class="style2">
                            PROV.*
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:DropDownList ID="cmbTipoInd" runat="server" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" Width="80px">
                            </asp:DropDownList>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtIndirizzo" runat="server" Width="300px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCivico" runat="server" Width="50px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCap" runat="server" Width="50px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" MaxLength="5"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:DropDownList ID="cmbComune" runat="server" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" Width="180px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtProvincia" runat="server" Width="40px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            TITOLO
                        </td>
                        <td class="style2">
                            TELEFONO 1
                        </td>
                        <td class="style2">
                            TELEFONO 2
                        </td>
                        <td class="style2">
                            CELLULARE
                        </td>
                        <td class="style2">
                            FAX
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            <asp:TextBox ID="txtTitolo" runat="server" Width="200px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtTelefono1" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtTelefono2" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtCellulare" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                        <td class="style2">
                            <asp:TextBox ID="txtFax" runat="server" Width="100px" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style2">
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            e-mail
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="vertical-align: top">
                            <asp:TextBox ID="txtEmail" runat="server" Width="550px" Font-Names="Arial" Font-Size="8pt"
                                MaxLength="149"></asp:TextBox>
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top" class="style3">
                            <strong>STORICO CONDOMINI AMMINISTRATI</strong>
                        </td>
                        <td class="style2">
                            NOTE
                        </td>
                        <td class="style2">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="vertical-align: top">
                            <div style="border: thin solid #0099FF; overflow: auto; width: 97%; height: 92px;"
                                id="divElenco">
                                <asp:DataGrid ID="DataGridCondAmministr" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="RoyalBlue" Font-Bold="False" Font-Italic="False"
                                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" CssClass="CssMaiuscolo"
                                    Font-Strikeout="False" Font-Underline="False" GridLines="None" PageSize="1" Style="z-index: 105;
                                    left: 8px; top: 32px" Width="97%">
                                    <Columns>
                                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CODNOMINI" ReadOnly="True"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA FINE"></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                        Font-Overline="False" Font-Size="8pt" CssClass="CssMaiuscolo" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="#0000C0" />
                                    <PagerStyle Mode="NumericPages" />
                                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                                </asp:DataGrid>
                            </div>
                        </td>
                        <td style="vertical-align: top">
                            <asp:TextBox ID="txtNote" runat="server" Width="100%" Font-Names="Arial" Font-Size="8pt"
                                CssClass="CssMaiuscolo" Height="92px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="vId" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('splash').style.visibility = 'hidden';

        function ConfermaEsci() {
            document.getElementById('btnEsci').style.visibility = 'hidden';
            if (document.getElementById('txtModificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                }
            }
        }
    </script>
</body>
</html>
