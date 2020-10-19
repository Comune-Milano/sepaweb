<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_NonIspezion.ascx.vb" Inherits="NEW_CENSIMENTO_Tab_NonIspezion" %>
<table style="width: 645px; height: 95px">
    <tr>
        <td style="width: 80px; vertical-align: top; height: 81px; text-align: left;">
            <div 
                
                style="left: 0px; vertical-align: top; overflow: auto; width: 703px; top: 0px;
                height: 135px; text-align: left; border-right: #ccccff solid; border-top: #ccccff solid; border-left: #ccccff solid; border-bottom: #ccccff solid;">
                <asp:DataGrid ID="DgvLocNoAccess" runat="server" AllowSorting="True" 
                    AutoGenerateColumns="False" BackColor="White" BorderColor="RoyalBlue" 
                    Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" 
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                    PageSize="1" Style="z-index: 105; left: 8px; top: 32px" Width="98%">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" 
                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                        Font-Underline="False" ForeColor="#0000C0" />
                    <Columns>
                        <asp:BoundColumn HeaderText="id" 
                            Visible="False" DataField="ID_LOCALE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="LOCALE">
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" 
                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                        Font-Underline="False" />
                </asp:DataGrid>
            </div>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" 
                BorderStyle="None" Font-Bold="True" Font-Names="Arial" Font-Size="10pt" 
                ForeColor="Black" MaxLength="100" ReadOnly="True" Style="left: 13px;
                top: 197px" Width="585px">Nessuna Selezione</asp:TextBox>
        </td>
        <td style="vertical-align: top; width: 38px; text-align: left; height: 81px;">
            <asp:Image ID="imgAddConv" onclick="myOpacNonIsp.toggle();"
                runat="server"
                ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png" 
                ToolTip="Aggiungi un Preventivo" style="width: 18px" />
            <br />
            <asp:ImageButton ID="BtnElimina" runat="server" 
                ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png" 
                Style="z-index: 103; left: 495px; top: 69px" TabIndex="2" 
                ToolTip="Elimina elemento selezionato dalla lista" />
        </td>
        <td style="vertical-align: top; width: 140px; height: 81px; text-align: left">
            <asp:HiddenField ID="HFtxtDesc" runat="server" />
            <asp:HiddenField ID="HFtxtId" runat="server" />
        </td>
    </tr>
</table>

 <div id="Locali"    
      
    
    style="border: thin none #3366ff; left: 2px; width: 799px; position: absolute; top: 22px; height: 600px; background-color: #dedede; visibility :hidden; vertical-align: top; text-align: left; z-index: 201; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); margin-right: 10px;">
               <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
                    
                   
                   
                   
                   Style="z-index: 100; left: 63px; position: absolute; top: 67px; height: 408px; width: 672px;" />
               <table style="width: 68%; position: absolute; top: 125px; left: 127px; z-index: 800; height: 72px;">
                   <tr>
                       <td class="style2">
                           &nbsp;</td>
                       <td class="style1">
                           <asp:Label ID="lblTipo0" runat="server" Font-Bold="True" Font-Names="Arial" 
                               Font-Size="8pt" Text="IMPOSSIBILITA' D'ISPEZIONE"></asp:Label>
                       </td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           <asp:Label ID="lblTipo" runat="server" Font-Bold="True" Font-Names="Arial" 
                               Font-Size="8pt" Text="Locale"></asp:Label>
                       </td>
                       <td class="style1">
                           <asp:DropDownList ID="cmbLocali" runat="server" Width="397px">
                           </asp:DropDownList>
                       </td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           &nbsp;</td>
                       <td class="style1">
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
                   <tr>
                       <td class="style2">
                           &nbsp;</td>
                       <td  style="text-align: right">
                           <asp:ImageButton ID="BtnSalva" runat="server" 
                               ImageUrl="~/NuoveImm/Img_SalvaVal.png" ToolTip="Salva" />
                       </td>
                       <td style="text-align: right">
            <img id="imgAnnulla" alt="Annulla" onclick="myOpacNonIsp.toggle();"
                    src="../NuoveImm/Img_AnnullaVal.png" 
                               style="left: 185px; cursor: pointer; top: 23px" /></td>
                   </tr>
               </table>
            </div>
            
<script type="text/javascript">
    myOpacNonIsp = new fx.Opacity('Locali', { duration: 200 });
    myOpacNonIsp.hide();
</script>