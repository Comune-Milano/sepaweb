<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaLotti.aspx.vb" Inherits="MANUTENZIONI_RicercaLotti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>RICERCA</title>
</head>
<body class="sfondo">
    <script type="text/javascript">  
     
    </script>
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">
                    Gestione - Gestione lotti - Ricerca
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia ricerca" ToolTip="Avvia ricerca"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" 
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%">
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="110px">Esercizio Finanziario</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbesercizio" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="110px">Struttura</asp:Label>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="cmbfiliale" runat="server" AppendDataBoundItems="true" AutoPostBack="false"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                            </td>
                            <td style="height: 40px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Width="110px">Tipo Lotto</asp:Label>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RBL1" runat="server" AutoPostBack="True" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" RepeatDirection="Horizontal" Width="300px">
                                    <asp:ListItem Selected="True" Value="E">Edifici</asp:ListItem>
                                    <asp:ListItem Value="I">Impianti</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Height="14px" Width="110px">Edificio o Impianto</asp:Label>
                            </td>
                            <td>
                               
                                <telerik:RadComboBox ID="cmbcomplesso" runat="server" 
                                    AppendDataBoundItems="true" AutoPostBack="false"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    Height="14px" Width="110px">Tipo Servizi</asp:Label>
                            </td>
                            <td>
                                
                                <telerik:RadComboBox ID="cmbservizi" runat="server" 
                                    AppendDataBoundItems="true" AutoPostBack="false"
                                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="40%">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%;">
                            </td>
                            <td style="height: 120px">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%">
                            </td>
                            <td>
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label Style="z-index: 10; left: 16px; top: 489px" Font-Bold="True" Font-Names="Arial"
                        Font-Size="8pt" ForeColor="Red" ID="lblErrore" runat="server" Text="Label" Visible="False"
                        Width="535px" /><br />
                </td>
            </tr>
            <tr>
                <td style="width: 670px; height: 21px;">
                    &nbsp;
                </td>
            </tr>
        </table>
        &nbsp;&nbsp;
    </div>
    <!--
    
       <div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;
     font:verdana; font-size:10px;'><br><img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso...">
       </div>
       -->
    </form>
</body>
</html>
