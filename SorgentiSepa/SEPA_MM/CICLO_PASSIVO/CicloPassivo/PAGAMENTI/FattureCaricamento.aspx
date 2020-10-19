<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FattureCaricamento.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureCaricamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #form1
        {
            width: 784px;
        }
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
        .style2
        {
            font-family: Arial;
            font-size: 8pt;
        }
    </style>
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
        };
        function ApriAnomalie() {

            window.open('FattureCaricAnomlie.aspx;');
        }
    </script>
</head>
<body >
    <form id="form1" runat="server">
    <table >

        <tr>
            <td>
                            <asp:Label ID="lblTitolo" runat="server" Text="Fatture Utenze" Style="font-family: Arial;
                                font-weight: 700; color: #990000; font-size: 14pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="style2">
                            Esercizio Finanziario
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbEsercizio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Tipo tracciato
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoTracciato" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Fornitore
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbFornitore" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" EnableTheming="True" ForeColor="Black">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Struttura competente
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbStruttra" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" AutoPostBack="True" EnableTheming="True"
                                ForeColor="Black">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Voce BP
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbPfVoci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" EnableTheming="True" ForeColor="Black">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Servizio
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbServizio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" EnableTheming="True" ForeColor="Black">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Voce servizio
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbPfVociImporto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="400px" EnableTheming="True" ForeColor="Black">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Data Caricamento
                        </td>
                        <td>
                            <asp:TextBox ID="txtDataCaricamento" runat="server" Width="80px" Wrap="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
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
            <td align="center">
                <table>
                    <tr>
                        <td>
                            <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="ESCI" ToolTip="Torna alla pagina home" />
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnCarica" runat="server" CssClass="bottone" Text="CARICA FATTURE" OnClientClick ="document.getElementById('dvvvPre').style.visibility = 'visible';" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="idParam" runat="server" Value="0" />
                <asp:HiddenField ID="idTipo" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    </form>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility = 'hidden';

</script>
</html>
