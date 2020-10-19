<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaGruppiLavoro.aspx.vb" Inherits="ANAUT_RicercaGruppiLavoro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>RisultatoRicercaD</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	    <style type="text/css">
            #contenitore
            {
                left: 13px;
            }
        </style>
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server" defaultbutton="btnVisualizza">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Gruppi di Lavoro A.U. </strong>
                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                        <img id="imgCambiaAmm" alt="Ricerca Rapida" onclick="cerca();" 
        src="../Contratti/Immagini/Search_16x16.png" 
        style="position: absolute; top: 24px; left: 643px; cursor: pointer" />
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
            <asp:TextBox ID="TextBox7" runat="server" BorderWidth="0px" Font-Bold="True" Font-Names="arial"
                Font-Size="12pt" ReadOnly="True" Width="657px">Nessuna Selezione</asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" Value="-1" />
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 533px; position: absolute; top: 494px; height: 20px;" 
                ToolTip="Home" />
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 102; left: 388px; position: absolute; top: 496px; height: 20px;" 
                ToolTip="Visualizza" />
            <asp:label id="LBLPROGR" style="Z-INDEX: 104; LEFT: 163px; POSITION: absolute; TOP: 308px" runat="server" Width="57px" Height="23px" Visible="False">Label</asp:label>
            <div id="contenitore" 
                style="position: absolute; width: 649px; height: 377px; z-index: 500; overflow: auto; top: 57px;">
            <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                AutoGenerateColumns="False" Font-Size="8pt" Width="670px" 
                PageSize="200" style="z-index: 105; left: 0px; position: absolute; top: 0px" 
                    Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" GridLines="None" CellPadding="2" 
                    ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="nome_gruppo" HeaderText="NOME GRUPPO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="APPLICAZIONE_AU" HeaderText="AU APPLICATA">
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
                        </div>
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
        //popupWindow.focus();
    </script>
	</body>
</html>
