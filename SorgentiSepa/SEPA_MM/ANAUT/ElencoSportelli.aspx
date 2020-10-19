<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoSportelli.aspx.vb" Inherits="ANAUT_ElencoSportelli" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Elenco Simulazioni</title>
</head>
<body>
    <form id="form1" runat="server" >
        <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 101; left: 576px; position: absolute; top: 493px" 
                        ToolTip="Home" TabIndex="2" />
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
    <div>
        &nbsp;
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg');
            width: 672px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 12pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label1" runat="server" 
                        Text="Sit. Sportelli/Operatori-Fino al 30/09/2012 - "></asp:Label>
                        
                    &nbsp;</strong><asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                    </span>
                    <br />
                    <br />
                    <asp:label id="Label2" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 16px; position: absolute; top: 79px">Sede Territoriale</asp:label>
               
               <asp:label id="Label5" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 16px; position: absolute; top: 464px">Fuori Orario: 8.00-9.00 / 13.00-14.30 / 16.30-19.00</asp:label>
                <asp:DropDownList id="cmbFiliale" tabIndex="1" 
                runat="server" Height="20px"  
                
                
                style="border: 1px solid black; z-index: 111; left: 60px; position: absolute; top: 76px" 
                Width="250px" AutoPostBack="True"></asp:DropDownList>
                    <div id="contenitore" 
                        
                        
                        style="position:absolute; overflow: auto; visibility: visible; top: 108px; left: 14px; width: 620px; height: 350px;">
                                                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt" Style="left: 28px;
                                    position: static; top: 203px" Width="100%" TabIndex="1"></asp:Label>
                    </div>
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
                </td>
            </tr>
        </table>
    
    </div>
                        </ContentTemplate>
                </asp:UpdatePanel>
                
    </form>
</body>
</html>

