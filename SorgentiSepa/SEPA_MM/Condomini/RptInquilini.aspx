<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptInquilini.aspx.vb" Inherits="Condomini_RptInquilini" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Inquilini Condominio</title>
    <script type ="text/javascript" >
        function doit(){
        if (!window.print){
        alert("Browser non supportato!")
        return
        }
        window.print()
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
  
    <div>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 13px; position: absolute;
            top: 12px" Visible="False" Width="525px"></asp:Label>
        <table width= "100%">
            
            <tr>
                <td style="vertical-align: top; text-align: left">
    
                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
                        
                        
                        TabIndex="2"
                        ToolTip="Esporta in Excel" />
    
                    <asp:ImageButton ID="btnExport0" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png" TabIndex="2"
                        ToolTip="Stampa in PDF" />
                    </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: center">
    
                    <asp:Label ID="lblTitle" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="15pt"
                        Style="z-index: 100; left: 215px; top: 11px" 
                        Width="70%">INQUILINI</asp:Label><br /><br /></td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left">
                <asp:DataGrid ID="DataGridInquilini" runat="server" 
                        AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False"
                    Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                    Font-Underline="False" PageSize="1" Style="z-index: 105; left: 8px;
                    top: 32px" Width="100%" CellPadding="4" ForeColor="#333333">
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="White" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <AlternatingItemStyle BackColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="POSIZIONE BIL.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_BILANCIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="INDIRIZZO">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="CITTA'">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.CITTA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="CAP">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.CAP") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CIVICO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO_COR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="INTERNO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTERNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="COD. UNITA" Visible="False">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SCALA">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PIANO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.PIANO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="TIPO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CF_IVA" HeaderText="C.F./P.IVA"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="MILL.PROP.">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.MIL_PRO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. ASC.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_ASC") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. COMP.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_COMPRO") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. GEST.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_GEST") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MILL. RISC.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.MIL_RISC") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MILL_PRES_ASS" HeaderText="MILL. PRES. ASS">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                Wrap="False" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="FOGLIO" HeaderText="FOGLIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MAPPALE" HeaderText="MAPPALE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="SUB" HeaderText="SUB"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="SOGGETTI" HeaderText="N. COMPONENTI">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="OSPITI" HeaderText="N. OSPITI"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid></td>
            </tr>
        </table>
    
    </div>
  
    <p style="text-align: right">
    
                    <img style="cursor:pointer; visibility: hidden;" alt="Stampa Documento" 
                        src="../NuoveImm/Img_Stampa_Grande.png"                      
                        id="IMG1" onclick ="doit();"/></p>
    </form>
</body>
</html>
