<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Edifici.ascx.vb" Inherits="Tab_Termico_Edifici" %>
<table id="TABBLE_LISTA" >
    <tr>
        <td>
            <asp:Label ID="lblPOMPE" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" Style="height: 15px" TabIndex="-1" Text="ELENCO EDIFICI ALIMENTATI "
                Width="248px"></asp:Label></td>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td>
            <div id="DIV1" style="border-right: #0000cc thin solid; border-top: #0000cc thin solid;
                visibility: visible; overflow: auto; border-left: #0000cc thin solid; width: 670px;
                border-bottom: #0000cc thin solid; height: 110px ">
                <asp:DataGrid ID="DataGridEdifici" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Transparent" PageSize="1" Style="z-index: 101; left: 9px" Width="104%" BorderColor="Blue">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Mode="NumericPages" Wrap="False" />
                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" BackColor="WhiteSmoke" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="DENOMINAZIONE">
                            <EditItemTemplate>
                                <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" />
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50%" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="8pt"
                                Font-Strikeout="False" Font-Underline="False" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="TOTALE_UI_AL" HeaderText="U.I.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE_UI" HeaderText="U.I. (Totali)">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE_MQ_AL" HeaderText="MQ">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE_MQ" HeaderText="MQ (Totali)">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UNITA" HeaderText="UNITA' IMMOBILIARI">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CHK" HeaderText="CHK" Visible="False"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                </asp:DataGrid></div>
            </td>
        <td>
            &nbsp; &nbsp;
        </td>
        <td>
            <table style="width: 100px">
                <tr>
                    <td style="width: 4px">
                        <asp:Label ID="lblTotaleUI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="90px">Totale U.I.</asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:TextBox ID="txtTotUI" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="15" Style="left: 144px; top: 192px; text-align: right" TabIndex="-1"
                            Width="80px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:Label ID="lblMq" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="90px">Tot. mq riscaldato</asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:TextBox ID="txtTotMq" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="15" Style="left: 144px; top: 192px; text-align: right" TabIndex="-1"
                            Width="80px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="#8080FF" TabIndex="-1" Text="ELENCO EDIFICI ALIMENTATI (Appartenenti ad altri complessi)"
                Width="384px"></asp:Label></td>
        <td>
        </td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <div id="Div2" style="border-right: #0000cc thin solid; border-top: #0000cc thin solid;
                visibility: visible; overflow: auto; border-left: #0000cc thin solid; width: 670px;
                border-bottom: #0000cc thin solid; height: 100px"><asp:DataGrid ID="DataGridEdificiExtra" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Transparent" PageSize="1" Style="z-index: 101; left: 9px" Width="100%" BorderColor="Blue">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Mode="NumericPages" Wrap="False" />
                    <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" Wrap="False" BackColor="WhiteSmoke" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE_UI_AL" HeaderText="U.I.">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE_UI" HeaderText="U.I. (Totali)">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE_MQ_AL" HeaderText="MQ">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TOTALE_MQ" HeaderText="MQ (Totali)">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="UNITA" HeaderText="UNITA' IMMOBILIARI">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Width="20%" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CHK" HeaderText="CHK" Visible="False"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                </asp:DataGrid></div>
            </td>
        <td>
        </td>
        <td>
            <table style="width: 100px">
                <tr>
                    <td style="width: 4px">
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="90px">Totale U.I.</asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:TextBox ID="txtTotUI_Extra" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="15" Style="left: 144px; top: 192px; text-align: right" TabIndex="-1"
                            Width="80px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 8px; top: 144px" TabIndex="-1" Width="90px">Tot. mq riscaldato</asp:Label></td>
                </tr>
                <tr>
                    <td style="width: 4px">
                        <asp:TextBox ID="txtTotMq_Extra" runat="server" Enabled="False" Font-Names="Arial" Font-Size="9pt"
                            MaxLength="15" Style="left: 144px; top: 192px; text-align: right" TabIndex="-1"
                            Width="80px"></asp:TextBox></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table style="width: 57%" id="TAB_BOTTONI">
                <tr>
                    <td style="height: 21px">
            <asp:TextBox ID="txtSel" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="500px"></asp:TextBox></td>
                    <td style="height: 21px">
                        <asp:ImageButton ID="btnAgg" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/btn_Aggiungi.jpg"
                            OnClientClick="document.getElementById('Tab_Termico_Edifici_txtAppareE').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_Edificio').style.visibility='visible';"
                            TabIndex="28" ToolTip="Aggiunge un nuovo edificio" /></td>
                    <td style="height: 21px">
                        <asp:ImageButton ID="btnElimina" runat="server" Height="12px" ImageUrl="~/NuoveImm/btn_Elimina.jpg"
                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloEdificio();"
                            TabIndex="29" ToolTip="Elimina l'edificio selezionato" /></td>
                </tr>
            </table>
        </td>
        <td>
        </td>
        <td>
        </td>
    </tr>
</table>

<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtAppareE"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

<div id="DIV_Edificio" style="left: 16px; width: 800px; position: absolute; top: 232px;
    height: 550px; background-color: whitesmoke; display: block; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);">
    <table style="border-right: blue 2px; border-top: blue 2px; left: 72px;
        border-left: blue 2px; border-bottom: blue 2px; position: absolute;
        top: 104px; background-color: #ffffff; z-index: 102;">
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">
                    <p class="MsoNormal" style="margin: 0cm 0cm 10pt">
                        <strong><span style="color: blue; font-family: 'Arial','sans-serif'">Gestione Edifici
                            non appartenenti al complesso dell'impianto</span></strong></p>
                </span></strong></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table1">
                    <tr>
                        <td>
                            <asp:Label ID="lblComplesso" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Complesso *</asp:Label></td>
                        <td style="width: 156px">
                            <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 56px" TabIndex="1" Width="500px">
                            </asp:DropDownList></td>
                        <td style="width: 3px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 33px">
                            <asp:Label ID="lblEdificio" runat="server" Font-Bold="False" Font-Names="Arial"
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                Width="110px">Edificio</asp:Label></td>
                        <td style="width: 156px; height: 33px">
                            <asp:DropDownList ID="cmbEdificio" runat="server" BackColor="White"
                                Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                top: 56px" TabIndex="1" Width="500px">
                            </asp:DropDownList></td>
                        <td style="width: 3px; height: 33px">
                        </td>
                    </tr>
                </table>
                <br />
                <asp:TextBox ID="txtID" runat="server" Height="16px" Style="left: 640px; top: 200px"
                    TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right" style="height: 22px">
                            <asp:ImageButton ID="btn_Inserisci" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_Termico_Edifici_txtAppareE').value='0';"
                                Style="cursor: pointer" TabIndex="53" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton ID="btn_Chiudi" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_Edificio').style.visibility='hidden';document.getElementById('Tab_Termico_Edifici_txtAppareE').value='0';"
                                Style="cursor: pointer" TabIndex="54" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="352px" ImageUrl="~/ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 24px; background-image: url(../ImmDiv/DivMGrande.png);
        position: absolute; top: 72px" Width="752px" />
</div>


<script type="text/javascript">


if (document.getElementById('Tab_Termico_Edifici_txtAppareE').value!='1') {
document.getElementById('DIV_Edificio').style.visibility='hidden';
}

</script>


