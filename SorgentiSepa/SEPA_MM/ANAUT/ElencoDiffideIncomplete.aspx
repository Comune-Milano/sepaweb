<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoDiffideIncomplete.aspx.vb" Inherits="ANAUT_ElencoDiffideIncomplete" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/jquery-1.8.2.js"></script>
<script type="text/javascript" src="../Contratti/jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../Contratti/jquery.corner.js"></script>

	<head>
    <link rel="stylesheet" type="text/css" href="impromptu.css" />
		<title>RisultatoRicercaD</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	    <style type="text/css">
            #contenitore
            {
                top: 107px;
            }
        </style>
	</head>
	<body bgcolor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
&nbsp;</strong><asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                        </span><br />
                        <br />
                        <br />
                        <asp:DropDownList ID="cmbBando" runat="server" 
                            style="position:absolute; top: 65px; left: 45px;" Font-Names="arial" 
                            Font-Size="10pt" AutoPostBack="True" CausesValidation="True">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbVisualizza" runat="server" 
                            style="position:absolute; top: 65px; left: 429px; " Font-Names="arial" 
                            Font-Size="10pt" AutoPostBack="True" CausesValidation="True">
                            <asp:ListItem Selected="True">TUTTE</asp:ListItem>
                            <asp:ListItem>VALIDE</asp:ListItem>
                            <asp:ListItem>ANNULLATE</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        <asp:Label ID="Label10" runat="server" Text="AU:" 
                            style="position:absolute; top: 68px; left: 14px;" Font-Names="arial" 
                            Font-Size="10pt"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="Visualizza:" 
                            style="position:absolute; top: 68px; left: 356px;" Font-Names="arial" 
                            Font-Size="10pt"></asp:Label>
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
                        
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton ID="btnAnnulla" 
                runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 598px; position: absolute; top: 486px; height: 20px;" 
                ToolTip="Home" />
            <asp:label id="LBLPROGR" 
                
                style="Z-INDEX: 104; LEFT: 102px; POSITION: absolute; TOP: 499px; width: 25px;" 
                runat="server" Height="23px" Visible="False">Label</asp:label>
                <div id="contenitore" 
                
                
                
                
                
                style="position: absolute; width: 641px; height: 348px; left: 14px; overflow: auto;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        style="z-index: 105; left: 0px; position: absolute; top: 0px; width: 1344px;" 
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3">
							<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
								<asp:BoundColumn DataField="BANDO" HeaderText="A.U."></asp:BoundColumn>
								<asp:BoundColumn DataField="NOME_FILE" HeaderText="FILE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CREAZIONE" HeaderText="DATA CREAZIONE">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PROTOCOLLO" HeaderText="PROTOCOLLO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CRITERI" HeaderText="CRITERI"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE"></asp:BoundColumn>
							    <asp:BoundColumn DataField="DATA_ANNULLO" HeaderText="DATA ANNULLO">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="ANNULLA"></asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle Mode="NumericPages" BackColor="#999999" ForeColor="Black" 
                                HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                        </div>
            </form>
                   
	</body>
    
</html>
