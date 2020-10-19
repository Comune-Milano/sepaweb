<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoLettereConvocazioni.aspx.vb" Inherits="ANAUT_ElencoLettereConvocazioni" %>

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
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                            Style="z-index: 101; left: -100px; position: absolute; top: -100px" 
                        ToolTip="Home" TabIndex="2" />
                   
                            <asp:Image ID="imgEsci" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" 
                        onclick="ConfermaEsci();" 
                        
                            style="z-index: 101;position:absolute; top: 485px; left: 586px; cursor:pointer" />
                        <asp:HiddenField ID="nomeFile" runat="server" />
    <div>
        &nbsp;
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg');
            width: 672px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label1" runat="server" Text="Elenco File Convocazioni AU"></asp:Label>
                        
                    </strong></span>
                    <br />
                    <br />
                    <asp:label id="Label2" runat="server" Font-Size="8pt" 
                Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 16px; position: absolute; top: 79px">Sportello</asp:label>
               

                <asp:DropDownList id="cmbFiliale" tabIndex="1" 
                runat="server" Height="20px"  
                
                
                style="border: 1px solid black; z-index: 111; left: 60px; position: absolute; top: 76px" 
                Width="250px" AutoPostBack="True"></asp:DropDownList>
                    <div id="contenitore" 
                        
                        
                        style="position:absolute; overflow: auto; visibility: visible; top: 108px; left: 14px; width: 620px; height: 350px;">
                    <table width="100%">
                        <tr>
                            <td style="width: 3px">
                                <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" Style="left: 28px;
                                    position: static; top: 203px" Width="100%" TabIndex="1"></asp:Label></td>
                        </tr>
                    </table>
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
    <script language="javascript" type="text/javascript">
        function Eliminafile(nomefile) {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Confermi di voler eliminare questo file? Saranno cancellate dal database anche le relative lettere.");
            if (chiediConferma == true) {
                document.getElementById('nomeFile').value = nomefile;
                document.getElementById('btnElimina').click();
            }
        }

        function ConfermaEsci() {
            document.location.href = 'pagina_home.aspx';
        }
    </script>
    
                        
</ContentTemplate>
                 
                </asp:UpdatePanel>
    </form>
</body>
</html>
