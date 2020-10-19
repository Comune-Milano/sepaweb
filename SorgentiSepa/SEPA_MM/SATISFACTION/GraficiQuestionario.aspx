<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GraficiQuestionario.aspx.vb"
    Inherits="SATISFACTION_GraficiQuestionario" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Grafici Questionario</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <style type="text/css">
        #Form1
        {
            width: 800px;
        }
    </style>
</head>
<body bgcolor="White">
    <form id="Form1" method="post" runat="server">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Grafici
                    Schede Questionari </strong></span>
                <br />
                <br />
                <asp:Label ID="Label1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                    Style="z-index: 102; left: 50px; position: absolute; top: 80px; right: 743px;"
                    TabIndex="-1">Servizi</asp:Label>
                <asp:DropDownList ID="ddlServizi" TabIndex="1" runat="server" Style="z-index: 103;
                    position: absolute; top: 80px; left: 124px" BorderStyle="Solid" BorderWidth="1px"
                    AutoPostBack="True" Font-Names="arial" Font-Size="10pt" Width="550px">
                    <asp:ListItem Value="0">---</asp:ListItem>
                    <asp:ListItem Value="1">Servizi di pulizia</asp:ListItem>
                    <asp:ListItem Value="2">Servizi di portierato</asp:ListItem>
                    <asp:ListItem Value="3">Servizi di riscaldamento</asp:ListItem>
                    <asp:ListItem Value="4">Servizi di manutenzione del verde</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                    Style="z-index: 104; left: 50px; position: absolute; top: 104px; right: 736px;"
                    TabIndex="-1">Domande
                </asp:Label>
                <asp:DropDownList ID="ddlDomande" TabIndex="2" runat="server" Style="z-index: 105;
                    position: absolute; top: 104px; right: 736px; left: 124px;" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Arial" Font-Size="10pt" Width="550px">
                </asp:DropDownList>
                <asp:Label ID="Label3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                    Style="z-index: 106; left: 50px; position: absolute; top: 128px; right: 336px;"
                    TabIndex="-1">Risposta
                </asp:Label>
                <asp:DropDownList ID="ddlRisposta" TabIndex="3" runat="server" Style="z-index: 107;
                    position: absolute; top: 128px; right: 638px; left: 124px;" BorderStyle="Solid"
                    BorderWidth="1px" Font-Names="Arial" Font-Size="10pt" Width="180px">
                    <asp:ListItem Selected="True">---</asp:ListItem>
                    <asp:ListItem Value="SI">SI</asp:ListItem>
                    <asp:ListItem Value="AB">AB - Abbastanza</asp:ListItem>
                    <asp:ListItem Value="PC">PC - Poco</asp:ListItem>
                    <asp:ListItem>NO</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                    Style="z-index: 106; left: 50px; position: absolute; top: 152px; right: 336px;"
                    TabIndex="-1">Valore
                </asp:Label>
                <asp:DropDownList ID="ddlValore" TabIndex="3" runat="server" Style="z-index: 107;
                    position: absolute; top: 152px; left: 124px;" BorderStyle="Solid" BorderWidth="1px"
                    Font-Names="Arial" Font-Size="10pt" Width="180px">
                    <asp:ListItem Selected="True">---</asp:ListItem>
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Label ID="Label6" runat="server"></asp:Label>
                <asp:Label ID="Label7" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                    Style="z-index: 106; left: 50px; position: absolute; top: 175px; right: 694px; height: 15px; margin-bottom: 3px;"
                    TabIndex="-1">Codice UI                </asp:Label>
                <asp:TextBox ID="cod" runat="server" Style="z-index: 107; position: absolute; top: 176px;
                    left: 124px;" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="10pt"
                    Width="180px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label5" runat="server" Font-Names="Arial" Font-Size="10pt" Style="text-align: center"
                    Width="100%"></asp:Label>
                <br />
                <asp:Chart ID="Chart1" runat="server" Height="250px" Width="250px">
                    <Series>
                        <asp:Series ChartType="Pie" Name="Series1">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea1">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Chart ID="Chart2" runat="server" Height="250px" Width="250px">
                    <Series>
                        <asp:Series Name="Series1" ChartType="Pie">
                        </asp:Series>
                    </Series>
                    <ChartAreas>
                        <asp:ChartArea Name="ChartArea2">
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
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
                    border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px"></asp:TextBox><br />
                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Falso.png" />
                <br />
                <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
        Style="z-index: 101; left: 466px; position: absolute; top: 515px; right: 204px;"
        TabIndex="25" ToolTip="Avvia Ricerca" />
    </p>
                <img onclick="document.location.href='pagina_home.aspx';" alt="" src="../NuoveImm/Img_Home.png"
                    style="position: absolute; top: 515px; left: 627px; cursor: pointer; height: 20px;" />
            </td>
        </tr>
    </table>
    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
