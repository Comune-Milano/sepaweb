<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListaManutenzioni.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_ListaManutenzioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lista manutenzioni</title>
    <base target="_self" />
    <script type="text/javascript" language="javascript">
        var selezionato;
        window.name = "modal";

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
    </script>
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
</head>
<body>
    <form id="form1" runat="server" target="modal">
    <div style="height: 180px; overflow: auto;">
        <asp:DataGrid runat="server" ID="DataGridManutenzioni" AutoGenerateColumns="False"
            CellPadding="1" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" GridLines="None"
            CellSpacing="1" Width="100%">
            <AlternatingItemStyle BackColor="#DDDDDD" ForeColor="#000000" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="id" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="NUMERO" HeaderText="N°"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
            </Columns>
            <EditItemStyle BackColor="#999999" />
            <FooterStyle BackColor="#EEEEEE" Font-Bold="True" ForeColor="Red" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Italic="False"
                Font-Overline="False" Font-Size="9pt" Font-Strikeout="False" Font-Underline="False"
                HorizontalAlign="Center" />
            <ItemStyle BackColor="#FFFFFF" ForeColor="#000000" />
            <PagerStyle BackColor="#507CD1" ForeColor="White" HorizontalAlign="Center" />
            <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#000000" />
        </asp:DataGrid>
    </div>
    <br />
    <br />
    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        Text="Nessuna Selezione" Width="100%" BackColor="White" BorderWidth="0px"></asp:TextBox>
    <br />
    <asp:Button ID="btnVisualizza" runat="server" CssClass="bottone" Text="VISUALIZZA"
        ToolTip="Salva" />
    <asp:HiddenField ID="IDM" runat="server" Value="0" />
    </form>
</body>
</html>
