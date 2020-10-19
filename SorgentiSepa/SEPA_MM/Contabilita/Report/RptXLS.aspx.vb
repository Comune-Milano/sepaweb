Imports System.IO
Imports Microsoft.VisualBasic
Imports OfficeOpenXml
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports System.Security.Policy
Imports System.Security

Partial Class Contabilita_Report_RptXLS
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Cache.SetExpires(DateTime.Now - New TimeSpan(1, 0, 0))
        Response.Cache.SetLastModified(DateTime.Now)
        Response.Cache.SetAllowResponseInBrowserHistory(False)
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String = "<div align='center' id='dvvvPre' style='position: absolute; background-color: #ffffff; width: 200px; height: 100px; top: 60px; left: 70px; z-index: 10; border: 0px dashed #660000; font-size: 12px;'>" _
                          & "<br /> <img src='../../NuoveImm/gearLoad.gif' alt='Caricamento in corso' /><br />Generazione file...attendere...<br /></div>"
        Response.Write(Str)
        Response.Flush()

        par.OracleConn.Open()
        par.cmd = par.OracleConn.CreateCommand()
        Dim query As String = Session.Item("query")
        Session.Remove("query")
        'query = query.Substring(1, query.LastIndexOf("ANNO") + 3) & query.Substring(528)
        'query = query.Replace("INITCAP(BIMESTRE) AS BIMESTRE,", "")
        'query = query.Replace("cod_intestatario,intestatario,num_bolletta,  INITCAP(BOLLETTAZIONE) AS BOLLETTAZIONE,INITCAP(CAPITOLO) AS CAPITOLO,  COMPETENZA_ACC, ", "")

        query = query.Replace("TRIM(TO_CHAR((NVL(IMPORTO_PAGATO,0)),'999G999G990D99')) AS IMPORTO", " ROUND(NVL(IMPORTO_PAGATO,0),2) AS IMPORTO")
        par.cmd.CommandText = query

        Dim Nome As String = ""
        Nome = Request.QueryString("NOMEFILE")

        Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
        Dim dt As New Data.DataTable
        da.Fill(dt)
        If String.IsNullOrEmpty(Nome) Then
            Nome = "EXPORT"
        End If
        If dt.Rows.Count > 0 Then


            Dim xls As New ExcelSiSol
            Dim nomefile1 As String = ""
            Try


                'nomefile1 = par.EsportaExcelAutomaticoDaGridViewAutogenerato(GridView1, "SelectExport", False, False)
                nomefile1 = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, Nome, Nome, dt, True, "../../FileTemp/", False)
                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile1) Then
                    Response.Write("<script>window.open('../../FileTemp/" & nomefile1 & "','');self.close();</script>")
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If

            Catch ex As Exception
                Response.Write("<script>alert('" & ex.Message & "')</script>")

            End Try


        End If
        par.OracleConn.Close()
        par.OracleConn.ClearAllPools()
    End Sub
End Class

