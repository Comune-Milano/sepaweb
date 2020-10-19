Imports System.IO

Partial Class ANAUT_rptDocMancante
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim DT As New Data.DataTable

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        If Not IsPostBack Then
            Try
                par.RiempiDList(Me, par.OracleConn, "cmbBando", "select * from utenza_bandi order by id desc", "DESCRIZIONE", "ID")
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Carica()
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                lblBando.Text = ex.Message
            End Try
        End If
    End Sub

    Protected Sub btnAnnulla_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnAnnulla.Click
        Response.Write("<script>document.location.href=""pagina_home.aspx""</script>")
    End Sub

    Protected Sub cmbBando_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cmbBando.SelectedIndexChanged
        Carica()
    End Sub

    Private Sub Carica()
        Dim Indice As String = cmbBando.SelectedItem.Value
        sStringaSQL1 = "select UTENZA_BANDI.DESCRIZIONE AS AU,utenza_dichiarazioni.pg,RAPPORTI_UTENZA.COD_CONTRATTO,utenza_comp_nucleo.cognome,utenza_comp_nucleo.nome,UTENZA_DOC_NECESSARI.descrizione as doc_mancante," _
            & " ( select cognome || ' '|| nome from utenza_comp_nucleo where id = utenza_doc_mancante.ID_COMP_DOC_MANC) as COMPONENTE_DOC_MANCANTE,UTENZA_DOC_MANCANTE.NOTE_DOC_MANC,utenza_doc_mancante.descrizione AS descrizione, " _
            & " rapporti_utenza.tipo_cor,rapporti_utenza.via_cor,rapporti_utenza.civico_cor,rapporti_utenza.scala_cor,rapporti_utenza.piano_cor,rapporti_utenza.luogo_cor,rapporti_utenza.cap_cor,GetOperatoreAU(UTENZA_DICHIARAZIONI.ID) AS OPERATORE from utenza_doc_mancante,utenza_dichiarazioni,UTENZA_DOC_NECESSARI,utenza_comp_nucleo,siscom_mi.rapporti_utenza,UTENZA_BANDI where UTENZA_BANDI.ID=" & Indice & " AND utenza_dichiarazioni.id=utenza_doc_mancante.id_dichiarazione and UTENZA_DOC_NECESSARI.id=utenza_doc_mancante.id_doc and utenza_comp_nucleo.id_dichiarazione=utenza_dichiarazioni.id and utenza_comp_nucleo.progr=0 and rapporti_utenza.cod_contratto=UTENZA_DICHIARAZIONI.rapporto and utenza_dichiarazioni.id_bando=UTENZA_BANDI.ID"
        bindgrid()
    End Sub

    Private Sub BindGrid()
        Try

            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(sStringaSQL1, par.OracleConn)
            da.Fill(DT)
            DataGrid1.DataSource = DT
            DataGrid1.DataBind()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("MIADT", DT)

        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Redirect("../Errore.aspx", False)

        End Try
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

    Protected Sub btnExport_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExport.Click
        Try
            DT = CType(HttpContext.Current.Session.Item("MIADT"), Data.DataTable)

            If DT.Rows.Count > 0 Then
              
                Dim nomefile As String = par.EsportaExcelDaDTWithDatagrid(DT, DataGrid1, "ExportDocMancante", , False, , False)

                If File.Exists(Server.MapPath("~\FileTemp\") & nomefile) Then
                    Response.Redirect("../FileTemp/" & nomefile)
                Else
                    Response.Write("<script>alert('Si è verificato un errore durante l\'esportazione. Riprovare!')</script>")
                End If
            Else
                Response.Write("<script>alert('Nessun dato da esportare!')</script>")
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class
