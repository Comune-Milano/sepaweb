<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRRS_D.aspx.vb" Inherits="RRS_RicercaRRS_D" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="Funzioni.js">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<script language="javascript" type="text/javascript">

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


</script>
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>RICERCA</title>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div class="FontTelerik">
            &nbsp;
        <table style="width: 100%;">
            <tr>
                <td class="TitoloModulo">Ordini - Gestione non patrimoniale - Ricerca - Diretta
                </td>
            </tr>
            <tr>
                <td style="width: 100%">
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca" />
                            </td>
                            <td>
                                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="overflow: auto;">
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                        Width="130px">Esercizio Finanziario</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="300px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">
                                    <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                        Width="130px">Num. Repertorio</asp:Label>
                                </td>
                                <td style="height: 20px">
                                    <asp:TextBox ID="txtNumRepertorio" runat="server" MaxLength="15" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                        TabIndex="4" Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="LblComplesso" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                        Width="130px">ODL</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtODL" runat="server" MaxLength="15" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                        TabIndex="4" Width="100px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtODL"
                                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                        Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblAnno" runat="server" Style="z-index: 100; left: 48px; top: 64px"
                                        Width="130px">Anno</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAnno" runat="server" MaxLength="15" Style="z-index: 102; left: 144px; top: 224px; text-align: right"
                                        TabIndex="4" Width="100px"></asp:TextBox>
                                    <asp:RegularExpressionValidator
                                        ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAnno" Display="Dynamic"
                                        ErrorMessage="max 4 Numeri" Font-Names="arial" Font-Size="8pt" TabIndex="303"
                                        ValidationExpression="\d+" Width="176px"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Style="z-index: 100; left: 80px; top: 288px"
                                        Width="130px">Autorizzazione</asp:Label>
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="DropDownListAutorizzazione" runat="server" AppendDataBoundItems="true"
                                        AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                        ResolvedRenderMode="Classic" Width="90%">
                                        <Items>
                                            <telerik:RadComboBoxItem Value="-1" Text="Tutti" Selected="true" />
                                            <telerik:RadComboBoxItem Value="0" Text="Non autorizzati" />
                                            <telerik:RadComboBoxItem Value="1" Text="Autorizzati" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="2" cellspacing="2">
                        <tr>
                            <td style="vertical-align: middle">
                                <img src="../../../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                            </td>
                            <td style="vertical-align: middle">
                                <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Utilizzare <b>*</b> come carattere jolly nelle ricerche</i></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
