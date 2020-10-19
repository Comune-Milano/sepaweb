<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Prospetto.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_Prospetto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../CicloPassivo.js"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" />
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js"></script>
    <title>Prospetto</title>
    <style type="text/css">
        .auto-style2 {
            position: absolute;
            top: 238px;
            left: 11px;
        }

        .auto-style3 {
            position: absolute;
            top: 202px;
            left: 15px;
            width: 300px;
        }

        .auto-style4 {
            position: absolute;
            top: 175px;
            left: 14px;
        }

        .auto-style6 {
            position: absolute;
            left: 129px;
            top: 7px;
        }

        .auto-style7 {
            position: absolute;
            left: 19px;
            top: 7px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <div>
            <table style="width: 100%">

                <tr>
                    <td class="TitoloModulo ">Gestione - Building Manager
                    </td>
                </tr>

                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="ImgProcedi" runat="server" Text="Procedi" Style="cursor: pointer;"
                                        TabIndex="31" OnClientClick="Conferma()" />


                                </td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="imgEsci" runat="server" Text="Esci" Style="cursor: pointer;" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px">&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="SCELTA DELL'ESERCIZIO FINANZIARIO" Font-Bold="True"
                            Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cmbEsercizio" runat="server"
                            Font-Names="arial" Font-Size="10pt">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:CheckBox ID="CheckBox1" runat="server" Font-Names="arial"
                            Font-Size="10pt"
                            Text="Carica Voci standard (carica una serie di voci nel business plan, che saranno comunque modificabili)"
                            Checked="True" Enabled="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblErrore" runat="server" Visible="False"
                            Style="top: 375px; left: 16px;" Font-Bold="True"
                            Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label></td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="prosegui" runat="server" Value="0" />

    </form>
    <script type="text/javascript">
        function Conferma() {

            var sicuro = window.confirm('Confermi di voler creare un nuovo Business Plan per l\'esercizio finanziario selezionato?');
            if (sicuro == true) {
                document.getElementById('prosegui').value = '1';
            }
            else {
                document.getElementById('prosegui').value = '0';
            }
        }
    </script>
</body>
</html>
