<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaVoce.aspx.vb" Inherits="PAGAMENTI_SceltaVoce" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <%-- <base target="<%=tipo%>" />--%>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Scelta Voce</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div id="DIV1" class="FontTelerik">
        <table style="left: 0px; top: 0px; width: 100%">
            <tr>
                <td class="TitoloModulo">
                    Ordini - Ordini e Pagamenti - Inserimento
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong></strong>
                    </span>
                    <table style="width: 100%">
                        <tr>
                            <td style="height: 5px">
                                <table>
                                    <tr>
                                        <td>
                                            <telerik:RadButton ID="btnStampa" runat="server" Text="Stampa" ToolTip="Stampa" OnClientClicking="function(sender, args){ConfermaStampa();}" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Esci" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="height: 21px; width: 10%">
                                            <asp:Label ID="Label4" runat="server" 
                                                Style="z-index: 100; left: 48px; top: 32px" Width="110px">Esercizio Finanziario</asp:Label>
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbEsercizio" Width="50%" AppendDataBoundItems="true" Filter="Contains"
                                                Enabled="false" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 10px">
                               
                               
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="cont" style="left: 16px; overflow: scroll; width: 100%; top: 56px; height: 380px;
                                    border-right: blue 1px solid; border-top: blue 1px solid; border-left: blue 1px solid;
                                    border-bottom: blue 1px solid;">
                                    <%=Tabella%>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 8px">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left">
                                <asp:CheckBox ID="ChkStampa" runat="server" Font-Bold="True" 
                                    ForeColor="Black" Text="Stampa con sottovoci" Width="200px" />
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left">
                                <asp:Label ID="lblErrore" runat="server" Visible="False" Style="top: 512px; left: 16px;"
                                    Font-Bold="True" Font-Names="arial" Font-Size="8pt" ForeColor="Red" Width="224px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="idVoce" runat="server" />
                    <asp:HiddenField ID="x" runat="server" Value="0" />
                    <asp:HiddenField ID="txtSTATO_PF" runat="server"></asp:HiddenField>
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                    </strong>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">

        function ConfermaEsci() {
            var chiediConferma;
            chiediConferma = window.confirm("Sei sicuro di voler uscire?");
            if (chiediConferma == true) {
                self.close();
            }
        }


        function ConfermaStampa() {

            if (document.getElementById('ChkStampa').checked == true) {
                //window.open('StampaPF.aspx?STR=0&CHIAMANTE=STAMPA_DETTAGLIO&VOCI=True', 'Stampa', '');
                window.open('StampaPF.aspx?STR=0&CHIAMANTE=STAMPA_DETTAGLIO&VOCI=True&EF_R=<%=vIdPianoFinanziario%>', 'Stampa', '');
            }
            else {
                //window.open('StampaPF.aspx?STR=0&CHIAMANTE=STAMPA_DETTAGLIO&VOCI=False', 'Stampa', '');
                window.open('StampaPF.aspx?STR=0&CHIAMANTE=STAMPA_DETTAGLIO&VOCI=False&EF_R=<%=vIdPianoFinanziario%>', 'Stampa', '');
            }
        }
        
    </script>
</body>
</html>
