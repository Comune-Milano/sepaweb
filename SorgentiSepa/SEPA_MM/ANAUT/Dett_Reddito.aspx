<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Dett_Reddito.aspx.vb" Inherits="ANAUT_Dett_Reddito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self" />
    <style type="text/css">
        #form1
        {
            width: 800px;
            height: 400px;
        }
        .style1
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 10pt;
        }
        .stileLblTOT
        {
            font-family: Arial;
            font-weight: bold;
            font-size: 9pt;
            text-align: right;
        }
    </style>
    <title>Dettaglio Reddito</title>
    <script language="javascript" type="text/javascript">
        function disabBackSpaceAndCanc(e) {
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 46 || key == 8)
                return false;
            else
                return true;
        }
    </script>
</head>
<body style="background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat;
    width: 810px;">
    <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica222" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%Response.Flush()%>
    <script type="text/javascript">
        window.name = "modal";
    </script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 98%; position: absolute; top: 21px; left: 6px;">
        <tr>
            <td>
                <strong><span style="vertical-align: top; font-size: 14pt; color: #801f1c; font-family: Arial;
                    text-align: center;">Dettaglio Reddito&nbsp; <span style="vertical-align: top; font-size: 14pt;
                        color: #801f1c; font-family: Arial"></span></span></strong>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="Red" Text="Label" Visible="False" Width="580px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td class="style1" style="width: 40%">
                            Componente del Nucleo
                        </td>
                        <td class="style1">
                            Tipologie Reddito
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel0" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cmbComponente" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="90%" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="vertical-align: bottom;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:CheckBoxList ID="chkTipiReddito" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" Width="500px" Enabled="false">
                                        <asp:ListItem style="color: black" Value="DIPEND">Dipendente</asp:ListItem>
                                        <asp:ListItem style="color: #009900" Value="AUTON">No Dipendente</asp:ListItem>
                                        <asp:ListItem style="color: #0066CC" Value="PENS">Pensioni</asp:ListItem>
                                        <asp:ListItem style="color: #CC6600" Value="PENS2">Pensioni Esenti</asp:ListItem>
                                        <asp:ListItem style="color: #669999" Value="NOISEE">Importi non rilevanti ai fini ISEE</asp:ListItem>
                                    </asp:CheckBoxList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td class="style1">
                Reddito
            </td>
        </tr>
        <tr>
            <td>
                <table style="padding-left: 15px; width: 770px; font-family: Arial, Helvetica, sans-serif;
                    font-size: 9pt; color: #FFFFFF; background-color: #0066CC; text-align: center;
                    font-weight: bold; border-collapse: collapse;" cellpadding="4">
                    <tr>
                        <td width="210px" style="border: 1px solid #FFFFFF">
                            DESCRIZIONE
                        </td>
                        <td width="230px" style="border: 1px solid #FFFFFF">
                            NUM.GIORNI.
                        </td>
                        <td width="220px" style="border: 1px solid #FFFFFF">
                            IMPORTO €.
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divGlobale" style="overflow: auto; height: 430px; width: 788px;">
                    <table>
                        <tr>
                            <td>
                                <div id="Dipend" style="border: 1px solid #000000; overflow: auto; width: 765px;
                                    display: none;">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:DataGrid ID="DataGridDipend" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;
                                            width: 100%" BorderColor="Black" BorderStyle="None" CellPadding="2" ShowHeader="False"
                                            GridLines="Horizontal">
                                            <PagerStyle Mode="NumericPages" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <ItemStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-Width="244px">
                                                    <ItemStyle Width="244px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="NUM. GIORNI">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNumGG" runat="server" Font-Names="Arial" Font-Size="8pt" Width="35px"
                                                            Style="text-align: right" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_GG") %>'></asp:TextBox>
                                                        <asp:Label ID="Label15" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="IMPORTO €.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotD" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="75px" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="IDIMPORTI" HeaderText="IDIMPORTI" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                ForeColor="White" />
                                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        </asp:DataGrid>
                                    </span></strong>
                                    <div style="text-align: right; padding: 3px 3px 3px 3px;">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lblTotD" runat="server" Text='TOT. REDD. DIPENDENTE' CssClass="stileLblTOT"></asp:Label>
                                                &nbsp
                                                <asp:TextBox ID="txtTotImporto" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Style="text-align: right" Font-Bold="True" Width="75px"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="calcolaTot_btn" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Auton" style="border: 1px solid #009900; overflow: auto; width: 765px;">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:DataGrid ID="DataGridAuton" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;"
                                            Width="100%" CellPadding="2" ShowHeader="False" GridLines="Horizontal">
                                            <PagerStyle Mode="NumericPages" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <ItemStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-Width="244px">
                                                    <ItemStyle Width="244px"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="NUM. GIORNI">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNumGG" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="35px" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_GG") %>'></asp:TextBox>
                                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="IMPORTO €." ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotA" runat="server"></asp:Label>
                                                        <asp:Image ID="imgCalcola" runat="server" Visible="false" ImageUrl="~/NuoveImm/Img_Calcolatrice.png"
                                                            Width="25" Height="25" Style="cursor: pointer" />
                                                        <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="75px" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" VerticalAlign="Middle" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="IDIMPORTI" HeaderText="IDIMPORTI" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                ForeColor="White" />
                                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        </asp:DataGrid>
                                    </span></strong>
                                    <div style="text-align: right; padding: 3px 3px 3px 3px;">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label1" runat="server" Text='TOT. REDD. AUTONOMO' CssClass="stileLblTOT"></asp:Label>
                                                &nbsp
                                                <asp:TextBox ID="txtImportoA" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                    Width="75px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="calcolaTot_btn" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div id="CalcoloTerreni" style="border: 1px solid #0000FF; visibility: hidden; position: absolute; width: 715px;
                                        height: 239px; background-color: #E9E9E9; top: 218px; left: 37px;">
                                        <table cellpadding="2" cellspacing="2" width="100%">
                                            <tr bgcolor="#0066FF" align="center" style="color: #FFFFFF">
                                                <td align="center" bgcolor="#0066FF">
                                                    CALCOLO REDDITO DA TERRENI
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    &nbsp; &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table style="width: 100%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt" Text="1)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label16" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. Domenicale Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDom1" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label17" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. agrario Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAgr1" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label19" runat="server" Font-Names="arial" Font-Size="8pt" Text="% Proprietà"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPerc1" runat="server" Width="39px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt" Text="2)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label10" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. Domenicale Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDom2" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label11" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. agrario Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAgr2" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label12" runat="server" Font-Names="arial" Font-Size="8pt" Text="% Proprietà"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPerc2" runat="server" Width="39px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="8pt" Text="3)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label14" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. Domenicale Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDom3" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label18" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. agrario Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAgr3" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label20" runat="server" Font-Names="arial" Font-Size="8pt" Text="% Proprietà"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPerc3" runat="server" Width="39px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label21" runat="server" Font-Names="arial" Font-Size="8pt" Text="4)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label22" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. Domenicale Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDom4" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label23" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. agrario Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAgr4" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label24" runat="server" Font-Names="arial" Font-Size="8pt" Text="% Proprietà"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPerc4" runat="server" Width="39px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label25" runat="server" Font-Names="arial" Font-Size="8pt" Text="5)"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label26" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. Domenicale Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtDom5" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label27" runat="server" Font-Names="arial" Font-Size="8pt" Text="Redd. agrario Visura"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAgr5" runat="server" Width="52px"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="Label28" runat="server" Font-Names="arial" Font-Size="8pt" Text="% Proprietà"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPerc5" runat="server" Width="39px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr align="right">
                                                <td>
                                                    <asp:ImageButton ID="imgInserisci" runat="server" ImageUrl="~/NuoveImm/Img_Elabora_Piccolo.png"
                                                        OnClientClick="NascondiDivCalcoloTerreni();" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <img id="imgChiudiDiv" alt="chiudi" src="../NuoveImm/Img_Esci1.png" onclick="NascondiDivCalcoloTerreni();" style="cursor:pointer"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                             <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="Pens" style="border: 1px solid #0066CC; overflow: auto; width: 765px; display: none;">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:DataGrid ID="DataGridPens" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;"
                                            Width="100%" CellPadding="2" ShowHeader="False" GridLines="Horizontal">
                                            <PagerStyle Mode="NumericPages" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <ItemStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-Width="244px">
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="NUM. GIORNI">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNumGG" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="35px" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_GG") %>'></asp:TextBox>
                                                        <asp:Label ID="Label8" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="IMPORTO €.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotP" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="75px" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="IDIMPORTI" HeaderText="IDIMPORTI" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                ForeColor="White" />
                                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        </asp:DataGrid>
                                    </span></strong>
                                    <div style="text-align: right; padding: 3px 3px 3px 3px;">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='TOT. REDD. PENSIONE' CssClass="stileLblTOT"></asp:Label>
                                                &nbsp
                                                <asp:TextBox ID="txtImportoP" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                    Width="75px" Font-Bold="True" ReadOnly="True"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="calcolaTot_btn" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="PensEs" style="border: 1px solid #CC6600; overflow: auto; width: 765px;
                                    display: none;">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:DataGrid ID="DataGridPensEs" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;"
                                            Width="100%" CellPadding="2" ShowHeader="False" GridLines="Horizontal">
                                            <PagerStyle Mode="NumericPages" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <ItemStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-Width="244px">
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="NUM. GIORNI">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNumGG" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="35px" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_GG") %>'></asp:TextBox>
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="IMPORTO €.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotPEs" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="75px" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="IDIMPORTI" HeaderText="IDIMPORTI" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                ForeColor="White" />
                                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        </asp:DataGrid>
                                    </span></strong>
                                    <div style="text-align: right; padding: 3px 3px 3px 3px;">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='TOT. REDD. PENS. ESENTI' CssClass="stileLblTOT"></asp:Label>
                                                &nbsp
                                                <asp:TextBox ID="txtImportoPes" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Style="text-align: right" Font-Bold="True" Width="75px" ReadOnly="True"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="calcolaTot_btn" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div id="NoIsee" style="border: 1px solid #669999; overflow: auto; width: 765px;
                                    display: none;">
                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                        <asp:DataGrid ID="DataGridNoIsee" runat="server" AutoGenerateColumns="False" BackColor="White"
                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="24" Style="z-index: 105;"
                                            Width="100%" CellPadding="2" ShowHeader="False" GridLines="Horizontal">
                                            <PagerStyle Mode="NumericPages" BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                            <ItemStyle ForeColor="Black" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-Width="244px">
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="NUM. GIORNI">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtNumGG" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="35px" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_GG") %>' Visible="False"></asp:TextBox>
                                                        <asp:Label ID="Label6" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="IMPORTO €.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotNoIsee" runat="server"></asp:Label>
                                                        <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                            Width="75px" Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:TextBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="IDIMPORTI" HeaderText="IDIMPORTI" Visible="False"></asp:BoundColumn>
                                            </Columns>
                                            <FooterStyle BackColor="White" ForeColor="#000066" />
                                            <HeaderStyle BackColor="#006699" Font-Bold="True" Font-Names="Arial" Font-Size="9pt"
                                                ForeColor="White" />
                                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                        </asp:DataGrid>
                                    </span></strong>
                                    <div style="text-align: right; padding: 3px 3px 3px 3px;">
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='TOTALE NO ISEE' CssClass="stileLblTOT"></asp:Label>
                                                &nbsp
                                                <asp:TextBox ID="txtImportoNoIsee" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                    Style="text-align: right" Font-Bold="True" Width="75px" ReadOnly="True"></asp:TextBox>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="calcolaTot_btn" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td align="right">
                <table style="width: 100%;">
                    <tr>
                        <td width="67%">
                            <asp:Button ID="calcolaTot_btn" runat="server" Text="Button" Style="visibility: hidden;" />
                        </td>
                        <td style="text-align: right; vertical-align: bottom">
                            <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                ToolTip="Conferma" TabIndex="-1" />
                        </td>
                        <td style="text-align: right; vertical-align: bottom">
                            <img id="exit" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" title="Esci" style="cursor: pointer"
                                onclick="ConfermaEsci()" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="dipendente" runat="server" Value="0" />
    <asp:HiddenField ID="autonomo" runat="server" Value="0" />
    <asp:HiddenField ID="pensione" runat="server" Value="0" />
    <asp:HiddenField ID="pens_esente" runat="server" Value="0" />
    <asp:HiddenField ID="noIsee" runat="server" Value="0" />
    <asp:HiddenField ID="svuotaTxt" runat="server" Value="0" />
    <asp:HiddenField ID="importoDip" runat="server" Value="0" />
    <asp:HiddenField ID="importoAuton" runat="server" Value="0" />
    <asp:HiddenField ID="importoPens" runat="server" Value="0" />
    <asp:HiddenField ID="importoPensEs" runat="server" Value="0" />
    <asp:HiddenField ID="importoNoIsee" runat="server" Value="0" />
    <asp:HiddenField ID="salvaRedditi" runat="server" Value="0" />
    <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
    <asp:HiddenField ID="azzeraTxt" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">


        if (document.getElementById('caric')) {
            document.getElementById('caric').style.visibility = 'hidden';
        }

        var opener = window.dialogArguments;
        window.opener.document.getElementById('caric').style.visibility = 'hidden';

        function Chiudi() {
            //            CloseModal2(document.getElementById('txtModificato').value);
            document.getElementById('txtModificato').value = '0';
            window.close();
        }
        function CloseModal(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }
        function CloseModal2(returnParameter) {
            window.returnValue = returnParameter;
            window.close();
        }
        function CalcolaTotale(txtImporto) {
            if (txtImporto.value != '') {
                document.getElementById('calcolaTot_btn').click()
            }
        }

        function SvuotaTextBox() {
            document.getElementById('svuotaTxt').value = '1';
            document.getElementById('calcolaTot_btn').click();
            document.getElementById('svuotaTxt').value = '0';
        }

        function VisualizzaDiv() {
            var i = 0;
            //chkTipiReddito.checked = true;
            var modulo = document.getElementById('form1').elements;
            for (i = 0; i < modulo.length; i++) {
                if (modulo[i].type == "checkbox") {
                    if (modulo[i].checked == true) {
                        switch (modulo[i].id.substring(15)) {
                            case '0':
                                document.getElementById('Dipend').style.display = 'block';
                                break;
                            case '1':
                                document.getElementById('Auton').style.display = 'block';
                                break;
                            case '2':
                                document.getElementById('Pens').style.display = 'block';
                                break;
                            case '3':
                                document.getElementById('PensEs').style.display = 'block';
                                break;
                            case '4':
                                document.getElementById('NoIsee').style.display = 'block';
                                break;
                            default:
                        }
                    }
                    else {
                        switch (modulo[i].id.substring(15)) {
                            case '0':
                                document.getElementById('Dipend').style.display = 'none';
                                break;
                            case '1':
                                document.getElementById('Auton').style.display = 'none';
                                break;
                            case '2':
                                document.getElementById('Pens').style.display = 'none';
                                break;
                            case '3':
                                document.getElementById('PensEs').style.display = 'none';
                                break;
                            case '4':
                                document.getElementById('NoIsee').style.display = 'none';
                                break;
                            default:
                        }
                    }
                }
            }
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\,]/g
        }

        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            o.value = o.value.replace('.', ',');
        }

        function AutoDecimal2(obj) {

            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a != 'NaN') {
                    if (a.substring(a.length - 3, 0).length >= 4) {
                        var decimali = a.substring(a.length, a.length - 2);
                        var dascrivere = a.substring(a.length - 3, 0);
                        var risultato = '';
                        while (dascrivere.replace('-', '').length >= 4) {
                            risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                        }
                        risultato = dascrivere + risultato + ',' + decimali;
                        //document.getElementById(obj.id).value = a.replace('.', ',')
                        document.getElementById(obj.id).value = risultato;
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace('.', ',');
                    }

                }
                else
                    document.getElementById(obj.id).value = '';
            }
        }
        function SostPuntVirg(e, obj) {
            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            }

        }
        //        function CalcolaTotale(obj, val, totale) {
        //            var qt = document.getElementById('' + obj.id + '').value.replace(",", ".");
        //            var csu = val;
        //            var adTot;

        //            adTot = qt * csu;
        //            adTot = parseFloat(adTot).toFixed(2);
        //            document.getElementById('' + totale.id + '').value = adTot
        //            document.getElementById('' + totale.id + '').value = document.getElementById('' + totale.id + '').value.replace(".", ",");
        //        }

        VisualizzaDiv();

        function ConfermaEsci() {

            if ((document.getElementById('txtModificato').value == '1') || (document.getElementById('txtModificato').value == '111')) {

                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.\nUscire senza salvare causerà la perdita delle modifiche!\nUscire ugualmente? Per non uscire premere ANNULLA.");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value = '111';
                }
                else {
                    if (document.getElementById('caric')) {
                        document.getElementById('caric').style.visibility = 'visible';

                    }
                    Chiudi();
                }
            }
            else {
                if (document.getElementById('caric')) {
                    document.getElementById('caric').style.visibility = 'visible';

                }
                Chiudi();
            }
        }


        function controllaBackspace(e, obj) {
            if (obj != undefined) {
                var sKeyPressed1;
                //var txt = document.getElementById('' + obj.id + '').value;
                if (window.event)
                    sKeyPressed1 = window.event.keyCode;     //IE
                else
                    sKeyPressed1 = e.which;                 //firefox

                if (document.activeElement.isTextEdit == true && document.activeElement.isContentEditable == true) {
                    if ((sKeyPressed1 == 46) || (sKeyPressed1 == 8) || (sKeyPressed1 == 116)) {
                        sKeyPressed1 = 0;
                        obj = '0';
                        //textb.value = obj;
                        document.getElementById('svuotaTxt').value = '1';
                        document.getElementById('azzeraTxt').value = '1';
                        document.getElementById('calcolaTot_btn').click()
                    }
                }
            }
            // return obj;
        }

        //        if (navigator.appName == 'Microsoft Internet Explorer') {
        //            window.document.onkeydown = controllaBackspace;
        //        }
        //        else {
        //            window.document.addEventListener("keydown", controllaBackspace, true);
        //        }
        function VisualizzaDivCalcoloTerreni() {
            if (document.getElementById('CalcoloTerreni')) {
                document.getElementById('CalcoloTerreni').style.visibility = 'visible';
            }
        }

        function NascondiDivCalcoloTerreni() {
            if (document.getElementById('CalcoloTerreni')) {
                document.getElementById('CalcoloTerreni').style.visibility = 'hidden';
            }
        }
    </script>
    </form>
</body>
</html>
