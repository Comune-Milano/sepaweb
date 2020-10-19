
Partial Class CICLO_PASSIVO_CicloPassivo_Plan_ScegliOperatore
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0
        If IsPostBack = False Then
            idPianoF.Value = Request.QueryString("IDP")
            idVoce.Value = Request.QueryString("IDV")

            CaricaStato()
            CaricaOperatori()


        End If
    End Sub

    Function CaricaOperatori()
        Try

            par.OracleConn.Open()
            par.SettaCommand(par)

            Dim s As String = "SELECT ID,COGNOME||' '||NOME AS DESCRIZIONE FROM OPERATORI WHERE MOD_CICLO_P=1 AND (BP_COMPILAZIONE=1 or bp_compilazione_l=1) AND id_ufficio<>0 AND cognome IS NOT NULL AND  ID_CAF=2 AND fl_eliminato='0' AND ID IN (SELECT pf_voci_operatori.id_operatore FROM siscom_mi.pf_voci_operatori,siscom_mi.pf_voci WHERE pf_voci_operatori.id_voce=pf_voci.ID AND pf_voci.id_piano_finanziario=" & idPianoF.Value & ") ORDER BY cognome ASC,nome ASC"
            par.RiempiDList(Me, par.OracleConn, "cmbServizio", s, "DESCRIZIONE", "ID")


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
            End If
            myReader5.Close()

            par.cmd.Dispose()

            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub ImgProcedi_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgProcedi.Click
        Response.Redirect("GestVociOperatore.aspx?IDP=" & idPianoF.Value & "&IDO=" & cmbServizio.SelectedItem.Value)
    End Sub
End Class
