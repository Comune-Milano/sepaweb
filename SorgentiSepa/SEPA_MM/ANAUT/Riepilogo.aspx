<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Riepilogo.aspx.vb" Inherits="ANAUT_Riepilogo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Ricerca</title>
	
	</head>
	<body bgcolor="#f2f5f1">
	<script type="text/javascript">
	    //document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 670px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Riepilogo 
                        Generale</strong></span><br />
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
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton 
                ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 100; left: 539px; position: absolute; top: 507px" 
                TabIndex="11" ToolTip="Home" />
                                    <asp:label id="lblBando" runat="server" Font-Size="12pt" 
                Font-Names="Arial" Font-Bold="True" 
                
                
                
             style="z-index: 102; left: 15px; position: absolute; top: 60px; width: 163px;">Anagrafe Utenza</asp:label>
                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                <asp:DropDownList id="cmbBando" tabIndex="1" 
                runat="server" Height="35px"  
                
                
                style="border: 1px solid black; z-index: 111; left: 165px; position: absolute; top: 57px" 
                Width="250px" AutoPostBack="True" Font-Names="arial" Font-Size="12pt"></asp:DropDownList>
									<asp:label id="Label5" runat="server" 
                Font-Size="10pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 108; left: 11px; position: absolute; top: 213px">DA VERIFICARE</asp:label>
									<asp:label id="Label6" runat="server" 
                Font-Size="10pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 10px; position: absolute; top: 362px">SOSP. INDAGINE*</asp:label>
	    <table style="width: 640px; position: absolute; height: 119px; top: 87px; left: 12px;">
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 9pt; font-weight: bold">
                <td>
                    TIPO</td>
                <td>
                    TOTALE</td>
                <td>
                    CARICATE DA OPERATORI</td>
                <td>
                    CARICATE AUTOMATICAMENTE</td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    CARICATE</td>
                <td>
                <asp:label id="lblCaricate" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="LBLCARICATEOP" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="lblAutomatiche" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    COMPLETE</td>
                <td>
                <asp:label id="lblComplete" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="lblComplete0" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="lblComplete1" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    INCOMPLETE</td>
                <td>
                <asp:label id="lblIncomplete" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="lblIncomplete0" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="lblIncomplete1" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    DA CANCELLARE</td>
                <td>
                <asp:label id="lblcancellare" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="lblcancellare0" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="lblcancellare1" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
            </tr>
        </table>
        <table style="width: 640px; position: absolute; height: 119px; top: 234px; left: 12px;">
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 9pt; font-weight: bold">
                <td>
                    TIPO</td>
                <td>
                    TOTALE</td>
                <td>
                    CARICATE DA OPERATORI</td>
                <td>
                    CARICATE AUTOMATICAMENTE</td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    CARICATE</td>
                <td>
                <asp:label id="Label1" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label2" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label3" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    COMPLETE</td>
                <td>
                <asp:label id="Label4" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="Label7" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="Label8" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    INCOMPLETE</td>
                <td>
                <asp:label id="Label9" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label10" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label11" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    DA CANCELLARE</td>
                <td>
                <asp:label id="Label12" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="Label13" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="Label14" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
            </tr>
        </table>
        <table style="width: 640px; position: absolute; height: 99px; top: 383px; left: 12px;">
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 9pt; font-weight: bold">
                <td>
                    TIPO</td>
                <td>
                    TOTALE</td>
                <td>
                    CARICATE DA OPERATORI</td>
                <td>
                    CARICATE AUTOMATICAMENTE</td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    CARICATE</td>
                <td>
                <asp:label id="Label15" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label16" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label17" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    INCOMPLETE</td>
                <td>
                <asp:label id="Label21" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label22" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
                <td>
                <asp:label id="Label23" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; " 
                ForeColor="#CC0000">0</asp:label>
                </td>
            </tr>
            <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 8pt">
                <td>
                    DA CANCELLARE</td>
                <td>
                <asp:label id="Label24" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="Label25" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
                <td>
                <asp:label id="Label26" runat="server" 
                Font-Size="9pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102;" 
                ForeColor="#CC0000">0</asp:label>
                                    </td>
            </tr>
        </table>
        </ContentTemplate>
                </asp:UpdatePanel>
		</form>
	    </body>
</html>
