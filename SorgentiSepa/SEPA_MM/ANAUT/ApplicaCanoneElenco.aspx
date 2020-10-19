<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ApplicaCanoneElenco.aspx.vb" Inherits="ANAUT_ApplicaCanoneElenco" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">

	<head>
    
    		<title>Elenco Applicazioni Canone</title>
		    
       
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Elenco Applicazione Canoni&nbsp; </strong>
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
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                        <asp:HiddenField ID="conferma" runat="server" Value="" />
                        
                    </td>
                </tr>
            </table>
            &nbsp;<img id="img1" alt="Esci" src="../NuoveImm/Img_Home.png" 
                            onclick="Esci()" 
                            
                style="position:absolute;cursor:pointer; top: 506px; left: 583px;"/>
            <asp:label id="LBLPROGR" 
                
                style="Z-INDEX: 104; LEFT: 102px; POSITION: absolute; TOP: 499px; width: 25px;" 
                runat="server" Height="23px" Visible="False">Label</asp:label>

                <div id="contenitore" 
                
                
                
                style="position: absolute; width: 640px; height: 364px; left: 14px; overflow: auto; top: 65px;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 600px;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        CellPadding="2" ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
								<asp:BoundColumn DataField="id" HeaderText="id" ReadOnly="True" 
                                    Visible="False">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="ANAGRAFE" HeaderText="ANAGRAFE UTENZA">
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="NOME_APPLICAZIONE" HeaderText="DESCRIZIONE">
                                </asp:BoundColumn>
							    <asp:TemplateColumn HeaderText="DETTAGLI">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
							    <asp:BoundColumn DataField="ID_TIPO_PROVENIENZA" HeaderText="PROVENIENZA" 
                                    Visible="False"></asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
                        </div>
            </form>
                   <script  language="javascript" type="text/javascript">

                       document.getElementById('dvvvPre').style.visibility = 'hidden';


                       function Esci() {
                           location.href = 'pagina_home.aspx';
                       }

    </script> 
	</body>
</html>

