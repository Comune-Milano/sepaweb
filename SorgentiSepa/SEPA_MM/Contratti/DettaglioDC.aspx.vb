
Partial Class Contratti_DettaglioDC
    Inherits System.Web.UI.Page
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            CaricaTabella()
        End If

    End Sub
    Private Sub CaricaTabella()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim Descr As String = ""
            Dim DispIniziale As Double = 0
            Dim Variazioni As Double = 0
            Dim restituiti As Double = 0

            Descr = par.DeCripta(Request.QueryString("D"))
            Me.lblVoceBp.Text = "Dettaglio voce " & Descr

            par.cmd.CommandText = "select TRIM(TO_CHAR(nvl(valore_lordo,0) ,'9G999G999G999G999G990D99')) from siscom_mi.pf_voci_struttura where id_voce = (select min(id) from siscom_mi.pf_voci where id_tipo_utilizzo=2)"
            lblDispIniziale.Text = "Disponibilità Iniziale: " & Format(CDbl(par.IfNull(par.cmd.ExecuteScalar, 0)), "##,##0.00")
            DispIniziale = CDbl(par.IfNull(par.cmd.ExecuteScalar, 0))

            par.cmd.CommandText = "SELECT sum(nvl(importo,0)) FROM siscom_mi.PF_VOCI_DEP_CAUZ WHERE ID_VOCE IN (select id from siscom_mi.pf_voci where id_tipo_utilizzo=2) "
            lblRicevuti.Text = "Elenco incassi ricevuti (Totale: " & Format(CDbl(par.IfNull(par.cmd.ExecuteScalar, 0)), "##,##0.00") & ")"
            Variazioni = CDbl(par.IfNull(par.cmd.ExecuteScalar, 0))

            par.cmd.CommandText = "SELECT rapporti_utenza.cod_contratto,TRIM(TO_CHAR(nvl(importo,0) ,'9G999G999G999G999G990D99')) as importo,TO_CHAR(TO_DATE(SUBSTR(DATA_ORA,1,8),'YYYYmmdd'),'DD/MM/YYYY') AS DATA_INS FROM siscom_mi.PF_VOCI_DEP_CAUZ,siscom_mi.rapporti_utenza WHERE rapporti_utenza.id = id_contratto and ID_VOCE IN (select min(id) from siscom_mi.pf_voci where id_tipo_utilizzo=2) order by DATA_ORA desc, cod_contratto asc"
            Dim dt As New Data.DataTable
            Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd.CommandText, par.OracleConn)
            da.Fill(dt)

            dgvDepCauz.DataSource = dt
            dgvDepCauz.DataBind()

            par.cmd.CommandText = "select sum(nvl(importo_approvato,0)) from siscom_mi.prenotazioni where id_stato=2 and id_voce_pf in (select id from siscom_mi.pf_voci where id_tipo_utilizzo=2) "
            restituiti = CDbl(par.IfNull(par.cmd.ExecuteScalar, 0))
            lblRestituiti.Text = "Totale Restituzioni " & Format(restituiti, "##,##0.00")
            lblDisponibilita.Text = "Disponibilità al " & Format(Now, "dd/MM/yyyy") & " :" & Format(DispIniziale + Variazioni - restituiti, "##,##0.00")


            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            ' par.OracleConn.Close()
            Session.Add("ERRORE", "Provenienza: Risultati Ricerca Dom. Gest.Locatari - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try


    End Sub
End Class
