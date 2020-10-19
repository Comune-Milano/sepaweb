<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FatturePagaUt.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FatturePagaUt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title></title>
    <style type="text/css">
              .bottone
        {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
        }
        </style>
    <script language="javascript" type="text/javascript">

        function chiudi(pulsante) {
            GetRadWindow().BrowserWindow.document.getElementById(pulsante).click();
            GetRadWindow().close();



        };
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) {
                oWindow = window.radWindow;
            } else {
                if (window.frameElement) {
                    if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;
                    };
                };
            };
            return oWindow;
        };

        function closeWin() {
            GetRadWindow().close();
        };
        //        function caricamentoincorso() {
        //            if (typeof (Page_ClientValidate) == 'function') {
        //                Page_ClientValidate();
        //                if (Page_IsValid) {
        //                    if (document.getElementById('caricamento') != null) {
        //                        document.getElementById('caricamento').style.display = 'block';
        //                    };
        //                }
        //                else {
        //                    alert('ATTENZIONE! Ci sono delle incongruenze dati della pagina e/o eventuali TAB!');
        //                };
        //            }
        //            else {
        //                if (document.getElementById('caricamento') != null) {
        //                    document.getElementById('caricamento').style.display = 'block';
        //                };
        //            };
        //        };
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
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
            EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="BottomRight"
            OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
    </div>
    <table style="width: 100%;" class="FontTelerik">
        <tr>
            <td style="font-size: 3pt">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="TitoloModulo">
                Emissione CDP
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnSalva" runat="server" Text="Salva CDP" ToolTip="Salva il pagamento"
                                Style="top: 0px; left: 1px" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnSalStampa" runat="server" Text="Stampa CDP" ToolTip="Stampa il pagamento"
                                Style="top: 0px; left: 1px" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" OnClientClicking="function(sender, args){chiudi('btnCrea');}"
                                ToolTip="Esci" Style="top: 0px; left: 1px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            Importo €.
                        </td>
                        <td>
                            <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="10pt" Width="80px"
                                ReadOnly="True" Style="text-align: right"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Data emissione
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmissione" runat="server" Font-Names="Arial" Font-Size="10pt"
                                Width="80px"></asp:TextBox>
                        </td>
                        <td>
                            Scadenza
                        </td>
                        <td>
                            <asp:TextBox ID="txtScadenza" runat="server" Font-Names="Arial" Font-Size="10pt"
                                Width="80px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Modalità Pagamento
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtModalita" runat="server" Font-Names="Arial" Font-Size="10pt"
                                Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Condizione Pagamento
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtcondizione" runat="server" Font-Names="Arial" Font-Size="10pt"
                                Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Descrizione
                        </td>
                        <td colspan="3">
                            <asp:TextBox ID="txtdescrizione" runat="server" Font-Names="Arial" Font-Size="10pt"
                                Height="62px" TextMode="MultiLine" Width="400px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="idModalita" runat="server" />
                <asp:HiddenField ID="idCondizione" runat="server" />
                <asp:HiddenField ID="idFornitore" runat="server" Value="0" />
                <asp:HiddenField ID="idIban" runat="server" Value="NULL" />
                <asp:HiddenField ID="idPagamento" runat="server" Value="0" />
                <asp:HiddenField ID="idEsercizio" runat="server" Value="0" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </form>
    <script language="javascript" type="text/javascript">
        //    document.getElementById('caricamento').style.display = 'none';
    </script>
</body>
</html>
