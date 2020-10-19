<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraduatoriaOLD.aspx.vb" Inherits="VSA_Graduatoria" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GRADUATORIA CAMBIO IN EMERGENZA (ART.22 C.10 RR 1/2004)</title>
    <style type="text/css">
        #contenitore
        {
            top: 55px;
            left: 14px;
        }

    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Graduatoria (ART.22 C.10 RR 1/2004) </strong>
                        <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                        </span><br />
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
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:TextBox ID="TextBox7" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                            border-bottom: white 1px solid; left: -1px; top: 45px;" Width="643px">Nessuna Selezione</asp:TextBox>
                        <br />
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" />
                        <asp:Button ID="btnVisualizza" runat="server" BackColor="Red" 
            Font-Bold="True" ForeColor="White"
                        Height="32px" TabIndex="2" Text="CONFERMA INVIO ASSEGNAZIONE" 
            
                            
                            style="z-index: 105; left: 3px; position: absolute; top: 496px; right: 424px; width: 247px;" />
                        <asp:Label ID="Label8" runat="server" Font-Names="ARIAL" Font-Size="8pt" 
                            ForeColor="Red" Visible="False"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            
<div id="contenitore" 
                style="position: absolute;  width: 643px; height: 400px; overflow: auto;">

						<asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
            AutoGenerateColumns="False" Font-Size="8pt" PageSize="21" 
            style="z-index: 107; left: 0px; position: absolute; top: 6px; width: 800px;" 
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" GridLines="None" AllowPaging="false">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="PG" HeaderText="PG" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
								<asp:TemplateColumn HeaderText="PG">
									<ItemTemplate>
										<asp:Label ID="Label1" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="DATA R.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DATA_RICHIESTA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="COGNOME">
									<ItemTemplate>
										<asp:Label ID="Label3" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="NOME">
									<ItemTemplate>
										<asp:Label ID="Label4" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="PIANO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PIANO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="AI">
									<ItemTemplate>
										<asp:Label ID="Label6" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.AI") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="RU">
									<ItemTemplate>
										<asp:Label ID="Label9" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.RU") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="RI">
									<ItemTemplate>
										<asp:Label ID="Label10" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.RI") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="AA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.AA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IV">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.IV") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="HM">
                                    <ItemTemplate>
                                        <asp:Label ID="Label13" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.HM") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="HA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label14" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.HA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="HT">
                                    <ItemTemplate>
                                        <asp:Label ID="Label15" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.HT") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="HP">
                                    <ItemTemplate>
                                        <asp:Label ID="Label16" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.HP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="AN">
                                    <ItemTemplate>
                                        <asp:Label ID="Label17" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.AN") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="FS">
                                    <ItemTemplate>
                                        <asp:Label ID="Label18" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.FS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PV">
                                    <ItemTemplate>
                                        <asp:Label ID="Label19" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PV") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="CD">
                                    <ItemTemplate>
                                        <asp:Label ID="Label20" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CD") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="AE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label21" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.AE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
                            <AlternatingItemStyle BackColor="Gainsboro" />
						</asp:datagrid>

                        

    </div>

 
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                Style="z-index: 102; left: 440px; position: absolute; top: 505px" 
                ToolTip="Export in Excel"  />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                
                Style="z-index: 104; left: 578px; position: absolute; top: 504px; height: 20px;" 
                ToolTip="Home" />
    
    </div>
    <img id="imgCambiaAmm" style="position:absolute; top: 467px; left: 637px; cursor:pointer"
            alt="Ricerca Rapida" onclick="cerca();"
                            src="../Condomini/Immagini/Search_16x16.png"  />
    </form>
    		    <script  language="javascript" type="text/javascript">
		        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    <script type="text/javascript">
        function cerca() {
            if (document.all) {
                finestra = showModelessDialog('Find.htm', window, 'dialogLeft:0px;dialogTop:0px;dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                finestra.focus
                finestra.document.close()
            }
            else if (document.getElementById) {
                self.find()
            }
            else window.alert('Il tuo browser non supporta questo metodo')
        }
</script>
</body>
</html>
