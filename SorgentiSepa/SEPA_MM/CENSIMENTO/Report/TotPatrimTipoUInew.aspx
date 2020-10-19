<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TotPatrimTipoUInew.aspx.vb" Inherits="CENSIMENTO_Report_TotPatrimTipoUI2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Tot. Patrimoniali per tipo UI</title>
 <style type="text/css">
        .titoli_tabella
        {
            font-size: 8pt;
            font-family: Arial;
            font-weight: bold;
            color: White;
            width: 100%;
            text-align: center;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
    
    <table width="100%">
            <tr>
                <td style="text-align: left; height: 21px;">
                    
                        <asp:ImageButton ID="btnExport" runat="server" ImageUrl="../../NuoveImm/Img_ExportExcel.png"
                            TabIndex="2" ToolTip="Esporta in Excel" />
                    
                </td>
            </tr>
            <tr>
                <td align="center">
                    <span style="font-family: Arial"><strong style="text-align: center">Totalizzazioni patrimoniali
                        per tipo UI </strong></span>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="idcomplessoHidden" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    

    <div style="width:100%; text-align:center">
    <table cellpadding="3" cellspacing="0" border="1px">
        
        <asp:Label ID="TXTtotalizz" runat="server" Text=""></asp:Label>

    </table>
    </div>
    

    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
