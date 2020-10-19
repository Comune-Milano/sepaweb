<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelezionaTipoContratto.aspx.vb" Inherits="Contratti_SelezionaTipoContratto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tipologia Contratto</title>
</head>
<body>
 <form id="form1" runat="server" defaultbutton="ImgProcedi" defaultfocus="ImgProcedi">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Seleziona 
                    Abbinamento Veloce</strong></span><br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<table width="50%">
                                            <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton9" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="1" 
                                    Text="ERP" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton1" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="1" 
                                    Text="USI DIVERSI DALL'ABITATIVO" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton2" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="2" Text="392/78" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton3" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="3" Text="431/98" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton4" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="4" 
                                    Text="O.A." />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton5" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="5" 
                                    Text="FORZE DELL'ORDINE" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton7" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="6" 
                                    Text="CAMBI CONSENSUALI" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton6" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="7" 
                                    Text="431/98 DEROGA ART.15 R.R. 1/2004" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton8" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="8" 
                                    Text="CANONE CONVENZIONATO" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RadioButton10" runat="server" Font-Bold="True" 
                                    Font-Names="ARIAL" Font-Size="10pt" GroupName="A" TabIndex="8" 
                                    Text="CONCESSIONE SPAZI PUBBICITARI" />
                            </td>
                        </tr>
                    </table>
&nbsp;
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 616px; position: absolute; top: 515px; height: 20px;" 
                        TabIndex="9" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        Style="z-index: 101; left: 712px; position: absolute; top: 515px" 
                        ToolTip="Home" TabIndex="10" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>

    
    </form>
        <script type="text/javascript">

            //popupWindow.focus();
    </script>
</body>
</html>
