<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultSchede.aspx.vb" Inherits="CENSIMENTO_RisultSchede" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
	<head>
		<title>Risultato Ricerca </title>
        <script type ="text/javascript" >

            function ApriImmobile() {

                if (document.getElementById('txtdesc').value.indexOf("COMPLESSO") != '-1') {
                    parent.main.location.replace('InserimentoComplessi.aspx?SK=' + document.getElementById('scheda').value + '&DSK=' + document.getElementById('txtdata').value + '&C=RisultSchede&ID=' + document.getElementById('id').value);

                }
                if (document.getElementById('txtdesc').value.indexOf("EDIFICIO") != '-1') {
                    parent.main.location.replace('InserimentoEdifici.aspx?SK=' + document.getElementById('scheda').value + '&DSK=' + document.getElementById('txtdata').value + '&C=RisultSchede&ID=' + document.getElementById('id').value);

                }
                if (document.getElementById('txtdesc').value.indexOf("UNIT") != '-1') {
                    parent.main.location.replace('UnitàComEdifici.aspx?SK=' + document.getElementById('scheda').value + '&DSK=' + document.getElementById('txtdata').value + '&C=RisultSchede&ID=' + document.getElementById('id').value);

                }

            }
        
        </script>
	</head>
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
		<form id="Form1" method="post" runat="server">
            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_Grande.png"
                Style="z-index: 103; left: 8px; position: absolute; top: 544px; right: 1093px;" 
                ToolTip="Export in excel" />
            &nbsp;&nbsp;&nbsp;
            <table style="left: 0px; position: absolute; top: 0px; height: 466px; width: 703px;">
                <tr>
                    <td style="width: 670px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; <asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label"></asp:Label>
                        &nbsp;</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <div style="left: 8px; overflow: auto; width: 776px; position: absolute; top: 51px;
                            height: 469px">
            <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False" 
                                AllowPaging="True" Font-Size="8pt" Width="97%" PageSize="200" 
                                style="z-index: 105; left: 193px; top: 54px" BackColor="White" 
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="ID" 
                                    HeaderText="ID">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IMMOBILE" HeaderText="IMMOBILE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_CENSIMENTO" HeaderText="DATA_CENSIMENTO">
                                </asp:BoundColumn>
							</Columns>
							<PagerStyle Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
						</asp:datagrid></div>
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
                    </td>
                </tr>
            </table>
            &nbsp; &nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
                Style="z-index: 2; left: 7px; position: absolute; top: 522px" 
                Width="632px">Nessuna Selezione</asp:TextBox>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
                Style="left: 8px; position: absolute; top: 546px; z-index: 2;" Text="Label"
                Visible="False" Width="624px"></asp:Label>

            <asp:HiddenField ID="id" runat="server" />
            <asp:HiddenField ID="txtdata" runat="server" />
            <asp:HiddenField ID="scheda" runat="server" Value="0" />
            <asp:HiddenField ID="txtdesc" runat="server" />

            
            <img alt="" src="../NuoveImm/Img_NuovaRicerca.png" 
            style="position: absolute; top: 544px; left: 562px; cursor: pointer;"
            onclick="document.location.href='RicercaPerSchedaCens.aspx';" 
                title="Nuova Ricerca" />
            <img alt="" src="../NuoveImm/Img_Home.png" 
            style="position: absolute; top: 544px; left: 714px; cursor: pointer;"
            onclick="document.location.href='pagina_home.aspx';" title="Home" /><img 
                alt="" src="../NuoveImm/Img_Visualizza.png" 
            style="position: absolute; top: 544px; left: 419px; cursor: pointer;"
            onclick="ApriImmobile();" title="Visualizza" /></form>
	</body>
</html>
