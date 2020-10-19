<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoNonIdonei.aspx.vb" Inherits="FSA_ElencoNonIdonei" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Mandati Negativi</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table >
        <tr>
            <td align="center" 
                style="font-family: ARIAL; font-size: 12pt; font-weight: bold">
                ELENCO 
                NON IDONEI</td>
        </tr>
        <tr>
            <td>
    &nbsp;<asp:Image ID="imgExcel" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
        Style="cursor: pointer; " />
                </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td>
    <asp:DataGrid ID="DataGridElenco" runat="server" AutoGenerateColumns="False"
        CellPadding="4" ForeColor="#333333" GridLines="None" Height="147px" Style="z-index: 11;
        left: 18px; top: 81px" >
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <Columns>
            <asp:TemplateColumn HeaderText="PROTOCOLLO">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="CHIAVE ENTE ESTERNO">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.CHIAVE_ENTE_ESTERNO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="COD.FISCALE">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DATA NASCITA">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.DATANASCITA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="RECAPITO">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.PRESSO_REC_DNTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="TIPO INDIRIZZO">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.TIPOVIA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="INDIRIZZO">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IND_REC_DNTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="CIVICO">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO_REC_DNTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="CAP">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.CAP_REC_DNTE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="COMUNE">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.COMUNEDI") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="ISEE">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.REDDITO_ISEE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="ISE">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.ISE_ERP") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="ISP">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.ISP_ERP") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="ISR">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.ISR_ERP") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="PSE">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.PSE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="VSE">
                <ItemTemplate>
                    <asp:Label ID="Label16" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.VSE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="QUOTA COMUNALE">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.quotacomunalepagata") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="QUOTA REGIONALE">
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.quotaregionalepagata") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="TOT. CONTRIBUTO">
                <ItemTemplate>
                    <asp:Label ID="Label19" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.TOTCONTRIBUTO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="DETRAZIONI">
                <ItemTemplate>
                    <asp:Label runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTODETRAZIONE") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTODETRAZIONE") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IBAN">
                <ItemTemplate>
                    <asp:Label ID="Label20" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IBAN") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="IDONEA">
                <ItemTemplate>
                    <asp:Label ID="Label21" runat="server" 
                        Text='<%# DataBinder.Eval(Container, "DataItem.IDONEA") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
        <EditItemStyle BackColor="#2461BF" Font-Names="Arial" Font-Size="9pt" 
            Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
        <SelectedItemStyle BackColor="Gainsboro" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
            ForeColor="#333333" Wrap="False" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
            Wrap="False" />
        <ItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" 
            Font-Size="8pt" Wrap="False" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
            Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" 
            Font-Strikeout="False" Font-Underline="False" Wrap="False" />
    </asp:DataGrid>
                    </td>
        </tr>
    </table>
    
    </div>


        <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
    </body>
</html>
