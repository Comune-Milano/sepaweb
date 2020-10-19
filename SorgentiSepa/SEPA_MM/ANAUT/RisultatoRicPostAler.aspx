<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoRicPostAler.aspx.vb" Inherits="ANAUT_RisultatoRicPostAler" %>

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
                top: 61px;
            }
        </style>
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Diffide/Ricevute </strong>
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
                        
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton ID="btnAnnulla" 
                runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 598px; position: absolute; top: 486px; height: 20px;" 
                ToolTip="Home" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 103; left: 460px; position: absolute; top: 486px" 
                ToolTip="Nuova Ricerca" />
                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Export_Grande.png"
                Style="z-index: 103; left: 333px; position: absolute; top: 487px" 
                ToolTip="Procedi con la stampa" />
            <asp:label id="LBLPROGR" 
                
                style="Z-INDEX: 104; LEFT: 102px; POSITION: absolute; TOP: 499px; width: 25px;" 
                runat="server" Height="23px" Visible="False">Label</asp:label>
                <div id="contenitore" 
                
                
                style="position: absolute; width: 640px; height: 364px; left: 14px; overflow: auto;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 1252px;" 
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="TIPO DIFFIDA">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="COGNOME">
									<ItemTemplate>
										<asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COD.CONTRATTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SEDE T./SPORTELLO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.FILIALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA DIFFIDA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DATA_GENERAZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA ESITO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ESITO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DESCRIZIONE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
                        </div>
            </form>
                   
	</body>
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
</html>