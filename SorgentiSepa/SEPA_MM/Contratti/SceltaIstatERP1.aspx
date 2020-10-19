<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SceltaIstatERP1.aspx.vb" Inherits="Contratti_SceltaIstatERP1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Adeguamento Istat</title>
    <style type="text/css">
        .RadUploadProgressArea {
            visibility: visible !important;
            width: 348px !important;
            height: auto !important;
            position: absolute;
            top: 150px;
            left: 172px;
        }
        .auto-style7 {
            z-index: 500;
            width: 653px;
            height: 462px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="imgProcedi" >    
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
        <div class="auto-style7">
            <telerik:RadProgressManager ID="RadProgressManager1" runat="server" />
            <telerik:RadProgressArea ID="RadProgressArea1" runat="server" Language=""
                BackColor="#CCCCCC" HeaderText="Elaborazione file"
                Skin="Web20" CssClass="ExportProgress" 
                style="left: 207px; top: 153px; z-index:500; width: 236px;" 
                ProgressIndicators="TotalProgressBar, TotalProgressPercent">
                <Localization UploadedFiles="Current item" CurrentFileName="Operazione:"></Localization>
            </telerik:RadProgressArea>
        </div>
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;
                        <asp:Label ID="Label7" runat="server" Text="Adeguamento ISTAT ERP"></asp:Label><br />
                    </strong></span>
                    <br />
                    &nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Font-Names="ARIAL" Font-Size="10pt"></asp:Label><br />
                    &nbsp; &nbsp;&nbsp;<br />
                    <div style="left: 14px; overflow: auto; width: 778px; position: absolute; top: 89px;
                        height: 392px">
                        <asp:DataGrid ID="DataGridVarIstat" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            meta:resourcekey="DataGrid1Resource1" PageSize="100" Style="z-index: 101; left: 483px;
                            top: 68px" Width="755px" TabIndex="5">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Mode="NumericPages" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="COD.CONTRATTO" 
                                    ReadOnly="True"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_DECORRENZA" HeaderText="DATA DECORRENZA" 
                                    ReadOnly="True"></asp:BoundColumn>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO" 
                                    ReadOnly="True"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CANONE_ATTUALE" HeaderText="CANONE ATTUALE">
                                </asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                        </asp:DataGrid></div>
                    <br />
                    &nbsp;
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
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        Style="z-index: 101; left: 666px; position: absolute; top: 501px" 
                        ToolTip="Home" TabIndex="4" />
                    <asp:ImageButton ID="imgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="z-index: 101; left: 573px; position: absolute; top: 501px" 
                        ToolTip="Procedi" TabIndex="3" />
                    <asp:ImageButton ID="ImgIndietro" 
                        runat="server" ImageUrl="~/NuoveImm/Img_IndietroGrande.png"
                        Style="z-index: 101; left: 490px; position: absolute; top: 501px" 
                        ToolTip="Indietro" TabIndex="2" CssClass="bottone" />
                        <asp:ImageButton ID="imgExport" 
                        runat="server" ImageUrl="~/NuoveImm/Img_Export.png"
                        Style="z-index: 101; left: 14px; position: absolute; top: 501px" 
                        ToolTip="Indietro" TabIndex="2" CssClass="bottone" />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    <script  language="javascript" type="text/javascript">
        if (document.getElementById('dvvvPre')) {
            document.getElementById('dvvvPre').style.visibility = 'hidden';
        }
    </script>
    </form>
</body>
</html>
