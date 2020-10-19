<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SimulaApplica1.aspx.vb" Inherits="ANAUT_SimulaApplica1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">

	<head>
    
    		<title>Simulazione</title>
		    
        <script type="text/javascript">
            
            function Conta() {
                var contatore;
                contatore = 0;
                re = new RegExp(':' + document.getElementById('ChSelezionato') + '$')  //generated control
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    elm = document.forms[0].elements[i]
                    if (elm.type == 'checkbox') {
                        if (elm.checked == true) {
                            contatore = contatore + 1;
                        }
                    }
                }
                if (document.all) {
                    document.getElementById('lblSelezionati').innerText = contatore;
                }
                else {
                    document.getElementById('lblSelezionati').textContent = contatore;
                }

            }
        </script>
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
                        Applicazione AU Gruppi.&nbsp; </strong>
                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
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
            &nbsp;<asp:ImageButton 
                ID="btnSelezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_SelezionaTuttiGrande.png"
                Style="z-index: 102; left: 158px; position: absolute; top: 506px" 
                ToolTip="Seleziona Tutti" TabIndex="2" />
                <asp:label id="Label1" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True"                 
                style="z-index: 106; left: 14px; position: absolute; top: 436px; width: 130px; height: 16px; right: 1426px;" 
                ForeColor="Black">Elementi Selezionati:</asp:label>
                <asp:label id="lblSelezionati" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 106; left: 139px; position: absolute; top: 436px; width: 97px; height: 16px; right: 1301px;" 
                ForeColor="Black">0</asp:label>
                <asp:ImageButton ID="btnDeselezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_DeSelezionaTutti.png"
                Style="z-index: 102; left: 14px; position: absolute; top: 506px" 
                ToolTip="Deseleziona tutti" TabIndex="1" />
                <img id="img1" alt="Esci" src="../NuoveImm/Img_Home.png" 
                            onclick="Esci()" 
                            
                style="position:absolute;cursor:pointer; top: 506px; left: 583px;"/>
                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Conferma.png"
                Style="z-index: 103; left: 468px; position: absolute; top: 506px; height: 20px;" 
                ToolTip="Avvia Simulazione" TabIndex="4" onclientclick="Conferma();" />
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
								<asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server" onclick="Conta();"/>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="50px" />
                                </asp:TemplateColumn>
								<asp:BoundColumn DataField="id" HeaderText="id" ReadOnly="True" 
                                    Visible="False">
                                    <HeaderStyle Width="200px" />
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="NOME_GRUPPO" HeaderText="NOME GRUPPO">
                                </asp:BoundColumn>
							    <asp:TemplateColumn HeaderText="DETTAGLI">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DETTAGLI") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
                        </div>
            </form>
                   <script  language="javascript" type="text/javascript">

                       document.getElementById('dvvvPre').style.visibility = 'hidden';

                       //                       document.getElementById('btnProcedi').style.visibility = 'hidden';
                       //                       document.getElementById('btnProcedi').style.position = 'absolute';
                       //                       document.getElementById('btnProcedi').style.left = '-100px';
                       //                       document.getElementById('btnProcedi').style.display = 'none';
                       //                       document.getElementById('dvvvPre').style.visibility = 'hidden';



                       Conta();

                       function Conferma() {
                                   var sicuro = window.confirm('Sei sicuro di voler applicare l\'AU?');
                                   if (sicuro == true) {
                                       document.getElementById('conferma').value = '1';
                                   }
                                   else {
                                       document.getElementById('conferma').value = '0';
                                   }
                               }


                       function Esci() {
                           location.href = 'pagina_home.aspx';
                       }

    </script> 
	</body>
</html>
