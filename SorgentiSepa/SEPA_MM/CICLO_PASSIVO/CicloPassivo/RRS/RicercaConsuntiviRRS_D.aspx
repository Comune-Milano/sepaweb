<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaConsuntiviRRS_D.aspx.vb"
    Inherits="RRS_RicercaConsuntiviRRS_D" %>

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
.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top;opacity:1}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top;opacity:1}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        .style1
        {
            width: 10%;
        }
        .style2
        {
            height: 20px;
            width: 10%;
        }
        </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnCerca" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div>
        <table style="width: 100%;">
            <tr>
                <td class="TitoloModulo">
                    Ordini - Gestione non patrimoniale - Consuntivazione - Diretta
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnCerca" runat="server" Text="Avvia la ricerca" ToolTip="Avvia la ricerca"
                                    CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                            <td>
                                <telerik:RadButton ID="btnHome" runat="server" Text="Esci" ToolTip="Home" CausesValidation="False">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <div style="overflow: auto;">
                        <div>
                            <table style="width: 100%">
                                <tr>
                                    <td class="style1">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <asp:Label ID="lblEF" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                            Width="130px">Esercizio Finanziario</asp:Label>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic" Width="500px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style2">
                                        <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                            Width="130px">Num. Repertorio</asp:Label>
                                    </td>
                                    <td style="height: 20px">
                                        <asp:TextBox ID="txtNumRepertorio" runat="server" Height="30px" MaxLength="50" Style="left: 144px;
                                            top: 112px" TabIndex="3" TextMode="MultiLine" Width="500px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left" class="style1">
                                        <asp:Label ID="LblComplesso" runat="server" Style="z-index: 100; left: 48px; top: 32px"
                                            Width="130px">ODL</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtODL" runat="server" MaxLength="15" Style="z-index: 102; left: 144px;
                                            top: 224px; text-align: right" TabIndex="4" Width="100px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtODL" Display="Dynamic"
                                                ErrorMessage="RegularExpressionValidator" Font-Names="Arial" Font-Size="8pt"
                                                Style="left: 224px; top: 232px" TabIndex="-1" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                                                Width="80px">Valore Numerico</asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top; text-align: left" class="style1">
                                        <asp:Label ID="lblAnno" runat="server" Style="z-index: 100; left: 48px; top: 64px"
                                            Width="130px">Anno</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAnno" runat="server" MaxLength="4" Style="z-index: 102; left: 688px;
                                            top: 192px" TabIndex="5" Width="50px"></asp:TextBox><asp:RegularExpressionValidator
                                                ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtAnno" Display="Dynamic"
                                                ErrorMessage="max 4 Numeri" Font-Names="arial" Font-Size="8pt" TabIndex="303"
                                                ValidationExpression="\d+" Width="176px"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
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
