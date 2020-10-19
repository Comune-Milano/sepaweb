<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DichiarazioneFO.aspx.vb" Inherits="Contratti_DichiarazioneFO" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">

    var Selezionato;

</script>
<head runat="server">
    <title>Dichiarazione F.O.</title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Seleziona 
                    Dichiarazione ISEE</strong></span><br />
                    <br />
                    &nbsp;<asp:Label ID="Label7" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt"
                        Text="Vengono visualizzate le dichiarazioni in stato &quot;COMPLETA&quot; ed &quot;ELABORATE&quot;."></asp:Label>
                    <br />
                    <br />
                    &nbsp;<br />
                    <a href="../cf/codice.htm" target="_blank"></a>
                    <br />
                    &nbsp;<br />
                    &nbsp;&nbsp;
                    <asp:DataGrid ID="DataGrid1" runat="server"
                        AutoGenerateColumns="False" AllowPaging="True" Font-Names="Arial"
                        Font-Size="8pt"
                        Style="z-index: 105; left: 7px; position: absolute; top: 86px; width: 775px;"
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" PageSize="15">
                        <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="PROGR" HeaderText="PROGRESSIVO">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="PG" HeaderText="PROTOCOLLO">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="COGNOME" HeaderText="COGNOME">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="NOME" HeaderText="NOME">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_NASCITA" HeaderText="DATA_NASCITA" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="DICHIARAZIONE">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA PR.">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CODICE FISCALE">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COGNOME">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOME">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>

                            <asp:BoundColumn Visible="False" DataField="PROGRESSIVO" HeaderText="PG2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="AGGIORNA_DICH"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>

                    &nbsp; &nbsp;
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
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                        <asp:TextBox ID="TextBox7" runat="server" Font-Bold="True"
                            Font-Names="ARIAL" Font-Size="12pt"
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid; left: -1px; top: 45px;"
                            Width="632px"
                            BackColor="#F7F7F7" Height="24px">Nessuna Selezione</asp:TextBox>
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
                    <asp:ImageButton ID="ImgIndietro" runat="server" ImageUrl="~/NuoveImm/Img_Indietro2.png"
                        Style="left: 474px; position: absolute; top: 546px;" TabIndex="21" />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 546px;" TabIndex="21" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 666px; position: absolute; top: 546px; height: 20px;"
                        ToolTip="Home" TabIndex="22" />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 14px; position: absolute; top: 453px; height: 43px; width: 762px;"
                        Visible="False"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
        </table>

        <div>

            <asp:HiddenField ID="LBLID" runat="server" />
            <asp:Button ID="btnRicarica" runat="server"
                Style="display: none;" />
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        function CreaDichiarazione() {

            window.open('../VSA/NuovaDichiarazioneVSA/DichAUnuova.aspx?ID=-1&T=3&FFOO=1&IDD=' + document.getElementById('LBLID').value +'&CODU=<%= Unita %>', 'DichNuovoRU', 'top=250,left=650,toolbar=no, location=no,status=no,menubar=no,scrollbars=yes,resizable=yes');

        }
    </script>
</body>
</html>
