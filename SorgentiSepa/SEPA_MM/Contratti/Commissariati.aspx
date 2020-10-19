<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Commissariati.aspx.vb" Inherits="Contratti_InteressiLegali" %>
<script type="text/javascript">
    var Uscita;
	Uscita = 1;
</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Commissariati P.S.</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px; height: 537px;">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Commissariati
                        P.S.</strong></span><br />
                    <br />
                    <br />
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;<br />
                    &nbsp;&nbsp;&nbsp;<br />
                    &nbsp; &nbsp;&nbsp;
                    <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                        Style="left: 396px; position: absolute; top: 91px" ToolTip="Elimina la voce selezionata" />
                    &nbsp;<br />
                    <br />
                    &nbsp;<br />
                    &nbsp; &nbsp;<br />
                    &nbsp; &nbsp;
                                        <img alt="Aggiungi Interessi Legali" onclick="document.getElementById('InserimentoLegali').style.visibility='visible';"
                                            src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer; left: 397px; position: absolute; top: 66px;" id="IMG1" />                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    &nbsp;&nbsp;
                    <div style="left: 14px; overflow: auto; width: 373px; position: absolute; top: 56px;
                        height: 450px">
                        <asp:DataGrid ID="DataGridIntLegali" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Height="155px"
                            meta:resourcekey="DataGrid1Resource1" PageSize="100" Style="z-index: 101; left: 483px;
                            top: 68px" Width="348px">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="COMMISSARIA" ReadOnly="True"
                                    Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COMMISSARIATO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit"
                                            meta:resourcekey="LinkButton1Resource1" Text="Seleziona"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" meta:resourcekey="LinkButton3Resource1"
                                            Text="Aggiorna"></asp:LinkButton><asp:LinkButton ID="LinkButton2" runat="server"
                                                CausesValidation="False" CommandName="Cancel" meta:resourcekey="LinkButton2Resource1"
                                                Text="Annulla"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid></div>
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Height="16px" Style="z-index: 104; left: 11px; position: absolute;
                        top: 511px" Visible="False" Width="525px"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                        meta:resourcekey="TextBox3Resource1" 
                        Style="border: 1px solid white; z-index: 5; left: 13px; position: absolute; top: 511px; width: 779px; " 
                        Text="Nessuna Selezione"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <div id="InserimentoLegali" style="display: block; left: 0px; width: 800px; position: absolute;
            top: 0px; height: 573px; background-color: #ffffff; text-align: left">
            <span style="font-family: Arial"></span>
            <br />
            <br />
            <table border="0" cellpadding="1" cellspacing="1" style="left: 243px; width: 48%;
                position: absolute; top: 214px; background-color: silver">
                <tr>
                    <td style="width: 52px; height: 19px; text-align: left">
                        <strong><span style="font-family: Arial">Nuovo</span></strong></td>
                    <td style="width: 274px; height: 19px; text-align: left">
                        <strong><span style="font-family: Arial"></span></strong></td>
                </tr>
                <tr>
                    <td style="width: 52px; height: 19px; text-align: left">
                    </td>
                    <td style="width: 274px; height: 19px; text-align: left">
                        &nbsp;</td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="width: 52px; height: 19px; text-align: left">
                        <span style="font-size: 10pt; font-family: Arial">Descrizione</span></td>
                    <td style="width: 274px; height: 19px; text-align: left">
                        <asp:TextBox ID="txtCommissariato" runat="server" Font-Names="Arial" Font-Size="9pt" Width="293px" MaxLength="50"></asp:TextBox>
                        </td>
                </tr>
                <tr>
                    <td style="width: 52px; height: 19px">
                        &nbsp;</td>
                    <td style="width: 274px; height: 19px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 52px; height: 19px">
                    </td>
                    <td align="right" style="width: 274px; height: 19px; text-align: right">
                        <table border="0" cellpadding="1" cellspacing="1" style="width: 110%">
                            <tr>
                                <td style="text-align: right">
                                    <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png" />&nbsp;<asp:ImageButton
                                            ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                            OnClientClick="document.getElementById('InserimentoLegali').style.visibility='hidden';" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="txtid" runat="server" />
        </div>
    
    </div>
    <script type="text/javascript">
document.getElementById('InserimentoLegali').style.visibility='hidden';
</script>
    </form>
</body>
</html>
