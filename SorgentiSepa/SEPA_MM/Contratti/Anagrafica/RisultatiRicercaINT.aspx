<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiRicercaINT.aspx.vb" Inherits="Contratti_Anagrafica_RisultatiRicerca" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 0;


    function ImpostaValore() {
        if (document.getElementById('CFISCALE').value != '') {
            opener.CFGlobale1 = document.getElementById('CFISCALE').value;
            opener.IDGlobale1 = document.getElementById('IDC').value;
            opener.ImpostaValore();
            self.close();
        }
        else {
            alert('Selezionare un componente dalla lista!');
        }
    }

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Elenco Contratti</title>
         <base target="_self"/>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	</head>
	<body bgColor="#ffffff">
		<form id="Form1" runat="server" defaultbutton="btnVisualizza">
            &nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Conferma.png"
                Style="z-index: 102; left: 300px; position: absolute; top: 404px" 
                ToolTip="Conferma" meta:resourcekey="btnVisualizzaResource1" 
                TabIndex="1" onclientclick="ImpostaValore();" />
            <table style="left: 1px; background-image: url(../../NuoveImm/SfondoMascheraRubrica.jpg);
                width: 501px; position: absolute; top: 2px; height: 460px; background-attachment: fixed; background-repeat: no-repeat;" id="TABLE1">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="14pt"
                                ForeColor="#660000" Style="z-index: 111; left: 13px; position: absolute; top: 22px"
                                Text="Risultati Ricerca" meta:resourcekey="Label1Resource1"></asp:Label>
                            &nbsp;&nbsp;</strong></span><br />
                        &nbsp;&nbsp;<br />
                        <img onclick="self.close();" alt="" src="../../NuoveImm/Img_EsciCorto.png" 
                            style="cursor:pointer;position:absolute; top: 401px; left: 412px; "/><asp:TextBox 
                            ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="16pt"
                            
                            Style="border: 1px solid white; z-index: 5; left: 8px; position: absolute; top: 366px;" 
                            Width="482px" meta:resourcekey="TextBox3Resource1" 
                            Text="Nessuna Selezione"></asp:TextBox>
                            <div id="Contenitore" 
                            
                            
                            style="position: absolute; width: 472px; height: 295px; top: 65px; left: 13px; overflow: auto">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            PageSize="50" 
                            Style="z-index: 101; left: 0px; position: absolute; top: 0px; width: 723px;" 
                            meta:resourcekey="DataGrid1Resource1" TabIndex="1">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARI" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="FISCALE_PIVA" HeaderText="COD_FISCALE" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="INTESTATARIO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" meta:resourcekey="LabelResource1" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD. FISCALE/P.IVA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" meta:resourcekey="LabelResource2" Text='<%# DataBinder.Eval(Container, "DataItem.FISCALE_PIVA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CONTRATTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        </div>

                        &nbsp;
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Style="z-index: 104; left: 9px; position: absolute;
                            top: 424px; height: 26px;" Visible="False" Width="484px"></asp:Label>
                    </td>
                </tr>
            </table>
            &nbsp;&nbsp;<asp:HiddenField ID="txtid" runat="server" />
            <asp:HiddenField ID="CFISCALE" runat="server" />
            <asp:HiddenField ID="IDC" runat="server" />
        </form>
	</body>
</html>
