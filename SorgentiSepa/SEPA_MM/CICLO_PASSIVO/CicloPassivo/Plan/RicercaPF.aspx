<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPF.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_Immagini_RicercaPF" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita = 1;

function $onkeydown() {

    if (event.keyCode==13) 
    {
        ApriContratto();
    }  
}
   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Elenco Piani Finanziari</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	    <style type="text/css">
            #Form1
            {
                width: 800px;
            }
        </style>
	</head>
	<body bgColor="White">
		<script type="text/javascript">
		   document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../../../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Elenco Piani Finanziari 
                        </strong></span><br />
            
            <asp:DataGrid ID="Datagrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="20" 
                    Style="z-index: 101; left: 15px; width: 762px; position:absolute; top: 66px;" 
                    TabIndex="1" BorderWidth="0px">
                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="INIZIO" HeaderText="INIZIO" ReadOnly="True" 
                        Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="FINE" HeaderText="FINE" ReadOnly="True" 
                        Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="INIZIO">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.INIZIO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="FINE">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.FINE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="STATO">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.STATO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="Navy" Wrap="False" />
                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" Wrap="False" />
            </asp:DataGrid>
            
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
                        <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                            Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                            border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px">Nessuna Selezione</asp:TextBox><br />
                        <asp:HiddenField ID="LBLID" runat="server" />
                        <asp:HiddenField ID="Label3" runat="server" />
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                        <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" 
                            ImageUrl="~/NuoveImm/Falso.png" />
                        
                        <br />
                        <img onclick="document.location.href='../../pagina_home.aspx';" alt="" src="../../../NuoveImm/Img_Home.png" 
                            
                            
                            style="position:absolute; top: 515px; left: 627px; cursor:pointer; height: 20px;"/><img 
                            onclick="ApriPiano();" alt="" src="../../../NuoveImm/Img_Visualizza.png" 
                            style="position:absolute; top: 515px; left: 475px; cursor:pointer;" 
                            id="Visualizza"/><asp:HiddenField ID="H1" runat="server" Value="0" />
                        <asp:HiddenField ID="chiamante" runat="server" />
                        <br />
                    </td>
                </tr>
            </table>
            &nbsp;<asp:label id="LBLPROGR" 
                style="Z-INDEX: 104; LEFT: 404px; POSITION: absolute; TOP: 500px" 
                runat="server" Width="57px" Height="23px" Visible="False" TabIndex="-1">Label</asp:label>
            &nbsp;
            &nbsp;&nbsp;&nbsp;
            
            &nbsp;
        </form>
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        function ApriPiano() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                switch (document.getElementById('chiamante').value) {
                    case '0':
                        {
                            document.location.href = 'Prospetto1.aspx?S=1&P=' + document.getElementById('LBLID').value;
                            break;
                        }
                    case '1':
                        {
                            document.location.href = 'Comp_P1_PF.aspx?ID=' + document.getElementById('LBLID').value;
                            break;
                        }
                    case '2':
                        {
                            document.location.href = 'ConvalidaAler.aspx?IDP=' + document.getElementById('LBLID').value;
                            break;
                        }
                    case '3':
                        {
                            document.location.href = 'Capitoli.aspx?IDP=' + document.getElementById('LBLID').value;
                            break;
                        }
                    case '4':
                        {
                            document.location.href = 'ConvalidaComune.aspx?IDP=' + document.getElementById('LBLID').value;
                            break;
                        }
                    case '5':
                        {
                            window.open('SitOperatori.aspx?IDP=' + document.getElementById('LBLID').value, 'Situazione', '');
                            break;
                        }
                    case '9':
                        {
                            document.location.href = 'AssegnaOperatore.aspx?IDP=' + document.getElementById('LBLID').value;
                            break;
                        }
                    case '11':
                        {
                            document.location.href = 'ScegliOperatore.aspx?IDP=' + document.getElementById('LBLID').value;
                            break;
                        }
                    case '12':
                        {
                            document.location.href = 'SitGenerale.aspx?IDP=' + document.getElementById('LBLID').value;
                            break;
                        }
                }
            }
            else {
                alert('Nessun Piano Finanziario Selezionato!');
            }

        }

    </script>
	</body>
</html>
