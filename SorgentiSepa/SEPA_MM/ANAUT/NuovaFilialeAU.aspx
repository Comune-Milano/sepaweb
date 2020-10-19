<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovaFilialeAU.aspx.vb" Inherits="ANAUT_NuovaFilialeAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
<base target="_self"/>
    
    <title>Inserisci Sede Territoriale</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-weight: bold;
            font-family: Arial, Helvetica, sans-serif;
            font-size:10pt;
        }
        #CONTENITORE
        {
            left: 13px;
        }
        .style2
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: xx-small;
            font-weight: bold;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
                    <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
        <table style="width:100%;">
            <tr style="background-color: #CC0000">
                <td class="style1" style="text-align: center">
                    STRUTTURE ANAGRAFE UTENZA</td>
            </tr>
            <tr>
            <td class="style2">
            STrutture che potranno operare sull'Anagrafe utenza
            </td>
            </tr>
            </table>

    
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" ForeColor="Red" Visible="False" 
                    style="position:absolute; top: 409px; left: 15px;"></asp:Label>
                    <asp:ImageButton ID="btnSalva" runat="server" 
                        ImageUrl="~/NuoveImm/img_SalvaModelli.png" 
                        style="position:absolute; top: 430px; left: 449px;" />
                <img id="imgEsci" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" 
                        onclick="self.close();" 
                        style="cursor:pointer;position:absolute; top: 430px; left: 533px;" />
   <div id="CONTENITORE" 
        
        
        
        style="overflow: auto; position: absolute; width: 95%; height: 338px; top: 62px">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="8" 
                        
                        style="position:absolute; z-index: 105; left: 0px; width: 100%; top: 0px;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        CellPadding="4" ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
                            	<asp:TemplateColumn HeaderText="NOME">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server"/>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:BoundColumn DataField="NOME">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="ID" HeaderText="ID_FILIALE" Visible="False">
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
                        </div>
                            <p>
        <asp:HiddenField ID="IDBANDO" runat="server" />
    </p>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
        </asp:UpdatePanel>
                        <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
    </form>
</body>
</html>
