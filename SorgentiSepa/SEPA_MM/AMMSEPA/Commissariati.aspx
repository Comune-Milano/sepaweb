<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Commissariati.aspx.vb" Inherits="AMMSEPA_Commissariati" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Commissariati P.S.</title>
    <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
    <script type="text/javascript">
        var Selezionato;
    </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Commissariati " Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="Immagini/SfondoHome.jpg" height="75px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 80%">
                                            <div style="width: 100%; overflow: auto; height: 480px;">
                                                <asp:DataGrid ID="DataGridIntLegali" runat="server" AutoGenerateColumns="False" BackColor="#F2F5F1"
                                                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                                                    PageSize="100" Width="100%" BorderColor="Navy" BorderStyle="Solid">
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
                                                        <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="COMMISSARIA" ReadOnly="True"
                                                            Visible="False"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="COMMISSARIATO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
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
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td style="width: 20%; vertical-align: top;">
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td>
                                                        <img alt="Aggiungi Interessi Legali" onclick="document.getElementById('TextBox1').value='1';myOpacity.toggle();"
                                                            src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer;" id="IMG1" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgModifica" OnClientClick="document.getElementById('TextBox1').value='2'"
                                                            runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png" ToolTip="Modifica Sede Territoriale"
                                                            EnableTheming="True" CausesValidation="False" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                                                            ToolTip="Elimina la voce selezionata" />
                                                    </td>
                                                </tr>
                                            </table>
                                            <div id="InserimentoLegali" 
                                                style="z-index: 199; position: absolute; top: 154px;
                                                left: 213px; background-image: url('../ImmDiv/SfondoDim1.jpg'); width: 490px; height: 320px;">
                                                                                                <table border="0" cellpadding="1" cellspacing="1" 
                                                                                                    style="height:217px; position: relative; top: 50px; left: 50px;">
                                                    <tr>
                                                        <td style="width: 52px; height: 19px; text-align: left">
                                                            <strong><span style="font-family: Arial">Nuovo</span></strong>
                                                        </td>
                                                        <td style="width: 274px; height: 19px; text-align: left">
                                                            <strong><span style="font-family: Arial">Commissariato</span></strong>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 52px; height: 19px; text-align: left">
                                                        </td>
                                                        <td style="width: 274px; height: 19px; text-align: left">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr style="font-size: 12pt; font-family: Times New Roman">
                                                        <td style="width: 52px; height: 19px; text-align: left">
                                                            <span style="font-size: 10pt; font-family: Arial">Descrizione</span>
                                                        </td>
                                                        <td style="width: 274px; height: 19px; text-align: left">
                                                            <asp:TextBox ID="txtCommissariato" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                                Width="293px" MaxLength="50" TabIndex="1"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 52px; height: 19px">
                                                            &nbsp;
                                                        </td>
                                                        <td style="width: 274px; height: 19px">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 52px; height: 19px">
                                                        </td>
                                                        <td align="right" style="width: 274px; height: 19px; text-align: right">
                                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 110%">
                                                                <tr>
                                                                    <td style="text-align: right">
                                                                        <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                                                            TabIndex="2" Style="height: 16px" />&nbsp;<asp:ImageButton ID="img_ChiudiSchema"
                                                                                runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('InserimentoLegali').style.visibility='hidden';"
                                                                                TabIndex="3" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;
                                <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                                    meta:resourcekey="TextBox3Resource1" Text="Nessuna Selezione" Width="482px" BackColor="#F2F5F1"
                                    BorderWidth="0px"></asp:TextBox>
                            </td>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 50%">
                                            &nbsp;
                                            <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Red" Height="16px" Visible="False" Width="525px"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 15%">
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    <asp:HiddenField ID="txtid" runat="server" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <script type="text/javascript">

        myOpacity = new fx.Opacity('InserimentoLegali', { duration: 200 });
        //myOpacity.hide();

        if (document.getElementById('TextBox1').value != '2') {
            myOpacity.hide();
        }
    </script>
    </form>
</body>
</html>
