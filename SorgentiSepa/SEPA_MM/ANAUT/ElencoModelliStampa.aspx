<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoModelliStampa.aspx.vb" Inherits="ANAUT_ElencoModelliStampa" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Selezionato;
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Modelli Disponibili</title>
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
        .style2
        {
            font-size: xx-small;
            color: #000000;
        }
        .style4
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: xx-small;
            font-weight: bold;
            color: #000000;
        }
        .style5
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: xx-small;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="text-align: center; font-weight: 700; font-family: Arial, Helvetica, sans-serif; font-size: small">
                    MODELLI DISPONIBILI PER LA STAMPA</td>
            </tr>
            <tr>
                <td style="text-align: center; font-weight: 700; font-family: Arial, Helvetica, sans-serif; font-size: small">
                    &nbsp; &nbsp;</td>
            </tr>
            <tr bgcolor="#FFFFCC">
                <td style="text-align: left; font-weight: 700; font-family: Arial, Helvetica, sans-serif; " 
                    class="style2">
                    1)
                    Fai click sul documento da stampare</td>
            </tr>
            <tr>
                <td class="style1">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        
                        style="z-index: 105; left: 0px; width: 100%;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        CellPadding="4" ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="MODELLO">
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="MODELLO1" HeaderText="ANTEPRIMA">
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
    
                    <br />
    
                </td>
            </tr>
            <tr bgcolor="#FFFFCC">
                <td class="style4">
                    2) Inserisci il numero di protocollo se previsto nel modello</td>
            </tr>
            <tr valign="middle">
                <td style="text-align: left">
                    <asp:Label ID="lblCodContratto7" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Numero di Protocollo:</asp:Label>
                    &nbsp;<asp:TextBox ID="txtProtocollo" runat="server"></asp:TextBox>
                    <br />
&nbsp;
                </td>
            </tr>
            <tr bgcolor="#FFFFCC" valign="middle">
                <td style="text-align: left" class="style4">
                    3) Inserisci la data di stampa 
                    (gg/mm/aaaa) del documento, se prevista nel modello.</td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="lblCodContratto8" runat="server" Font-Names="ARIAL" 
                        Font-Size="10pt" Font-Bold="False">Data di stampa:</asp:Label>
                    &nbsp;<asp:TextBox ID="txtProtocollo0" runat="server" Width="73px"></asp:TextBox>
                    <br />
&nbsp; </td>
            </tr>
            <tr bgcolor="#FFFFCC">
                <td style="text-align: left" class="style5">
                    4) Premi il pulsante procedi per visualizzare il documento in formato PDF.</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp;&nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                                            ToolTip="Stampa il modello selezionato" style="height: 20px" 
                                             
                  />
                    &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<img alt="" src="../NuoveImm/Img_Esci_AMM.png" onclick="self.close();" style="cursor:pointer"/></td>
            </tr>
            <tr valign="middle">
                <td style="text-align: left">
                    <asp:Image ID="imgAlert" runat="server" ImageUrl="~/IMG/Alert.gif" 
                        Visible="False" />
                    <asp:Label ID="lblsindacati" runat="server" Font-Names="ARIAL" 
                        Font-Size="8pt" Font-Bold="True" Visible="False">AU pervenuta tramite SINDACATI</asp:Label>
                    </td>
            </tr>
            <tr valign="middle">
                <td style="text-align: left">
                    <asp:Image ID="imgAlert0" runat="server" ImageUrl="~/IMG/Alert.gif" 
                        Visible="False" />
                    <asp:Label ID="lblsindacati0" runat="server" Font-Names="ARIAL" 
                        Font-Size="8pt" Font-Bold="True" Visible="False">Contratto FF.OO.</asp:Label>
                    </td>
            </tr>
        </table>
                            <asp:HiddenField ID="LBLID" runat="server" Value="" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                         <asp:HiddenField ID="H1" runat="server" Value="0" />
                             <asp:HiddenField ID="idc" runat="server" />
    <asp:HiddenField ID="idd" runat="server" />
    </div>
    <script type="text/javascript" language="javascript">
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
    </form>
</body>
</html>
