<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Millesimali.ascx.vb" Inherits="CENSIMENTO_Tab_UtMillesimali" %>

<style type="text/css">
    .style1
    {
        width: 440px;
    }
    .style2
    {
        width: 705px;
    }
</style>

<table style="width: 895px; height: 95px">
    <tr>
        <td style="vertical-align: top; text-align: left" class="style2" >
            <div style="border: medium solid #ccccff; left: 0px; vertical-align: top;
                overflow: auto; width: 703px; top: 0px; height: 135px; text-align: left">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                    AutoGenerateColumns="False" Font-Size="8pt" Width="690px" 
                    style="z-index: 105; left: 8px; top: 32px; vertical-align: top; text-align: left;" 
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                    Font-Strikeout="False" Font-Underline="False" BorderColor="Black" 
                    GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="MediumBlue" HorizontalAlign="Left"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn HeaderText="ROWNUM" Visible="False" DataField="ROWNUM"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DETTAGLI">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE_TABELLA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:TemplateColumn Visible="False">
									<ItemTemplate>
										<asp:LinkButton id="LinkButton1" runat="server" Text="Modifica" CausesValidation="false" CommandName="Edit">Seleziona</asp:LinkButton>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:LinkButton id="LinkButton3" runat="server" Text="Aggiorna" CommandName="Update"></asp:LinkButton>&nbsp;
										<asp:LinkButton id="LinkButton2" runat="server" Text="Annulla" CausesValidation="false" CommandName="Cancel"></asp:LinkButton>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
						</asp:datagrid></div>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="10pt" ForeColor="Black" MaxLength="100" Style="left: 13px;
                top: 197px" Width="352px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox></td>
        <td style="vertical-align: top; height: 81px; text-align: left">
                    <table style="width:100%;">
                        <tr>
                            <td>
            <asp:Image ID="imgAddConv" onclick="document.getElementById('Tab_Millesimali1_TextBox2').value='2';document.getElementById('Tab_Millesimali1_HFtxtId').value='0';FxMill.toggle();"
                runat="server"
                ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/40px-Crystal_Clear_action_edit_add.png" 
                ToolTip="Aggiungi un Preventivo" style="width: 18px; cursor:pointer"  />
                            </td>
                        </tr>
                        <tr>
                            <td>
                    <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="~/Condomini/Immagini/pencil-icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" 
                ToolTip="Modifica Elemento Selezionato" CausesValidation="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                            Style="z-index: 102; left: 392px; top: 387px" OnClientClick="DeleteConfirm()"
                ToolTip="Elimina Elemento Selezionato" TabIndex="9" />
                            </td>
                        </tr>
                    </table>
            </td>
        <td style="vertical-align: top; width: 140px; height: 81px; text-align: left">
            <asp:HiddenField ID="HFtxtDesc" runat="server" Value="0" />
            <asp:HiddenField ID="HFtxtId" runat="server" Value="0" />
            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />
            <br />
        </td>
    </tr>
</table>


                        <div style="position: absolute; top: 0px; left: 0px; width: 798px; height: 608px; visibility: hidden; z-index:500; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-color: white; right: 326px;" 
                            id="DivMillesimi">
                            &nbsp;
               <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
                                
                                
                                
                                
                                
                                Style=" left: 20px; top: 58px; height: 460px; width: 754px; margin-right: 0px; z-index: 999; position: absolute; bottom: 7px;" />
                            <table style="width: 83%; position: absolute; top: 86px; left: 69px; z-index: 999; height: 384px;">
                                <tr>
                                    <td class="style1"  >
                                        <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Tipologia Tab. Mill." style="z-index : 500"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1" >
        <asp:DropDownList ID="DrLMillesimi" runat="server" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 24px; border-left: black 1px solid;
            border-bottom: black 1px solid;  top: 32px" TabIndex="1" Width="440px">
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
                                            Font-Size="8pt" Text="Descrizione"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1" >
        <asp:TextBox ID="txtDescr" runat="server" MaxLength="20" Style="left: 24px;
           top: 80px" Width="440px" TabIndex="2"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1" >
                                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt" Text="Desc. Tabella"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1"  >
        <asp:TextBox ID="txtDescTabella" runat="server" MaxLength="20" Style="left: 24px;
           top: 80px" Width="440px" TabIndex="2" Height="72px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1"  >
                                        <asp:Label ID="lblElenco" runat="server" Font-Bold="True" Font-Names="Arial" 
                                            Font-Size="8pt" Text="lblElenco"></asp:Label>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style1"  >
        <div style="border-color: gray; border-width: thin; left: 23px; overflow: auto;
            width: 440px; top: 175px; height: 72px;
            ">
            <asp:CheckBoxList ID="ListLista" runat="server" EnableTheming="False" Font-Names="Arial"
                Font-Size="8pt" Height="25px" RepeatLayout="Flow" Style="z-index: 1; left: 10px;
                top: 12px" Width="100%" TabIndex="4">
            </asp:CheckBoxList></div>
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
                        <asp:HiddenField ID="TextBox2" runat="server" />
    <script  language="javascript" type="text/javascript">
        FxMill = new fx.Opacity('DivMillesimi', { duration: 200 });
        //FxMill.hide();
        if (document.getElementById('Tab_Millesimali1_TextBox2').value != '2') {
            FxMill.hide(); ;
        }
        function DeleteConfirm() {
            if (document.getElementById('Tab_Millesimali1_HFtxtId').value != 0) {
                var Conferma
                Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                if (Conferma == false) {
                    document.getElementById('Tab_Millesimali1_txtConfElimina').value = '0';

                }
                else {
                    document.getElementById('Tab_Millesimali1_txtConfElimina').value = '1';

                }
            }
        }

    </script>






