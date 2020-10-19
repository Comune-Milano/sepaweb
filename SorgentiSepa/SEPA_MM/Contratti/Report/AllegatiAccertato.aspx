<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AllegatiAccertato.aspx.vb" Inherits="Contratti_Report_AllegatiAccertato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Allegati</title>
	</head>
	<body bgcolor="#f2f5f1" >
	
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Allegati Accertato <asp:Label ID="Label4" runat="server" Text="DD"></asp:Label><br />
                        </strong></span>
                        <br />
                        <br />
                        &nbsp;<br />
                        &nbsp;&nbsp;<br />
                        &nbsp;&nbsp;
                        <br />
                        &nbsp; &nbsp;&nbsp;<br />
                        <br />
                        &nbsp;<br />
                        &nbsp;
                        &nbsp; &nbsp;<br />
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp;<br />
                        &nbsp; &nbsp; &nbsp;
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;<br />
                        &nbsp; &nbsp;
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
                            border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px">Nessuna Selezione</asp:TextBox>
                            <asp:HiddenField ID="LBLID" runat="server" />
                            <asp:HiddenField ID="elimina" runat="server" Value="0" />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 549px" Visible="False" Width="525px"></asp:Label>
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="btnNuovoAllegato" runat="server" ImageUrl="~/NuoveImm/Img_AllegaFile.png"
                Style="z-index: 102; left: 363px; position: absolute; top: 507px" 
                ToolTip="Allega Nuovo File" 
                onclientclick="AllegaFile();" />
                <asp:ImageButton ID="imgElimina" runat="server" ImageUrl="~/NuoveImm/Img_EliminaFile.png"
                Style="z-index: 102; left: 524px; position: absolute; top: 507px" 
                ToolTip="Allega Nuovo File" 
                onclientclick="EliminaFile();" />
           <img onclick="document.location.href='../../Contabilita/pagina_home.aspx';" alt="" src="../../NuoveImm/Img_Home.png" 
                            
                            
                style="position:absolute; top: 505px; left: 720px; cursor:pointer; height: 20px;"/>&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <script type="text/javascript">
                function CompletaData(e, obj) {
                    // Check if the key is a number
                    var sKeyPressed;

                    sKeyPressed = (window.event) ? event.keyCode : e.which;

                    if (sKeyPressed < 48 || sKeyPressed > 57) {
                        if (sKeyPressed != 8 && sKeyPressed != 0) {
                            // don't insert last non-numeric character
                            if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                        }
                    }
                    else {
                        if (obj.value.length == 2) {
                            obj.value += "/";
                        }
                        else if (obj.value.length == 5) {
                            obj.value += "/";
                        }
                        else if (obj.value.length > 9) {
                            var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                            if (selText.length == 0) {
                                // make sure the field doesn't exceed the maximum length
                                if (window.event) {
                                    event.keyCode = 0;
                                }
                                else {
                                    e.preventDefault();
                                }
                            }
                        }
                    }
                }
    </script>
            <div id="ElContratti" 
                
                
                
                
                
                
                style="overflow: auto; position: absolute; width: 776px; height: 371px; top: 70px; left: 12px;">
            <asp:DataGrid ID="Datagrid2" runat="server" 
                    AutoGenerateColumns="False" Font-Bold="False" Font-Italic="False" 
                    Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="200" 
                    Style="z-index: 101; left: 9px; width: 800px;" 
                    TabIndex="1" CellPadding="4" ForeColor="#333333">
                <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" 
                    ForeColor="White" />
                <PagerStyle Wrap="False" BackColor="#2461BF" ForeColor="White" 
                    HorizontalAlign="Center" />
                <AlternatingItemStyle Wrap="False" BackColor="White" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA" ReadOnly="True" 
                        Visible="False">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="TIPOLOGIA">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="DATA EMISSIONE">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.DATA_EMISSIONE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="PERIODO RIFERIMENTO">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.PERIODO_REF") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="ALLEGATO">
                        <ItemTemplate>
                            <asp:Label runat="server" 
                                Text='<%# DataBinder.Eval(Container, "DataItem.ALLEGATO") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    ForeColor="White" Wrap="False" />
                <EditItemStyle Wrap="False" BackColor="#2461BF" />
                <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" 
                    ForeColor="#333333" />
                <ItemStyle Wrap="False" BackColor="#EFF3FB" />
            </asp:DataGrid>
            </div>

		</form>
        <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility = 'hidden';

            function AllegaFile() {
                var dialogResults = window.showModalDialog('NuovoAllegato.aspx?T=99', 'window', 'status:no;dialogWidth:670px;dialogHeight:450px;dialogHide:true;help:no;scroll:no');
                //if ((dialogResults != undefined) && (dialogResults == '1') && (dialogResults != false)) {
                    //document.getElementById('USCITA').value = '0';
                    //document.getElementById('MostrMsgSalva').value = '0';
                    //document.getElementById('imgSalva').click();
                //}
            }

            function EliminaFile() {
                if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sei sicuro di voler cancellare il documento selezionato?");
                    if (chiediConferma == false) {
                        document.getElementById('elimina').value = '0';
                    }
                    else {
                        document.getElementById('elimina').value = '1';
                    }
                }
                else {
                    alert('Nessun File Selezionato!');
                }

            }
    </script>
		</body>
</html>
