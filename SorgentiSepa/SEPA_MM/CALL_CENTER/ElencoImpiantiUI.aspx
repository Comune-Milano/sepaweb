<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoImpiantiUI.aspx.vb" Inherits="CALL_CENTER_ElencoImpiantiUI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table style="left: 0px; BACKGROUND-IMAGE: url('../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Elenco Impianti UI</strong></span><br />
                    <br />

                    <br />
                   <asp:Label ID="lblNumImpianti" runat="server" 
                        style="position:absolute; top: 60px; left: 268px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                   <asp:Label ID="lblIDU" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="9pt"
                
            Style="z-index: 106; left: 11px; position: absolute; top: 60px; width: 445px;"></asp:Label>
                        <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                        <asp:TextBox ID="txtmsg" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                            
                        Style="border: 1px solid white; position:absolute; left: 5px; top: 478px;" 
                        Width="777px">Nessuna Selezione</asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
        
                    <asp:Image ID="btnExcel" runat="server" 
                        style="position:absolute; top: 507px; left: 576px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Export_Grande.png" Visible="False"/>
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 507px; left: 713px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                </td>
            </tr>
            <asp:HiddenField ID="tipo_impianto" runat="server" />
            <asp:HiddenField ID="id_impianto" runat="server" />
            <asp:HiddenField ID="id_edificio" runat="server" />
            <asp:HiddenField ID="tipo" runat="server" />
            <asp:HiddenField ID="cod_impianto" runat="server" />
            <asp:HiddenField ID="identificativo" runat="server" />
    </table>
   
   <div>
   <div id="contenitore" 
            
            
           style="position: absolute; width: 770px; height: 317px; overflow: auto; top: 93px; left: 13px;">
            <asp:DataGrid ID="Datagrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="50" 
                    Style="z-index: 101; left: 0px; width: 748px; position:absolute; top: 0px; " 
                    TabIndex="1" BorderWidth="0px">
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <Columns>
                    <asp:BoundColumn DataField="COD_IMPIANTO" HeaderText="CODICE IMPIANTO">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_IMPIANTO" HeaderText="ID_IMPIANTO" 
                        Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="RIFERIMENTO" HeaderText="RIFERITO A">
                    </asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Navy" Wrap="False" />
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid>        
        </div>
    </div>
            
    
    </form>
    <script type="text/javascript">
        var selezionato;
        function ConfermaEsci() {

                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    window.close()
                }

            }
    </script>
</body>
</html>
