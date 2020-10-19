<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoModelloAU.aspx.vb" Inherits="ANAUT_NuovoModelloAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
<script type="text/javascript" language="javascript">
    window.name = "modal";
        </script>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Modello AU</title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-weight: bold;
            font-family: Arial, Helvetica, sans-serif;
            font-size:10pt;
        }
        .style2
        {
            height: 23px;
        }
        .style3
        {
            height: 20px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server" target="modal">
        <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>

    <div>
   
        <table style="width:100%;">
            <tr style="background-color: #CC0000">
                <td class="style1" style="text-align: center">
                    MODELLO ANAGRAFE UTENZA</td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" style="width:100%;">
                        <tr>
                            <td class="style2">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Anagrafe Utenza"></asp:Label>
                            </td>
                            <td class="style2">
    
                    <asp:DropDownList ID="cmbAU" runat="server" 
                         Font-Names="arial" 
                        Font-Size="10pt" Width="300px" TabIndex="1">
                    </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                &nbsp;&nbsp;</td>
                            <td class="style2">
                                &nbsp;&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Titolo Modello"></asp:Label>
                            </td>
                            <td>
                        <asp:TextBox ID="txtTitolo" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="100" Style="z-index: 113; "
                            TabIndex="2" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp; &nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Note"></asp:Label>
                            </td>
                            <td>
                <asp:TextBox ID="txtNote" runat="server" Font-Names="arial" 
                    Font-Size="8pt" Height="99px" MaxLength="500" TextMode="MultiLine" 
                    Width="300px" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp; &nbsp;</td>
                            <td>
                                &nbsp; &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="style3">
                <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Allega File"></asp:Label>
                            </td>
                            <td class="style3">
        <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt" 
                        TabIndex="4" Width="300px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                &nbsp;</td>
                            <td class="style3">
                <asp:Label ID="Label5" runat="server" Font-Names="arial" 
                    Font-Size="8pt" Text="Il file da allegare deve essere di tipo RTF (Rich Text Format)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style3">
                                &nbsp;</td>
                            <td class="style3">
                <asp:Label ID="Label6" runat="server" Font-Names="arial" 
                    Font-Size="8pt" Text="Dimensione massima 10Mb"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    &nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/img_SalvaModelli.png"
                         
                        TabIndex="5" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img id="imgEsci" alt="Esci" src="../NuoveImm/Img_EsciCorto.png" 
                        onclick="self.close();" style="cursor:pointer" /></td>
            </tr>
        </table>
   
    </div>
     
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            </ContentTemplate>
    </asp:UpdatePanel>
    <p>
        &nbsp;</p>
    <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
   </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('divLoading').style.visibility = 'hidden';
    </script>
</body>
</html>
