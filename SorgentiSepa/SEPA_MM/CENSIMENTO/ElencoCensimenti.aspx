<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoCensimenti.aspx.vb" Inherits="NEW_CENSIMENTO_ElencoCensimenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>


<head id="Head1" runat="server">
		<title>Elenco Censimenti</title>
        <style type="text/css">
            #Form1
            {
                height: 609px;
                width: 784px;
            }
        </style>
        <script type ="text/javascript" >

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
	
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">

		<form id="Form1" method="post" runat="server">   
                        
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Elenco 
        dei Censimenti dello Stato Manutentivo 
        <div style="left: 8px; overflow: auto; width: 781px; position: absolute; top: 51px;
                height: 446px">
                <asp:DataGrid ID="DataGridElenco" runat="server" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    GridLines="None" PageSize="24" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="762px">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle ForeColor="Black" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_INIZIO" HeaderText="DATA INIZIO" 
                            ReadOnly="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_FINE" HeaderText="DATA FINE" ReadOnly="True">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                </asp:DataGrid>
            </div>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 13px; position: absolute; top: 600px" Text="Label"
                Visible="False" Width="624px"></asp:Label>
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 726px; position: absolute; top: 547px; height: 20px;" 
                ToolTip="Home" />
            <asp:ImageButton ID="btnApriCens" runat="server" ImageUrl="../CENSIMENTO/IMMCENSIMENTO/NuovoCens.png"
                Style="z-index: 101; left: 414px; position: absolute; top: 547px" 
                ToolTip="Apri un Nuovo Censimento dello Stato Manutentivo" OnClientClick ="myOpacity.toggle(); document.getElementById('txtVisible').value =1" />
            <asp:ImageButton ID="btnChiudiCens" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/ChiudiCens.png"
                Style="z-index: 101; left: 570px; position: absolute; top: 547px" 
                ToolTip="Chiudi un Censeimento dello Stato Manutentivo" 
            onclientclick="myOpacity.toggle(); document.getElementById('txtVisible').value =1" />
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
            Style="z-index: 2; left: 7px; position: absolute; top: 502px" 
            Width="632px">Nessuna Selezione</asp:TextBox>
            </span></strong>   
                        
		<script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>

 <div id="DivDate"    

    
            
            
            
            
            
            style="border: thin none #3366ff; left: -1px; width: 800px; position: absolute; top: 2px; height: 600px; background-color: #dedede; visibility :visible; vertical-align: top; text-align: left; z-index: 201; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); margin-right: 10px;">
               <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
                    
                   
                   
                   Style="z-index: 100; left: 32px; position: absolute; top: 55px; height: 408px; width: 687px;" />
               <table style="width: 42%; position: absolute; top: 161px; left: 221px; z-index: 800; height: 72px;">
                   <tr>
                       <td >
                           <asp:Label ID="lblTipo" runat="server" Font-Bold="True" Font-Names="Arial" 
                               Font-Size="8pt" Text="Data Inizio"></asp:Label>
                       </td>
                       <td >
                           <asp:TextBox ID="txtDataInizio" runat="server" Width="100px"></asp:TextBox>
                       </td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td >
                           <asp:Label ID="lblDataFine" runat="server" Font-Bold="True" Font-Names="Arial" 
                               Font-Size="8pt" Text="Data Fine"></asp:Label>
                       </td>
                       <td c">
                           <asp:TextBox ID="txtDataFine" runat="server" Width="100px"></asp:TextBox>
                       </td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td >
                           &nbsp;</td>
                       <td >
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td >
                           &nbsp;</td>
                       <td >
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td >
                           &nbsp;</td>
                       <td >
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td >
                           &nbsp;</td>
                       <td >
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td >
                           &nbsp;</td>
                       <td  style="text-align: right">
                           <asp:ImageButton ID="BtnSalva" runat="server" 
                               ImageUrl="~/NuoveImm/Img_SalvaVal.png" ToolTip="Salva" />
                       </td>
                       <td style="text-align: right">
            <img id="imgAnnulla" alt="Annulla" onclick="myOpacity.toggle();"
                    src="../NuoveImm/Img_AnnullaVal.png" 
                               style="left: 185px; cursor: pointer; top: 23px" /></td>
                   </tr>
               </table>
            </div>
                    <asp:HiddenField ID="txtId" runat="server" />
        <asp:HiddenField ID="txtVisible" runat="server" Value="0" />
        <script type="text/javascript">

            myOpacity = new fx.Opacity('DivDate', { duration: 200 });
            //myOpacity.hide();
            if (document.getElementById('txtVisible').value != '1') {
                myOpacity.hide();;
            }
        </script>




		</form>
		
	</body>
</html> 