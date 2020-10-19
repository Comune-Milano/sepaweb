<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPagamentiD.aspx.vb"
    Inherits="PAGAMENTI_RicercaPagamentiD" %>

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
    <style type="text/css">
        .style1
        {
            width: 10%;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
      <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        &nbsp;
        <table style="width: 100%;">
            <tr>
                <td class="TitoloModulo">
                     Ordini - Ordini e pagamenti - Ricerca - Diretta
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia Ricerca" ToolTip="Avvia Ricerca" />
                            </td>
                            <td>
                                &nbsp;
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
                  
                    <div>
                                                <table style="width:100%">
                            <tr>
                                <td class="style1">
                                </td>
                                <td >
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="Label4" runat="server" 
                                        Style="z-index: 100; left: 48px; top: 32px" Width="110px">Esercizio Finanziario</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <telerik:RadComboBox ID="cmbEsercizio" Width="400px" AppendDataBoundItems="true"
                                        Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="LblComplesso" runat="server"  Style="z-index: 100; left: 48px; top: 32px" Width="110px">ODL</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtODL" runat="server"  MaxLength="15"
                                        Style="z-index: 102; left: 144px; top: 224px; text-align: right" TabIndex="18"
                                        Width="100px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtODL"
                                        Display="Dynamic" ErrorMessage="RegularExpressionValidator" Font-Names="Arial"
                                        Font-Size="8pt" Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                        Width="80px">Valore Numerico</asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    <asp:Label ID="lblAnno" runat="server"
                                        Style="z-index: 100; left: 48px; top: 64px" Width="110px">Anno</asp:Label>
                                </td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtAnno" runat="server"  MaxLength="4"
                                        Style="z-index: 102; left: 688px; top: 192px" TabIndex="23" Width="50px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAnno"
                                        Display="Dynamic" ErrorMessage="max 4 Numeri" Font-Names="arial" Font-Size="8pt"
                                        TabIndex="303" ValidationExpression="\d+" Width="176px"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                </td>
                                <td style="height: 21px">
                                </td>
                            </tr>
                            </table>
                    </div>
                 
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
