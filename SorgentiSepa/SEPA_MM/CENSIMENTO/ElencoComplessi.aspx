<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoComplessi.aspx.vb" Inherits="NEW_CENSIMENTO_ElencoComplessi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>RisultatiComplessi</title>
	    <style type="text/css">
            #Form1
            {
                width: 785px;
            }
        </style>
        <script type="text/javascript">
    function cerca() {
        if (document.all) {
            finestra = showModelessDialog('Find.htm', window, 'dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
            finestra.focus
            finestra.document.close()
        }
        else if (document.getElementById) {
            self.find()
        }
        else window.alert('Il tuo browser non supporta questo metodo')
    }
</script>
	</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
		<form id="Form1" method="post" runat="server" defaultbutton="btnVisualizza" 
        defaultfocus="btnVisualizza">
            &nbsp;<span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
            Elenco Complessi Immobiliari</strong></span>&nbsp;&nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 725px; position: absolute; top: 572px; height: 20px;" 
                ToolTip="Home" />
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 102; left: 521px; position: absolute; top: 572px; right: 273px;" 
                ToolTip="Visualizza" />
            &nbsp; &nbsp;
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Names="Arial" Font-Size="12pt" MaxLength="100" Style="left: 17px; position: absolute;
                top: 544px; z-index: 2;" Width="632px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
                Style="left: 18px; position: absolute; top: 571px; z-index: 2;" Text="Label"
                Visible="False" Width="624px"></asp:Label>
                        <br />
                        </span><br />
                        <br />
                        <br />
                        <img id="imgCambiaAmm" 
            alt="Ricerca Rapida" onclick="cerca();"
                            src="../Condomini/Immagini/Search_16x16.png" style="left: 773px; cursor: pointer;
                            top: 59px; right: 135px; position: absolute;" /><br />
                        <br />
                        <div style="left: 19px; overflow: auto; width: 749px; position: absolute; top: 58px;
                            height: 480px; right: 156px;">
            <asp:datagrid id="DgvComplessi" runat="server" Font-Names="Arial" AutoGenerateColumns="False" 
                                Font-Size="8pt" Width="96%" PageSize="16" 
                                style="z-index: 105; left: 8px; top: 64px" BackColor="White" Font-Bold="False" 
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                Font-Underline="False" GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_COMPLESSO" HeaderText="CODICE">
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="DENOMINAZIONE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CIVICO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
						</asp:datagrid></div>


        <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility='hidden';
        </script>
	        <asp:HiddenField ID="txtId" runat="server" />


        </form>
        </body>
</html>
