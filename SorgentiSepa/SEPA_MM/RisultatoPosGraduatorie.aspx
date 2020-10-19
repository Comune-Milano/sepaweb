<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoPosGraduatorie.aspx.vb" Inherits="RisultatoPosGraduatorie" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    var Selezionato;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>RisultatoRicDom</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	    <style type="text/css">
            #contenitore
            {
                top: 56px;
                left: 15px;
            }
        </style>
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server" defaultbutton="btnVisualizza" 
        defaultfocus="DataGrid1">
            &nbsp;
			<asp:label id="LBLPROGR" style="Z-INDEX: 101; LEFT: 206px; POSITION: absolute; TOP: 399px" runat="server" Width="57px" Height="23px" Visible="False">Label</asp:label>
            &nbsp;<br />
            <table style="left: 0px; background-image: url(NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Domande Trovate N. </strong>
                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
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
                        <asp:TextBox ID="TextBox3" runat="server" BorderWidth="0px" Font-Bold="True" Font-Names="arial"
                            Font-Size="12pt" ReadOnly="True" Width="657px">Nessuna Selezione</asp:TextBox><br />
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" Value="-1" />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton 
                ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 102; left: 176px; position: absolute; top: 480px" 
                ToolTip="Visualizza" />
                <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                Style="z-index: 102; left: 309px; position: absolute; top: 481px" 
                ToolTip=" dati visualizzati in formato xls" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 103; left: 446px; position: absolute; top: 480px" 
                ToolTip="Nuova Ricerca" />
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 104; left: 589px; position: absolute; top: 480px" />
            &nbsp;
            <div id="contenitore" 
                style="position: absolute; width: 640px; height: 374px; overflow: auto">
						<asp:datagrid id="DataGrid1" runat="server" Width="1600px" Font-Names="Arial" 
                            AutoGenerateColumns="False" Font-Size="8pt" PageSize="200" 
                            style="z-index: 107; left: 0px; position: absolute; top: 0px" BackColor="White" 
                            Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" GridLines="None" CellPadding="0">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Navy"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID_DOMANDA" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME" ReadOnly="True" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NOME" HeaderText="NOME" ReadOnly="True" 
                                    Visible="False"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="GRADUATORIA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.BANDO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="POSIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="COGNOME">
									<ItemTemplate>
										<asp:Label id=Label1 runat="server" Font-Names="ARIAL" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox id=TextBox1 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
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
										<asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="COD. FISCALE">
									<ItemTemplate>
										<asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
										</asp:Label>
									</ItemTemplate>
									<EditItemTemplate>
										<asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
										</asp:TextBox>
									</EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ALLOGGIO">
									<ItemTemplate>
										<asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ALLOGGIO") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ISBARC/R">
									<ItemTemplate>
										<asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ISBARC_R") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="MOT.ESCLUSIONE">
									<ItemTemplate>
										<asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.MOTIVO_ESCLUSIONE") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="TIPO IND.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_VIA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CIVICO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CAP">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CAP") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="ANZIANI">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ANZIANI") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INVALIDI">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INVALIDI") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="FAM.N. FORMAZ.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.F_NUOVA_F") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PROFUGHI">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PROFUGHI") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
                            <AlternatingItemStyle BackColor="Gainsboro" />
						</asp:datagrid>
                        </div>

    <img id="imgCambiaAmm" alt="Ricerca Rapida" onclick="cerca();" 
        src="ASS/Immagini/Search_16x16.png" 
        style="position: absolute; top: 33px; left: 644px; cursor: pointer" /></form>
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

