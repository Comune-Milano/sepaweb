<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_ElencoPrezzi.ascx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_Tab_ElencoPrezzi" %>

<table style="width:100%">
    <tr>
        <td style="vertical-align: top; text-align: left; width:100%" >
            <div style="border-right: #ccccff 2px solid; border-top: #ccccff 2px solid; left: 0px; vertical-align: top;
                overflow: auto; border-left: #ccccff 2px solid; width: 100%; border-bottom: #ccccff 2px solid;
                top: 0px; height: 105px; text-align: left">
                <asp:DataGrid ID="DataGridElPrezzi" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderColor="Gray" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="98%" BorderWidth="1px">
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                </asp:DataGrid></div>
        </td>
        
        <td style="vertical-align: top; text-align: left">
            <table>
                <tr>
                    <td>
            <asp:Image ID="imgAggiungiServ" 
               
                runat="server" onclick="document.getElementById('USCITA').value='1'; document.getElementById('DivPrezzi').style.visibility='visible';document.getElementById('Tab_ElencoPrezzi1_divVisibility').value='1';document.getElementById('Tab_ElencoPrezzi1_txtDescrizione').value='';document.getElementById('Tab_ElencoPrezzi1_idSelezionato').value=-1"
                ImageUrl="../../../NuoveImm/btn_Aggiungi.jpg" 
                ToolTip="Aggiungi " Style="cursor:pointer;"   />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:ImageButton ID="btnApriAppalti" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="../../../NuoveImm/Img_Modifica.png" OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DivPrezzi').style.visibility='visible';document.getElementById('Tab_ElencoPrezzi1_divVisibility').value='1';"
                            TabIndex="16" ToolTip="Modifica voce selezionata" /></td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:ImageButton ID="btnEliminaAppalti" runat="server" ImageUrl="../../../NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfEliminaElPrezzi();"
                            TabIndex="15" ToolTip="Elimina voce selezionata" 
                            CausesValidation="False" /></td>
                </tr>
            </table>

        </td>
    </tr>
    <tr>
        <td >
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="60"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="92%">Nessuna Selezione</asp:TextBox>

        </td>
        <td>
            <asp:HiddenField ID="divVisibility" runat="server" Value="0" />
            <asp:HiddenField ID="idSelezionato" runat="server" Value="-1" />
            <asp:HiddenField ID="confElimina" runat="server" Value="0" />
        </td>
    </tr>
</table>


<div id="DivPrezzi"    
    
    
    
    
    
    
    
    style="border: thin none #3366ff; width: 802px; position: absolute; height: 283px; left: -4px; top: 209px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); visibility: hidden; background-color: #dedede; vertical-align: top; text-align: left; z-index: 201; margin-right: 10px;">
    &nbsp;
    <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="../../../ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 0px; background-image: url('../../../ImmDiv/DivMGrande.png');
        position: absolute; top: 51px; height: 217px; width: 781px;" />
    <table style="border-color: #6699ff; border-width: thin; z-index: 105; left: 31px; width: 728px;
        margin-right: 0px; position: absolute; top: 23px;
        height: 221px; ">
        <tr>
            <td style="vertical-align: top; height: 18px; text-align: left">
                <strong><span style="color: #660000; font-family: Arial">Elenco Prezzi</span></strong></td>
        </tr>
        <tr>
            <td class="style1" style="vertical-align: top; height: 18px; text-align: left">
                &nbsp;</td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left;">
                <div style=  " width: 97%">
                    <asp:TextBox ID="txtDescrizione" runat="server" Height="159px" MaxLength="500" 
                        TextMode="MultiLine" Width="98%"></asp:TextBox>
                </div>
                
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: right">
                            <asp:ImageButton ID="btn_Inserisci" runat="server" ImageUrl="Immagini/Next.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Servizio_txtAppareP').value='0';"
                                Style="cursor: pointer" TabIndex="55" ToolTip="Salva" />
            <asp:Image ID="imgEsci" 
               
                runat="server" onclick="document.getElementById('USCITA').value='1'; document.getElementById('DivPrezzi').style.visibility='hidden'; 
                                        document.getElementById('Tab_ElencoPrezzi1_txtDescrizione').value='';document.getElementById('Tab_ElencoPrezzi1_divVisibility').value='0';
                                        document.getElementById('Tab_ElencoPrezzi1_idSelezionato').value='-1'"
                ImageUrl="~/NuoveImm/Img_Esci.png" 
                ToolTip="Aggiungi " Style="cursor:pointer;"   />
            </td>
        </tr>
    </table>
</div>









