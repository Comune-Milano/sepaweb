Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums
Imports System.Data.OleDb

Partial Class Contratti_RisultatiDepCauzionali
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim sStringaSql As String
    Dim scriptblock As String

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Dim Str As String
        Response.Expires = 0
        Str = "<div align='center' id='dvvvPre' style='position:absolute; background-color:#ffffff; text-align:center; width:200px; height:100px; top:200px; left:300px; z-index:10; border:1px dashed #660000;"
        Str = Str & "font:verdana; font-size:10px;'><br><img src='Immagini/load.gif' alt='caricamento in corso' ><br>caricamento in corso..."
        Str = Str & "<" & "/div>"

        Response.Write(Str)


        If Not IsPostBack Then
            Response.Flush()
            LBLID.Value = "-1"
            Cerca()
        End If
        ScriptManager.RegisterStartupScript(Page, Me.GetType(), "Key", "<script>MakeStaticHeader('" + DataGrid1.ClientID + "', 350, 776 , 25 ,false); </script>", False)
    End Sub

    Public Property sStringaSQL1() As String
        Get
            If Not (ViewState("par_sStringaSQL1") Is Nothing) Then
                Return CStr(ViewState("par_sStringaSQL1"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_sStringaSQL1") = value
        End Set

    End Property

    Private Function Cerca()
        sStringaSQL1 = Session.Item("DEP")
        BindGrid()
    End Function

    Private Sub BindGrid()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim miocolore As String = "#99CCFF"

            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)

            da.Fill(dt)

            Label4.Text = DataGrid1.Items.Count
            DataGrid1.DataSource = dt
            DataGrid1.DataBind()
            Session.Add("MIADT", dt)
            Dim idVoce As Integer = 0
            Dim Descr As String = ""
            Dim DescrVoce As String = ""

            'par.cmd.CommandText = "SELECT id,codice,descrizione  " _
            '                    & "FROM siscom_mi.PF_VOCI " _
            '                    & "WHERE ID_TIPO_UTILIZZO=2 " _
            '                    & "AND ID_PIANO_FINANZIARIO = " & par.RicavaPianoUltimoApprovato
            'Dim reader As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader
            'If reader.Read Then
            '    idVoce = par.IfNull(reader("id"), "")
            '    Descr = par.IfNull(reader("codice"), "") & " " & par.IfNull(reader("descrizione"), "")
            '    DescrVoce = Descr
            'End If
            'reader.Close()

            'Dim Disponibilita As Double = 0
            'par.cmd.CommandText = "SELECT (NVL (valore_lordo, 0) + NVL (assestamento_valore_lordo, 0) + NVL (variazioni, 0)) - nvl( (SELECT SUM (NVL (importo_approvato, 0)) FROM siscom_mi.prenotazioni WHERE id_stato = 2 AND id_voce_pf IN (SELECT id FROM siscom_mi.pf_voci WHERE id_tipo_utilizzo = 2 AND id_piano_finanziario =(SELECT MAX (id) FROM siscom_mi.pf_main  WHERE id_stato = 5))),0) FROM siscom_mi.pf_voci_struttura  WHERE id_voce IN (SELECT id FROM siscom_mi.pf_voci WHERE id_tipo_utilizzo = 2 AND id_piano_finanziario = (SELECT MAX (id) FROM siscom_mi.pf_main WHERE id_stato = 5))"
            'Disponibilita = CDbl(par.IfNull(par.cmd.ExecuteScalar, 0))
            'Descr = Descr & " €." & Format(Disponibilita, "##,##0.00")
            'Me.lblVoceBp.Text = "<a href=DettaglioDC.aspx?D=" & par.Cripta(DescrVoce) & "&IDVOCE=" & idVoce & " target='_blank'>Disponibilità voce " & Descr

            Label4.Text = dt.Rows.Count

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            ' par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza: Risultati Ricerca Dom. Gest.Locatari - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        If H1.Value = "1" Then
            ExportXLS_Chiama100()
            H1.Value = "0"
        End If
    End Sub

    Private Function ExportXLS_Chiama100()
        Dim dt As New Data.DataTable
        Dim xls As New ExcelSiSol
        Try
            dt = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)
            If dt.Rows.Count > 0 Then


                Dim nomeFile As String = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportDepCauzionali", "Dep.Cauzionali", dt, , , )
                If File.Exists(Server.MapPath("~\FileTemp\") & nomeFile) Then
                    'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('../FileTemp/" & nomeFile & "','_blank','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                    Response.Redirect("../FileTemp/" & nomeFile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception
            Response.Write(ex.Message)
        End Try
    End Function

End Class
