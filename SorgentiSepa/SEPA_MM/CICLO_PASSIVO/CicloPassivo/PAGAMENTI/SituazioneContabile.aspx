<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazioneContabile.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_SituazioneContabile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>Situazione Contabile Generale per Esercizio Finanziario</title>
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
    <style type="text/css">
        .auto-style1 {
            width: 10%;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div style="height: 500px">
            <table border="0" cellpadding="2" cellspacing="2" width="100%">
                <tr>
                    <td colspan="2" class="TitoloModulo">Report - Situazione contabile - Per Esercizio
                    </td>
                </tr>
                <tr>
                    <td colspan="2">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <table>
                            <tr>
                                <td>
                                    <telerik:RadButton ID="btnStampa" runat="server" Text="Stampa" ToolTip="Stampa" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="btnAnnulla" runat="server" Style="top: 0px; left: 0px" Text="Esci"
                                        ToolTip="Home" />
                                </td>
                            </tr>
                        </table>
                    </td>

                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text="Esercizio contabile" ></asp:Label>
                    </td>
                    <td style="width: 80%">

                        <telerik:RadComboBox ID="DropDownListEsercizioContabile" runat="server" AppendDataBoundItems="true"
                            AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                            ResolvedRenderMode="Classic" Width="40%">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label3" runat="server" Text="Data esercizio al" ></asp:Label>
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="TextBoxAl" runat="server" Width="75px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBoxAl"
                            Display="Dynamic" ErrorMessage="Inserire le date nel formato gg/mm/aaaa" Font-Bold="False"
                            Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="243px"></asp:RegularExpressionValidator>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
