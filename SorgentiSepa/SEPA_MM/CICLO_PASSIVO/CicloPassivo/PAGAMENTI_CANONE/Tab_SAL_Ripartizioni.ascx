<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_SAL_Ripartizioni.ascx.vb" Inherits="Tab_SAL_Ripartizioni" %>

<div id="DIV_RIP">
    &nbsp;
    <table style="border-right: blue 2px; border-top: blue 2px; left: 32px;
        border-left: blue 2px; border-bottom: blue 2px; position: absolute;
        top: 80px; background-color: #ffffff; z-index: 102;" id="TABLE1" >
        <tr>
            <td>
                <strong><span style="color: #0000ff; font-family: Arial">Dettaglio Ripartizione</span></strong></td>
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table5">
                        <tr>
                            <td>
                                <asp:Label ID="lblTipologia" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Tipo</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTIPO" runat="server" Font-Size="8pt" MaxLength="300"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px" TabIndex="-1" Width="550px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblDettaglio" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    Width="110px">Denominazione</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtDenominazione" runat="server" Font-Size="8pt"
                                    MaxLength="500" ReadOnly="True" Style="z-index: 10; left: 408px; top: 171px"
                                    TabIndex="-1" Width="550px" Height="50px" TextMode="MultiLine"></asp:TextBox></td>
                        </tr>
                    </table>
                <table>
                    <tr>
                        <td style="height: 5px">
                        </td>
                        <td style="height: 5px">
                        </td>
                        <td style="height: 5px">
                        </td>
                        <td style="width: 30px; height: 5px">
                        </td>
                        <td style="height: 5px">
                        </td>
                        <td style="height: 5px">
                        </td>
                        <td style="height: 5px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 21px">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                Width="150px">IMPORTO APPROVATO</asp:Label></td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtImportoTOT" runat="server" Font-Bold="True"
                                Font-Size="8pt" MaxLength="30" ReadOnly="True" Style="z-index: 10; left: 408px;
                                top: 171px; text-align: right" TabIndex="-1" Width="120px"></asp:TextBox></td>
                        <td style="height: 21px">
                            <asp:Label ID="lblEuro8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label></td>
                        <td style="width: 30px; height: 21px;">
                        </td>
                        <td style="height: 21px">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                Width="200px">IMPORTO RESTANTE DA RIPARTIRE</asp:Label></td>
                        <td style="height: 21px">
                            <asp:TextBox ID="txtImportoRimasto" runat="server" Font-Bold="False" Font-Size="8pt" MaxLength="30"
                                Style="z-index: 10; left: 408px; top: 171px" TabIndex="-1" Width="120px" 
                                Font-Names="Arial" ReadOnly="True"></asp:TextBox></td>
                        <td style="height: 21px">
                            <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td>
                            </td>
                        <td style="width: 30px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                                Width="150px">IMPORTO DA ASSEGNARE</asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtImportoRipartito" runat="server" Font-Bold="True" Font-Size="8pt"
                                MaxLength="30" Style="z-index: 10; left: 408px; top: 171px; text-align: right"
                                Width="120px" Font-Names="Arial"></asp:TextBox></td>
                        <td>
                            <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label></td>
                        <td style="width: 30px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 30px">
                            </td>
                        <td style="height: 30px">
                            </td>
                        <td style="height: 30px">
                            </td>
                        <td style="width: 30px; height: 30px;">
                        </td>
                        <td style="height: 30px">
                        </td>
                        <td style="height: 30px">
                        </td>
                        <td style="height: 30px">
                        </td>
                    </tr>
                </table>
                </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="1" cellspacing="1" style="width: 71%">
                    <tr>
                        <td align="right">
                            <asp:ImageButton ID="btn_Inserisci1" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('Tab_SAL_Ripartizioni_txtAppare1').value='0';"
                                Style="cursor: pointer" TabIndex="1" ToolTip="Salva le modifiche apportate" />
                            <asp:ImageButton ID="btn_Chiudi1" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_RIP').style.visibility='hidden';document.getElementById('Tab_SAL_Ripartizioni_txtAppare1').value='0';"
                                Style="cursor: pointer" TabIndex="2" ToolTip="Esci senza inserire" /></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Image ID="Image1" runat="server" BackColor="White" Height="432px" ImageUrl="../../../ImmDiv/DivMGrande.png"
        Style="z-index: 101; left: 8px;
        position: absolute; top: 56px" Width="776px" />
                            </div>

<asp:TextBox ID="txtAppare1"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
<asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>




<script type="text/javascript">


if (document.getElementById('Tab_SAL_Ripartizioni_txtAppare1').value!='1') {
document.getElementById('DIV_RIP').style.visibility='hidden';
}
</script>

<table id="TABBLE_LISTA">
    <tr>
        <td>
            <table id="Table2">
                <tr>
                    <td><asp:Label ID="lblELENCO_INTERVENTI" runat="server" Font-Bold="True" Font-Names="Arial"
                Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO EDIFICI/IMPIANTI" Width="300px"></asp:Label></td>
                    <td style="width: 250px">
                    </td>
                    <td style="width: 71px">
                    </td>
                    <td>
                        <asp:ImageButton ID="btnRipartisci" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/CICLO_PASSIVO/CicloPassivo/PAGAMENTI_CANONE/Immagini/Img_RipartisceImporti.png"
                            TabIndex="11" ToolTip="Ripartisce gli importi" /></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                overflow: auto; border-left: #0000cc thin solid; width: 750px; border-bottom: #0000cc thin solid;
                height: 150px">
                <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                    BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                    left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                    TabIndex="18" Width="720px">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPO" HeaderText="TIPO">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" ReadOnly="True" Visible="False">
                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                        </asp:BoundColumn>                        
                        <asp:TemplateColumn HeaderText="IMPORTO">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                    Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>' Width="120px"></asp:TextBox>
                                <asp:Label ID="Label3"
                                        runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                            <EditItemTemplate>
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                    ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                    Text="Annulla"></asp:LinkButton>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                    Text="Modifica">Seleziona</asp:LinkButton>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" Wrap="False" />
                </asp:DataGrid></div>
            <asp:TextBox ID="txtSel1" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="704px"></asp:TextBox></td>
    </tr>
</table>



