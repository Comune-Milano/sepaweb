<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DichiarazioneArt15.aspx.vb" Inherits="Contratti_DichiarazioneArt15" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dichiarazione Art.15</title>
</head>
<body>
    <form id="form1" runat="server">
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Seleziona 
                    Domanda ERP Art.15</strong></span><br />
                    <br />
                    &nbsp;<asp:Label ID="Label7" runat="server" Font-Bold="True" 
                        Font-Names="ARIAL" Font-Size="9pt" 
                        
                        Text="Vengono visualizzate le domande con deroga art.15 accolta, con lettera B o lettera A (alloggi lett. B)"></asp:Label>
                    <br />
                    <br />
                    &nbsp;<br />
                    <a href="../cf/codice.htm" target="_blank"></a><br />
                    &nbsp;<br />
                    &nbsp;&nbsp;<asp:datagrid id="DataGrid1" runat="server" 
                        AutoGenerateColumns="False" AllowPaging="True" Font-Names="Arial" 
                        Font-Size="8pt" 
                        style="z-index: 105; left: 7px; position: absolute; top: 86px; width: 775px; " 
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" PageSize="15">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID" ReadOnly="True">
                                </asp:BoundColumn>
								<asp:BoundColumn Visible="False" DataField="PG" HeaderText="PG" ReadOnly="True">
                                </asp:BoundColumn>
								<asp:TemplateColumn HeaderText="PG">
									<ItemTemplate>
										<asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD.FISCALE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ISEE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ISEE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ISE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ISE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="LETT.ART.15">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.LETTERA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid><br />
                    <br />
                    <br />
                    <br />
                    <br />

                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                        <asp:TextBox ID="TextBox7" runat="server" Font-Bold="True" 
                        Font-Names="ARIAL" Font-Size="12pt"
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                            border-bottom: white 1px solid; left: -1px; top: 45px;" Width="632px" 
                            BackColor="#F7F7F7" Height="24px">Nessuna Selezione</asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="LBLID" runat="server" />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 516px; " TabIndex="21" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 666px; position: absolute; top: 515px; height: 20px;" 
                        ToolTip="Home" TabIndex="22" />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 14px; position: absolute;
                        top: 453px; height: 43px; width: 762px;" Visible="False"></asp:Label>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    <div>
    
    </div>
    </form>
                <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>
