<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_ValoriMilleismalil.ascx.vb" Inherits="CENSIMENTO_Tab_ValoriMilleismalil" %>
<style type="text/css">

    .style1
    {
        width: 440px;
    }
</style>
<table style="width: 645px; height: 95px">
    <tr>
        <td style="vertical-align: top; width: 80px; height: 81px; text-align: left">
            <div style="border-right: #ccccff solid; border-top: #ccccff solid; left: 0px; vertical-align: top;
                overflow: auto; border-left: #ccccff solid; width: 703px; border-bottom: #ccccff solid;
                top: 0px; height: 135px; text-align: left">
            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False"
                BackColor="White" BorderColor="Black" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                    Font-Underline="False" PageSize="15" Style="z-index: 105; left: 7px;
                top: 36px" Width="97%" GridLines="None">
                <PagerStyle Mode="NumericPages" />
                <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="MediumBlue" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ROWNUM" HeaderText="ROWNUM" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="VALORE_MILLESIMO" HeaderText="VALORE_MILLESIMO" Visible="False">
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="DESCRIZIONE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="VALORE">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.VALORE_MILLESIMO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                Text="Modifica">Seleziona</asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton>&nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                Text="Annulla"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
            </asp:DataGrid></div>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" ForeColor="Black" MaxLength="100" Style="left: 13px;
                top: 197px" Width="352px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox></td>
        <td style="vertical-align: top; width: 40px; height: 81px; text-align: left">
                    <img alt="" src="../Condomini/Immagini/40px-Crystal_Clear_action_edit_add.png" 
                        
                        onclick="document.getElementById('Tab_ValoriMilleismalil1_TextBox2').value='2';document.getElementById('Tab_ValoriMilleismalil1_HFtxtId').value='0';FxValMill.toggle();" 
                        id="ImgAddMil" style="visibility:visible; cursor: pointer;"/><br />
            <br />
                        <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="~/Condomini/Immagini/pencil-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" 
                ToolTip="Modifica Elemento Selezionato" CausesValidation="False" />
                    <br />
            <br />
            <asp:ImageButton ID="BtnElimina" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/minus_icon.png"
                Style="z-index: 103; left: 495px; top: 69px" TabIndex="2" OnClientClick="DeleteConfirm()"
                ToolTip="Elimina elemento selezionato dalla lista" /></td>
        <td style="vertical-align: top; width: 24px; height: 81px; text-align: left">
            <asp:HiddenField ID="HFtxtDesc" runat="server" />
            <asp:HiddenField ID="HFtxtId" runat="server" />
            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
            <br />
            </td>
    </tr>
</table>
<asp:HiddenField ID="TextBox2" runat="server" Value="0" />


                        <div style="position: absolute; top: 0px; left: 0px; width: 681px; height: 525px; visibility: hidden; z-index:500; background-image: url('~/ImmDiv/DivMGrande.png'); background-color: white;" 
                            id="DivMillesimi">
                            &nbsp;
               <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
                                
                                
                                
                                
                                Style=" left: 5px;top: 5px; height: 506px; width: 666px; margin-right: 0px; z-index: 999; position: absolute; bottom: 14px;" />
                            <table style="width: 80%; position: absolute; top: 138px; left: 28px; z-index: 999; height: 151px;">
                                <tr>
                                    <td class="style1"  >
                                        <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Tabellla  Millesimale Edificio" 
                                            style="z-index : 500"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan=3 >
        <asp:DropDownList ID="DrLMillesimi" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 24px; border-left: black 1px solid;
            border-bottom: black 1px solid;  top: 32px" TabIndex="1" Width="521px">
        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan=3 >
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Tabellla  Millesimale Complesso" 
                                            style="z-index : 500"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan=3 >
        <asp:DropDownList ID="DrLMillesimiComp" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 24px; border-left: black 1px solid;
            border-bottom: black 1px solid;  top: 32px" TabIndex="1" Width="521px">
        </asp:DropDownList>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1" >
                                        <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Valore"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1" >
        <asp:TextBox ID="txtValore" runat="server" MaxLength="9" Style="left: 24px;
           top: 80px" Width="116px" TabIndex="2"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1"  >
                                        &nbsp;</td>
                                    <td>
        <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="" 
        ToolTip="Salva" TabIndex="5" />
                                    </td>
                                    <td>
        <asp:ImageButton ID="btnEsci" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="" 
        ToolTip="Salva" TabIndex="5" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
    <script  language="javascript" type="text/javascript">
        FxValMill = new fx.Opacity('DivMillesimi', { duration: 200 });
        //FxMill.hide();
        if (document.getElementById('Tab_ValoriMilleismalil1_TextBox2').value != '2') {
            FxValMill.hide();
//            alert('NASCOSTO!');
       }
        function DeleteConfirm() {
            if (document.getElementById('Tab_ValoriMilleismalil1_HFtxtId').value != 0) {
                var Conferma
                Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                if (Conferma == false) {
                    document.getElementById('Tab_ValoriMilleismalil1_txtConfElimina').value = '0';

                }
                else {
                    document.getElementById('Tab_ValoriMilleismalil1_txtConfElimina').value = '1';

                }
            }
        }
        if (document.getElementById('TxtInterno').disabled == true) {
            document.getElementById('ImgAddMil').style.visibility = 'hidden'
            
        }
       
    </script>
    


                        
                        


